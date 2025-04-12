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
using System.Security.Cryptography;
using System.Text;

namespace NHOM20_DATN.Patient
{
    public partial class Thong_Tin_Tu_Van : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();
        private string vnp_TmnCode = "X4TR6660";
        private string vnp_HashSecret = "EN10K7GAAI8F0OPG9ZK518CPKSG5HI QF";
        private string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private string vnp_ReturnUrl = "https://localhost:44395/Patient/Thong_Tin_Tu_Van.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDateRange();

                // Xử lý phản hồi từ VNPay
                if (Request.QueryString["vnp_TxnRef"] != null)
                {
                    string vnp_ResponseCode = Request.QueryString["vnp_ResponseCode"];
                    string vnp_TxnRef = Request.QueryString["vnp_TxnRef"];
                    string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];

                    if (KiemTraThanhToanVNPay(vnp_TxnRef, vnp_SecureHash))
                    {
                        if (vnp_ResponseCode == "00") // Thanh toán thành công
                        {
                            var tuVanTemp = Session["TuVanTemp"] as Dictionary<string, object>;
                            if (tuVanTemp != null)
                            {
                                string idTuVan = TaoMaTuVan();
                                string linkGoogleMeet = "https://meet.google.com/" + Guid.NewGuid().ToString().Substring(0, 8);
                                DangKyTuVan(idTuVan, tuVanTemp["IDBenhNhan"].ToString(), tuVanTemp["IDBacSi"].ToString(),
                                    (DateTime)tuVanTemp["Ngay"], (TimeSpan)tuVanTemp["Gio"], tuVanTemp["TrieuChung"].ToString(), linkGoogleMeet);
                                Session.Remove("TuVanTemp");
                                Response.Write("<script>alert('Đăng ký tư vấn thành công!');</script>");
                            }
                        }
                        else
                        {
                            Response.Write($"<script>alert('Thanh toán thất bại. Mã lỗi: {vnp_ResponseCode}');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Dữ liệu thanh toán không hợp lệ.');</script>");
                    }
                }

                // Lấy thông tin bác sĩ từ query string
                string tenBacSi = Request.QueryString["TenBacSi"];
                string chuyenKhoa = Request.QueryString["ChuyenKhoa"];
                string trinhDo = Request.QueryString["TrinhDo"];
                string email = Request.QueryString["Email"];
                string vaiTro = Request.QueryString["VaiTro"];
                string idBacSi = Request.QueryString["IDBacSi"];

                lblTenBacSi.Text = tenBacSi;
                lblChuyenKhoa.Text = chuyenKhoa;
                lblTrinhDo.Text = trinhDo;
                lblEmail.Text = email;
                lblVaiTro.Text = vaiTro;
                lblIDBacSi.Text = idBacSi;

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
            DateTime today = DateTime.Today;
            DateTime minDate = today;
            DateTime maxDate = today.AddMonths(2).AddDays(-today.Day);
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
                lblIDBenhNhan.Text = dt.Rows[0]["IDBenhNhan"].ToString();
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowOverlay", "showOverlay();", true);
            try
            {
                string idBenhNhan = lblIDBenhNhan.Text;
                if (!DateTime.TryParse(txtNgay.Text, out DateTime ngay)) throw new Exception("Ngày không hợp lệ.");
                if (!TimeSpan.TryParse(ddlGio.Text, out TimeSpan gio)) throw new Exception("Giờ không hợp lệ.");
                string trieuChung = txtTrieuChung.Text.Trim();
                string idBacSi = lblIDBacSi.Text;

                if (!((gio >= TimeSpan.FromHours(7) && gio <= TimeSpan.FromHours(11)) ||
                      (gio >= TimeSpan.FromHours(14) && gio <= TimeSpan.FromHours(17))))
                {
                    throw new Exception("Thời gian tư vấn chỉ có thể chọn từ 7:00-11:00 hoặc 14:00-17:00.");
                }

                if (string.IsNullOrEmpty(idBacSi) || string.IsNullOrEmpty(idBenhNhan))
                {
                    throw new Exception("Không tìm thấy thông tin bác sĩ hoặc bệnh nhân.");
                }

                if (KiemTraTrungLich(idBacSi, ngay, gio))
                {
                    throw new Exception("Bác sĩ đã có lịch tư vấn vào thời điểm này.");
                }

                // Lưu thông tin tạm vào Session
                Session["TuVanTemp"] = new Dictionary<string, object>
                {
                    {"IDBenhNhan", idBenhNhan},
                    {"IDBacSi", idBacSi},
                    {"Ngay", ngay},
                    {"Gio", gio},
                    {"TrieuChung", trieuChung}
                };

                // Tạo URL thanh toán và chuyển hướng
                string paymentUrl = TaoYeuCauThanhToanVNPay();
                Response.Redirect(paymentUrl, false); // Sử dụng false để tránh ThreadAbortException
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi: {ex.Message}');</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideOverlay", "hideOverlay();", true);
            }
        }

        private string TaoYeuCauThanhToanVNPay()
        {
            string vnp_TxnRef = DateTime.Now.Ticks.ToString();
            int amount = 50000;
            string vnp_OrderType = "billpayment"; // hoặc "billpayment", "topup", tùy loại dịch vụ
            string vnp_ExpireDate = DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss");
            string vnp_Amount = (amount * 100).ToString();
            string vnp_OrderInfo = "Thanh toan tu van truc tuyen";
            string vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string vnp_IpAddr = GetClientIpAddress(); // Sử dụng hàm mới để lấy IP



            var vnp_Params = new Dictionary<string, string>
        {
            {"vnp_Version", "2.1.0"},
            {"vnp_Command", "pay"},
            {"vnp_TmnCode", vnp_TmnCode},
            {"vnp_Amount", vnp_Amount},
            {"vnp_BankCode", "NCB"},
            {"vnp_CreateDate", vnp_CreateDate},
            {"vnp_CurrCode", "VND"},
            {"vnp_IpAddr", vnp_IpAddr},
            {"vnp_Locale", "vn"},
            {"vnp_OrderInfo", vnp_OrderInfo},
            {"vnp_ReturnUrl", vnp_ReturnUrl},
            {"vnp_TxnRef", vnp_TxnRef}
        };

            if (!string.IsNullOrEmpty(vnp_OrderType))
                vnp_Params.Add("vnp_OrderType", vnp_OrderType);

            if (!string.IsNullOrEmpty(vnp_ExpireDate))
                vnp_Params.Add("vnp_ExpireDate", vnp_ExpireDate);

            // Sắp xếp tham số theo thứ tự alphabet và tạo chuỗi dữ liệu để mã hóa
            string rawData = string.Join("&", vnp_Params.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}={kvp.Value}"));
            System.Diagnostics.Debug.WriteLine("Data string for hash: " + rawData);
            string vnp_SecureHash = HmacSHA512(vnp_HashSecret, rawData);
            System.Diagnostics.Debug.WriteLine("Secure hash: " + vnp_SecureHash);
            string queryString = string.Join("&", vnp_Params.OrderBy(kvp => kvp.Key)
            .Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            string paymentUrl = $"{vnp_Url}?{queryString}&vnp_SecureHash={vnp_SecureHash}";
            System.Diagnostics.Debug.WriteLine("Payment URL: " + paymentUrl);
            return paymentUrl;
        }

        private string GetClientIpAddress()
        {
            if (Request.IsLocal)
            {
                return "192.168.1.100";
            }

            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return Request.ServerVariables["REMOTE_ADDR"];
        }

        private string HmacSHA512(string key, string input)
        {
            var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private bool KiemTraThanhToanVNPay(string vnp_TxnRef, string vnp_SecureHash)
        {
            var vnpData = new SortedDictionary<string, string>();
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_") && key != "vnp_SecureHash" && key != "vnp_SecureHashType")
                {
                    vnpData.Add(key, Request.QueryString[key]);
                }
            }

            string rawData = string.Join("&", vnpData.OrderBy(x => x.Key)
                .Select(kvp => $"{kvp.Key}={kvp.Value}")); // ❗ KHÔNG ENCODE giá trị

            string calculatedHash = HmacSHA512(vnp_HashSecret, rawData);

            return vnp_SecureHash == calculatedHash;
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
            }
            else
            {
                throw new Exception("Đăng ký tư vấn không thành công!");
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

        private void GửiEmailThamSoBenhNhan(string idTuVan, string idBenhNhan, string linkGoogleMeet, DateTime ngay, TimeSpan gio)
        {
            try
            {
                string emailBenhNhan = LayEmailBenhNhan(idBenhNhan);
                if (string.IsNullOrEmpty(emailBenhNhan)) throw new Exception("Không tìm thấy email của bệnh nhân.");

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
                    client.Authenticate("bananahospitaldanang@gmail.com", "bvezpjcixxoypqvi");
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