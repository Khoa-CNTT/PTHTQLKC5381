using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Quan_Ly_Lich_Kham : System.Web.UI.Page
    {
        LopKetNoi ketNoi = new LopKetNoi();
        AppointmentManagerment_Service appointmentManagerment_Service = new AppointmentManagerment_Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                view_ListA();
            }
        }

        public void view_ListA()
        {

            DataTable dt = appointmentManagerment_Service.viewListAppointment();

            if (dt.Rows.Count > 0 && dt != null)
            {
                gridAppointmentManager.DataSource = dt;
                gridAppointmentManager.DataBind();
            }
            else
            {
                string message = "Chưa có lịch khám!";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }

        }


        //=======Comand
        protected void gridAppointmentManager_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DoiGio")
            {
                //get id and day on selected item gridview
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string dayWork = commandArgs[0];
                string idPk = commandArgs[1];
                string docId = commandArgs[2];
                string timeWork = commandArgs[3];
                hiddenIdPk.Value = idPk;
                hiddenOldDay.Value = dayWork;
                hiddenOldTime.Value = timeWork;
                hiddenDocID.Value = docId;
                //      Đổi giờ
                pn_AT.Visible = true;
                string script = "openForm('#chTime_container');";
                ScriptManager.RegisterStartupScript(this, GetType(), "display", script, true);


                lbl_idPk.Text = idPk;
                //set text cho ngày
                var dayCalender = Convert.ToDateTime(dayWork);

                txtCalender.Text = DateTime.Parse(dayWork).ToString("yyyy-MM-dd");

                //Reset giờ
                ddl_aT.Items.Clear();
                List<string> availableTime = appointmentManagerment_Service.availableHour(dayWork, docId);

                //check leng of string 
                if (!(availableTime.Count > 0))
                {
                    ListItem item = new ListItem("Hôm nay bác sĩ đã full lịch", "");
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
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string dayWork = commandArgs[0];
                string idPk = commandArgs[1];
                string docId = commandArgs[2];
                string bnID = commandArgs[3];
                appointmentManagerment_Service.mailCancelAppointmentManager(idPk);
                appointmentManagerment_Service.deleteAppointmentManager(idPk, docId);
                string message = "Đã gửi mail thông báo hủy cho bệnh nhân và bác sĩ";
                string script = "showAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                view_ListA();
                return;

            }
            else if (e.CommandName == "XemTT")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string idBenhNhan = commandArgs[0];
                string sql = "SELECT * FROM BenhNhan WHERE IDBenhNhan = @IDBenhNhan";
                SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@IDBenhNhan", idBenhNhan)
    };

                DataTable dt = ketNoi.docdulieu(sql, parameters);


                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string details = $@"
    <p><strong>Mã bệnh nhân:</strong> {row["IDBenhNhan"]?.ToString() ?? ""}</p>
    <p><strong>Họ tên:</strong> {row["HoTen"]?.ToString() ?? ""}</p>
    <p><strong>Ngày sinh:</strong> {(row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy") : "")}</p>
    <p><strong>Giới tính:</strong> {row["GioiTinh"]?.ToString() ?? ""}</p>
    <p><strong>Số điện thoại:</strong> {row["SoDienThoai"]?.ToString() ?? ""}</p>
    <p><strong>Email:</strong> {row["Email"]?.ToString() ?? ""}</p>
    <p><strong>Địa chỉ:</strong> {row["DiaChi"]?.ToString() ?? ""}</p>";

                    patientDetails.InnerHtml = details;

                    // Hiển thị modal
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModal",
                        "document.getElementById('detailModal').style.display='block';", true);
                    return;
                }






            }



        }
        //======Paging
        protected void gridAppointmentManager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = appointmentManagerment_Service.viewListAppointment();
            gridAppointmentManager.DataSource = dt;
            gridAppointmentManager.PageIndex = e.NewPageIndex;
            gridAppointmentManager.DataBind();
        }


        //=======Filter
        protected void filter_specialty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_item = filter_specialty.SelectedValue;
            if (selected_item == "") view_ListA();
            else viewList_Filter(selected_item);
        }



        public void viewList_Filter(string status)
        {
            string query_list = "select  lkbn.NgayKham, lkbn.ThoiGianKham , lkbn.TrangThai, bn.SoDienThoai, bn.HoTen ,pk.IDPhieu , bs.HoTen as BSKham, bs.IDBacSi ,bn.IDBenhNhan   " +
                "from PhieuKham pk  " +
                " JOIN LichKhamBenhNhan lkbn ON pk.IDPhieu = lkbn.IDPhieu " +
                " join BenhNhan bn on pk.IDBenhNhan  = bn.IDBenhNhan  " +
                "  JOIN LichKhamBacSi lkbs ON pk.IDPhieu = lkbs.IDPhieu  " +
                "   join BacSi bs on pk.IDBacSi = bs.IDBacSi   " +
                "  where lkbn.TrangThai =@TrangThai " +
                " order by  lkbn.NgayKham, lkbn.ThoiGianKham";
            SqlParameter[] sp = new SqlParameter[] {
        new SqlParameter("@TrangThai",status)


            };
            DataTable ds = ketNoi.docdulieu(query_list, sp);
            if (ds.Rows.Count > 0 && ds != null)
            {
                gridAppointmentManager.DataSource = ds;
                gridAppointmentManager.DataBind();

            }
            else
            {
                string message = "Không có thông tin tương ứng";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }

        }






        //=======save button
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string timeselect = ddl_aT.SelectedValue;
            string daySelect = txtCalender.Text.ToString();
            string idPk = hiddenIdPk.Value;
            string oldDAy = hiddenOldDay.Value;
            string oldTime = hiddenOldTime.Value;
            if (timeselect != "")
            {
                int result = appointmentManagerment_Service.updateApointmentManager(idPk, timeselect, daySelect);
                if (result != 0)
                {
                    appointmentManagerment_Service.mailChangeAppointmentManager(idPk, oldDAy, oldTime);
                    string message = "Đã gửi mail thông báo cho bệnh nhân";
                    string script = "showAlert('" + message + "','success');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                    //string script = $"alert('{message}'); window.location='Quan_Ly_Lich_Kham.aspx';";
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "redirect", script, true);
                    view_ListA();

                }
                else
                {
                    string message = "Cập nhật thất bại";
                    string script = "showAlert('" + message + "','error');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                    view_ListA();
                }

            }








        }

        //==========close panel
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            pn_AT.Visible = false;
            string script = "closeForm('#chTime_container');";
            ScriptManager.RegisterStartupScript(this, GetType(), "display", script, true);
        }

        //===========Seacching 
        protected void btn_search_Click(object sender, EventArgs e)
        {
            DataTable dt = appointmentManagerment_Service.searchAppointment(txt_searching.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridAppointmentManager.DataSource = dt;
                gridAppointmentManager.DataBind();
            }
            else
            {
                string message = "Không tìm thấy thông tin";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }

        }

        //==========Set available hour
        protected void txtCalender_TextChanged(object sender, EventArgs e)
        {
            string day_selected = txtCalender.Text.ToString();
            string docID = hiddenDocID.Value;
            DateTime checkDate = DateTime.Parse(day_selected);


            //bool checkDay = checkDate.Day > DateTime.Now.Day;
            //bool checkMonth = checkDate.Month > DateTime.Now.Month;
            //bool checkYear = checkDate.Year > DateTime.Now.Year;

            if (checkDate <= DateTime.Now.Date)
            {
                txtCalender.Text = DateTime.Parse(hiddenOldDay.Value).ToString("yyyy-MM-dd");

                string message = "Môc";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }
            List<string> availableHour = appointmentManagerment_Service.availableHour(docID, day_selected);
            if (availableHour.Count > 0)
            {
                foreach (string a in availableHour)
                {
                    ListItem item = new ListItem(a, a);
                    ddl_aT.Items.Add(item);
                }
            }
            else
            {
                ListItem item = new ListItem("Lịch khám ngày này đã full", "");
                ddl_aT.Items.Add(item);
            }

        }


    }
}