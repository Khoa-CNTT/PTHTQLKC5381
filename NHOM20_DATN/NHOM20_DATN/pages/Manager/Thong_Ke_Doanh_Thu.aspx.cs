using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Thong_Ke_Doanh_Thu : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();

        // Chuỗi JSON trả về client
        public string LabelsJson = "[]";
        public string DataJson = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Tạo danh sách năm từ 2020 đến năm hiện tại
                int currentYear = DateTime.Now.Year;
                for (int year = currentYear; year >= 2020; year--)
                {
                    ddlNam.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }

                ddlNam.SelectedValue = currentYear.ToString();
                ddlLoaiThongKe.SelectedValue = "month"; // mặc định
            }

            LoadDoanhThu(ddlLoaiThongKe.SelectedValue);
        }

        protected void ddlLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDoanhThu(ddlLoaiThongKe.SelectedValue);
        }

        private void LoadDoanhThu(string kieuThongKe)
        {
            int nam = int.Parse(ddlNam.SelectedValue);
            string sql = "";
            SqlParameter[] parameters = { new SqlParameter("@Nam", nam) };

            if (kieuThongKe == "week")
            {
                sql = @"
                SELECT DATEPART(WEEK, NgayThanhToan) AS Nhom, SUM(Amount) AS TongTien
                FROM ThanhToan
                WHERE YEAR(NgayThanhToan) = @Nam
                GROUP BY DATEPART(WEEK, NgayThanhToan)
                ORDER BY Nhom";
            }
            else if (kieuThongKe == "month")
            {
                sql = @"
                SELECT MONTH(NgayThanhToan) AS Nhom, SUM(Amount) AS TongTien
                FROM ThanhToan
                WHERE YEAR(NgayThanhToan) = @Nam
                GROUP BY MONTH(NgayThanhToan)
                ORDER BY Nhom";
            }
            else if (kieuThongKe == "year")
            {
                sql = @"
                SELECT YEAR(NgayThanhToan) AS Nhom, SUM(Amount) AS TongTien
                FROM ThanhToan
                GROUP BY YEAR(NgayThanhToan)
                ORDER BY Nhom";
                parameters = null; // Không cần tham số năm khi thống kê theo năm
            }

            DataTable dt = db.docdulieu(sql, parameters);

            List<string> labels = new List<string>();
            List<decimal> data = new List<decimal>();

            // Mảng chứa doanh thu cho mỗi tháng từ 1 đến 12
            decimal[] monthlyData = new decimal[12];
            // Mảng chứa doanh thu cho từng tuần từ 1 đến 53
            decimal[] weeklyData = new decimal[53];
            // Mảng chứa doanh thu cho từng năm từ 2020 đến năm hiện tại
            int currentYear = DateTime.Now.Year;
            decimal[] yearlyData = new decimal[currentYear - 2020 + 1];

            foreach (DataRow row in dt.Rows)
            {
                int nhom = Convert.ToInt32(row["Nhom"]);
                decimal totalAmount = Convert.ToDecimal(row["TongTien"]);

                if (kieuThongKe == "week")
                {
                    // Gán doanh thu vào mảng cho tuần tương ứng
                    weeklyData[nhom] = totalAmount;
                    labels.Add("Tuần " + nhom);
                    data.Add(totalAmount);
                }
                else if (kieuThongKe == "month")
                {
                    // Gán doanh thu vào mảng cho tháng tương ứng
                    monthlyData[nhom - 1] = totalAmount;
                    labels.Add("Tháng " + nhom);
                    data.Add(totalAmount);
                }
                else if (kieuThongKe == "year")
                {
                    // Gán doanh thu vào mảng cho năm tương ứng
                    yearlyData[nhom - 2020] = totalAmount;
                    labels.Add("Năm " + nhom);
                    data.Add(totalAmount);
                }
            }

            // Cập nhật dữ liệu theo từng loại thống kê

            if (kieuThongKe == "week")
            {
                DataJson = JsonConvert.SerializeObject(weeklyData.ToList());
                LabelsJson = JsonConvert.SerializeObject(Enumerable.Range(1, 52).Select(i => "Tuần " + i).ToList());
            }
            else if (kieuThongKe == "month")
            {
                // Thêm các tháng không có doanh thu
                for (int i = 0; i < 12; i++)
                {
                    if (monthlyData[i] == 0)
                    {
                        monthlyData[i] = 0;  // Nếu không có dữ liệu, gán giá trị 0
                    }
                }
                DataJson = JsonConvert.SerializeObject(monthlyData.ToList());
                LabelsJson = JsonConvert.SerializeObject(new List<string> {
                    "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                    "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"
                });
            }
            else if (kieuThongKe == "year")
            {
                // Thêm các năm không có doanh thu
                for (int i = 0; i < yearlyData.Length; i++)
                {
                    if (yearlyData[i] == 0)
                    {
                        yearlyData[i] = 0;  // Nếu không có dữ liệu, gán giá trị 0
                    }
                }
                DataJson = JsonConvert.SerializeObject(yearlyData.ToList());
                LabelsJson = JsonConvert.SerializeObject(Enumerable.Range(2020, currentYear - 2020 + 1).Select(y => "Năm " + y).ToList());
            }
        }
    }
}
