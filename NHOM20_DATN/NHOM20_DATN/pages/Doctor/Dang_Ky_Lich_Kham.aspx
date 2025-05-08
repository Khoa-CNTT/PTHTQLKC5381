<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Doctor_MasterPage.Master" AutoEventWireup="true" CodeBehind="Dang_Ky_Lich_Kham.aspx.cs" Inherits="NHOM20_DATN.Dang_Ky_Lich_Kham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
   <style>
        :root {
            --primary-color: #4361ee;
            --primary-light: #4895ef;
            --secondary-color: #3f37c9;
            --accent-color: #f72585;
            --dark-color: #1a1a2e;
            --light-color: #f8f9fa;
            --success-color: #4cc9f0;
            --text-color: #2b2d42;
            --border-radius: 10px;
            --box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
            --transition: all 0.4s cubic-bezier(0.175, 0.885, 0.32, 1.275);
        }

        body {
            font-family: 'Poppins', -apple-system, BlinkMacSystemFont, sans-serif;
            background-color: #f0f2f5;
            color: var(--text-color);
            line-height: 1.6;
        }

        .container {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 24px;
            margin-top: 24px;
            width:660px;
        }

        .group {
            margin-bottom: 24px;
            position: relative;
            animation: fadeInUp 0.6s ease-out;
        }

        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .form-wrapper {
            background-color: white;
            padding: 32px;
            border-radius: var(--border-radius);
            box-shadow: var(--box-shadow);
            max-width: 900px;
            margin: 0 auto;
            transform: translateY(0);
            transition: var(--transition);
        }

        .form-wrapper:hover {
            transform: translateY(-5px);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.15);
        }

        .layout-wrapper {
            display: flex;
            gap: 24px;
            
        }

        .info-wrapper {
            flex: 0.8;
            border-radius: var(--border-radius);
             margin-left:80px;
        }

        .form-wrapper {
            flex: 2;
            margin-left: 0;
        }

        h2 {
            margin: 16px 0 24px;
            color: var(--primary-color);
            font-size: 20px;
            font-weight: 600;
            position: relative;
            display: inline-block;
            margin-left:50px;
        }
        h2 {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    max-width: 90vw; /* Giới hạn chiều rộng tối đa */
    margin-left: 0; /* Bỏ căn lề trái */
    padding: 0 15px; /* Thêm padding hai bên */
}
       

        label {
            font-weight: 500;
            margin-bottom: 8px;
            display: block;
           color: var(--primary-color);
            font-size: 14px;
            font-weight: 600;
            letter-spacing: 0.5px;
            transition: var(--transition);
        }

        .control {
            width: 100%;
            padding: 14px 16px;
            margin-top: 6px;
            border: 2px solid #e9ecef;
            border-radius: var(--border-radius);
            font-size: 14px;
            background-color: white;
            transition: var(--transition);
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.05);
        }

        .control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 3px rgba(67, 97, 238, 0.2);
            outline: none;
            transform: translateY(-2px);
        }

        .control:hover {
            border-color: var(--primary-light);
        }

        .btn-primary {
            background-color: var(--primary-color);
            color: white;
            padding: 14px 28px;
            border: none;
            border-radius: var(--border-radius);
            cursor: pointer;
            font-size: 16px;
            font-weight: 500;
            transition: var(--transition);
            margin-left: 0;
            width: 100%;
            max-width: 200px;
            display: block;
            margin: 24px auto 0;
            box-shadow: 0 4px 14px rgba(67, 97, 238, 0.4);
            position: relative;
            overflow: hidden;
        }

        .btn-primary:hover {
            background-color: var(--secondary-color);
            transform: translateY(-3px);
            box-shadow: 0 10px 20px rgba(67, 97, 238, 0.3);
        }

        .btn-primary::after {
            content: '';
            position: absolute;
            top: 50%;
            left: 50%;
            width: 5px;
            height: 5px;
            background: rgba(255, 255, 255, 0.5);
            opacity: 0;
            border-radius: 100%;
            transform: scale(1, 1) translate(-50%);
            transform-origin: 50% 50%;
        }

        .btn-primary:focus:not(:active)::after {
            animation: ripple 1s ease-out;
        }

        @keyframes ripple {
            0% {
                transform: scale(0, 0);
                opacity: 0.5;
            }
            100% {
                transform: scale(20, 20);
                opacity: 0;
            }
        }

        .required {
            color: var(--accent-color);
            font-weight: bold;
        }

        .medical-info h4 {
            background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
            border-radius: var(--border-radius);
            padding: 14px 16px;
            color: white;
            font-weight: 600;
            margin: 0 0 16px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            text-align: center;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 15px;
        }

        .medical-info {
            background-color: white;
            padding: 20px;
            border-radius: var(--border-radius);
            box-shadow: var(--box-shadow);
            height: 100%;
            width: 300px;
            margin-left: -25px;
            transition: var(--transition);
        }

        .medical-info:hover {
            transform: translateY(-5px);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.15);
        }

        .trong .hinhbs1, .trong .hinhbs2 {
            width: 100%;
            height: 200px;
            border-radius: var(--border-radius);
            margin-bottom: 16px;
            object-fit: cover;
            transition: var(--transition);
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .trong .hinhbs1:hover, .trong .hinhbs2:hover {
            transform: scale(1.03) rotate(1deg);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }

        .trong p {
            margin-bottom: 12px;
            font-size: 14px;
            transition: var(--transition);
        }

        .trong p:hover {
            color: var(--primary-color);
        }

        .trong p strong {
            font-weight: 600;
            color: var(--primary-color);
        }

       
        .control[type="date"]::-webkit-calendar-picker-indicator {
            filter: invert(0.5);
            cursor: pointer;
            transition: var(--transition);
        }

        .control[type="date"]::-webkit-calendar-picker-indicator:hover {
            filter: invert(0.3);
        }

        select.control {
            appearance: none;
            background-image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='%234361ee' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3e%3cpolyline points='6 9 12 15 18 9'%3e%3c/polyline%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-position: right 16px center;
            background-size: 16px;
            padding-right: 40px;
        }

        
        @keyframes float {
            0% { transform: translateY(0px); }
            50% { transform: translateY(-10px); }
            100% { transform: translateY(0px); }
        }

        .floating {
            animation: float 6s ease-in-out infinite;
        }

      
    
@media (max-width: 767px) {
    /* Reset layout chính */
    #content {
        margin-left: 0 !important;
        width: 100% !important;
        padding: 15px !important;
    }

    .container {
        grid-template-columns: 1fr !important;
        width: 100% !important;
        gap: 15px !important;
        z-index:0;
       
    }

    .info-wrapper{
        display:none;
    }
    .control {
        width: 100% !important;
        max-width: none !important;
    }

   
    .medical-info .hinhbs1, 
    .medical-info .hinhbs2 {
        display: none !important;
    }

    .medical-info {
        width: 100% !important;
        margin-left: 0 !important;
        padding: 15px !important;
    }

    #DDLgiokham {
        width: 100% !important;
        max-width: 100% !important;
    }
}

@media (min-width: 768px) and (max-width: 991px) {
    .container {
        grid-template-columns: 1fr 1fr !important;
        width: 90% !important;
    }

    .medical-info {
        width: 90% !important;
    }
}
.group { position: relative; }
.hours-wrapper {
  max-height: 180px;            
  overflow-y: auto;            
  padding: 25px;
  border: 2px solid #e9ecef;   
  border-radius: var(--border-radius);
  background: white;
  box-shadow: 0 1px 3px rgba(0,0,0,0.05);
  transition: var(--transition);
   position: absolute;
  z-index: 10;
  background: white;
  width: calc(100% - 4px);
  margin-top:5px;

}
.hours-wrapper:hover {
  box-shadow: 0 4px 10px rgba(0,0,0,0.1);
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
     <asp:ListItem Text="Chọn buổi khám" Value="Chọn buổi khám"></asp:ListItem>
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
  <label>Chọn giờ khám <span class="required">*</span></label>
  <div class="hours-wrapper">
    <asp:CheckBoxList 
        ID="cblGiokham" 
        runat="server" 
        RepeatDirection="Horizontal" 
        RepeatLayout="Flow" 
        CssClass="checkbox-list">
    </asp:CheckBoxList>
  </div>
</div>
                     <div class="group">
   
    <div class="group">
    <label  Text="txtSoDienThoai">Trình độ <span class="required">*</span></label>
    <asp:TextBox ID="Txttrinhdo" CssClass="control" runat="server" Placeholder=""></asp:TextBox>
</div>

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
