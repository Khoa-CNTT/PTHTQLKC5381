using NHOM20_DATN.res.service;
using NHOM20_DATN.res.service.ThongKe;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        ThongKeBenhNhanService thongKeBenhNhanService = new ThongKeBenhNhanService();
        PatientManagerment patientsService = new PatientManagerment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCurrentYear();
            }


        }
        // =================== Grid View ==================
        public void loadView(DataTable dt_list)
        {
            if(dt_list == null)
            {
                string message = "Chưa có ai đăng ký！ ";
                string script = "showAlert('" + message + "','info');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }

            GridView_All.DataSource = dt_list;
            GridView_All.DataBind();
            return;

        }

        //===============Pagination===========
        protected void GridView_All_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_All.PageIndex = e.NewPageIndex;
            string mode = ViewState["ThongKeMode"]?.ToString();
            checkMode(mode);

        }

        //==========================     GridView   ==========================
        //========================== GridView All =====================
        public void loadAll()
        {
            DataTable dt_list = new DataTable();
            // Datatable list all patient order current year
            dt_list = thongKeBenhNhanService.getAll();
            // Datatable chart always display current year/
            DataTable dt_chart = new DataTable();
            dt_chart = thongKeBenhNhanService.countAll();
            loadView(dt_list);
            loadChartYear(dt_chart);
        }
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
            if (dt_list.Rows.Count <= 0)
            {
                string message = "Hôm nay chưa có ai đăng ký！ ";
                string script = "showAlert('" + message + "','info');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                loadView(null);
                lbl_totalPatient.Text = "" + countAllPatient();
                lbl_allPk.Text = "" + (countDaHuy(null) + countDaDangKy(null));
                lbl_DaHuy.Text = "" + countDaHuy(null);
                lbl_DaDangKy.Text = "" + countDaDangKy(null);
                return;
            }


            // load view with this month
            string todayString = DateTime.Today.ToString("yyyy-MM-dd");
            loadView(dt_list);
            loadChartInDay(dt_chart, todayString);
        }

        public void loadFromToDate()
        {

            DateTime fromDate, toDate;
            // check formart
            if (!(DateTime.TryParse(txtFromDate.Text, out fromDate) && DateTime.TryParse(txtToDate.Text, out toDate)))
            {
                string message = "Kiểm tra lại ngày đã chọn";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }
            TimeSpan khoangCach = toDate - fromDate;
            int cachngay = int.Parse(khoangCach.TotalDays.ToString());
            // check if input > 90 days
            if (cachngay > 90)
            {
                string message = "Vui lòng chọn nhỏ hơn 90 ngày";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }else if(cachngay <= 0){
                string message = "Vui lòng chọn từ quá khứ đến tương lai!";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }

            DataTable dt_list = new DataTable();
            dt_list = thongKeBenhNhanService.getFromTo(fromDate, toDate);
            DataTable dt_chart = new DataTable();
            dt_chart = thongKeBenhNhanService.countFromTo(fromDate, toDate);
            loadView(dt_list);
            loadChartFromToDate(dt_chart, fromDate, toDate);
            return;
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

                DataRow[] dkRows = dt.Select($"Thang = {month} AND (TrangThai = 'DaDangKy' Or TrangThai = 'DaKham')");
                DataRow[] huyRows = dt.Select($"Thang = {month} AND TrangThai = 'DaHuy'");

                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
                int huyCount = huyRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));

                dkData.Add(dkCount);
                huyData.Add(huyCount);
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
                DataRow[] dkRows = dt.Select($"Ngay = {day} AND (TrangThai = 'DaDangKy' Or TrangThai = 'DaKham') ");
                DataRow[] huyRows = dt.Select($"Ngay = {day} AND TrangThai = 'DaHuy'");
                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
                int huyCount = huyRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
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
                DataRow[] dkRows = dt.Select($"Ngay = '{ngay:yyyy-MM-dd}' AND (TrangThai = 'DaDangKy' Or TrangThai = 'DaKham') ");
                DataRow[] huyRows = dt.Select($"Ngay = '{ngay:yyyy-MM-dd}' AND TrangThai = 'DaHuy'");
                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
                int huyCount = huyRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
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
                DataRow[] dkRows = dt.Select($"Ngay = '{day}' AND (TrangThai = 'DaDangKy' Or TrangThai = 'DaKham')");
                DataRow[] huyRows = dt.Select($"Ngay = '{day}' AND TrangThai = 'DaHuy'");
                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
                int huyCount = huyRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
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
        public void loadChartFromToDate(DataTable dt, DateTime fromDate, DateTime toDate)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> dkData = new List<int>();
            // let run from date to date by add 1 day
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                string dayString = date.ToString("yyyy-MM-dd");
                labels.Add(date.ToString("dd/MM"));
                DataRow[] dkRows = dt.Select($"Ngay = '{dayString}' AND (TrangThai = 'DaDangKy' Or TrangThai = 'DaKham')");
                DataRow[] huyRows = dt.Select($"Ngay = '{dayString}' AND TrangThai = 'DaHuy'");
                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
                int huyCount = huyRows.Sum(r => Convert.ToInt32(r["SoLuotKham"]));
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


        //========================== Chart count by Year ==========================
        public void loadChartYear(DataTable dt)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> dkData = new List<int>();
            // let run from date to date by add 1 day
            var years = dt.AsEnumerable()
              .Select(row => row.Field<int>("Nam"))
              .Distinct()
              .OrderBy(n => n)
              .ToList();
            foreach (var year in years)
            {
                labels.Add(year.ToString());

                // Tổng số lượt khám cho cả DaDangKy và DangCho trong năm
                int dkCount = dt.AsEnumerable()
                    .Where(r => r.Field<int>("Nam") == year &&
                               (r.Field<string>("TrangThai") == "DaDangKy" || r.Field<string>("TrangThai") == "DaKham"))
                    .Sum(r => Convert.ToInt32(r["SoLuotKham"]));

                int huyCount = dt.AsEnumerable()
                    .Where(r => r.Field<int>("Nam") == year && r.Field<string>("TrangThai") == "DaHuy")
                    .Sum(r => Convert.ToInt32(r["SoLuotKham"]));

                dkData.Add(dkCount);
                huyData.Add(huyCount);
            }
            // total PAtient
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

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            loadFromToDate();
            ViewState["ThongKeMode"] = "FromToDate";
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            loadAll();
            ViewState["ThongKeMode"] = "All";
        }
        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            string mode = ViewState["ThongKeMode"]?.ToString();
            Response.Clear();
            Response.Buffer = true;
            
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            GridView_All.AllowPaging = false;
            string nameAction = checkMode(mode);
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeBenhNhan" + nameAction + ".xls");
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                GridView_All.RenderControl(hw); // Đổ GridView vào output HTML

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
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
            if(dt==null) return countDaHuy;
            foreach (DataRow row in dt.Rows)
            {
                //Get rows which is DaHuy
                string trangthai = row["TrangThai"].ToString();
                int luotKham = int.Parse(row["SoLuotKham"].ToString());
                if (trangthai == "DaHuy")
                {
                    countDaHuy += luotKham;
                }
            }
            return countDaHuy;
        }
        public int countDaDangKy(DataTable dt)
        {

            int countDaDangKy = 0;
            if (dt == null) return countDaDangKy;
            // Count Trang Thai Da Huy
            foreach (DataRow row in dt.Rows)
            {
                //Get rows which is DaHuy
                string trangthai = row["TrangThai"].ToString();
                int luotKham = int.Parse(row["SoLuotKham"].ToString());
                if (trangthai == "DaDangKy"|| trangthai == "DaKham")
                {
                    countDaDangKy += luotKham;
                }
            }
            return countDaDangKy;
        }

        public string checkMode(string mode)
        {
            if (mode == "ThisYear")
            {
                loadCurrentYear();
                return "NamNay";
            }
            else if (mode == "ThisMonth")
            {
                loadThisMonth();
                return "ThangNay";
            }
            else if (mode == "ThisWeek")
            {
                loadThisWeek();
                return "TuanNay";
            }
            else if (mode == "Today")
            {
                loadToday();
                return "HomNay";
            }
            else if (mode == "FromToDate")
            {
                loadFromToDate();
                return "KhoangThoiGian";
            }
            else if (mode == "All")
            {
                loadAll();
                return "TatCa";
            }
            
                // set default view if there are no action
                loadCurrentYear();
                return "NamNay";
            

        }
        


        public override void VerifyRenderingInServerForm(Control control)
        {
            // Bắt buộc phải override nếu export GridView
        }




    }
}