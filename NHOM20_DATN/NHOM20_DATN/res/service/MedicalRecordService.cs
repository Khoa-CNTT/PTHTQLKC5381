using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace NHOM20_DATN.res.service
{
    public class MedicalRecordService
    {
        LopKetNoi db = new LopKetNoi();
        public DataTable getAll(string idbs)
        {
            string query = "select hs.IDHS,hs.IDBN, hs.IDLSK,lsk.IDPhieu, bn.HoTen, pk.NgayKham, hs.ChanDoan, hs.DonThuoc, hs.GhiChu, hs.NgayCapNhat, lsk.HuongDieuTri  from HoSoBenhAn hs, BenhNhan bn, BacSi bs, LichSuKham lsk, PhieuKham pk" +
                " where hs.IDBN = bn.IDBenhNhan and bs.IDBacSi = hs.IDBS and hs.IDLSK = lsk.IDLichSu and pk.IDPhieu = lsk.IDPhieu" +
                " and hs.IDBS = @idbs";
            SqlParameter[] param = new SqlParameter[] {
      new SqlParameter("@idbs",idbs)
      };
            DataTable dt = new DataTable();
            dt = db.docdulieu(query, param);
            return dt;
        }
        public DataTable getByPatientName(string name, string idbs)
        {
            name = "%" + name + "%";
            string query = "select hs.IDHS,hs.IDBN, hs.IDLSK, bn.HoTen, pk.NgayKham, hs.ChanDoan, hs.DonThuoc, hs.GhiChu, hs.NgayCapNhat, lsk.HuongDieuTri from HoSoBenhAn hs, BenhNhan bn, BacSi bs, LichSuKham lsk, PhieuKham pk" +
                "  where hs.IDBN = bn.IDBenhNhan and bs.IDBacSi = hs.IDBS and hs.IDLSK = lsk.IDLichSu and pk.IDPhieu = lsk.IDPhieu" +
                "   and (bn.HoTen COLLATE SQL_Latin1_General_CP1_CI_AI like @nameKey )" +
                "   and hs.IDBS = @idbs";
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@nameKey", name),
            new SqlParameter("@idbs", idbs)};
            DataTable dt = new DataTable();
            dt = db.docdulieu(query, param);
            return dt;
        }

        // ========        UPDATE
        public int update(string idbs, string idbn, string idhs,string idpk, string chandoan, string donthuoc, string ngaycn, string ghichu)
        {
            string query = "update HoSoBenhAn " +
                " set ChanDoan = @chandoan, DonThuoc = @donthuoc, NgayCapNhat = @ngaycn, GhiChu = @ghichu " +
                " where IDHS = @idhs and IDBS = @idbs and IDBN = @idbn";
            string updateTrangThai = " update LichKhamBenhNhan " +
                " set TrangThai = 'DaKham' " +
                " where IDBenhNhan=@IDBN and IDPhieu=@IDPK";

            SqlParameter[] prUpdateTrangThai = new SqlParameter[] {
                new SqlParameter("@IDBN", idbn),
                new SqlParameter("@IDPK", idpk)

            };
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@idbs", idbs),
                new SqlParameter("@idbn", idbn),
                new SqlParameter("@idhs", idhs),
                new SqlParameter("@chandoan", chandoan),
                new SqlParameter("@donthuoc", donthuoc),
                new SqlParameter("@ngaycn", ngaycn),
                new SqlParameter("@ghichu", ghichu)
            };
            int result = db.CapNhat(query, pr);
            int resultTrangThai = db.CapNhat(updateTrangThai, prUpdateTrangThai);
            if(!(result != 0)  ) return 0 ;
            return result;
        }

        private static readonly HttpClient httpClient = new HttpClient();
        public async Task<string> GoiYDonThuocTuCohere(string chanDoan)
        {
            string apiKey = "eZvRjLjDmhCeclXwQWlcG0w0W6px5f3KDXl9UfFQ"; 
            string modelUrl = "https://api.cohere.ai/generate";

            var request = new HttpRequestMessage(HttpMethod.Post, modelUrl);
            request.Headers.Add("Authorization", $"Bearer {apiKey}");

            var body = new
            {
                prompt = $"Given the diagnosis, return only the names of appropriate medications, separated by commas. Do not include any explanation or extra text. If the diagnosis is invalid or not a real symptom, return exactly: Invalid diagnosis. Diagnosis: {chanDoan}'.",
                max_tokens = 100,
                temperature = 0.7,
                stop = new[] { "\n" }
            };

            string jsonBody = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Lỗi từ Cohere API: {response.StatusCode}");

            var result = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(result);
            string resultText = json["text"]?.ToString()?.Trim() ?? "";

            
            if (string.IsNullOrWhiteSpace(resultText) ||
                resultText.ToLower().Contains("invalid") ||
                !resultText.Any(char.IsLetter)) // kết quả không có ký tự chữ cái nào
            {
                return "Không có đề xuất hợp lệ.";
            }

            return resultText;
        }
    }
}