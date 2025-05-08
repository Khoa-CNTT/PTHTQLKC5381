using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.DoctorOnline
{
    public partial class Xem_Thong_Tin_Tu_Van : System.Web.UI.Page
    {
        LopKetNoi ketNoi = new LopKetNoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/Dang_Nhap.aspx");
                }

            }
        }
        private void LoadData()
        {
            string sql;
            SqlParameter[] parameters = null;

            if (Session["Role"].ToString() == "BacSiOn")
            {
                sql = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, lt.Ngay, lt.Gio, 
               lt.TrieuChung, lt.LinkJitsi AS Link
               FROM LichTuVan lt
               INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
               WHERE lt.IDBacSi = @IDBacSi
               ORDER BY lt.Ngay DESC, lt.Gio DESC";

                parameters = new SqlParameter[] {
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                sql = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, lt.Ngay, lt.Gio, 
               lt.TrieuChung, lt.LinkJitsi AS Link
               FROM LichTuVan lt
               INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
               ORDER BY lt.Ngay DESC, lt.Gio DESC";
            }

            DataTable dt = ketNoi.docdulieu(sql, parameters);

            LoadNgayKham(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "swal('Lỗi','Không tìm thấy dữ liệu!','error');", true);
            }
        }
        private void LoadNgayKham(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            var ngayKhamList = dt.AsEnumerable()
                .Select(row => row.Field<DateTime>("Ngay").Date)
                .Distinct()
                .OrderBy(date => date)
                .ToList();

            ddlNgayKham.Items.Clear();
            ddlNgayKham.Items.Add(new ListItem("Tất cả ngày", ""));

            foreach (var ngay in ngayKhamList)
            {
                ddlNgayKham.Items.Add(new ListItem(ngay.ToString("dd/MM/yyyy"), ngay.ToString("yyyy-MM-dd")));
            }
        }
       

       

        protected void ddlNgayKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedNgay = ddlNgayKham.SelectedValue;
            if (!string.IsNullOrEmpty(selectedNgay))
            {
                FilterByNgayKham(selectedNgay);
            }
            else
            {
                LoadData();
            }
        }

        private void FilterByNgayKham(string ngayKham)
        {
            string query;
            SqlParameter[] parameters;

            DateTime ngay = DateTime.ParseExact(ngayKham, "yyyy-MM-dd", null);

            if (Session["Role"].ToString() == "BacSiOn")
            {
                query = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, lt.Ngay, lt.Gio, 
                 lt.TrieuChung, lt.LinkJitsi AS Link
                 FROM LichTuVan lt
                 INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
                 WHERE CONVERT(date, lt.Ngay) = CONVERT(date, @Ngay)
                 AND lt.IDBacSi = @IDBacSi
                 ORDER BY lt.Gio DESC";

                parameters = new SqlParameter[] {
            new SqlParameter("@Ngay", ngay),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, lt.Ngay, lt.Gio, 
                 lt.TrieuChung, lt.LinkJitsi AS Link
                 FROM LichTuVan lt
                 INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
                 WHERE CONVERT(date, lt.Ngay) = CONVERT(date, @Ngay)
                 ORDER BY lt.Gio DESC";

                parameters = new SqlParameter[] {
            new SqlParameter("@Ngay", ngay)
        };
            }

            DataTable dt = ketNoi.docdulieu(query, parameters);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void gridDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string query_list;
            SqlParameter[] pr;

            if (Session["Role"].ToString() == "BacSiOn")
            {
                query_list = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, lt.Ngay, lt.Gio, 
                      lt.TrieuChung, lt.LinkJitsi AS Link, lt.TrangThai
                      FROM LichTuVan lt
                      INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
                      WHERE lt.IDBacSi = @IDBacSi
                      ORDER BY lt.Ngay DESC, lt.Gio DESC";

                pr = new SqlParameter[] {
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query_list = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, lt.Ngay, lt.Gio, 
                      lt.TrieuChung, lt.LinkJitsi AS Link, lt.TrangThai
                      FROM LichTuVan lt
                      INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
                      ORDER BY lt.Ngay DESC, lt.Gio DESC";

                pr = new SqlParameter[] { };
            }

            DataTable dataTable = ketNoi.docdulieu(query_list, pr);
            GridView1.DataSource = dataTable;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }



        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string searchText = txt_Searching.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadData();
                return;
            }

            string sql_search;
            SqlParameter[] pr;
            string searchTerm = "%" + searchText + "%"; // Thêm wildcard để tìm kiếm phần tử

            if (Session["Role"].ToString() == "BacSiOn")
            {
                sql_search = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, 
                      lt.Ngay, lt.Gio, lt.TrieuChung, lt.LinkJitsi AS Link
                      FROM LichTuVan lt
                      INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
                      WHERE (lt.IDTuVan LIKE @SearchTerm OR bn.HoTen LIKE @SearchTerm)
                      AND lt.IDBacSi = @IDBacSi
                      ORDER BY lt.Ngay DESC, lt.Gio DESC";

                pr = new SqlParameter[] {
            new SqlParameter("@SearchTerm", searchTerm),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                sql_search = @"SELECT lt.IDTuVan, bn.IDBenhNhan, bn.HoTen AS HoTenBenhNhan, 
                      lt.Ngay, lt.Gio, lt.TrieuChung, lt.LinkJitsi AS Link
                      FROM LichTuVan lt
                      INNER JOIN BenhNhan bn ON lt.IDBenhNhan = bn.IDBenhNhan
                      WHERE lt.IDTuVan LIKE @SearchTerm OR bn.HoTen LIKE @SearchTerm
                      ORDER BY lt.Ngay DESC, lt.Gio DESC";

                pr = new SqlParameter[] {
            new SqlParameter("@SearchTerm", searchTerm)
        };
            }

            DataTable dt = ketNoi.docdulieu(sql_search, pr);

            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                // Giữ nguyên dữ liệu gốc nếu không tìm thấy
                LoadData();
                ClientScript.RegisterStartupScript(this.GetType(), "SearchError",
                    "swal('Thông báo', 'Không tìm thấy kết quả phù hợp!', 'info');", true);
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

            ddlNgayKham.SelectedIndex = 0;
            txt_Searching.Text = "";
            LoadData();
        }
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idBenhNhan = btn.CommandArgument;

            string sql = @"SELECT * FROM BenhNhan WHERE IDBenhNhan = @IDBenhNhan";
            SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@IDBenhNhan", idBenhNhan)
    };

            DataTable dt = ketNoi.docdulieu(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string details = $@"
            <p><strong>Mã bệnh nhân:</strong> {row["IDBenhNhan"]}</p>
            <p><strong>Họ tên:</strong> {row["HoTen"]}</p>
            <p><strong>Ngày sinh:</strong> {Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy")}</p>
            <p><strong>Giới tính:</strong> {row["GioiTinh"]}</p>
            <p><strong>Số điện thoại:</strong> {row["SoDienThoai"]}</p>
            <p><strong>Email:</strong> {row["Email"]}</p>
            <p><strong>Địa chỉ:</strong> {row["DiaChi"]}</p>
            
        ";

                patientDetails.InnerHtml = details;

                // Hiển thị modal
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModal",
                    "document.getElementById('detailModal').style.display='block';", true);
            }
        }
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0 && GridView1.DataSource == null)
            {
                // Tạo DataTable với cấu trúc tương tự GridView
                DataTable dt = new DataTable();
                dt.Columns.Add("IDTuVan");
                dt.Columns.Add("IDBenhNhan");
                dt.Columns.Add("HoTenBenhNhan");
                dt.Columns.Add("Ngay");
                dt.Columns.Add("Gio");
                dt.Columns.Add("TrieuChung");
                dt.Columns.Add("Link");

                // Thêm 5 dòng placeholder
                for (int i = 0; i < 7; i++)
                {
                    dt.Rows.Add(" ", " ", " ", " ", " ", " ");
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

                // Thêm class placeholder cho các cell
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.CssClass = "placeholder-row";
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.CssClass = "placeholder-cell";
                    }

                    // Ẩn nút Xem chi tiết ở cột cuối cùng
                    if (row.Cells.Count > 0)
                    {
                        row.Cells[row.Cells.Count - 1].Text = ""; // Xóa nội dung ô
                        row.Cells[row.Cells.Count - 1].CssClass = "placeholder-cell hidden-button";
                    }
                }
            }
        }
    }
}