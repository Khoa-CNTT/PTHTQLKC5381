using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace NHOM20_DATN
{
    public partial class Dang_Ky : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                return;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string reEnterPassword = txtReEnterPassword.Text.Trim();

            // Kiểm tra mật khẩu nhập lại
            if (password != reEnterPassword)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: 'Lỗi', text: 'Mật khẩu nhập lại không khớp!' });", true);
                return;
            }

            LopKetNoi db = new LopKetNoi();

            // Kiểm tra tên đăng nhập
            string checkUserQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] checkParams = { new SqlParameter("@TenDangNhap", username) };
            DataTable dt = db.docdulieu(checkUserQuery, checkParams);

            if (dt != null && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: 'Đăng ký thất bại', text: 'Tên đăng nhập đã tồn tại!' });", true);
                return;
            }

            // Kiểm tra email
            string checkEmailQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email";
            SqlParameter[] checkEmailParams = { new SqlParameter("@Email", email) };
            DataTable dtEmail = db.docdulieu(checkEmailQuery, checkEmailParams);

            if (dtEmail != null && Convert.ToInt32(dtEmail.Rows[0][0]) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: 'Đăng ký thất bại', text: 'Email đã tồn tại!' });", true);
                return;
            }

            // Tạo ID mới cho bệnh nhân
            string newId = GenerateUniqueId("BN");

            // Thêm vào bảng TaiKhoan
            string insertUserQuery = "INSERT INTO TaiKhoan (ID, TenDangNhap, MatKhau, Email) VALUES (@ID, @TenDangNhap, @MatKhau, @Email)";
            SqlParameter[] insertUserParams = {
                new SqlParameter("@ID", newId),
                new SqlParameter("@TenDangNhap", username),
                new SqlParameter("@MatKhau", password),
                new SqlParameter("@Email", email)
            };
            int result = db.CapNhat(insertUserQuery, insertUserParams);

            if (result <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: 'Đăng ký thất bại', text: 'Không thể thêm tài khoản.' });", true);
                return;
            }

            // Thêm vào bảng BenhNhan
            string insertPatientQuery = @"
                INSERT INTO BenhNhan (IDBenhNhan, HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi) 
                VALUES (@IDBenhNhan, @HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi)";
            SqlParameter[] patientParams = {
                new SqlParameter("@IDBenhNhan", newId),
                new SqlParameter("@HoTen", ""),
                new SqlParameter("@NgaySinh", DBNull.Value),
                new SqlParameter("@GioiTinh", "Khac"),
                new SqlParameter("@SoDienThoai", ""),
                new SqlParameter("@Email", email),
                new SqlParameter("@DiaChi", "")
            };
            db.CapNhat(insertPatientQuery, patientParams);

            // Đăng ký thành công
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire({ icon: 'success', title: 'Đăng ký thành công' });", true);
        }

        private string GenerateUniqueId(string prefix)
        {
            return prefix + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
    }
}
