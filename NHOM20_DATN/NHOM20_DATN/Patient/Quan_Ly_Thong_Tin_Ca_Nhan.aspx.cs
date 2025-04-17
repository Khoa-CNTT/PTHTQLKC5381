using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.Patient
{
    public partial class Quan_Ly_Thong_Tin_Ca_Nhan : System.Web.UI.Page
    {
        LopKetNoi lopKetNoi = new LopKetNoi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HienThiThongTinCaNhan();
            }
        }

        private void HienThiThongTinCaNhan()
        {
            string userId = Session["UserID"].ToString();
            string role = Session["Role"].ToString();
            string query = "";

            if (role == "BenhNhan")
            {
                query = "SELECT IDBenhNhan AS ID, HoTen, NgaySinh, GioiTinh, CanCuocCongDan, DiaChi, SoDienThoai, Email FROM BenhNhan WHERE IDBenhNhan = @ID";
            }
            else if (role == "BacSi")
            {
                query = "SELECT IDBacSi AS ID, HoTen, NgaySinh, GioiTinh, DiaChiPhongKham AS DiaChi, SoDienThoai, Email FROM BacSi WHERE IDBacSi = @ID";
            }
            else if (role == "QuanLy")
            {
                query = "SELECT IDQuanLy AS ID, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email FROM QuanLy WHERE IDQuanLy = @ID";
            }

            // Thực thi query để lấy dữ liệu
            SqlParameter[] param = { new SqlParameter("@ID", userId) };
            LopKetNoi kb = new LopKetNoi();
            dlThongTinCaNhan.DataSource = kb.docdulieu(query, param);
            dlThongTinCaNhan.DataBind();
        }

        // Sự kiện khi nhấn nút "Cập nhật"
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string userId = btn.CommandArgument;

            // Lấy thông tin từ các điều khiển trên giao diện
            TextBox txtHoTen = (TextBox)btn.NamingContainer.FindControl("txtHoTen");
            RadioButton rdoNam = (RadioButton)btn.NamingContainer.FindControl("rdoNam");
            RadioButton rdoNu = (RadioButton)btn.NamingContainer.FindControl("rdoNu");
            RadioButton rdoKhac = (RadioButton)btn.NamingContainer.FindControl("rdoKhac");
            TextBox txtNgaySinh = (TextBox)btn.NamingContainer.FindControl("txtNgaySinh"); // Thêm TextBox cho NgaySinh
            TextBox txtCanCuocCongDan = (TextBox)btn.NamingContainer.FindControl("txtCanCuocCongDan");
            TextBox txtDiaChi = (TextBox)btn.NamingContainer.FindControl("txtDiaChi");
            TextBox txtSoDienThoai = (TextBox)btn.NamingContainer.FindControl("txtSoDienThoai");
            TextBox txtEmail = (TextBox)btn.NamingContainer.FindControl("txtEmail");

            // Lấy các giá trị
            string hoTen = txtHoTen.Text;
            string gioiTinh = rdoNam.Checked ? "Nam" : rdoNu.Checked ? "Nu" : "Khac";
            DateTime ngaySinh = DateTime.Parse(txtNgaySinh.Text); // Chuyển đổi NgaySinh sang DateTime
            string canCuocCongDan = txtCanCuocCongDan.Text;
            string diaChi = txtDiaChi.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string email = txtEmail.Text;

            string role = Session["Role"].ToString();
            string query = "";
            SqlParameter[] param = null;

            if (role == "BenhNhan")
            {
                query = "UPDATE BenhNhan SET HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, CanCuocCongDan = @CanCuocCongDan, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE IDBenhNhan = @ID";
                param = new SqlParameter[]
                {
            new SqlParameter("@HoTen", hoTen),
            new SqlParameter("@GioiTinh", gioiTinh),
            new SqlParameter("@NgaySinh", ngaySinh), // Cập nhật NgaySinh
            new SqlParameter("@CanCuocCongDan", canCuocCongDan),
            new SqlParameter("@DiaChi", diaChi),
            new SqlParameter("@SoDienThoai", soDienThoai),
            new SqlParameter("@Email", email),
            new SqlParameter("@ID", userId)
                };
            }
            else if (role == "BacSi")
            {
                query = "query = UPDATE BacSi SET HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, DiaChiPhongKham = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE IDBacSi = @ID";
                param = new SqlParameter[]
                {
            new SqlParameter("@HoTen", hoTen),
            new SqlParameter("@GioiTinh", gioiTinh),
            new SqlParameter("@NgaySinh", ngaySinh), // Cập nhật NgaySinh cho BacSi
            new SqlParameter("@DiaChi", diaChi),
            new SqlParameter("@SoDienThoai", soDienThoai),
            new SqlParameter("@Email", email),
            new SqlParameter("@ID", userId)
                };
            }
            else if (role == "QuanLy")
            {
                query = "UPDATE QuanLy SET HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE IDQuanLy = @ID";
                param = new SqlParameter[]
                {
            new SqlParameter("@HoTen", hoTen),
            new SqlParameter("@GioiTinh", gioiTinh),
            new SqlParameter("@NgaySinh", ngaySinh), // Cập nhật NgaySinh cho QuanLy
            new SqlParameter("@DiaChi", diaChi),
            new SqlParameter("@SoDienThoai", soDienThoai),
            new SqlParameter("@Email", email),
            new SqlParameter("@ID", userId)
                };
            }

            LopKetNoi kb = new LopKetNoi();
            int result = kb.CapNhat(query, param);

            if (result > 0) // Nếu cập nhật thành công
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Đăng ký thành công.', 'success');", true);
            }
            else // Nếu có lỗi
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Đăng ký thất bại.', 'success');", true);
            }

            HienThiThongTinCaNhan(); // Cập nhật lại thông tin sau khi lưu
        }

        protected void btnHuyCapNhat_Click(object sender, EventArgs e)
        {
            HienThiThongTinCaNhan();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hủy đăng ký thành công.', 'success');", true);
        }
    
    }
}