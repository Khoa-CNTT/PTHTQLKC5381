using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.Consultant
{
    public partial class Tu_Van_Suc_Khoe_Ban_Dau : System.Web.UI.Page
    {
        LopKetNoi kn = new LopKetNoi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachCauHoi();
            }
        }

        private void LoadDanhSachCauHoi()
        {
            string sql = @"SELECT T.ID, B.HoTen, T.CauHoi, T.ThoiGian
                           FROM TinNhanChatBot T
                           JOIN BenhNhan B ON T.IDBenhNhan = B.IDBenhNhan
                           WHERE T.TrangThai = 0
                           ORDER BY T.ThoiGian DESC";

            rptCauHoi.DataSource = kn.docdulieu(sql, null);
            rptCauHoi.DataBind();
        }

        protected void btnTraLoi_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.Button)sender;
            var item = (System.Web.UI.WebControls.RepeaterItem)btn.NamingContainer;
            var txtTraLoi = (System.Web.UI.WebControls.TextBox)item.FindControl("txtTraLoi");

            string traLoi = txtTraLoi.Text.Trim();
            int id = Convert.ToInt32(btn.CommandArgument);

            if (!string.IsNullOrEmpty(traLoi))
            {
                string sql = "UPDATE TinNhanChatBot SET TraLoi = @TraLoi, TrangThai = 1 WHERE ID = @ID";
                SqlParameter[] prms = {
                    new SqlParameter("@TraLoi", traLoi),
                    new SqlParameter("@ID", id)
                };

                try
                {
                    kn.CapNhat(sql, prms);
                    // Sau khi cập nhật, thông báo thành công cho người dùng
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Phản hồi đã được gửi thành công!');", true);
                    Response.Redirect(Request.RawUrl);  // Trang sẽ tự động reload
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, thông báo cho người dùng
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Đã có lỗi xảy ra. Vui lòng thử lại sau.');", true);
                }
            }
            else
            {
                // Nếu không có phản hồi, thông báo cho người dùng
                ScriptManager.RegisterStartupScript(this, this.GetType(), "warning", "alert('Vui lòng nhập phản hồi trước khi gửi!');", true);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            LoadDanhSachCauHoi(); // Tự động cập nhật danh sách câu hỏi
        }
    }
}