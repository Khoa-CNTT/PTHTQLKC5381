<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dat_Lai_Mat_Khau.aspx.cs" Inherits="NHOM20_DATN.Dat_Lai_Mat_Khau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js"></script>
    <style>
        .Tong {
            padding: 50px;
            position: relative;
            height: 350px;
        }

        .bang2 tr td {
            padding: 10px;
        }

        .mail {
            width: 250px;
            border-radius: 5px;
            height: 30px;
            border: 1px solid #C0C0C0;
        }

        .khung1 {
            position: relative;
            font-size: 16px;
            width: 550px;
            height: 300px;
            margin-left: 320px;
            margin-top: -50px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
            font-family: Arial, sans-serif;
        }

        .bang2 {
            position: absolute;
            margin-top: 70px;
            margin-left: 30px;
        }

        .xacnhan1 {
            position: absolute;
            font-size: 16px;
            margin-top: 250px;
            margin-left: 180px;
        }

        .mau {
            color: white;
            background-color: dodgerblue;
            width: 200px;
            border-radius: 8px;
            border: 1px solid dodgerblue;
            height: 30px;
        }

        .success {
            color: green;
        }

        .error {
            color: red;
        }

        tr td .mail {
            border-radius: 5px;
            height: 30px;
        }

        .quen {
            position: absolute;
            font-size: 26px;
            font-weight: 500;
            margin-top: -30px;
            margin-left: -100px;
            z-index: 10;
        }

        .tieude2 {
            position: absolute;
            margin-top: 210px;
            margin-left: 50px;
            font-size: 12px;
            color: red;
            width: 450px;
        }
        
        /* Quy tắc cho màn hình trung bình */
        @media (max-width: 1200px) and (min-width: 993px) {
            .khung1 {
                height: 300px;
                width: 450px;
                font-size: 13px;
                margin-top: -50px;
                margin-left: 170px;
            }

            .quen {
                margin-left: -100px;
            }

            .bang2 {
                margin-left: 10px;
                margin-top: 50px;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .khung1 {
                height: 300px;
                width: 450px;
                font-size: 13px;
                margin-top: -50px;
                margin-left: 90px;
            }

            .quen {
                margin-left: -50px;
            }

            .bang2 {
                margin-left: 10px;
                margin-top: 50px;
            }

            .xacnhan1 .mau {
                margin-top: -20px;
                margin-left: -50px;
            }
        }

        @media (max-width: 768px) {
            .khung1 {
                height: 300px;
                width: 450px;
                font-size: 13px;
                margin-top: -50px;
                margin-left: -40px;
            }

            .bang2 {
                margin-left: 10px;
                margin-top: 50px;
            }

            .xacnhan1 .mau {
                margin-top: -20px;
                margin-left: -50px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Tong">
        <div style="text-align: center; margin-bottom: 20px; font-weight: bold; font-size: 18px">
            <asp:Label CssClass="quen" ID="Label3" runat="server" Text="Đặt lại mật Khẩu"></asp:Label>
        </div>
        <div class="than2" id="lammoi">
            <div class="khung1">
                <table class="bang2">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Nhập mật khẩu cũ: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox CssClass="mail" ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Nhập mật khẩu mới: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox CssClass="mail" ID="txtxacminh" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Nhập lại mật khẩu mới: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox CssClass="mail" ID="txtmatkhau" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <asp:Label CssClass="tieude2" ID="Label2" runat="server" Text=""></asp:Label>


                    </tr>
                </table>
                <div class="xacnhan1">

                    <asp:Button CssClass="mau" ID="btndatlai" runat="server" OnClick="btndatlai_Click" Text="Xác nhận" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
