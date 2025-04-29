using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service.ThongKe
{

    public class ThongKeBenhNhanService
    {
        LopKetNoi kn = new LopKetNoi();

        //============ get all ==========
        public DataTable countAllPatient()
        {
            DataTable dt = new DataTable();
            string sql = "select count(*) as SLBN from BenhNhan";
            SqlParameter[] pr = new SqlParameter[]{};
            dt = kn.docdulieu(sql, pr);     
            return dt;
        }
        //============ get by month ==========
        public DataTable countWithMonth(){
            DataTable dt = new DataTable();

            
            
            
            return dt;
        }
        //============ get by day ==========
        public DataTable countWithDay()
        {
            DataTable dt = new DataTable();




            return dt;
        }

        //============ count from to  ==========
        public DataTable countFromTo(string fromDay, string toDay)
        {
            DataTable dt = new DataTable();
                



            return dt;
        }
    }
}