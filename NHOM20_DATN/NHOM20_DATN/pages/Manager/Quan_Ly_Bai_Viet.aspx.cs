using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Quan_Ly_Bai_Viet : System.Web.UI.Page
    {
        QlbvService qlbvService = new QlbvService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            //DataTable dt = new DataTable();
            //dt = qlbvService.getAll();
            ds_baiviet.DataSource = qlbvService.getAll();
            ds_baiviet.DataBind();
        }

        //public void openAddNews(object sender, EventArgs e)
        //{
        //    noiDung_txt.Text="";
        //    tieude_txt.Text="";



        //}


        public void Addnews_click(object sender, EventArgs e)
        {
            string noiDung = noiDung_txt.Text;
            string tieuDe = tieude_txt.Text;
            string idContent = id_content.Value;
            int idContentInt = int.Parse(idContent);
            //check title and des is empty
            if (noiDung.Length <= 0 || tieuDe.Length <= 0)
            {
                Response.Write("<script>alert('Bài viết không được trống')</script>");
                return;
            }
            //check if exist ID,then just update
            DataTable dt = new DataTable();
            dt = qlbvService.getById(idContentInt);
            if (dt.Rows.Count != 0)
            {


            }




            //create date and name file
            string filePath = "";
            DateTime date = DateTime.Now;
            string createDate = date.ToString("MM/dd/yyyy");
            // get file img
            if (fileImg.HasFile)
            {
                //get path
                string fileFolder = Server.MapPath("img/BaiViet/" + fileImg.FileName);
                filePath = "img/BaiViet/" + fileImg.FileName;
                //save to folder
                fileImg.SaveAs(fileFolder);
            }

            //console log result
            int result = qlbvService.add(tieuDe, noiDung, filePath, createDate);

            LoadData();

        }


        //Search News
        public void btn_Search_Click(object sender, EventArgs e)
        {
            string keyname = txt_Searching.Text;
            if (keyname == "")
            {
                LoadData();
                return;
            }
            DataTable listBaiViet = new DataTable();
            listBaiViet = qlbvService.getCLoseResult(keyname);
            ds_baiviet.DataSource = listBaiViet;
            ds_baiviet.DataBind();
        }

        public void edit_News(object sender, EventArgs e)
        {
            //====== 1 get value  
            Button btn = (Button)sender;
            DataListItem item = (DataListItem)btn.NamingContainer;
            HiddenField hiddenField = (HiddenField)item.FindControl("id_Content");

            //get id
            string idBaiVietString = hiddenField.Value;
            //convert to int
            int idBaiViet = int.Parse(idBaiVietString);
            DataTable listBaiViet = new DataTable();
            //search news from database
            listBaiViet = qlbvService.getById(idBaiViet);

            //======== 2 set value to form 
            string tieude = listBaiViet.Rows[0]["Caption"].ToString();
            string noiDung = listBaiViet.Rows[0]["Content"].ToString();
            string anh = listBaiViet.Rows[0]["Image"].ToString();
            DateTime getNgayDang = (DateTime)listBaiViet.Rows[0]["CreateDate"];
            string ngayDang = getNgayDang.ToString("dd/MM/yyyy");

            id_content.Value = idBaiVietString;
            create_date.Value = ngayDang;
            tieude_txt.Text = tieude;
            noiDung_txt.Text = noiDung;
            imageUrl.Value = anh;
            ClientScript.RegisterStartupScript(this.GetType(), "update", "open_formAddNews();", true);

        }

        public void delete_News(object sender, EventArgs e)
        {


        }


    }
}