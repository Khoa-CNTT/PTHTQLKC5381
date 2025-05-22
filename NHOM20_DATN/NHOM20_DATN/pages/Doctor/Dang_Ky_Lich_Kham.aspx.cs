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
    [Serializable]
    public class BookingEntry
    {
        public string NgayKham { get; set; }
        public string Buoi { get; set; }
        public List<string> Gio { get; set; }
    }

    public partial class Dang_Ky_Lich_Kham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Bookings"] = new List<BookingEntry>();
                ViewState["LastNgay"] = string.Empty;
                ViewState["LastBuoi"] = string.Empty;
                ViewState["SelectedHours"] = new List<string>();

                txtNgayKham.Attributes["min"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                txtNgayKham.Attributes["max"] = DateTime.Now.AddMonths(2).ToString("yyyy-MM-dd");
                if (Session["UserID"] == null)
                {
                    Response.Redirect("/Dang_Nhap.aspx");
                    return;
                }
                LoadDoctorData(Session["UserID"].ToString());
            }
        }

        private void LoadDoctorData(string doctorID)
        {
            var sql = "SELECT HoTen, Email,SoDienThoai,DiaChiPhongKham,TrinhDo FROM BacSi WHERE IDBacSi = @DoctorID";
            var parameters = new[] { new SqlParameter("@DoctorID", doctorID) };
            var kn = new LopKetNoi();
            var dt = kn.docdulieu(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                var r = dt.Rows[0];
                txtHoTen.Text = r["HoTen"].ToString();
                txtEmail.Text = r["Email"].ToString();
                txtSoDienThoai.Text = r["SoDienThoai"].ToString();
                txtDiaChi.Text = r["DiaChiPhongKham"].ToString();
                Txttrinhdo.Text = r["TrinhDo"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showAlert('Không tìm thấy thông tin bác sĩ.', 'warning');", true);
            }
        }

        protected void txtNgayKham_TextChanged(object sender, EventArgs e)
        {
            SaveCurrentSelection();

            // 2) Đặt LastNgay = ngày mới, nhưng reset LastBuoi
            ViewState["LastNgay"] = txtNgayKham.Text;
            ViewState["LastBuoi"] = string.Empty;           // <— quan trọng!
            ViewState["SelectedHours"] = new List<string>();

            // 3) reset UI: dropdown về chỉ mục đầu, xoá hết checkbox
            ddlbuoikham.SelectedIndex = 0;
            cblGiokham.Items.Clear();

        }

        protected void ddlbuoikham_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1) Lưu selection của buổi cũ (trên cùng ngày)
            SaveCurrentSelection();

            // 2) Cập nhật LastBuoi, SelectedHours
            ViewState["LastBuoi"] = ddlbuoikham.SelectedValue;
            ViewState["SelectedHours"] = new List<string>();

            // 3) build lại list giờ
            PopulateHours(ddlbuoikham.SelectedValue);

            // 4) restore những giờ đã có trong ViewState (nếu có)
            RestoreSelection();
        }
        private void PopulateHours(string buoi)
        {
            cblGiokham.Items.Clear();
            var hours = new List<(string Text, string Value)>();
            if (buoi == "Sáng" || buoi == "Cả Ngày")
                hours.AddRange(new[]{
            ("7h","07:00:00"),("7h30","07:30:00"),("8h","08:00:00"),
            ("8h30","08:30:00"),("9h","09:00:00"),("9h30","09:30:00"),
            ("10h","10:00:00"),("10h30","10:30:00")
        });
            if (buoi == "Chiều" || buoi == "Cả Ngày")
                hours.AddRange(new[]{
            ("13h30","13:30:00"),("14h","14:00:00"),
            ("14h30","14:30:00"),("15h","15:00:00"),
            ("15h30","15:30:00"),("16h","16:00:00"),
            ("16h30","16:30:00")
        });
            if (!hours.Any())
            {
                cblGiokham.Items.Add(new ListItem("Chưa chọn buổi khám", "0"));
            }
            else
            {
                foreach (var h in hours)
                    cblGiokham.Items.Add(new ListItem(h.Text, h.Value));
            }
        }
        protected void cblGiokham_SelectedIndexChanged(object sender, EventArgs e)
        {
            var prev = ViewState["SelectedHours"] as List<string>;
            foreach (ListItem it in cblGiokham.Items)
            {
                if (it.Selected && !prev.Contains(it.Value)) prev.Add(it.Value);
                if (!it.Selected && prev.Contains(it.Value)) prev.Remove(it.Value);
            }
            ViewState["SelectedHours"] = prev;
        }

        private void SaveCurrentSelection()
        {
            var lastNgay = ViewState["LastNgay"] as string;
            var lastBuoi = ViewState["LastBuoi"] as string;
            var hours = ViewState["SelectedHours"] as List<string>;

            if (string.IsNullOrEmpty(lastNgay) || string.IsNullOrEmpty(lastBuoi))
                return;
            var list = ViewState["Bookings"] as List<BookingEntry>;
            list.RemoveAll(b => b.NgayKham == lastNgay && b.Buoi == lastBuoi);
            if (hours != null && hours.Any())
            {
                list.Add(new BookingEntry
                {
                    NgayKham = lastNgay,
                    Buoi = lastBuoi,
                    Gio = new List<string>(hours)
                });
            }

            ViewState["Bookings"] = list;
        }


        private void RestoreSelection()
        {
            var list = ViewState["Bookings"] as List<BookingEntry>;
            var currentDate = txtNgayKham.Text;
            var currentBuoi = ddlbuoikham.SelectedValue;
            if (list == null || string.IsNullOrEmpty(currentDate) || string.IsNullOrEmpty(currentBuoi))
                return;

            var existing = list.FirstOrDefault(b => b.NgayKham == currentDate && b.Buoi == currentBuoi);
            if (existing != null)
            {
                // 1) (Đã có PopulateHours rồi)
                // 2) chọn lại các checkbox
                foreach (ListItem it in cblGiokham.Items)
                    it.Selected = existing.Gio.Contains(it.Value);

                // 3) cập nhật ViewState
                ViewState["SelectedHours"] = new List<string>(existing.Gio);
            }
        }
        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) { showWarning("Vui lòng đăng nhập."); return; }
            SaveCurrentSelection();
            var bookings = ViewState["Bookings"] as List<BookingEntry>;
            if (bookings == null || !bookings.Any())
            {
                showWarning("Chưa có lịch nào để đăng ký.");
                return;
            }

            string doctorID = Session["UserID"].ToString();
            int inserted = 0, skipped = 0;
            var kn = new LopKetNoi();

            foreach (var b in bookings)
            {
                foreach (var t in b.Gio)
                {
                    var checkSql = "SELECT COUNT(*) FROM BuoiKham WHERE IDBacSi=@IDBacSi AND NgayKham=@NgayKham AND ThoiGianKham=@ThoiGianKham";
                    var ps = new[] {
                        new SqlParameter("@IDBacSi", doctorID),
                        new SqlParameter("@NgayKham", b.NgayKham),
                        new SqlParameter("@ThoiGianKham", t)
                    };
                    var dt = kn.docdulieu(checkSql, ps);
                    int cnt = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
                    if (cnt > 0) { skipped++; continue; }

                    var insSql = "INSERT INTO BuoiKham (IDBacSi,Buoi,NgayKham,ThoiGianKham) VALUES(@IDBacSi,@Buoi,@NgayKham,@ThoiGianKham)";
                    var insPs = new[] {
                        new SqlParameter("@IDBacSi", doctorID),
                        new SqlParameter("@Buoi", b.Buoi),
                        new SqlParameter("@NgayKham", b.NgayKham),
                        new SqlParameter("@ThoiGianKham", t)
                    };
                    if (kn.CapNhat(insSql, insPs) > 0) inserted++;
                }
            }

            if (inserted > 0)
            {
                var msg = $"Đã đăng ký {inserted} khung giờ.";
                if (skipped > 0) msg += $" ({skipped} khung giờ đã tồn tại.)";
                showSuccess(msg);
            }
            else showWarning("Khung giờ đã đăng ký.");

            // Reset toàn bộ sau khi đăng ký
            ViewState["Bookings"] = new List<BookingEntry>();
            ViewState["LastNgay"] = ViewState["LastBuoi"] = string.Empty;
            ViewState["SelectedHours"] = new List<string>();
            txtNgayKham.Text = string.Empty;
            ddlbuoikham.SelectedIndex = 0;
            cblGiokham.Items.Clear();
            ResetForm();
        }

        private void showWarning(string t) => ScriptManager.RegisterStartupScript(this, GetType(), "warn", $"showAlert('{t}','warning');", true);
        private void showSuccess(string t) => ScriptManager.RegisterStartupScript(this, GetType(), "succ", $"showAlert('{t}','success');", true);

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