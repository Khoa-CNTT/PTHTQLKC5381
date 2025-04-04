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
            if (Session["UserID"] == null || Session["Role"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadChuyenKhoa();
                LoadPhongKham();
                LoadData();
            }
        }
        private void LoadData()
        {
            string sql;
            SqlParameter[] parameters = null;

            if (Session["Role"].ToString() == "BacSi")
            {
                sql = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
               p.IDPhongKham, ck.TenChuyenKhoa as ChuyenKhoa, ck.IDChuyenKhoa 
               FROM PhieuKham p 
               INNER JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa
               WHERE p.IDBacSi = @IDBacSi";

                parameters = new SqlParameter[] {
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                sql = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
               p.IDPhongKham, ck.TenChuyenKhoa as ChuyenKhoa, ck.IDChuyenKhoa 
               FROM PhieuKham p 
               INNER JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa";
            }

            DataTable dt = ketNoi.docdulieu(sql, parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Response.Write("Không có dữ liệu.");
            }
        }
        private void LoadChuyenKhoa()
        {
            string sql_specialist = "SELECT * FROM ChuyenKhoa";
            DataTable chuyenkhoa_DT = ketNoi.docdulieu(sql_specialist, null);

            ddlChuyenKhoa.DataSource = chuyenkhoa_DT;
            ddlChuyenKhoa.DataTextField = "TenChuyenKhoa";
            ddlChuyenKhoa.DataValueField = "IDChuyenKhoa";
            ddlChuyenKhoa.DataBind();
            ddlChuyenKhoa.Items.Insert(0, new ListItem("Chọn chuyên khoa"));

        }
        private void LoadPhongKham()
        {
            string sql_phongKham;
            SqlParameter[] parameters = null;

            if (Session["Role"].ToString() == "BacSi")
            {
                string sqlBacSi = "SELECT ChuyenKhoaID FROM BacSi WHERE IDBacSi = @IDBacSi";
                SqlParameter[] prBacSi = { new SqlParameter("@IDBacSi", Session["UserID"].ToString()) };
                DataTable dtBacSi = ketNoi.docdulieu(sqlBacSi, prBacSi);

                if (dtBacSi != null && dtBacSi.Rows.Count > 0)
                {
                    int chuyenKhoaID = Convert.ToInt32(dtBacSi.Rows[0]["ChuyenKhoaID"]);
                    sql_phongKham = "SELECT * FROM PhongKham WHERE IDChuyenKhoa = @ChuyenKhoaID";
                    parameters = new SqlParameter[] {
                new SqlParameter("@ChuyenKhoaID", chuyenKhoaID)
            };
                }
                else
                {
                    sql_phongKham = "SELECT * FROM PhongKham";
                }
            }
            else
            {
                sql_phongKham = "SELECT * FROM PhongKham";
            }

            DataTable phongKham_DT = ketNoi.docdulieu(sql_phongKham, parameters);

            ddlPhongKham.DataSource = phongKham_DT;
            ddlPhongKham.DataTextField = "TenPhongKham";
            ddlPhongKham.DataValueField = "IDPhongKham";
            ddlPhongKham.DataBind();
            ddlPhongKham.Items.Insert(0, new ListItem("Chọn phòng khám", ""));
        }
        protected void ddlChuyenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedChuyenKhoa = ddlChuyenKhoa.SelectedItem.Text;
            if (selectedChuyenKhoa != "Chọn chuyên khoa")
            {
                viewList_Filter1(selectedChuyenKhoa);
            }
        }
        protected void ddlPhongKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPhongKham = ddlPhongKham.SelectedValue;
            if (!string.IsNullOrEmpty(selectedPhongKham))
            {
                viewList_Filter2(selectedPhongKham);
            }
        }

        protected void gridDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string query_list;
            SqlParameter[] pr;

            if (Session["Role"].ToString() == "BacSi")
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                      p.IDPhongKham, ck.TenChuyenKhoa as ChuyenKhoa, ck.IDChuyenKhoa 
                      FROM PhieuKham p 
                      INNER JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa
                      WHERE p.IDBacSi = @IDBacSi";

                pr = new SqlParameter[] {
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                      p.IDPhongKham, ck.TenChuyenKhoa as ChuyenKhoa, ck.IDChuyenKhoa 
                      FROM PhieuKham p 
                      INNER JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa";
                pr = new SqlParameter[] { };
            }

            DataTable dataTable = ketNoi.docdulieu(query_list, pr);
            GridView1.DataSource = dataTable;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }


        public void viewList_Filter2(string selectedPhongKham)
        {
            string query_list;
            SqlParameter[] sp;

            if (Session["Role"].ToString() == "BacSi")
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
                     p.SoDienThoai, p.Email, p.IDPhongKham, 
                     pk.TenPhongKham, ck.TenChuyenKhoa as ChuyenKhoa
                     FROM PhieuKham p 
                     INNER JOIN PhongKham pk ON pk.IDPhongKham = p.IDPhongKham
                     INNER JOIN ChuyenKhoa ck ON ck.IDChuyenKhoa = p.IDChuyenKhoa
                     WHERE p.IDPhongKham = @IDPhongKham 
                     AND p.IDBacSi = @IDBacSi
                     ORDER BY p.NgayKham, p.ThoiGianKham";

                sp = new SqlParameter[] {
            new SqlParameter("@IDPhongKham", selectedPhongKham),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, 
                     p.SoDienThoai, p.Email, p.IDPhongKham, 
                     pk.TenPhongKham, ck.TenChuyenKhoa as ChuyenKhoa
                     FROM PhieuKham p 
                     INNER JOIN PhongKham pk ON pk.IDPhongKham = p.IDPhongKham
                     INNER JOIN ChuyenKhoa ck ON ck.IDChuyenKhoa = p.IDChuyenKhoa
                     WHERE p.IDPhongKham = @IDPhongKham
                     ORDER BY p.NgayKham, p.ThoiGianKham";

                sp = new SqlParameter[] {
            new SqlParameter("@IDPhongKham", selectedPhongKham)
        };
            }

            DataTable ds = ketNoi.docdulieu(query_list, sp);
            if (ds != null && ds.Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();


            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage",
                    "swal('Thông báo', 'Không có bệnh nhân nào đăng ký tại phòng này!', 'info');", true);
            }
        }

        //==============Filter==============
        public void viewList_Filter1(string specialty)
        {
            string query_list;
            SqlParameter[] sp;

            if (Session["Role"].ToString() == "BacSi")
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                     p.IDPhongKham, c.TenChuyenKhoa as ChuyenKhoa, c.IDChuyenKhoa
                     FROM PhieuKham p 
                     INNER JOIN ChuyenKhoa c ON c.IDChuyenKhoa = p.IDChuyenKhoa 
                     WHERE c.TenChuyenKhoa = @TenChuyenKhoa AND p.IDBacSi = @IDBacSi";

                sp = new SqlParameter[] {
            new SqlParameter("@TenChuyenKhoa", specialty),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                query_list = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                     p.IDPhongKham, c.TenChuyenKhoa as ChuyenKhoa, c.IDChuyenKhoa
                     FROM PhieuKham p 
                     INNER JOIN ChuyenKhoa c ON c.IDChuyenKhoa = p.IDChuyenKhoa 
                     WHERE c.TenChuyenKhoa = @TenChuyenKhoa";

                sp = new SqlParameter[] {
            new SqlParameter("@TenChuyenKhoa", specialty)
        };
            }

            DataTable ds = ketNoi.docdulieu(query_list, sp);
            if (ds != null && ds.Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();


            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "FilterError",
                    $"swal('Thông báo', 'Không có bệnh nhân nào thuộc chuyên khoa {specialty}!', 'info');", true);
            }
        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string searchTerm = "%" + txt_Searching.Text.Trim() + "%";
            string sql_search;
            SqlParameter[] pr;

            if (Session["Role"].ToString() == "BacSi")
            {
                sql_search = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                     p.IDPhongKham, ck.TenChuyenKhoa AS ChuyenKhoa, pk.TenPhongKham 
                     FROM PhieuKham p 
                     LEFT JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa 
                     LEFT JOIN PhongKham pk ON p.IDPhongKham = pk.IDPhongKham 
                     WHERE (p.IDBenhNhan LIKE @id OR p.HoTen LIKE @term)
                     AND p.IDBacSi = @IDBacSi";

                pr = new SqlParameter[] {
            new SqlParameter("@id", "%" + txt_Searching.Text.Trim() + "%"),
            new SqlParameter("@term", searchTerm),
            new SqlParameter("@IDBacSi", Session["UserID"].ToString())
        };
            }
            else
            {
                sql_search = @"SELECT p.IDBenhNhan, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.Email, 
                      p.IDPhongKham, ck.TenChuyenKhoa AS ChuyenKhoa, pk.TenPhongKham 
                      FROM PhieuKham p 
                      LEFT JOIN ChuyenKhoa ck ON p.IDChuyenKhoa = ck.IDChuyenKhoa 
                      LEFT JOIN PhongKham pk ON p.IDPhongKham = pk.IDPhongKham 
                      WHERE p.IDBenhNhan LIKE @id OR p.HoTen LIKE @term";

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

            ddlChuyenKhoa.SelectedIndex = 0;
            ddlPhongKham.SelectedIndex = 0;
            txt_Searching.Text = "";
            LoadData();
        }
    }
}