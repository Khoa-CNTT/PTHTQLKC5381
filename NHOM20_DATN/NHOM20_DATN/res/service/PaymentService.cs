using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.res.service
{
    public class PaymentService
    {
        LopKetNoi db = new LopKetNoi();
        public  PaymentService() { }
        public int add(string IDBN, string IDPK, string Sotien, string TrangThai, string ThoiGian)
        {
            string sql = "insert into ThanhToan(IDBenhNhan, IDPhieu, SoTien, TrangThai, ThoiGianThanhToan) " +
                " values (@IDBN, @IDPK,@tien,@tg)";
            SqlParameter[] pr_add = {
            new SqlParameter("@IDBN", IDBN),
            new SqlParameter("@IDPK", IDPK),
            new SqlParameter("@tien", Sotien),
            new SqlParameter("@tg", ThoiGian)
        };
            int result_TK = db.CapNhat(sql, pr_add);
            return result_TK;
        }


        public int update(string idtt,  string idbn, string idpk, string Sotien, string ngaytt)
        {
            string sql = "UPDATE ThanhToan" +
            " SET SoTien=  @SoTien " +
            " ,TrangThai = @TrangThai" +
            " , ThoiGianThanhToan =@ngaytt" +
            " WHERE IDBenhNhan = @IDBN " +
            " and IDPhieu = @IDPK " +
            " and IDThanhToan = @id";
            SqlParameter[] pr_add = {
                new SqlParameter("@id", idtt),
            new SqlParameter("@IDBN", idbn),
            new SqlParameter("@IDPK", idpk),
            new SqlParameter("@tien", Sotien),
            new SqlParameter("@tien", ngaytt),
        };
            int result_TK = db.CapNhat(sql, pr_add);
            return result_TK;
        }

        //public void delete(string idContent)
        //{
        //    string delete_query = "delete BaiVietSK where ID_Content = @idContent";
        //    //Truyen tham so
        //    SqlParameter[] pr_delete = new SqlParameter[] {
        //    new SqlParameter("@idContent", idContent)
        //    };
        //    int result_delete = db.CapNhat(delete_query, pr_delete);




        //}




    }
}