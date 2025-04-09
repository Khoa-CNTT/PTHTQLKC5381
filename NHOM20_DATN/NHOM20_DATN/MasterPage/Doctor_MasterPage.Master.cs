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
            if (!IsPostBack)
            {
                if ((string)Session["Role"] == null || (string)Session["Role"] == "")
                {
                    Response.Redirect("../../Dang_Nhap.aspx");
                }
                else if ((string)Session["Role"] != "BacSi")
                {
                    Response.Redirect("../../Error_forbidden.html");
                }

            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            // Xóa session và điều hướng về trang đăng nhập
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Dang_Nhap.aspx");
        }


    }
}