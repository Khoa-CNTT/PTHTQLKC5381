using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.Home_Component
{
    public partial class List_Tin_Tuc : System.Web.UI.Page
    {
        QlbvService QlbvService = new QlbvService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            loadData();
        }

        public void loadData()
        {
            DataTable dt = new DataTable();
            dt = QlbvService.getAll();
            ListNews.DataSource = dt;
            ListNews.DataBind();
        }














    }
}