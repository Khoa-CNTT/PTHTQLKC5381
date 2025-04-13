<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dang_Nhap.aspx.cs" Inherits="NHOM20_DATN.Dang_Nhap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        #content_container #login_container {
            text-align: center;
            width: 100%;
            height: 40em;
            background-image: url('/img/background_login.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            position: relative;
            overflow-x: visible;
        }

        #content_container #container_alert {
            background-color: rgba(181, 227, 227, 0.773);
            padding: 0.5em 3em;
            display: flex;
            justify-content: space-between;
        }

            #content_container #container_alert i {
                margin: 0 15px;
            }

            #content_container #container_alert a {
                text-decoration: none;
                font-weight: 600;
                color: #3b3a3a;
            }

        #content_container #login_container #login_form {
            background-color: rgba(245, 250, 255, 0.986);
            position: absolute;
            width: 32%;
            max-width: 600px;
            border-radius: 1em;
            display: flex;
            flex-flow: column;
            align-items: center;
            left: 0;
            right: 0;
            margin: auto;
            margin-top: 3em;
        }


        #content_container #login_form h2 {
            color: rgb(59, 194, 252);
            margin-top: 1em;
            margin-bottom: 1em;
        }

        #content_container #login_form .inp_mxn {
            display: flex;
            justify-content: space-between;
            flex-wrap: wrap;
        }


        #content_container #login_form .inp_mdn,
        .inp_mk,
        .inp_mxn,
        .inp_submit {
            margin: 1em 0;
            width: 80%;
        }

        #content_container #login_form input {
            width: 100%;
            padding: 0.5em 1em;
            border-radius: 1.5em;
            border: 1px solid #ccc;
            background-color: #e5e5e582;
        }

        #content_container #login_form .inp_mxn input {
            width: 50%;
        }

        #content_container #login_form .inp_mxn span {
            padding: 1em 2em;
            background-color: rgb(59, 194, 252);
        }

        #content_container .inp_submit .inp_savep {
            float: left;
            display: flex;
            align-items: center;
        }

            #content_container .inp_submit .inp_savep input {
                width: 1.5em;
                height: 1.5em;
                margin-right: 10px;
            }

            #content_container .inp_submit .inp_savep label {
                font-size: 1em;
            }

        #content_container .inp_submit .inp_btn {
            margin-top: 15px;
        }

        #content_container #login_form .inp_submit .inp_btn input {
            padding: 1em 2em;   
            border: none;
            background: linear-gradient(to right, #57cdfd, #0eb5fd);
            border-radius: 2em;
            cursor: pointer;
            color: aliceblue;
            font-weight: 500;
        }

         #content_container #login_form .inp_submit .inp_btn input:hover {
            background: rgb(14, 181, 253);
         }



        #content_container #login_form .inp_mxn,
        button {
            font-weight: 600;
            color: aliceblue;
        }

        #content_container #login_form .inp_regis {
            margin-bottom: 2em;
        }

        #content_container #login_form .inp_submit .inp_savep input {
            width: 35%;
        }



        @media screen and (max-width: 1380px) {
            #content_container #login_form .inp_mxn {
                display: block;
            }


                #content_container #login_form .inp_mxn input {
                    margin-bottom: 2em;
                    width: 100%;
                    padding: 1.3em 2em;
                }

            #content_container .inp_mxn span {
                width: 100%;
            }

            #content_container #login_container {
                height: 37em;
            }
        }

        @media screen and (max-width: 1100px) {

            #content_container #login_form .inp_submit .inp_savep, .inp_btn {
                width: 100%;
                margin-bottom: 1em;
            }

            #content_container #login_container #login_form {
                width: 45%;
            }
        }

        @media screen and (max-width: 900px) {

            #content_container #login_form .inp_submit .inp_savep, .inp_btn {
                width: 100%;
                margin-bottom: 1em;
            }

            #content_container #login_container #login_form {
                width: 81%;
            }
        }


        @media screen and (max-width: 768px) {

            #content_container #container_alert {
                padding: 1.5em 1em;
            }

            #content_container #login_form input {
                padding: 1.3em 0;
                padding-left: 1em;
            }

            #content_container #login_form .inp_mxn input {
                padding: 1.3em 0;
                padding-left: 1em;
            }
        }

        .captcha-text {
            font-size: 24px;
            letter-spacing: 4px;
            font-weight: bold;
            text-transform: uppercase;
            padding: 10px;
            border: 2px dashed #007bff;
            display: inline-block;
            background: #f8f9fa;
            border-radius: 5px;
        }

        .captcha-refresh {
            cursor: pointer;
            width: 30px;
            height: 30px;
            border-radius: 50%;
            transition: transform 0.2s ease;
        }

            .captcha-refresh:hover {
                transform: rotate(360deg);
                background-color: #f0f0f0;
            }

        .form-control {
            max-width: 200px;
            margin: 0 auto;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content_container">
        <div id="login_container">
            <div id="login_form">
                <h2>ĐĂNG NHẬP</h2>
                <div class="inp_mdn">
                    <asp:TextBox ID="txtUsername" CssClass="inp" placeholder="Tên đăng nhập" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>
                <div class="inp_mk">
                    <asp:TextBox ID="txtPassword" CssClass="inp" TextMode="Password" placeholder="Mật Khẩu" runat="server"></asp:TextBox>
                </div>
                <asp:CheckBox ID="chkShowPassword" runat="server" Text="Hiển thị mật khẩu" OnCheckedChanged="chkShowPassword_CheckedChanged" AutoPostBack="true" />
                <asp:RequiredFieldValidator ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>
                <div class="inp_submit">
                    <div class="form-group text-center">
                        <div class="d-flex align-items-center justify-content-center gap-2">
                            <asp:Label ID="lblCaptcha" runat="server" CssClass="captcha-text fw-bold text-primary fs-4"></asp:Label>
                            <asp:TextBox ID="txtCaptcha" CssClass="form-control text-center" placeholder="Nhập mã xác nhận" runat="server"></asp:TextBox>
                        </div>

                        <asp:RequiredFieldValidator ControlToValidate="txtCaptcha" Display="Dynamic" ForeColor="Red"
                            ErrorMessage="Vui lòng nhập mã xác nhận" runat="server">Vui lòng nhập mã xác nhận</asp:RequiredFieldValidator>
                        <asp:Label ID="lblCaptchaError" runat="server" ForeColor="Red"></asp:Label>
                        <div class="inp_btn">
                            <asp:Button ID="btnDN" runat="server" Text="ĐĂNG NHẬP" OnClick="btnDN_Click" />
                        </div>
                    </div>
                    <div class="inp_regis">Đăng ký tài khoản <a href="Dang_Ky.aspx">tại đây</a> </div>
                    <div class="inp_regis">Quên mật khẩu hãy bấm <a href="/Patient/Quen_Mat_Khau.aspx">vào đây</a> </div>
                </div>
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
