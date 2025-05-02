using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            // Cập nhật trạng thái thành 'DaHuy' thay vì xóa
            string sql = @"UPDATE LichKhamBenhNhan 
                  SET TrangThai = 'DaHuy', Ghichu = @reason 
                  WHERE IDPhieu = @idPk";

            SqlParameter[] pr = new SqlParameter[] {
        new SqlParameter("@idPk", idPk),
        new SqlParameter("@reason", reason) // Lưu lý do hủy vào Ghichu
    };

            int result =kn.CapNhat(sql, pr);

            if (result > 0)
            {
                DoctorService.mailCancelAppointment(idPk, reason);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Đã hủy lịch và gửi mail thông báo.', 'success');", true);
                Response.Redirect("Xem_Lich_Kham.aspx");
            }
        }
        //load list view
        public void view_List()
        {

            pn_AT.Visible = false;
            string idU = (string)Session["UserID"];
            string sql_LK = "select * " +
                "from PhieuKham pk " +
                "JOIN LichKhamBenhNhan lkb ON pk.IDPhieu = lkb.IDPhieu " +
                "join BenhNhan bn on pk.IDBenhNhan = bn.IDBenhNhan " +
                "where pk.IDBacSi = @idBS AND lkb.TrangThai <> 'DaHuy' " + // Chỉ hiển thị các trạng thái khác 'DaHuy'
                "order by lkb.NgayKham, lkb.ThoiGianKham";

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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Chưa có lịch khám nào.', 'warning');", true);
            }
        }

        //Filter

        protected void ddl_specialty_selectedindexchanged(object sender, EventArgs e)
        {
            string selected_item = filter_specialty.SelectedValue;
            if (selected_item == "") view_List();
            else viewList_Filter(selected_item);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Không có thông tin.', 'warning');", true);
            }

        }


        //================change the time=====================
        //=======Command
        protected void gridAppointment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DoiGio")
            {
                //get id and day on selected item gridview
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string dayWork = commandArgs[0];
                string idPk = commandArgs[1];
                string timeWork = commandArgs[2];
                //set value for hidden timework, daywork, idPk
                hiddenIdPk.Value = idPk;
                hiddenOldDay.Value = dayWork;
                hiddenOldTime.Value = timeWork;
                string docID = (string)Session["UserID"];
                
                pn_AT.Visible = true;
                lbl_idPk.Text = idPk;
                //Reset list giờ
                ddl_aT.Items.Clear();

                List<string> availableTime = DoctorService.availableHour(dayWork, docID);
                //check leng of string 
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
                Response.Redirect("Xem_Lich_Kham.aspx");
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
        protected void reload_Btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Xem_Lich_Kham.aspx");
        }

        //=========Search click
        protected void btn_search_Click(object sender, EventArgs e)
        {

            string idU = (string)Session["UserID"];
            //string idU = "TK001";
            string nameKey = "%" + txt_searching.Text + "%";
            string sql_search = "select *  " +
                "from PhieuKham pk " +
                 "JOIN LichKhamBenhNhan lkb ON pk.IDPhieu = lkb.IDPhieu " +
                "join BenhNhan bn on pk.IDBenhNhan  = bn.IDBenhNhan " +
                "where (pk.HoTen COLLATE SQL_Latin1_General_CP1_CI_AI like @name " +
                "or pk.IDPhieu COLLATE SQL_Latin1_General_CP1_CI_AI like @name " +
                "or CAST(pk.ThoiGianKham AS VARCHAR) LIKE @name " +
                "or CONVERT(VARCHAR, pk.NgayKham, 103) LIKE  @name ) " +
                "order by  lkb.NgayKham, lkb.ThoiGianKham";
            SqlParameter[] pr = new SqlParameter[]
            {
                 new SqlParameter("@idBS",idU),
                new SqlParameter("@name",nameKey)

            };
            DataTable dt = kn.docdulieu(sql_search, pr);
            if (dt.Rows.Count > 0 && dt != null)
            {
                gridAppointment.DataSource = dt;
                gridAppointment.DataBind();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Không tìm thấy dữ liệu.', 'warning');", true);
            }

        }
    }
}