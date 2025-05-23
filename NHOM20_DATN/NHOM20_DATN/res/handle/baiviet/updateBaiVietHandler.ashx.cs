using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.handle.baiviet
{
    /// <summary>
    /// Summary description for updateBaiVietHandler
    /// </summary>
    public class updateBaiVietHandler : IHttpHandler
    {
        QlbvService qlbvService = new QlbvService();
        public void ProcessRequest(HttpContext context)
        {
          
            string id = context.Request.Form["id"];
            var files = context.Request.Files;
            var tieude = context.Request.Form["tieude"];
            NameValueCollection form = context.Request.Form;
            NameValueCollection copiedForm = new NameValueCollection(form);
            copiedForm.Remove("tieude");
            copiedForm.Remove("id");
            copiedForm.Remove("inputCount");
            string log = "";
            string pathImgToDB = "";
            string img_String = "";
            int inputCount = 0;
            
            int.TryParse(context.Request.Form["inputCount"], out inputCount);

            // Xử lý các file

            for (int i = 0; i < inputCount; i++)
            {
                HttpPostedFile file = files["file" + i];

                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = context.Server.MapPath("/img/BaiViet/" + fileName);
                    file.SaveAs(path);
                    img_String = "/img/BaiViet/" + fileName;
                    pathImgToDB += img_String + ",";
                }
                else
                {
                    // Nếu không có file, lấy từ span/text (gửi dưới dạng (filenameX))
                    string fileKey = "filename" + i + "";
                    string rawFilename = context.Request.Form[fileKey];

                    if (!string.IsNullOrEmpty(rawFilename))
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(rawFilename, @"\(\[image\d+\]\)(.*?)\(\[image\d+\]\)");
                        if (match.Success)
                        {
                            string extractedFileName = match.Groups[1].Value;
                            
                            img_String = "/img/BaiViet/" + extractedFileName;
                            pathImgToDB += img_String + ",";
                        }
                    }


                }
            }

            // Xử lý các input text
            foreach (string key in copiedForm)
            {
                string val = form[key];
                log += $"{val}";
            }
            if (log == null || log == "")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("fail");
                return;
            }
            //DataTable dtTieuDe = new DataTable();

            //bool existsNews = dtTieuDe.AsEnumerable()
            //    .Any(row => row["TieuDe"].ToString() == tieude);
            //if (existsNews)
            //{
            //    context.Response.ContentType = "text/plain";
            //    context.Response.Write("exist");
            //    return;
            //}

          

            int result = qlbvService.update(id,tieude, log, pathImgToDB, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            context.Response.ContentType = "text/plain";
            
            context.Response.Write("success");

        }

        public bool IsReusable => false;
    }
}