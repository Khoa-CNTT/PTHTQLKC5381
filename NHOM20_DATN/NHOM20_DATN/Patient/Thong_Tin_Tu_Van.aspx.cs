using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MimeKit;
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
        private string redirectUrl = "https://a3b6-113-165-214-240.ngrok-free.app/Patient/Tu_Van_Suc_Khoe_Truc_Tuyen.aspx";
        private string ipnUrl = "https://a3b6-113-165-214-240.ngrok-free.app/Patient/Tu_Van_Suc_Khoe_Truc_Tuyen.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDateRange();
                XuLyKetQuaMoMo();
                HienThiThongTinBacSi();
                var thongTinThanhToan = Session["ThongTinThanhToan"] as Dictionary<string, object>;
                if (thongTinThanhToan != null)
                {
                    string idBenhNhan = thongTinThanhToan["IDBenhNhan"].ToString();
                    LayThongTinBenhNhan(idBenhNhan);
                }
            }
        }

        private void XuLyKetQuaMoMo()
        {
            if (Request.QueryString["resultCode"] == null) return;

            string resultCode = Request.QueryString["resultCode"];
            string amount = Request.QueryString["amount"];
            var thongTinThanhToan = Session["ThongTinThanhToan"] as Dictionary<string, object>;

            if (thongTinThanhToan == null) return;

            string idBenhNhan = thongTinThanhToan["IDBenhNhan"].ToString();
            string idPhieu = thongTinThanhToan["IDPhieu"].ToString();
            decimal soTien = Convert.ToDecimal(amount);

            if (resultCode == "0") // Thanh toán thành công
            {
                LuuThanhToan(idBenhNhan, idPhieu, soTien, "DaThanhToan");

                var tuVanTemp = Session["TuVanTemp"] as Dictionary<string, object>;
                if (tuVanTemp != null)
                {
                    string idTuVan = TaoMaTuVan();
                    string linkJitsi = "https://meet.jit.si/" + Guid.NewGuid().ToString().Substring(0, 8);
                    DateTime ngay = (DateTime)tuVanTemp["Ngay"];
                    TimeSpan gio = (TimeSpan)tuVanTemp["Gio"];
                    string idBacSi = tuVanTemp["IDBacSi"].ToString();
                    string trieuChung = tuVanTemp["TrieuChung"].ToString();

                    DangKyTuVan(idTuVan, idBenhNhan, idBacSi, ngay, gio, trieuChung, linkJitsi);
                    GửiEmailThamSoBenhNhan(idTuVan, idBenhNhan, linkJitsi, ngay, gio);
                    Session.Remove("TuVanTemp");
                }

                ThongBaoVaChuyenTrang("Thanh toán thành công!", redirectUrl);
            }
            else
            {
                LuuThanhToan(idBenhNhan, idPhieu, soTien, "ThatBai");
                Response.Write("<script>alert('Thanh toán thất bại hoặc bị hủy.');</script>");
            }

            // Có thể xóa luôn Session ở đây nếu cần
            // Session.Remove("ThongTinThanhToan");
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
            string idBenhNhan = (string)((Dictionary<string, object>)Session["TuVanTemp"])["IDBenhNhan"];
            string idPhieuTam = "PhieuTam_" + orderId;  // Tạo ID phiếu tạm
            var thongTinThanhToan = new Dictionary<string, object>
            {
                ["IDBenhNhan"] = idBenhNhan,
                ["IDPhieu"] = idPhieuTam,
                ["SoTien"] = amount,
                ["OrderId"] = orderId,
                ["RequestId"] = requestId
            };
            Session["ThongTinThanhToan"] = thongTinThanhToan;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(paymentRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();

                dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                string payUrl = jsonResponse.payUrl;

                // Chuyển hướng đến trang nhập số thẻ (test)
                Response.Redirect(payUrl);
            }

        }

        private void LuuThanhToan(string idBenhNhan, string idPhieu, decimal soTien, string trangThai)
        {
            string sql = "INSERT INTO ThanhToan (IDBenhNhan, IDPhieu, SoTien, TrangThai, ThoiGianThanhToan) " +
                         "VALUES (@IDBenhNhan, @IDPhieu, @SoTien, @TrangThai, @ThoiGianThanhToan)";

            SqlParameter[] parameters = {
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                new SqlParameter("@IDPhieu", idPhieu),
                new SqlParameter("@SoTien", soTien),
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@ThoiGianThanhToan", DateTime.Now)
    };

            db.CapNhat(sql, parameters);
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


        private void DangKyTuVan(string idTuVan, string idBenhNhan, string idBacSi, DateTime ngay, TimeSpan gio, string trieuChung, string linkJitsi)
        {
            try
            {
                string sql = "INSERT INTO LichTuVan (IDTuVan, IDBenhNhan, IDBacSi, Ngay, Gio, TrieuChung, LinkJitsi) " +
                             "VALUES (@IDTuVan, @IDBenhNhan, @IDBacSi, @Ngay, @Gio, @TrieuChung, @LinkJitsi)";

                SqlParameter[] parameters = {
                new SqlParameter("@IDTuVan", idTuVan),
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                new SqlParameter("@IDBacSi", idBacSi),
                new SqlParameter("@Ngay", ngay),
                new SqlParameter("@Gio", gio),
                new SqlParameter("@TrieuChung", trieuChung),
                new SqlParameter("@LinkJitsi", linkJitsi)
        };

                int result = db.CapNhat(sql, parameters);

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