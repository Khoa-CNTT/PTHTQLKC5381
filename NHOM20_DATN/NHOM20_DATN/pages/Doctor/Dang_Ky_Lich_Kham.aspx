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
       
            background-color: #f0f2f5;
            color: var(--text-color);
            line-height: 1.6;
        }
        #content{
                 font-family: 'Poppins', -apple-system, BlinkMacSystemFont, sans-serif;
        }

       .container {
    display: grid;
    grid-template-columns: repeat(2, minmax(0, 1fr));
    gap: 24px;
    margin-top: 24px;
    width: 100%;
    max-width: 900px;   
    margin-left: auto;
    margin-right: auto;
}

       .contain-dklk{
            background-color: #d9d9d96e;
       }

        .group {
            margin-bottom: 24px;
            position: relative;
           
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
        .form-wrapper {
    flex: 2;
    padding: 32px;
    max-width: none;
}


.form-wrapper {
    max-width: 1000px;
}


.group > .group {
    margin: 0;
    padding: 0;
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
            margin-top:200px;
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
    overflow-x:auto;          
    padding: 40px;
    border: 2px solid #e9ecef;   
    border-radius: var(--border-radius);
    background: white;
    box-shadow: 0 1px 3px rgba(0,0,0,0.05);
    transition: var(--transition);
    position: static;    
    width: 100%;
    margin-top: 6px;    
}
.hours-wrapper {
  position: absolute !important;   
  top: 37px; 
  left: 0 !important;
  width: 100% !important;           
  margin: 0 !important;             
  padding: 26px !important;        
  background: white !important;
  border: 2px solid #e9ecef !important;
  border-radius: var(--border-radius) !important;
  box-shadow: 0 4px 10px rgba(0,0,0,0.1) !important;
  max-height: 200px !important;
  
  z-index: 9999 !important;    
  
}
.hours-group {
  position: relative;    
  overflow: visible;      
  grid-column: 1 / -1;    
  margin-bottom: 24px;
}
.hours-group .hours-wrapper {
  position: absolute;
  top: 100%;             
  left: 0;
  width: 100%;
  max-height: 180px;
  
  padding: 12px;
  background: white;
  border: 2px solid #e9ecef;
  border-radius: var(--border-radius);
  box-shadow: 0 4px 10px rgba(0,0,0,0.1);
  z-index: 9999;         
}



.hours-wrapper .checkbox-list span {
  width: calc(20% - 10px);
  display: flex;
  align-items: center;
}
.hours-wrapper .checkbox-list {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  gap: 12px;
  padding: 12px;
}

.hours-wrapper .checkbox-list input[type="checkbox"] {
  display: none;
}

.hours-wrapper .checkbox-list label {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 10px 6px;
  background: #f0f2f5;
  border: 2px solid transparent;
  border-radius: var(--border-radius);
  cursor: pointer;
  font-size: 14px;
  transition: var(--transition);
  user-select: none;
  
}

.hours-wrapper .checkbox-list label:hover {
  background: var(--primary-light);
  color: white;
}


.hours-wrapper .checkbox-list input[type="checkbox"]:checked + label {
  background: var(--primary-color);
  color: white;
  border-color: var(--primary-color);
  box-shadow: 0 4px 10px rgba(67, 97, 238, 0.2);
}
.group1{
    position:absolute;
    margin-top:240px;
    width:250px;
}
.group2{
    position:absolute;
    margin-top:350px;
    width:250px;
}
.group3{
    position:absolute;
    margin-top:350px;
    margin-left:280px;
    width:250px;
}
.container,
.group {
  overflow: visible !important;
}
.required1{

    margin-top:35px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    
    <h2>Đăng ký lịch khám</h2>
    <div class="layout-wrapper">
        <div class="info-wrapper">
            <!-- Phần thông tin cơ sở y tế -->
            <div class="medical-info">
                <h4>Thông tin cơ sở y tế</h4>
                <div class="trong">
                    <p><strong>Bệnh viện BANANA</strong></p>
                    <p>Cơ sở 220 Phan Thanh - Thành phố Đà Nẵng</p>
                    <img class="hinhbs1" src="../../img/anhbs.jpg" />
                    <img class="hinhbs2" src="../../img/anhbs2.jpg" />
                </div>
            </div>
        </div>

        <div class="form-wrapper">
            <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="container">
                        <!-- Các thông tin cố định -->
                        <div class="group4">
                            <label>Họ và tên <span class="required">*</span></label>
                            <asp:TextBox ID="txtHoTen" CssClass="control" runat="server" ReadOnly="true" />
                        </div>

                        <!-- Control ngày khám -->
                        <div class="group5">
                            <label>Chọn ngày khám <span class="required">*</span></label>
                            <asp:TextBox ID="txtNgayKham" CssClass="control" runat="server" 
                                       TextMode="Date" AutoPostBack="true" 
                                       OnTextChanged="txtNgayKham_TextChanged" />
                        </div>

                        <div class="group">
                            <label>Số điện thoại <span class="required">*</span></label>
                            <asp:TextBox ID="txtSoDienThoai" CssClass="control" runat="server" ReadOnly="true" />
                        </div>

                        <!-- Phần buổi và giờ khám trong UpdatePanel con -->
                        <asp:UpdatePanel ID="upSchedule" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="group">
                                    <label>Buổi Khám <span class="required">*</span></label>
                                    <asp:DropDownList ID="ddlbuoikham" runat="server" CssClass="control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlbuoikham_SelectedIndexChanged">
                                        <asp:ListItem Text="Chọn buổi khám" Value="" />
                                        <asp:ListItem Text="Sáng" Value="Sáng" />
                                        <asp:ListItem Text="Chiều" Value="Chiều" />
                                        <asp:ListItem Text="Cả Ngày" Value="Cả Ngày" />
                                    </asp:DropDownList>
                                </div>

                                <div class="group">
                                    <label class="required1">Chọn giờ khám <span class="required">*</span></label>
                                    <div class="hours-wrapper">
                                        <asp:CheckBoxList ID="cblGiokham" runat="server" 
                                            AutoPostBack="true" OnSelectedIndexChanged="cblGiokham_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow" 
                                            CssClass="checkbox-list">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlbuoikham" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtNgayKham" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <!-- Các thông tin khác -->
                        <div class="group1">
                            <label>Email <span class="required">*</span></label>
                            <asp:TextBox ID="txtEmail" CssClass="control" runat="server" ReadOnly="true" />
                        </div>

                        <div class="group3">
                            <label>Trình độ <span class="required">*</span></label>
                            <asp:TextBox ID="Txttrinhdo" CssClass="control" runat="server" ReadOnly="true" />
                        </div>

                        <div class="group2">
                            <label>Địa chỉ <span class="required">*</span></label>
                            <asp:TextBox ID="txtDiaChi" CssClass="control" runat="server" ReadOnly="true" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDangKy" />
                </Triggers>
            </asp:UpdatePanel>

            <!-- Nút đăng ký -->
            <div class="group">
                <asp:Button ID="btnDangKy" runat="server" CssClass="btn btn-primary" 
                    Text="Đăng ký" OnClick="btnDangKy_Click" />
            </div>
        </div>
    </div>

    <script>
        function showAlert(message, iconType) {
            Swal.fire({
                title: message,
                icon: iconType,
                confirmButtonText: 'OK',
                allowOutsideClick: false
            });
        }
        
    </script>
</asp:Content>
