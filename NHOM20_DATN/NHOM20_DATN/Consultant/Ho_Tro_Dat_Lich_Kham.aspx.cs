using NHOM20_DATN.sendMail;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.Consultant
{
    public partial class Ho_Tro_Dat_Lich_Kham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (Session["UserID"] != null)
                {
                    string userID = Session["UserID"].ToString();
                    BindNgayKhamRepeater();
                    txtNgayKham.Attributes["min"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                    LoadChuyenKhoa();
                }
                else
                {
                    Response.Redirect("/Dang_Nhap.aspx");
                }
                
            }
        }
        protected void btnTimKiemBN_Click(object sender, EventArgs e)
        {
            string emailBenhNhan = txtemail1.Text.Trim();

            if (string.IsNullOrEmpty(emailBenhNhan))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng nhập email bệnh nhân', 'warning');", true);
                return;
            }

            // Lưu ID bệnh nhân vào ViewState để sử dụng sau này
            ViewState["CurrentPatientEmail"] = emailBenhNhan;

            // Truy vấn thông tin bệnh nhân từ database
            string sql = "SELECT * FROM BenhNhan WHERE Email = @Email";
            SqlParameter[] parameters = {
        new SqlParameter("@Email", emailBenhNhan)
    };

            LopKetNoi kn = new LopKetNoi();
            DataTable dt = kn.docdulieu(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // Điền thông tin vào các control
                txtHoTen.Text = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString() : "";
                txtEmail.Text = row["Email"] != DBNull.Value ? row["Email"].ToString() : "";
                txtNgaySinh.Text = row["NgaySinh"] != DBNull.Value ?
                    Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd") : "";
                txtSoDienThoai.Text = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : "";

                // Xử lý giới tính
                string gioiTinh = row["GioiTinh"] != DBNull.Value ? row["GioiTinh"].ToString().Trim() : "";
                if (!string.IsNullOrEmpty(gioiTinh))
                {
                    gtRadioList.SelectedValue = gioiTinh;
                }

                txtCCCD.Text = row["CanCuocCongDan"] != DBNull.Value ? row["CanCuocCongDan"].ToString() : "";
                txtDiaChi.Text = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : "";

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Đã tìm thấy thông tin bệnh nhân', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Không tìm thấy bệnh nhân với email này', 'error');", true);

                // Xóa thông tin nếu không tìm thấy
                ClearPatientInfo();
            }
        }
        private void ClearPatientInfo()
        {
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtNgaySinh.Text = "";
            txtSoDienThoai.Text = "";
            gtRadioList.ClearSelection();
            txtCCCD.Text = "";
            txtDiaChi.Text = "";
        }
        public class NgayKhamInfo
        {
            public string NgayValue { get; set; }
            public string NgayThang { get; set; }
            public string Thu { get; set; }
            public string ActiveClass { get; set; }
        }
        private void BindNgayKhamRepeater()
        {
            string[] dayOfWeekVN = {
        "Chủ nhật", "Thứ hai", "Thứ ba",
        "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"
    };

            DateTime today = DateTime.Today;
            var listDates = new List<NgayKhamInfo>();

            for (int i = 1; i <= 5; i++)
            {
                DateTime d = today.AddDays(i);
                listDates.Add(new NgayKhamInfo
                {
                    NgayValue = d.ToString("yyyy-MM-dd"),
                    NgayThang = d.ToString("dd/MM"),
                    Thu = dayOfWeekVN[(int)d.DayOfWeek],
                    ActiveClass = ""
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Không có bác sĩ nào trong ngày khám này.', 'warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Không có bác sĩ nào trong ngày khám này.', 'warning');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Không có bác sĩ nào trong ngày khám này.', 'warning');", true);
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
            if (string.IsNullOrEmpty(txtemail1.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng tìm kiếm bệnh nhân trước khi đăng ký', 'warning');", true);
                return;
            }
            if (ddlChuyenKhoa.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng chọn chuyên khoa', 'warning');", true);
                return;
            }

            if (ddlPhongKham.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng chọn Phòng khám', 'warning');", true);
                return;
            }

            if (ddlBacSi.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng chọn bác sĩ', 'warning');", true);
                return;
            }

            if (string.IsNullOrEmpty(txtNgayKham.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng chọn ngày khám', 'warning');", true);
                return;
            }

            if (ddlbuoikham.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng chọn buổi khám', 'warning');", true);
                return;
            }


            if (string.IsNullOrEmpty(txtTrieuChung.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Vui lòng nhập triệu chứng', 'warning');", true);
                return;
            }


            string emailBenhNhan = ViewState["CurrentPatientEmail"] != null ?
                ViewState["CurrentPatientEmail"].ToString() :
                txtEmail.Text.Trim();

            // Kiểm tra xem Email bệnh nhân có tồn tại không
            string getPatientIdSql = "SELECT IDBenhNhan FROM BenhNhan WHERE Email = @Email";
            SqlParameter[] getPatientIdParams = {
        new SqlParameter("@Email", emailBenhNhan)
    };

            LopKetNoi checkPatientDb = new LopKetNoi();
            DataTable dtPatientId = checkPatientDb.docdulieu(getPatientIdSql, getPatientIdParams);

            if (dtPatientId == null || dtPatientId.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "showAlert('Bệnh nhân không tồn tại, vui lòng kiểm tra lại email', 'error');", true);
                return;
            }
            string idBenhNhan = dtPatientId.Rows[0]["IDBenhNhan"].ToString();

            // Phần còn lại của phương thức giữ nguyên, nhưng thay idBenhNhan bằng giá trị từ txtMaBenhNhan
            string idBacSi = ddlBacSi.SelectedValue;
            string idPhongKham = ddlPhongKham.SelectedValue;
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
            string checkDuplicateSql = @"
                SELECT 
                    COUNT(*) as SoLuong,
                    MAX(CASE WHEN TrangThai = 'DaDangKy' THEN 1 ELSE 0 END) as CoLichDangKy,
                    MAX(CASE WHEN TrangThai = 'DaHuy' THEN 1 ELSE 0 END) as CoLichDaHuy
                FROM LichKhamBenhNhan 
                WHERE IDBenhNhan = @IDBenhNhan 
                AND CAST(NgayKham AS DATE) = CAST(@NgayKham AS DATE)";

            SqlParameter[] checkDuplicateParams = {
                new SqlParameter("@IDBenhNhan", idBenhNhan),
                new SqlParameter("@NgayKham", idngaykham)
            };

            LopKetNoi checkDbb = new LopKetNoi();
            DataTable dtDuplicate = checkDbb.docdulieu(checkDuplicateSql, checkDuplicateParams);

            if (dtDuplicate != null && dtDuplicate.Rows.Count > 0)
            {

                int soLuong = Convert.ToInt32(dtDuplicate.Rows[0]["SoLuong"]);

                if (soLuong > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "Swal.fire({ " +
                        "title: 'Đã đăng ký', " +
                        "text: 'Bệnh nhân đã đăng ký khám trong ngày này. Mỗi bệnh nhân chỉ được đăng ký một lần mỗi ngày.', " +
                        "icon: 'warning', " +
                        "confirmButtonText: 'OK' " +
                        "});", true);
                    return;
                }
                bool coLichDangKy = dtDuplicate.Rows[0]["CoLichDangKy"] != DBNull.Value && Convert.ToInt32(dtDuplicate.Rows[0]["CoLichDangKy"]) == 1;
                bool coLichDaHuy = dtDuplicate.Rows[0]["CoLichDaHuy"] != DBNull.Value && Convert.ToInt32(dtDuplicate.Rows[0]["CoLichDaHuy"]) == 1;

                if (coLichDangKy)
                {
                    // Lấy thông tin lịch khám hiện có
                    string existingAppointmentSql = @"
                    SELECT TOP 1 
                        b.HoTen as TenBacSi,
                        p.TenPhongKham,
                        lk.NgayKham,
                        lk.ThoiGianKham
                    FROM LichKhamBenhNhan lk
                    JOIN BacSi b ON lk.IDBacSi = b.IDBacSi
                    JOIN PhongKham p ON lk.SoPhongKham = p.IDPhongKham
                    WHERE lk.IDBenhNhan = @IDBenhNhan
                    AND CAST(lk.NgayKham AS DATE) = CAST(@NgayKham AS DATE)
                    AND lk.TrangThai = 'DaDangKy'";

                    SqlParameter[] existingAppointmentParams = {
            new SqlParameter("@IDBenhNhan", idBenhNhan),
            new SqlParameter("@NgayKham", idngaykham)
        };

                    DataTable dtExisting = checkDbb.docdulieu(existingAppointmentSql, existingAppointmentParams);

                    if (dtExisting != null && dtExisting.Rows.Count > 0)
                    {
                        string existingInfo =
                            $"Bác sĩ: {dtExisting.Rows[0]["TenBacSi"]}<br/>" +
                            $"Phòng khám: {dtExisting.Rows[0]["TenPhongKham"]}<br/>" +
                            $"Thời gian: {Convert.ToDateTime(dtExisting.Rows[0]["ThoiGianKham"]).ToString("HH:mm")}";

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                            $"Swal.fire({{ " +
                            $"title: 'Bệnh nhân đã có lịch khám', " +
                            $"html: 'Bệnh nhân đã có lịch khám trong ngày này:<br/><br/>{existingInfo}<br/><br/>Vui lòng chọn ngày khác hoặc hủy lịch khám hiện tại trước khi đăng ký mới.', " +
                            $"icon: 'warning', " +
                            $"confirmButtonText: 'OK' " +
                            $"}});", true);
                        return;
                    }
                }
                else if (coLichDaHuy)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "Swal.fire({ " +
                        "title: 'Lịch khám đã hủy', " +
                        "text: 'Bệnh nhân đã có lịch khám trong ngày này nhưng đã hủy. Bạn có muốn đăng ký lại không?', " +
                        "icon: 'question', " +
                        "showCancelButton: true, " +
                        "confirmButtonText: 'Đăng ký lại', " +
                        "cancelButtonText: 'Hủy bỏ' " +
                        "}).then((result) => { " +
                        "if (result.isConfirmed) { " +
                        "__doPostBack('" + btnDangKy.UniqueID + "', 'Continue'); " +
                        "} " +
                        "});", true);
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


                string insertSql = "INSERT INTO PhieuKham (IDPhieu, IDBenhNhan, IDBacSi, IDPhongKham, IDChuyenKhoa, HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, NgayKham, ThoiGianKham, TrieuChung,  IDBuoi) " +
                           "VALUES (@IDPhieu, @IDBenhNhan, @IDBacSi, @IDPhongKham, @IDChuyenKhoa, @HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi, @NgayKham, @ThoiGianKham, @TrieuChung, @IDBuoi)";
                string inserthsba = "INSERT INTO HoSoBenhAn (IDBS,IDBN) " +
                         "VALUES (@IDBacSi,@IDBenhNhan)";

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

                    // Gửi email xác nhận
                    string tieude = "BANANA Hospital – Xác nhận đăng ký lịch khám";
                    string noidung = "Kính chào Quý khách,\n\n" +
                        "Quý khách đã đăng ký khám thành công với bác sĩ:\n\n" +
                        "🩺 Tên bác sĩ: " + tenBacSi + "\n" +
                        "🕒 Thời gian khám: " + idgiokham + "\n" +
                        "📅 Ngày khám: " + idngaykham + "\n\n" +
                        "Quý khách vui lòng đến trước giờ khám khoảng 10 phút để đảm bảo quy trình khám bệnh được diễn ra thuận lợi và tránh những sự cố không mong muốn.\n\n" +
                        "Xin chân thành cảm ơn Quý khách đã tin tưởng và lựa chọn Banana Hospital!\n\n" +
                        "Trân trọng,\n" +
                        "Ban Quản Lý\n" +
                        "BANANA HOSPITAL";

                    sendMai_gmail sendmail = new sendMai_gmail();
                    sendmail.sendMail_gmail(idemail, tieude, noidung);

                    // Hiển thị thông báo chi tiết hơn
                    string successMessage =
                        "Thông tin lịch hẹn:<br/>" +
                        "Mã bệnh nhân: " + idBenhNhan + "<br/>" +
                        "Bác sĩ: " + tenBacSi + "<br/>" +
                        "Ngày khám: " + Convert.ToDateTime(idngaykham).ToString("dd/MM/yyyy") + "<br/>" +
                        "Giờ khám: " + idgiokham + "<br/>" +
                        "Phòng khám: " + ddlPhongKham.SelectedItem.Text;

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "Swal.fire({ " +
                        "title: 'Đăng ký thành công', " +
                        "html: '" + successMessage.Replace("'", "\\'") + "', " +
                        "icon: 'success', " +
                        "confirmButtonText: 'OK' " +
                        "});", true);

                    // Xóa form sau khi đăng ký thành công 
                    ClearFormAfterSuccess();


                }

            }
        }
        private void ClearFormAfterSuccess()
        {
            // Giữ lại thông tin bệnh nhân, chỉ xóa các lựa chọn đặt lịch
            ddlChuyenKhoa.SelectedIndex = 0;
            ddlPhongKham.Items.Clear();
            ddlBacSi.Items.Clear();
            ddlbuoikham.Items.Clear();
            DDLgiokham.Items.Clear();
            txtNgayKham.Text = "";
            txtTrieuChung.Text = "";

            // Load lại danh sách chuyên khoa
            LoadChuyenKhoa();
        }
    }
}