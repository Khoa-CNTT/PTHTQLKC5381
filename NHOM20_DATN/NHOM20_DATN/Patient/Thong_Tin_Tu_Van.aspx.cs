using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using MailKit.Security;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NHOM20_DATN.Patient
{
    public partial class Thong_Tin_Tu_Van : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();

        // Thông tin MoMo
        private string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
        private string partnerCode = "MOMO";
        private string accessKey = "F8BBA842ECF85";
        private string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
        private string redirectUrl = "https://8809-14-165-151-227.ngrok-free.app/Patient/Tu_Van_Suc_Khoe_Truc_Tuyen.aspx";
        private string ipnUrl = "https://8809-14-165-151-227.ngrok-free.app/Patient/Tu_Van_Suc_Khoe_Truc_Tuyen.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Các tác vụ khởi tạo thông thường
                SetDateRange();
                HienThiThongTinBacSi();

                // Xử lý kết quả thanh toán MoMo nếu có query string
                if (Request.QueryString["resultCode"] != null)
                {
                    XuLyKetQuaMoMo();
                }

                // Hiển thị thông tin bệnh nhân nếu có IDBenhNhan trong session
                if (Session["IDBenhNhan"] != null)
                {
                    LayThongTinBenhNhan(Session["IDBenhNhan"].ToString());
                }
            }
        }



        private void XuLyKetQuaMoMo()
        {
            if (Request.QueryString["resultCode"] == null) return;

            string resultCode = Request.QueryString["resultCode"];
            string amount = Request.QueryString["amount"];
            string orderId = Request.QueryString["orderId"];
            string requestId = Request.QueryString["requestId"];
            string message = Request.QueryString["message"];

            var thongTinThanhToan = Session["ThongTinThanhToan"] as Dictionary<string, object>;
            if (thongTinThanhToan == null)
            {
                Response.Write("<script>alert('Session ThongTinThanhToan bị null');</script>");
                return;
            }

            string idBenhNhan = thongTinThanhToan["IDBenhNhan"].ToString();
            decimal soTien = Convert.ToDecimal(amount);

            if (resultCode == "0") // Thanh toán thành công
            {
                try
                {
                    // Lưu thông tin thanh toán thành công
                    LuuThanhToan(idBenhNhan, soTien, "DaThanhToan", orderId, requestId, resultCode, message);
                    Response.Write("<script>console.log('Lưu thanh toán thành công');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Lỗi lưu thanh toán: " + ex.Message.Replace("'", "") + "');</script>");
                }

                var tuVanTemp = Session["TuVanTemp"] as Dictionary<string, object>;
                if (tuVanTemp == null)
                {
                    Response.Write("<script>alert('Session TuVanTemp bị null');</script>");
                    return;
                }

                try
                {
                    string idTuVan = TaoMaTuVan();
                    string linkJitsi = "https://meet.jit.si/" + Guid.NewGuid().ToString().Substring(0, 8);
                    DateTime ngay = (DateTime)tuVanTemp["Ngay"];
                    TimeSpan gio = (TimeSpan)tuVanTemp["Gio"];
                    string idBacSi = tuVanTemp["IDBacSi"].ToString();
                    string trieuChung = tuVanTemp["TrieuChung"].ToString();

                    // Kiểm tra giá trị đầu vào
                    if (string.IsNullOrEmpty(idBenhNhan) || string.IsNullOrEmpty(idBacSi))
                    {
                        Response.Write("<script>alert('ID bệnh nhân hoặc bác sĩ bị rỗng');</script>");
                        return;
                    }

                    // Đăng ký tư vấn
                    DangKyTuVan(idTuVan, idBenhNhan, idBacSi, ngay, gio, trieuChung, linkJitsi);
                    Response.Write("<script>console.log('Lưu tư vấn thành công');</script>");

                    // Gửi email thông báo
                    GửiEmailThamSoBenhNhan(idTuVan, idBenhNhan, linkJitsi, ngay, gio);
                    Session.Remove("TuVanTemp");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Lỗi khi đăng ký tư vấn: " + ex.Message.Replace("'", "") + "');</script>");
                }

                ThongBaoVaChuyenTrang("Thanh toán thành công!", redirectUrl);
            }
            else
            {
                try
                {
                    // Lưu trạng thái thanh toán thất bại
                    LuuThanhToan(idBenhNhan, soTien, "ThatBai", orderId, requestId, resultCode, message);
                    Response.Write("<script>console.log('Đã lưu thanh toán thất bại');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Lỗi khi lưu trạng thái thất bại: " + ex.Message.Replace("'", "") + "');</script>");
                }

                Response.Write("<script>alert('Thanh toán thất bại hoặc bị hủy.');</script>");
            }

            // Xóa thông tin session sau khi xử lý xong
            Session.Remove("ThongTinThanhToan");
        }

        private async Task TaoYeuCauThanhToanMoMo()
        {
            string orderId = DateTime.Now.Ticks.ToString();
            string requestId = orderId;
            int amount = 50000;
            string orderInfo = "Thanh toán tư vấn trực tuyến";
            string extraData = "";

            string rawHash = $"accessKey={accessKey}&amount={amount}&extraData={extraData}&ipnUrl={ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType=payWithATM";
            string signature = HmacSHA256(secretKey, rawHash);

            var paymentRequest = new
            {
                partnerCode,
                accessKey,
                requestId,
                amount = amount.ToString(),
                orderId,
                orderInfo,
                redirectUrl,
                ipnUrl,
                extraData,
                requestType = "payWithATM",
                signature,
                lang = "vi"
            };

            // Lưu thông tin thanh toán vào Session
            string idBenhNhan = Session["IDBenhNhan"]?.ToString();
            Session["ThongTinThanhToan"] = new Dictionary<string, object>
    {
        { "IDBenhNhan", idBenhNhan },
        { "Amount", amount },
        { "OrderId", orderId },
        { "RequestId", requestId }
    };

            // Gửi request đến MoMo
            using (var client = new HttpClient())
            {
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                string responseString = await response.Content.ReadAsStringAsync();

                var jsonResponse = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                string payUrl = jsonResponse["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    Response.Redirect(payUrl);
                }
                else
                {
                    Response.Write("<script>alert('Không thể tạo yêu cầu thanh toán. Vui lòng thử lại.');</script>");
                }
            }
        }


        private void LuuThanhToan(string idBenhNhan, decimal soTien, string trangThai, string orderId, string requestId, string resultCode, string message)
        {
            // Kiểm tra đầu vào để đảm bảo không bị null hoặc sai dữ liệu
            if (string.IsNullOrEmpty(idBenhNhan) || soTien <= 0)
            {
                // Đưa ra lỗi nếu ID bệnh nhân không hợp lệ hoặc số tiền không hợp lệ
                throw new ArgumentException("Dữ liệu đầu vào không hợp lệ");
            }

            // Sinh mã ID thanh toán tự động
            string idThanhToan = "TT" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Truy vấn SQL để lưu dữ liệu vào bảng ThanhToan
            string sql = @"INSERT INTO ThanhToan (IDThanhToan, IDBenhNhan, SoTien, TrangThai, OrderId, RequestId, ResultCode, Message, NgayThanhToan)
                   VALUES (@IDThanhToan, @IDBenhNhan, @SoTien, @TrangThai, @OrderId, @RequestId, @ResultCode, @Message, GETDATE())";

            try
            {
                // Tạo các tham số SQL
                SqlParameter[] parameters =
                {
            new SqlParameter("@IDThanhToan", idThanhToan),
            new SqlParameter("@IDBenhNhan", idBenhNhan),
            new SqlParameter("@SoTien", soTien),
            new SqlParameter("@TrangThai", trangThai),
            new SqlParameter("@OrderId", orderId),
            new SqlParameter("@RequestId", requestId),
            new SqlParameter("@ResultCode", resultCode),
            new SqlParameter("@Message", message)
        };

                // Cập nhật cơ sở dữ liệu
                db.CapNhat(sql, parameters);
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý lỗi tùy theo yêu cầu
                Console.WriteLine("Lỗi khi lưu thanh toán: " + ex.Message);
                throw;  // Ném lại lỗi
            }
        }




        private string HmacSHA256(string key, string message)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        private void ThongBaoVaChuyenTrang(string message, string url)
        {
            string script = $@"
        <script>
            alert('{message}');
            setTimeout(function() {{
                window.location.href = '{url}';
            }}, 1500);
        </script>";
            Response.Write(script);
        }

        private void HienThiThongTinBacSi()
        {
            lblTenBacSi.Text = Request.QueryString["TenBacSi"];
            lblChuyenKhoa.Text = Request.QueryString["ChuyenKhoa"];
            lblTrinhDo.Text = Request.QueryString["TrinhDo"];
            lblEmail.Text = Request.QueryString["Email"];
            lblVaiTro.Text = Request.QueryString["VaiTro"];
            lblIDBacSi.Text = Request.QueryString["IDBacSi"];
        }

        private void SetDateRange()
        {
            DateTime today = DateTime.Today;
            txtNgay.Attributes.Add("min", today.ToString("yyyy-MM-dd"));
            txtNgay.Attributes.Add("max", today.AddMonths(2).AddDays(-today.Day).ToString("yyyy-MM-dd"));
        }

        private void LayThongTinBenhNhan(string idBenhNhan)
        {
            string sql = "SELECT IDBenhNhan FROM BenhNhan WHERE IDBenhNhan = @IDBenhNhan";
            SqlParameter[] parameters = { new SqlParameter("@IDBenhNhan", idBenhNhan) };
            DataTable dt = db.docdulieu(sql, parameters);
            if (dt.Rows.Count > 0)
                lblIDBenhNhan.Text = dt.Rows[0]["IDBenhNhan"].ToString();
            else
                lblIDBenhNhan.Text = "Không tìm thấy thông tin bệnh nhân.";
        }

        private string TaoMaTuVan()
        {
            return "TV" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        protected async void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                string idBenhNhan = lblIDBenhNhan.Text;
                string idBacSi = lblIDBacSi.Text;

                if (!DateTime.TryParse(txtNgay.Text, out DateTime ngay))
                    throw new Exception("Ngày không hợp lệ.");

                if (!TimeSpan.TryParse(ddlGio.Text, out TimeSpan gio))
                    throw new Exception("Giờ không hợp lệ.");

                if (!((gio >= TimeSpan.FromHours(7) && gio <= TimeSpan.FromHours(11)) ||
                      (gio >= TimeSpan.FromHours(14) && gio <= TimeSpan.FromHours(17))))
                    throw new Exception("Thời gian tư vấn phải trong khoảng 7:00-11:00 hoặc 14:00-17:00.");

                if (KiemTraTrungLich(idBacSi, ngay, gio))
                    throw new Exception("Bác sĩ đã có lịch tư vấn vào thời điểm này.");

                string trieuChung = txtTrieuChung.Text.Trim();

                Session["TuVanTemp"] = new Dictionary<string, object>
        {
            {"IDBenhNhan", idBenhNhan},
            {"IDBacSi", idBacSi},
            {"Ngay", ngay},
            {"Gio", gio},
            {"TrieuChung", trieuChung}
        };

                await TaoYeuCauThanhToanMoMo();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}');</script>");
            }
        }



        private void DangKyTuVan(string idTuVan, string idBenhNhan, string idBacSi, DateTime ngay, TimeSpan gio, string trieuChung, string linkJitsi)
        {
            try
            {
                // Câu lệnh SQL để chèn cuộc tư vấn vào bảng LichTuVan
                string sql = "INSERT INTO LichTuVan (IDTuVan, IDBenhNhan, IDBacSi, Ngay, Gio, TrieuChung, LinkJitsi) " +
                             "VALUES (@IDTuVan, @IDBenhNhan, @IDBacSi, @Ngay, @Gio, @TrieuChung, @LinkJitsi)";

                // Cấu hình các tham số cho câu lệnh SQL
                SqlParameter[] parameters = {
            new SqlParameter("@IDTuVan", idTuVan),
            new SqlParameter("@IDBenhNhan", idBenhNhan),
            new SqlParameter("@IDBacSi", idBacSi),
            new SqlParameter("@Ngay", ngay),
            new SqlParameter("@Gio", gio),
            new SqlParameter("@TrieuChung", trieuChung),
            new SqlParameter("@LinkJitsi", linkJitsi)
        };

                // Thực hiện câu lệnh SQL
                int result = db.CapNhat(sql, parameters);

                // Kiểm tra kết quả và gửi email nếu thành công
                if (result > 0)
                {
                    GửiEmailThamSoBenhNhan(idTuVan, idBenhNhan, linkJitsi, ngay, gio);
                }
                else
                {
                    throw new Exception("Không thể đăng ký cuộc tư vấn.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Response.Write($"<script>alert('Lỗi khi đăng ký tư vấn: {ex.Message}');</script>");
            }
        }


        private bool KiemTraTrungLich(string idBacSi, DateTime ngay, TimeSpan gio)
        {
            string sql = "SELECT COUNT(*) FROM LichTuVan WHERE IDBacSi = @IDBacSi AND Ngay = @Ngay AND Gio = @Gio";
            SqlParameter[] parameters = {
                new SqlParameter("@IDBacSi", idBacSi),
                new SqlParameter("@Ngay", ngay),
                new SqlParameter("@Gio", gio)
            };
            DataTable dt = db.docdulieu(sql, parameters);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        private void GửiEmailThamSoBenhNhan(string idTuVan, string idBenhNhan, string linkJitsi, DateTime ngay, TimeSpan gio)
        {
            try
            {
                // Lấy email của bệnh nhân
                string emailBenhNhan = LayEmailBenhNhan(idBenhNhan);
                if (string.IsNullOrEmpty(emailBenhNhan)) throw new Exception("Không tìm thấy email của bệnh nhân.");

                // Tiêu đề và nội dung email
                string subject = "BANANA HOSPITAL XIN CHÀO QUÝ KHÁCH";
                string body = $"Thông tin cuộc tư vấn\n" +
                              $"Bạn đã đăng ký cuộc tư vấn với bác sĩ.\n" +
                              $"ID Tư Vấn: {idTuVan}\n" +
                              $"Link Jitsi: {linkJitsi}\n" +
                              $"Ngày: {ngay:dd-MM-yyyy}\n" +
                              $"Giờ: {gio:hh\\:mm}\n" +
                              $"Bạn vui lòng vào cuộc họp online trước 5 phút để đề phòng phát sinh sự cố! Banana trân trọng cảm ơn bạn";

                // Tạo tin nhắn email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bệnh viện Banana Hospital", "bananahospitaldanang@gmail.com"));
                message.To.Add(new MailboxAddress("", emailBenhNhan));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder { TextBody = body };
                message.Body = bodyBuilder.ToMessageBody();

                // Gửi email
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("bananahospitaldanang@gmail.com", "bvezpjcixxoypqvi");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và hiển thị thông báo
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