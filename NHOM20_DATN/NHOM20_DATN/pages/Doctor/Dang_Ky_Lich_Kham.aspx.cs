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
    public partial class Dang_Ky_Lich_Kham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Chỉ tải thông tin bác sĩ khi lần đầu tiên tải trang
            {
                ViewState["SelectedHours"] = new List<string>();
                txtNgayKham.Attributes["min"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                if (Session["UserID"] != null)
                {
                    string doctorID = Session["UserID"].ToString();
                    LoadDoctorData(doctorID); // Gọi hàm để tải thông tin bác sĩ
                }
                else
                {
                    Response.Redirect("/Dang_Nhap.aspx"); // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
                }
            }
        }
        private void LoadDoctorData(string doctorID)
        {

            string sql = "SELECT HoTen, Email,SoDienThoai,DiaChiPhongKham,TrinhDo FROM BacSi WHERE IDBacSi = @DoctorID";
            SqlParameter[] parameters = {
        new SqlParameter("@DoctorID", doctorID)
    };

            LopKetNoi kn = new LopKetNoi();
            DataTable doctorData = kn.docdulieu(sql, parameters);
            txtEmail.ReadOnly = true; // không cho bệnh nhân chỉnh sửa khi load dữ liệu lên
            txtHoTen.ReadOnly = true;
            txtSoDienThoai.ReadOnly = true;
            Txttrinhdo.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            if (doctorData.Rows.Count > 0)
            {
                DataRow row = doctorData.Rows[0];


                txtHoTen.Text = row["HoTen"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtSoDienThoai.Text = row["SoDienThoai"].ToString();
                txtDiaChi.Text = row["DiaChiPhongKham"].ToString();
                Txttrinhdo.Text = row["TrinhDo"].ToString();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Không tìm thấy thông tin bác sĩ.', 'warning');", true);
            }
        }
        protected void ddlbuoikham_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Đọc các giờ đang chọn trước đó
            bool justReset = ViewState["FormJustReset"] != null && (bool)ViewState["FormJustReset"];
            if (justReset)
            {
                ViewState["FormJustReset"] = false; // reset flag
            }
            else
            {
                var prev = ViewState["SelectedHours"] as List<string> ?? new List<string>();
                foreach (ListItem item in cblGiokham.Items)
                {
                    if (item.Selected && !prev.Contains(item.Value))
                        prev.Add(item.Value);
                }
                ViewState["SelectedHours"] = prev;
            }

            // 2. Xóa hết và thêm lại theo buổi khám
            cblGiokham.Items.Clear();
            string buoi = ddlbuoikham.SelectedValue;
            List<(string Text, string Value)> hours = new List<(string Text, string Value)>();

            if (buoi == "Sáng" || buoi == "Cả Ngày")
            {
                hours.AddRange(new[]
                {
            ("7h", "07:00:00"),
            ("7h30", "07:30:00"),
            ("8h", "08:00:00"),
            ("8h30", "08:30:00"),
            ("9h", "09:00:00"),
            ("9h30", "09:30:00"),
            ("10h", "10:00:00"),
            ("10h30", "10:30:00")
        });
            }
            if (buoi == "Chiều" || buoi == "Cả Ngày")
            {
                hours.AddRange(new[]
                {
            ("13h30", "13:30:00"),
            ("14h", "14:00:00"),
            ("14h30", "14:30:00"),
            ("15h", "15:00:00"),
            ("15h30", "15:30:00"),
            ("16h", "16:00:00"),
            ("16h30", "16:30:00")
        });
            }
            if (hours.Count == 0)
            {
                cblGiokham.Items.Add(new ListItem("Chưa chọn buổi khám", "0"));
                return;
            }

            foreach (var (text, val) in hours)
                cblGiokham.Items.Add(new ListItem(text, val));

            // 3. Khôi phục chọn trước đó
            if (!justReset)
            {
                foreach (ListItem item in cblGiokham.Items)
                {
                    if (ViewState["SelectedHours"] is List<string> prev && prev.Contains(item.Value))
                        item.Selected = true;
                }
            }
        }
        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra login
            if (Session["UserID"] == null)
            {
                showWarning("Vui lòng đăng nhập trước khi đăng ký khám.");
                return;
            }

            // 2. Kiểm tra ngày + buổi
            if (string.IsNullOrEmpty(txtNgayKham.Text))
            {
                showWarning("Hãy chọn ngày khám.");
                return;
            }
            if (ddlbuoikham.SelectedValue == "Chọn buổi khám")
            {
                showWarning("Vui lòng chọn buổi khám.");
                return;
            }

            // 3. Lấy tất cả khung giờ từ ViewState (đã bao gồm cả buooi sang  + buoi chieu va ca ngay)
            var persisted = ViewState["SelectedHours"] as List<string> ?? new List<string>();
            // Và cập nhật những tick cuối trên 
            foreach (ListItem it in cblGiokham.Items)
            {
                if (it.Selected && !persisted.Contains(it.Value))
                    persisted.Add(it.Value);
            }
            // neu chua chon gio thi thong bao
            // any la phuong thuc kiem tra trong c#
            if (!persisted.Any())
            {
                showWarning("Hãy chọn ít nhất một khung giờ khám.");
                return;
            }

            string doctorID = Session["UserID"].ToString();
            string buoiKham = ddlbuoikham.SelectedValue;
            string ngayKham = txtNgayKham.Text;

            var kn = new LopKetNoi();
            int insertedCount = 0;
            int alreadyCount = 0;

            // 4. Duyệt từng giờ trong persisted để insert
            foreach (var time in persisted)
            {
                // kiểm tra duplicate
                string checkSql = @"
            SELECT COUNT(*) 
            FROM BuoiKham 
            WHERE IDBacSi = @IDBacSi 
              AND NgayKham = @NgayKham 
              AND ThoiGianKham = @ThoiGianKham";
                SqlParameter[] checkParams = {
            new SqlParameter("@IDBacSi", doctorID),
            new SqlParameter("@NgayKham", ngayKham),
            new SqlParameter("@ThoiGianKham", time)
        };
                var dt = kn.docdulieu(checkSql, checkParams);
                int cnt = (dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0][0]) : 0;

                if (cnt > 0)
                {
                    alreadyCount++;
                    continue;
                }

                // insert mới và kiểm tra khung giờ đã được đăng ký chưa
                string insertSql = @"
            INSERT INTO BuoiKham (IDBacSi, Buoi, NgayKham, ThoiGianKham)
            VALUES (@IDBacSi, @Buoi, @NgayKham, @ThoiGianKham)";
                SqlParameter[] insParams = {
            new SqlParameter("@IDBacSi", doctorID),
            new SqlParameter("@Buoi", buoiKham),
            new SqlParameter("@NgayKham", ngayKham),
            new SqlParameter("@ThoiGianKham", time)
        };
                if (kn.CapNhat(insertSql, insParams) > 0)
                    insertedCount++;
            }

            // 5. Thông báo kết quả 
            if (insertedCount > 0)
            {
                var msg = $"Đăng ký thành công khung giờ.";
                if (alreadyCount > 0)
                    msg += $" ({alreadyCount} khung giờ đã tồn tại và được bỏ qua.)";
                showSuccess(msg);
                ResetForm();
            }
            else
            {
                showWarning("Khung giờ đã được đăng ký .");
            }

            // cập nhật lại ViewState
            ViewState["SelectedHours"] = persisted;
        }

        // Các hàm phụ để gọi SweetAlert cho gọn
        private void showWarning(string text)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"showAlert('{text}', 'warning');", true);
        }
        private void showSuccess(string text)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"showAlert('{text}', 'success');", true);
        }

        private void ResetForm()
        {
            txtNgayKham.Text = "";
            ddlbuoikham.SelectedIndex = 0;
            cblGiokham.Items.Clear();
            ViewState["SelectedHours"] = new List<string>();
            ViewState["FormJustReset"] = true;
        }
    }
}