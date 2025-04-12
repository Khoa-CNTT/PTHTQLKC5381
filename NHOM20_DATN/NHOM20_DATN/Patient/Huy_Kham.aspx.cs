using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using NHOM20_DATN.sendMail;

namespace NHOM20_DATN
{
    public partial class Huy_Kham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra nếu người dùng đã đăng nhập
                if (Session["UserID"] != null)
                {
                    string idBenhNhan = Session["UserID"].ToString();
                    TaiDanhSachPhieuKham(idBenhNhan);
                    LoaddPhieuKham(idBenhNhan);

                }
                else
                {
                    Response.Write("<script>alert('Vui lòng đăng nhập!');</script>");
                    Response.Redirect("~/Dang_Nhap.aspx");
                }
            }
        }
        private void LoaddPhieuKham(string idBenhNhan)
        {
            string sql2 = @"SELECT TOP 1 * FROM PhieuKham 
           WHERE IDBenhNhan = @IDBenhNhan
           ORDER BY NgayKham ASC, ThoiGianKham ASC";
            SqlParameter[] parameters2 = { new SqlParameter("@IDBenhNhan", idBenhNhan) };

            LopKetNoi knn = new LopKetNoi();
            DataTable dtt = knn.docdulieu(sql2, parameters2);

            if (dtt != null && dtt.Rows.Count > 0)
            {
                DataRow row = dtt.Rows[0];
                lbid.Text = row["IDPhieu"].ToString();
                lbhoten.Text = row["HoTen"].ToString();
                lbemail.Text = row["Email"].ToString();
                lbngaysinh.Text = row["NgaySinh"].ToString();
                lbsdt.Text = row["SoDienThoai"].ToString();
                lbgioitinh.Text = row["GioiTinh"].ToString();
                lbdiachi.Text = row["DiaChi"].ToString();
                lbthoigian.Text = $"{row["NgayKham"]} - {row["ThoiGianKham"]}";
                lbtrieuchung.Text = row["TrieuChung"].ToString();
                lbphongkham.Text = row["IDPhongKham"].ToString();

            }
            else
            {
                lbid.Text = "";
                lbhoten.Text = "";
                lbemail.Text = "";
                lbngaysinh.Text = "";
                lbsdt.Text = "";
                lbgioitinh.Text = "";
                lbdiachi.Text = "";
                lbthoigian.Text = "";
                lbtrieuchung.Text = "";
                lbphongkham.Text = "";
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "swal('Lỗi','Không tìm thấy phiếu khám nào!','error');", true);
            }
        }

        protected void btnnut_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lbthoigian.Text))
            {
                string[] parts = lbthoigian.Text.Split('-');
                DateTime ngayKham;
                TimeSpan gioKham;

                if (DateTime.TryParse(parts[0].Trim(), out ngayKham) &&
                    TimeSpan.TryParse(parts[1].Trim(), out gioKham))
                {
                    DateTime thoiDiemKham = ngayKham.Date + gioKham;
                    if (DateTime.Now > thoiDiemKham.AddHours(-3))
                    {
                        string script = @"
                    swal({
                        title: 'Không thể hủy đăng ký',
                        text: 'Bạn chỉ có thể hủy đăng ký trước giờ khám 3 tiếng. Thời gian hủy đã hết!',
                        icon: 'error',
                        button: 'Đóng'
                    });";
                        ClientScript.RegisterStartupScript(this.GetType(), "KhongTheHuy", script, true);
                        return;
                    }

                    string email = lbemail.Text;
                    string hoTen = lbhoten.Text;
                    string thoiGian = lbthoigian.Text;
                    string idPhieu = lbid.Text;
                    bool huyThanhCong = XoaDangKy(idPhieu);

                    if (huyThanhCong)
                    {
                        string successScript = @"
                    swal({
                        title: 'Thành công',
                        text: 'Bạn đã hủy đăng ký khám thành công!',
                        icon: 'success',
                        button: 'Đóng'
                    }).then(() => {";
                        string tieude = "BANANA HOPITAL ĐÀ NẴNG XIN CHÀO QUÝ KHÁCH !!!\n ";
                        string noidung = "Bạn đã hủy đăng ký khám bệnh thành công\n" +
                                        "\nID Phiếu: " + idPhieu +
                                        "\nHọ tên: " + hoTen +
                                        "\nThời gian: " + thoiGian;

                        sendMai_gmail sendmail = new sendMai_gmail();
                        int emailResult = sendmail.sendMail_gmail(email, tieude, noidung);

                        if (emailResult > 0)
                        {
                            successScript += @"
                        swal({
                            title: 'Thành công',
                            text: 'Đã gửi email thông báo hủy khám!',
                            icon: 'success',
                            button: 'Đóng'
                        }).then(() => {";
                        }
                        successScript += @"
                            " + Page.ClientScript.GetPostBackEventReference(btnnut, "") + @"
                        });";

                        if (emailResult > 0)
                        {
                            successScript += "});";
                        }

                        ClientScript.RegisterStartupScript(this.GetType(), "Success", successScript, true);
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "swal('Lỗi','Không thể xác định thời gian khám!','error');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "swal('Lỗi','Không tìm thấy thông tin đăng ký!','error');", true);
            }
            string idBenhNhan = Session["UserID"].ToString();
            LoaddPhieuKham(idBenhNhan);
            TaiDanhSachPhieuKham(idBenhNhan);
            gvDanhSachPhieu.SelectedIndex = -1;
        }

        private bool XoaDangKy(string idPhieu)
        {
            string sql = "DELETE FROM LichKhamBacSi WHERE IDPhieu = @IDPhieu; " +
            "DELETE FROM LichKhamBenhNhan WHERE IDPhieu = @IDPhieu; " +
            "DELETE FROM PhieuKham WHERE IDPhieu = @IDPhieu; ";
            SqlParameter[] parameters = {
             new SqlParameter("@IDPhieu", idPhieu) };

            LopKetNoi knn1 = new LopKetNoi();
            int result = knn1.CapNhat(sql, parameters);

            return result > 0;
        }


        protected void lnkShowPhongKham_Click(object sender, EventArgs e)
        {
            LopKetNoi knn = new LopKetNoi();
            DataTable dt = knn.docdulieu("SELECT IDPhongKham, TenPhongKham, MoTa, ViTri FROM PhongKham", null);
            string phongKhamInfo = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    phongKhamInfo += "ID: " + row["IDPhongKham"] + "<br/>";
                    phongKhamInfo += "Tên Phòng Khám: " + row["TenPhongKham"] + "<br/>";
                    phongKhamInfo += "Mô Tả: " + row["MoTa"] + "<br/>";
                    phongKhamInfo += "Vị Trí: " + row["ViTri"] + "<br/><br/>";
                }
            }
            else
            {
                phongKhamInfo = "Không tìm thấy thông tin phòng khám.";
            }

            ClientScript.RegisterStartupScript(this.GetType(), "showInfo", "showPhongKhamInfo('" + phongKhamInfo + "');", true);
        }

        private void TaiDanhSachPhieuKham(string idBenhNhan)
        {
            string sql = @"SELECT p.IDPhieu, p.NgayKham
              FROM PhieuKham p
              WHERE p.IDBenhNhan = @IDBenhNhan
              ORDER BY p.NgayKham ASC";

            SqlParameter[] parameters = { new SqlParameter("@IDBenhNhan", idBenhNhan) };
            LopKetNoi knn = new LopKetNoi();
            DataTable dt = knn.docdulieu(sql, parameters);

            if (dt == null || dt.Rows.Count == 0)
            {
                dt = new DataTable();
                dt.Columns.Add("IDPhieu");
                dt.Columns.Add("NgayKham");

                DataRow newRow = dt.NewRow();
                newRow["IDPhieu"] = "KHÔNG CÓ PHIẾU";
                newRow["NgayKham"] = DBNull.Value;
                dt.Rows.Add(newRow);
            }

            gvDanhSachPhieu.DataSource = dt;
            gvDanhSachPhieu.DataBind();

            if (dt.Rows.Count == 1 && dt.Rows[0]["IDPhieu"].ToString() == "KHÔNG CÓ PHIẾU")
            {
                gvDanhSachPhieu.Rows[0].Visible = false;
            }
        }

        protected void gvDanhSachPhieu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  sự kiện click cho dòng
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvDanhSachPhieu, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#e6f2ff';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                if (e.Row.RowIndex == gvDanhSachPhieu.SelectedIndex)
                {
                    e.Row.CssClass = "selected-row";
                    e.Row.Style["background-color"] = "#cce5ff";
                    e.Row.Style["font-weight"] = "bold";
                }
            }
        }

        protected void gvDanhSachPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvDanhSachPhieu.SelectedIndex >= 0)
            {
                string idPhieu = gvDanhSachPhieu.SelectedDataKey.Value.ToString();
                LoadChiTietPhieuKham(idPhieu);
                string idBenhNhan = Session["UserID"].ToString();
                TaiDanhSachPhieuKham(idBenhNhan);
                gvDanhSachPhieu.SelectedIndex = gvDanhSachPhieu.SelectedIndex;
            }
        }

        private void LoadChiTietPhieuKham(string idPhieu)
        {
            string sql = @"SELECT * FROM PhieuKham WHERE IDPhieu = @IDPhieu";
            SqlParameter[] parameters = { new SqlParameter("@IDPhieu", idPhieu) };

            LopKetNoi knn = new LopKetNoi();
            DataTable dt = knn.docdulieu(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lbid.Text = row["IDPhieu"].ToString();
                lbhoten.Text = row["HoTen"].ToString();
                lbemail.Text = row["Email"].ToString();
                lbngaysinh.Text = row["NgaySinh"].ToString();
                lbsdt.Text = row["SoDienThoai"].ToString();
                lbgioitinh.Text = row["GioiTinh"].ToString();
                lbdiachi.Text = row["DiaChi"].ToString();
                lbthoigian.Text = $"{Convert.ToDateTime(row["NgayKham"]).ToString("dd/MM/yyyy")} - {row["ThoiGianKham"].ToString()}";
                lbtrieuchung.Text = row["TrieuChung"].ToString();
                lbphongkham.Text = row["IDPhongKham"].ToString();
            }
        }
        protected void gvDanhSachPhieu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectRow")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string idPhieu = gvDanhSachPhieu.DataKeys[rowIndex].Value.ToString();
                LoadChiTietPhieuKham(idPhieu);
                foreach (GridViewRow row in gvDanhSachPhieu.Rows)
                {
                    row.CssClass = "";
                }
                gvDanhSachPhieu.Rows[rowIndex].CssClass = "selected-row";
            }
        }
    }
}