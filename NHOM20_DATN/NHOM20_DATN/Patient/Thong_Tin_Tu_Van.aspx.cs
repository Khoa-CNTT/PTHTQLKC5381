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
using System.Linq;

namespace NHOM20_DATN.Patient
{
    public partial class Thong_Tin_Tu_Van : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();

        private string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
        private string partnerCode = "MOMO";
        private string accessKey = "F8BBA842ECF85";
        private string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
        private string redirectUrl = "https://b282-2001-ee0-4b43-4fe0-e4e1-992b-478c-7092.ngrok-free.app/Patient/Thong_Tin_Tu_Van.aspx";
        private string ipnUrl = "https://b282-2001-ee0-4b43-4fe0-e4e1-992b-478c-7092.ngrok-free.app/Patient/Thong_Tin_Tu_Van.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDateRange();
                HienThiThongTinBacSi();

                if (Request.QueryString["resultCode"] != null && Request.QueryString["extraData"] != null)
                {
                    XuLyKetQuaMoMo();
                }

                if (Session["IDBenhNhan"] != null)
                {
                    LayThongTinBenhNhan(Session["IDBenhNhan"].ToString());
                }
            }
        }

        protected void txtNgay_TextChanged(object sender, EventArgs e)
        {
            CapNhatKhungGio(); // Gọi lại hàm cập nhật khung giờ
        }

        private void CapNhatKhungGio()
        {
            DateTime now = DateTime.Now;
            TimeSpan currentTime = now.TimeOfDay;

            string[] availableTimes = new string[]
            {
        "07:00", "07:30", "08:00", "08:30", "09:00", "09:30",
        "10:00", "10:30", "11:00",
        "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00"
            };

            ddlGio.Items.Clear();
            ddlGio.Items.Add(new ListItem("Chọn giờ", ""));

            DateTime selectedDate;
            if (DateTime.TryParse(txtNgay.Text, out selectedDate))
            {
                foreach (string timeStr in availableTimes)
                {
                    if (TimeSpan.TryParse(timeStr, out TimeSpan itemTime))
                    {
                        if (selectedDate.Date == now.Date)
                        {
                            if (itemTime > currentTime)
                            {
                                ddlGio.Items.Add(new ListItem(timeStr, timeStr));
                            }
                        }
                        else
                        {
                            ddlGio.Items.Add(new ListItem(timeStr, timeStr));
                        }
                    }
                }
            }
            else
            {
                ddlGio.Items.Clear();
                ddlGio.Items.Add(new ListItem("Vui lòng chọn ngày hợp lệ", ""));
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
            string partnerCode = Request.QueryString["partnerCode"];
            string orderType = Request.QueryString["orderType"];
            string transId = Request.QueryString["transId"];
            string payType = Request.QueryString["payType"];
            string responseTimeStr = Request.QueryString["responseTime"];
            string signature = Request.QueryString["signature"];
            string orderInfo = Request.QueryString["orderInfo"] ?? "";
            string extraDataEncoded = Request.QueryString["extraData"];

            if (string.IsNullOrEmpty(extraDataEncoded))
            {
                Response.Write("<script>alert('Thiếu thông tin extraData');</script>");
                return;
            }

            string extraData = HttpUtility.UrlDecode(extraDataEncoded);
            var dict = extraData.Split('&')
                        .Select(p => p.Split('='))
                        .Where(p => p.Length == 2)
                        .GroupBy(p => p[0])
                        .ToDictionary(g => g.Key, g => HttpUtility.UrlDecode(g.Select(x => x[1]).FirstOrDefault()));

            string idBenhNhan = dict.ContainsKey("IDBenhNhan") ? dict["IDBenhNhan"] : null;

            if (string.IsNullOrEmpty(idBenhNhan))
            {
                Response.Write("<script>alert('Không tìm thấy ID bệnh nhân trong extraData');</script>");
                return;
            }

            decimal soTien = Convert.ToDecimal(amount);
            long responseTime = 0;
            long.TryParse(responseTimeStr, out responseTime);

            if (resultCode == "0")
            {
                try
                {
                    LuuThanhToan(idBenhNhan, soTien, partnerCode, orderId, requestId,
                                 orderInfo, orderType, transId, int.Parse(resultCode), message,
                                 payType, responseTime, extraData, signature);

                    string idTuVan = TaoMaTuVan();
                    string linkJitsi = "https://meet.jit.si/" + Guid.NewGuid().ToString().Substring(0, 8);
                    DateTime ngay = DateTime.Parse(dict["Ngay"]);
                    string gioString = dict.ContainsKey("Gio") ? dict["Gio"] : null;

                    if (string.IsNullOrEmpty(gioString))
                    {
                        Response.Write("<script>alert('Không tìm thấy giờ trong extraData');</script>");
                        return;
                    }

                    if (gioString.Count(c => c == ':') == 1)
                    {
                        gioString += ":00";
                    }

                    if (!TimeSpan.TryParse(gioString, out TimeSpan gio))
                    {
                        Response.Write("<script>alert('Định dạng giờ không hợp lệ: " + gioString + "');</script>");
                        return;
                    }

                    string idBacSi = dict["IDBacSi"];
                    string trieuChung = dict["TrieuChung"];

                    if (string.IsNullOrEmpty(idBenhNhan) || string.IsNullOrEmpty(idBacSi))
                    {
                        Response.Write("<script>alert('ID bệnh nhân hoặc bác sĩ bị rỗng');</script>");
                        return;
                    }

                    // Đăng ký tư vấn
                    DangKyTuVan(idTuVan, idBenhNhan, idBacSi, ngay, gio, trieuChung, linkJitsi);

                    // Cập nhật ThanhToan với IDTuVan
                    CapNhatIDTuVan(orderId, idTuVan);

                    GửiEmailThamSoBenhNhan(idTuVan, idBenhNhan, linkJitsi, ngay, gio);

                    string script = "Swal.fire({ icon: 'success', title: 'Thanh toán thành công', text: 'Thông tin thanh toán đã được ghi nhận! Bạn vui lòng kiểm tra thông tin tư vấn qua mail.' });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "thongbao", script, true);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Lỗi lưu thanh toán: " + ex.Message.Replace("'", "") + "');</script>");
                }

                ThongBaoVaChuyenTrang("Đăng ký tư vấn thành công! Thông tin tư vấn sẽ gửi đến mail của bạn.", redirectUrl);
            }
            else
            {
                try
                {
                    LuuThanhToan(idBenhNhan, soTien, partnerCode, orderId, requestId,
                                 orderInfo, orderType, transId, int.Parse(resultCode), message,
                                 payType, responseTime, extraData, signature);

                    Response.Write("<script>console.log('Đã lưu thanh toán thất bại');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Lỗi khi lưu trạng thái thất bại: " + ex.Message.Replace("'", "") + "');</script>");
                }

                Response.Write("<script>alert('Thanh toán thất bại hoặc bị hủy.');</script>");
            }
        }

        private void CapNhatIDTuVan(string orderId, string idTuVan)
        {
            string sql = "UPDATE ThanhToan SET IDTuVan = @IDTuVan WHERE OrderId = @OrderId";
            SqlParameter[] parameters = {
        new SqlParameter("@IDTuVan", idTuVan),
        new SqlParameter("@OrderId", orderId)
    };
            db.CapNhat(sql, parameters);
        }

        private async Task TaoYeuCauThanhToanMoMo(string extraDataParams)
        {
            string orderId = DateTime.Now.Ticks.ToString();
            string requestId = orderId;
            int amount = 50000;
            string orderInfo = "Thanh toán tư vấn trực tuyến";

            string idBenhNhan = Session["IDBenhNhan"]?.ToString() ?? "Unknown";
            string extraData = HttpUtility.UrlEncode($"IDBenhNhan={idBenhNhan}&OrderId={orderId}&{extraDataParams}");

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

            using (var client = new HttpClient())
            {
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                string responseString = await response.Content.ReadAsStringAsync();

                var jsonResponse = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                string payUrl = jsonResponse["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    Response.Redirect(payUrl, false);
                }
                else
                {
                    Response.Write("<script>alert('Không thể tạo yêu cầu thanh toán. Vui lòng thử lại.');</script>");
                }
            }
        }

        private void LuuThanhToan(
            string idBenhNhan, decimal amount, string partnerCode, string orderId, string requestId,
            string orderInfo, string orderType, string transId, int resultCode, string message,
            string payType, long responseTime, string extraData, string signature)
        {
            if (string.IsNullOrEmpty(idBenhNhan) || amount <= 0)
            {
                throw new ArgumentException("Dữ liệu đầu vào không hợp lệ");
            }

            string sql = @"
                INSERT INTO ThanhToan 
                (PartnerCode, OrderId, RequestId, Amount, OrderInfo, OrderType, 
                 TransId, ResultCode, Message, PayType, ResponseTime, ExtraData, 
                 Signature, IDBenhNhan, NgayThanhToan)
                VALUES 
                (@PartnerCode, @OrderId, @RequestId, @Amount, @OrderInfo, @OrderType, 
                 @TransId, @ResultCode, @Message, @PayType, @ResponseTime, @ExtraData, 
                 @Signature, @IDBenhNhan, GETDATE())";

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@PartnerCode", partnerCode),
                    new SqlParameter("@OrderId", orderId),
                    new SqlParameter("@RequestId", requestId),
                    new SqlParameter("@Amount", amount),
                    new SqlParameter("@OrderInfo", orderInfo),
                    new SqlParameter("@OrderType", orderType),
                    new SqlParameter("@TransId", transId),
                    new SqlParameter("@ResultCode", resultCode),
                    new SqlParameter("@Message", message),
                    new SqlParameter("@PayType", payType),
                    new SqlParameter("@ResponseTime", responseTime),
                    new SqlParameter("@ExtraData", extraData),
                    new SqlParameter("@Signature", signature),
                    new SqlParameter("@IDBenhNhan", idBenhNhan)
                };

                db.CapNhat(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu thanh toán: " + ex.Message);
                throw;
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
                <script src='https://cdn.jsdelivr.net/npm/sweetalert2@11'></script>
                <script>
                    Swal.fire({{
                        icon: 'success',
 WELL                        title: '{message}',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                        didClose: () => {{
                            window.location.href = '{url}';
                        }}
                    }});
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

                if (KiemTraTrungLichBenhNhan(idBenhNhan, ngay, gio))
                    throw new Exception("Bạn đã đăng ký tư vấn vào khung giờ này.");

                string trieuChung = txtTrieuChung.Text.Trim();

                var extraDataDict = new Dictionary<string, string>()
                {
                    { "IDBenhNhan", idBenhNhan },
                    { "IDBacSi", idBacSi },
                    { "Ngay", ngay.ToString("yyyy-MM-dd") },
                    { "Gio", gio.ToString(@"hh\:mm") },
                    { "TrieuChung", trieuChung }
                };

                string extraData = string.Join("&", extraDataDict.Select(kvp =>
                    $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value)}"));

                await TaoYeuCauThanhToanMoMo(extraData);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
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

        private bool KiemTraTrungLichBenhNhan(string idBenhNhan, DateTime ngay, TimeSpan gio)
        {
            string sql = @"
                SELECT COUNT(*) FROM LichTuVan
                WHERE IDBenhNhan = @IDBenhNhan AND Ngay = @Ngay AND Gio = @Gio
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                new SqlParameter("@Ngay", ngay.Date),
                new SqlParameter("@Gio", gio)
            };

            object ketqua = db.LayGiaTri(sql, parameters);
            return Convert.ToInt32(ketqua) > 0;
        }

        private void GửiEmailThamSoBenhNhan(string idTuVan, string idBenhNhan, string linkJitsi, DateTime ngay, TimeSpan gio)
        {
            try
            {
                if (string.IsNullOrEmpty(idTuVan) || string.IsNullOrEmpty(linkJitsi))
                    throw new ArgumentException("ID Tư vấn hoặc Link Jitsi không hợp lệ.");
                if (ngay == default(DateTime) || ngay == DateTime.MinValue)
                    throw new ArgumentException("Ngày không hợp lệ.");
                if (gio == TimeSpan.Zero || gio < TimeSpan.Zero)
                    throw new ArgumentException("Giờ không hợp lệ.");

                string emailBenhNhan = LayEmailBenhNhan(idBenhNhan);
                if (string.IsNullOrEmpty(emailBenhNhan))
                    throw new Exception("Không tìm thấy email của bệnh nhân.");

                string subject = "BANANA HOSPITAL XIN CHÀO QUÝ KHÁCH";

                string ngayFormatted = ngay.ToString("dd-MM-yyyy");
                string gioFormatted = gio.ToString(@"hh\:mm");
                string idTuVanEncoded = System.Web.HttpUtility.HtmlEncode(idTuVan);
                string linkJitsiEncoded = System.Web.HttpUtility.HtmlEncode(linkJitsi);

                string body = $@"
<div style='background-color: #f7f7f7; padding: 20px; font-family: Arial, sans-serif;'>
    <div style='max-width: 600px; margin: auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
        <h2 style='color: #4CAF50; text-align: center;'>BANANA HOSPITAL XIN CHÀO QUÝ KHÁCH</h2>
        <p style='font-size: 16px; color: #555;'>
            Bạn đã đăng ký cuộc tư vấn với bác sĩ của chúng tôi. Dưới đây là thông tin chi tiết:
        </p>
        <table style='width: 100%; font-size: 16px; color: #333; margin-bottom: 20px;'>
            <tr>
                <td style='padding: 8px 0;'><strong>ID Tư Vấn:</strong></td>
                <td style='padding: 8px 0;'>{idTuVanEncoded}</td>
            </tr>
            <tr>
                <td style='padding: 8px 0;'><strong>Link Jitsi:</strong></td>
                <td style='padding: 8px 0;'>
                    <a href='{linkJitsiEncoded}' target='_blank' style='color: #1E90FF; text-decoration: none;'>Tham gia Cuộc Họp</a>
                </td>
            </tr>
            <tr>
                <td style='padding: 8px 0;'><strong>Ngày:</strong></td>
                <td style='padding: 8px 0;'>{ngayFormatted}</td>
            </tr>
            <tr>
                <td style='padding: 8px 0;'><strong>Giờ:</strong></td>
                <td style='padding: 8px 0;'>{gioFormatted}</td>
            </tr>
        </table>
        <p style='font-size: 16px; color: #555;'>
            Bạn vui lòng vào cuộc họp online trước 5 phút để đề phòng phát sinh sự cố!<br><br>
            Nếu có thắc mắc, xin liên hệ với chúng tôi qua <strong>hotline 1900 3456</strong> để được hỗ trợ chi tiết.
        </p>
        <p style='font-size: 16px; color: #4CAF50; font-weight: bold; text-align: center; margin-top: 30px;'>
            Cảm ơn bạn đã tin tưởng lựa chọn dịch vụ tại Bệnh viện BANANA!<br>
            <em>Nơi an tâm sức khỏe, gửi trọn hi vọng và niềm tin!</em>
        </p>
    </div>
</div>";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bệnh viện Banana Hospital", "bananahospitaldanang@gmail.com"));
                message.To.Add(new MailboxAddress("", emailBenhNhan));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                message.Body = bodyBuilder.ToMessageBody();

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
                string errorMessage = $"Lỗi khi gửi email: {ex.Message}. Giá trị: ngay={ngay}, gio={gio}";
                Response.Write($"<script>alert('{errorMessage}');</script>");
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