using NHOM20_DATN.res.service;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Doctor
{
    public partial class Cap_Nhat_Ho_So_Benh_An : System.Web.UI.Page
    {
        MedicalRecordService medicalRecordService = new MedicalRecordService();
        PatientManagerment patientService = new PatientManagerment();
        LichSuKhamService lskService = new LichSuKhamService();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idbs = (string)Session["UserID"];
                if ((string)Session["Role"] == null || (string)Session["Role"] == "")
                {
                    Response.Redirect("~/Dang_Nhap.aspx");
                    return;
                }

                loadData();

            }

        }

        public void loadData()
        {
            string idbs = (string)Session["UserID"];
            DataTable dtHS = new DataTable();
            dtHS = medicalRecordService.getAll(idbs);
            if(dtHS.Rows.Count <= 0)
            {
                string message = "Không có bệnh nhân nào";
                string script = "ShowAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }
            gridMedicalRecord.DataSource = dtHS;
            gridMedicalRecord.DataBind();
        }

        //      BTN EDIT
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    gridMedicalRecord.Columns[0].Visible = true;
        //    btnEdit.Visible = false;
        //    cancelEdit.Visible = true;



        //}
        //      CANCEL EDITING
        //protected void cancelEdit_Click(object sender, EventArgs e)
        //{
        //    gridMedicalRecord.Columns[0].Visible = false;
        //    btnEdit.Visible = true;
        //    cancelEdit.Visible = false;

        //}


        //Command event
        protected void gridMedicalRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editSelect")
            {

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '#' });
                string idHS = commandArgs[0];
                string idBN = commandArgs[1];
                string hoten = commandArgs[2];
                string chandoan = commandArgs[3];
                string donthuoc = commandArgs[4];
                string ghichu = commandArgs[5];
                string idPK = commandArgs[6];
                string huongdtr = commandArgs[7];
                txtBN_edit.Text = idBN;
                txtHS_edit.Text = idHS;
                txtPK_edit.Text = idPK;
                txtHoTen_edit.Text = hoten;
                txtChanDoan_edit.Text = chandoan;
                txtDonThuoc_edit.Text = donthuoc.ToString();
                txtGhiChu_edit.Text = ghichu;
                txtHuongDtr_edit.Text = huongdtr;
                pn_Update.Visible = true;
                //cancelEdit.Visible = false;
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
            string idPK = txtPK_edit.Text;
            string hoten = txtHoTen_edit.Text;
            string chandoan = txtChanDoan_edit.Text;
            string donthuoc = txtDonThuoc_edit.Text;
            string ghichu = txtGhiChu_edit.Text;
            string huongdtr = txtHuongDtr_edit.Text;
            DateTime dayDatetime = DateTime.Now;
            string ngaycapnhat = dayDatetime.ToString("MM/dd/yyyy");
            int result = medicalRecordService.update(idBs, idBn, idHs,idPK, chandoan, donthuoc, ngaycapnhat, ghichu);
            int resultLSK = lskService.update(idBn, idPK, chandoan, huongdtr);
            if (result != 0)
            {
                string message = "Cập Nhật Thành Công";
                string script = "ShowAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                
                // Close form after finish update
                pn_Update.Visible = false;
                string scriptClose = "CloseForm();";
                ScriptManager.RegisterStartupScript(this, GetType(), "display", scriptClose, true);

                //cancelEdit.Visible = true;
                // then load page
                loadData();
                return;
            }
            else
            {
                string message = "Cập nhật thất bại";
                string script = "ShowAlert('" + message + "','error')";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }
        }
        //          Close update form
        protected void btn_Close_Update_Click(object sender, EventArgs e)
        {
            txtBN_edit.Text = "";
            txtPK_edit.Text = "";
            txtHS_edit.Text = "";
            txtHoTen_edit.Text = "";
            txtChanDoan_edit.Text = "";
            txtDonThuoc_edit.Text = "";
            txtGhiChu_edit.Text = "";
            txtHuongDtr_edit.Text = "";
            pn_Update.Visible = false;
            //cancelEdit.Visible = true;
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


        //=========== Xem thông tin bệnh nhân ===============
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idBenhNhan = btn.CommandArgument;

            DataTable dt = patientService.findById(idBenhNhan);

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
            }
        }



        //===========
        protected async void btnGoiY_Click(object sender, EventArgs e)
        {


            string chanDoan = txtChanDoan_edit.Text.Trim();
            if (string.IsNullOrEmpty(chanDoan))
            {
                string message = "Hãy nhập chẩn đoán";
                string script = "ShowAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                txtDonThuoc_edit.Text = "";
                return;
            }

            try
            {
                string donThuoc = await medicalRecordService.GoiYDonThuocTuCohere(chanDoan);
                
                if (donThuoc == "Không có đề xuất hợp lệ.")
                {
                    string script = "ShowAlert('Chẩn đoán không hợp lệ hoặc không có thuốc phù hợp','warning')";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                    txtDonThuoc_edit.Text = "";
                }
                else
                {
                    txtDonThuoc_edit.Text = donThuoc;
                }
          

            }
            catch (Exception ex)
            {
                txtDonThuoc_edit.Text = "";
            }
        }




    }
}