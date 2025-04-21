using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        LopKetNoi kb = new LopKetNoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KiemTraDangNhap();
                LoadCauHoiThuongGap();
                LoadLichSuHoiDap();
            }
        }

        private void KiemTraDangNhap()
        {
            if (Session["TenDangNhap"] != null)
            {
                string tenDN = Session["TenDangNhap"].ToString();
                lnkDangNhapDangKy.InnerHtml = $"<img style='width: 28px; padding-right: 5px;' src='img/icon_nguoidung.png' /> {tenDN}";
                lnkDangNhapDangKy.HRef = "#";
                caidat.PostBackUrl = "~/Dang_Nhap.aspx";
                quanLyThongTin.Visible = true;
                btnDatLai.Visible = true;
                dangXuat.Visible = true;
            }
            else
            {
                lnkDangNhapDangKy.InnerHtml = "<i class='fas fa-user'></i> Đăng nhập / Đăng ký";
                lnkDangNhapDangKy.HRef = "~/Dang_Nhap.aspx";
                caidat.PostBackUrl = "~/Dang_Nhap.aspx";
                quanLyThongTin.Visible = false;
                btnDatLai.Visible = false;
                dangXuat.Visible = false;
            }
        }

        private void LoadCauHoiThuongGap()
        {
            string sql = "SELECT * FROM CauHoiThuongGap";
            LopKetNoi kn = new LopKetNoi();
            rptLichSuHoiDap.DataSource = kn.docdulieu(sql, null);
            rptLichSuHoiDap.DataBind();
        }

        private void LoadLichSuHoiDap()
        {
            // Giả sử bạn lưu ID người dùng đăng nhập trong Session
            string idBenhNhan = Session["IDBenhNhan"]?.ToString();

            if (!string.IsNullOrEmpty(idBenhNhan))
            {
                string sql = @"SELECT CauHoi, TraLoi, ThoiGian
                       FROM TinNhanChatBot
                       WHERE IDBenhNhan = @IDBenhNhan AND TrangThai = 1
                       ORDER BY ThoiGian DESC";

                SqlParameter[] prms = {
            new SqlParameter("@IDBenhNhan", idBenhNhan)
        };

                rptLichSuHoiDap.DataSource = kb.docdulieu(sql, prms);
                rptLichSuHoiDap.DataBind();
            }
        }

        protected void btnGuiCauHoi_Click(object sender, EventArgs e)
        {
            string idNguoiDung = Session["UserID"]?.ToString();
            string cauHoi = txtCauHoiMoi.Text.Trim();

            if (string.IsNullOrEmpty(idNguoiDung))
            {
                lblPhanHoi.Text = "❌ Bạn cần đăng nhập để gửi câu hỏi.";
                lblPhanHoi.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(cauHoi))
            {
                lblPhanHoi.Text = "❌ Vui lòng nhập câu hỏi trước khi gửi.";
                lblPhanHoi.Visible = true;
                return;
            }

            LopKetNoi kn = new LopKetNoi();
            string sql = "INSERT INTO TinNhanChatBot (IDBenhNhan, CauHoi, ThoiGian, TrangThai) VALUES (@ID, @CauHoi, GETDATE(), 0)";
            SqlParameter[] prms = {
        new SqlParameter("@ID", idNguoiDung),
        new SqlParameter("@CauHoi", cauHoi)
    };
            kn.CapNhat(sql, prms);

            lblPhanHoi.Text = "✅ Câu hỏi của bạn đã được gửi đến tư vấn viên. Vui lòng chờ phản hồi.";
            lblPhanHoi.Visible = true;
            txtCauHoiMoi.Text = "";
        }


        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Dang_Nhap.aspx");
        }
    }
}