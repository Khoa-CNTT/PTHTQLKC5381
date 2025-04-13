<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Doctor_MasterPage.Master" AutoEventWireup="true" CodeBehind="Dang_Ky_Lich_Kham.aspx.cs" Inherits="NHOM20_DATN.Dang_Ky_Lich_Kham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
       body {
    font-family: 'Arial', sans-serif;
    background-color: #f4f7f8;
    color: #333;
    line-height: 1.6;
}

.container {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 20px;
    margin-top: 20px;
}

.group {
    margin-bottom: 15px;
}
.form-wrapper {
    background-color: white;    
    padding: 30px;               
    border-radius: 10px;          
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);  
    max-width: 900px;            
    margin: 0 auto; 
   
}
.layout-wrapper {
    display: flex;
    gap: 10px; 
}

.info-wrapper {
    flex: 0.8;
    border-radius: 10px;
}

.form-wrapper {
    flex: 2;
    margin-left: 0; 
}

.form-wrapper {
    max-width: 900px;
    max-height:900px;
}

h2 {
    margin-top:10px;
            
    margin-bottom: 20px;          
    color: deepskyblue; 
    font-size:20px;
    font-weight:bold;
}
label {
    font-weight: bold;
    margin-bottom: 5px;
    display: inline-block;
    color: #555;
}
.control {
    width: 100%;
    padding: 12px;
    margin-top: 5px;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 16px;
    background-color: #f9f9f9;
    transition: all 0.3s ease;
}

.control:focus {
    border-color: #007bff;  
    box-shadow: 0 0 8px rgba(0, 123, 255, 0.25);
    outline: none;
}


.control:hover {
    border-color: #999;
}


.btn-primary {
    background-color: #007bff;
    color: white;
    padding: 10px 15px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 16px;
    transition: background-color 0.3s ease;
    margin-left:290px;
}

.btn-primary:hover {
    background-color: #0056b3;
}

.required {
    color: red;
}
h4{
    background-color:deepskyblue;
    border-radius:5px;
    padding :5px;
    color:white;
    font-weight:bold;
}

.medical-info{
    background-color:white;
    padding:5px;
     border-radius:15px;
}
.trong .hinhbs1{
    width:250px;
    height:220px;
    border-radius:5px;
   
}
.trong .hinhbs2{
    width:250px;
    height:220px;
    margin-top:10px;
    border-radius:5px;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <h2>Trang chủ > Đăng ký lịch khám</h2>

<div class="layout-wrapper">
    <div class="info-wrapper">
        <div class="medical-info">
            <h4>Thông tin cơ sở y tế</h4>
            <div class="trong">
                 <p><strong>Bệnh viện BANANA</strong></p>
                 <p>Cơ sở 220 Phan Thanh - Thành phố Đà Nẵng</p>
                <img class="hinhbs1" src="../../img/anhbs.jpg"/>
                <img class="hinhbs2" src="../../img/anhbs2.jpg"/>
            </div>
           
        </div>
    </div>

    <div class="form-wrapper">
        <div class="container">
            <div class="group">
                <label  Text="txtHoTen">Họ và tên <span class="required">*</span></label>
                <asp:TextBox ID="txtHoTen" CssClass="control" runat="server" Placeholder="Nhập họ và tên"></asp:TextBox>
            </div>
             <div class="group">
     <label Text="ddlNgayKham">Chọn ngày khám <span class="required">*</span></label>
     <asp:TextBox ID="txtNgayKham" CssClass="control" runat="server" TextMode="Date"></asp:TextBox>
 </div>
           
            <div class="group">
                <label  Text="txtSoDienThoai">Số điện thoại <span class="required">*</span></label>
                <asp:TextBox ID="txtSoDienThoai" CssClass="control" runat="server" Placeholder="Nhập số điện thoại"></asp:TextBox>
            </div>

           
                  <div class="group">
 <label >Buổi Khám <span class="required">*</span></label>
 <asp:DropDownList ID="ddlbuoikham" CssClass="control" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlbuoikham_SelectedIndexChanged">
     <asp:ListItem Text="Chọn buổi khám" Value=""></asp:ListItem>
     <asp:ListItem Text="Sáng" Value="Sáng"></asp:ListItem>
     <asp:ListItem Text="Chiều" Value="Chiều"></asp:ListItem>
     <asp:ListItem Text="Cả Ngày" Value="Cả Ngày"></asp:ListItem>
 </asp:DropDownList>
</div>
           <div class="group">
     <label  Text="txtEmail">Email <span class="required">*</span></label>
     <asp:TextBox ID="txtEmail" CssClass="control" runat="server" Placeholder="Nhập email"></asp:TextBox>
 </div>

            <div class="group">
                <label  Text="ddlTinhThanh">Chọn giờ khám <span class="required">*</span></label>
                                   <asp:DropDownList ID="DDLgiokham" runat="server" CssClass="control">
                                    <asp:ListItem Value=" " Text="Giờ Khám" >
                                    </asp:ListItem>
                                </asp:DropDownList>
            </div>
                     <div class="group">
    <label  Text="ddlGioiTinh">Giới tính <span class="required">*</span></label>
    <asp:DropDownList ID="ddlGioiTinh" CssClass="control" runat="server">
        <asp:ListItem Text="Chọn giới tính" Value=""></asp:ListItem>
        <asp:ListItem Text="Nam" Value="Nam"></asp:ListItem>
        <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
    </asp:DropDownList>
</div>

           

            <div class="group">
                <label  Text="txtDiaChi">Địa chỉ <span class="required">*</span></label>
                <asp:TextBox ID="txtDiaChi" CssClass="control" runat="server" Placeholder="Nhập địa chỉ"></asp:TextBox>
            </div>
        </div>

        <div class="group">
            <asp:Button ID="btnDangKy" CssClass="btn btn-primary" runat="server" Text="Đăng ký" OnClick="btnDangKy_Click" />
        </div>
    </div>
</div>
    <script>
        function showAlert(message, iconType) {
            Swal.fire({
                title: message,
                icon: iconType,
                confirmButtonText: 'OK'
            });
        }
    </script>
</asp:Content>
