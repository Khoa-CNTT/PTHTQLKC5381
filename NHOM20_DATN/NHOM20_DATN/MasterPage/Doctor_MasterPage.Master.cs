using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.MasterPage
{
    public partial class Doctor_MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["UserID"] = "BS8ED6DB63";
            //Session["Role"] = "BacSi";
            //Session["TenDangNhap"] = "Nguyenx văn việt";
            if (!IsPostBack)
            {
                if ((string)Session["Role"] == null || (string)Session["Role"] == "")
                {
                    Response.Redirect("~/Dang_Nhap.aspx");

                }
                else if ((string)Session["Role"] != "BacSi")
                {
                    Response.Redirect("~/Error_forbidden.html");
                }
                string name = (string)Session["TenDangNhap"];
                doctor_Name.InnerText = name;
            }
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