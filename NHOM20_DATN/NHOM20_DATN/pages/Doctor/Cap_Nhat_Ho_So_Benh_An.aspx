<%@ Page Title="" Language="C#"  Async="true" MasterPageFile="~/MasterPage/Doctor_MasterPage.Master" AutoEventWireup="true" CodeBehind="Cap_Nhat_Ho_So_Benh_An.aspx.cs" Inherits="NHOM20_DATN.pages.Doctor.Cap_Nhat_Ho_So_Benh_An" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../style/doctor/medical_records.css" rel='stylesheet'>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="loading" style="display: none;">
  <div class="spinner"></div>
</div>
    <div class="title-hsba">
        <h1>Cập Nhật Hồ Sơ Bệnh Án</h1>
    </div>
    <div id="list_here">

        <div class="d_flex container_control">
            <%-- Search --%>
            <div class="seach_bar">
                <asp:TextBox ID="txt_Searching" placeholder="Tìm kiếm" runat="server"></asp:TextBox>
                <asp:LinkButton ID="btn_Search" CssClass="btn_search" OnClick="btn_Search_Click" runat="server"><i class="fa-solid fa-magnifying-glass"></i></asp:LinkButton>
            </div>
            <%-- Edit --%>
            <%--     <div class="btn_edit" id="btn_edit">
                <asp:LinkButton ID="btnEdit" OnClick="btnEdit_Click" runat="server">
                    <i class="fa-solid fa-pen-to-square"></i>
                    Sửa
                </asp:LinkButton>
            </div>--%>
            <%-- Cancel Edit --%>
            <%--    <div class="btn_cancelEdit" id="btn_cancelEdit">
                <asp:LinkButton ID="cancelEdit" Visible="false" OnClick="cancelEdit_Click" runat="server">
                    Hủy
                </asp:LinkButton>
            </div>--%>
        </div>


        <%-- ==========     List        =============== --%>
        <div class="table_medic">
            <asp:GridView ID="gridMedicalRecord" runat="server" AutoGenerateColumns="False" CellPadding="4"
                OnRowCommand="gridMedicalRecord_RowCommand"
                CssClass="gridview-table" ForeColor="#333333" GridLines="None"
                OnPageIndexChanging="gridMedicalRecord_PageIndexChanging" AllowPaging="true" PageSize="5">
                <Columns>
                    <%-- ==============     Check list  =============== --%>
                    <%-- ==============    edit  =============== --%>
                    <asp:TemplateField Visible="true">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditRow"
                                CommandArgument='<%#Eval("IDHS") +"#"+Eval("IDBN") +"#"+Eval("HoTen")+"#"+Eval("ChanDoan")+"#"+Eval("DonThuoc")+"#"+Eval("GhiChu")+"#"+Eval("IDPhieu") + "#"+Eval("HuongDieuTri")  %>'
                                CommandName="editSelect" runat="server">
                                <i class="fa-solid fa-pen-to-square" style="color: dodgerblue;"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- ==============     ID BenhNhan  =============== --%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IdBN" runat="server" Text='<%#Eval("IDBN") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     ID HS  =============== --%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IdHS" runat="server" Text='<%#Eval("IDHS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     ID Phieu Kham  =============== --%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IdPK" runat="server" Text='<%#Eval("IDPhieu") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     Họ tên    =============== --%>
                    <asp:TemplateField HeaderText="Họ tên">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Hoten" title='<%#Eval("HoTen") %>' runat="server" Text='<%#Eval("HoTen") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     Ngày khám   =============== --%>
                    <asp:TemplateField HeaderText="Ngày Khám">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Ngay" title='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngayK) ? ngayK.ToString("dd/MM/yyyy") : ""  %>' runat="server" Text='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngayKtxt) ? ngayKtxt.ToString("dd/MM/yyyy") : ""  %>'></asp:Label>
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- ==============     Chẩn đoán    =============== --%>
                    <asp:TemplateField HeaderText="Chẩn Đoán">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ChanDoan" title='<%#Eval("ChanDoan") %>' runat="server" Text='<%#Eval("ChanDoan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- ==============     Đơn thuốc    =============== --%>
                    <asp:TemplateField HeaderText="Đơn Thuốc">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DonThuoc" title='<%#Eval("DonThuoc") %>' runat="server" Text='<%#Eval("DonThuoc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     Ngày Cập Nhật    =============== --%>
                    <asp:TemplateField HeaderText="Ngày Cập Nhật">
                        <ItemTemplate>
                            <asp:Label ID="lbl_NgayCapNhat"
                                title='<%#DateTime.TryParse(Eval("NgayCapNhat")?.ToString(), out DateTime ngaycn) ? ngaycn.ToString("dd/MM/yyyy") : "" %>'
                                runat="server"
                                Text='<%#DateTime.TryParse(Eval("NgayCapNhat")?.ToString(), out DateTime ngaytxt) ? ngaytxt.ToString("dd/MM/yyyy") : "" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     Ghi Chú    =============== --%>
                    <asp:TemplateField HeaderText="Ghi Chú">
                        <ItemTemplate>
                            <asp:Label ID="lbl_GhiChu" title='<%#Eval("GhiChu") %>' runat="server" Text='<%#Eval("GhiChu") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--============== Xem thông tin bệnh nhân ===============--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDetail" runat="server" Text="Xem chi tiết"
                                CommandArgument='<%# Eval("IDBN") %>'
                                OnClick="btnDetail_Click" CssClass="btn-detail" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerStyle BackColor="" ForeColor="" HorizontalAlign="Left" CssClass="pagination" />
            </asp:GridView>
        </div>
    </div>

    <div id="detailModal" class="modal">
        <div class="modal-content">
            <span class="close">&times;</span>
            <h2 class="modal-title">Thông tin bệnh nhân</h2>
            <div id="patientDetails" runat="server"></div>
        </div>
    </div>
    <asp:HiddenField ID="hiddenIdBN" runat="server" />
    <asp:HiddenField ID="hiddenIdHS" runat="server" />
    <asp:HiddenField ID="hiddenHoTen" runat="server" />
    <asp:HiddenField ID="hiddenDonThuoc" runat="server" />
    <asp:HiddenField ID="hiddenChanDoan" runat="server" />
    <asp:HiddenField ID="hiddenGhiChu" runat="server" />

    <%-- Panel update --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div id="patientUpdate-container-dad">
        <div id="patientUpdate_container" class="">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
          
            <asp:Panel ID="pn_Update" runat="server" Visible="false" CssClass="pnl_Update">
                <div class="chTime_content">
                    <h2>Cập Nhật Hồ Sơ</h2>
                    <asp:TextBox ID="txtHS_edit" Visible="false" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtBN_edit" Visible="false" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtPK_edit" Visible="false" runat="server"></asp:TextBox>
                    <%-- inp hoten --%>
                    <div class="hoten-inp">
                        <span><b>Họ Tên</b></span>
                        <asp:TextBox ID="txtHoTen_edit" disabled runat="server"></asp:TextBox>
                    </div>
                    <div class="grid_2">
                        <%-- inp chandoan --%>
                        <div class="chandoan-inp">
                            <span><b>Chẩn Đoán</b></span>
                            <asp:TextBox ID="txtChanDoan_edit" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                            <asp:Button ID="btnGoiY" runat="server" Text="Gợi ý thuốc" OnClick="btnGoiY_Click" />
                        </div>
                        <%-- inp don thuoc --%>
                        <div class="donthuoc-inp">
                            <span><b>Đơn Thuốc</b></span>
                            <asp:TextBox ID="txtDonThuoc_edit" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <%-- inp huong dtr --%>
                    <div class="huongdtr-inp">
                        <span><b>Hướng điều trị</b></span>
                        <asp:TextBox ID="txtHuongDtr_edit" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
                    <%-- inp ghichu --%>
                    <div class="ghichu-inp">
                        <span><b>Ghi Chú</b></span>
                        <asp:TextBox ID="txtGhiChu_edit" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
                    <div class="contain_btn">
                        <asp:Button ID="btn_Save_Update" runat="server" OnClick="btn_Save_Update_Click" Text="Lưu" />
                        <asp:Button ID="btn_Close_Update" runat="server" OnClick="btn_Close_Update_Click" Text="Đóng" />
                    </div>
                </div>
     </asp:Panel>
          </ContentTemplate>
</asp:UpdatePanel>     
    </div>
      </div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function ShowAlert(notice, warn) {
            Swal.fire({
                title: notice,
                icon: warn,
                confirmButtonText: 'OK'
            });
        }
        function OpenForm() {
            const btn_open = document.querySelector("#patientUpdate_container");
            btn_open.classList.add("d_block");
        }
        function CloseForm() {
            const btn_open = document.querySelector("#patientUpdate_container");
            btn_open.classList.remove("d_block");
        }
        function showLoading() {
            var loading = document.getElementById("loading");
            if (loading) loading.style.display = "flex";
        }

        function hideLoading() {
            var loading = document.getElementById("loading");
            if (loading) loading.style.display = "none";
        }
        
        //const btn_goiy = document.querySelector("#ContentPlaceHolder1_btnGoiY");
        //btn_goiy.addEventListener("click",function(){
        ////    document.getElementById("<%= txtDonThuoc_edit.ClientID %>").value = "Đang phân tích...";
        //})
        window.addEventListener("load", hideLoading);

        document.addEventListener("DOMContentLoaded", function () {
            // LinkButton dạng __doPostBack
            //document.querySelectorAll("a").forEach(function (a) {
            //    a.addEventListener("click", function (e) {
            //        // Nếu là LinkButton do ASP.NET sinh ra (có __doPostBack)
            //        if (a.href && a.href.includes("__doPostBack")) {
            //            showLoading();
            //        }
            //    });
            //});

            // submit form
            document.querySelectorAll("form").forEach(function (form) {
                form.addEventListener("submit", function () {
                    showLoading();
                });
            });
        });

        //  UpdatePanel
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
    <script src="/js/medical_record.js"></script>

</asp:Content>
