using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.MasterPage
{
    public partial class Managerment_MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if ((string)Session["Role"] == null || (string)Session["Role"] == "")
                //{
                //    Response.Redirect("/Dang_Nhap.aspx");
                //}
                //else if ((string)Session["Role"] != "QuanLy")
                //{
                //    Response.Redirect("~/Error_forbidden.html");
                //}

                //}
            }
        }
        //Đăng xuất
        protected void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Dang_Nhap.aspx");
        }
    }
}