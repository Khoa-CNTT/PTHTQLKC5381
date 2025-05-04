<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quen_Mat_Khau.aspx.cs" Inherits="NHOM20_DATN.Quen_Mat_Khau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tong {
            padding: 50px;
            position: relative;
            height: 500px;
        }

        .bang1 {
            margin-top: 30px;
            margin-left: 30px;
        }

        .mail {
            width: 280px;
        }

        .khung {
            width: 300px;
            font-size: 16px;
            height: 200px;
            margin-left: 650px;
            padding: 20px;
            background-color: #bbbbbb7a;
            border-radius: 10px;
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.3);
            font-family: Arial, sans-serif;
            margin-top: -450px;
        }

        .khung1 {
            font-size: 16px;
            width: 550px;
            height: 175px;
            margin-left: -70px;
            margin-top: 80px;
            background-color: #bbbbbb7a;
            border-radius: 10px;
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.3);
            font-family: Arial, sans-serif;
        }

        .hienthi1 {
            margin-top: -30px;
            margin-left: -225px;
            color: red;
        }

        .bang2 {
            position: absolute;
        }

            .bang2 tr td {
                padding: 5px;
            }

        .than2 {
            margin-left: 50px;
            margin-top: 30px;
        }

        .bang2 {
            margin-top: 20px;
            margin-left: 50px;
        }

        .xacnhan1 {
            position: absolute;
            font-size: 13px;
            margin-left: 10px;
        }

        .mau {
            color: white;
            background-color: dodgerblue;
            width: 280px;
            border-radius: 8px;
            border: 1px solid dodgerblue;
            height: 30px;
            margin-left: 0px;
            margin-top: 25px;
        }

        .quaylai {
            margin-left: 70px;
            margin-top: 5px;
            font-style: italic;
            font-size: 11px;
        }

            .quaylai a {
                text-decoration: none;
                cursor: pointer;
            }

                .quaylai a:hover {
                    color: red;
                }

        @media (min-width: 1201px) {
            .tong {
                margin-left: -1px;
            }

            .khung {
                height: 180px;
                width: 380px;
                font-size: 13px;
                transition: all 0.3s ease;
                transform-style: preserve-3d;
                position: relative;
                z-index: 1;
            }

                .khung:hover {
                    transform: translateY(-8px);
                    box-shadow: 0 12px 24px rgba(0, 0, 0, 0.2);
                    z-index: 10;
                }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 170px;
                width: 380px;
                font-size: 13px;
                margin-top: 80px;
                transition: all 0.3s ease;
                transform-style: preserve-3d;
                position: relative;
                z-index: 2;
            }

            .xacnhan1 .mau {
                margin-top: -10px;
                margin-left: -125px;
            }

            .hienthi {
                margin-left: 290px !important;
                margin-top: -35px;
                position: absolute;
            }

            .hienthi1 {
                margin-top: -40px !important;
                margin-left: -390px !important;
                width: 400px;
                font-size: 13px;
            }
        }


        /* Quy tắc cho màn hình trung bình */
        @media (max-width: 1200px) and (min-width: 993px) {
            .tong {
                margin-left: 0px;
                width: 940px;
            }

            .hienthi {
                margin-left: 290px !important;
                margin-top: -40px;
                position: absolute;
            }

            .hienthi1 {
                margin-top: -35px !important;
                margin-left: -380px !important;
                width: 400px;
                font-size: 13px;
            }

            .khung {
                height: 190px;
                width: 400px;
                font-size: 16px;
                margin-left: 180px;
                margin-top: -450px;
            }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 180px;
                width: 400px;
                font-size: 16px;
                margin-top: 60px;
                margin-left: -70px;
            }

            .xacnhan1 .mau {
                margin-top: 7px;
                margin-left: -125px;
            }

            .dau4 {
                display: none;
            }

            .anhnenqmk img {
                width: 910px !important;
                height: 500px !important;
                margin-left: -40px;
            }

            .anhnenqmk {
                position: relative;
                z-index: -20;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .tong {
                margin-left: 0px;
                width: 695px;
            }

            .khung {
                height: 190px;
                width: 400px;
                font-size: 13px;
                margin-top: -450px;
                margin-left: 110px;
            }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 170px;
                width: 400px;
                font-size: 13px;
                margin-top: 50px;
                margin-left: -70px;
            }

            .xacnhan1 .mau {
                margin-top: -20px;
                margin-left: -125px;
            }

            .dau4 {
                display: none;
            }

            .hienthi {
                margin-left: 285px !important;
                margin-top: -35px;
                position: absolute;
            }

            .hienthi1 {
                margin-top: -35px !important;
                margin-left: -370px !important;
                width: 400px;
            }

            .anhnenqmk img {
                width: 685px !important;
                height: 500px !important;
                margin-left: -40px;
            }

            .anhnenqmk {
                position: relative;
                z-index: -20;
            }
        }

        /* Quy tắc cho màn hình rất nhỏ */
        @media (max-width: 768px) {
            .tong {
                margin-left: 0px;
                width: 460px;
            }

            .quen {
                top: 100px;
            }

            .khung {
                height: 160px;
                width: 380px;
                font-size: 10px;
                margin-top: -420px;
                margin-left: -10px;
            }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 170px;
                width: 380px;
                font-size: 10px;
                top: -50px !important;
                margin-left: -70px;
            }

            .xacnhan1 .mau {
                margin-left: -125px;
                font-size: 10px;
            }

            .xacnhan1 {
                margin-top: 100px !important;
            }

            .eye-icon {
                cursor: pointer;
                position: absolute;
                margin-left: 2px !important;
                margin-top: 4px;
            }

            .dau4 {
                display: none;
            }

            #ContentPlaceHolder1_Label2 {
                margin-left: 100px;
            }

            .hienthi {
                margin-left: 300px !important;
                margin-top: -35px;
                position: absolute;
            }

            .hienthi1 {
                margin-top: -26px !important;
                margin-left: -350px !important;
                width: 400px;
            }

            .anhnenqmk img {
                width: 450px !important;
                height: 500px !important;
                margin-left: -40px;
            }

            .anhnenqmk {
                position: relative;
                z-index: -20;
            }

            .quen {
                font-size: 18px;
                top: 40px;
            }

            .xacnhan {
                margin-top: -10px !important;
            }
        }

        .hienthi {
            color: transparent;
            transition: color 0.3s;
            font-size: 20px;
            margin-left: 10px;
        }

        #ContentPlaceHolder1_Label2 {
            position: absolute;
            margin-left: 457px;
            margin-top: -38px;
            color: red;
            display: inline;
        }

        .success {
            color: green;
        }

        .error {
            color: red;
        }

        #ContentPlaceHolder1_revEmail {
            position: absolute;
            margin-top: -40px;
            margin-left: 460px;
            color: red;
            display: inline;
        }

        .lb2 {
            width: 300px;
        }

        .success {
            color: green;
            font-size: 20px;
        }

        .error {
            color: red;
            font-size: 20px;
        }

        .eye-icon {
            cursor: pointer;
            position: absolute;
            margin-left: 10px;
            margin-top: 10px;
        }

        .dau4 {
            margin-top: -10px;
            width: 500px;
            margin-left: 50px;
        }

            .dau4 p {
                margin: -2px 0px;
            }

            .dau4 p, h6 {
                font-size: 13px;
                font-style: italic;
                color: #2d4747;
            }

            .dau4 h6 {
                margin-top: 10px;
                color: #4461F2;
                font-style: italic;
            }

            .dau4 h3 {
                margin-left: 35px;
                font-style: italic;
                color: #FFA500
            }

        .quen {
            position: absolute;
            margin-top: -10px;
            margin-left: -60px;
            color: #0051FF;
            font-weight: 600;
            font-family: 'Times New Roman', Times, serif;
        }

        .mail::placeholder {
            padding-left: 0;
        }

        .mail {
            width: 100%;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .input-with-icon {
            position: relative;
            width: 250px;
        }

        .mail {
            width: 280px;
            padding: 10px 30px 10px 30px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .email-icon {
            position: absolute;
            left: 10px;
            top: 50%;
            transform: translateY(-50%);
            color: #999;
            pointer-events: none;
        }

        .anhnenqmk img {
            width: 1200px;
            height: 480px;
            margin-top: -40px;
        }
    </style>
    <div class="tong">
        <div class="anhnenqmk">
            <img src="/img/Anhquenmatkhau.jpeg" />
        </div>
        <div class="khung">
            <div style="text-align: center; margin-bottom: 20px; font-weight: bold; font-size: 18px">
                <asp:Label CssClass="quen" ID="Label3" runat="server" Text="Quên Mật Khẩu"></asp:Label>
            </div>
            <div class="than1">
                <div class="bang1">
                    <div class="input-with-icon">

                        <asp:TextBox CssClass="mail" ID="txtemail" runat="server" placeholder="Nhập Email đã đăng ký"></asp:TextBox>
                        <i class="fas fa-envelope email-icon"></i>
                    </div>
                    <asp:Label ID="Label6" runat="server" Text="" CssClass="hienthi">
                         
                    </asp:Label>
                </div>
                <div class="xacnhan" style="margin-top: 5px; margin-left: 30px;">
                    <asp:Button CssClass="mau" ID="btnxacnhan" runat="server" Text="Xác nhận" OnClick="btnxacnhan_Click" />
                </div>
                <div class="quaylai">
                    Quay lại trang đăng nhập
                <a href="/Dang_Nhap.aspx">tại đây !!!</a>
                </div>
                <div class="hienthi1">
                    <asp:Label ID="Label2" CssClass="lb2" runat="server" Text=""></asp:Label>
                    <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtemail" Display="Dynamic" runat="server" ForeColor="Red"
                        ErrorMessage="Địa chỉ email không hợp lệ"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$">
                            Địa chỉ email không hợp lệ
                    </asp:RegularExpressionValidator>
                </div>
            </div>

            <div class="than2" style="display: none;" id="lammoi">
                <div class="khung1">
                    <table class="bang2">
                        <tr>
                            <td>
                                <asp:TextBox CssClass="mail" ID="txtxacminh" placeholder="Nhập mã xác nhận" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>

                            <td>
                                <asp:TextBox CssClass="mail" ID="txtmatkhau" placeholder="Nhập mật khẩu mới" runat="server" TextMode="Password"></asp:TextBox>
                                <span class="eye-icon" onclick="togglePasswordVisibility()"><i class="far fa-eye"></i>
                                </span>
                            </td>
                        </tr>
                    </table>
                    <div class="xacnhan1" style="margin-top: 130px; margin-left: 180px">
                        <asp:Button CssClass="mau" ID="btndatlai" runat="server" Text="Đặt lại mật khẩu" OnClick="btndatlai_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function togglePasswordVisibility() {
            var passwordInput = document.getElementById("<%= txtmatkhau.ClientID %>");
            if (passwordInput.type === "password") {
                passwordInput.type = "text";
            } else {
                passwordInput.type = "password";
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            $("#<%= txtemail.ClientID %>").on("input", function () {
                var email = $(this).val();
                var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

                $("#<%= Label2.ClientID %>").text("").hide();

                if (emailPattern.test(email)) {
                    $.ajax({
                        type: "POST",
                        url: "Quen_Mat_Khau.aspx/CheckEmail",
                        data: JSON.stringify({ email: email }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d) {
                                $("#<%= Label6.ClientID %>")
                                    .text("✔")
                                    .removeClass("error")
                                    .addClass("success")
                                    .css("display", "inline");
                            } else {
                                $("#<%= Label6.ClientID %>")
                                    .text("✖")
                                    .removeClass("success")
                                    .addClass("error")
                                    .css("display", "inline");
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error("AJAX Error: " + status + error);
                            $("#<%= Label6.ClientID %>")
                                .text("✖")
                                .removeClass("success")
                                .addClass("error")
                                .css("display", "inline");
                        }
                    });
                } else {
                    $("#<%= Label6.ClientID %>").text("").removeClass("success error");
                }
            });
        });
    </script>
</asp:Content>
