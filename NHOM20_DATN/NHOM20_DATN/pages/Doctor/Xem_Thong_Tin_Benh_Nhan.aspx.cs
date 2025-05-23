using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace NHOM20_DATN
{
    public partial class Xem_Thong_Tin_Benh_Nhan : System.Web.UI.Page
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

            if (Session["Role"].ToString() == "BacSi")
            {
                sql = @"SELECT p.IDBenhNhan, p.HoTen, p.NgayKham, p.ThoiGianKham, 
               p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
               FROM PhieuKham p 
               LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
               WHERE p.IDBacSi = @IDBacSi
               ORDER BY p.NgayKham DESC";


                parameters = new SqlParameter[] {
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                sql = @"SELECT p.IDBenhNhan, p.HoTen, p.NgayKham, p.ThoiGianKham, 
                   p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
                   FROM PhieuKham p 
                   LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
                   ORDER BY p.NgayKham DESC";
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
                .Select(row => row.Field<DateTime>("NgayKham").Date)
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
        protected void ddlTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTrangThai = ddlTrangThai.SelectedValue;
            if (!string.IsNullOrEmpty(selectedTrangThai))
            {
                FilterByTrangThai(selectedTrangThai);
            }
            else
            {
                LoadData();
            }
        }

        private void FilterByTrangThai(string trangThai)
        {
            string query;
            SqlParameter[] parameters;

            if (Session["Role"].ToString() == "BacSi")
            {
                query = @"SELECT p.IDPhieu, p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
             p.SoDienThoai, p.Email, p.NgayKham, p.ThoiGianKham, 
             p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
             FROM PhieuKham p 
             LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
             WHERE (lk.TrangThai = @TrangThai OR (@TrangThai = 'DaDangKy' AND lk.TrangThai IS NULL))
             AND p.IDBacSi = @IDBacSi
             ORDER BY p.NgayKham DESC";

                parameters = new SqlParameter[] {
            new SqlParameter("@TrangThai", trangThai),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query = @"SELECT p.IDPhieu, p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
             p.SoDienThoai, p.Email, p.NgayKham, p.ThoiGianKham, 
             p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
             FROM PhieuKham p 
             LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
             WHERE (lk.TrangThai = @TrangThai OR (@TrangThai = 'DaDangKy' AND lk.TrangThai IS NULL))
             ORDER BY p.NgayKham DESC";

                parameters = new SqlParameter[] {
            new SqlParameter("@TrangThai", trangThai)
        };
            }

            DataTable dt = ketNoi.docdulieu(query, parameters);
            GridView1.DataSource = dt;
            GridView1.DataBind();
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

            if (Session["Role"].ToString() == "BacSi")
            {
                query = @"SELECT p.IDPhieu, p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
                 p.SoDienThoai, p.Email, p.NgayKham, p.ThoiGianKham, 
                 p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
                 FROM PhieuKham p 
                 LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
                 WHERE CONVERT(date, p.NgayKham) = CONVERT(date, @NgayKham)
                 AND p.IDBacSi = @IDBacSi
                 ORDER BY p.ThoiGianKham";

                parameters = new SqlParameter[] {
            new SqlParameter("@NgayKham", ngay),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query = @"SELECT p.IDPhieu, p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
                 p.SoDienThoai, p.Email, p.NgayKham, p.ThoiGianKham, 
                 p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
                 FROM PhieuKham p 
                 LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
                 WHERE CONVERT(date, p.NgayKham) = CONVERT(date, @NgayKham)
                 ORDER BY p.ThoiGianKham";

                parameters = new SqlParameter[] {
            new SqlParameter("@NgayKham", ngay)
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

            if (Session["Role"].ToString() == "BacSi")
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                  p.IDPhongKham, ck.TenChuyenKhoa as ChuyenKhoa, ck.IDChuyenKhoa,
                  p.NgayKham, p.ThoiGianKham, p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
                  FROM PhieuKham p 
                  INNER JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa
                  LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
                  WHERE p.IDBacSi = @IDBacSi";

                pr = new SqlParameter[] {
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                  p.IDPhongKham, ck.TenChuyenKhoa as ChuyenKhoa, ck.IDChuyenKhoa,
                  p.NgayKham, p.ThoiGianKham, p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
                  FROM PhieuKham p 
                  INNER JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa
                  LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu";
                pr = new SqlParameter[] { };
            }

            DataTable dataTable = ketNoi.docdulieu(query_list, pr);
            GridView1.DataSource = dataTable;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }


    
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string searchTerm = "%" + txt_Searching.Text.Trim() + "%";
            string sql_search;
            SqlParameter[] pr;

            if (Session["Role"].ToString() == "BacSi")
            {
                sql_search = @"SELECT p.IDPhieu, p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
                     p.SoDienThoai, p.Email, p.NgayKham, p.ThoiGianKham, 
                     p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
                     FROM PhieuKham p 
                     LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
                     WHERE (p.IDBenhNhan LIKE @id OR p.HoTen LIKE @term
                        OR CONVERT(varchar, p.NgayKham, 103) LIKE @term
                        OR CONVERT(varchar, p.ThoiGianKham, 108) LIKE @term
                        OR p.TrieuChung LIKE @term
                        OR ISNULL(lk.TrangThai, 'DaDangKy') LIKE @term)
                     AND p.IDBacSi = @IDBacSi";

                pr = new SqlParameter[] {
            new SqlParameter("@id", "%" + txt_Searching.Text.Trim() + "%"),
            new SqlParameter("@term", searchTerm),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                sql_search = @"SELECT p.IDPhieu, p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
                      p.SoDienThoai, p.Email, p.NgayKham, p.ThoiGianKham, 
                      p.TrieuChung, ISNULL(lk.TrangThai, 'DaDangKy') AS TrangThai
                      FROM PhieuKham p 
                      LEFT JOIN LichKhamBenhNhan lk ON p.IDPhieu = lk.IDPhieu
                      WHERE p.IDBenhNhan LIKE @id OR p.HoTen LIKE @term
                      OR CONVERT(varchar, p.NgayKham, 103) LIKE @term
                        OR CONVERT(varchar, p.ThoiGianKham, 108) LIKE @term
                        OR p.TrieuChung LIKE @term
                        OR ISNULL(lk.TrangThai, 'DaDangKy') LIKE @term";

                pr = new SqlParameter[] {
            new SqlParameter("@id", "%" + txt_Searching.Text.Trim() + "%"),
            new SqlParameter("@term", searchTerm)
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
                GridView1.DataSource = null;
                GridView1.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "SearchError",
                    "swal('Thông báo', 'Không tìm thấy kết quả phù hợp!', 'info');", true);
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

            ddlTrangThai.SelectedIndex = 0;
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
        protected void GridView1_PreRenderr(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0 && GridView1.DataSource == null)
            {
                // Tạo DataTable với cấu trúc tương tự GridView thực tế
                DataTable dt = new DataTable();
                dt.Columns.Add("IDBenhNhan");
                dt.Columns.Add("HoTen");
                dt.Columns.Add("NgayKham");
                dt.Columns.Add("ThoiGianKham");
                dt.Columns.Add("TrieuChung");
                dt.Columns.Add("TrangThai");

                // Thêm 7 dòng placeholder (tương ứng với PageSize)
                for (int i = 0; i < 7; i++)
                {
                    dt.Rows.Add("", "", "", "", "", "");
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

                // Thêm class placeholder cho các cell và ẩn nút Xem chi tiết
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