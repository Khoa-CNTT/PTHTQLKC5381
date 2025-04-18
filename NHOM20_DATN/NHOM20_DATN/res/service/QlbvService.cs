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
            string add_query = "insert into BaiVietSK(TieuDe, NoiDung, HinhAnh, NgayDang) values (@TieuDe, @NoiDung,@HinhAnh,@NgayDang)";
            //Truyen tham so
            SqlParameter[] pr_add = {
            new SqlParameter("@TieuDe", Content),
            new SqlParameter("@NoiDung", Caption),
            new SqlParameter("@HinhAnh", Image),
            new SqlParameter("@NgayDang", CreateDate)
        };
            int result_TK = db.CapNhat(add_query, pr_add);
            return result_TK;

        }

        public int  update(string idContent, string Title, string Description, string Image, string CreateDate)
        {
            string update_query = "UPDATE BaiVietSK" +
            " SET TieuDe=  @TieuDe " +
            " ,NoiDung = @NoiDung" +
            " , HinhAnh =@HinhAnh" +
            " , NgayDang = @NgayDang" +
            " WHERE IDBaiViet = @IDBaiViet ";
            //Truyen tham so
            SqlParameter[] pr_update = {
            new SqlParameter("@TieuDe", Title),
            new SqlParameter("@NoiDung", Description),
            new SqlParameter("@HinhAnh", Image),
            new SqlParameter("@NgayDang", CreateDate),
            new SqlParameter("@IDBaiViet", idContent)
        };
            int result_update = db.CapNhat(update_query, pr_update);
            return result_update;
        }

        public void delete(string idContent)
        {
            string delete_query = "delete BaiVietSK where IDBaiViet = @idContent";
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
            string bv_query = "Select * from  BaiVietSK WHERE IDBaiViet = @id_content ";
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
            string sql_search = "SELECT * FROM BaiVietSK " +
                " WHERE (Caption COLLATE SQL_Latin1_General_CP1_CI_AI like @nameKey )";
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