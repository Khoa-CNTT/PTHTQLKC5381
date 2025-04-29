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
    public partial class Tin_Tuc : System.Web.UI.Page
    {
        QlbvService qlbvService = new QlbvService();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string maBVstring = Request.QueryString["maBV"] + "";
            if (maBVstring == "") return;
            int maBV = int.Parse(maBVstring);
            loadData(maBV);
        }
        public void loadData(int maBV)
        {
            DataTable tableBaiViet = new DataTable();
            tableBaiViet = qlbvService.getById(maBV);

            //convert data from db to row  
            string tieude = tableBaiViet.Rows[0]["TieuDe"].ToString();
            string noiDung = tableBaiViet.Rows[0]["NoiDung"].ToString();
            string anh = tableBaiViet.Rows[0]["HinhAnh"].ToString();
            DateTime getNgayDang = (DateTime)tableBaiViet.Rows[0]["NgayDang"];
            string ngayDang = getNgayDang.ToString("dd/MM/yyyy");

            string[] lines = noiDung.Split(new[] { "\n" }, StringSplitOptions.None);


            string result = "";
            foreach (string line in lines)
            {
                result += $"<p>{line.Trim()}</p>"; // Thêm thẻ <p> và loại bỏ khoảng trắng
            }

            //Display content with tag <p>
            date_txt.InnerText = ngayDang;
            title_txt.InnerText = tieude;
            noiDung_literal.Text = result;
            imgContent.Attributes["src"] = anh;


        }
    }
}