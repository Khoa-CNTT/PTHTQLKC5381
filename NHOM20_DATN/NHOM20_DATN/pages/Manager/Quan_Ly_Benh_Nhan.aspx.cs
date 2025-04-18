using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Quan_Ly_Benh_Nhan : System.Web.UI.Page
    {
        PatientManagerment patientManagerment = new PatientManagerment();
        LopKetNoi ketNoi = new LopKetNoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                viewListPatients();
            }
        }


        public void viewListPatients()
        {
            DataTable dt = patientManagerment.viewPatient();
            gridPatientsManager.DataSource = dt;
            gridPatientsManager.DataBind();

        }


        protected void gridPatientsManager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = patientManagerment.viewPatient();
            gridPatientsManager.DataSource = dt;
            gridPatientsManager.PageIndex = e.NewPageIndex;
            gridPatientsManager.DataBind();

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            string keySearch = txt_searching.Text;
            DataTable dt = patientManagerment.searchPatients(keySearch);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridPatientsManager.DataSource = dt;
                gridPatientsManager.DataBind();
            }
            else
            {
                string message = "Không có thông tin tương ứng";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }
        }

        //=============== Button CRUD
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            gridPatientsManager.Columns[1].Visible = true;
            btnEdit.Visible = false;
            btnAdd.Visible = false;
            btnDelete.Visible = false;
            cancelDelete.Visible = true;
            deleteSelect.Visible = true;
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            gridPatientsManager.Columns[0].Visible = true;
            btnEdit.Visible = false;
            btnAdd.Visible = false;
            btnDelete.Visible = false;
            cancelEdit.Visible = true;



        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pn_Add.Visible = true;
            btnEdit.Visible = false;
            btnAdd.Visible = false;
            btnDelete.Visible = false;
            string js = $"openForm('#patientAdd_container')";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "redirect", js, true);



        }
        //==========================
        //Panel Add
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string username = txtTK.Text;
            string password = txtMK.Text;
            string name = txtName.Text;
            string birth = txtNgaySinh.Text;
            string phone = txtSDT.Text;
            string email = txtEmail.Text;
            string gender = radioGT.SelectedValue;
            //check existing patient
            if (patientManagerment.checkUserExisting(username) == 0)
            {
                string message = "Tên đăng nhập đã tồn tại";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }
            string idBN = "BN" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            int result = patientManagerment.addPatient(idBN, username, password, name, birth, gender, phone, email);
            if (result != 0)
            {
                string message = "Thêm bệnh nhân thành công";
                string script = "showAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }
            else
            {
                string message = "Thêm bệnh nhân thất bại!";
                string script = "showAlert('" + message + "','error');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            pn_Add.Visible = false;
            btnEdit.Visible = true;
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            string js = $"closeForm('#patientAdd_container')";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "redirect", js, true);

        }
        //=========================
        //Update has been clicked

        protected void cancelEdit_Click(object sender, EventArgs e)
        {
            gridPatientsManager.Columns[0].Visible = false;
            btnEdit.Visible = true;
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            cancelEdit.Visible = false;

        }

        // ============Check edit item
        //Hold checked in checkbox
        protected void gridPatientsManager_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        //===========After click button delete
        protected void deleteSelect_Click(object sender, EventArgs e)
        {
            List<string> selectedItem = new List<string>();
            int result = 0;
            foreach (GridViewRow row in gridPatientsManager.Rows)
            {
                CheckBox checkSelected = (CheckBox)row.FindControl("checkDelete");
                Label lblID = (Label)row.FindControl("lbl_IdBN");
                if (checkSelected != null && checkSelected.Checked && lblID != null)
                {

                    string idBn = lblID.Text;
                    result = patientManagerment.deletePatients(idBn);
                }
            }
            Response.Redirect("Quan_Ly_Benh_Nhan.aspx");
        }

        protected void cancelDelete_Click(object sender, EventArgs e)
        {
            gridPatientsManager.Columns[1].Visible = false;
            btnEdit.Visible = true;
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            cancelDelete.Visible = false;
            deleteSelect.Visible = false;
        }
        //=================================
        //Command event
        protected void gridPatientsManager_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editSelect")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string id = commandArgs[0];
                string name = commandArgs[1];
                string ngaySinh = commandArgs[2];
                string SDT = commandArgs[3];
                string email = commandArgs[4];
                string gt = commandArgs[5];
                //hiddenEmail.Value= email;
                //hiddenGT.Value= gt;
                //hiddenIdBN.Value= id;
                //hiddenName.Value= name;
                //hiddenNgaySinh.Value= ngaySinh;
                //hiddenSDT.Value = SDT;
                txtIDBenhNhan_edit.Text = id;
                txtName_edit.Text = name;
                //txtNgaySinh_edit.Text = DateTime.Parse(ngaySinh).ToString("yyyy-MM-dd");
                txtNgaySinh_edit.Text = DateTime.Parse(ngaySinh).ToString("yyyy-MM-dd");
                txtSDT_edit.Text = SDT;
                txtEmail_edit.Text = email;
                radioGT_edit.SelectedValue = gt;

                //      Open Form
                pn_Update.Visible = true;
                cancelEdit.Visible = false;
                string js = $"openForm('#patientUpdate_container')";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "redirect", js, true);


            }

        }
        //Panel Edit
        protected void btn_Save_Update_Click(object sender, EventArgs e)
        {
            string id = txtIDBenhNhan_edit.Text;
            string name = txtName_edit.Text;
            string ngaysinh = DateTime.Parse(txtNgaySinh_edit.Text).ToString("MM/dd/yyyy");
            string sdt = txtSDT_edit.Text;
            string email = txtEmail_edit.Text;
            string gt = radioGT_edit.SelectedValue.ToString();
            int result = patientManagerment.updatePatient(id, name, ngaysinh, gt, sdt, email);
            if (result != 0)
            {
                string message = "Cập nhật thành công!";
                string script = "showAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }
            else
            {
                string message = "Cập nhật thất bại!";
                string script = "showAlert('" + message + "','error');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }
        }

        protected void btn_Close_Update_Click(object sender, EventArgs e)
        {
            //txtIDBenhNhan_edit.Text = "";
            //txtEmail_edit.Text = "";
            //txtName_edit.Text = "";
            //radioGT_edit.SelectedValue;
            pn_Update.Visible = false;
            cancelEdit.Visible = true;
            string js = $"closeForm('#patientUpdate_container')";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "redirect", js, true);

        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idBenhNhan = btn.CommandArgument;

            string sql = @"SELECT * FROM BenhNhan WHERE IDBenhNhan = @IDBenhNhan";
            SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@IDBenhNhan", idBenhNhan)
    };

            DataTable dt = ketNoi.docdulieu(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string details = $@"
            <p><strong>Mã bệnh nhân:</strong> {row["IDBenhNhan"]}</p>
            <p><strong>Họ tên:</strong> {row["HoTen"]}</p>
            <p><strong>Ngày sinh:</strong> {Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy")}</p>
            <p><strong>Giới tính:</strong> {row["GioiTinh"]}</p>
            <p><strong>Số điện thoại:</strong> {row["SoDienThoai"]}</p>
            <p><strong>Email:</strong> {row["Email"]}</p>
            <p><strong>Địa chỉ:</strong> {row["DiaChi"]}</p>
            
        ";

                patientDetails.InnerHtml = details;

                // Hiển thị modal
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModal",
                    "document.getElementById('detailModal').style.display='block';", true);
            }
        }
    }
}