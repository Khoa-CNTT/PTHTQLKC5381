﻿using System;
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
                    Response.Redirect("/Dang_Nhap.aspx");
                }
            }
        }
        private void LoaddPhieuKham(string idBenhNhan)
        {
            string sql2 = @"SELECT TOP 1 p.* FROM PhieuKham p
                   INNER JOIN LichKhamBenhNhan l ON p.IDPhieu = l.IDPhieu
                   WHERE p.IDBenhNhan = @IDBenhNhan
                   AND l.TrangThai = 'DaDangKy'
                   ORDER BY p.NgayKham ASC, p.ThoiGianKham ASC";
            SqlParameter[] parameters2 = { new SqlParameter("@IDBenhNhan", idBenhNhan) };

            LopKetNoi knn = new LopKetNoi();
            DataTable dtt = knn.docdulieu(sql2, parameters2);

            if (dtt != null && dtt.Rows.Count > 0)
            {
                DataRow row = dtt.Rows[0];
                lbid.Text = row["IDPhieu"].ToString();
                lbhoten.Text = row["HoTen"].ToString();
                lbemail.Text = row["Email"].ToString();
                if (row["NgaySinh"] != DBNull.Value)
                {
                    DateTime ngaySinh = Convert.ToDateTime(row["NgaySinh"]);
                    lbngaysinh.Text = ngaySinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    lbngaysinh.Text = "";
                }
                lbsdt.Text = row["SoDienThoai"].ToString();
                lbgioitinh.Text = row["GioiTinh"].ToString();
                lbdiachi.Text = row["DiaChi"].ToString();
                DateTime ngayKham = Convert.ToDateTime(row["NgayKham"]);
                TimeSpan thoiGianKham = TimeSpan.Parse(row["ThoiGianKham"].ToString());
                lbthoigian.Text = $"{ngayKham.ToString("dd/MM/yyyy")} - {thoiGianKham.ToString(@"hh\:mm")}";
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

            if (Session["DaXuLyHuy"] != null && (bool)Session["DaXuLyHuy"])
            {
                Session["DaXuLyHuy"] = false;
                return;
            }

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
                        ClientScript.RegisterStartupScript(this.GetType(), "KhongTheHuy",
                            @"swal({
                        title: 'Không thể hủy đăng ký',
                        text: 'Bạn chỉ có thể hủy đăng ký trước giờ khám 3 tiếng.',
                        icon: 'error',
                        button: 'Đóng'
                    });", true);
                        return;
                    }

                    string idPhieu = lbid.Text;
                    bool huyThanhCong = XoaDangKy(idPhieu);

                    if (huyThanhCong)
                    {
                        Session["DaXuLyHuy"] = true;
                        try
                        {
                            string tieude = "BANANA Hospital – Xác nhận huỷ đăng ký khám";
                            string noidung = @"
                            <div style='background-color: #f5f5f5; padding: 10px 0; font-family: Arial, sans-serif;'>
                              <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                                <div style='background-color: #13bdbd; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
                                  Bệnh viện BANANA HOSPITAL
                                </div>
                                <div style='padding: 30px; text-align: left;'>
                                  <h2 style='color: #13bdbd;'>Xác nhận huỷ đăng ký lịch khám</h2>
      
                                  <p>Xin chào <strong style='color: #13bdbd;'>Quý khách</strong>,</p>
                                  <p>Chúng tôi xin xác nhận rằng Quý khách đã <strong style='color: #13bdbd;'>huỷ thành công</strong> đăng ký khám với bác sĩ:</p>

                                  <ul style='list-style: none; padding-left: 0;'>
                                      <li>🧾 <strong>Mã phiếu:</strong> " + idPhieu + @"</li>
                                      <li>👤 <strong>Họ và tên:</strong> " + lbhoten.Text + @"</li>
                                      <li>🕒 <strong>Thời gian:</strong> " + lbthoigian.Text + @"</li>
                                  </ul>

                                  <p>Chúng tôi rất tiếc khi Quý khách huỷ lịch hẹn. Hy vọng sẽ được phục vụ Quý khách trong những lần tới.</p>
                                  <p>Nếu đây là sự nhầm lẫn, Quý khách vui lòng liên hệ với chúng tôi để được hỗ trợ kịp thời.</p>
                                  <p style='margin-top: 10px;'>Xin chân thành cảm ơn Quý khách đã tin tưởng <strong style='color: #13bdbd;'>BANANA Hospital</strong></p>
                                  <p>Trân trọng,</p>
                                  <p><strong style='color: #13bdbd;'>Bệnh viện BANANA HOSPITAL</strong></p>
                                </div>
                              </div>
                            </div>";
                            new sendMai_gmail().sendMail_gmail(lbemail.Text, tieude , noidung);
                        }
                        catch { }

                        string successScript = @"
                    swal({
                        title: 'Thành công',
                        text: 'Đã hủy đăng ký thành công! Hệ thống đã gửi email xác nhận',
                        icon: 'success',
                        button: 'Đóng'
                    }).then(() => {
                        window.location.href = window.location.href.split('?')[0];
                    });";

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
            string sqlCheck = "SELECT IDLichSu FROM LichSuKham WHERE IDPhieu = @IDPhieu";
            SqlParameter[] checkParams = { new SqlParameter("@IDPhieu", idPhieu) };

            LopKetNoi knn = new LopKetNoi();
            DataTable dt = knn.docdulieu(sqlCheck, checkParams);

            string sql = @"
            DELETE FROM HoSoBenhAn WHERE IDLSK IN (SELECT IDLichSu FROM LichSuKham WHERE IDPhieu = @IDPhieu);
            DELETE FROM LichSuKham WHERE IDPhieu = @IDPhieu;
            DELETE FROM LichKhamBacSi WHERE IDPhieu = @IDPhieu; 
             
            UPDATE LichKhamBenhNhan 
            SET TrangThai = 'DaHuy' 
            WHERE IDPhieu = @IDPhieu AND TrangThai != 'DaHuy';
            DELETE FROM PhieuKham WHERE IDPhieu = @IDPhieu;";
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
                  INNER JOIN LichKhamBenhNhan l ON p.IDPhieu = l.IDPhieu
                  WHERE p.IDBenhNhan = @IDBenhNhan 
                  AND l.TrangThai = 'DaDangKy'
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