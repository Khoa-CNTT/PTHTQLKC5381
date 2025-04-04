using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service
{
    public class QlbvService
    {

        LopKetNoi db = new LopKetNoi();
        public QlbvService() { }
        public int add(string Caption, string Content, string Image, string CreateDate)
        {
            string add_query = "insert into BaiVietSK(Caption, Content, Image, CreateDate) values (@Caption, @Content,@Image,@CreateDate)";
            //Truyen tham so
            SqlParameter[] pr_add = {
            new SqlParameter("@Content", Content),
            new SqlParameter("@Caption", Caption),
            new SqlParameter("@Image", Image),
            new SqlParameter("@CreateDate", CreateDate)
        };
            int result_TK = db.CapNhat(add_query, pr_add);
            return result_TK;

        }

        public void update(string idContent, string Caption, string Content, string Image, string CreateDate)
        {
            string update_query = "UPDATE BaiVietSK" +
            " SET Content=  @Content " +
            " ,Caption = @Caption" +
            " , Image =@Image" +
            " , CreateDate = @CreateDate" +
            " WHERE ID_Content = @idContent " +
            " and Caption = @Caption";
            //Truyen tham so
            SqlParameter[] pr_update = {
            new SqlParameter("@Content", Content),
            new SqlParameter("@Caption", Caption),
            new SqlParameter("@Image", Image),
            new SqlParameter("@CreateDate", CreateDate),
            new SqlParameter("@idContent", idContent)
        };
            int result_update = db.CapNhat(update_query, pr_update);

        }

        public void delete(string idContent)
        {
            string delete_query = "delete BaiVietSK where ID_Content = @idContent";
            //Truyen tham so
            SqlParameter[] pr_delete = new SqlParameter[] {
            new SqlParameter("@idContent", idContent)
            };
            int result_delete = db.CapNhat(delete_query, pr_delete);



        }

        public DataTable getAll()
        {
            string bv_query = "Select * from  BaiVietSK";
            SqlParameter[] pr_getAll = new SqlParameter[] { };
            DataTable result_BV = db.docdulieu(bv_query, pr_getAll);
            return result_BV;

        }

        public DataTable getById(int id_content)
        {
            string bv_query = "Select * from  BaiVietSK WHERE ID_Content = @id_content ";
            SqlParameter[] pr_getAll = new SqlParameter[] {
            new SqlParameter("@id_content", id_content),
         };
            DataTable result_BV = db.docdulieu(bv_query, pr_getAll);
            return result_BV;

        }

        //Search function
        public DataTable getCLoseResult(string searchTxt)
        {
            string nameKey = "%" + searchTxt + "%";
            string sql_search = "SELECT * FROM BaiVietSK b " +
                "WHERE (b.Caption COLLATE SQL_Latin1_General_CP1_CI_AI like @nameKey )";
            SqlParameter[] pr_key = new SqlParameter[]
            {
                new SqlParameter("@nameKey", nameKey)
            };
            DataTable dt = new DataTable();
            dt = db.docdulieu(sql_search, pr_key);
            return dt;
        }


    }
}