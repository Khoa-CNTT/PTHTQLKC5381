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
            else
            {
                string eventTarget = Request["__EVENTTARGET"];
                string eventArgument = Request["__EVENTARGUMENT"];

                if (eventTarget == "deleteNews")
                {
                    string[] args = eventArgument.Split('|');
                    string idNews = args[0];
                    deleteNews(idNews);
                    return;
                }else if (eventTarget == "closeForm")
                {
                    Closenews_click(sender,e);
                    return;
                }



            }
        }
        public void LoadData()
        {
            //DataTable dt = new DataTable();
            //dt = qlbvService.getAll();
            ds_baiviet.DataSource = qlbvService.getAll();
            ds_baiviet.DataBind();
            return;
        }

        //public void openAddNews(object sender, EventArgs e)
        //{
        //    noiDung_txt.Text="";
        //    tieude_txt.Text="";



        //}


        //public void Addnews_click(object sender, EventArgs e)
        //{
        //    string noiDung = noiDung_txt.Text;
        //    string tieuDe = tieude_txt.Text;
        //    string idContent = id_content.Value;
        //    //create date and name file
        //    DateTime date = DateTime.Now;
        //    string createDate = date.ToString("MM/dd/yyyy");
        //    //File upload
        //    string img_String = "";

        //    if (fileImg.HasFile && fileImg != null)
        //    {
        //        fileImg.SaveAs(Server.MapPath("/img/BaiViet/" + fileImg.FileName));//thêm ~ trước /img nếu không load được ảnh
        //        img_String = "/img/BaiViet/" + fileImg.FileName;//thêm ~ trước /img nếu không load được ảnh
        //    }
        //    else
        //    {
        //        img_String = imgHidden.Value + "";
        //    }
        //    int idContentInt = -1;
        //    if (idContent!= null&& idContent!="")
        //    {
        //        idContentInt = int.Parse(idContent);
        //    }
        //    //check title and des is empty
        //    if (noiDung.Length <= 0 || tieuDe.Length <= 0)
        //    {
        //        string message = "Tiêu Đề và Nội Dung Bắt Buộc Phải Có!";
        //        string script = "showAlert('" + message + "','warning');";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        //        LoadData();
        //        return;
        //    }
        //    DataTable dtTieuDe = new DataTable();
        //    dtTieuDe = qlbvService.getCLoseResult(tieuDe);
        //    bool existsNews = dtTieuDe.AsEnumerable()
        //        .Any(row => row["TieuDe"].ToString() == tieuDe);
        //    if (existsNews && idContentInt == -1 )
        //    {
        //        string message = "Đã có tiêu đề này!";
        //        string script = "showAlert('" + message + "','warning');";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        //        LoadData();
        //        return;
        //    }
        //    //check if exist ID,then just update
        //    DataTable dt = new DataTable();
        //    dt = qlbvService.getById(idContentInt);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        string scriptUpdate = updateNews(idContent, tieuDe, noiDung, img_String, createDate);
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", scriptUpdate, true);
        //        LoadData();
        //        return;
        //    }
        //    string scriptAdd = addNews(tieuDe, noiDung, img_String, createDate);
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", scriptAdd, true);
        //    LoadData();
  
        //    return;


        //}

        //close form
        public void Closenews_click(object sender, EventArgs e)
        {
            id_content.Value = "";
            create_date.Value = "";
            imageUrl.Value = "";
            //tieude_txt.Text = "";
            //noiDung_txt.Text = "";
            string script = "close_formAddNews();";
            ScriptManager.RegisterStartupScript(this, GetType(), "display", script, true);
            LoadData() ;
            return;
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
            //Button btn = (Button)sender;
            //DataListItem item = (DataListItem)btn.NamingContainer;
            //HiddenField hiddenField = (HiddenField)item.FindControl("id_Content");

            ////get id
            //string idBaiVietString = hiddenField.Value;
            ////convert to int
            //int idBaiViet = int.Parse(idBaiVietString);
            //DataTable listBaiViet = new DataTable();
            ////search news from database
            //listBaiViet = qlbvService.getById(idBaiViet);

            ////======== 2 set value to form 
            //string tieude = listBaiViet.Rows[0]["TieuDe"].ToString();
            //string noiDung = listBaiViet.Rows[0]["NoiDung"].ToString();
            //string anh = listBaiViet.Rows[0]["HinhAnh"].ToString();
            //DateTime getNgayDang = (DateTime)listBaiViet.Rows[0]["NgayDang"];
            //string ngayDang = getNgayDang.ToString("dd/MM/yyyy");

            //id_content.Value = idBaiVietString;
            //create_date.Value = ngayDang;
            //tieude_txt.Text = tieude;
            //noiDung_txt.Text = noiDung;
            //imageUrl.Value = anh;
            //imgHidden.Value = anh;
            //ClientScript.RegisterStartupScript(this.GetType(), "update", "open_formAddNews();", true);

        }

        //public void delete_News(object sender, EventArgs e)
        //{

        //    Button btn = (Button)sender;
        //    DataListItem item = (DataListItem)btn.NamingContainer;
        //    HiddenField hiddenField = (HiddenField)item.FindControl("id_Content");
        //    string idBaiVietString = hiddenField.Value;
        //    string result = deleteNews(idBaiVietString);
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", result, true);
        //    LoadData();


        //}


        //                  FUNCTION



        //      ADD
        public string addNews(string tieuDe, string noiDung, string img_String, string createDate) {

            int result = qlbvService.add(tieuDe, noiDung, img_String, createDate);
            string message = "Thêm Thất Bại";
            string script = "showAlert('" + message + "','error');";
            //check
            if (result != 0)
            {
                message = "Thêm Thành Công";
                script = "showAlert('" + message + "','success');";
                return script;
            }
            return script;
        } 

        //      UPDATE
        public string updateNews(string id, string tieuDe, string noiDung, string img_String, string createDate)
        {
            int result = qlbvService.update(id,tieuDe, noiDung, img_String, createDate);
            string message = "Cập Nhật Thất Bại";
            string script = "showAlert('" + message + "','error');";
            //check
            if (result != 0)
            {
                 message = "Cập Nhật Thành Công";
                 script = "showAlert('" + message + "','success');";
                return script;
            }
            return script;
            

        }
        //      DELETE
        public void deleteNews(string id)
        {
            int result = qlbvService.delete(id);
            string message = "Xóa Thất Bại";
            string script = "showAlert('" + message + "','error');";
            //check
            if (result != 0)
            {
                message = "Xóa Thành Công";
                script = "showAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                LoadData();
                return;

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            LoadData();
            return;
           



        }
        //===============================Function =========================================
        //public string GetFirstImage(string imageList)
        //{
        //    if (string.IsNullOrEmpty(imageList)) return string.Empty;

        //    var images = imageList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    return images.Length > 0 ? ResolveUrl(images[0]) : string.Empty;
        //}
    }
}

