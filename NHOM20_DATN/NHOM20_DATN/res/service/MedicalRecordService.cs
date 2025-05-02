using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service
{
    public class MedicalRecordService
    {
        LopKetNoi db = new LopKetNoi();
        public DataTable getAll(string idbs)
        {
            string query = "select hs.IDHS,hs.IDBN, hs.IDLSK, bn.HoTen, pk.NgayKham, hs.ChanDoan, hs.DonThuoc, hs.GhiChu, hs.NgayCapNhat from HoSoBenhAn hs, BenhNhan bn, BacSi bs, LichSuKham lsk, PhieuKham pk" +
                " where hs.IDBN = bn.IDBenhNhan and bs.IDBacSi = hs.IDBS and hs.IDLSK = lsk.IDLichSu and pk.IDPhieu = lsk.IDPhieu" +
                " and hs.IDBS = @idbs";
            SqlParameter[] param = new SqlParameter[] {
      new SqlParameter("@idbs",idbs)
      };
            DataTable dt = new DataTable();
            dt = db.docdulieu(query, param);
            return dt;
        }
        public DataTable getByPatientName(string name, string idbs)
        {
            name = "%" + name + "%";
            string query = "select hs.IDHS,hs.IDBN, hs.IDLSK, bn.HoTen, pk.NgayKham, hs.ChanDoan, hs.DonThuoc, hs.GhiChu, hs.NgayCapNhat from HoSoBenhAn hs, BenhNhan bn, BacSi bs, LichSuKham lsk, PhieuKham pk" +
                "  where hs.IDBN = bn.IDBenhNhan and bs.IDBacSi = hs.IDBS and hs.IDLSK = lsk.IDLichSu and pk.IDPhieu = lsk.IDPhieu" +
                "   and (bn.HoTen COLLATE SQL_Latin1_General_CP1_CI_AI like @nameKey )" +
                "   and hs.IDBS = @idbs";
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@nameKey", name),
            new SqlParameter("@idbs", idbs)};
            DataTable dt = new DataTable();
            dt = db.docdulieu(query, param);
            return dt;
        }

        // ========        UPDATE
        public int update(string idbs, string idbn, string idhs, string chandoan, string donthuoc, string ngaycn, string ghichu)
        {
            string query = "update HoSoBenhAn " +
                " set ChanDoan = @chandoan, DonThuoc = @donthuoc, NgayCapNhat = @ngaycn, GhiChu = @ghichu " +
                " where IDHS = @idhs and IDBS = @idbs and IDBN = @idbn";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@idbs", idbs),
                new SqlParameter("@idbn", idbn),
                new SqlParameter("@idhs", idhs),
                new SqlParameter("@chandoan", chandoan),
                new SqlParameter("@donthuoc", donthuoc),
                new SqlParameter("@ngaycn", ngaycn),
                new SqlParameter("@ghichu", ghichu)

            };
            int result = db.CapNhat(query, pr);
            return result;
        }


    }
}