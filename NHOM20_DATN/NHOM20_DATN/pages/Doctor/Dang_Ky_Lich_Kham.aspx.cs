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
        {// Xóa các giờ khám trước đó
            DDLgiokham.Items.Clear();
            // Lấy giá trị của buổi khám
            string buoiKham = ddlbuoikham.SelectedValue;

            if (buoiKham == "Sáng") // Giả sử buổi sáng có ID là 1
            {
                // Thêm các giờ khám của buổi sáng
              
                DDLgiokham.Items.Add(new ListItem("7h", "07:00:00"));
                DDLgiokham.Items.Add(new ListItem("7h30", "07:30:00"));
                DDLgiokham.Items.Add(new ListItem("8h", "08:00:00"));
                DDLgiokham.Items.Add(new ListItem("8h30", "08:30:00"));
                DDLgiokham.Items.Add(new ListItem("9h", "09:00:00"));
                DDLgiokham.Items.Add(new ListItem("9h30", "09:30:00"));
                DDLgiokham.Items.Add(new ListItem("10h", "10:00:00"));
                DDLgiokham.Items.Add(new ListItem("10h30", "10:30:00"));
            }
            else if (buoiKham == "Chiều") // Giả sử buổi chiều có ID là 2
            {
                // Thêm các giờ khám của buổi chiều
                DDLgiokham.Items.Add(new ListItem("13h30", "13:30:00"));
                DDLgiokham.Items.Add(new ListItem("14h", "14:00:00"));
                DDLgiokham.Items.Add(new ListItem("14h30", "14:30:00"));
                DDLgiokham.Items.Add(new ListItem("15h", "15:00:00"));
                DDLgiokham.Items.Add(new ListItem("15h30", "15:30:00"));
                DDLgiokham.Items.Add(new ListItem("16h", "16:00:00"));
                DDLgiokham.Items.Add(new ListItem("16h30", "16:30:00"));
            }
            else if (buoiKham == "Cả Ngày")
            {
                DDLgiokham.Items.Add(new ListItem("7h", "07:00:00"));
                DDLgiokham.Items.Add(new ListItem("7h30", "07:30:00"));
                DDLgiokham.Items.Add(new ListItem("8h", "08:00:00"));
                DDLgiokham.Items.Add(new ListItem("8h30", "08:30:00"));
                DDLgiokham.Items.Add(new ListItem("9h", "09:00:00"));
                DDLgiokham.Items.Add(new ListItem("9h30", "09:30:00"));
                DDLgiokham.Items.Add(new ListItem("10h", "10:00:00"));
                DDLgiokham.Items.Add(new ListItem("10h30", "10:30:00"));
                DDLgiokham.Items.Add(new ListItem("13h30", "13:30:00"));
                DDLgiokham.Items.Add(new ListItem("14h", "14:00:00"));
                DDLgiokham.Items.Add(new ListItem("14h30", "14:30:00"));
                DDLgiokham.Items.Add(new ListItem("15h", "15:00:00"));
                DDLgiokham.Items.Add(new ListItem("15h30", "15:30:00"));
                DDLgiokham.Items.Add(new ListItem("16h", "16:00:00"));
                DDLgiokham.Items.Add(new ListItem("16h30", "16:30:00"));
            }
            else
            {
                // Nếu không có buổi khám nào được chọn, hiển thị mặc định
                DDLgiokham.Items.Add(new ListItem("Giờ Khám", "0"));
            }
        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {

            if (Session["UserID"] != null)
            {
                if (Session["UserID"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Vui lòng đăng nhập trước khi đăng ký khám.', 'warning');", true);
                    return;
                }

                // kiểm tra các trường đã được chọn chưa nếu chưa thì sẽ thông báo


                if (string.IsNullOrEmpty(txtNgayKham.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy chọn ngày khám.', 'warning');", true);
                    return;
                }

                if (DDLgiokham.SelectedValue == "Chọn buổi khám")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Hãy chọn giờ khám.', 'warning');", true);
                    return;
                }

                if (ddlbuoikham.SelectedValue == "Chọn buổi khám")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Vui lòng chọn buổi khám.', 'warning');", true);
                    return;
                }


                string doctorID = Session["UserID"].ToString();

                // Lấy thông tin từ form

                string buoiKham = ddlbuoikham.SelectedValue;
                string idgiokham = DDLgiokham.SelectedValue;
                string ngayKham = txtNgayKham.Text;

                string checkDuplicateSql = @"
            SELECT COUNT(*) 
            FROM BuoiKham 
            WHERE IDBacSi = @IDBacSi 
            AND NgayKham = @NgayKham 
            AND ThoiGianKham = @ThoiGianKham";

                SqlParameter[] checkDuplicateParams = {
            new SqlParameter("@IDBacSi", doctorID),
            new SqlParameter("@NgayKham", ngayKham),
            new SqlParameter("@ThoiGianKham", idgiokham)
        };

                LopKetNoi checkDbb = new LopKetNoi();
                DataTable dtDuplicate = checkDbb.docdulieu(checkDuplicateSql, checkDuplicateParams);
                int duplicateCount = 0;

                if (dtDuplicate != null && dtDuplicate.Rows.Count > 0)
                {
                    duplicateCount = Convert.ToInt32(dtDuplicate.Rows[0][0]);
                }

                // Nếu đã đăng ký khám trong ngày và giờ này, không cho phép đăng ký thêm
                if (duplicateCount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Bạn đã đăng ký khung giờ này trong ngày hôm nay.', 'warning');", true);
                    return;
                }

                // Tạo câu lệnh SQL để thêm thông tin vào bảng BuoiKham
                string sql = "INSERT INTO BuoiKham (IDBacSi,Buoi, NgayKham,ThoiGianKham) " +
                             "VALUES (@IDBacSi, @Buoi,@NgayKham,@ThoiGianKham)";

                // Tạo các tham số cho câu lệnh SQL
                SqlParameter[] parameters = {
            new SqlParameter("@IDBacSi", doctorID), // ID bác sĩ từ session
            new SqlParameter("@Buoi", buoiKham),
            new SqlParameter("@NgayKham", ngayKham),
             new SqlParameter("@ThoiGianKham", idgiokham)

        };

                // Thực thi câu lệnh SQL
                LopKetNoi kn = new LopKetNoi();
                int result = kn.CapNhat(sql, parameters);

                // Kiểm tra kết quả
                if (result > 0)
                {
                   
                    // Đăng ký thành công
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Đăng ký thành công.', 'success');", true);

                    ResetForm();
                }
                else
                {
                    // Đăng ký thất bại
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert('Đăng ký thất bại.', 'error');", true);
                }
            }
            else
            {
                // Nếu không có ID bác sĩ trong session, chuyển hướng đến trang đăng nhập
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void ResetForm()
        {
            txtNgayKham.Text = "";
            ddlbuoikham.SelectedIndex = 0;
            DDLgiokham.Items.Clear();
            // Nếu bạn muốn set lại danh sách giờ theo mặc định thì có thể gọi lại ddlbuoikham_SelectedIndexChanged

            // Các textbox khác (nếu có người nhập thêm ghi chú, mô tả,...)
            // txtGhiChu.Text = "";
        }
    }
}