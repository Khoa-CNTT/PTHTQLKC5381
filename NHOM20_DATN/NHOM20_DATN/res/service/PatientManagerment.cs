using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service
{
    public class PatientManagerment
    {

        LopKetNoi kn = new LopKetNoi();


        public PatientManagerment() { }


        //============Get list patients
        public DataTable viewPatient()
        {
            string query = "select *" +
                "from BenhNhan";
            SqlParameter[] pr = new SqlParameter[] { };
            DataTable dt = kn.docdulieu(query, pr);
            return dt;

        }
        //=============

        //=============Search patiens
        public DataTable searchPatients(string key)
        {
            string query = @"SELECT IDBenhNhan, HoTen, NgaySinh, SoDienThoai, Email, GioiTinh 
                    FROM BenhNhan 
                    WHERE HoTen LIKE @key 
                    OR IDBenhNhan LIKE @key 
                    OR SoDienThoai LIKE @key 
                    OR Email LIKE @key 
                    OR CONVERT(varchar, NgaySinh, 103) LIKE @key
                    OR CONVERT(varchar, NgaySinh, 101) LIKE @key
                    OR CONVERT(varchar, NgaySinh, 120) LIKE @key";

            SqlParameter[] pr = new SqlParameter[] {
        new SqlParameter("@key", "%" + key + "%")
    };

            return kn.docdulieu(query, pr);
        }

        public int checkUserExisting(string username)
        {
            string checkUserQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] checkParams = { new SqlParameter("@TenDangNhap", username) };
            DataTable dt = kn.docdulieu(checkUserQuery, checkParams);


            if (dt != null && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                return 0;
            }
            return 1;


        }



        //===============

        //=============== Add patients
        public int addPatient(string IDBenhNhan, string TenDn, string MatKhau, string TenBN, string NgaySinh, string GioiTinh, string SoDienThoai, string Email)
        {
            string query_BN = "insert into BenhNhan (IDBenhNhan,HoTen, NgaySinh, GioiTinh, SoDienThoai,Email) " +
                "values(@idbn, @tenbn, @ngaysinh, @gioitinh, @sdt, @email)";
            SqlParameter[] pr_bn = new SqlParameter[] {
                new SqlParameter("@idbn", IDBenhNhan),
                new SqlParameter("@tenbn",TenBN),
                new SqlParameter("@ngaysinh",NgaySinh),
                new SqlParameter("@gioitinh",GioiTinh),
                new SqlParameter("@sdt",SoDienThoai),
                new SqlParameter("@email",Email),
            };

            string query_TK = "insert into TaiKhoan(ID, TenDangNhap, MatKhau, Email) " +
                "values(@id, @tendangnhap, @matkhau, @email)";

            SqlParameter[] pr_tk = new SqlParameter[] {
                new SqlParameter("@id", IDBenhNhan),
                new SqlParameter("@tendangnhap", TenDn),
                new SqlParameter("@matkhau", MatKhau),
                new SqlParameter("@email", Email)
            };

            int result_TK = kn.CapNhat(query_TK, pr_tk);
            int result_BN = kn.CapNhat(query_BN, pr_bn);

            if (result_BN == 0 || result_TK == 0)
            {
                string deleteTK = "delete TaiKhoan where ID = @idBn";
                SqlParameter[] pr_dtk = new SqlParameter[] {
                 new SqlParameter ("@idBn", IDBenhNhan)
                };
                int delete_Tk = kn.CapNhat(deleteTK, pr_dtk);
                return 0;
            }
            return 1;
        }


        //===============

        //=============== delete patients
        public int deletePatients(string idBN)
        {
            string query_BN = "delete BenhNhan where IDBenhNhan = @idBn";
            string query_TK = "delete TaiKhoan where ID = @idBn";
            SqlParameter[] pr_bn = new SqlParameter[] {
                new SqlParameter ("@idBn", idBN)
            };
            SqlParameter[] pr_tk = new SqlParameter[] {
                 new SqlParameter ("@idBn", idBN)
            };

            int result_bn = kn.CapNhat(query_BN, pr_bn);
            int result_tk = kn.CapNhat(query_TK, pr_tk);
            if (result_bn != 0 && result_tk != 0)
            {
                return 1;
            }
            return 0;


        }


        //===============


        //=============== update patients
        public int updatePatient(string idbn, string name, string birth, string gender, string phoneNum, string email)
        {
            string query = "update BenhNhan set " +
                "HoTen = @name, " +
                "NgaySinh = @birth, " +
                "GioiTinh= @gt, " +
                "SoDienThoai = @sdt, " +
                "Email = @email " +
                "where IDBenhNhan = @idbn";
            SqlParameter[] pr_bn = new SqlParameter[] {
                new SqlParameter("@name",name),
                new SqlParameter("@birth",birth),
                new SqlParameter("@gt",gender),
                new SqlParameter("@sdt",phoneNum),
                new SqlParameter("@email",email),
                new SqlParameter("@idbn",idbn),

            };
            int result = kn.CapNhat(query, pr_bn);
            if (result != 0) return 1;
            return 0;



        }
    }
}