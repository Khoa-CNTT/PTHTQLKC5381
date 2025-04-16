using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TenDangNhap"] != null)
                {
                    // Người dùng đã đăng nhập
                    string tenDN = Session["TenDangNhap"].ToString();
                    lnkDangNhapDangKy.InnerHtml = tenDN;
                    lnkDangNhapDangKy.HRef = "#";

                    


                    caidat.PostBackUrl = "~/Dang_Nhap.aspx"; // Không thay đổi URL khi nhấn vào
                    quanLyThongTin.Visible = true; // Hiển thị "Quản lý thông tin cá nhân"
                    btnDatLai.Visible = true;
                    dangXuat.Visible = true; // Hiển thị "Đăng xuất"

                }
                else
                {
                    // Người dùng chưa đăng nhập
                    caidat.PostBackUrl = "~/Dang_Nhap.aspx"; // Điều hướng đến trang đăng nhập
                    quanLyThongTin.Visible = false; // Ẩn "Quản lý thông tin cá nhân"
                    btnDatLai.Visible = false;
                    dangXuat.Visible = false; // Ẩn "Đăng xuất"
                }
            }


            // Kiểm tra xem người dùng đã đăng nhập hay chưa bằng cách kiểm tra giá trị trong Session["TenDangNhap"]
            //if (Session["TenDangNhap"] != null)
            //{
            //    // Nếu người dùng đã đăng nhập, thay đổi nội dung của link đăng nhập/đăng ký thành tên đăng nhập
            //    string tenDangNhap = Session["TenDangNhap"].ToString(); // Lấy tên đăng nhập từ Session

            //    // Cập nhật nội dung link với tên đăng nhập và giữ lại hình ảnh người dùng
            //    lnkDangNhapDangKy.InnerHtml = "<img style='width: 28px; padding-right: 5px;' src='img/icon_nguoidung.png' />" + tenDangNhap;

            //    // Xóa liên kết đến trang đăng nhập/đăng ký vì người dùng đã đăng nhập
            //    lnkDangNhapDangKy.HRef= "#";
            //}
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            // Xóa session và điều hướng về trang đăng nhập
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Dang_Nhap.aspx");
        
        }
    }
}