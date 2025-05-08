using NHOM20_DATN.res.service;
using NHOM20_DATN.res.service.ThongKe;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Thong_Ke_Benh_Nhan_Tu_Van : System.Web.UI.Page
    {
        protected string labelsJson;
        protected string dkDataJson;
        ThongKeBenhNhanTuVanService tuVanService = new ThongKeBenhNhanTuVanService();
        PatientManagerment patientsService = new PatientManagerment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCurrentYear();
            }
        }
        //========================== Main function =================================
        public void LoadView(DataTable dt_list)
        {
            if (dt_list == null|| dt_list.Rows.Count<=0)
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

        //========================== GridView =================================
        public void loadCurrentYear()
        {
            DataTable dt_list = new DataTable();
            // Datatable list all patient TV current year
            dt_list = tuVanService.getAllInYear(DateTime.Now);
            // Datatable chart always display current year/
            DataTable dt_chart = new DataTable();
            dt_chart = tuVanService.countPatientCurrentYear();
            LoadView(dt_list);
            loadChartInYear(dt_chart);

        }
        public void loadThisMonth()
        {
            DataTable dt_list = new DataTable();
            // Datatable list all patient TV current year
            dt_list = tuVanService.getAllInMonth(DateTime.Now);
            // Datatable chart always display current year/
            DataTable dt_chart = new DataTable();
            dt_chart = tuVanService.countPatientCurrentMonth();
            LoadView(dt_list);
            loadChartInMonth(dt_chart);
        }
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
            dt_list = tuVanService.getFromTo(startOfWeek, endOfWeek);
            // count all patient this month
            DataTable dt_chart = new DataTable();
            dt_chart = tuVanService.countFromTo(startOfWeek, endOfWeek);
            // load view with this month
            LoadView(dt_list);
            loadChartInWeek(dt_chart);
        }
        public void loadToday()
        {
            DateTime today = DateTime.Today;

            DataTable dt_list = new DataTable();
            //list all patient order this month
            dt_list = tuVanService.getFromTo(today, today);
            // count all patient this month
            DataTable dt_chart = new DataTable();
            dt_chart = tuVanService.countFromTo(today, today);
            if (dt_list.Rows.Count <= 0)
            {
                string message = "Hôm nay chưa có ai đăng ký！ ";
                string script = "showAlert('" + message + "','info');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                LoadView(null);
                lbl_totalPatient.Text = "" + countAllPatient();
                lbl_allPk.Text = "" +countDaDK(null);
                return;
            }


            // load view with this month
            string todayString = DateTime.Today.ToString("yyyy-MM-dd");
            LoadView(dt_list);
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
            }
            else if (cachngay <= 0)
            {
                string message = "Vui lòng chọn từ quá khứ đến tương lai!";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }

            DataTable dt_list = new DataTable();
            dt_list = tuVanService.getFromTo(fromDate, toDate);
            DataTable dt_chart = new DataTable();
            dt_chart = tuVanService.countFromTo(fromDate, toDate);
            LoadView(dt_list);
            loadChartFromToDate(dt_chart, fromDate, toDate);
            return;
        }
        public void loadAll()
        {
            DataTable dt_list = new DataTable();
            // Datatable list all patient order current year
            dt_list = tuVanService.getAll();
            // Datatable chart always display current year/
            DataTable dt_chart = new DataTable();
            dt_chart = tuVanService.countAll();
            LoadView(dt_list);
            loadChartYear(dt_chart);
        }

        //========================== Chart =================================
        //========================== Chart count by year ==========================
        public void loadChartInYear(DataTable dt)
        {
            List<string> labels = new List<string>();
            List<int> data = new List<int>();

            for (int month = 1; month <= 12; month++)
            {
                labels.Add("Tháng " + month);
                DataRow[] dkRows = dt.Select($"Thang = {month}");
                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["LuotDangKy"]));
                data.Add(dkCount);

            }
            lbl_totalPatient.Text = "" + countAllPatient();
            // total Pk
            lbl_allPk.Text = "" + countDaDK(dt);
            // DaHuy Pk

            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            dkDataJson = js.Serialize(data);
        }
        //========================== Chart count by Month ==========================
        public void loadChartInMonth(DataTable dt)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> data = new List<int>();
            int daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                labels.Add("Ngày " + day);
                DataRow[] dkRows = dt.Select($"Ngay = {day}");
                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["LuotDangKy"]));
            }
            lbl_totalPatient.Text = "" + countAllPatient();
            // total Pk
            lbl_allPk.Text = "" + countDaDK(dt);
            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            dkDataJson = js.Serialize(data);
        }
        //================= Count Current week  ============

        public void loadChartInWeek(DataTable dt)
        {
            List<string> labels = new List<string>();
            List<int> huyData = new List<int>();
            List<int> dkData = new List<int>();

            DateTime today = DateTime.Today;
            int delta = DayOfWeek.Monday - today.DayOfWeek;
            if (delta > 0) delta -= 7; //check if sunday

            DateTime startOfWeek = today.AddDays(delta);
            DateTime endOfWeek = startOfWeek.AddDays(6);
            string[] thu = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" };

            for (int i = 0; i < 7; i++)
            {
                DateTime ngay = startOfWeek.AddDays(i);
                labels.Add(thu[i]);
                DataRow[] dkRows = dt.Select($"Ngay = '{ngay:yyyy-MM-dd}'");

                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["LuotDangKy"]));

            }
            lbl_totalPatient.Text = "" + countAllPatient();
            // total tv
            lbl_allPk.Text = "" + countDaDK(dt);



            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
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
                DataRow[] dkRows = dt.Select($"Ngay = '{day}' ");

                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["LuotDangKy"]));
            }
            lbl_totalPatient.Text = "" + countAllPatient();
            lbl_allPk.Text = "" + countDaDK(dt);
            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            dkDataJson = js.Serialize(dkData);
        }


        //========================== Chart count by Day ==========================
        public void loadChartFromToDate(DataTable dt, DateTime fromDate, DateTime toDate)
        {
            List<string> labels = new List<string>();
            List<int> dkData = new List<int>();
            // let run from date to date by add 1 day
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                string dayString = date.ToString("yyyy-MM-dd");
                labels.Add(date.ToString("dd/MM"));
                DataRow[] dkRows = dt.Select($"Ngay = '{dayString}'");
                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["LuotDangKy"]));
                dkData.Add(dkCount);
            }
            lbl_totalPatient.Text = "" + countAllPatient();
            lbl_allPk.Text = "" + countDaDK(dt);


            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            dkDataJson = js.Serialize(dkData);
        }


        //========================== Chart count by Year ==========================
        public void loadChartYear(DataTable dt)
        {
            List<string> labels = new List<string>();
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

                DataRow[] dkRows = dt.Select($"Nam = '{year}'");

                int dkCount = dkRows.Sum(r => Convert.ToInt32(r["LuotDangKy"]));
                dkData.Add(dkCount);

            }
            // total PAtient
            lbl_totalPatient.Text = "" + countAllPatient();
            // total Pk
            lbl_allPk.Text = "" + countDaDK(dt);
            JavaScriptSerializer js = new JavaScriptSerializer();
            labelsJson = js.Serialize(labels);
            dkDataJson = js.Serialize(dkData);
        }




        //========================== Paging =================================
        protected void GridView_All_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_All.PageIndex = e.NewPageIndex;
            string mode = ViewState["ThongKeTuVan"]?.ToString();
            checkMode(mode);
        }

        //===================================================================
        //========================== Button =================================

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            string mode = ViewState["ThongKeTuVan"]?.ToString();
            Response.Clear();
            Response.Buffer = true;

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            GridView_All.AllowPaging = false;
            string nameAction = checkMode(mode);
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeTuVan" + nameAction + ".xls");
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                GridView_All.RenderControl(hw); // Đổ GridView vào output HTML

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnThisYear_Click(object sender, EventArgs e)
        {
            loadCurrentYear();
            ViewState["ThongKeTuVan"] = "TV_ThisYear";
        }

        protected void btnThisMonth_Click(object sender, EventArgs e)
        {
            loadThisMonth();
            ViewState["ThongKeTuVan"] = "TV_ThisMonth";
        }

        protected void btnThisWeek_Click(object sender, EventArgs e)
        {
            loadThisWeek();
            ViewState["ThongKeTuVan"] = "TV_ThisWeek";
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            loadToday();
            ViewState["ThongKeTuVan"] = "TV_Today";
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            loadFromToDate();
            ViewState["ThongKeTuVan"] = "TV_FromToDate";
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            loadAll();
            ViewState["ThongKeTuVan"] = "TV_All";
        }


        //========================== Function =================================
        public string checkMode(string mode)
        {
            if (mode == "TV_ThisYear")
            {
                loadCurrentYear();
                return "NamNay";
            }
            else if (mode == "TV_ThisMonth")
            {
                loadThisMonth();
                return "ThangNay";
            }
            else if (mode == "TV_ThisWeek")
            {
                loadThisWeek();
                return "TuanNay";
            }
            else if (mode == "TV_Today")
            {
                loadToday();
                return "HomNay";
            }
            else if (mode == "TV_FromToDate")
            {
                loadFromToDate();
                return "KhoangThoiGian";
            }
            else if (mode == "TV_All")
            {
                loadAll();
                return "TatCa";
            }

            // set default view if there are no action
            loadCurrentYear();
            return "NamNay";


        }
        public int countAllPatient()
        {
            DataTable dt_Patient = new DataTable();
            // get All patient
            dt_Patient = patientsService.viewPatient();
            return dt_Patient.Rows.Count;
        }

        public int countDaDK(DataTable dt)
        {
            int countDK = 0;
            // Count Trang Thai dk
            if (dt == null) return countDK;
            foreach (DataRow row in dt.Rows)
            {
                int luotKham = int.Parse(row["LuotDangKy"].ToString());
                countDK += luotKham;
            }
            return countDK;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Bắt buộc phải override nếu export GridView
        }

        
    }
}