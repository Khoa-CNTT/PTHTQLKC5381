<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Benh_Nhan.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Quan_Ly_Benh_Nhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../style/manager/patientManagerment.css" rel='stylesheet'>
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="list_here">

        <div class="d_flex crud_bar">
            <div class="seach_bar">
                <asp:TextBox ID="txt_searching" placeholder="Tìm Kiếm" runat="server"></asp:TextBox>
                <asp:LinkButton ID="btn_search" CssClass="btn_search" OnClick="btn_search_Click" runat="server">
                <i class="fa-solid fa-magnifying-glass"></i> 
                </asp:LinkButton>
            </div>

            <div class="btn_Containner">
                <%-- Add --%>
                <div class="btn_add">
                    <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" runat="server">
                        <i class="fa-solid fa-plus"></i>
                    Thêm
                    </asp:LinkButton>
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
                <%-- Delete --%>
                <div class="btn_delete">
                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" runat="server">
                        <i class="fa-regular fa-trash-can"></i>
                    Xóa
                    </asp:LinkButton>
                </div>
                <div class="deleteSelected">
                    <asp:LinkButton ID="deleteSelect" Visible="false" OnClick="deleteSelect_Click" runat="server">
                    Xóa
                    </asp:LinkButton>
                </div>
                <div class="cancelDelete">
                    <asp:LinkButton ID="cancelDelete" Visible="false" OnClick="cancelDelete_Click" runat="server">
                     Hủy
                    </asp:LinkButton>
                </div>





            </div>

        </div>
        <%-- List --%>
        <div class="table_doctor">
            <asp:GridView ID="gridPatientsManager" runat="server" AutoGenerateColumns="False" CellPadding="4"
                OnRowDataBound="gridPatientsManager_RowDataBound" OnRowCommand="gridPatientsManager_RowCommand"
                CssClass="gridview-table" ForeColor="#333333" GridLines="None"
                OnPageIndexChanging="gridPatientsManager_PageIndexChanging" AllowPaging="true" PageSize="5">
                <Columns>
                    <%-- ==============     Check list  =============== --%>
                    <%-- ==============    edit  =============== --%>

                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditRow"
                                CommandArgument='<%#Eval("IDBenhNhan") +","+Eval("HoTen")+","+Eval("NgaySinh")+","+Eval("SoDienThoai")+","+Eval("Email")+","+Eval("GioiTinh") %>'
                                CommandName="editSelect" runat="server">
                                <i class="fa-solid fa-pen-to-square" style="color: dodgerblue;"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============   checked delete  =============== --%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="checkDelete" AutoPostBack="true" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     ID BenhNhan  =============== --%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IdBN" runat="server" Text='<%#Eval("IDBenhNhan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- ==============     Họ tên    =============== --%>
                    <asp:TemplateField HeaderText="Họ tên">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Hoten" title='<%#Eval("HoTen") %>' runat="server" Text='<%#Eval("HoTen") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     Ngày sinh   =============== --%>
                    <asp:TemplateField HeaderText="Ngày Sinh">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Ngay" title='<%#(DateTime.Parse(Eval("NgaySinh").ToString())).ToString("dd/MM/yyyy") %>' runat="server" Text='<%#(DateTime.Parse(Eval("NgaySinh").ToString())).ToString("dd/MM/yyyy") %>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <%-- ==============     SĐT    =============== --%>
                    <asp:TemplateField HeaderText="SĐT">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SDT" title='<%#Eval("SoDienThoai") %>' runat="server" Text='<%#Eval("SoDienThoai") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- ==============     Email    =============== --%>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Email" title='<%#Eval("Email") %>' runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ==============     Giới tính    =============== --%>
                    <asp:TemplateField HeaderText="Giới tính">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Nam"
                                CssClass="lblNam"
                                title="Nam"
                                runat="server"
                                Text="Nam"
                                Visible='<%# Eval("GioiTinh").ToString() == "Nam" %>'>
                            </asp:Label>
                            <asp:Label ID="lbl_Nu"
                                CssClass="lblNu"
                                title="Nữ"
                                runat="server"
                                Text="Nữ"
                                Visible='<%# Eval("GioiTinh").ToString() == "Nu" %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Thao tác">
                     <ItemTemplate>
                         <asp:Button ID="btnDetail" runat="server" Text="Xem chi tiết" 
                             CommandArgument='<%# Eval("IDBenhNhan") %>' 
                             OnClick="btnDetail_Click" CssClass="btn-detail" />
                     </ItemTemplate>
                 </asp:TemplateField>



                </Columns>
                <PagerStyle BackColor="" ForeColor="" HorizontalAlign="Left" CssClass="pagination" />
            </asp:GridView>
            <div id="detailModal" class="modal">
                 <div class="modal-content">
                     <span class="close">&times;</span>
                     <h2 class="modal-title">Thông tin chi tiết bệnh nhân</h2>
                     <div id="patientDetails" runat="server"></div>
                 </div>
        </div>
        <asp:HiddenField ID="hiddenIdBN" runat="server" />
        <asp:HiddenField ID="hiddenName" runat="server" />
        <asp:HiddenField ID="hiddenNgaySinh" runat="server" />
        <asp:HiddenField ID="hiddenSDT" runat="server" />
        <asp:HiddenField ID="hiddenEmail" runat="server" />
        <asp:HiddenField ID="hiddenGT" runat="server" />

        <div id="patientAdd_container">
            <asp:Panel ID="pn_Add" runat="server" Visible="false" CssClass="pnl_Add">
                <div class="chTime_content">
                    <h2>Thêm bệnh nhân</h2>

                    <%-- IDBN --%>
                    <asp:TextBox ID="txtIDBenhNhan" Visible="false" runat="server"></asp:TextBox>
                    <div class="grid_2">
                        <%-- TK --%>
                        <div class="chandoan-inp">
                            <span><b>Tài Khoản</b></span>
                            <asp:TextBox ID="txtTK" placeholder="Tên tài khoản" runat="server"></asp:TextBox>
                        </div>
                        <%-- MK --%>
                        <div class="donthuoc-inp">
                            <span><b>Mật Khẩu</b></span>
                            <asp:TextBox ID="txtMK" placeholder="Mật khẩu" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <%-- Ten --%>
                    <div class="ghichu-inp">
                        <span><b>Tên</b></span>
                        <asp:TextBox ID="txtName" placeholder="Tên bệnh nhân" runat="server"></asp:TextBox>
                    </div>
                    <div class="grid_2">
                        <%-- SDT --%>
                        <div class="ghichu-inp">
                            <span><b>SĐT</b></span>
                            <asp:TextBox ID="txtSDT" type="number" placeholder="SĐT" runat="server"></asp:TextBox>
                        </div>
                        <%-- Email --%>
                        <div class="ghichu-inp">
                            <span><b>Email</b></span>
                            <asp:TextBox ID="txtEmail" placeholder="Email" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <%-- NgaySinh --%>
                    <div class="ghichu-inp">
                        <span><b>Ngày Sinh</b></span>
                        <asp:TextBox ID="txtNgaySinh" type="date" placeholder="Ngày Sinh" runat="server"></asp:TextBox>
                    </div>
                    <%-- GT --%>
                    <div class="gioitinh-inp" style="display: flex; align-items: center; gap: 10px;">
                        <span><b>Giới Tính</b></span>
                        <asp:RadioButtonList ID="radioGT" runat="server">
                            <asp:ListItem Value="Nam" Text="Nam"></asp:ListItem>
                            <asp:ListItem Value="Nu" Text="Nữ"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="contain_btn">
                        <div class="grid_2">
                            <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Lưu" />
                            <asp:Button ID="btn_Close" runat="server" OnClick="btn_Close_Click" Text="Đóng" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>



        <%-- Panel update --%>
        <div id="patientUpdate_container">
            <asp:Panel ID="pn_Update" runat="server" Visible="false" CssClass="pnl_Update">
                <div class="chTime_content">
                    <h2>Sửa thông tin</h2>
                    <asp:TextBox ID="txtIDBenhNhan_edit" Visible="false" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtName_edit" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtNgaySinh_edit" type="date" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtSDT_edit" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtEmail_edit" runat="server"></asp:TextBox>
                    <asp:RadioButtonList ID="radioGT_edit" runat="server">
                        <asp:ListItem Value="Nam" Text="Nam"></asp:ListItem>
                        <asp:ListItem Value="Nu" Text="Nữ"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Button ID="btn_Save_Update" runat="server" OnClick="btn_Save_Update_Click" Text="Lưu" />
                    <asp:Button ID="btn_Close_Update" runat="server" OnClick="btn_Close_Update_Click" Text="Đóng" />

                </div>
            </asp:Panel>
        </div>
    </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script>
    function showAlert(notice, warn) {
        Swal.fire({
            title: notice,
            icon: warn,
            confirmButtonText: 'OK'
        });
    }
</script>
    <script src="/js/patient_manager.js"></script>


    <script>

        // Lấy modal
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
    </script>



</asp:Content>
