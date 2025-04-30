using MimeKit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace NHOM20_DATN.Patient
{
    public partial class Huy_Tu_Van : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IDBenhNhan"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vui lòng đăng nhập!'); window.location='Dang_Nhap.aspx';", true);
                    btnHuy.Enabled = false;
                }
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowOverlay", "showOverlay();", true);
            string idTuVan = txtIDTuVan.Text.Trim();
            string idBenhNhan = Session["IDBenhNhan"]?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(idTuVan) || string.IsNullOrEmpty(idBenhNhan))
            {
                ThongBao("Bạn phải nhập mã tư vấn hợp lệ.");
                return;
            }

            try
            {
                string sqlCheck = "SELECT * FROM LichTuVan WHERE IDTuVan = @IDTuVan AND IDBenhNhan = @IDBenhNhan";
                SqlParameter[] parametersCheck = {
                    new SqlParameter("@IDTuVan", idTuVan),
                    new SqlParameter("@IDBenhNhan", idBenhNhan)
                };
                DataTable dt = db.docdulieu(sqlCheck, parametersCheck);

                if (dt.Rows.Count > 0)
                {
                    string sqlDelete = "DELETE FROM LichTuVan WHERE IDTuVan = @IDTuVan";
                    SqlParameter[] parametersDelete = {
                        new SqlParameter("@IDTuVan", idTuVan)
                    };
                    int result = db.CapNhat(sqlDelete, parametersDelete);

                    if (result > 0)
                    {
                        GửiEmailXacNhanHuy(idBenhNhan, idTuVan);
                        ThongBao("Hủy tư vấn thành công.");
                    }
                    else
                    {
                        ThongBao("Không thể hủy tư vấn. Vui lòng thử lại.");
                    }
                }
                else
                {
                    ThongBao("Mã tư vấn không tồn tại hoặc không thuộc về bạn.");
                }
            }
            catch (Exception ex)
            {
                ThongBao("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void GửiEmailXacNhanHuy(string idBenhNhan, string idTuVan)
        {
            try
            {
                string emailBenhNhan = LayEmailBenhNhan(idBenhNhan);

                if (string.IsNullOrEmpty(emailBenhNhan))
                {
                    ThongBao("Không tìm thấy email bệnh nhân.");
                    return;
                }

                // Tạo nội dung email
                string subject = "Xác nhận hủy tư vấn";

                string body = @"
                <html>
                <head>
                    <style>
                        body {
                            font-family: 'Segoe UI', Tahoma, Verdana, sans-serif;
                            background-color: #f4f6f8;
                            margin: 0;
                            padding: 20px;
                        }
                        .email-container {
                            background-color: #ffffff;
                            padding: 30px;
                            border-radius: 10px;
                            box-shadow: 0 0 10px rgba(0,0,0,0.1);
                            max-width: 600px;
                            margin: 0 auto;
                        }
                        .email-header {
                            text-align: center;
                            color: #0d6efd;
                            margin-bottom: 20px;
                        }
                        .email-content {
                            font-size: 16px;
                            color: #333333;
                            line-height: 1.6;
                        }
                        .email-footer {
                            margin-top: 30px;
                            text-align: center;
                            font-size: 14px;
                            color: #999999;
                        }
                        .btn-support {
                            display: inline-block;
                            margin-top: 20px;
                            padding: 12px 24px;
                            background-color: #FD1D1D;
                            color: #FFFFFF;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold;
                        }
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <h2 class='email-header'>XÁC NHẬN HỦY TƯ VẤN</h2>
                        <div class='email-content'>
                            <p>Chào bạn,</p>
                            <p>Chúng tôi xin xác nhận rằng yêu cầu hủy tư vấn với mã <strong>" + idTuVan + @"</strong> của bạn đã được xử lý thành công.</p>
                            <p>Chúng tôi rất tiếc vì bạn đã phải hủy cuộc tư vấn. Nếu bạn cần hỗ trợ thêm, hoặc muốn đặt lịch tư vấn mới, xin vui lòng liên hệ với đội ngũ hỗ trợ của chúng tôi.</p>
                            <a href='bananahospitaldanang@gmail.com' class='btn-support'>Liên hệ hỗ trợ</a>
                            <p>Xin cảm ơn bạn đã tin tưởng và sử dụng dịch vụ của chúng tôi.</p>
                            <p>Hi vọng chúng tôi có thể mang lại cho bạn một dịch vụ hoàn hảo, yên tâm trong một tương lai gần nhất..</p>
                            <p>Chúc bạn một ngày tốt lành! Xin cảm ơn bạn!</p>
                        </div>
                        <div class='email-footer'>
                            &copy; 2025 Banana Hospital. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>
                ";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bệnh viện", "bananahospitaldanang@gmail.com"));
                message.To.Add(new MailboxAddress("", emailBenhNhan));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("bananahospitaldanang@gmail.com", "bvezpjcixxoypqvi"); // Mật khẩu ứng dụng
                    client.Send(message);
                    client.Disconnect(true);
                }

                ThongBao("Email xác nhận đã được gửi.");
            }
            catch (Exception ex)
            {
                ThongBao("Gửi email xác nhận thất bại: " + ex.Message);
            }
        }

        private string LayEmailBenhNhan(string idBenhNhan)
        {
            try
            {
                string sql = "SELECT Email FROM BenhNhan WHERE IDBenhNhan = @IDBenhNhan";
                SqlParameter[] parameters = {
                    new SqlParameter("@IDBenhNhan", idBenhNhan)
                };
                DataTable dt = db.docdulieu(sql, parameters);

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Email"].ToString();
                }
            }
            catch (Exception ex)
            {
                ThongBao("Lỗi khi lấy email bệnh nhân: " + ex.Message);
            }

            return string.Empty;
        }

        private void ThongBao(string message)
        {
            string script = $@"
        hideOverlay();
        var msgBox = document.getElementById('successMessage');
        if (msgBox) {{
            msgBox.innerText = '{message}';
            msgBox.style.display = 'block';
            setTimeout(function() {{
                msgBox.style.display = 'none';
            }}, 3000);
        }}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CustomAlert", script, true);
        }
    }
}
