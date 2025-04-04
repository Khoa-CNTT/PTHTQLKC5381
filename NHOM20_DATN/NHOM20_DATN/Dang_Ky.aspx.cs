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
    public partial class Dang_Ky : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string reEnterPassword = txtReEnterPassword.Text;
            LopKetNoi db = new LopKetNoi();

            // Kiểm tra tên đăng nhập đã tồn tại chưa
            string checkUserQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] checkParams = { new SqlParameter("@TenDangNhap", username) };
            DataTable dt = db.docdulieu(checkUserQuery, checkParams); // truyền tham số vào dt

            if (dt != null && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                string js = "alert('Tên đăng nhập đã tồn tại!');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", js, true);
                return;
            }
            // Kiểm tra email đã tồn tại chưa
            string checkEmailQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email";
            SqlParameter[] checkEmailParams = { new SqlParameter("@Email", email) };
            DataTable dtEmail = db.docdulieu(checkEmailQuery, checkEmailParams);

            if (dtEmail != null && Convert.ToInt32(dtEmail.Rows[0][0]) > 0)
            {
                string js = "alert('Email đã tồn tại!');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", js, true);
                return;
            }
            // Tạo ID mới cho bệnh nhân
            string newId = GenerateUniqueId("BN");

            // Thêm tài khoản vào bảng TaiKhoan
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
                string js = "alert('Đăng ký không thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", js, true);
            }
            else
            {
                // Thêm thông tin vào bảng BenhNhan
                string insertPatientQuery = "INSERT INTO BenhNhan (IDBenhNhan, HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi) VALUES (@IDBenhNhan, @HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi)";
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

                string js = "alert('Đăng ký thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", js, true);
            }
        }
        private string GenerateUniqueId(string prefix)
        {
            return prefix + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}