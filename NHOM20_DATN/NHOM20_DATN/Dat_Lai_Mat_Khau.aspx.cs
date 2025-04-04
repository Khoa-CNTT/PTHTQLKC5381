using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace NHOM20_DATN
{
    public partial class Dat_Lai_Mat_Khau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void ShowSweetAlert(string title, string message, string type)
        {
            string script = $@"swal('{title}', '{message.Replace("'", "\\'")}', '{type}');";
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        protected void btndatlai_Click(object sender, EventArgs e)
        {
            string matKhauCu = TextBox1.Text.Trim();
            string matKhauMoi = txtxacminh.Text.Trim();
            string nhapLaiMatKhauMoi = txtmatkhau.Text.Trim();
            string userID = Session["UserID"].ToString(); // Lấy ID người dùng từ session

            // Kiểm tra ô trống
            if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(nhapLaiMatKhauMoi))
            {
                ShowSweetAlert("Lỗi", "Vui lòng điền đầy đủ thông tin.", "error");
                return;
            }

            // Kiểm tra mật khẩu cũ TRƯỚC TIÊN
            LopKetNoi ketNoi = new LopKetNoi();
            string query = "SELECT MatKhau FROM TaiKhoan WHERE ID = @ID";
            SqlParameter[] checkParams = new SqlParameter[] { new SqlParameter("@ID", userID) };

            DataTable result = ketNoi.docdulieu(query, checkParams);
            if (result == null || result.Rows.Count == 0)
            {
                ShowSweetAlert("Lỗi", "Không tìm thấy người dùng.", "error");
                return;
            }

            string matKhauHienTai = result.Rows[0]["MatKhau"].ToString();
            if (matKhauCu != matKhauHienTai)
            {
                ShowSweetAlert("Lỗi", "Mật khẩu cũ không đúng.", "error");
                return;
            }

            // Sau khi xác nhận mật khẩu cũ đúng, mới kiểm tra các điều kiện khác

            // Kiểm tra mật khẩu khớp
            if (matKhauMoi != nhapLaiMatKhauMoi)
            {
                ShowSweetAlert("Lỗi", "Mật khẩu mới không khớp.", "error");
                return;
            }

            // Kiểm tra độ mạnh mật khẩu
            if (!KiemTraDoManhMatKhau(matKhauMoi))
            {
                ShowSweetAlert("Lỗi", "Mật khẩu phải có ít nhất 10 ký tự, bao gồm 1 chữ cái in hoa và 1 ký tự đặc biệt.", "error");
                return;
            }

            // Cập nhật mật khẩu mới (giữ nguyên)
            query = "UPDATE TaiKhoan SET MatKhau = @MatKhauMoi WHERE ID = @ID";
            SqlParameter[] updateParams = new SqlParameter[]
            {
        new SqlParameter("@MatKhauMoi", matKhauMoi),
        new SqlParameter("@ID", userID)
            };

            int rowsAffected = ketNoi.CapNhat(query, updateParams);
            if (rowsAffected > 0)
            {
                ShowSweetAlert("Thành công", "Đặt lại mật khẩu thành công.", "success");
            }
            else
            {
                ShowSweetAlert("Lỗi", "Đặt lại mật khẩu không thành công.", "error");
            }
        }

        // Hàm kiểm tra độ mạnh của mật khẩu
        private bool KiemTraDoManhMatKhau(string password)
        {
            // Biểu thức chính quy yêu cầu: ít nhất 10 ký tự, 1 chữ cái in hoa, 1 ký tự đặc biệt
            string pattern = @"^(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}