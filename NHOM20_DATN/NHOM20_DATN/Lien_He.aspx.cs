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
            // Tiêu đề email
            string subject = "YÊU CẦU LIÊN HÊ TỪ WEBSITE";

            // Nội dung gửi đến email bệnh viện
            string body = $@"
<div style='background-color: #f5f5f5; padding: 20px 0; font-family: Arial, sans-serif;'>
  <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
    
    <div style='background-color: #006666; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
      BANANA HOSPITAL
    </div>
    
    <div style='padding: 30px; text-align: left; color: #333;'>
      <h2 style='color: #333;'>Xác nhận thông tin liên hệ</h2>
      
      <p>Khách hàng đã liên hệ với thông tin sau:</p>

      <ul style='list-style: none; padding-left: 0;'>
        <li>👤 <strong>Họ tên:</strong> {name}</li>
        <li>📧 <strong>Email:</strong> {email}</li>
        <li>📞 <strong>Số điện thoại:</strong> {phone}</li>
        <li>📝 <strong>Ghi chú:</strong> {description}</li>
      </ul>

      <p style='margin-top: 30px;'>Vui lòng kiểm tra và xác nhận lại thông tin!</p>

      <p>Trân trọng,<br/>
      <strong>Phòng Khám BANANA Hospital</strong></p>
    </div>

  </div>
</div>";


            // Tạo đối tượng gửi mail
            sendMai_gmail sendmail = new sendMai_gmail();

            // Gửi đến email bệnh viện
            int result = sendmail.sendMail_gmail("bananahospitaldanang@gmail.com", subject, body);

            // Thông báo kết quả
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Gửi liên hệ thành công.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert(' Thất bại.', 'error');", true);
            }
        }
    }
}