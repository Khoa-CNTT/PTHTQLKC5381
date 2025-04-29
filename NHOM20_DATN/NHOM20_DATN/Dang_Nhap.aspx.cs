using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;

namespace NHOM20_DATN
{
    public partial class Dang_Nhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateCaptcha();
            }
        }
        protected void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                txtPassword.TextMode = TextBoxMode.Password;
            }
        }

        private static Random random = new Random();


        protected void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            // Sinh mã gồm 6 ký tự ngẫu nhiên
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string captcha = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // Lưu mã vào Session
            Session["Captcha"] = captcha;

            // Hiển thị mã xác nhận
            lblCaptcha.Text = captcha;
        }


        protected void btnDN_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string enteredCaptcha = txtCaptcha.Text.ToUpper(); // Lấy mã CAPTCHA người dùng nhập và chuyển thành chữ hoa
            string storedCaptcha = Session["Captcha"] as string;

            if (string.IsNullOrEmpty(storedCaptcha) || enteredCaptcha != storedCaptcha.ToUpper())
            {
                // Nếu nhập sai CAPTCHA, chỉ hiển thị lỗi, không tạo CAPTCHA mới ngay
                lblCaptchaError.Text = "Mã xác nhận không đúng! Vui lòng thử lại.";
                return;
            }

            // Nếu kiểm tra thành công, tạo CAPTCHA mới cho lần đăng nhập tiếp theo
            GenerateCaptcha();

            LopKetNoi kb = new LopKetNoi();

            // Kiểm tra tên đăng nhập và mật khẩu (đoạn code này giữ nguyên)
            string loginSql = "SELECT ID FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
            SqlParameter[] loginParams = {
        new SqlParameter("@TenDangNhap", username),
        new SqlParameter("@MatKhau", password)
    };
            DataTable dt = kb.docdulieu(loginSql, loginParams);

            if (dt.Rows.Count > 0)
            {
                string userId = dt.Rows[0]["ID"].ToString();

                // Phân quyền (đoạn code này giữ nguyên)

                // Phân quyền dựa trên ID
                if (userId.StartsWith("BN"))
                {
                    // Thiết lập Session cho bệnh nhân
                    Session["UserID"] = userId;
                    Session["Role"] = "BenhNhan";
                    Session["TenDangNhap"] = username;

                    // Gán trực tiếp vì userId đã là IDBenhNhan
                    Session["IDBenhNhan"] = userId;

                    Response.Redirect("Default.aspx");
                }


                else if (userId.StartsWith("BS") || userId.StartsWith("TK"))
                {
                    // Bác sĩ
                    Session["UserID"] = userId;
                    Session["Role"] = "BacSi";
                    Session["TenDangNhap"] = username;
                    Response.Redirect("pages/Doctor/Xem_Lich_Kham.aspx");
                    
                }
                else if (userId.StartsWith("QL"))
                {
                    // Quản lý
                    Session["UserID"] = userId;
                    Session["Role"] = "QuanLy";
                    Session["TenDangNhap"] = username;
                    Response.Redirect("pages/Manager/Quan_Ly_Bac_Si.aspx");
                }

                else if (userId.StartsWith("TV"))
                {
                    // Quản lý
                    Session["UserID"] = userId;
                    Session["Role"] = "TuVan";
                    Session["TenDangNhap"] = username;
                    Response.Redirect("~/Consultant/Tu_Van_Suc_Khoe_Ban_Dau.aspx");
                }
                else if (userId.StartsWith("BO"))
                {
                    // Quản lý
                    Session["UserID"] = userId;
                    Session["Role"] = "BacSiOn";
                    Session["TenDangNhap"] = username;
                    Response.Redirect("pages/DoctorOnline/Xem_Thong_Tin_Tu_Van.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
             "Swal.fire({ icon: 'error', title: 'Đăng nhập thất bại', text: 'Tên đăng nhập hoặc mật khẩu không đúng!' });", true);

            }
        
         }
    }
}