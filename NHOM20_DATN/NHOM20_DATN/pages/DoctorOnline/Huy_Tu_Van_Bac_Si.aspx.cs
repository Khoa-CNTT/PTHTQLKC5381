using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NHOM20_DATN.pages.DoctorOnline
{
    public partial class Huy_Tu_Van_Bac_Si : System.Web.UI.Page
    {
        private readonly LopKetNoi kn;
        private readonly string emailSender = ConfigurationManager.AppSettings["EmailSender"];
        private readonly string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];

        public Huy_Tu_Van_Bac_Si()
        {
            kn = new LopKetNoi();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblThongBao.Visible = false;
                pnlThongTin.Visible = false;
            }
        }

        protected void btnTim_Click(object sender, EventArgs e)
        {
            string idTuVan = txtIDTuVan.Text.Trim();

            if (string.IsNullOrEmpty(idTuVan))
            {
                HienThiThongBao("Vui lòng nhập mã tư vấn!", true);
                return;
            }

            string sql = "SELECT IDTuVan, IDBenhNhan, Ngay, Gio FROM LichTuVan WHERE IDTuVan = @IDTuVan";
            SqlParameter[] prms = { new SqlParameter("@IDTuVan", idTuVan) };
            DataTable dt = kn.docdulieu(sql, prms);

            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    DateTime ngay = Convert.ToDateTime(dt.Rows[0]["Ngay"]);
                    TimeSpan gio = TimeSpan.Parse(dt.Rows[0]["Gio"].ToString());
                    DateTime thoiGianTuVan = ngay.Date + gio;

                    if ((thoiGianTuVan - DateTime.Now).TotalHours < 10)
                    {
                        HienThiThongBao("Chỉ được hủy trước 10 tiếng!", true);
                        pnlThongTin.Visible = false;
                        return;
                    }

                    ViewState["ThoiGianTuVan"] = thoiGianTuVan;
                    ViewState["IDBenhNhan"] = dt.Rows[0]["IDBenhNhan"].ToString();
                    lblThongTin.Text = $"Thời gian tư vấn: {thoiGianTuVan:dd/MM/yyyy HH:mm}";
                    pnlThongTin.Visible = true;
                    lblThongBao.Visible = false;
                }
                catch (Exception ex)
                {
                    HienThiThongBao($"Lỗi khi xử lý thời gian: {ex.Message}", true);
                }
            }
            else
            {
                HienThiThongBao("Không tìm thấy tư vấn.", true);
                pnlThongTin.Visible = false;
            }
        }

        protected async void btnHuy_Click(object sender, EventArgs e)
        {
            if (ViewState["ThoiGianTuVan"] == null || ViewState["IDBenhNhan"] == null)
            {
                HienThiThongBao("Vui lòng tìm tư vấn trước.", true);
                return;
            }

            DateTime thoiGianTuVan = (DateTime)ViewState["ThoiGianTuVan"];
            if ((thoiGianTuVan - DateTime.Now).TotalHours < 10)
            {
                HienThiThongBao("Chỉ được hủy trước 10 tiếng!", true);
                return;
            }

            string idTuVan = txtIDTuVan.Text.Trim();
            string lyDo = txtLyDo.Text.Trim();
            string idBenhNhan = ViewState["IDBenhNhan"].ToString();

            if (string.IsNullOrEmpty(lyDo))
            {
                HienThiThongBao("Vui lòng nhập lý do hủy!", true);
                return;
            }

            string sqlEmail = "SELECT Email FROM BenhNhan WHERE IDBenhNhan = @ID";
            SqlParameter[] prmsEmail = { new SqlParameter("@ID", idBenhNhan) };
            object emailObj = kn.LayGiaTri(sqlEmail, prmsEmail);
            string email = emailObj != null ? emailObj.ToString() : "";

            string sqlUpdate = "UPDATE LichTuVan SET TrangThai = N'Đã hủy' WHERE IDTuVan = @IDTuVan";
            SqlParameter[] prmsUpdate = { new SqlParameter("@IDTuVan", idTuVan) };
            int kq = kn.CapNhat(sqlUpdate, prmsUpdate);

            if (kq > 0)
            {
                HienThiThongBao("Đã hủy tư vấn thành công.", false);
                await GuiEmailAsync(email, idTuVan, lyDo);
                pnlThongTin.Visible = false;
                txtLyDo.Text = "";
            }
            else
            {
                HienThiThongBao("Hủy tư vấn thất bại.", true);
            }
        }

        private void HienThiThongBao(string message, bool isError)
        {
            lblThongBao.Text = message;
            lblThongBao.CssClass = isError ? "alert alert-danger" : "alert alert-success";
            lblThongBao.Visible = true;
        }

        private async Task GuiEmailAsync(string email, string idTuVan, string lyDo)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(emailSender) || string.IsNullOrEmpty(emailPassword))
                return;

            try
            {
                using (MailMessage mail = new MailMessage(emailSender, email))
                {
                    mail.Subject = "Thông báo hủy lịch tư vấn";
                    mail.Body = $@"
                    <!DOCTYPE html>
                    <html lang='vi'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                                color: #333;
                            }}
                            .container {{
                                width: 100%;
                                max-width: 600px;
                                margin: 20px auto;
                                background-color: #ffffff;
                                border-radius: 8px;
                                overflow: hidden;
                                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                            }}
                            .header {{
                                background-color: #28a745;
                                color: #ffffff;
                                text-align: center;
                                padding: 20px;
                                font-size: 24px;
                                font-weight: bold;
                            }}
                            .content {{
                                padding: 20px;
                                line-height: 1.6;
                            }}
                            .highlight {{
                                color: #dc3545;
                                font-weight: bold;
                            }}
                            .footer {{
                                text-align: center;
                                padding: 10px;
                                background-color: #f8f9fa;
                                color: #6c757d;
                                font-size: 12px;
                            }}
                            a {{
                                color: #007bff;
                                text-decoration: none;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                Thông báo hủy lịch tư vấn
                            </div>
                            <div class='content'>
                                <p>Kính gửi Quý khách,</p>
                                <p>Chúng tôi xin thông báo rằng lịch tư vấn có mã <span class='highlight'>{idTuVan}</span> đã bị hủy.</p>
                                <p><strong>Lý do:</strong> {lyDo}</p>
                                <p>Chúng tôi rất tiếc vì sự bất tiện này và cam kết sẽ hỗ trợ Quý khách sắp xếp lại lịch tư vấn nếu cần. Vui lòng liên hệ với chúng tôi qua email <a href='mailto:bananahospitaldanang@gmail.com'>bananahospitaldanang@gmail.com</a> hoặc số điện thoại <a href='tel:+84912345678'>0912 345 678</a> để được hỗ trợ thêm.</p>
                                <p>Trân trọng,</p>
                                <p><strong>Đội ngũ Doctor Online</strong></p>
                            </div>
                            <div class='footer'>
                                <p>© 2025 Banana Hospital. Dành mọi sự ưu tiên đến khách hàng.</p>
                                <p><a href='bananahospitaldanang@gmail.com'>BANANA HOSPITAL</a></p>
                            </div>
                        </div>
                    </body>
                    </html>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(emailSender, emailPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi gửi email: {ex.Message}");
            }
        }
    }
}