using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Web.Services;



namespace NHOM20_DATN.pages.Manager
{
    public partial class Quan_Ly_Thanh_Toan : System.Web.UI.Page
    {
        LopKetNoi lopKetNoi = new LopKetNoi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachThanhToan();
            }
        }

        private void LoadDanhSachThanhToan()
        {
            string sql = "SELECT * FROM ThanhToan"; // thay bằng tên bảng thực tế của bạn
            DataTable dt = lopKetNoi.docdulieu(sql, null);
            GridViewThanhToan.DataSource = dt;
            GridViewThanhToan.DataBind();

            TinhTongDoanhThu(dt);
        }

        private void LoadDanhSachThanhToan(DateTime tuNgay, DateTime denNgay)
        {
            string sql = "SELECT * FROM ThanhToan WHERE NgayThanhToan BETWEEN @TuNgay AND @DenNgay";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };
            DataTable dt = lopKetNoi.docdulieu(sql, parameters);
            GridViewThanhToan.DataSource = dt;
            GridViewThanhToan.DataBind();

            TinhTongDoanhThu(dt);
        }

        private void TinhTongDoanhThu(DataTable dt)
        {
            decimal tong = 0;
            foreach (DataRow row in dt.Rows)
            {
                decimal tien = 0;
                decimal.TryParse(row["Amount"].ToString(), out tien); // sửa thành "Amount" cho đúng với database của bạn
                tong += tien;
            }
            lblTongDoanhThu.Text = $"Tổng doanh thu: {tong:N0} VNĐ";
        }

        protected void btnLocNgay_Click(object sender, EventArgs e)
        {
            // Lấy ngày từ TextBox
            DateTime tuNgay, denNgay;

            if (DateTime.TryParse(txtTuNgay.Text, out tuNgay) && DateTime.TryParse(txtDenNgay.Text, out denNgay))
            {
                // Gọi hàm lấy dữ liệu đã lọc
                LoadDanhSachThanhToan(tuNgay, denNgay);
            }
            else
            {
                // Nếu lỗi định dạng ngày
                lblTongDoanhThu.Text = "Vui lòng nhập đúng định dạng ngày!";
            }
        }

        protected void btnHomNay_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            LoadDanhSachThanhToan(today, today);
        }

        protected void btnTuanNay_Click(object sender, EventArgs e)
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime endOfWeek = startOfWeek.AddDays(6);
            LoadDanhSachThanhToan(startOfWeek, endOfWeek);
        }

        protected void btnThangNay_Click(object sender, EventArgs e)
        {
            DateTime startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            LoadDanhSachThanhToan(startOfMonth, endOfMonth);
        }

        protected void btnTatCa_Click(object sender, EventArgs e)
        {
            LoadDanhSachThanhToan();
        }

        [WebMethod]
        public static object GetChartData()
        {
            // Kết nối với cơ sở dữ liệu và lấy dữ liệu doanh thu
            LopKetNoi lopKetNoi = new LopKetNoi();
            string sql = "SELECT NgayThanhToan, SUM(Amount) AS TotalAmount FROM ThanhToan GROUP BY NgayThanhToan";
            DataTable dt = lopKetNoi.docdulieu(sql, null);

            var labels = new System.Collections.Generic.List<string>();
            var data = new System.Collections.Generic.List<decimal>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add(Convert.ToDateTime(row["NgayThanhToan"]).ToString("dd/MM/yyyy"));
                data.Add(Convert.ToDecimal(row["TotalAmount"]));
            }

            return new { labels = labels, data = data };
        }

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DanhSachThanhToan.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // Tắt phân trang nếu GridView có
            GridViewThanhToan.AllowPaging = false;
            LoadDanhSachThanhToan(); // Load lại toàn bộ dữ liệu

            GridViewThanhToan.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Để xuất Excel không bị lỗi
        }
    }
}