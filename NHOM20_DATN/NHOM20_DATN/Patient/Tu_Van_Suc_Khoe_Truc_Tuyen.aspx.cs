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
            string sql = "SELECT b.IDBacSi, b.HoTen, b.TrinhDo, b.Email, b.VaiTro, b.ChuyenKhoaID, b.HinhAnh, c.TenChuyenKhoa FROM BacSi b " +
                         "JOIN ChuyenKhoa c ON b.ChuyenKhoaID = c.IDChuyenKhoa " +
                         "WHERE b.VaiTro = @VaiTro";
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
                Response.Redirect("../Dang_Nhap.aspx");
            }
            else
            {
                Button btn = (Button)sender;
                DataListItem item = (DataListItem)btn.NamingContainer;

                string idBacSi = ((Label)item.FindControl("lbl_idbacsi")).Text;
                string tenBacSi = ((Label)item.FindControl("lbl_hoten")).Text;
                string chuyenKhoa = ((Label)item.FindControl("lbl_chuyenkhoa")).Text;
                string trinhDo = ((Label)item.FindControl("lbl_trinhdo")).Text;
                string email = ((Label)item.FindControl("lbl_email")).Text;
                string vaiTro = ((Label)item.FindControl("lbl_vaitro")).Text;

                // Nếu không dùng UpdatePanel thì dùng Response.Redirect như sau:
                // Response.Redirect($"../Patient/Thong_Tin_Tu_Van.aspx?...");

                // Nếu có dùng UpdatePanel thì dùng dòng này:
                string url = $"../Patient/Thong_Tin_Tu_Van.aspx?IDBacSi={idBacSi}&TenBacSi={tenBacSi}&ChuyenKhoa={chuyenKhoa}&TrinhDo={trinhDo}&Email={email}&VaiTro={vaiTro}";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + url + "';", true);
            }
        }
    }
}