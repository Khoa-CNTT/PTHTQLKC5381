using NHOM20_DATN.sendMail;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN
{
    public partial class Dang_Ky_Kham_Truc_Tiep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Chỉ tải thông tin người dùng và chuyên khoa/phòng khám khi lần đầu tiên tải trang
            {
                BindNgayKhamRepeater();

                // Đặt thuộc tính min cho txtNgayKham
                txtNgayKham.Attributes["min"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");


                txtNgayKham.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                if (Session["UserID"] != null)
                {
                    string userID = Session["UserID"].ToString();
                    LoadUserData(userID);
                }
                else
                {
                    Response.Redirect("~/Dang_Nhap.aspx");
                }
                LoadChuyenKhoa();
               

            }
        }
        private void LoadUserData(string userID)
        {
            string sql = "SELECT HoTen, Email, NgaySinh,GioiTinh, SoDienThoai, CanCuocCongDan, DiaChi FROM BenhNhan WHERE IDBenhNhan = @UserID";
            SqlParameter[] parameters = {
        new SqlParameter("@UserID", userID)
    };

            LopKetNoi kn = new LopKetNoi();
            DataTable userData = kn.docdulieu(sql, parameters);

            if (userData.Rows.Count > 0)
            {
                DataRow row = userData.Rows[0];

                // Kiểm tra thông tin cá nhân có đầy đủ hay không
                bool isMissingInfo = false;

                txtHoTen.Text = row["HoTen"].ToString();
                if (string.IsNullOrEmpty(txtHoTen.Text))
                {
                    isMissingInfo = true;
                }
                txtHoTen.ReadOnly = true;
                txtEmail.Text = row["Email"].ToString();
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    isMissingInfo = true;
                }
                txtEmail.ReadOnly = true; // không cho bệnh nhân chỉnh sửa khi load dữ liệu lên
                gtRadioList.SelectedValue = row["GioiTinh"].ToString();
                
                if (row["NgaySinh"] != DBNull.Value)
                {
                    txtNgaySinh.Text = Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    txtNgaySinh.Text = "Thông tin chưa có";
                    isMissingInfo = true;
                }
                txtNgaySinh.ReadOnly = true;
                txtSoDienThoai.Text = row["SoDienThoai"].ToString();
                if (string.IsNullOrEmpty(txtSoDienThoai.Text))
                {
                    isMissingInfo = true;
                }
                txtSoDienThoai.ReadOnly = true;
                txtDiaChi.Text = row["DiaChi"].ToString();
                if (string.IsNullOrEmpty(txtDiaChi.Text))
                {
                    isMissingInfo = true;
                }
                txtDiaChi.ReadOnly = true;
                txtCCCD.Text = row["CanCuocCongDan"].ToString();
                if (string.IsNullOrEmpty(txtCCCD.Text))
                {
                    isMissingInfo = true;
                }
                txtCCCD.ReadOnly = true;
                // Nếu thiếu thông tin, hiển thị thông báo cập nhật
                if (isMissingInfo)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy cập nhật thông tin cá nhân.', 'warning');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy cập nhật thông tin cá nhân.', 'warning');", true);
            }
        }
        private void BindNgayKhamRepeater()
        {
            // Mảng chuyển đổi DayOfWeek sang tiếng Việt
            string[] dayOfWeekVN = {
        "Chủ nhật", "Thứ hai", "Thứ ba",
        "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"
    };

            DateTime today = DateTime.Today;
            var listDates = new List<dynamic>();

            // Ví dụ: hiển thị hôm nay và 3 ngày tiếp theo (4 ngày tổng cộng)
            for (int i = 1; i < 7; i++)
            {
                DateTime d = today.AddDays(i);
                listDates.Add(new
                {
                    NgayValue = d.ToString("yyyy-MM-dd"), // giá trị dùng cho txtNgayKham
                    NgayThang = d.ToString("dd/MM"),
                    Thu = dayOfWeekVN[(int)d.DayOfWeek]
                });
            }

            rptNgayKham.DataSource = listDates;
            rptNgayKham.DataBind();
        }
        protected void txtNgayKham_TextChanged(object sender, EventArgs e)
        {
            string ngayKham = txtNgayKham.Text;
            if (!string.IsNullOrEmpty(ngayKham))
            {
                // Gọi hàm load buổi khám dựa theo bác sĩ và ngày khám
                LoadBuoiKham(ddlBacSi.SelectedValue, ngayKham);
            }
        }
        private void LoadChuyenKhoa()
        {
            string sql_specialist = "SELECT * FROM ChuyenKhoa";
            SqlParameter[] checkParams = { };
            LopKetNoi kn = new LopKetNoi();

            DataTable chuyenkhoa_DT = kn.docdulieu(sql_specialist, checkParams);
            ddlChuyenKhoa.DataSource = chuyenkhoa_DT;
            ddlChuyenKhoa.DataTextField = "TenChuyenKhoa";
            ddlChuyenKhoa.DataValueField = "IDChuyenKhoa";
            ddlChuyenKhoa.DataBind();
            ddlChuyenKhoa.Items.Insert(0, new ListItem("Chọn chuyên khoa"));
            LoadBacSi(ddlChuyenKhoa.SelectedItem.Value);
        }

        private void LoadBacSi(string ChuyenkhoaId)
        {
            string sql_doctor = "SELECT * FROM BacSi WHERE ChuyenKhoaID = @ChuyenkhoaId AND VaiTro = @VaiTro ORDER BY IDBacSi;";
            SqlParameter[] checkParams = {
        new SqlParameter("@ChuyenkhoaId", ChuyenkhoaId),
        new SqlParameter("@VaiTro", "Offline")
    };

            LopKetNoi kn = new LopKetNoi();
            DataTable bs_DT = kn.docdulieu(sql_doctor, checkParams);
            ddlBacSi.DataSource = bs_DT;
            ddlBacSi.DataTextField = "HoTen";
            ddlBacSi.DataValueField = "IDBacSi";
            ddlBacSi.DataBind();
            ddlBacSi.Items.Insert(0, new ListItem("Chọn bác sĩ", "0"));
        }
        protected void ddlChuyenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChuyenKhoa.SelectedIndex > 0) // Nếu chọn chuyên khoa hợp lệ
            {
                string chuyenKhoaId = ddlChuyenKhoa.SelectedValue;
                LoadBacSi(chuyenKhoaId);
                LoadPhongKham(chuyenKhoaId);

                // Reset buổi khám
                ddlbuoikham.Items.Clear();
                ddlbuoikham.Items.Insert(0, new ListItem("Chọn buổi khám", ""));
            }
            else
            {
                // Xóa danh sách nếu chọn "Chọn chuyên khoa"
                ddlBacSi.Items.Clear();
                ddlBacSi.Items.Insert(0, new ListItem("Chọn bác sĩ", "0"));
                ddlPhongKham.Items.Clear();
                ddlPhongKham.Items.Insert(0, new ListItem("Chọn phòng khám", ""));
            }
        }

        private void LoadPhongKham(string chuyenKhoaId)
        {
            string sql = "SELECT * FROM PhongKham WHERE IDChuyenKhoa = @ChuyenKhoaID";
            SqlParameter[] parameters = {
        new SqlParameter("@ChuyenKhoaID", chuyenKhoaId)
    };

            LopKetNoi kn = new LopKetNoi();
            DataTable dt = kn.docdulieu(sql, parameters);

            ddlPhongKham.DataSource = dt;
            ddlPhongKham.DataTextField = "TenPhongKham";
            ddlPhongKham.DataValueField = "IDPhongKham";
            ddlPhongKham.DataBind();
            ddlPhongKham.Items.Insert(0, new ListItem("Chọn phòng khám", ""));
        }
        private void LoadBuoiKham(string BacSiId, string ngayKham)
        {
            // Truy vấn lấy các giá trị Buoi và IDBuoi
            string sql_buoiKham = "SELECT IDBuoi, Buoi FROM BuoiKham " +
                                  "WHERE IDBacSi = @IDBacSi AND NgayKham = @NgayKham " +
                                  "ORDER BY Buoi";

            SqlParameter[] parameters = {
        new SqlParameter("@IDBacSi", BacSiId),
        new SqlParameter("@NgayKham", ngayKham)
    };

            LopKetNoi kn = new LopKetNoi();
            DataTable buoiKham_DT = kn.docdulieu(sql_buoiKham, parameters);
            if (!string.IsNullOrWhiteSpace(ngayKham))
            {
                if (buoiKham_DT != null && buoiKham_DT.Rows.Count > 0)
                {
                    var distinctBuoi = buoiKham_DT.AsEnumerable()
                        .GroupBy(row => row["Buoi"])
                        .Select(g => g.First());

                    if (distinctBuoi.Any())
                    {
                        DataTable dtDistinctBuoi = distinctBuoi.CopyToDataTable();
                        ddlbuoikham.DataSource = dtDistinctBuoi;
                        ddlbuoikham.DataValueField = "IDBuoi";
                        ddlbuoikham.DataTextField = "Buoi";
                        ddlbuoikham.DataBind();


                        ddlbuoikham.Items.Insert(0, new ListItem("Chọn buổi khám", ""));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Bác sĩ không có giờ khám này.', 'warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Bác sĩ không có giờ khám này.', 'warning');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Bác sĩ không có giờ khám này.', 'warning');", true);
            }
        }
        protected void ddlBacSi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBacSi.SelectedValue != "0")
            {
                LoadNgayKham(ddlBacSi.SelectedValue);
                ddlbuoikham.Items.Clear();
                ddlbuoikham.Items.Insert(0, new ListItem("Chọn buổi khám", ""));
                DDLgiokham.Items.Clear();
            }
            else
            {
                txtNgayKham.Text = "";
                ddlbuoikham.Items.Clear();
                DDLgiokham.Items.Clear();
            }
        }

        private void LoadNgayKham(string BacSiId)
        {
            string sql_ngayKham = "SELECT DISTINCT Buoi, IDBuoi FROM BuoiKham WHERE IDBacSi = @IDBacSi AND NgayKham = @NgayKham";
            SqlParameter[] parameters = {
        new SqlParameter("@IDBacSi", BacSiId)
    };

            LopKetNoi kn = new LopKetNoi();
            DataTable ngayKham_DT = kn.docdulieu(sql_ngayKham, parameters);

            if (ngayKham_DT != null && ngayKham_DT.Rows.Count > 0)
            {

                string firstAvailableDate = Convert.ToDateTime(ngayKham_DT.Rows[0]["NgayKham"]).ToString("yyyy-MM-dd");
                txtNgayKham.Text = firstAvailableDate;
            }
            else
            {
                txtNgayKham.Text = "";
            }
        }
        protected void ddlbuoikham_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLgiokham.Items.Clear();  // Xóa các item cũ trong ddlgiokham

            if (ddlbuoikham.SelectedValue != "" && !string.IsNullOrEmpty(txtNgayKham.Text))
            {
                string buoiKhamId = ddlbuoikham.SelectedValue;
                string ngayKham = txtNgayKham.Text;

                // Cập nhật truy vấn để lấy tất cả các ThoiGianKham cho BuoiKham tương ứng
                string sql_gioKham = "SELECT ThoiGianKham FROM BuoiKham " +
                              "WHERE IDBacSi = @IDBacSi AND Buoi = @Buoi AND NgayKham = @NgayKham " +
                              "ORDER BY ThoiGianKham";
                SqlParameter[] parameters = {
    new SqlParameter("@IDBacSi", ddlBacSi.SelectedValue),
    new SqlParameter("@Buoi", ddlbuoikham.SelectedItem.Text), // Sử dụng giá trị Buoi từ dropdown
    new SqlParameter("@NgayKham", txtNgayKham.Text)
};

                LopKetNoi kn = new LopKetNoi();
                DataTable gioKham_DT = kn.docdulieu(sql_gioKham, parameters);

                if (gioKham_DT != null && gioKham_DT.Rows.Count > 0)
                {
                    foreach (DataRow row in gioKham_DT.Rows)
                    {
                        string gioKham = row["ThoiGianKham"].ToString();
                        DDLgiokham.Items.Add(new ListItem(gioKham, gioKham));  // Thêm giờ khám vào ddlgiokham
                    }
                }
                else
                {
                    DDLgiokham.Items.Add(new ListItem("Không có giờ khám", ""));
                }
            }
            else
            {
                DDLgiokham.Items.Add(new ListItem("Giờ Khám", ""));
            }
        }
        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Vui lòng đăng nhập trước khi đăng ký khám.', 'warning');", true);
                return;
            }

            // kiểm tra các trường đã được chọn chưa nếu chưa thì sẽ thông báo

            if (ddlChuyenKhoa.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy chọn chuyên khoa.', 'warning');", true);
                return;
            }

            if (ddlPhongKham.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy chọn phòng khám.', 'warning');", true);
                return;
            }

            if (string.IsNullOrEmpty(txtNgayKham.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy chọn ngày khám.', 'warning');", true);
                return;
            }

            if (DDLgiokham.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy chọn giờ khám.', 'warning');", true);
                return;
            }

            if (string.IsNullOrEmpty(txtTrieuChung.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy nhập triệu chứng.', 'warning');", true);
                return;
            }
            if (ddlbuoikham.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Vui lòng chọn buổi khám.', 'warning');", true);
                return;
            }


            string idBenhNhan = Session["UserID"].ToString();
            string idBacSi = ddlBacSi.SelectedValue;
            string idPhongKham = ddlPhongKham.SelectedValue; // Lấy IDPhongKham từ dropdown phòng khám
            string idhoten = txtHoTen.Text;
            string idchuyenkhoa = ddlChuyenKhoa.SelectedValue;
            string idemail = txtEmail.Text;
            string idsdt = txtSoDienThoai.Text;
            string idcccd = txtCCCD.Text;
            string idgioitinh = gtRadioList.Text;
            string iddiachi = txtDiaChi.Text;
            string idngaysinh = txtNgaySinh.Text;
            string lyDoKham = txtTrieuChung.Text;
            string idngaykham = txtNgayKham.Text;
            string idgiokham = DDLgiokham.SelectedValue;
            string buoiKham = ddlbuoikham.SelectedValue;
            

            // kiểm tra xem đã chọn bác sĩ chưa

            string checkBacSiSql = "SELECT COUNT(*) FROM BacSi WHERE IDBacSi = @IDBacSi";
            SqlParameter[] checkBacSiParams = {
            new SqlParameter("@IDBacSi", idBacSi)
};

            LopKetNoi checkBacSiDb = new LopKetNoi();
            DataTable dtBacSi = checkBacSiDb.docdulieu(checkBacSiSql, checkBacSiParams);

            if (dtBacSi == null || dtBacSi.Rows.Count == 0 || Convert.ToInt32(dtBacSi.Rows[0][0]) == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Bác sĩ không hợp lệ vui lòng chọn lại.', 'warning');", true);
                return;
            }



            // Kiểm tra xem bệnh nhân đã đăng ký trong ngày chưa
            string checkDuplicateSql = "SELECT COUNT(*) FROM LichKhamBenhNhan WHERE IDBenhNhan = @IDBenhNhan AND CAST(NgayKham AS DATE) = CAST(@NgayKham AS DATE) AND TrangThai <> 'DaHuy'";
            SqlParameter[] checkDuplicateParams = {
        new SqlParameter("@IDBenhNhan", idBenhNhan),
        new SqlParameter("@NgayKham", idngaykham)
    };

            LopKetNoi checkDbb = new LopKetNoi();
            DataTable dtDuplicate = checkDbb.docdulieu(checkDuplicateSql, checkDuplicateParams);
            int duplicateCount = 0;

            if (dtDuplicate != null && dtDuplicate.Rows.Count > 0)
            {
                duplicateCount = Convert.ToInt32(dtDuplicate.Rows[0][0]);
            }

            if (duplicateCount > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Bạn đã đăng ký khám trong ngày hôm nay ! Không thể đăng ký thêm.', 'warning');", true);
                return;
            }
            // Kiểm tra số lượng bệnh nhân đã đăng ký trong khung giờ
            string checkSql = "SELECT COUNT(*) FROM PhieuKham WHERE NgayKham = @NgayKham AND ThoiGianKham = @ThoiGianKham AND IDBacSi = @IDBacSi";
            SqlParameter[] checkParams = {
        new SqlParameter("@NgayKham", idngaykham),
        new SqlParameter("@ThoiGianKham", idgiokham),
         new SqlParameter("@IDBacSi", idBacSi)
    };

            LopKetNoi checkDb = new LopKetNoi();
            DataTable dt = checkDb.docdulieu(checkSql, checkParams);
            int count = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                count = Convert.ToInt32(dt.Rows[0][0]);
            }

            if (count >= 2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Khung giờ này đã đầy, vui lòng chọn giờ khác.', 'warning');", true);
                return;
            }


            //string newIdLKBN = "LKBN" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            string newId = "PK" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            //string newIdLKBS = "LKBS" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            string newIdlsk = "LS" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

            string insertSql = "INSERT INTO PhieuKham (IDPhieu, IDBenhNhan, IDBacSi, IDPhongKham, IDChuyenKhoa, HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, NgayKham, ThoiGianKham, TrieuChung,  IDBuoi) " +
                       "VALUES (@IDPhieu, @IDBenhNhan, @IDBacSi, @IDPhongKham, @IDChuyenKhoa, @HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi, @NgayKham, @ThoiGianKham, @TrieuChung, @IDBuoi)";
            string inserthsba = "INSERT INTO HoSoBenhAn (IDBS,IDBN,IDLSK) " +
                     "VALUES (@IDBacSi,@IDBenhNhan,@IDLSK)";
            string insertlsk = "INSERT INTO LichSuKham (IDLichSu,IDBenhNhan,IDPhieu) " +
                     "VALUES (@IDLichSu,@IDBenhNhan,@IDPhieu)";

            string insertlkbs = "INSERT INTO LichKhamBacSi (IDBenhNhan, IDBacsi,IDPhieu,NgayKham,ThoiGianKham,SoPhongKham,IDBuoi) " +
                     "VALUES (@IDBenhNhan, @IDBacSi,@IDPhieu, @NgayKham, @ThoiGianKham, @SoPhongKham,@IDBuoi)";

            string insertlkbn = "INSERT INTO LichKhamBenhNhan(IDBenhNhan,IDPhieu,IDBuoi,TrangThai,NgayKham,ThoiGianKham)" +
                "VALUES (@IDBenhNhan,@IDPhieu,@IDBuoi,@TrangThai,@NgayKham,@ThoiGianKham)";
            SqlParameter[] parametersForLkbs = {
    //new SqlParameter("@ID", newIdLKBS),
    new SqlParameter("@IDBacSi", idBacSi),
    new SqlParameter("@NgayKham", idngaykham),
    new SqlParameter("@ThoiGianKham", idgiokham),
    new SqlParameter("@SoPhongKham", idPhongKham),
    new SqlParameter("@IDBenhNhan", idBenhNhan),
    new SqlParameter("@IDPhieu", newId),
    new SqlParameter("@IDBuoi", buoiKham)
};

            SqlParameter[] parametersLKBN =
            {
                //new SqlParameter("@ID", newIdLKBN),
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                new SqlParameter("@IDPhieu", newId),
                new SqlParameter("@IDBuoi", buoiKham),
                new SqlParameter("@TrangThai","DaDangKy"),
                new SqlParameter("@NgayKham", idngaykham),
                new SqlParameter("@ThoiGianKham", idgiokham)


            };

            SqlParameter[] parametersHSBA =
          {
                //new SqlParameter("@ID", newIdLKBN),
                 new SqlParameter("@IDBacSi", idBacSi),
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                new SqlParameter("@IDLSK", newIdlsk)
            };

            SqlParameter[] parametersLSK =
        {
                //new SqlParameter("@ID", newIdLKBN),
                new SqlParameter("@IDLichSu", newIdlsk),
                
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                 new SqlParameter("@IDPhieu", newId)

            };

            SqlParameter[] parametersForPhieuKham = {
        new SqlParameter("@IDPhieu", newId),
        new SqlParameter("@IDBenhNhan", idBenhNhan),
        new SqlParameter("@IDBacSi", idBacSi),
        new SqlParameter("@IDPhongKham", idPhongKham),
        new SqlParameter("@IDChuyenKhoa", idchuyenkhoa),
        new SqlParameter("@HoTen", idhoten),
        new SqlParameter("@NgaySinh", idngaysinh),
        new SqlParameter("@GioiTinh", idgioitinh),
        new SqlParameter("@SoDienThoai", idsdt),
        new SqlParameter("@Email", idemail),
        new SqlParameter("@DiaChi", iddiachi),
        new SqlParameter("@NgayKham", idngaykham),
        new SqlParameter("@ThoiGianKham", idgiokham),
        new SqlParameter("@TrieuChung", lyDoKham),
        new SqlParameter("@IDBuoi", buoiKham)
    };
            LopKetNoi kb = new LopKetNoi();
            int result = kb.CapNhat(insertSql, parametersForPhieuKham);

            LopKetNoi lkbs = new LopKetNoi();
            int resultLkbs = lkbs.CapNhat(insertlkbs, parametersForLkbs);

            LopKetNoi lkbn = new LopKetNoi();
            int resultLkbn = lkbn.CapNhat(insertlkbn, parametersLKBN);

            LopKetNoi LSK = new LopKetNoi();
            int resultLSK = LSK.CapNhat(insertlsk, parametersLSK);

            LopKetNoi HSBA = new LopKetNoi();
            int resultHSBA = HSBA.CapNhat(inserthsba, parametersHSBA);

           



            if (result > 0)
            {

                string tenBacSi = "";
                string sqlTenBacSi = "SELECT HoTen FROM BacSi WHERE IDBacSi = @IDBacSi";
                SqlParameter[] paramTenBacSi = new SqlParameter[]
                {
        new SqlParameter("@IDBacSi", idBacSi)
                };
                LopKetNoi kbt = new LopKetNoi();
                DataTable userData = kbt.docdulieu(sqlTenBacSi, paramTenBacSi);
                if (userData != null && userData.Rows.Count > 0)
                {
                    tenBacSi = userData.Rows[0]["HoTen"].ToString();
                }
                //mailSender mailSender = new mailSender();
                //string tieude = "BANANA XIN CHÀO QUÝ KHÁCH !\n ";
                // string noidung = "Bạn đã đăng ký thành công bác sĩ\n" + idBacSi + "Giờ khám \n" + idgiokham + "Ngày\n" + idngaykham + "Bạn vui lòng đến trước giờ khám khoảng 10 phút để đề phòng những sự cố không mong muốn ! Xin cảm ơn!";
                // //mail test
                // mailSender.sendMail_CancelAppointment("rick38@ethereal.email",tieude ,noidung);

                string tieude = "BANANA Hospital – Xác nhận đăng ký lịch khám";

                string noidung = @"
<div style='background-color: #f5f5f5; padding: 20px 0; font-family: Arial, sans-serif;'>
  <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
    <div style='background-color: #006666; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
      BANANA HOSPITAL
    </div>
    <div style='padding: 30px; text-align: left;'>
      <h2 style='color: #333;'>Xác nhận đăng ký lịch khám</h2>
      <p>Xin chào <strong>Quý khách</strong>,</p>

      <p>Chúc mừng Quý khách đã đăng ký khám thành công với bác sĩ:</p>

      <ul style='list-style: none; padding-left: 0;'>
          <li>🩺 <strong>Tên bác sĩ:</strong> " + tenBacSi + @"</li>
          <li>🕒 <strong>Thời gian khám:</strong> " + idgiokham + @"</li>
          <li>📅 <strong>Ngày khám:</strong> " + idngaykham + @"</li>
          <li>📅 <strong>Phòng Khám:</strong> " + idPhongKham + @"</li>
      </ul>

      <p>Quý khách vui lòng đến trước giờ khám khoảng <strong>10 phút</strong> để đảm bảo quy trình khám bệnh được diễn ra thuận lợi và tránh những sự cố không mong muốn.</p>

      <p style='margin-top: 30px;'>Xin chân thành cảm ơn Quý khách đã tin tưởng và lựa chọn <strong>BANANA Hospital</strong>!</p>

      <p>Trân trọng,</p>
      <p><strong>Ban Quản Lý</strong><br/>BANANA HOSPITAL</p>
    </div>
  </div>
</div>";


                sendMai_gmail sendmail = new sendMai_gmail();
                sendmail.sendMail_gmail(idemail, tieude, noidung);


                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Đăng ký thành công.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Đăng ký thất bại.', 'error');", true);
            }

        }
    }
}