using NHOM20_DATN.sendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN
{
    public partial class Lien_He : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu người nhập
            string name = txtName.Text.Trim();
            string email = txtMail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string description = txtDes.Text.Trim();

            // Tiêu đề email
            string subject = "YÊU CẦU ĐĂNG KÝ TỪ WEBSITE";

            // Nội dung gửi đến email bệnh viện
            string body = $"Khách hàng đã đăng ký khám với thông tin sau:\n" +
                          $"- Họ tên: {name}\n" +
                          $"- Email: {email}\n" +
                          $"- Số điện thoại: {phone}\n" +
                          $"- Ghi chú: {description}";

            // Tạo đối tượng gửi mail
            sendMai_gmail sendmail = new sendMai_gmail();

            // Gửi đến email bệnh viện
            int result = sendmail.sendMail_gmail("bananahospitaldanang@gmail.com", subject, body);

            // Thông báo kết quả
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Gui lien he thanh cong.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert(' Thất bại.', 'error');", true);
            }
        }
    }
}