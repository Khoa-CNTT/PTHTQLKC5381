<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Bac_Si.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Quan_Ly_Bac_Si" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
  <link href="../../style/manager/doctor_register.css" rel='stylesheet'>
  <link href="../../style/manager/doctor_list_style.css" rel='stylesheet'>
  <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div id="container_regist">
       <%-- Button --%>
       <div id="cont_btn">
           <div class="btn_regist_dt"><a href="#">Đăng ký bác sĩ</a></div>
           <div class="btn_list"><a href="#">Danh sách bác sĩ</a></div>
       </div>

       <%--====================     Regist Form ========================== --%>
       <%--====================     Regist Form ========================== --%>
       <div id="regist_here">
           <div id="regist_form">
               <h2>ĐĂNG KÝ BÁC SĨ</h2>
               <%-- =======inp username====== --%>
               <div class="inp_mk">
                   <asp:TextBox ID="txtUsername" placeholder="Tên đăng nhập" runat="server"></asp:TextBox>
               </div>
               <%-- <asp:RequiredFieldValidator ControlToValidate="txtUsername" CssClass="cl_red" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>--%>
               <%-- =======inp email====== --%>
               <div class="inp_mk">
                   <asp:TextBox ID="txtEmail" placeholder="Email" runat="server"></asp:TextBox>
               </div>
               <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                   ErrorMessage="Vui lòng nhập email" Operator="NotEqual" CssClass="cl_red"
                   ValueToCompare=""></asp:CompareValidator>
               <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" Display="Dynamic" runat="server" ForeColor="Red"
                   ErrorMessage="Địa chỉ email không hợp lệ"
                   ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$">Địa chỉ email không hợp lệ
               </asp:RegularExpressionValidator>
               <%--<asp:RequiredFieldValidator ControlToValidate="txtEmail" CssClass="cl_red" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>--%>
               <div class="inp_mk">
                   <%-- =======inp specialist====== --%>
                   <asp:DropDownList ID="ddlChuyenKhoa" CssClass="inp_specialists" runat="server">
                   </asp:DropDownList>

               </div>
               <asp:CompareValidator ID="cv1" runat="server" ControlToValidate="ddlChuyenKhoa" Display="Dynamic"
                   ErrorMessage="Vui lòng chọn khoa" Operator="NotEqual" CssClass="cl_red"
                   ValueToCompare="Chọn chuyên khoa"></asp:CompareValidator>
               <%-- =======inp password====== --%>
               <div class="inp_mk">
                   <asp:TextBox ID="txtPassword" placeholder="Mật khẩu" TextMode="Password" runat="server"></asp:TextBox>
                   <asp:RegularExpressionValidator ControlToValidate="txtPassword" runat="server" ErrorMessage="Mật khẩu phải có ít nhất 10 kí tự, bao gồm 1 chữ cái in hoa và 1 ký tự đặc biệt" Display="Dynamic" ValidationExpression="^(?=.*[A-Z])(?=.*[\W]).{10,}$" ForeColor="Red"></asp:RegularExpressionValidator>
               </div>
               <%-- <asp:RequiredFieldValidator ControlToValidate="txtPassword" CssClass="cl_red" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>--%>
               <%-- =======  inp reEnter password  ======= --%>
               <div class="inp_mk">
                   <asp:TextBox ID="txtReEnterPassword" placeholder="Nhập lại mật khẩu" TextMode="Password" runat="server"></asp:TextBox>
               </div>
               <%-- <asp:RequiredFieldValidator ControlToValidate="txtReEnterPassword" CssClass="cl_red" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>--%>
               <asp:CompareValidator ControlToValidate="txtReEnterPassword" ControlToCompare="txtPassword" ErrorMessage="Không thể để trống" Display="Dynamic" runat="server">Mật khẩu không khớp vui lòng nhập lại</asp:CompareValidator>
               <%-- =======inp vai tro====== --%>
               <div class="inp_mk">
                   <asp:DropDownList ID="ddlVaiTro" CssClass="inp_specialists" runat="server">
                       <asp:ListItem Value="" Text="Chọn vai trò"></asp:ListItem>
                       <asp:ListItem Value="Offline" Text="Offline"></asp:ListItem>
                       <asp:ListItem Value="Online" Text="Online"></asp:ListItem>
                   </asp:DropDownList>
               </div>
               <%-- <asp:RequiredFieldValidator ControlToValidate="txtReEnterPassword" CssClass="cl_red" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>--%>

               <div class="inp_submit">
                   <div class="inp_btn">
                       <asp:Button ID="btnRegister" runat="server" Text="ĐĂNG KÝ" OnClick="btnRegister_Click" />
                   </div>
               </div>
           </div>
       </div>
       <%-- ============================================ --%>

       <%-- ======================      List dotor           ====================== --%>
       <%-- ======================      List dotor           ====================== --%>
       <asp:HiddenField runat="server" ID="lastSortDirection" />
       <asp:HiddenField runat="server" ID="lastSortExpression" />
       <div id="list_here">

           <div class="d_flex">
               <div class="seach_bar">
                   <asp:TextBox ID="txt_Searching" placeholder="Tìm kiếm" runat="server"></asp:TextBox>
                   <asp:LinkButton ID="btn_Search" CssClass="btn_search" OnClick="btn_Search_Click" runat="server"><i class="fa-solid fa-magnifying-glass"></i> </asp:LinkButton>
               </div>
               <div class="filter_bar">
                   <asp:DropDownList OnSelectedIndexChanged="ddl_Specialty_SelectedIndexChanged"
                       CssClass="pd_tb_2px ddl_filter" ID="filter_specialty" runat="server"
                       AutoPostBack="true">
                   </asp:DropDownList>

               </div>
           </div>
           <div class="load_list">
               <asp:LinkButton ID="LinkButton1" title="Load lại danh sách" CssClass="refresh_btn" OnClick="LinkButton1_Click1" runat="server"><i class="fa-solid fa-rotate-right"></i></asp:LinkButton>
           </div>

           <div class="radius-2em">
               <div class="table_doctor">
                   <asp:GridView ID="gridDoctor" runat="server" AutoGenerateColumns="False" CellPadding="4"
                       OnRowEditing="gridDoctor_RowEditing" OnRowCancelingEdit="gridDoctor_RowCancelingEdit"
                       OnRowDataBound="gridDoctor_RowDataBound" OnRowUpdating="GridDoctor_RowUpdating"
                       AllowSorting="True" OnSorting="gridDoctor_Sorting"
                       OnPageIndexChanging="gridDoctor_PageIndexChanging" AllowPaging="true" PageSize="5"
                       OnRowDeleting="gridDoctor_RowDeleting" CssClass="gridview-table" ForeColor="#4d4c4c" GridLines="None">
                       <AlternatingRowStyle BackColor="White" />

                       <Columns>

                           <%-- ==============     ID    =============== --%>
                           <asp:TemplateField HeaderText="ID" SortExpression="IDBacSi">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_ID" title='<%#Eval("IDBacSi") %>' runat="server" Text='<%#Eval("IDBacSi") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>


                           <%-- ==============     Họ tên    =============== --%>
                           <asp:TemplateField HeaderText="Họ Tên" SortExpression="HoTen">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_Name" title='<%#Eval("HoTen") %>' runat="server" Text='<%#Eval("HoTen") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txt_Name" runat="server" Text='<%#Bind("HoTen") %>'></asp:TextBox>
                               </EditItemTemplate>
                           </asp:TemplateField>



                           <%-- ==============     Chuyên Khoa    =============== --%>

                           <asp:TemplateField HeaderText="Chuyên Khoa" SortExpression="IDChuyenKhoa">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_Specialty" title='<%#Eval("TenChuyenKhoa") %>' runat="server" Text='<%#Eval("TenChuyenKhoa") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:DropDownList CssClass="pd_tb_2px" ID="ddl_Specialty" runat="server"></asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <%-- ==============     Địa chỉ pk    =============== --%>
                           <asp:TemplateField HeaderText="Địa Chỉ Phòng Khám" SortExpression="DiaChiPhongKham">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_Address" title='<%#Eval("DiaChiPhongKham") %>' runat="server" Text='<%#Eval("DiaChiPhongKham") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txt_Address" runat="server" Text='<%#Bind("DiaChiPhongKham") %>'></asp:TextBox>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <%-- ==============     SĐT    =============== --%>
                           <asp:TemplateField HeaderText="Số Điện Thoại" SortExpression="SoDienThoai">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_Phone" title='<%#Eval("SoDienThoai") %>' runat="server" Text='<%#Eval("SoDienThoai") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txt_Phone" runat="server" Text='<%#Bind("SoDienThoai") %>'></asp:TextBox>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <%-- ==============     Email    =============== --%>
                           <asp:TemplateField HeaderText="Email" SortExpression="Email">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_Email" title='<%#Eval("Email") %>' runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txt_Email" runat="server" Text='<%#Bind("Email") %>'></asp:TextBox>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <%-- ==============     Trinh đo    =============== --%>
                           <asp:TemplateField HeaderText="Trình Độ" SortExpression="TrinhDo">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_level" title='<%#Eval("TrinhDo") %>' runat="server" Text='<%#Eval("TrinhDo") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txt_Level" runat="server" Text='<%#Bind("TrinhDo") %>'></asp:TextBox>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <%-- ==============     Vai  tro    =============== --%>
                           <asp:TemplateField HeaderText="Vai Trò" SortExpression="VaiTro">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_Duty" runat="server" Text='<%#Eval("VaiTro") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:DropDownList CssClass="pd_tb_2px" ID="ddl_Duty" runat="server"></asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <%-- ==============    Hinh anh   =============== --%>

                           <asp:TemplateField HeaderText="Hình ảnh">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_Img" title='<%#Eval("HinhAnh") %>' runat="server" Text='<%#Eval("HinhAnh") %>'></asp:Label>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:FileUpload ID="up_Img" runat="server" />
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <%-- ==============     Btn    =============== --%>
                           <%-- ==============     Btn    =============== --%>

                           <asp:TemplateField ShowHeader="False">
                               <ItemTemplate>
                                   <asp:LinkButton ID="EditButton"
                                       runat="server"
                                       CssClass="EditButton"
                                       CommandName="Edit"
                                       Text="Sửa" />
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <div class="btn_edit">
                                       <div class="btn_update">
                                           <asp:LinkButton ID="UpdateButton"
                                               runat="server"
                                               CssClass="UpdateButton"
                                               CommandName="Update"
                                               Text="Cập nhật" />
                                       </div>
                                       <div class="btn_update">
                                           <asp:LinkButton ID="Cancel"
                                               runat="server"
                                               CssClass="CancelButton"
                                               CommandName="Cancel"
                                               Text="Hủy" />
                                       </div>
                                   </div>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField>
                               <ItemTemplate>
                                   <div class="btn_delete">
                                       <asp:LinkButton ID="DeleteButton"
                                           CssClass="DeleteButton"
                                           Text="Xóa"
                                           CommandName="Delete"
                                           runat="server" />
                                   </div>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                       <PagerStyle BackColor="" ForeColor="" HorizontalAlign="Left" CssClass="pagination" />
                   </asp:GridView>
               </div>
           </div>
       </div>
   </div>


   <script src="../../js/doctorCreateAccount.js"></script>
</asp:Content>
