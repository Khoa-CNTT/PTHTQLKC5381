using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Doctor
{
    public partial class Cap_Nhat_Ho_So_Benh_An : System.Web.UI.Page
    {
        MedicalRecordService medicalRecordService = new MedicalRecordService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        public void loadData()
        {
            string idbs = (string)Session["UserID"];
            DataTable dtHS = new DataTable();
            dtHS = medicalRecordService.getAll(idbs);
            gridMedicalRecord.DataSource = dtHS;
            gridMedicalRecord.DataBind();
        }

        //      BTN EDIT
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            gridMedicalRecord.Columns[0].Visible = true;
            btnEdit.Visible = false;
            cancelEdit.Visible = true;



        }
        //      CANCEL EDITING
        protected void cancelEdit_Click(object sender, EventArgs e)
        {
            gridMedicalRecord.Columns[0].Visible = false;
            btnEdit.Visible = true;
            cancelEdit.Visible = false;

        }


        //Command event
        protected void gridMedicalRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editSelect")
            {

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string idHS = commandArgs[0];
                string idBN = commandArgs[1];
                string hoten = commandArgs[2];
                string chandoan = commandArgs[3];
                string donthuoc = commandArgs[4];
                string ghichu = commandArgs[5];
                txtBN_edit.Text = idBN;
                txtHS_edit.Text = idHS;
                txtHoTen_edit.Text = hoten;
                txtChanDoan_edit.Text = chandoan;
                txtDonThuoc_edit.Text = donthuoc;
                txtGhiChu_edit.Text = ghichu;
                pn_Update.Visible = true;
                cancelEdit.Visible = false;
                string script = "OpenForm();";
                ScriptManager.RegisterStartupScript(this, GetType(), "display", script, true);
            }

        }
        //Panel Edit
        protected void btn_Save_Update_Click(object sender, EventArgs e)
        {
            string idBs = (string)Session["UserID"];
            string idBn = txtBN_edit.Text;
            string idHs = txtHS_edit.Text;
            string hoten = txtHoTen_edit.Text;
            string chandoan = txtChanDoan_edit.Text;
            string donthuoc = txtDonThuoc_edit.Text;
            string ghichu = txtGhiChu_edit.Text;
            DateTime dayDatetime = DateTime.Now;
            string ngaycapnhat = dayDatetime.ToString("MM/dd/yyyy");
            int result = medicalRecordService.update(idBs, idBn, idHs, chandoan, donthuoc, ngaycapnhat, ghichu);
            if (result != 0)
            {
                string message = "Cập Nhật Thành Công";
                string script = "ShowAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                // Close form after finish update
                pn_Update.Visible = false;
                cancelEdit.Visible = true;
                // then load page
                loadData();
            }
            else
            {
                string message = "Cập nhật thất bại";
                string script = "ShowAlert('" + message + "','error')";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);

            }
        }
        //          Close update form
        protected void btn_Close_Update_Click(object sender, EventArgs e)
        {
            txtHS_edit.Text = "";
            txtHS_edit.Text = "";
            txtHoTen_edit.Text = "";
            txtChanDoan_edit.Text = "";
            txtDonThuoc_edit.Text = "";
            txtGhiChu_edit.Text = "";
            pn_Update.Visible = false;
            cancelEdit.Visible = true;
            string script = "CloseForm();";
            ScriptManager.RegisterStartupScript(this, GetType(), "display", script, true);
        }

        //          Paging
        protected void gridMedicalRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string idbs = (string)Session["UserID"];
            DataTable dt = medicalRecordService.getAll(idbs);
            gridMedicalRecord.DataSource = dt;
            gridMedicalRecord.PageIndex = e.NewPageIndex;
            gridMedicalRecord.DataBind();

        }
        //              Search
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string keyText = txt_Searching.Text;
            string idbs = (string)Session["UserID"];
            DataTable dt = new DataTable();
            dt = medicalRecordService.getByPatientName(keyText, idbs);

            if (!(dt.Rows.Count >= 1))
            {

                string message = "Không tìm thấy bệnh nhân";
                string script = "ShowAlert('" + message + "','error');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }
            else if (keyText == null || keyText == "")
            {
                loadData();
                return;
            }
            gridMedicalRecord.DataSource = dt;
            gridMedicalRecord.DataBind();



        }
    }
}