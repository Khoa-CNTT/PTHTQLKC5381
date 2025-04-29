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

            String TieuDe = "Mã Xác Nhận Của Bạn Gửi Từ Hệ Thống Bệnh Viện Banana Hospital";
            String maXacNhan = taoMa(); // Giả sử hàm taoMa() tạo ra mã xác nhận
            String NoiDung = @"
                <div style='background-color: #f5f5f5; padding: 10px 0; font-family: Arial, sans-serif;'> 
                  <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                    <div style='background-color: #13bdbd; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
                      Bệnh viện BANANA HOSPITAL
                    </div>
                    <div style='padding: 30px; text-align: left;'>
                      <h2 style='color: #13bdbd;'>Mã Xác Nhận Của Bạn</h2>
      
                      <p>Xin chào <strong style='color: #13bdbd;'>Quý khách</strong>,</p>
                      <p>Chúng tôi xin gửi mã xác nhận cho việc yêu cầu <strong style='color: #13bdbd;'>đặt lại mật khẩu</strong> của Quý khách:</p>

                      <ul style='list-style: none; padding-left: 0;'>
                          <li>🧾 <strong>Mã xác nhận:</strong> " + maXacNhan + @"</li>
                      </ul>

                      <p>Nếu bạn không yêu cầu thay đổi mật khẩu, vui lòng bỏ qua email này.</p>
                      <p>Nếu gặp bất kỳ vấn đề nào, Quý khách vui lòng liên hệ với chúng tôi để được hỗ trợ.</p>
                      <p style='margin-top: 10px;'>Xin chân thành cảm ơn Quý khách đã tin tưởng <strong style='color: #13bdbd;'>BANANA Hospital</strong></p>
                      <p>Trân trọng,</p>
                      <p><strong style='color: #13bdbd;'>Bệnh viện BANANA HOSPITAL</strong></p>
                    </div>
                  </div>
                </div>";

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