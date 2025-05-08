using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service
{
    public class LichSuKhamService
    {
        LopKetNoi kn = new LopKetNoi();
        public LichSuKhamService()
        {
        }

        public int update(string idbn, string idpk, string chandoan, string huongdtr)
        {
            
            string query_update = "update LichSuKham " +
                " set ChanDoan = @chandoan, HuongDieuTri = @huongdtr " +
                " where IDBenhNhan=@IDBN and IDPhieu=@IDPK";

            SqlParameter[] prUpdate = new SqlParameter[] {
                new SqlParameter("@IDBN", idbn),
                new SqlParameter("@IDPK", idpk),
                 new SqlParameter("@chandoan", chandoan),
                new SqlParameter("@huongdtr", huongdtr)
            };
        
            int result = kn.CapNhat(query_update, prUpdate);
            if (result == 0 ) return 0;
            return result;
        }




    }
}