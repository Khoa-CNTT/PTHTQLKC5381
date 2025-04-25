using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;

namespace NHOM20_DATN
{
    public class LopKetNoi
    {
        SqlConnection con;
        private void ketnoi()
        {
            string sqlCon = @"Data Source=DESKTOP-DU79F74;Initial Catalog=KLTN_QLKB_22_04;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True";
            con = new SqlConnection(sqlCon);
            con.Open();
            //Data Source = DESKTOP - DU79F74; Initial Catalog = KLTNQUANLYKHAMBENH; Integrated Security = True; Connect Timeout = 30; Encrypt = True; TrustServerCertificate = True
        }
        private void dongketnoi()
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
            }


        }
        public DataTable docdulieu(string sql, SqlParameter[] checkParams)
        {
            DataTable dt = new DataTable();
            try
            {
                ketnoi();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                if (checkParams != null)
                {
                    da.SelectCommand.Parameters.AddRange(checkParams);
                }
                da.Fill(dt);
                dongketnoi();
            }
            catch
            {
                dt = null;
            }
            finally
            {
                dongketnoi();
            }
            return dt;
        }
        public int CapNhat(string sql, SqlParameter[] parameters)
        {
            int ketqua = 0;
            try
            {
                ketnoi();
                SqlCommand cmd = new SqlCommand(sql, con);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                ketqua = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in CapNhat: " + ex.Message);
                throw;
            }
            finally
            {
                dongketnoi();
            }
            return ketqua;
        }
    }
}