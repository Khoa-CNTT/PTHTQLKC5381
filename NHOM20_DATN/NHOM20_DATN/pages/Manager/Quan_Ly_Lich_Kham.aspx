<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Lich_Kham.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Quan_Ly_Lich_Kham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../style/manager/ManagerAppointment_style.css" rel='stylesheet'>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="loading" style="display: none;">
  <div class="spinner"></div>
</div>
    <div id="container_content">
    <h1>Quản Lý Lịch Khám</h1>
        
    <div id="list_here">

    <div class="d_flex search_container">
        <div class="seach_bar">
            <asp:TextBox ID="txt_searching" placeholder="Tìm Kiếm" runat="server"></asp:TextBox>
            <asp:LinkButton ID="btn_search" CssClass="btn_search" OnClick="btn_search_Click" runat="server">
                <i class="fa-solid fa-magnifying-glass"></i> 
            </asp:LinkButton>
        </div>
        <div class="filter_bar">
            <asp:DropDownList OnSelectedIndexChanged="filter_specialty_SelectedIndexChanged"
                CssClass="pd_tb_2px ddl_filter" ID="filter_specialty" runat="server"
                AutoPostBack="true" >
                <asp:ListItem Text="Trạng Thái" Value=""></asp:ListItem>
                <asp:ListItem Text="Đã Khám" Value="DaKham"></asp:ListItem>
                <asp:ListItem Text="Đã Đăng Ký" Value="DaDangKy"></asp:ListItem>
                <asp:ListItem Text="Đã Hủy" Value="DaHuy"></asp:ListItem>
            </asp:DropDownList>

        </div>
    </div>
    


    <%-- List --%>
    <div class="table_doctor">
        <asp:GridView ID="gridAppointmentManager" runat="server" AutoGenerateColumns="False" CellPadding="4"
             OnRowCommand="gridAppointmentManager_RowCommand"
            CssClass="gridview-table" ForeColor="#333333" GridLines="None"
             OnPageIndexChanging="gridAppointmentManager_PageIndexChanging" AllowPaging="true" PageSize="5">
            <Columns>

                <%-- ==============     ID PK  =============== --%>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbl_IDPhieu" title='<%#Eval("IDPhieu") %>' runat="server" Text='<%#Eval("IDPhieu") %>'></asp:Label><br />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <%-- ==============     ID BS  =============== --%>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbl_IDBacsi" title='<%#Eval("IDBacsi") %>' runat="server" Text='<%#Eval("IDBacsi") %>'></asp:Label><br />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <%-- ==============     ID BN  =============== --%>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbl_IDBenhNhan" title='<%#Eval("IDBenhNhan") %>' runat="server" Text='<%#Eval("IDBenhNhan") %>'></asp:Label><br />
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- ==============     NGÀY GIỜ   =============== --%>
                <asp:TemplateField HeaderText="Ngày/Giờ">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Ngay" title='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngaycn) ? ngaycn.ToString("dd/MM/yyyy") : "" %>' runat="server" Text='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngaycn1) ? ngaycn1.ToString("dd/MM/yyyy") : ""%>'></asp:Label><br />
                        <asp:Label ID="lbl_ThoiGian" title='<%#(DateTime.Parse(Eval("ThoiGianKham").ToString())).ToString("HH:mm") %>' runat="server" Text='<%#(DateTime.Parse(Eval("ThoiGianKham").ToString())).ToString("HH:mm")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- ==============     Họ tên    =============== --%>
                <asp:TemplateField HeaderText="Họ tên">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Hoten" title='<%#Eval("Hoten") %>' runat="server" Text='<%#Eval("Hoten") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- ==============     SĐT    =============== --%>
                <asp:TemplateField HeaderText="SĐT">
                    <ItemTemplate>
                        <asp:Label ID="lbl_SDT" title='<%#Eval("SoDienThoai") %>' runat="server" Text='<%#Eval("SoDienThoai") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <%-- ==============     BS Khám    =============== --%>
                 <asp:TemplateField HeaderText="BS Khám">
                     <ItemTemplate>
                         <asp:Label ID="lbl_BSKham" title='<%#Eval("BSKham") %>' runat="server" Text='<%#Eval("BSKham") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                <%-- ==============     Trạng thái    =============== --%>
                <asp:TemplateField HeaderText="Trạng Thái">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DaDangKy"
                            CssClass="lblDadangky"
                            title="Đã Đăng Ký"
                            BackColor="#58abed"
                            ForeColor="White"
                            runat="server"
                            Text="Đã Đăng Ký"
                            Visible='<%# Eval("TrangThai").ToString() == "DaDangKy" %>'>
                        </asp:Label>
                        <asp:Label ID="lbl_ChoKham"
                            CssClass="lblDangcho"
                            title="Đã Khám"
                            BackColor="#25981B"
                            ForeColor="White"
                            runat="server"
                            Text="Đã Khám"
                            Visible='<%# Eval("TrangThai").ToString() == "DaKham" %>'>
                    </asp:Label>
                        <asp:Label ID="lbl_DaHuy"
                            CssClass="lblDahuy"
                            BackColor="#ff3e3e"
                            ForeColor="White"
                            title="Đã hủy"
                            runat="server"
                            Text="Đã hủy"
                            Visible='<%# Eval("TrangThai").ToString() == "DaHuy" %>'>
                     </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Đổi giờ --%>
                <asp:TemplateField HeaderText="Đổi giờ">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_ChangeTime" CommandArgument='<%#Eval("NgayKham") +","+ Eval("IDPhieu")+","+Eval("IDBacSi")+","+Eval("ThoiGianKham")%>' 
                            CommandName="DoiGio" runat="server">
                         <i class="fa-regular fa-clock"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Xem thông tin --%>
                <asp:TemplateField HeaderText="Thông Tin ">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_ViewDetail" CommandArgument='<%#Eval("IDBenhNhan")+","+ Eval("IDPhieu") %>' CommandName="XemTT" runat="server">
                             <i class="fa-solid fa-id-card"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Xóa --%>
                <asp:TemplateField Visible="true">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_Delete" CssClass="btn-delete" CommandArgument='<%#Eval("NgayKham") +","+ Eval("IDPhieu")+","+Eval("  IDBacsi") +","+ Eval("IDBenhNhan")%>' CommandName="Xoa" runat="server">
                             <i class="fa-regular fa-trash-can"></i>
                        </asp:LinkButton>                                       

                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
            <PagerStyle BackColor="" ForeColor="" HorizontalAlign="Left" CssClass="pagination" />
        </asp:GridView>
        <%-- View info --%>
            <div id="detailModal" class="modal">
         <div class="modal-content">
             <span class="close">&times;</span>
             <h2 class="modal-title">Thông tin chi tiết bệnh nhân</h2>
             <div id="patientDetails" runat="server"></div>
         </div>
</div>
    </div>
        <%-- HiddenValue --%>
    <asp:HiddenField ID="hiddenIdPk" runat="server" />
        <asp:HiddenField ID="hiddenDocID" runat="server" />
    <asp:HiddenField ID="hiddenOldDay" runat="server" />
    <asp:HiddenField ID="hiddenOldTime" runat="server" />


    <div id="chTime_container" class="">
        <asp:Panel ID="pn_AT" runat="server" Visible="false"  CssClass="pnl_AT">
            <div class="chTime_content">

                <h2>Đổi giờ khám phiếu: <br />
                    <asp:Label ID="lbl_idPk" ForeColor="#00a2ff" runat="server" Text=""></asp:Label>

                </h2>
                <div class="grid_2">
                <div class="wrapper_list">
                    <asp:DropDownList ID="ddl_aT"
                        runat="server"
                        onmousedown="this.size=5;"
                        onclick="this.size=5;"
                        onfocusout="this.size=1;"
                        ondblclick="this.size=1;"
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:TextBox ID="txtCalender" type="date"  OnTextChanged="txtCalender_TextChanged" CssClass="inp_Date" runat="server"></asp:TextBox>
                </div>
                    </div>
                
                <div class="grid_2">
                        <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Lưu" />
    <asp:Button ID="btn_Close" runat="server" OnClick="btn_Close_Click" Text="Đóng" />
                </div>
            </div>
        </asp:Panel>
    </div>
        </div>
     <%--   <div id="notice" class="notice d_none"  >
            <i class="fa-solid fa-xmark" onclick="close_notice()"></i>
            <b>Chú ý:</b>
            <p>Do tính chất công việc của bác sĩ nên quản lý chỉ có thể chọn được ngày ở tương lai!</p>
        </div>--%>

</div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        //function showAlert(titleValue, textValue, iconValue) {
        //    Swal.fire({
        //        title: titleValue,
        //        text: textValue,
        //        icon: iconValue
        //    });
        //}

        function showAlert(titleValue, iconValue) {
            Swal.fire({
                title: titleValue,
                icon: iconValue
            });
        }
        var modal = document.getElementById("detailModal");

        // Lấy nút đóng modal
        var span = document.getElementsByClassName("close")[0];

        // Khi click vào nút đóng, đóng modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // Khi click bất kỳ đâu ngoài modal, đóng modal
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }

        // Hàm hiển thị modal (có thể gọi từ code-behind)
        function showModal() {
            modal.style.display = "block";
        }
        function showLoading() {
            var loading = document.getElementById("loading");
            if (loading) loading.style.display = "flex";
        }

        function hideLoading() {
            var loading = document.getElementById("loading");
            if (loading) loading.style.display = "none";
        }

        // Khi load trang xong thì ẩn loading
        window.addEventListener("load", hideLoading);


        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll(".btn-delete").forEach(function (btn) {
                btn.addEventListener("click", function () {
                    showLoading();
                });
            });
            // LinkButton dạng __doPostBack
            //document.querySelectorAll("a").forEach(function (a) {
            //    a.addEventListener("click", function (e) {
            //        // Nếu là LinkButton do ASP.NET sinh ra (có __doPostBack)
            //        if (a.href && a.href.includes("__doPostBack")) {
            //            showLoading();
            //        }
            //    });
            //});

            // Bắt submit form
            document.querySelectorAll("form").forEach(function (form) {
                form.addEventListener("submit", function () {
                    showLoading();
                });
            });
            document.querySelectorAll("form").forEach(function (form) {
                form.addEventListener("submit", function () {
                    showLoading();
                });
            });
        });

        // Nếu dùng UpdatePanel
        if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                showLoading();
            });
            prm.add_endRequest(function () {
                hideLoading();
            });
        }
    </script>
    
    <script src="/js/appointment_manager.js">
    </script>
</asp:Content>
