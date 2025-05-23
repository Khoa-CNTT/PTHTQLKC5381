using NHOM20_DATN.pages.Manager;
using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace NHOM20_DATN.res.handle.baiviet
{
    /// <summary>
    /// Summary description for addBaiVietHandler
    /// </summary>
    public class addBaiVietHandler : IHttpHandler
    {
        QlbvService qlbvService = new QlbvService();
        public void ProcessRequest(HttpContext context)
        {
            var files = context.Request.Files;
            var tieude = context.Request.Form["tieude"];
            NameValueCollection form = context.Request.Form;
            NameValueCollection copiedForm = new NameValueCollection(form);
            copiedForm.Remove("tieude");
                
            string log = "";
            string pathImgToDB = "";
            string img_String = "";
            // Xử lý các file
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = context.Server.MapPath("/img/BaiViet/" + fileName);
                    file.SaveAs(path);
                    img_String = "/img/BaiViet/" + fileName;
                    pathImgToDB += img_String + ",";
                    //log += $"File {i + 1}: {fileName}\n"; // Thứ tự và tên file
                }
                //save to db
            }

            // Xử lý các input text
            foreach (string key in copiedForm)
            {
                string val = form[key];
                log += $"{val}";
            }
            if (log == null|| log == "")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("fail");
                return;
            }
            DataTable dtTieuDe = new DataTable();
            dtTieuDe = qlbvService.getExactResult(tieude);
            bool existsNews = dtTieuDe.AsEnumerable()
                .Any(row => row["TieuDe"].ToString() == tieude);
            if (existsNews)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("exist");
                return;
            }
            int result = qlbvService.add(tieude, log, pathImgToDB,"");
            context.Response.ContentType = "text/plain";
            context.Response.Write("success");

            //context.Response.ContentType = "text/plain";
            //context.Response.Write(log);
        }

        public bool IsReusable => false;
    }
}