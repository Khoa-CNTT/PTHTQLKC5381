<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dat_Lai_Mat_Khau.aspx.cs" Inherits="NHOM20_DATN.Dat_Lai_Mat_Khau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js"></script>
    <style>
        .Tong {
            padding: 50px;
            position: relative;
            height: 480px;
            position: relative;
        }

        .nentrongsuot {
            position: absolute;
            top: 50px;
            left: 140px;
            width: 400px;
            height: 320px;
            background-color: rgb(191 191 191 / 34%);
            backdrop-filter: blur(3px);
            -webkit-backdrop-filter: blur(3px);
            z-index: 1;
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2), 0 5px 10px rgba(0, 0, 0, 0.2);
        }

        .nendatlai img {
            height: 450px;
            width: 1000px;
            position: absolute;
            margin-left: 70px;
            margin-top: -40px;
            z-index: 0;
        }

        .than2 img {
            height: 400px;
            width: 400px;
            position: absolute;
            margin-top: -380px;
            margin-left: 560px;
            border-radius: 5px;
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
            font-size: 15px;
            width: 500px;
            height: 300px;
            margin-left: 90px;
            margin-top: 70px;
            border-radius: 10px;
            font-family: Arial, sans-serif;
            line-height: 0.5;
            padding: 20px;
            z-index: 2;
            position: relative;
        }

        .lb11, .lb12, .lb13 {
            margin-bottom: 15px;
            display: block;
        }

        .mail1, .mail2, .mail3 {
            margin-bottom: 10px;
            display: block;
            width: 100%;
            max-width: 350px;
            height: 40px;
            border-radius: 5px;
            background: rgba(255, 255, 255, 0.25);
            backdrop-filter: blur(10px);
            -webkit-backdrop-filter: blur(10px);
            border-radius: 10px;
            border: 1px solid rgba(255, 255, 255, 0.18);
            box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.1);
        }

        .bang2 {
            position: absolute;
            margin-top: 70px;
            margin-left: 30px;
        }

        .xacnhan1 {
            position: absolute;
            font-size: 16px;
            margin-top: 0px;
            margin-left: 0px;
            height: 50px;
            width: 350px !important;
        }

        .mau {
            color: white;
            background-color: dodgerblue;
            width: 350px;
            border: 1px solid dodgerblue;
            height: 40px;
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
            font-weight: 1000;
            margin-top: 10px;
            margin-left: -420px;
            z-index: 10;
            color: #191970;
            font-weight: bold;
            font-family: 'Times New Roman', Times, serif;
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
                margin-top: 80px;
                margin-left: 220px;
            }

            .nendatlai {
                width: 940px;
                margin-left: -50px;
            }

            .quen {
                margin-left: -100px;
            }

            .bang2 {
                margin-left: 10px;
                margin-top: 50px;
            }

            .than2 img {
                display: none;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .khung1 {
                height: 300px;
                width: 450px;
                font-size: 13px;
                margin-top: 100px;
                margin-left: 130px;
            }

            .nendatlai {
                width: 700px;
                margin-left: -50px;
            }

            .quen {
                margin-left: -100px;
                margin-top: 20px;
            }

            .bang2 {
                margin-left: 10px;
                margin-top: 50px;
            }

            .xacnhan1 .mau {
                margin-top: -10px;
                margin-left: 0px;
            }

            .than2 img {
                display: none;
            }
        }

        @media (max-width: 768px) {

            .khung1 {
                height: 300px;
                width: 450px;
                font-size: 12px;
                margin-top: 80px;
                margin-left: 0px;
            }

            .nendatlai img {
                width: 460px;
                margin-left: -50px;
            }

            .bang2 {
                margin-left: 10px;
                margin-top: 50px;
            }

            .xacnhan1 .mau {
                margin-top: 10px;
                margin-left: 0px;
            }

            .than2 img {
                display: none;
            }

            .quen {
                margin-left: -80px;
                font-size: 20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Tong">

        <div class="nendatlai">
            <img src="/img/Anh2.jpg" />
        </div>
        <div class="nentrongsuot"></div>
        <div style="text-align: center; margin-bottom: 20px; font-weight: bold; font-size: 18px">
            <asp:Label CssClass="quen" ID="Label3" runat="server" Text="Đặt lại mật Khẩu"></asp:Label>
        </div>
        <div class="than2" id="lammoi">
            <div class="khung1">

                <asp:TextBox CssClass="mail1" ID="TextBox1" placeholder="Nhập mật khẩu cũ" runat="server"></asp:TextBox>

                <br />

                <asp:TextBox CssClass="mail2" ID="txtxacminh" placeholder="Nhập mật khẩu mới" runat="server"></asp:TextBox>
                <br />

                <asp:TextBox CssClass="mail3" ID="txtmatkhau" placeholder="Nhập lại mật khẩu mới" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Label CssClass="tieude2" ID="Label2" runat="server" Text=""></asp:Label>



                <div class="xacnhan1">

                    <asp:Button CssClass="mau" ID="btndatlai" runat="server" OnClick="btndatlai_Click" Text="Xác nhận" />
                </div>
            </div>

        </div>

    </div>
</asp:Content>
