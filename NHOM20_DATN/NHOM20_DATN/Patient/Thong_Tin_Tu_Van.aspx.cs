using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace NHOM20_DATN.Patient
{
    public partial class Thong_Tin_Tu_Van : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDateRange();

                // Lấy thông tin bác sĩ từ query string
                string tenBacSi = Request.QueryString["TenBacSi"];
                string chuyenKhoa = Request.QueryString["ChuyenKhoa"];
                string trinhDo = Request.QueryString["TrinhDo"];
                string email = Request.QueryString["Email"];
                string vaiTro = Request.QueryString["VaiTro"];
                string idBacSi = Request.QueryString["IDBacSi"];

                // Hiển thị thông tin bác sĩ
                lblTenBacSi.Text = tenBacSi;
                lblChuyenKhoa.Text = chuyenKhoa;
                lblTrinhDo.Text = trinhDo;
                lblEmail.Text = email;
                lblVaiTro.Text = vaiTro;

                // Hiển thị ID bác sĩ
                lblIDBacSi.Text = idBacSi;

                // Lấy ID bệnh nhân từ Session
                if (Session["IDBenhNhan"] != null)
                {
                    string idBenhNhan = Session["IDBenhNhan"].ToString();
                    LayThongTinBenhNhan(idBenhNhan);
                }
                else
                {
                    lblIDBenhNhan.Text = "Chưa có ID bệnh nhân";
                }

            }

        }

        private void SetDateRange()
        {
            // Lấy ngày hiện tại
            DateTime today = DateTime.Today;

            // Xác định ngày bắt đầu là ngày hiện tại
            DateTime minDate = today;

            // Xác định ngày kết thúc là ngày cuối cùng của tháng sau
            DateTime maxDate = today.AddMonths(2).AddDays(-today.Day);

            // Đặt giá trị cho thuộc tính min và max của TextBox
            txtNgay.Attributes.Add("min", minDate.ToString("yyyy-MM-dd"));
            txtNgay.Attributes.Add("max", maxDate.ToString("yyyy-MM-dd"));
        }
        private void LayThongTinBenhNhan(string idBenhNhan)
        {
            string sql = "SELECT IDBenhNhan, HoTen, NgaySinh, GioiTinh, CanCuocCongDan, SoDienThoai, Email, DiaChi " +
                         "FROM BenhNhan WHERE IDBenhNhan = @IDBenhNhan";

            SqlParameter[] parameters = { new SqlParameter("@IDBenhNhan", idBenhNhan) };
            DataTable dt = db.docdulieu(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblIDBenhNhan.Text = row["IDBenhNhan"].ToString();
            }
            else
            {
                lblIDBenhNhan.Text = "Không tìm thấy thông tin bệnh nhân.";
            }
        }
        private string TaoMaTuVan()
        {
            return "TV" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            // Hiển thị overlay
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowOverlay", "showOverlay();", true);

            try
            {
                string idBenhNhan = lblIDBenhNhan.Text;
                DateTime ngay = DateTime.Parse(txtNgay.Text);
                TimeSpan gio = TimeSpan.Parse(ddlGio.Text);
                string trieuChung = txtTrieuChung.Text;
                string idBacSi = lblIDBacSi.Text;

                // Kiểm tra giờ có nằm trong khung giờ sáng hoặc chiều
                if (!((gio >= TimeSpan.FromHours(7) && gio <= TimeSpan.FromHours(11)) ||
                      (gio >= TimeSpan.FromHours(14) && gio <= TimeSpan.FromHours(17))))
                {
                    Response.Write("<script>alert('Thời gian tư vấn chỉ có thể chọn từ 7:00-11:00 hoặc 14:00-17:00.');</script>");
                    return;
                }

                if (string.IsNullOrEmpty(idBacSi))
                {
                    Response.Write("<script>alert('Không tìm thấy thông tin bác sĩ.');</script>");
                    return;
                }

                string linkGoogleMeet = "https://meet.google.com/" + Guid.NewGuid().ToString().Substring(0, 8);
                string idTuVan = TaoMaTuVan();

                DangKyTuVan(idTuVan, idBenhNhan, idBacSi, ngay, gio, trieuChung, linkGoogleMeet);
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Đã xảy ra lỗi: " + ex.Message + "');</script>");
            }
            finally
            {
                // Ẩn overlay sau khi hoàn tất
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideOverlay", "hideOverlay();", true);
            }
        }

        private void DangKyTuVan(string idTuVan, string idBenhNhan, string idBacSi, DateTime ngay, TimeSpan gio, string trieuChung, string linkGoogleMeet)
        {
            string sql = "INSERT INTO LichTuVan (IDTuVan, IDBenhNhan, IDBacSi, Ngay, Gio, TrieuChung, LinkGoogleMeet) " +
                         "VALUES (@IDTuVan, @IDBenhNhan, @IDBacSi, @Ngay, @Gio, @TrieuChung, @LinkGoogleMeet)";

            SqlParameter[] parameters = {
                new SqlParameter("@IDTuVan", idTuVan),
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                new SqlParameter("@IDBacSi", idBacSi),
                new SqlParameter("@Ngay", ngay),
                new SqlParameter("@Gio", gio),
                new SqlParameter("@TrieuChung", trieuChung),
                new SqlParameter("@LinkGoogleMeet", linkGoogleMeet)
            };

            int result = db.CapNhat(sql, parameters);

            if (result > 0)
            {
                GửiEmailThamSoBenhNhan(idTuVan, idBenhNhan, linkGoogleMeet, ngay, gio);
                Response.Write("<script>alert('Đăng ký tư vấn thành công!');</script>");
            }
            else
            {
                Response.Write("<script>alert('Đăng ký tư vấn không thành công!');</script>");
            }
        }

        private void GửiEmailThamSoBenhNhan(string idTuVan, string idBenhNhan, string linkGoogleMeet, DateTime ngay, TimeSpan gio)
        {
            try
            {
                // Lấy email bệnh nhân từ database
                string emailBenhNhan = LayEmailBenhNhan(idBenhNhan);

                if (string.IsNullOrEmpty(emailBenhNhan))
                {
                    Response.Write("<script>alert('Không tìm thấy email của bệnh nhân.');</script>");
                    return;
                }

                // Tạo nội dung email
                string subject = "BANANA HOSPITAL XIN CHÀO QUÝ KHÁCH";
                string body = $"Thông tin cuộc tư vấn\n" +
                              $"Bạn đã đăng ký cuộc tư vấn với bác sĩ.\n" +
                              $"ID Tư Vấn: {idTuVan}\n" +
                              $"Link Google Meet: {linkGoogleMeet}\n" +
                              $"Ngày: {ngay:dd-MM-yyyy}\n" +
                              $"Giờ: {gio:hh\\:mm}\n" +
                              $"Bạn vui lòng vào cuộc họp online trước 5 phút để đề phòng phát sinh sự cố! Banana trân trọng cảm ơn bạn";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bệnh viện Hospital", "bananahospitaldanang@gmail.com"));
                message.To.Add(new MailboxAddress("", emailBenhNhan));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { TextBody = body };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("bananahospitaldanang@gmail.com", "bvezpjcixxoypqvi"); // Sử dụng mật khẩu ứng dụng

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi khi gửi email: {ex.Message}');</script>");
            }
        }

        private string LayEmailBenhNhan(string idBenhNhan)
        {
            string sql = "SELECT Email FROM BenhNhan WHERE IDBenhNhan = @IDBenhNhan";
            SqlParameter[] parameters = { new SqlParameter("@IDBenhNhan", idBenhNhan) };
            DataTable dt = db.docdulieu(sql, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0]["Email"].ToString() : string.Empty;
        }
    }
}