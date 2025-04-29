using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NHOM20_DATN
{
    public partial class Xem_Lich_Su_Kham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("/Dang_Nhap.aspx");

            if (!IsPostBack)
                LoadLichSuKham(Session["UserID"].ToString());
        }

        private void LoadLichSuKham(string userID)
        {
            string sql = @"
SELECT 
    ls.IDLichSu,
    bn.HoTen AS HoTenBenhNhan,
    bs.HoTen AS HoTenBacSi,  
    ls.ChanDoan, 
    ls.HuongDieuTri,
    hsa.NgayCapNhat,  
    hsa.ChanDoan AS ChanDoanHoSo, 
    hsa.DonThuoc
FROM LichSuKham ls
LEFT JOIN HoSoBenhAn hsa ON ls.IDLichSu = hsa.IDLSK
INNER JOIN PhieuKham pk ON ls.IDPhieu = pk.IDPhieu
INNER JOIN BenhNhan bn ON pk.IDBenhNhan = bn.IDBenhNhan
INNER JOIN BacSi bs ON pk.IDBacSi = bs.IDBacSi
WHERE ls.IDBenhNhan = @UserID
ORDER BY hsa.NgayCapNhat DESC";

            var pars = new[] { new SqlParameter("@UserID", userID) };
            var dt = new LopKetNoi().docdulieu(sql, pars);

            gvLichSuKham.DataSource = dt;
            gvLichSuKham.DataBind();
        }

        protected void gvLichSuKham_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            DataRowView row = (DataRowView)e.Row.DataItem;
            string tenBN = row["HoTenBenhNhan"].ToString();
            string tenBS = row["HoTenBacSi"].ToString();
            string ngayKham = row["NgayCapNhat"] != DBNull.Value
                             ? Convert.ToDateTime(row["NgayCapNhat"]).ToString("dd/MM/yyyy")
                             : "Chưa cập nhật";
            string chanDoan = row["ChanDoan"].ToString();
            string huong = row["HuongDieuTri"].ToString();
            string donThuoc = row["DonThuoc"]?.ToString() ?? "Không có";

            e.Row.Attributes["onclick"] =
                $"showDetail('{tenBN}','{tenBS}','{ngayKham}','{chanDoan}','{huong}','{donThuoc}');";
        }
    }
}
