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

                if (Session["IDBenhNhan_HienTai"] != null)
                {
                    LoadLichSuTheoBenhNhan(Session["IDBenhNhan_HienTai"].ToString());
                }
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
            var btn = (Button)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            var txtTraLoi = (TextBox)item.FindControl("txtTraLoi");

            string traLoi = txtTraLoi.Text.Trim();
            int id = Convert.ToInt32(btn.CommandArgument);

            if (!string.IsNullOrEmpty(traLoi))
            {
                string sqlUpdate = "UPDATE TinNhanChatBot SET TraLoi = @TraLoi, TrangThai = 1 WHERE ID = @ID";
                SqlParameter[] prms = {
                    new SqlParameter("@TraLoi", traLoi),
                    new SqlParameter("@ID", id)
                };

                try
                {
                    kn.CapNhat(sqlUpdate, prms);

                    string sqlGetID = "SELECT IDBenhNhan FROM TinNhanChatBot WHERE ID = @ID";
                    SqlParameter[] p = { new SqlParameter("@ID", id) };
                    var dt = kn.docdulieu(sqlGetID, p);

                    if (dt.Rows.Count > 0)
                    {
                        string idBenhNhan = dt.Rows[0]["IDBenhNhan"].ToString();

                        // Ghi nhớ bệnh nhân đang trò chuyện
                        Session["IDBenhNhan_HienTai"] = idBenhNhan;

                        // Hiển thị toàn bộ cuộc trò chuyện của bệnh nhân đó
                        LoadLichSuTheoBenhNhan(idBenhNhan);
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Phản hồi đã được gửi thành công!');", true);
                    txtTraLoi.Text = "";
                    LoadDanhSachCauHoi();
                    UpdatePanel1.Update();
                }
                catch (Exception)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Đã có lỗi xảy ra. Vui lòng thử lại sau.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "warning", "alert('Vui lòng nhập phản hồi trước khi gửi!');", true);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            LoadDanhSachCauHoi();

            if (Session["IDBenhNhan_HienTai"] != null)
            {
                LoadLichSuTheoBenhNhan(Session["IDBenhNhan_HienTai"].ToString());
            }
        }

        protected void btnLocNgay_Click(object sender, EventArgs e)
        {
            DateTime? tuNgay = null;
            DateTime? denNgay = null;

            if (DateTime.TryParse(txtTuNgay.Text, out DateTime parsedTuNgay))
                tuNgay = parsedTuNgay;

            if (DateTime.TryParse(txtDenNgay.Text, out DateTime parsedDenNgay))
                denNgay = parsedDenNgay;

            if (Session["IDBenhNhan_HienTai"] != null)
            {
                string idBenhNhan = Session["IDBenhNhan_HienTai"].ToString();
                LoadLichSuTheoBenhNhan(idBenhNhan, tuNgay, denNgay);
            }
        }

        private void LoadLichSuTheoBenhNhan(string idBenhNhan, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            string sql = @"SELECT CauHoi, TraLoi, ThoiGian 
                   FROM TinNhanChatBot
                   WHERE IDBenhNhan = @IDBenhNhan";

            List<SqlParameter> prms = new List<SqlParameter>
    {
        new SqlParameter("@IDBenhNhan", idBenhNhan)
    };

            if (tuNgay.HasValue)
            {
                sql += " AND CONVERT(date, ThoiGian) >= @TuNgay";
                prms.Add(new SqlParameter("@TuNgay", tuNgay.Value.Date));
            }

            if (denNgay.HasValue)
            {
                sql += " AND CONVERT(date, ThoiGian) <= @DenNgay";
                prms.Add(new SqlParameter("@DenNgay", denNgay.Value.Date));
            }

            sql += " ORDER BY ThoiGian ASC";

            rptLichSu.DataSource = kn.docdulieu(sql, prms.ToArray());
            rptLichSu.DataBind();
        }
    }
}