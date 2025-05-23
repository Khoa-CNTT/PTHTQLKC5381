using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Crypto;
using System.Drawing;
using System.Web.Services.Description;

namespace NHOM20_DATN
{
    public partial class Xem_Lich_Kham : System.Web.UI.Page
    {
        LopKetNoi kn = new LopKetNoi();
        DoctorService DoctorService = new DoctorService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                view_List();
            }
            else
            {
                string eventTarget = Request["__EVENTTARGET"];
                string eventArgument = Request["__EVENTARGUMENT"];

                if (eventTarget == "CancelAppointment")
                {
                    string[] args = eventArgument.Split('|');
                    string idPk = args[0];
                    string dayWork = args[1];
                    string reason = args[2];

                    // Call your cancel method with reason
                    CancelAppointmentWithReason(idPk, dayWork, reason);
                }
            }

        }
        private void CancelAppointmentWithReason(string idPk, string dayWork, string reason)
        {
            string docId = (string)Session["UserID"];

            // Gửi mail trước khi xóa dữ liệu
            DoctorService.mailCancelAppointment(idPk, reason);

            // Lấy IDLichSu từ LichSuKham
            string sql_idlsk = "SELECT IDLichSu FROM LichSuKham WHERE IDPhieu = @idPk";
            SqlParameter[] parameters = { new SqlParameter("@idPk", idPk) };

            LopKetNoi kn = new LopKetNoi();
            DataTable dt = kn.docdulieu(sql_idlsk, parameters);

            // Nếu tồn tại IDLichSu thì mới thực hiện xóa HoSoBenhAn
            if (dt.Rows.Count > 0)
            {
                string idLSK = dt.Rows[0]["IDLichSu"].ToString();

                // Thực hiện xóa theo đúng thứ tự
                string sql = @"
        
        UPDATE LichKhamBenhNhan 
        SET TrangThai = 'DaHuy', Ghichu = @reason 
        WHERE IDPhieu = @idPk;

      
        DELETE FROM HoSoBenhAn 
        WHERE IDLSK IN (SELECT IDLichSu FROM LichSuKham WHERE IDPhieu = @idPk);

        DELETE FROM LichSuKham WHERE IDPhieu = @idPk;
        DELETE FROM LichKhamBacSi WHERE IDPhieu = @idPk;
        DELETE FROM PhieuKham WHERE IDPhieu = @idPk;";

                SqlParameter[] pr = new SqlParameter[] {
            new SqlParameter("@idPk", idPk),
            new SqlParameter("@reason", reason),
            new SqlParameter("@idLSK", idLSK)
        };

                int result = kn.CapNhat(sql, pr);

                if (result > 0)
                {
                    string script = @"Swal.fire({ 
            title: 'Thành công!', 
            text: 'Đã hủy lịch và gửi mail thông báo.', 
            icon: 'success', 
            confirmButtonText: 'OK' 
        }).then(() => { window.location.href = 'Xem_Lich_Kham.aspx'; });";

                    ScriptManager.RegisterStartupScript(this, GetType(), "cancelSuccess", script, true);
                }
                else
                {
                    string script = @"Swal.fire({ 
            title: 'Lỗi!', 
            text: 'Thao tác thất bại.', 
            icon: 'error', 
            confirmButtonText: 'OK' 
        });";

                    ScriptManager.RegisterStartupScript(this, GetType(), "cancelError", script, true);
                }
            }
        }

        //load list view
        public void view_List()
        {

            pn_AT.Visible = false;
            string idU = (string)Session["UserID"];
            string sql_LK = @"SELECT 
    lkb.IDPhieu,
    lkb.NgayKham,
    lkb.ThoiGianKham,
    lkb.TrangThai,
    bn.Hoten,
    bn.SoDienThoai
FROM LichKhamBenhNhan lkb
LEFT JOIN PhieuKham pk ON lkb.IDPhieu = pk.IDPhieu
JOIN BenhNhan bn ON lkb.IDBenhNhan = bn.IDBenhNhan
WHERE 
    (pk.IDBacSi = @idBS OR lkb.TrangThai = 'DaHuy')
ORDER BY lkb.NgayKham DESC";

            SqlParameter[] pr = new SqlParameter[] {
        new SqlParameter("@idBS", idU)
    };
            DataTable dt = kn.docdulieu(sql_LK, pr);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridAppointment.DataSource = dt;
                gridAppointment.DataBind();

            }
            else
            {
                string script = @"
            Swal.fire({
                title: 'Thông báo',
                text: 'Chưa có lịch khám nào!',
                icon: 'warning',
                confirmButtonText: 'OK'
            });";

                ScriptManager.RegisterStartupScript(this, GetType(),
                    "noAppointmentWarning", script, true);
            }
        }

        //Filter

        protected void ddl_specialty_selectedindexchanged(object sender, EventArgs e)
        {
            string selectedStatus = filter_specialty.SelectedValue;
            string idBS = (string)Session["UserID"];

            string sql = $@"SELECT 
    lkb.IDPhieu,
    lkb.NgayKham,
    lkb.ThoiGianKham,
    lkb.TrangThai,
    bn.Hoten,          
    bn.SoDienThoai     
FROM LichKhamBenhNhan lkb
LEFT JOIN PhieuKham pk ON lkb.IDPhieu = pk.IDPhieu
INNER JOIN BenhNhan bn ON lkb.IDBenhNhan = bn.IDBenhNhan  
WHERE 
    (pk.IDBacSi = @idBS OR lkb.TrangThai = 'DaHuy') 
    {(string.IsNullOrEmpty(selectedStatus) ? "" : " AND lkb.TrangThai = @status")}";

            // Thêm parameters
            List<SqlParameter> parameters = new List<SqlParameter> {
        new SqlParameter("@idBS", idBS)
    };

            if (!string.IsNullOrEmpty(selectedStatus))
            {
                parameters.Add(new SqlParameter("@status", selectedStatus));
            }

            DataTable dt = kn.docdulieu(sql, parameters.ToArray());

            // Bind data vào GridView
            gridAppointment.DataSource = dt;
            gridAppointment.DataBind();
        }
        public void viewList_Filter(string status)
        {

            string idDoc = (string)Session["UserID"];
            string query_list = "select * " +
                "from PhieuKham pk " +
                "JOIN LichKhamBenhNhan lkb ON pk.IDPhieu = lkb.IDPhieu " +
                "join BenhNhan bn on pk.IDBenhNhan = bn.IDBenhNhan " +
                "where pk.IDBacSi = @idDoc " +
                "and TrangThai = @TrangThai " + // Đã tự động loại trừ 'DaHuy' vì đang filter theo status cụ thể
                "order by lkb.NgayKham, lkb.ThoiGianKham";
            SqlParameter[] sp = new SqlParameter[] {
        new SqlParameter("@TrangThai",status),
         new SqlParameter("@idDoc",idDoc)


            };
            DataTable ds = kn.docdulieu(query_list, sp);
            if (ds.Rows.Count > 0 && ds != null)
            {
                gridAppointment.DataSource = ds;
                gridAppointment.DataBind();

            }
            else
            {
                string script = $@"Swal.fire({{ 
    title: 'Không tìm thấy ', 
    icon: 'warning', 
    confirmButtonText: 'OK' 
}});";
                ScriptManager.RegisterStartupScript(this, GetType(), "uniqueKey", script, true);
            }

        }


        //================change the time=====================
        //=======Command
        protected void gridAppointment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DoiGio")
            {

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string dayWork = commandArgs[0];
                string idPk = commandArgs[1];
                string timeWork = commandArgs[2];

                hiddenIdPk.Value = idPk;
                hiddenOldDay.Value = dayWork;
                hiddenOldTime.Value = timeWork;
                string docID = (string)Session["UserID"];
                
                pn_AT.Visible = true;
                lbl_idPk.Text = idPk;
                //Reset list giờ
                ddl_aT.Items.Clear();

                List<string> availableTime = DoctorService.availableHour(dayWork, docID);
                
                if (!(availableTime.Count > 0))
                {
                    ListItem item = new ListItem("Lịch làm của bạn đã Full", "");
                    ddl_aT.Items.Add(item);
                    return;
                }
                else
                {
                    foreach (string hour in availableTime)
                    {
                        ListItem item = new ListItem(hour, hour);
                        ddl_aT.Items.Add(item);
                    }
                    ddl_aT.SelectedValue = DateTime.Parse(timeWork).ToString("HH:mm");
                }


            }//============Xóa=============

            else if (e.CommandName == "XemTT")
            {
                string idPk = e.CommandArgument.ToString();
               
                string sql = @"
      SELECT bn.HoTen, bn.NgaySinh, bn.GioiTinh, bn.SoDienThoai, bn.DiaChi
      FROM PhieuKham pk
      JOIN BenhNhan bn ON pk.IDBenhNhan = bn.IDBenhNhan
      WHERE pk.IDPhieu = @idPk";
                SqlParameter[] ps = { new SqlParameter("@idPk", idPk) };
                DataTable dt = kn.docdulieu(sql, ps);

                if (dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    lblModal_HoTen.Text = r["HoTen"].ToString();
                    lblModal_NgaySinh.Text = DateTime.Parse(r["NgaySinh"].ToString())
                                                .ToString("dd/MM/yyyy");
                    lblModal_GioiTinh.Text = r["GioiTinh"].ToString();
                    lblModal_SDT.Text = r["SoDienThoai"].ToString();
                    lblModal_DiaChi.Text = r["DiaChi"].ToString();

                   
                    string script = "var myModal = new bootstrap.Modal(document.getElementById('"
                                    + pnlPatientModal.ClientID
                                    + "')); myModal.show();";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                        "ShowPatientModal", script, true);
                }
                else
                {
                  
                    ScriptManager.RegisterStartupScript(this, GetType(),
                        "noPatient", "alert('Không tìm thấy thông tin bệnh nhân.');", true);
                }
            }



        }

        //============PANEL=============
        //========Lưu giờ ==============


        protected void btn_Save_Click(object sender, EventArgs e)
        {

            string timeSelect = ddl_aT.SelectedValue;
            string idPk = hiddenIdPk.Value;
            string oldDAy = hiddenOldDay.Value;
            string oldTime = hiddenOldTime.Value;
            string docID = (string)Session["UserID"];
            //string docID = "TK001";
            if (timeSelect != "")
            {
                int result = DoctorService.updateApointment(idPk, docID, timeSelect);
                if (result != 0)
                {
                    DoctorService.mailChangeAppointment(idPk, oldDAy, oldTime);
                    string js = "showAlert('Thành công. Đã gửi mail báo đổi giờ!', 'success');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", js, true);
                    Response.Redirect("Xem_Lich_Kham.aspx");

                    view_List();
                    pn_AT.Visible = false;
                }
                else
                {
                    string js = "showAlert('Cập nhật thất bại!', 'warning');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", js, true);

                }

            }


        }
        //=======Đóng Panel===============
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            pn_AT.Visible = false;
        }
        //=======Paging
        protected void gridAppointment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string idU = (string)Session["UserID"];
            //string idU = "TK001";
            string sql_LK = "select *  " +
                "from PhieuKham pk " +
                 "JOIN LichKhamBenhNhan lkb ON pk.IDPhieu = lkb.IDPhieu " +
                "join BenhNhan bn on pk.IDBenhNhan  = bn.IDBenhNhan " +
                "where pk.IDBacSi = @idBS " +
                "order by  lkb.NgayKham, lkb.ThoiGianKham";

            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@idBS",idU)
            };
            DataTable dt = kn.docdulieu(sql_LK, pr);
            gridAppointment.DataSource = dt;
            gridAppointment.PageIndex = e.NewPageIndex;
            gridAppointment.DataBind();
        }
        //========Reload


        //=========Search click
        protected void btn_search_Click(object sender, EventArgs e)
        {
            string idU = (string)Session["UserID"]; // ID của bác sĩ đang đăng nhập
            string nameKey = "%" + txt_searching.Text + "%";

            string sql_search = @"SELECT *  
                        FROM PhieuKham pk 
                        JOIN LichKhamBenhNhan lkb ON pk.IDPhieu = lkb.IDPhieu 
                        JOIN BenhNhan bn ON pk.IDBenhNhan = bn.IDBenhNhan 
                        WHERE (pk.HoTen COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @name 
                               OR pk.IDPhieu COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @name 
                               OR CAST(pk.ThoiGianKham AS VARCHAR) LIKE @name 
                               OR CONVERT(VARCHAR, pk.NgayKham, 103) LIKE @name)
                               AND lkb.TrangThai IN ('DaDangKy', 'DangCho')
                               AND pk.IDBacSi = @idBacSi -- CHỈ lấy bệnh nhân của bác sĩ này
                        ORDER BY lkb.NgayKham, lkb.ThoiGianKham";

            SqlParameter[] pr = new SqlParameter[]
            {
        new SqlParameter("@idBacSi", idU), // Thêm tham số ID bác sĩ
        new SqlParameter("@name", nameKey)
            };

            DataTable dt = kn.docdulieu(sql_search, pr);

            if (dt != null && dt.Rows.Count > 0)
            {
                gridAppointment.DataSource = dt;
                gridAppointment.DataBind();
            }
            else
            {
                string script = $@"Swal.fire({{ 
    title: 'Không tìm thấy ', 
    icon: 'warning', 
    confirmButtonText: 'OK' 
}});";
                ScriptManager.RegisterStartupScript(this, GetType(), "uniqueKey", script, true);
            }
        }

    }
}