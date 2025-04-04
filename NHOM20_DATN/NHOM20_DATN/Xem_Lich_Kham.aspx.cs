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

        }

        //load list view
        public void view_List()
        {

            pn_AT.Visible = false;
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
            //string idDoc = "TK001";
            string query_list = "select *  " +
                " from PhieuKham pk  " +
                " JOIN LichKhamBenhNhan lkb ON pk.IDPhieu = lkb.IDPhieu  " +
                " join BenhNhan bn on pk.IDBenhNhan  = bn.IDBenhNhan  " +
                " where pk.IDBacSi = @idDoc " +
                " and TrangThai = @TrangThai " +
                " order by  lkb.NgayKham, lkb.ThoiGianKham";
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
                //string docID = "TK001";
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
            else if (e.CommandName == "Xoa")
            {
                string docId = (string)Session["UserID"];
                //string docId = "TK001";
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string dayWork = commandArgs[0];
                string idPk = commandArgs[1];
                DoctorService.mailCancelAppointment(idPk);
                DoctorService.deleteAppointment(idPk, docId);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Đã gửi mail.', 'success');", true);
                Response.Redirect("Xem_Lich_Kham.aspx");
            }
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
                    Response.Write("<script>alert('Đã gửi mail báo đổi giờ');</script>");
                    Response.Redirect("Xem_Lich_Kham.aspx");

                }
                else
                {

                    Response.Write("<script>alert('Cập nhật thất bại " + idPk + "');</script>");
                    Response.Redirect("Xem_Lich_Kham.aspx");

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