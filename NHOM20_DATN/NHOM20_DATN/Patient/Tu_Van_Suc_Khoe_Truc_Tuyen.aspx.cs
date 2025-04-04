using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.Patient
{
    public partial class Tu_Van_Suc_Khoe_Truc_Tuyen : System.Web.UI.Page
    {
        LopKetNoi db = new LopKetNoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBacSiTuVanOnline();
            }
        }

        private void LoadBacSiTuVanOnline()
        {
            string sql = "SELECT IDBacSi, HoTen, TrinhDo, Email, VaiTro, ChuyenKhoaID, HinhAnh FROM BacSi WHERE VaiTro = @VaiTro";
            SqlParameter[] parameters = {
                new SqlParameter("@VaiTro", "Online")
            };

            DataTable dt = db.docdulieu(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                dl_bacsi.DataSource = dt;
                dl_bacsi.DataBind();
            }
            else
            {
                // Xử lý khi không có bác sĩ nào
                dl_bacsi.DataSource = null;
                dl_bacsi.DataBind();
            }
        }

        protected void btn_tuvan_Click(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                // Lấy nút bấm đã được click
                Button btn = (Button)sender;

                // Tìm DataListItem chứa nút bấm này
                DataListItem item = (DataListItem)btn.NamingContainer;

                // Lấy thông tin bác sĩ từ các Label trong DataListItem
                Label lblIDBacSi = (Label)item.FindControl("lbl_idbacsi"); // Thay đổi thành lbl_idbacsi
                string idBacSi = lblIDBacSi.Text; // Lấy ID bác sĩ
                string tenBacSi = ((Label)item.FindControl("lbl_hoten")).Text; // Lấy tên bác sĩ
                string chuyenKhoa = ((Label)item.FindControl("lbl_chuyenkhoa")).Text;
                string trinhDo = ((Label)item.FindControl("lbl_trinhdo")).Text;
                string email = ((Label)item.FindControl("lbl_email")).Text;
                string vaiTro = ((Label)item.FindControl("lbl_vaitro")).Text;

                // Điều hướng đến trang ThongTinTuVan và truyền thông tin bác sĩ
                Response.Redirect($"../Patient/Thong_Tin_Tu_Van.aspx?IDBacSi={idBacSi}&TenBacSi={tenBacSi}&ChuyenKhoa={chuyenKhoa}&TrinhDo={trinhDo}&Email={email}&VaiTro={vaiTro}");
            }
        }
    }
}