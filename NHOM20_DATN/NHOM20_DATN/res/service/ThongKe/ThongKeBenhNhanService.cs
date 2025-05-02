using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service.ThongKe
{

    public class ThongKeBenhNhanService
    {
        LopKetNoi kn = new LopKetNoi();
        //============ get all in today ==========
        public DataTable getAllInDay(DateTime day)
        {
            DataTable dt = new DataTable();
            string sql = "select * from LichKhamBenhNhan  " +
                " where DAY(NgayKham)= DAY(@day)";

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
            string sql = "select * from LichKhamBenhNhan  " +
                " where MONTH(NgayKham)= MONTH(@month)";
                
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
            string sql = "select * from LichKhamBenhNhan  " +
                " where Year(NgayKham)= Year(@year)";

            SqlParameter[] pr = new SqlParameter[] {
           new SqlParameter("@year",year)
            };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }



        //============ get by user month ==========
        public DataTable countMostWithMonth(DateTime month){

            DataTable dt = new DataTable();
            string sql = "select top 10 pk.HoTen , count(*) as LanKham from PhieuKham pk, BacSi bs, BenhNhan bn " +
                " where pk.IDBenhNhan = bn.IDBenhNhan and pk.IDBacSi = bs.IDBacSi " +
                " and   month(pk.NgayKham) = month(@month)" +
                "  group by MONTH(pk.NgayKham),pk.HoTen " +
                " ORDER BY  MONTH(pk.NgayKham)";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@month",month)
            };  
            dt = kn.docdulieu(sql,pr); 
            return dt;
        }
        //============ get by day ==========
        public DataTable countWithDay(DateTime day)
        {
            DataTable dt = new DataTable();
            string sql = "select * from PhieuKham pk, BacSi bs, BenhNhan bn " +
                " where pk.IDBenhNhan = bn.IDBenhNhan and pk.IDBacSi = bs.IDBacSi" +
                " and  day(pk.NgayKham) = day(@day)  " +
                " group by day(pk.NgayKham),pk.HoTen" +
                " ORDER BY day(pk.NgayKham)";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@day",day)
            };
            dt = kn.docdulieu(sql, pr);


            return dt;
        }

        //============ get from to  ==========
        public DataTable getFromTo(DateTime fromDay, DateTime toDay)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT IDBenhNhan,IDPhieu, TrangThai, NgayKham, ThoiGianKham  FROM LichKhamBenhNhan " +
                " WHERE NgayKham BETWEEN @fromDay AND @toDay";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@fromDay",fromDay),
                new SqlParameter("@toDay",toDay)
            };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }


        //============== Count ========================
        //============== Count current year============
        public DataTable countPatientCurrentYear() {
            DataTable dt = new DataTable();
            string sql = "SELECT MONTH(NgayKham) AS Thang,TrangThai, COUNT(*) AS SoLuotKham FROM LichKhamBenhNhan " +
                "  WHERE YEAR(NgayKham) = YEAR(GETDATE()) " +
                " GROUP BY MONTH(NgayKham),TrangThai " +
                " ORDER BY Thang";
            SqlParameter[] pr = new SqlParameter[]{};
            dt = kn.docdulieu(sql,pr);
            return dt;
        }
        //============== Count Current Month  ============
        public DataTable countPatientCurrentMonth()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Day(NgayKham) AS Ngay,TrangThai, COUNT(*) AS SoLuotKham FROM LichKhamBenhNhan " +
                "  WHERE Month(NgayKham) = Month(GETDATE()) " +
                " and Year(NgayKham) = Year(GETDATE())" +
                " GROUP BY Day(NgayKham),TrangThai " +
                " ORDER BY Ngay";
            SqlParameter[] pr = new SqlParameter[] { };
            dt = kn.docdulieu(sql, pr);
            return dt;
        }
        //============== Count Current From Day to Day  ============
        public DataTable countFromTo(DateTime fromDay, DateTime toDay)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT NgayKham as Ngay,TrangThai, COUNT(*) AS SoLuotKham FROM " +
                " LichKhamBenhNhan WHERE  NgayKham BETWEEN @fromDate AND @toDate " +
                " GROUP BY  NgayKham, TrangThai " +
                " ORDER BY Ngay";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDay),
                new SqlParameter("@toDate", toDay)
            };

            dt = kn.docdulieu(sql, pr);
            return dt;



        }





    }
}