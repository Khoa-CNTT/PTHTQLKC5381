using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using NHOM20_DATN.sendMail;

namespace NHOM20_DATN.res.service
{
    public class DoctorService
    {

        LopKetNoi kn = new LopKetNoi();
        public DoctorService() { }

        public List<string> availableHour(string day, string doctorId)
        {
            //======available time
            List<string> allHour = new List<string> {
              "07:00","07:30","08:00","08:30","09:00","09:30","10:00","10:30",
                 "13:00","13:30","14:00","14:30","15:00","15:30","16:00","16:30"
             };


            //sql
            //if sql get 2 people on the same time, it will display the time and save to occupied time list
            string sql_occupiedTime = " SELECT lbn.ThoiGianKham " +
                       "FROM LichKhamBenhNhan lbn " +
                       "Join PhieuKham pk on lbn.IDPhieu = pk.IDPhieu " +
                       "WHERE lbn.NgayKham = @day " +
                       "AND IDBacSi = @docID " +
                       "AND lbn.ThoiGianKham  in " +
                       "(select case when COUNT(lbn.ThoiGianKham) >=2 then lbn.ThoiGianKham " +
                       "else '' end " +
                       "FROM LichKhamBenhNhan lbn " +
                       "Join PhieuKham pk on lbn.IDPhieu = pk.IDPhieu " +
                       "WHERE lbn.NgayKham = @day " +
                       "AND IDBacSi = @docID " +
                       "group by lbn.ThoiGianKham)";

            SqlParameter[] pr = new SqlParameter[]
            {
                        new SqlParameter("@day", day),
                        new SqlParameter("@docID", doctorId)
            };

            DataTable dt = kn.docdulieu(sql_occupiedTime, pr);

            List<string> occupied = convertToList(dt, "ThoiGianKham");

            if (occupied.Count <= 0 || occupied == null)
            {
                return allHour;
            }
            List<string> available = allHour.Except(occupied).ToList();
            return available;
        }





        //ConvertList
        public List<String> convertToList(DataTable dt, string rowName)
        {

            List<string> list = new List<string>();
            //foreach (DataRow row in dt.Rows)
            //      {
            //          list.Add(row[rowName].ToString());
            //      Console.WriteLine(row[rowName].ToString());
            //  }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime time = DateTime.Parse(dt.Rows[i][rowName].ToString());
                list.Add(time.ToString("HH:mm"));
            }

            return list;
        }


        //Xóa
        public int deleteAppointment(string idPhieu, string idBs)
        {

            string query_deletePK = "delete from PhieuKham where IDBacSi= @IDBacSi and IDPhieu = @IDPhieu";
            string query_deleteLKBS = "delete from LichKhamBacSi where IDBacSi= @IDBacSi and IDPhieu = @IDPhieu";
            string query_deleteLKBN = "delete from LichKhamBenhNhan where IDPhieu = @IDPhieu";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@IDBacSi",idBs),
                new SqlParameter("@IDPhieu", idPhieu)
            };

            SqlParameter[] prBS = new SqlParameter[] {
                new SqlParameter("@IDBacSi",idBs),
                new SqlParameter("@IDPhieu", idPhieu)
            };


            SqlParameter[] prBN = new SqlParameter[] {
                new SqlParameter("@IDPhieu", idPhieu)
            };
            int resultLKBS = kn.CapNhat(query_deleteLKBS, prBS);
            int resultLKBN = kn.CapNhat(query_deleteLKBN, prBN);
            int resultPk = kn.CapNhat(query_deletePK, pr);

            if (resultPk != 0 && resultLKBS != 0 && resultLKBN != 0) return 1;
            return 0;
        }

        //Cập nhật
        public int updateApointment(string idPhieu, string idBs, string time)
        {
            string query_UpdateTimePK = "UPDATE PhieuKham " +
                    "SET ThoiGianKham= @time " +
                    "WHERE IDPhieu = @idPk " +
                    "and IDBacSi = @idBs";

            string query_UpdateTimeLKBS = "UPDATE LichKhamBacSi " +
                    "SET ThoiGianKham= @time " +
                    "WHERE IDPhieu = @idPk " +
                    "and IDBacSi = @idBs";
            string query_UpdateTimeLKBN = "UPDATE LichKhamBenhNhan " +
                    "SET ThoiGianKham= @time " +
                    "WHERE IDPhieu = @idPk ";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("@idBs", idBs) ,
                new SqlParameter("@idPk",idPhieu),
                new SqlParameter("@time",time)
            };
            SqlParameter[] prBS = new SqlParameter[] {
                new SqlParameter("@idBs", idBs) ,
                new SqlParameter("@idPk",idPhieu),
                new SqlParameter("@time",time)
            };
            SqlParameter[] prBN = new SqlParameter[] {
                new SqlParameter("@idPk",idPhieu),
                new SqlParameter("@time",time)
            };

            int resultPK = kn.CapNhat(query_UpdateTimePK, pr);
            int resultLBS = kn.CapNhat(query_UpdateTimeLKBS, prBS);
            int resultLBN = kn.CapNhat(query_UpdateTimeLKBN, prBN);
            if (resultPK != 0 && resultLBS != 0 && resultLBN != 0) return 1;
            return 0;
        }

        public void mailCancelAppointment(string idPk, string reason)
        {
            string query_getMailCancel = "select * from PhieuKham where IDPhieu = @idPK";
            SqlParameter[] pr = new SqlParameter[] {
        new SqlParameter ("@idPK", idPk)
    };
            DataTable dt = kn.docdulieu(query_getMailCancel, pr);

            //====Get email client
            string email = dt.Rows[0]["Email"].ToString();

            string name = dt.Rows[0]["HoTen"].ToString();
            DateTime day_db = DateTime.Parse(dt.Rows[0]["NgayKham"].ToString());
            string day = day_db.ToString("dd/MM/yyyy");
            DateTime time_db = DateTime.Parse(dt.Rows[0]["ThoiGianKham"].ToString());
            string time = time_db.ToString("HH:mm");

            string subject = $"Thông Báo Hủy Lịch Khám Ngày" + day;
            string description = $@"
<div style='background-color: #f5f5f5; padding: 20px 0; font-family: Arial, sans-serif;'>
  <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
    
    <div style='background-color: #cc0000; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
      BANANA HOSPITAL
    </div>
    
    <div style='padding: 30px; text-align: left; color: #333;'>
      <p>Kính gửi <strong>{name}</strong>,</p>

      <p>Chúng tôi rất tiếc phải thông báo rằng lịch khám của bạn đã bị <strong style='color: red;'>hủy</strong>.</p>

      <ul style='list-style: none; padding-left: 0;'>
        <li>🔹 <strong>Mã phiếu:</strong> {idPk}</li>
        <li>🔹 <strong>Thời gian:</strong> {time} ngày {day}</li>
        <li>🔹 <strong>Lý do hủy:</strong> {reason}</li>
      </ul>

      <p>Chúng tôi xin lỗi vì sự bất tiện này và mong bạn thông cảm.</p>

      <p>Trân trọng,<br/>
      <strong>Phòng Khám BANANA Hospital</strong></p>
    </div>

  </div>
</div>";


            sendMai_gmail mailSender = new sendMai_gmail();
            mailSender.sendMail_gmail(email, subject, description);
        }
        public void mailChangeAppointment(string idPk, string dayOld, string timeOld)
        {
            string query_getMailCancel = "select * from PhieuKham where IDPhieu = @idPK";
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter ("@idPK", idPk)
            };
            DataTable dt = kn.docdulieu(query_getMailCancel, pr);
            //====Get email client
            string email = dt.Rows[0]["Email"].ToString();
            //string email = "rick38@ethereal.email";
            string name = dt.Rows[0]["HoTen"].ToString();

            DateTime day_db = DateTime.Parse(dt.Rows[0]["NgayKham"].ToString());
            string day = day_db.ToString("dd/MM/yyyy");
            DateTime time_db = DateTime.Parse(dt.Rows[0]["ThoiGianKham"].ToString());
            string time = time_db.ToString("HH:mm");
            DateTime day_old = DateTime.Parse(dayOld);
            dayOld = day_old.ToString("dd/MM/yyyy");

            string subject = "Bệnh viện banana: Đổi Giờ Khám Ngày " + dayOld;
            string description = "Bác sĩ đã Đổi giờ khám của bạn!\n" +
            "Giờ khám của bạn lúc: " + timeOld + " , ngày " + dayOld +
            "\nĐược đổi qua lúc: " + time + ", ngày " + day;
            //mailSender mailSender = new mailSender();
            //mailSender.sendMail_CancelAppointment(email, subject, description);

            sendMai_gmail mailSender = new sendMai_gmail();
            mailSender.sendMail_gmail(email, subject, description);

        }

    }
}