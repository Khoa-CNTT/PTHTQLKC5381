using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Doctor
{
    public partial class Quan_Ly_Thong_Tin_Ca_Nhan_Bac_Si_Offline : System.Web.UI.Page
    {
        LopKetNoi kn = new LopKetNoi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadThongTin();
            }
        }

        private void LoadThongTin()
        {
            string id = Session["UserID"].ToString();
            string sql = @"
                SELECT bs.HoTen, ck.TenChuyenKhoa AS ChuyenKhoa, bs.DiaChiPhongKham, bs.TrinhDo,
                       bs.SoDienThoai, bs.Email, bs.VaiTro, bs.HinhAnh
                FROM BacSi bs
                JOIN ChuyenKhoa ck ON bs.ChuyenKhoaID = ck.IDChuyenKhoa
                WHERE bs.IDBacSi = @ID";
            SqlParameter[] pr = { new SqlParameter("@ID", id) };
            DataTable dt = kn.docdulieu(sql, pr);

            dvThongTin.DataSource = dt;
            dvThongTin.DataBind();
        }

        protected void dvThongTin_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            string id = Session["UserID"].ToString();
            string diaChi = e.NewValues["DiaChiPhongKham"]?.ToString() ?? "";
            string trinhDo = e.NewValues["TrinhDo"]?.ToString() ?? "";
            string soDT = e.NewValues["SoDienThoai"]?.ToString() ?? "";
            string email = e.NewValues["Email"]?.ToString() ?? "";

            string sql = @"UPDATE BacSi SET DiaChiPhongKham = @DiaChi, TrinhDo = @TrinhDo, 
                           SoDienThoai = @SDT, Email = @Email WHERE IDBacSi = @ID";
            SqlParameter[] pr = {
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@TrinhDo", trinhDo),
                new SqlParameter("@SDT", soDT),
                new SqlParameter("@Email", email),
                new SqlParameter("@ID", id)
            };

            kn.CapNhat(sql, pr);
            dvThongTin.ChangeMode(DetailsViewMode.ReadOnly);
            LoadThongTin();

            string script = "Swal.fire({ icon: 'success', title: 'Cập nhật thành công', text: 'Thông tin của bạn đã được lưu!' });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "thongbao", script, true);
        }

        protected void dvThongTin_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            dvThongTin.ChangeMode(e.NewMode);
            LoadThongTin();
        }
    }
}