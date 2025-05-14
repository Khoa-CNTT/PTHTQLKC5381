using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Cmp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.test
{
    public partial class Test1 : System.Web.UI.Page
    {
        private const string OPENAI_API_KEY = "eZvRjLjDmhCeclXwQWlcG0w0W6px5f3KDXl9UfFQ";
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        private static readonly HttpClient httpClient = new HttpClient();
        protected async void btnGoiY_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            txtDonThuoc.Text = "Đang phân tích...";

            string chanDoan = txtChanDoan.Text.Trim();
            if (string.IsNullOrEmpty(chanDoan))
            {
                lblStatus.Text = "Vui lòng nhập chẩn đoán.";
                txtDonThuoc.Text = "";
                return;
            }

            try
            {
                string donThuoc = await GoiYDonThuocTuCohere(chanDoan);
                txtDonThuoc.Text = donThuoc;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi: " + ex.Message;
                txtDonThuoc.Text = "";
            }
        }


        private async Task<string> GoiYDonThuocTuCohere(string chanDoan)
        {
            string apiKey = "eZvRjLjDmhCeclXwQWlcG0w0W6px5f3KDXl9UfFQ"; // Thay thế với API Key của bạn
            string modelUrl = "https://api.cohere.ai/generate";

            var request = new HttpRequestMessage(HttpMethod.Post, modelUrl);
            request.Headers.Add("Authorization", $"Bearer {apiKey}");

            var body = new
            {
                prompt = $"Chẩn đoán bệnh: {chanDoan}. Hãy chỉ gợi ý tên các loại thuốc mà cho người bệnh sử dụng, không cần giải thích thêm, ngắn gọn.",
                max_tokens = 100,  // Giới hạn số token
                temperature = 0.7,
                stop = new[] { "\n" } // Đảm bảo chỉ trả về tên thuốc
            };

            string jsonBody = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Lỗi từ Cohere API: {response.StatusCode}");

            var result = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(result);
            string resultText = json["text"]?.ToString()?.Trim() ?? "Không có đề xuất.";

            // Lọc ra chỉ tên thuốc
            return resultText;
        }

        //private string FilterDrugNames(string input)
        //{
        //    // Sử dụng biểu thức chính quy (Regex) để tìm các tên thuốc (những từ viết hoa và có thể chứa dấu gạch nối)
        //    var matches = Regex.Matches(input, @"\b[A-Z][a-zA-Z\-]*\b");

        //    // Tạo một danh sách tên thuốc
        //    var drugNames = matches.Cast<Match>().Select(m => m.Value).ToList();

        //    // Nếu không tìm thấy tên thuốc nào, trả về "Không có đề xuất."
        //    if (drugNames.Count == 0)
        //        return "Không có đề xuất.";

        //    // Trả về tên thuốc dưới dạng chuỗi, ngăn cách bởi dấu phẩy
        //    return string.Join(", ", drugNames);
        //}
    }
}