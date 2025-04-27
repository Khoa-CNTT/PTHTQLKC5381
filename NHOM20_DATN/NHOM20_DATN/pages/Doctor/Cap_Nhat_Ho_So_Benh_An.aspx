<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Doctor_MasterPage.Master" AutoEventWireup="true" CodeBehind="Cap_Nhat_Ho_So_Benh_An.aspx.cs" Inherits="NHOM20_DATN.pages.Doctor.Cap_Nhat_Ho_So_Benh_An" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../style/doctor/medical_records.css" rel='stylesheet'>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="list_here">

        <div class="d_flex container_control">
            <%-- Search --%>
            <div class="seach_bar">
                <asp:TextBox ID="txt_Searching" placeholder="Tìm kiếm" runat="server"></asp:TextBox>
                <asp:LinkButton ID="btn_Search" CssClass="btn_search" OnClick="btn_Search_Click" runat="server"><i class="fa-solid fa-magnifying-glass"></i></asp:LinkButton>
            </div>
            <%-- Edit --%>
            <div class="btn_edit" id="btn_edit">
                <asp:LinkButton ID="btnEdit" OnClick="btnEdit_Click" runat="server">
                    <i class="fa-solid fa-pen-to-square"></i>
                    Sửa
                </asp:LinkButton>
            </div>
            <%-- Cancel Edit --%>
            <div class="btn_cancelEdit" id="btn_cancelEdit">
                <asp:LinkButton ID="cancelEdit" Visible="false" OnClick="cancelEdit_Click" runat="server">
                    Hủy
                </asp:LinkButton>
            </div>
        </div>


        <%-- ==========     List        =============== --%>
        <div class="table_medic">
            <asp:GridView ID="gridMedicalRecord" runat="server" AutoGenerateColumns="False" CellPadding="4"
                OnRowCommand="gridMedicalRecord_RowCommand"
                CssClass="gridview-table" ForeColor="#333333" GridLines="None"
                OnPageIndexChanging="gridMedicalRecord_PageIndexChanging" AllowPaging="true" PageSize="5">
                <columns>
                    <%-- ==============     Check list  =============== --%>
                    <%-- ==============    edit  =============== --%>
                    <asp:TemplateField Visible="false">
                        <itemtemplate>
                            <asp:LinkButton ID="btnEditRow"
                                CommandArgument='<%#Eval("IDHS") +","+Eval("IDBN") +","+Eval("HoTen")+","+Eval("ChanDoan")+","+Eval("DonThuoc")+","+Eval("GhiChu") %>'
                                CommandName="editSelect" runat="server">
                                <i class="fa-solid fa-pen-to-square" style="color: dodgerblue;"></i>
                            </asp:LinkButton>
                        </itemtemplate>
                    </asp:TemplateField>

                    <%-- ==============     ID BenhNhan  =============== --%>
                    <asp:TemplateField Visible="false">
                        <itemtemplate>
                            <asp:Label ID="lbl_IdBN" runat="server" Text='<%#Eval("IDBN") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <%-- ==============     ID HS  =============== --%>
                    <asp:TemplateField Visible="false">
                        <itemtemplate>
                            <asp:Label ID="lbl_IdHS" runat="server" Text='<%#Eval("IDHS") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <%-- ==============     Họ tên    =============== --%>
                    <asp:TemplateField HeaderText="Họ tên">
                        <itemtemplate>
                            <asp:Label ID="lbl_Hoten" title='<%#Eval("HoTen") %>' runat="server" Text='<%#Eval("HoTen") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <%-- ==============     Ngày khám   =============== --%>
                    <asp:TemplateField HeaderText="Ngày Khám">
                        <itemtemplate>
                            <asp:Label ID="lbl_Ngay" title='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngayK) ? ngayK.ToString("dd/MM/yyyy") : ""  %>' runat="server" Text='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngayKtxt) ? ngayKtxt.ToString("dd/MM/yyyy") : ""  %>'></asp:Label>
                            <br />
                        </itemtemplate>
                    </asp:TemplateField>

                    <%-- ==============     Chẩn đoán    =============== --%>
                    <asp:TemplateField HeaderText="Chẩn Đoán">
                        <itemtemplate>
                            <asp:Label ID="lbl_ChanDoan" title='<%#Eval("ChanDoan") %>' runat="server" Text='<%#Eval("ChanDoan") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>

                    <%-- ==============     Đơn thuốc    =============== --%>
                    <asp:TemplateField HeaderText="Đơn Thuốc">
                        <itemtemplate>
                            <asp:Label ID="lbl_DonThuoc" title='<%#Eval("DonThuoc") %>' runat="server" Text='<%#Eval("DonThuoc") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <%-- ==============     Ngày Cập Nhật    =============== --%>
                    <asp:TemplateField HeaderText="Ngày Cập Nhật">
                        <itemtemplate>
                            <asp:Label ID="lbl_NgayCapNhat" 
                                title='<%#DateTime.TryParse(Eval("NgayCapNhat")?.ToString(), out DateTime ngaycn) ? ngaycn.ToString("dd/MM/yyyy") : "" %>'
                                runat="server"
                                Text='<%#DateTime.TryParse(Eval("NgayCapNhat")?.ToString(), out DateTime ngaytxt) ? ngaytxt.ToString("dd/MM/yyyy") : "" %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <%-- ==============     Ghi Chú    =============== --%>
                    <asp:TemplateField HeaderText="Ghi Chú">
                        <itemtemplate>
                            <asp:Label ID="lbl_GhiChu" title='<%#Eval("GhiChu") %>' runat="server" Text='<%#Eval("GhiChu") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>

                </columns>
                <pagerstyle backcolor="" forecolor="" horizontalalign="Left" cssclass="pagination" />
            </asp:GridView>
        </div>
    </div>
    <asp:HiddenField ID="hiddenIdBN" runat="server" />
    <asp:HiddenField ID="hiddenIdHS" runat="server" />
    <asp:HiddenField ID="hiddenHoTen" runat="server" />
    <asp:HiddenField ID="hiddenDonThuoc" runat="server" />
    <asp:HiddenField ID="hiddenChanDoan" runat="server" />
    <asp:HiddenField ID="hiddenGhiChu" runat="server" />

    <%-- Panel update --%>
    <div id="patientUpdate_container" class="">
        <asp:Panel ID="pn_Update" runat="server" Visible="false" CssClass="pnl_Update">
            <div class="chTime_content">
                <h2>Cập Nhật Hồ Sơ</h2>
                <asp:TextBox ID="txtHS_edit" Visible="false" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtBN_edit" Visible="false" runat="server"></asp:TextBox>
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
                    </div>
                    <%-- inp don thuoc --%>
                    <div class="donthuoc-inp">
                        <span><b>Đơn Thuốc</b></span>
                        <asp:TextBox ID="txtDonThuoc_edit" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
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






    </script>



    <script src="/js/doctorCreateAccount.js"></script>
</asp:Content>
