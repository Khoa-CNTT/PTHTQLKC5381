using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using NHOM20_DATN.sendMail;

namespace NHOM20_DATN.res.service
{
    public class AppointmentManagerment_Service
    {


        LopKetNoi kn = new LopKetNoi();
        LichSuKhamService lichSuKhamService = new LichSuKhamService();
        public AppointmentManagerment_Service() { }



        public DataTable viewListAppointment()
        {
            string query_Appointment = "select  lkbn.NgayKham, lkbn.ThoiGianKham , lkbn.TrangThai, bn.SoDienThoai, bn.HoTen ,pk.IDPhieu , bs.HoTen as BSKham, bs.IDBacSi ,bn.IDBenhNhan   " +
        " from PhieuKham pk  " +
        " JOIN LichKhamBenhNhan lkbn ON pk.IDPhieu = lkbn.IDPhieu  " +
        " join BenhNhan bn on pk.IDBenhNhan  = bn.IDBenhNhan " +
        " JOIN LichKhamBacSi lkbs ON pk.IDPhieu = lkbs.IDPhieu   " +
        " join BacSi bs on pk.IDBacSi = bs.IDBacSi " +
        " order by  lkbn.NgayKham desc ";
            SqlParameter[] pr = new SqlParameter[] { };
            DataTable dt = kn.docdulieu(query_Appointment, pr);
            return dt;

        }



        public DataTable searchAppointment(string key)
        {
            key = "'%" + key + "%'";
            string query_Appointment = "select  lkbn.NgayKham, lkbn.ThoiGianKham , lkbn.TrangThai, bn.SoDienThoai, bn.HoTen ,pk.IDPhieu , bs.HoTen as BSKham, bs.IDBacSi ,bn.IDBenhNhan   " +
        " from PhieuKham pk   " +
        " JOIN LichKhamBenhNhan lkbn ON pk.IDPhieu = lkbn.IDPhieu  " +
        " join BenhNhan bn on pk.IDBenhNhan  = bn.IDBenhNhan " +
        "JOIN LichKhamBacSi lkbs ON pk.IDPhieu = lkbs.IDPhieu   " +
        "join BacSi bs on pk.IDBacSi = bs.IDBacSi " +
        " where pk.IDPhieu like " + key +
        " or bn.HoTen like N" + key +
        " or bs.HoTen like N" + key +
        " or bs.IDBacSi like N" + key +
        " or bn.IDBenhNhan like N" + key +
        " order by  lkbn.NgayKham, lkbn.ThoiGianKham";
            SqlParameter[] pr = new SqlParameter[] { };
            DataTable dt = kn.docdulieu(query_Appointment, pr);
            return dt;

        }



        //===============
        public List<string> availableHour(string day, string doctorId)
        {
            //======available time
            List<string> allHour = new List<string> {
        "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30",
        "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30"
             };
            //sql
            //if sql get 2 people on the same time, it will display the time and save to occupied time list
            string sql_occupiedTime = " SELECT lbn.ThoiGianKham " +
        " FROM LichKhamBenhNhan lbn " +
        " Join PhieuKham pk on lbn.IDPhieu = pk.IDPhieu " +
        " WHERE lbn.NgayKham = @day " +
        " AND IDBacSi = @docID " +
        " AND lbn.ThoiGianKham  in " +
        " (select case when COUNT(lbn.ThoiGianKham) >=2 then lbn.ThoiGianKham " +
        " else '' end " +
        " FROM LichKhamBenhNhan lbn " +
        " Join PhieuKham pk on lbn.IDPhieu = pk.IDPhieu " +
        " WHERE lbn.NgayKham = @day " +
        " AND IDBacSi = @docID " +
        " group by lbn.ThoiGianKham)";

            SqlParameter[] pr = new SqlParameter[]
            {
        new SqlParameter("@day", day),
            new SqlParameter("@docID", doctorId)
            };

            DataTable dt = kn.docdulieu(sql_occupiedTime, pr);

            List<string> occupied = convertToListTime(dt, "ThoiGianKham");

            if (occupied.Count <= 0 || occupied == null)
            {
                return allHour;
            }
            List<string> available = allHour.Except(occupied).ToList();
            return available;
        }

        //==================ConvertList
        public List<String> convertToListTime(DataTable dt, string rowName)
        {
            if (dt == null) return new List<string>();
            List<string> list = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime time = DateTime.Parse(dt.Rows[i][rowName].ToString());
                list.Add(time.ToString("HH:mm"));
            }

            return list;
        }












        //===================Xóa
        public int deleteAppointmentManager(string idPhieu, string idBs)
        {

            string query_deletePK = "delete from PhieuKham where IDBacSi= @IDBacSi and IDPhieu = @IDPhieu";
            string query_deleteLKBS = "delete from LichKhamBacSi where IDBacSi= @IDBacSi and IDPhieu = @IDPhieu";
            string query_deleteLKBN = "delete from LichKhamBenhNhan where IDPhieu = @IDPhieu";
            string query_deleteLSK = "delete from LichSuKham where IDPhieu = @IDPhieu";
            string query_deleteHSBA = "delete from HoSoBenhAn where IDLSK = @IDLSK";
            DataTable dtLSK = lichSuKhamService.getByIDPK(idPhieu);
            string idlsk = dtLSK.Rows[0]["IDLichSu"].ToString();
            SqlParameter[] pr = new SqlParameter[] {
        new SqlParameter("@IDBacSi", idBs),
            new SqlParameter("@IDPhieu", idPhieu)
    };

            SqlParameter[] prBS = new SqlParameter[] {
        new SqlParameter("@IDBacSi", idBs),
            new SqlParameter("@IDPhieu", idPhieu)
    };


            SqlParameter[] prBN = new SqlParameter[] {
        new SqlParameter("@IDPhieu", idPhieu)
    };
            SqlParameter[] prLSK = new SqlParameter[] {
        new SqlParameter("@IDPhieu", idPhieu)
    };
            SqlParameter[] prHSBA = new SqlParameter[] {
        new SqlParameter("@IDLSK", idlsk)
    };
            int resultHSBA = kn.CapNhat(query_deleteHSBA, prHSBA);
            int resultLSK = kn.CapNhat(query_deleteLSK, prLSK);
            int resultLKBS = kn.CapNhat(query_deleteLKBS, prBS);
            int resultLKBN = kn.CapNhat(query_deleteLKBN, prBN);
            int resultPk = kn.CapNhat(query_deletePK, pr);


            if (resultPk != 0 && resultLKBS != 0 && resultLKBN != 0) return 1;
            return 0;
        }










        //================Cập nhật
        public int updateApointmentManager(string idPhieu, string time, string day)
        {
            string query_UpdateTimePK = "UPDATE PhieuKham " +
        "SET ThoiGianKham= @time, NgayKham= @day " +
        "WHERE IDPhieu = @idPk ";


            string query_UpdateTimeLKBS = "UPDATE LichKhamBacSi " +
        "SET ThoiGianKham= @time, NgayKham= @day " +
        "WHERE IDPhieu = @idPk  ";

            string query_UpdateTimeLKBN = "UPDATE LichKhamBenhNhan " +
        "SET ThoiGianKham= @time, NgayKham= @day " +
        "WHERE IDPhieu = @idPk ";
            SqlParameter[] pr = new SqlParameter[] {

        new SqlParameter("@idPk", idPhieu),
            new SqlParameter("@time", time),
            new SqlParameter("@day", day)
    };
            SqlParameter[] prBS = new SqlParameter[] {

        new SqlParameter("@idPk", idPhieu),
            new SqlParameter("@time", time),
            new SqlParameter("@day", day)
    };
            SqlParameter[] prBN = new SqlParameter[] {
        new SqlParameter("@idPk", idPhieu),
            new SqlParameter("@time", time),
            new SqlParameter("@day", day)
    };

            int resultPK = kn.CapNhat(query_UpdateTimePK, pr);
            int resultLBS = kn.CapNhat(query_UpdateTimeLKBS, prBS);
            int resultLBN = kn.CapNhat(query_UpdateTimeLKBN, prBN);
            if (resultPK != 0 && resultLBS != 0 && resultLBN != 0) return 1;
            return 0;
        }












        public void mailCancelAppointmentManager(string idPk)
        {
            string query_getMailCancel = "select pk.Email as paEmail,bs.Email as docEmail , pk.IDPhieu, pk.HoTen ,pk.ThoiGianKham, pk.NgayKham " +
        "from PhieuKham pk  " +
        "join BacSi bs on pk.IDBacSi = bs.IDBacSi " +
        "where IDPhieu = @idPK";
            SqlParameter[] pr = new SqlParameter[] {
        new SqlParameter("@idPK", idPk)
    };
            DataTable dt = kn.docdulieu(query_getMailCancel, pr);
            //====Get email client
            string emailPatient = dt.Rows[0]["paEmail"].ToString();
            ////====Get mail doctor
            string emailDoctor = dt.Rows[0]["docEmail"].ToString();

            //string emailPatient = "rick38@ethereal.email";
            //string emailDoctor = "rick38@ethereal.email";

            string name = dt.Rows[0]["HoTen"].ToString();
            DateTime day_db = DateTime.Parse(dt.Rows[0]["NgayKham"].ToString());
            string day = day_db.ToString("dd/MM/yyyy");
            DateTime time_db = DateTime.Parse(dt.Rows[0]["ThoiGianKham"].ToString());
            string time = time_db.ToString("HH:mm");

            //========Doctor
            string subjectDoc = "Bệnh viện Banana: Hủy Khám " + name.ToUpper() + " " + time + " Ngày " + day;
            string descriptionDoc = "Quản trị viên của bệnh viện đã HỦY LỊCH KHÁM của bệnh nhân!" + name +
        "\n Mã phiếu HỦY KHÁM của bệnh nhân là: " + idPk
        + " vào lúc " + time + " ngày " + day + ".";

            //mailSender sender = new mailSender();
            //sender.sendMail_CancelAppointment(emailPatient, subjectPat, descriptionPat);
            //sender.sendMail_CancelAppointment(emailDoctor, subjectDoc, descriptionDoc);



            sendMai_gmail mailSender = new sendMai_gmail();
            //=========Patient
            BenhNhanMailHuy(emailPatient, idPk, day, time);
            //========Doctor
            BacSiMailHuy(emailDoctor, idPk, day, time);

        }

        public void BenhNhanMailHuy(string email, string idPk, string day, string time)
        {
            try
            {
                string tieude = "BANANA Hospital – Hủy lịch khám " + idPk;
                string noidung = @"
                            <div style='background-color: #f5f5f5; padding: 10px 0; font-family: Arial, sans-serif;'>
                              <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                                <div style='background-color: #13bdbd; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
                                  Bệnh viện BANANA HOSPITAL
                                </div>
                                <div style='padding: 30px; text-align: left;'>
                                  <h2 style='color: #13bdbd;'>Huỷ lịch khám</h2>
      
                                  <p>Xin chào <strong style='color: #13bdbd;'>Quý khách</strong>,</p>
                                  <p>Chúng tôi xin xác nhận rằng lịch khám của Quý khách đã <strong style='color: #13bdbd;'>Bị Huỷ</strong> bởi quản trị viên. Thông tin phiếu khám của quý khách:</p>
                                  <ul style='list-style: none; padding-left: 0;'>
                                      <li>🧾 <strong>Mã phiếu:</strong> " + idPk + @"</li>
                                      <li>🗓 <strong>Ngày:</strong> " + day + @"</li>
                                      <li>🕒 <strong>Thời gian:</strong> " + time + @"</li>
                                  </ul>
<p>Nguyên nhân hủy là vì một số việc xảy ra không dự kiến trước được. Mong quý khách thông cảm.</p>
                                  <p>Chúng tôi rất tiếc khi Quý khách huỷ lịch hẹn. Hy vọng sẽ được phục vụ Quý khách trong những lần tới.</p>
                                  <p>Nếu có thắc mắc vui lòng liên hệ với chúng tôi để được hỗ trợ kịp thời.</p>
                                  <p style='margin-top: 10px;'>Xin chân thành cảm ơn Quý khách đã tin tưởng <strong style='color: #13bdbd;'>BANANA Hospital</strong></p>
                                  <p>Trân trọng,</p>
                                  <p><strong style='color: #13bdbd;'>Bệnh viện BANANA HOSPITAL</strong></p>
                                </div>
                              </div>
                            </div>";
                sendMai_gmail mailSender = new sendMai_gmail();
                mailSender.sendMail_gmail(email, tieude, noidung);
            }
            catch { }
        }
        public void BacSiMailHuy(string email, string idPk, string day, string time)
        {
            try
            {
                string tieude = "BANANA Hospital – Hủy lịch khám " + idPk + "";
                string noidung = @"
                            <div style='background-color: #f5f5f5; padding: 10px 0; font-family: Arial, sans-serif;'>
                              <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                                <div style='background-color: #13bdbd; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
                                  Bệnh viện BANANA HOSPITAL
                                </div>
                                <div style='padding: 30px; text-align: left;'>
                                  <h2 style='color: #13bdbd;'>Huỷ lịch khám</h2>
      
                                  <p>Xin chào <strong style='color: #13bdbd;'>Bác Sĩ</strong>,</p>
                                  <p>Bệnh viện xin xác nhận rằng lịch khám của Bác Sĩ đã <strong style='color: #13bdbd;'>Bị Huỷ</strong> bởi quản trị viên. Thông tin phiếu khám đã hủy:</p>
                                  <ul style='list-style: none; padding-left: 0;'>
                                      <li>🧾 <strong>Mã phiếu:</strong> " + idPk + @"</li>
                                      <li>🗓 <strong>Ngày:</strong> " + day + @"</li>
                                      <li>🕒 <strong>Thời gian:</strong> " + time + @"</li>
                                  </ul>
<p>Nguyên nhân hủy là vì một số việc xảy ra không dự kiến trước được. Mong bác sĩ thông cảm.</p>
                                  <p>Nếu có thắc mắc vui lòng liên hệ để được hỗ trợ kịp thời.</p>
                                  <p style='margin-top: 10px;'>Xin chân thành cảm ơn Bác Sĩ đã hết mình vì <strong style='color: #13bdbd;'>BANANA Hospital</strong></p>
                                  <p>Trân trọng,</p>
                                  <p><strong style='color: #13bdbd;'>Bệnh viện BANANA HOSPITAL</strong></p>
                                </div>
                              </div>
                            </div>";
                sendMai_gmail mailSender = new sendMai_gmail();
                mailSender.sendMail_gmail(email, tieude, noidung);
            }
            catch { }
        }







        public void mailChangeAppointmentManager(string idPk, string dayOld, string timeOld)
        {
            string query_getMailReschedule = "select pk.Email as paEmail,bs.Email as docEmail , pk.IDPhieu, pk.HoTen ,pk.ThoiGianKham, pk.NgayKham " +
        "from PhieuKham pk  " +
        "join BacSi bs on pk.IDBacSi = bs.IDBacSi " +
        "where IDPhieu = @idPK";

            SqlParameter[] pr = new SqlParameter[] {
        new SqlParameter("@idPK", idPk)
    };
            DataTable dt = kn.docdulieu(query_getMailReschedule, pr);
            //====Get email client
            string emailUser = dt.Rows[0]["paEmail"].ToString();
            //====Get email doctor
            string emailDoctor = dt.Rows[0]["docEmail"].ToString();

            //string emailUser = "rick38@ethereal.email";
            //string emailDoctor = "rick38@ethereal.email";

            string name = dt.Rows[0]["HoTen"].ToString();
            DateTime day_db = DateTime.Parse(dt.Rows[0]["NgayKham"].ToString());
            string day = day_db.ToString("dd/MM/yyyy");
            DateTime time_db = DateTime.Parse(dt.Rows[0]["ThoiGianKham"].ToString());
            string time = time_db.ToString("HH:mm");
            DateTime day_old = DateTime.Parse(dayOld);
            dayOld = day_old.ToString("dd/MM/yyyy");

        //    string subjectPat = "Bệnh viện banana: Đổi Giờ Khám Ngày " + dayOld;
        //    string descriptionPat = "Quản trị viên đã Đổi giờ khám của bạn!" +
        //"\nGiờ khám của bạn lúc: " + timeOld + " , ngày " + dayOld +
        //"\nĐược đổi qua lúc: " + time + ", ngày " + day;
        //    string subjectDoc = "Bệnh viện banana: Đổi Giờ Khám " + name + " Ngày " + dayOld;
        //    string descriptionDoc = "Quản trị viên đã Đổi giờ khám của bệnh nhân " + name.ToUpper() + " !" +
        //"\nGiờ khám của bệnh nhân lúc: " + timeOld + " , ngày " + dayOld +
        //"\nĐược đổi qua lúc: " + time + ", ngày " + day;

            //mailSender mailSender = new mailSender();
            //send to patient
            //mailSender.sendMail_CancelAppointment(emailUser, subjectPat, descriptionPat);
            //send to doctor
            //mailSender.sendMail_CancelAppointment(emailDoctor, subjectDoc, descriptionDoc);

            //sendMai_gmail mailSender = new sendMai_gmail();
            //mailSender.sendMail_gmail(emailUser, subjectPat, descriptionPat);
            //mailSender.sendMail_gmail(emailDoctor, subjectDoc, descriptionDoc);
            BenhNhanMailDoiGio(emailUser, idPk, timeOld, dayOld, time, day);
            BacSiMailDoiGio(emailDoctor,idPk, timeOld, dayOld, time, day);
        }



        public void BenhNhanMailDoiGio(string email, string idPk, string timeOld, string dayOld, string timeNew, string dayNew)
        {
            try
            {
                string tieude = "BANANA Hospital – Đổi Giờ lịch khám " + idPk + "";
                string noidung = @"
                            <div style='background-color: #f5f5f5; padding: 10px 0; font-family: Arial, sans-serif;'>
                              <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                                <div style='background-color: #13bdbd; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
                                  Bệnh viện BANANA HOSPITAL
                                </div>
                                <div style='padding: 30px; text-align: left;'>
                                  <h2 style='color: #13bdbd;'>Đổi Giờ lịch khám</h2>
      
                                  <p>Xin chào <strong style='color: #13bdbd;'>Quý Khách</strong>,</p>
                                    <p>Chúng tôi xin xác nhận rằng lịch khám của Quý khách đã <strong style='color: #13bdbd;'>Đổi Giờ </strong> bởi quản trị viên. Thông tin phiếu khám của quý khách:</p>
                                  <ul style='list-style: none; padding-left: 0;'>
                                      <li>🧾 <strong>Mã phiếu:</strong> " + idPk + @"</li>
                                      <li>🗓 <strong>Ngày:</strong> " + dayOld + @"</li>
                                      <li>🕒 <strong>Thời gian:</strong> " + timeOld + @"</li>
                                  </ul>

<p>Vì một vài sự cố nên lịch khám của bạn sẽ được dời qua:</p>
<ul style='list-style: none; padding-left: 0;'>
                                      <li>🗓 <strong>Ngày:</strong> " + dayNew + @"</li>
                                      <li>🕒 <strong>Thời gian:</strong> " + timeNew + @"</li>
                                  </ul>
                                  <p>Nếu Quý Khách có thắc mắc vui lòng liên hệ để được hỗ trợ kịp thời.</p>
                                  <p style='margin-top: 10px;'>Xin chân thành cảm ơn Bác Sĩ đã hết mình vì <strong style='color: #13bdbd;'>BANANA Hospital</strong></p>
                                  <p>Trân trọng,</p>
                                  <p><strong style='color: #13bdbd;'>Bệnh viện BANANA HOSPITAL</strong></p>
                                </div>
                              </div>
                            </div>";
                sendMai_gmail mailSender = new sendMai_gmail();
                mailSender.sendMail_gmail(email, tieude, noidung);
            }
            catch { }
        }

        public void BacSiMailDoiGio(string email, string idPk, string timeOld, string dayOld, string timeNew, string dayNew)
        {
            try
            {
                string tieude = "BANANA Hospital – Đổi Giờ lịch khám " + idPk + "";
                string noidung = @"
                            <div style='background-color: #f5f5f5; padding: 10px 0; font-family: Arial, sans-serif;'>
                              <div style='max-width: 600px; background: white; margin: auto; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                                <div style='background-color: #13bdbd; color: white; padding: 20px; text-align: center; font-size: 24px; font-weight: bold;'>
                                  Bệnh viện BANANA HOSPITAL
                                </div>
                                <div style='padding: 30px; text-align: left;'>
                                  <h2 style='color: #13bdbd;'>Đổi Giờ lịch khám</h2>
      
                                  <p>Xin chào <strong style='color: #13bdbd;'>Bác Sĩ</strong>,</p>
                                    <p>Bệnh viện xin xác nhận rằng lịch khám của Bác Sĩ đã <strong style='color: #13bdbd;'>Bị Huỷ</strong> bởi quản trị viên. Thông tin phiếu khám:</p>
                                  <ul style='list-style: none; padding-left: 0;'>
                                      <li>🧾 <strong>Mã phiếu:</strong> " + idPk + @"</li>
                                      <li>🗓 <strong>Ngày:</strong> " + dayOld + @"</li>
                                      <li>🕒 <strong>Thời gian:</strong> " + timeOld + @"</li>
                                  </ul>

<p>Vì một vài sự cố nên lịch khám này sẽ được dời qua:</p>
<ul style='list-style: none; padding-left: 0;'>
                                      <li>🗓 <strong>Ngày:</strong> " + dayNew + @"</li>
                                      <li>🕒 <strong>Thời gian:</strong> " + timeNew + @"</li>
                                  </ul>
                                  <p>Nếu  có thắc mắc vui lòng liên hệ để được hỗ trợ kịp thời.</p>
                                  <p style='margin-top: 10px;'>Xin chân thành cảm ơn Quý khách đã tin tưởng <strong style='color: #13bdbd;'>BANANA Hospital</strong></p>
                                  <p>Trân trọng,</p>
                                  <p><strong style='color: #13bdbd;'>Bệnh viện BANANA HOSPITAL</strong></p>
                                </div>
                              </div>
                            </div>";
                sendMai_gmail mailSender = new sendMai_gmail();
                mailSender.sendMail_gmail(email, tieude, noidung);
            }
            catch { }
        }

    }
}