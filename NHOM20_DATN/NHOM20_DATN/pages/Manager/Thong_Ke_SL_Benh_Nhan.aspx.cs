using NHOM20_DATN.res.service;
using NHOM20_DATN.res.service.ThongKe;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Thong_Ke_SL_Benh_Nhan : System.Web.UI.Page
    {
        protected string labelsJson;
        protected string huyDataJson;
        protected string dkDataJson;
        ThongKeBenhNhanService thongKeBenhNhanService= new ThongKeBenhNhanService();
        PatientManagerment patientsService = new PatientManagerment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCurrentYear();
            }


        }
        // =====================View==================
        public void loadView(DataTable dt_list)
        {
            //list all patient order current year
            GridView_All.DataSource = dt_list;
            GridView_All.DataBind();
            //chart always display current year/
        }

        //==========================     GridView   ==========================
        //========================== GridView current year =====================
        public void loadCurrentYear()
        {
            DataTable dt_list = new DataTable();
            // Datatable list all patient order current year
            dt_list = thongKeBenhNhanService.getAllInYear(DateTime.Now);
            // Datatable chart always display current year/
            DataTable dt_chart = new DataTable();
            dt_chart = thongKeBenhNhanService.countPatientCurrentYear();
            loadView(dt_list);
            loadChartInYear(dt_chart);
        }
        //========================== GridView this month ==========================
        public void loadThisMonth()
        {
            DataTable dt_list = new DataTable();
            //list all patient order this month
            dt_list = thongKeBenhNhanService.getAllInMonth(DateTime.Now);
            // count all patient this month
            DataTable dt_chart = new DataTable();
            dt_chart = thongKeBenhNhanService.countPatientCurrentMonth();
            // load view with this month
            loadView(dt_list);
            loadChartInMonth(dt_chart);
        }
        //========================== GridView this week ==========================
        public void loadThisWeek()
        {
            DateTime today = DateTime.Today;
            // Tính ngày đầu tuần (Thứ 2)
            int delta = DayOfWeek.Monday - today.DayOfWeek;
            DateTime startOfWeek = today.AddDays(delta);
            // Ngày cuối tuần (Chủ nhật)
            DateTime endOfWeek = startOfWeek.AddDays(6);
            
            DataTable dt_list = new DataTable();
            //list all patient order this month
            dt_list = thongKeBenhNhanService.getFromTo(startOfWeek, endOfWeek);
            // count all patient this month
            DataTable dt_chart = new DataTable();
            dt_chart = thongKeBenhNhanService.countFromTo(startOfWeek, endOfWeek);
            // load view with this month
            loadView(dt_list);
            loadChartInWeek(dt_chart);
        }
        //========================== GridView today ==========================
        public void loadToday()
        {
            DateTime today = DateTime.Today;

            DataTable dt_list = new DataTable();
            //list all patient order this month
            dt_list = thongKeBenhNhanService.getFromTo(today, today);
            // count all patient this month
            DataTable dt_chart = new DataTable();
            dt_chart = thongKeBenhNhanService.countFromTo(today, today);
            // load view with this month
            string todayString =DateTime.Today.ToString("yyyy-MM-dd");
            loadView(dt_list);
            loadChartInDay(dt_chart, todayString);
        }




        //========================== Chart  ==========================
        //========================== Chart count by year ==========================
        public void loadChartInYear(DataTable dt)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> dkData = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                labels.Add("Tháng " + month);
                DataRow[] dkRows = dt.Select($"Thang = {month} AND TrangThai = 'DaDangKy'");
                DataRow[] huyRows = dt.Select($"Thang = {month} AND TrangThai = 'DaHuy'");
                dkData.Add(dkRows.Length > 0 ? Convert.ToInt32(dkRows[0]["SoLuotKham"]) : 0);
                huyData.Add(huyRows.Length > 0 ? Convert.ToInt32(huyRows[0]["SoLuotKham"]) : 0);
            }
            lbl_totalPatient.Text = "" + countAllPatient();
            // total Pk
            lbl_allPk.Text = ""+ (countDaHuy(dt) + countDaDangKy(dt));
            // DaHuy Pk
            lbl_DaHuy.Text = ""+ countDaHuy(dt);
            // DaDangKy pk
            lbl_DaDangKy.Text = ""+ countDaDangKy(dt);

            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            huyDataJson = js.Serialize(huyData);
            dkDataJson = js.Serialize(dkData);
        }
        //========================== Chart count by Month ==========================
        public void loadChartInMonth(DataTable dt)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> dkData = new List<int>();
            int daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                labels.Add("Ngày " + day);
                DataRow[] dkRows = dt.Select($"Ngay = {day} AND TrangThai = 'DaDangKy'");
                DataRow[] huyRows = dt.Select($"Ngay = {day} AND TrangThai = 'DaHuy'");
                dkData.Add(dkRows.Length > 0 ? Convert.ToInt32(dkRows[0]["SoLuotKham"]) : 0);
                huyData.Add(huyRows.Length > 0 ? Convert.ToInt32(huyRows[0]["SoLuotKham"]) : 0);
            }
            lbl_totalPatient.Text = "" + countAllPatient();
            lbl_totalPatient.Text = "" + countAllPatient();
            // total Pk
            lbl_allPk.Text = "" + (countDaHuy(dt) + countDaDangKy(dt));
            // DaHuy Pk
            lbl_DaHuy.Text = "" + countDaHuy(dt);
            // DaDangKy pk
            lbl_DaDangKy.Text = "" + countDaDangKy(dt);


            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            huyDataJson = js.Serialize(huyData);
            dkDataJson = js.Serialize(dkData);
        }
        //================= Count Current week  ============

        public void loadChartInWeek(DataTable dt)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> dkData = new List<int>();

            DateTime today = DateTime.Today;
            int delta = DayOfWeek.Monday - today.DayOfWeek;
            if (delta > 0) delta -= 7; // Nếu hôm nay là Chủ Nhật, sẽ lùi lại 6 ngày

            DateTime startOfWeek = today.AddDays(delta);
            DateTime endOfWeek = startOfWeek.AddDays(6);
            string[] thu = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" };

            for (int i = 0; i < 7; i++)
            {
                DateTime ngay = startOfWeek.AddDays(i);
                labels.Add(thu[i]);
                DataRow[] dkRows = dt.Select($"Ngay = '{ngay:yyyy-MM-dd}' AND TrangThai = 'DaDangKy'");
                DataRow[] huyRows = dt.Select($"Ngay = '{ngay:yyyy-MM-dd}' AND TrangThai = 'DaHuy'");
                dkData.Add(dkRows.Length > 0 ? Convert.ToInt32(dkRows[0]["SoLuotKham"]) : 0);
                huyData.Add(huyRows.Length > 0 ? Convert.ToInt32(huyRows[0]["SoLuotKham"]) : 0);
            }
            lbl_totalPatient.Text = "" + countAllPatient();
            // total Pk
            lbl_allPk.Text = "" + (countDaHuy(dt) + countDaDangKy(dt));
            // DaHuy Pk
            lbl_DaHuy.Text = "" + countDaHuy(dt);
            // DaDangKy pk
            lbl_DaDangKy.Text = "" + countDaDangKy(dt);


            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            huyDataJson = js.Serialize(huyData);
            dkDataJson = js.Serialize(dkData);
        }




        //========================== Chart count by Day ==========================
        public void loadChartInDay(DataTable dt, string day)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> dkData = new List<int>();

            for (int i = 1; i <= 1; i++)
            {
                labels.Add("Ngày " + day);
                DataRow[] dkRows = dt.Select($"Ngay = '{day}' AND TrangThai = 'DaDangKy'");
                DataRow[] huyRows = dt.Select($"Ngay = '{day}' AND TrangThai = 'DaHuy'");
                dkData.Add(dkRows.Length > 0 ? Convert.ToInt32(dkRows[0]["SoLuotKham"]) : 0);
                huyData.Add(huyRows.Length > 0 ? Convert.ToInt32(huyRows[0]["SoLuotKham"]) : 0);
            }
            lbl_totalPatient.Text = "" + countAllPatient();
            lbl_totalPatient.Text = "" + countAllPatient();
            // total Pk
            lbl_allPk.Text = "" + (countDaHuy(dt) + countDaDangKy(dt));
            // DaHuy Pk
            lbl_DaHuy.Text = "" + countDaHuy(dt);
            // DaDangKy pk
            lbl_DaDangKy.Text = "" + countDaDangKy(dt);


            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            huyDataJson = js.Serialize(huyData);
            dkDataJson = js.Serialize(dkData);
        }








        //===================== Button function ====================
        protected void btnToday_Click(object sender, EventArgs e)
        {
            loadToday();
            ViewState["ThongKeMode"] = "Today";
        }

        protected void btnThisWeek_Click(object sender, EventArgs e)
        {
            loadThisWeek();
            ViewState["ThongKeMode"] = "ThisWeek";
        }

        protected void btnThisMonth_Click(object sender, EventArgs e)
        {

            loadThisMonth();
            ViewState["ThongKeMode"] = "ThisMonth";

        }

        protected void btnThisYear_Click(object sender, EventArgs e)
        {
            loadCurrentYear();
            ViewState["ThongKeMode"] = "ThisYear";


        }

        //====================== Functions ======================
        public int countAllPatient()
        {
            DataTable dt_Patient = new DataTable();
            // get All patient
            dt_Patient = patientsService.viewPatient();
            return dt_Patient.Rows.Count;
        }


        public int countDaHuy(DataTable dt)
        {
            int countDaHuy = 0;
            // Count Trang Thai Da Huy
            foreach (DataRow row in dt.Rows)
            {
                //Get rows which is DaHuy
                string trangthai = row["TrangThai"].ToString();
                int luotKham = int.Parse(row["SoLuotKham"].ToString());
                if (trangthai == "DaHuy")
                {
                    countDaHuy+= luotKham;
                }
            }
            return countDaHuy;
        }
        public int countDaDangKy(DataTable dt)
        {
            int countDaDangKy = 0;
            // Count Trang Thai Da Huy
            foreach (DataRow row in dt.Rows)
            {
                //Get rows which is DaHuy
                string trangthai = row["TrangThai"].ToString();
                int luotKham = int.Parse(row["SoLuotKham"].ToString());
                if (trangthai == "DaDangKy")
                {
                    countDaDangKy+= luotKham;
                }
            }
            return countDaDangKy;
        }

        protected void GridView_All_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_All.PageIndex = e.NewPageIndex;
            string mode = ViewState["ThongKeMode"]?.ToString();
            if (mode == "ThisYear")
            {
                loadCurrentYear();
            }
            else if (mode == "ThisMonth")
            {
                loadThisMonth();
            }
            else if(mode == "ThisWeek")
            {
                loadThisWeek();
            }
            else if (mode == "Today")
            {
                loadToday();
            }
            else
            {
                // Mặc định nếu chưa chọn
                loadCurrentYear();
            }
         
        }

     

    }
}