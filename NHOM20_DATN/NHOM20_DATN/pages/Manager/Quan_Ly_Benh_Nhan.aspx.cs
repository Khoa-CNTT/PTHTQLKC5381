﻿using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
            string keySearch = txt_searching.Text.Trim();
            if (string.IsNullOrEmpty(keySearch))
            {
                viewListPatients(); 
                return;
            }

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


        protected void ShowAlert(string message, string type)
        {
            string script = $"showAlert('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        private void ResetButtonStates()
        {
            btnEdit.Visible = true;
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            cancelEdit.Visible = false;
            cancelDelete.Visible = false;
            deleteSelect.Visible = false;
            gridPatientsManager.Columns[0].Visible = false;
            gridPatientsManager.Columns[1].Visible = false;
        }
        //==========================
        //Panel Add
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTK.Text))
            {
                ShowAlert("Vui lòng nhập tên tài khoản", "warning");
                ResetButtonStates();
                return;
            }

            if (string.IsNullOrEmpty(txtMK.Text))
            {
                ShowAlert("Vui lòng nhập mật khẩu", "warning");
                ResetButtonStates();
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                ShowAlert("Vui lòng nhập tên bệnh nhân", "warning");
                ResetButtonStates();
                return;
            }

            if (string.IsNullOrEmpty(txtSDT.Text))
            {
                ShowAlert("Vui lòng nhập số điện thoại", "warning");
                ResetButtonStates();
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ShowAlert("Vui lòng nhập email", "warning");
                ResetButtonStates();
                return;
            }

            if (string.IsNullOrEmpty(txtNgaySinh.Text))
            {
                ShowAlert("Vui lòng chọn ngày sinh", "warning");
                ResetButtonStates();
                return;
            }

            if (radioGT.SelectedValue == null)
            {
                ShowAlert("Vui lòng chọn giới tính", "warning");
                ResetButtonStates();
                return;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                if (addr.Address != txtEmail.Text)
                {
                    ShowAlert("Email không hợp lệ", "warning");
                    ResetButtonStates();
                    return;
                }
            }
            catch
            {
                ShowAlert("Email không hợp lệ", "warning");
                ResetButtonStates();
                return;
            }
            if (txtSDT.Text.Length < 10 || !txtSDT.Text.All(char.IsDigit))
            {
                ShowAlert("Số điện thoại không hợp lệ", "warning");
                ResetButtonStates();
                return;
            }

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
                ResetButtonStates();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                viewListPatients();
                return;
            }
            string idBN = "BN" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            int result = patientManagerment.addPatient(idBN, username, password, name, birth, gender, phone, email);
            if (result != 0)
            {
                txtTK.Text = string.Empty;
                txtMK.Text = string.Empty;
                txtName.Text = string.Empty;
                txtNgaySinh.Text = string.Empty;
                txtSDT.Text = string.Empty;
                txtEmail.Text = string.Empty;
                radioGT.ClearSelection();

                string message = "Thêm bệnh nhân thành công";
                string script = "showAlert('" + message + "','success');";
                ResetButtonStates();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                viewListPatients();
            }
            else
            {
                string message = "Thêm bệnh nhân thất bại!";
                string script = "showAlert('" + message + "','error');";
                ResetButtonStates();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                viewListPatients();
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            pn_Add.Visible = false;
            ResetButtonStates();
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
                    ShowAlert("Đã Xóa", "success");
                }
            }
            viewListPatients();
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
                try
                {
                    DateTime date = DateTime.Parse(ngaySinh, CultureInfo.InvariantCulture);
                    txtNgaySinh_edit.Text = date.ToString("yyyy-MM-dd");
                }
                catch
                {
                    txtNgaySinh_edit.Text = ""; // hoặc báo lỗi
                }


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
            string ngaysinh;

            try
            {
                DateTime date = DateTime.Parse(txtNgaySinh_edit.Text, CultureInfo.InvariantCulture);
                ngaysinh = date.ToString();
            }
            catch
            {
                ngaysinh = ""; // hoặc báo lỗi
            }



            string sdt = txtSDT_edit.Text;
            string email = txtEmail_edit.Text;
            string gt = radioGT_edit.SelectedValue.ToString();
            int result = patientManagerment.updatePatient(id, name, ngaysinh, gt, sdt, email);
            if (result != 0)
            {
                string message = "Cập nhật thành công!";
                string script = "showAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                cancelEdit.Visible = true;
                viewListPatients();

            }
            else
            {
                string message = "Cập nhật thất bại!";
                string script = "showAlert('" + message + "','error');";
                cancelEdit.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                viewListPatients();
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
    }
}