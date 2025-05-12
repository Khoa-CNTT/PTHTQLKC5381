using NHOM20_DATN.res.service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace NHOM20_DATN.res.handle.baiviet
{
    /// <summary>
    /// Summary description for LoadUpdateBaiVietHandler
    /// </summary>
    public class LoadUpdateBaiVietHandler : IHttpHandler
    {

        QlbvService qlbvService = new QlbvService();
        public void ProcessRequest(HttpContext context)
        {
            string mode = context.Request.QueryString["mode"];
            string id = context.Request.QueryString["id"];
            int idInt = int.Parse(id);
            // Giả sử bạn có DataTable
            string baseUrl = context.Request.Url.GetLeftPart(UriPartial.Authority);
            DataTable dt = qlbvService.getById(idInt);
            //string content = dt.Rows[0]["NoiDung"].ToString();
            string idBV = "";
            string title = "";
            string content = "";
            string img = "";

            if (dt.Rows.Count > 0)
            {
                idBV = dt.Rows[0]["IDBaiViet"].ToString();
                title = dt.Rows[0]["TieuDe"].ToString();
                content = dt.Rows[0]["NoiDung"].ToString();
                img = dt.Rows[0]["HinhAnh"].ToString();
            }
            context.Response.ContentType = "application/json";
            var json = new JavaScriptSerializer().Serialize(new { idBV = idBV, content = content, hinhanh = img, tieude = title });
            context.Response.Write(json);
        }


        public bool IsReusable => false;
    }
}