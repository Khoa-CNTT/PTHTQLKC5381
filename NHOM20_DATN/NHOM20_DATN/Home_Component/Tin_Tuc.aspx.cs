using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            string[] imagePaths = anh.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder htmlBuilder = new StringBuilder();

            // Regex lấy cả đoạn text hoặc ảnh
            string pattern = @"\(\[Start(\d+)\]\)(.*?)\(\[End\1\]\)|\(\[image(\d+)\]\)(.*?)\(\[image\3\]\)";
            var matches = Regex.Matches(noiDung, pattern, RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                if (match.Groups[2].Success) // check content
                {
                    string content = match.Groups[2].Value.Trim();
                    htmlBuilder.Append($"<p>{Server.HtmlEncode(content).Replace("\n", "<br />")}</p>");
                }
                else if (match.Groups[4].Success) // img
                {
                    string imageFile = match.Groups[4].Value.Trim();
                    string matchedPath = imagePaths.FirstOrDefault(p => p.EndsWith(imageFile));
                    if (!string.IsNullOrEmpty(matchedPath))
                    {
                        htmlBuilder.Append($"<div class='detail_img'> <img src='{matchedPath}' id='imgContent' alt=''></div>");
                    }
                }
            }



            
            title_txt.InnerText = tieude;
            date_txt.InnerText = ngayDang;
            noiDung_literal.Text = htmlBuilder.ToString();

            // head image of content
            if (imagePaths.Length > 0)
            {
                imgContent.Attributes["src"] = imagePaths[0];
            }

        }
    }
}