using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.pages.Manager
{
    public partial class Quan_Ly_Bac_Si : System.Web.UI.Page
    {
        LopKetNoi kn = new LopKetNoi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadSpecialist();
                filter_Doctor_RowDataBound();
                viewList();

            }
        }

        //========Danh sách chuyên khoa
        public void loadSpecialist()
        {
            string sql_specialist = "select * from ChuyenKhoa";
            DataTable chuyenkhoa_DT = kn.docdulieu(sql_specialist, new SqlParameter[] { });
            ddlChuyenKhoa.DataSource = chuyenkhoa_DT;
            ddlChuyenKhoa.DataTextField = "TenChuyenKhoa";
            ddlChuyenKhoa.DataValueField = "IDChuyenKhoa";
            ddlChuyenKhoa.DataBind();
            ddlChuyenKhoa.Items.Insert(0, new ListItem("Chọn chuyên khoa", ""));


        }


        //============================
        //============================Đăng ký bác sĩ
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;

            string checkUserQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] checkParams = { new SqlParameter("@TenDangNhap", username) };
            DataTable dt = kn.docdulieu(checkUserQuery, checkParams);

            //kiểm tra tồn tại Bác sĩ
            if (dt != null && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                string message = "Tên đăng nhập đã tồn tại";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                return;
            }
            // Tạo ID mới cho bs
            string newId = "BS" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            // Thêm tài khoản 
            string insertUserQuery = "INSERT INTO TaiKhoan (ID, TenDangNhap, MatKhau, Email) VALUES (@ID, @TenDangNhap, @MatKhau, @Email)";
            SqlParameter[] insertUserParams = {
            new SqlParameter("@ID", newId),
            new SqlParameter("@TenDangNhap", username),
            new SqlParameter("@MatKhau", password),
            new SqlParameter("@Email", email) };
            int result = kn.CapNhat(insertUserQuery, insertUserParams);


            // =======Thêm Thông tin nếu dùng form đăng ký
            // thêm thông tin vào bảng bacsi
            string insertDoctorQuery = "insert into bacsi (IDBacSi, HoTen, ChuyenKhoaID,DiaChiPhongKham, SoDienthoai,Email , trinhdo, vaitro, hinhanh) " +
                "values (@IDBacsi, '', @chuyenkhoaid, @diachiphongkham, @sodienthoai, @email, @trinhdo, @vaitro,'')";
            SqlParameter[] doctorParams = {
            new SqlParameter("@IDBacSi", newId),

            new SqlParameter("@chuyenkhoaid",ddlChuyenKhoa.SelectedItem.Value), /*chuyên khoa phải được tạo để adđ*/
            new SqlParameter("@diachiphongkham", ""),
            new SqlParameter("@sodienthoai", ""),
            new SqlParameter("@email", email),
            new SqlParameter("@trinhdo", ""),
            new SqlParameter("@vaitro", ddlVaiTro.SelectedItem.Value)

            };
            //========check và cập nhật thông tin đăng ký
            if (kn.CapNhat(insertDoctorQuery, doctorParams) != 0)
            {
                string message = "Đăng ký thành công";
                string script = "showAlert('" + message + "','success');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
                viewList();
            }
            else
            {
                //=======Nếu không thêm vào được thì xóa user không thêm đc
                string sql_delete = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap;";
                SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@TenDangNhap", username) };
                int a = kn.CapNhat(sql_delete, para);
                string message = "Không thêm được do thiếu thông tin";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);



            }

        }
        //============================Đăng ký bác sĩ==============
        //======================================================

        //======================================================
        //============================Danh sách bác sĩ==============

        //View danh sách
        public void viewList()
        {
            string query_list = "select * from BacSi b , ChuyenKhoa c where c.IDChuyenKhoa = b.ChuyenKhoaID";
            SqlParameter[] sp = new SqlParameter[] { };
            DataTable ds = kn.docdulieu(query_list, sp);
            if (ds.Rows.Count > 0 && ds != null)
            {
                gridDoctor.DataSource = ds;
                gridDoctor.DataBind();
            }
        }



        //==============Cột Grid view==============

        //==============Edit Grid view==============
        protected void gridDoctor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridDoctor.EditIndex = e.NewEditIndex;
            viewList();
        }


        //==============RowDataBound droplist==============
        protected void gridDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                DropDownList ddl_Duty = (DropDownList)e.Row.FindControl("ddl_Duty");
                DropDownList ddl_Specialty = (DropDownList)e.Row.FindControl("ddl_Specialty");
                if (ddl_Specialty != null)
                {
                    //==========Load vai trò
                    ddl_Duty.Items.Insert(0, new ListItem("Offline", "Offline"));
                    ddl_Duty.Items.Insert(1, new ListItem("Online", "Online"));
                    //=========Load chuyên khoa vào dropdown
                    string get_list = "select * from ChuyenKhoa";

                    DataTable dt = kn.docdulieu(get_list, new SqlParameter[] { });
                    ddl_Specialty.DataSource = dt;
                    ddl_Specialty.DataTextField = "TenChuyenKhoa";
                    ddl_Specialty.DataValueField = "IdChuyenKhoa";
                    ddl_Specialty.DataBind();
                    // Set the selected value based on current data
                    string currentSpecialtyId = DataBinder.Eval(e.Row.DataItem, "ChuyenKhoaID").ToString();
                    ddl_Specialty.SelectedValue = currentSpecialtyId;
                }
            }

        }
        //==============Update item Grid view (must be called in update btn)==============
        protected void GridDoctor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label id = gridDoctor.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            TextBox name = gridDoctor.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            TextBox address = gridDoctor.Rows[e.RowIndex].FindControl("txt_Address") as TextBox;
            TextBox phone = gridDoctor.Rows[e.RowIndex].FindControl("txt_Phone") as TextBox;
            TextBox email = gridDoctor.Rows[e.RowIndex].FindControl("txt_Email") as TextBox;
            TextBox level = gridDoctor.Rows[e.RowIndex].FindControl("txt_Level") as TextBox;
            FileUpload upload = gridDoctor.Rows[e.RowIndex].FindControl("up_Img") as FileUpload;
            Label anhHidden = gridDoctor.Rows[e.RowIndex].FindControl("hidden_imgUrl") as Label;
            //File upload
            string img_String = "";

            if (upload.HasFile && upload!=null)
            {
                upload.SaveAs(Server.MapPath("~/img/" + upload.FileName));
                img_String = "~/img/" + upload.FileName;
            }
            else{
                img_String = anhHidden.Text+"";
            }

            
            //droplist
            DropDownList ddListDuty = gridDoctor.Rows[e.RowIndex].FindControl("ddl_Duty") as DropDownList;
            DropDownList ddList = gridDoctor.Rows[e.RowIndex].FindControl("ddl_Specialty") as DropDownList;
            //bind dropdown-list
            //update data
            string update_query = "update BacSi set HoTen = @HoTen,ChuyenKhoaId = @ChuyenKhoaId , " +
                "DiaChiPhongKham = @DiaChiPhongKham, " +
                "SoDienThoai = @SoDienThoai, " +
                "Email = @Email , " +
                "TrinhDo = @TrinhDo," +
                "VaiTro = @VaiTro," +
                "HinhAnh = @HinhAnh " +
                "where IDBacSi = @IDBacSi";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@IDBacSi", id.Text),
                new SqlParameter("@HoTen", name.Text),
                new SqlParameter("@ChuyenKhoaId", ddList.SelectedItem.Value),
                new SqlParameter("@DiaChiPhongKham", address.Text),
                new SqlParameter("@Email", email.Text),
                new SqlParameter("@SoDienThoai", phone.Text),
                new SqlParameter("@TrinhDo", level.Text),
                 new SqlParameter("@VaiTro", ddListDuty.SelectedItem.Value),
                 new SqlParameter("@HinhAnh", img_String)

            };
            int result = kn.CapNhat(update_query, param);
            //set -1 to cancel edit
            gridDoctor.EditIndex = -1;
            //Trả về xem bảng
            viewList();
        }

        //==============Delete item Grid view.==============
        protected void gridDoctor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label id = gridDoctor.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            string doctorID = id.Text;
            string deleteBS = "delete from bacsi where IDBacSi= @IDBacSi ";
            string deleteTK = "delete from taikhoan where ID= @IDBacSi";
            string deleteAppointmentDoc = "delete from LichKhambacSi where IDBacSi= @IDBacSi";
            string deleteAppointmentPa = "delete from PhieuKham where IDBacSi= @IDBacSi";

            SqlParameter[] bsId = new SqlParameter[]
            {
                new SqlParameter("@IDBacSi",doctorID)
            };

            SqlParameter[] bstk = new SqlParameter[]
            {
                new SqlParameter("@IDBacSi",doctorID)
            };
            SqlParameter[] apDoc = new SqlParameter[]
            {
                new SqlParameter("@IDBacSi",doctorID)
            };
            SqlParameter[] apPat = new SqlParameter[]
            {
                new SqlParameter("@IDBacSi",doctorID)
            };

            int resultLK = kn.CapNhat(deleteAppointmentDoc, apDoc);
            int resultPK = kn.CapNhat(deleteAppointmentPa, apPat);

            int resultBS = kn.CapNhat(deleteBS, bsId);
            int resultTK = kn.CapNhat(deleteTK, bstk);

            //int resultLK = kn.CapNhat(deleteAppointmentDoc, apDoc);
            //int resultPK = kn.CapNhat(deleteAppointmentPa, apPat);

            viewList();
        }
        //==============Cancel editing Grid view==============
        protected void gridDoctor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridDoctor.EditIndex = -1;
            viewList();
        }


       
        //==============sorting==============
        protected void gridDoctor_Sorting(object sender, GridViewSortEventArgs e)
        {
            string query_list = "select * from BacSi b , ChuyenKhoa c where c.IDChuyenKhoa = b.ChuyenKhoaID ";
            SqlParameter[] pr = new SqlParameter[] {

            };

            DataTable dataTable = kn.docdulieu(query_list, pr);


            if (lastSortExpression.Value == e.SortExpression.ToString())
            {
                if (lastSortDirection.Value == SortDirection.Ascending.ToString())
                {
                    e.SortDirection = SortDirection.Descending;
                }
                else
                {
                    e.SortDirection = SortDirection.Ascending;
                }
                lastSortDirection.Value = e.SortDirection.ToString();
                lastSortExpression.Value = e.SortExpression;
            }
            else
            {
                lastSortExpression.Value = e.SortExpression;
                e.SortDirection = SortDirection.Ascending;
                lastSortDirection.Value = e.SortDirection.ToString();
            }

            DataView dv = dataTable.DefaultView;
            if (e.SortDirection == SortDirection.Ascending)
            {
                dv.Sort = e.SortExpression;
            }
            else
            {
                dv.Sort = e.SortExpression + " DESC";
            }


            dataTable = dv.ToTable();
            gridDoctor.DataSource = dataTable.DefaultView;
            gridDoctor.DataBind();

        }
        //==============paging==============
        protected void gridDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string query_list = "select * from BacSi b , ChuyenKhoa c where c.IDChuyenKhoa = b.ChuyenKhoaID ";
            SqlParameter[] pr = new SqlParameter[] {

            };
            DataTable dataTable = kn.docdulieu(query_list, pr);
            gridDoctor.DataSource = dataTable;
            gridDoctor.PageIndex = e.NewPageIndex;
            gridDoctor.DataBind();
        }



        //
        public void filter_Doctor_RowDataBound()
        {
            // Load chuyên khoa vào dropdown
            string get_list = "select * from ChuyenKhoa";
            DataTable dt = kn.docdulieu(get_list, new SqlParameter[] { });
            filter_specialty.DataSource = dt;
            filter_specialty.DataTextField = "TenChuyenKhoa";
            filter_specialty.DataValueField = "IdChuyenKhoa";
            filter_specialty.DataBind();
            filter_specialty.Items.Insert(0, new ListItem("Chọn chuyên khoa", ""));

            // Set the selected value based on current data

        }


        //==============Filter==============
        public void viewList_Filter(string specialty)
        {

            string query_list = "select * from BacSi b , ChuyenKhoa c where c.IDChuyenKhoa = b.ChuyenKhoaID and b.ChuyenKhoaID = @ChuyenKhoaID";
            SqlParameter[] sp = new SqlParameter[] {
        new SqlParameter("@ChuyenKhoaID",specialty)};
            DataTable ds = kn.docdulieu(query_list, sp);
            if (ds.Rows.Count > 0 && ds != null)
            {
                gridDoctor.DataSource = ds;
                gridDoctor.DataBind();

            }
            else
            {
                string js = "showAlert('Không có thông tin tương ứng');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", js, true);

            }

        }

        protected void ddl_Specialty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_item = filter_specialty.SelectedValue;
            if (selected_item == "") viewList();
            else
                viewList_Filter(selected_item);
        }

        //==============Searching==============
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string nameKey = "%" + txt_Searching.Text + "%";
            string sql_search = "SELECT * FROM BacSi b, ChuyenKhoa c, TaiKhoan t " +
                "WHERE b.ChuyenKhoaID = c.IDChuyenKhoa and t.ID = b.IDBacSi " +
                "and(b.HoTen COLLATE SQL_Latin1_General_CP1_CI_AI like @name " +
                "or c.TenChuyenKhoa COLLATE SQL_Latin1_General_CP1_CI_AI like @name " +
                "or t.TenDangNhap COLLATE SQL_Latin1_General_CP1_CI_AI like  @name " +
                "or b.Email COLLATE SQL_Latin1_General_CP1_CI_AI like @name)";
            SqlParameter[] pr = new SqlParameter[]
            {
                new SqlParameter("@name",nameKey)

            };
            DataTable dt = kn.docdulieu(sql_search, pr);
            if (dt.Rows.Count > 0 && dt != null)
            {
                gridDoctor.DataSource = dt;
                gridDoctor.DataBind();
            }
            else
            {
                string message = "Không tìm thấy dữ liệu";
                string script = "showAlert('" + message + "','warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
            }

        }


    }
}