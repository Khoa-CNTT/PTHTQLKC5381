using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN
{
    public partial class Xuat_Phieu_Kham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachBenhNhan();
            }
        }

        private void LoadDanhSachBenhNhan()
        {
            LopKetNoi lop = new LopKetNoi();
            string sql = @"SELECT IDPhieu, HoTen, NgayKham, GioiTinh 
                   FROM PhieuKham 
                   ORDER BY NgayKham DESC"; 

            
            gvBenhNhan.DataSource = lop.docdulieu(sql, new SqlParameter[0]);
            gvBenhNhan.DataBind();
        }


        protected void btnTim_Click(object sender, EventArgs e)
        {
            string ten = txtTenBenhNhan.Text.Trim();
            string ngaySinh = txtNgayKham.Text.Trim();
            LopKetNoi lop = new LopKetNoi();
            string sql = "SELECT IDPhieu, HoTen, NgayKham, GioiTinh FROM PhieuKham WHERE HoTen LIKE @ten";
            if (!string.IsNullOrEmpty(ngaySinh))
            {
                sql += " AND NgayKham = @NgayKham";
            }
            SqlParameter[] param = {
        new SqlParameter("@ten", "%" + ten + "%")
    };
            if (!string.IsNullOrEmpty(ngaySinh))
            {
                param = param.Append(new SqlParameter("@NgayKham", DateTime.Parse(ngaySinh))).ToArray();
            }

            gvBenhNhan.DataSource = lop.docdulieu(sql, param);
            gvBenhNhan.DataBind();
        }
        protected void gvBenhNhan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ChonBenhNhan")
            {
                string idPhieu = e.CommandArgument.ToString();
                LopKetNoi lop = new LopKetNoi();

                // SQL query phù hợp với cấu trúc bảng PhieuKham
                string sql = @"SELECT 
                p.IDPhieu, p.HoTen, p.NgaySinh, p.GioiTinh, p.SoDienThoai, p.DiaChi, 
                p.NgayKham, p.ThoiGianKham, p.TrieuChung,
                b.HoTen AS TenBacSi, pk.TenPhongKham
            FROM PhieuKham p
            LEFT JOIN BacSi b ON p.IDBacSi = b.IDBacSi
            LEFT JOIN PhongKham pk ON p.IDPhongKham = pk.IDPhongKham
            WHERE p.IDPhieu = @id";

                SqlParameter[] pr = { new SqlParameter("@id", idPhieu) };
                DataTable dt = lop.docdulieu(sql, pr);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Hiển thị Panel
                    pnlPhieuKham.Visible = true;

                    // Đổ dữ liệu vào các Label...
                    lblMaPhieu.Text = dt.Rows[0]["IDPhieu"].ToString();
                    lblHoTen.Text = dt.Rows[0]["HoTen"].ToString();
                    lblNgaySinh.Text = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]).ToString("dd/MM/yyyy");
                    lblTuoi.Text = (DateTime.Now.Year - Convert.ToDateTime(dt.Rows[0]["NgaySinh"]).Year).ToString();
                    lblGioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                    lblSoDienThoai.Text = dt.Rows[0]["SoDienThoai"].ToString();
                    lblDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                    lblNgayKham.Text = Convert.ToDateTime(dt.Rows[0]["NgayKham"]).ToString("dd/MM/yyyy");

                    lblTrieuChung.Text = dt.Rows[0]["TrieuChung"].ToString();
                    lblBacSi.Text = dt.Rows[0]["TenBacSi"].ToString();
                    
                    lblNgay.Text = DateTime.Now.ToString("dd");
                    lblThang.Text = DateTime.Now.ToString("MM");
                    lblNam.Text = DateTime.Now.ToString("yyyy");
                   

                    // Cuộn trang xuống phần phiếu khám
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal",
         "document.getElementById('pnlPhieuKham').style.visibility='visible'; showModal();", true);
                }
            }
        }


    }
}