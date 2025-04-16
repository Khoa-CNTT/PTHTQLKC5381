<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quen_Mat_Khau.aspx.cs" Inherits="NHOM20_DATN.Quen_Mat_Khau" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <style>
        .tong {
            padding: 50px;
            position: relative;
            height: 450px;
        }

        .bang1 tr td {
            padding: 20px;
        }

        .bang1 {
            margin-top: -20px;
        }

        .mail {
            width: 250px;
        }

        .khung {
            width: 550px;
            font-size: 16px;
            height: 187px;
            margin-left: 350px;
            padding: 20px;
            background-color: #fff;
            border-radius: 10px; /* Bo góc */
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
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

        .than2 {
            margin-left: 50px;
            margin-top: 30px;
        }

        .khung1 {
            font-size: 16px;
            width: 550px;
            height: 165px;
            margin-left: -70px;
            margin-top: 70px;
            background-color: #fff; 
            border-radius: 10px; 
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3); 
            font-family: Arial, sans-serif;
        }

        .bang2 tr td {
            padding: 10px;
        }

        .bang2 {
            margin-top: 20px;
            margin-left: 30px;
        }

        .xacnhan1 {
            position: absolute;
            font-size: 16px;
        }

        .mau {
            color: white;
            background-color: dodgerblue;
            width: 200px;
            border-radius: 8px;
            border: 1px solid dodgerblue;
            height: 30px;
        }

        .quaylai {
            margin-left: 250px;
            margin-top: -30px;
        }

            .quaylai a {
                text-decoration: none;
                cursor: pointer;
            }

                .quaylai a:hover {
                    color: LightSkyBlue;
                }

        @media (min-width: 1201px) {
            .tong {
                margin-left: -55px;
            }

            .khung {
                height: 190px;
                width: 600px;
                font-size: 16px;
            }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 170px;
                width: 600px;
                font-size: 16px;
                margin-top: 50px;
            }

            .xacnhan1 .mau {
                margin-top: -20px;
                margin-left: 10px;
            }
        }


        /* Quy tắc cho màn hình trung bình */
        @media (max-width: 1200px) and (min-width: 993px) {
            .tong {
                margin-left: -255px;
            }

            .khung {
                height: 190px;
                width: 600px;
                font-size: 16px;
            }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 170px;
                width: 600px;
                font-size: 16px;
                margin-top: 60px;
            }

            .xacnhan1 .mau {
                margin-top: -20px;
                margin-left: 10px;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .tong {
                margin-left: -355px;
            }

            .khung {
                height: 190px;
                width: 600px;
                font-size: 13px;
            }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 170px;
                width: 600px;
                font-size: 13px;
                margin-top: 60px;
            }

            .xacnhan1 .mau {
                margin-top: -20px;
                margin-left: 10px;
            }
        }

        /* Quy tắc cho màn hình rất nhỏ */
        @media (max-width: 768px) {
            .tong {
                margin-left: -395px;
            }

            .khung {
                height: 200px;
                width: 450px;
                font-size: 13px;
            }

            .than2 {
                margin-left: 50px;
            }

            .khung1 {
                height: 160px;
                width: 450px;
                font-size: 13px;
                margin-top: 50px;
            }

            .xacnhan1 .mau {
                margin-top: -20px;
                margin-left: -50px;
            }
                .eye-icon {
                cursor: pointer;
                position: absolute;
                margin-left: 2px !important;
                margin-top: 4px;
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
            margin-top: 4px;
        }
    </style>
    <div class="tong">
        <div class="khung">
            <div style="text-align: center; margin-bottom: 20px; font-weight: bold; font-size: 18px">
                <asp:Label CssClass="quen" ID="Label3" runat="server" Text="Quên Mật Khẩu"></asp:Label>
            </div>
            <div class="than1">
                <table class="bang1">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Nhập Email đã đăng ký: "></asp:Label>

                        </td>
                        <td style="margin-left: 20px;">
                            <asp:TextBox CssClass="mail" ID="txtemail" runat="server" placeholder="Nhập email của bạn"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" Text="" CssClass="hienthi">
                         
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="xacnhan" style="margin-top: 35px; margin-left: 30px;">
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
                                <asp:Label ID="Label4" runat="server" Text="Nhập mã xác nhận: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox CssClass="mail" ID="txtxacminh" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Nhập mật khẩu mới: "></asp:Label>
                            </td>
                            
                            <td>
                        <asp:TextBox CssClass="mail" ID="txtmatkhau" runat="server" TextMode="Password"></asp:TextBox>
                        <span class="eye-icon" onclick="togglePasswordVisibility()">
                            👁️
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

               // Ẩn dòng thông báo lỗi khi bắt đầu nhập lại email
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
