using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service.ThongKe
{
    public class ThongKeBenhNhanTuVanService
    {
        public ThongKeBenhNhanTuVanService()
        {
        }

        LopKetNoi kn = new LopKetNoi();

        //============================ List  ===============================
        //============ get all in today ==========
        public DataTable getAllInDay(DateTime day)
        {
            DataTable dt = new DataTable();
            string sql = "select * from LichTuVan  " +
                " where DAY(Ngay)= DAY(@day)";

            SqlParameter[] pr = new SqlParameter[] {
         new SqlParameter("@day",day)
            };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }
        //============ get all in this month ==========
        public DataTable getAllInMonth(DateTime month)
        {

            DataTable dt = new DataTable();
            string sql = "select * from LichTuVan  " +
                " where MONTH(Ngay)= MONTH(@month)";

            SqlParameter[] pr = new SqlParameter[] {
           new SqlParameter("@month",month)
            };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }
        
        //============ get all in this year ==========
        public DataTable getAllInYear(DateTime year)
        {

            DataTable dt = new DataTable();
            string sql = "select * from LichTuVan  " +
                " where Year(Ngay)= Year(@year)";

            SqlParameter[] pr = new SqlParameter[] {
           new SqlParameter("@year",year)
            };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }
        //============ get All ==========
        public DataTable getAll()
        {

            DataTable dt = new DataTable();
            string sql = "select * from LichTuVan  ";
            SqlParameter[] pr = new SqlParameter[] {
            };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }



        //============ get from to  ==========
        public DataTable getFromTo(DateTime fromDay, DateTime toDay)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT *  FROM LichTuVan " +
                " WHERE Ngay BETWEEN @fromDay AND @toDay";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@fromDay",fromDay),
                new SqlParameter("@toDay",toDay)
            };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }



        //============================ Count  ===============================


        //============== Count current year (12 month) ============
        public DataTable countPatientCurrentYear()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT MONTH(Ngay) AS Thang, COUNT(*) AS LuotDangKy FROM LichTuVan " +
                "  WHERE YEAR(Ngay) = YEAR(GETDATE()) " +
                " GROUP BY MONTH(Ngay) " +
                " ORDER BY Thang";
            SqlParameter[] pr = new SqlParameter[] { };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }
        //============== Count Current Month (30days)  ============
        public DataTable countPatientCurrentMonth()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Day(Ngay) AS Ngay, COUNT(*) AS LuotDangKy FROM LichTuVan " +
                "  WHERE Month(Ngay) = Month(GETDATE()) " +
                " and Year(Ngay) = Year(GETDATE())" +
                " GROUP BY Day(Ngay) " +
                " ORDER BY Ngay";
            SqlParameter[] pr = new SqlParameter[] { };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }
        //============== Count Current From Day to Day  ============
        public DataTable countFromTo(DateTime fromDay, DateTime toDay)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Ngay, COUNT(*) AS LuotDangKy FROM " +
                " LichTuVan WHERE  Ngay BETWEEN @fromDate AND @toDate " +
                " GROUP BY  Ngay " +
                " ORDER BY Ngay";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDay),
                new SqlParameter("@toDate", toDay)
            };

            dt = kn.docdulieu(sql, pr);
            return dt;
        }

        //============== Count All  ============
        public DataTable countAll()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Year(Ngay) AS Nam, COUNT(*) AS LuotDangKy FROM LichTuVan " +
                   " GROUP BY Year(Ngay) " +
                   " ORDER BY Nam";
            SqlParameter[] pr = new SqlParameter[] { };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }







    }
}