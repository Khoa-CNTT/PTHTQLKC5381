using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NHOM20_DATN.sendMail;

namespace NHOM20_DATN
{
    public partial class Quen_Mat_Khau : System.Web.UI.Page
    {
        LopKetNoi ketNoi = new LopKetNoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Hidelammoi", "document.getElementById('lammoi').style.display = 'none';", true);
            }
        }
        private void ShowSweetAlert(string title, string message, string type)
        {
            string script = $@"swal('{title}', '{message.Replace("'", "\\'")}', '{type}');";
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        protected void btnxacnhan_Click(object sender, EventArgs e)
        {
            String Email = txtemail.Text.Trim();
            if (string.IsNullOrEmpty(Email))
            {
                ShowSweetAlert("Lỗi", "Email không được bỏ trống.", "error");
                return;
            }

            // Kiểm tra email có tồn tại không
            if (!CheckEmail(Email))
            {
                ShowSweetAlert("Lỗi", "Email không tồn tại trong hệ thống.", "error");
                return;
            }

            // Nếu email tồn tại, tiếp tục gửi mã xác nhận
            String TieuDe = "Mã Xác Nhận Của Bạn Gửi Từ Hệ Thống Bệnh Viện Banana Hospital";
            String maXacNhan = taoMa();
            String NoiDung = "Mã xác nhận của bạn là: " + maXacNhan;

            sendMai_gmail sendmail = new sendMai_gmail();
            int emailResult = sendmail.sendMail_gmail(Email, TieuDe, NoiDung);
            Session["MaXacNhan"] = maXacNhan;

            if (emailResult > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Showlammoi", "document.getElementById('lammoi').style.display = 'block';", true);
                ShowSweetAlert("Thành công", "Gửi email thành công. Vui lòng kiểm tra hộp thư.", "success");
            }
            else
            {
                ShowSweetAlert("Lỗi", "Gửi email thất bại. Vui lòng thử lại.", "error");
            }
        }

        protected void btndatlai_Click(object sender, EventArgs e)
        {
            string email = txtemail.Text;
            string xacMinh = txtxacminh.Text.Trim();
            string matKhau = txtmatkhau.Text;

            if (string.IsNullOrEmpty(xacMinh))
            {
                ShowSweetAlert("Lỗi", "Mã xác nhận không được bỏ trống.", "error");
                return;
            }

            string maXacNhan = Session["MaXacNhan"] as string;

            if (!string.Equals(xacMinh, maXacNhan, StringComparison.OrdinalIgnoreCase))
            {
                ShowSweetAlert("Lỗi", "Mã xác nhận không đúng.", "error");
                return;
            }

            // Sau khi mã đúng mới kiểm tra mật khẩu
            if (string.IsNullOrEmpty(matKhau))
            {
                ShowSweetAlert("Lỗi", "Mật khẩu không được bỏ trống.", "error");
                return;
            }

            if (!KiemTraMatKhau(matKhau))
            {
                ShowSweetAlert("Lỗi", "Mật khẩu phải có ít nhất 10 ký tự, bao gồm 1 chữ cái in hoa và 1 ký tự đặc biệt.", "error");
                return;
            }

            // Cập nhật mật khẩu mới
            string query = "UPDATE TaiKhoan SET MatKhau = @NewPassword WHERE Email = @Email";
            SqlParameter[] parameters =
            {
                new SqlParameter("@NewPassword", mahoa(matKhau)),
                new SqlParameter("@Email", email)
            };

            int rowsAffected = ketNoi.CapNhat(query, parameters);

            if (rowsAffected > 0)
            {
                ShowSweetAlert("Thành công", "Đặt lại mật khẩu thành công.", "success");
            }
            else
            {
                ShowSweetAlert("Lỗi", "Đặt lại mật khẩu không thành công.", "error");
            }
        }

        private bool KiemTraMatKhau(string password)
        {
            if (password.Length < 10) return false;

            bool coChuHoa = password.Any(char.IsUpper);
            bool coKyTuDacBiet = password.Any(ch => !char.IsLetterOrDigit(ch));

            return coChuHoa && coKyTuDacBiet;
        }

        private string taoMa()
        {
            Random random = new Random();
            return random.Next(10000, 99999).ToString();
        }


        [System.Web.Services.WebMethod]
        public static bool CheckEmail(string email)
        {
            LopKetNoi ketNoi = new LopKetNoi();
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email";
            SqlParameter[] parameters = { new SqlParameter("@Email", email) };

            DataTable dt = ketNoi.docdulieu(query, parameters);

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }
            return false;
        }

        private string mahoa(string password)
        {
            return password;
        }
    }
}