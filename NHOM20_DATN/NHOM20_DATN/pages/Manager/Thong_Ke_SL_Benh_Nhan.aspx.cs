using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Thong_Ke_SL_Benh_Nhan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //loadChart();
            

        }

        //public void loadChart()
        //{
        //    var data = new Dictionary<string, (int Nam, int Nu)>
        //{
        //    { "Tháng 1", (20, 25) },
        //    { "Tháng 2", (30, 28) },
        //    { "Tháng 3", (15, 22) },
        //    { "Tháng 4", (25, 30) }
        //};

        //    foreach (var item in data)
        //    {
        //        Chart1.Series["Nam"].Points.AddXY(item.Key, item.Value.Nam);
        //        Chart1.Series["Nữ"].Points.AddXY(item.Key, item.Value.Nu);
        //    }




        //}





    }
}