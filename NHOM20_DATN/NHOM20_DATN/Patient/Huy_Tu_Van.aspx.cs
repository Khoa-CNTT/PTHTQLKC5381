using MimeKit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Net.Http;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;
namespace NHOM20_DATN.Patient
{
    public partial class Huy_Tu_Van : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();

        private string refundEndpoint = "https://test-payment.momo.vn/v2/gateway/api/refund";
        private string partnerCode = "MOMO";
        private string accessKey = "F8BBA842ECF85";
        private string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IDBenhNhan"] == null)
                {
                    Response.Redirect("~/Dang_Nhap.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    btnHuy.Enabled = false;
                }
                else
                {
                    btnHuy.Enabled = true;
                }
            }
        }

        protected async void btnHuy_Click(object sender, EventArgs e)
        {
            string idTuVan = txtIDTuVan.Text.Trim();
            string idBenhNhan = Session["IDBenhNhan"]?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(idTuVan) || string.IsNullOrEmpty(idBenhNhan))
            {
                ThongBao("Bạn phải nhập mã tư vấn hợp lệ.");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);
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
                    DateTime ngayTuVan = Convert.ToDateTime(dt.Rows[0]["Ngay"]);
                    TimeSpan gioTuVan = TimeSpan.Parse(dt.Rows[0]["Gio"].ToString());
                    DateTime thoiGianTuVan = ngayTuVan.Add(gioTuVan);
                    TimeSpan thoiGianConLai = thoiGianTuVan - DateTime.Now;

                    if (thoiGianConLai.TotalHours < 10)
                    {
                        ThongBao("Bạn chỉ có thể hủy tư vấn trước ít nhất 10 giờ so với thời điểm tư vấn.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);
                        return;
                    }

                    var transaction = KiemTraGiaoDichThanhToan(idTuVan, idBenhNhan);
                    if (transaction == null || transaction.ResultCode != 0)
                    {
                        ThongBao("Không tìm thấy giao dịch thanh toán hợp lệ hoặc giao dịch không thành công.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);
                        return;
                    }

                    string sqlDeleteThanhToan = "DELETE FROM ThanhToan WHERE IDTuVan = @IDTuVan";
                    SqlParameter[] parametersDeleteThanhToan = { new SqlParameter("@IDTuVan", idTuVan) };
                    db.CapNhat(sqlDeleteThanhToan, parametersDeleteThanhToan);



                    string sqlDelete = "DELETE FROM LichTuVan WHERE IDTuVan = @IDTuVan";
                    SqlParameter[] parametersDelete = {
                new SqlParameter("@IDTuVan", idTuVan)
            };
                    int result = db.CapNhat(sqlDelete, parametersDelete);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);


                    if (result > 0)
                    {
                        bool refundSuccess = await ThucHienHoanTien(transaction.OrderId, transaction.TransId, transaction.Amount, idBenhNhan);
                        if (refundSuccess)
                        {
                            CapNhatTrangThaiHoanTien(transaction.OrderId, "Refunded");
                            GửiEmailXacNhanHuy(idBenhNhan, idTuVan, transaction.Amount);
                            ThongBao("Hủy tư vấn và hoàn tiền thành công. Vui lòng kiểm tra email để biết thêm chi tiết.");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);
                        }
                        else
                        {
                            GửiEmailXacNhanHuy(idBenhNhan, idTuVan, transaction.Amount, false);
                            ThongBao("Hủy tư vấn thành công nhưng hoàn tiền thất bại. Vui lòng liên hệ hỗ trợ.");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);

                        }
                    }
                    else
                    {
                        ThongBao("Không thể hủy tư vấn. Vui lòng thử lại.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);

                    }
                }
                else
                {
                    ThongBao("Mã tư vấn không tồn tại hoặc không thuộc về bạn.");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOverlay", "hideOverlay();", true);

                }
            }
            catch (Exception ex)
            {
            }
        }

        private class TransactionInfo
        {
            public string OrderId { get; set; }
            public string TransId { get; set; }
            public decimal Amount { get; set; }
            public int ResultCode { get; set; }
        }

        private TransactionInfo KiemTraGiaoDichThanhToan(string idTuVan, string idBenhNhan)
        {
            try
            {
                string sql = @"
            SELECT OrderId, TransId, Amount, ResultCode, ExtraData 
            FROM ThanhToan 
            WHERE IDBenhNhan = @IDBenhNhan 
            AND IDTuVan = @IDTuVan";
                SqlParameter[] parameters = {
            new SqlParameter("@IDBenhNhan", idBenhNhan),
            new SqlParameter("@IDTuVan", idTuVan)
        };
                DataTable dt = db.docdulieu(sql, parameters);

                if (dt.Rows.Count > 0)
                {
                    string debugInfo = $"OrderId: {dt.Rows[0]["OrderId"]}, TransId: {dt.Rows[0]["TransId"]}, Amount: {dt.Rows[0]["Amount"]}, ResultCode: {dt.Rows[0]["ResultCode"]}, ExtraData: {dt.Rows[0]["ExtraData"]}";

                    return new TransactionInfo
                    {
                        OrderId = dt.Rows[0]["OrderId"].ToString(),
                        TransId = dt.Rows[0]["TransId"].ToString(),
                        Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]),
                        ResultCode = Convert.ToInt32(dt.Rows[0]["ResultCode"])
                    };
                }
                else
                {
                    string sqlAll = "SELECT OrderId, TransId, Amount, ResultCode, ExtraData FROM ThanhToan WHERE IDBenhNhan = @IDBenhNhan";
                    SqlParameter[] paramAll = { new SqlParameter("@IDBenhNhan", idBenhNhan) };
                    DataTable dtAll = db.docdulieu(sqlAll, paramAll);
                    string debugAll = $"TongSoBanGhiThanhToan: {dtAll.Rows.Count}";
                    if (dtAll.Rows.Count > 0)
                    {
                        debugAll += "\\n" + string.Join("\\n", dtAll.AsEnumerable().Select(row => $"OrderId: {row["OrderId"]}, ExtraData: {row["ExtraData"]}"));
                    }
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "debug", $"alert('KhongTimThayGiaoDich: {debugAll.Replace("'", "\\'")}');", true);
                }
                return null;
            }
            catch (Exception ex)
            {
                ThongBao("Lỗi truy vấn ThanhToan: " + ex.Message + "\\nInnerException: " + (ex.InnerException?.Message ?? "None"));
                return null;
            }
        }

        private async Task<bool> ThucHienHoanTien(string orderId, string transId, decimal amount, string idBenhNhan)
        {
            try
            {
                string requestId = DateTime.Now.Ticks.ToString();
                string description = $"Hoàn tiền cho tư vấn của bệnh nhân {idBenhNhan}";
                string rawHash = $"accessKey={accessKey}&amount={amount}&description={description}&orderId={orderId}&partnerCode={partnerCode}&requestId={requestId}&transId={transId}";
                string signature = HmacSHA256(secretKey, rawHash);

                var refundRequest = new
                {
                    partnerCode,
                    orderId,
                    requestId,
                    amount = (long)amount,
                    transId,
                    accessKey,
                    description,
                    signature,
                    lang = "vi"
                };

                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(refundRequest), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(refundEndpoint, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "debug", $"alert('MoMo Response: {responseString}');", true);

                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
                    int resultCode = jsonResponse.resultCode;

                    return resultCode == 0;
                }
            }
            catch (Exception ex)
            {
                ThongBao("Lỗi khi thực hiện hoàn tiền: " + ex.Message + "\\nInnerException: " + (ex.InnerException?.Message ?? "None"));
                return false;
            }
        }

        private void CapNhatTrangThaiHoanTien(string orderId, string status)
        {
            string sql = "UPDATE ThanhToan SET RefundStatus = @RefundStatus WHERE OrderId = @OrderId";
            SqlParameter[] parameters = {
                new SqlParameter("@RefundStatus", status),
                new SqlParameter("@OrderId", orderId)
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

        private void GửiEmailXacNhanHuy(string idBenhNhan, string idTuVan, decimal amount = 0, bool refundSuccess = true)
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
                string refundMessage = refundSuccess
                    ? $"<p>Số tiền <strong>{amount:N0} VND</strong> đã được hoàn lại vào tài khoản của bạn. Vui lòng kiểm tra tài khoản thanh toán.</p>"
                    : "<p>Hoàn tiền không thành công. Vui lòng liên hệ hỗ trợ qua email bananahospitaldanang@gmail.com.</p>";

                string body = @"
                <html>
                <head>
                    <style>
                        body {
                            font-family: 'Segoe UI', Tahoma, Verdana, sans-serif;
                            background-color: #f0f2f5;
                            margin: 0;
                            padding: 20px;
                        }
                        .email-container {
                            background-color: #ffffff;
                            padding: 30px;
                            border-radius: 12px;
                            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                            max-width: 600px;
                            margin: 0 auto;
                            border: 1px solid #d1e7dd;
                        }
                        .email-header {
                            text-align: center;
                            color: #198754;
                            margin-bottom: 25px;
                            font-size: 24px;
                        }
                        .email-content {
                            font-size: 16px;
                            color: #333333;
                            line-height: 1.7;
                        }
                        .email-content strong {
                            color: #0d6efd;
                        }
                        .btn-support {
                            display: inline-block;
                            margin-top: 25px;
                            padding: 12px 24px;
                            background-color: #0d6efd;
                            color: #ffffff !important;
                            text-decoration: none;
                            border-radius: 6px;
                            font-weight: 600;
                        }
                        .email-footer {
                            margin-top: 35px;
                            text-align: center;
                            font-size: 14px;
                            color: #6c757d;
                        }
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <h2 class='email-header'>XÁC NHẬN HỦY TƯ VẤN</h2>
                        <div class='email-content'>
                            <p>Chào bạn,</p>
                            <p>Chúng tôi xin xác nhận rằng yêu cầu hủy tư vấn với mã <strong>" + idTuVan + @"</strong> của bạn đã được xử lý thành công.</p>
                            " + refundMessage + @"
                            <p>Chúng tôi rất tiếc vì bạn đã phải hủy cuộc tư vấn. Nếu bạn cần hỗ trợ thêm hoặc muốn đặt lịch tư vấn mới, xin vui lòng liên hệ với đội ngũ hỗ trợ của chúng tôi.</p>
                            <a href='mailto:bananahospitaldanang@gmail.com' class='btn-support'>Liên hệ hỗ trợ</a>
                            <p style='margin-top: 20px;'>Xin cảm ơn bạn đã tin tưởng và sử dụng dịch vụ của chúng tôi.</p>
                            <p>Chúc bạn một ngày tốt lành!</p>
                        </div>
                        <div class='email-footer'>
                            © 2025 Banana Hospital. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bệnh viện", "bananahospitaldanang@gmail.com"));
                message.To.Add(new MailboxAddress("", emailBenhNhan));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("bananahospitaldanang@gmail.com", "bvezpjcixxoypqvi");
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
                    }}, 5000);
                }}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CustomAlert", script, true);
        }
    }
}