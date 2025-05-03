<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Huy_Kham.aspx.cs" Inherits="NHOM20_DATN.Huy_Kham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        @media (min-width: 1201px) {
            .than5 {
                margin-left: 600px !important;
            }

            .nen5 {
                width: 870px !important;
                margin-left: 320px !important;
                margin-top: -380px !important;
            }

            .btn-xoa {
                margin-top: 90px !important;
                margin-left: 550px !important;
            }

            .btn-in {
                height: 45px;
                width: 150px;
                margin-top: 90px !important;
                margin-left: 850px !important;
                position: absolute;
            }

            .ghichu {
                margin-left: 580px !important;
                margin-top: 150px !important;
            }
        }


        /* Quy tắc cho màn hình trung bình */
        @media (max-width: 1200px) and (min-width: 993px) {
            .tong5 {
                font-size: 13px !important;
                height: 630px !important;
            }

            .logo5 img {
                margin-left: -40px !important;
                margin-top: 23px !important;
            }

            .tieude5 {
                width: 900px !important;
            }

            .anhnenhuykham .anhhuykham {
                width: 400px !important;
            }

            .nentrong {
                width: 650px !important;
                margin-left: -80px !important;
                height: 350px !important;
                top: 160px !important;
            }

            .id5 {
                top: -20px !important;
            }

            .phong5 {
                font-size: 22px !important;
                margin-top: -20px !important;
            }

            .phongkham5 {
                font-size: 22px !important;
            }

            .nen5 {
                width: 520px !important;
                margin-left: -100px !important;
                top: 470px !important;
                height: 550px !important;
            }

            .phieukham5 {
                font-size: 32px !important;
                margin-left: -30px !important;
                top: -20px !important;
            }

            .tieudephieu5 {
                margin-left: 190px !important;
                font-size: 30px;
            }

            .bienvien5 {
                margin-left: 30px !important;
                margin-top: -40px !important;
            }

            .ghichu {
                margin-left: 300px !important;
            }

            .than5 {
                margin-left: 290px !important;
            }

            .nen5 {
                width: 670px !important;
                margin-left: 245px !important;
            }

            .danh-sach-phieu-container {
                position: fixed;
                left: 30px;
                top: 60px;
                width: 190px !important;
                height: 550px !important;
                background-color: #f8f9fa;
                border-right: 1px solid #dee2e6;
                overflow-y: auto;
                padding: -5px;
                z-index: 1000;
                position: absolute;
            }

            .grid-view-simple {
                font-size: 11px !important;
            }

                .grid-view-simple th {
                    font-size: 11px !important;
                }

            .ghichu {
                margin-top: 120px !important;
                margin-left: 370px !important;
            }

            .trai5 {
                margin-left: 10px !important;
            }

            .chinh5 {
                margin-top: -10px !important;
            }

            .phai5 {
                margin-left: 310px !important;
            }

            .khung1, .khung2, .khung3, .khung4, .khung5, .khung6, .khung7 {
                width: 170px !important;
                font-size: 12px !important;
            }

            .btn-xoa {
                width: 120px;
                height: 30px;
                margin-top: 80px !important;
                margin-left: 350px !important;
                font-size: 12px !important;
            }

            .thoigiankham5 {
                margin-left: -40px !important;
                top: -10px !important;
            }

            .btn-xoa-disabled {
                background-color: #cccccc !important;
                color: #666666 !important;
                cursor: not-allowed !important;
                pointer-events: none !important;
            }

            .btn-in {
                width: 120px;
                height: 30px;
                margin-top: 80px !important;
                margin-left: 650px !important;
                font-size: 12px !important;
                position: absolute;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .khung1, .khung2, .khung3, .khung4, .khung6, .khung7 {
                width: 160px !important;
                font-size: 13px !important;
            }

            .tong5 {
                height: 610px !important;
            }

            .khung5 {
                width: 480px !important;
                font-size: 13px !important;
            }

            .phai5 h3 {
                font-size: 12px !important;
            }

            .phai5 {
                margin-left: 30px !important;
            }

            .danh-sach-phieu-container {
                position: fixed;
                margin-left: -290px;
                top: 60px;
                width: 140px !important;
                height: 530px !important;
                background-color: #f8f9fa;
                border-right: 1px solid #dee2e6;
                overflow-y: auto;
                padding: -5px;
                z-index: 1000;
                position: absolute;
            }

            .grid-view-simple {
                font-size: 11px !important;
            }

                .grid-view-simple th {
                    font-size: 11px !important;
                }

            .trai5 {
                margin-left: -250px !important;
            }

                .trai5 h3 {
                    font-size: 12px !important;
                }

            .tong5 {
                font-size: 10px !important;
            }

            .tieude5 {
                width: 680px !important;
                margin-left: -260px;
            }

            .tieudephieu5 {
                margin-left: -100px !important;
            }

            .bienvien5 {
                margin-left: -170px !important;
                margin-top: -40px !important;
            }

            .logo5 img {
                margin-left: -250px !important;
                margin-top: 15px !important;
            }

            .ghichu {
                margin-left: 100px !important;
            }

            .than5 {
                margin-left: 160px !important;
            }

            .thoigiankham5 {
                margin-left: -60px !important;
            }

            .nen5 {
                width: 520px !important;
                margin-left: -100px !important;
                top: 470px !important;
                height: 540px !important;
            }

            .ghichu {
                margin-top: 100px !important;
                margin-left: -20px !important;
            }

            .btn-xoa {
                width: 120px;
                height: 30px;
                margin-top: 50px !important;
                margin-left: 0px !important;
                font-size: 12px !important;
            }

            .btn-in {
                width: 120px;
                height: 30px;
                margin-top: 50px !important;
                margin-left: 200px !important;
                font-size: 12px !important;
                position: absolute;
            }

            .anhnenhuykham .anhhuykham, .nentrong {
                display: none;
            }

            .tong5 {
            }
        }

        /* Quy tắc cho màn hình rất nhỏ */
        @media (max-width: 768px) {
            .tong5 {
                width: 100%;
                margin-left: -900px !important;
                padding: 0px;
                height: 750px !important;
                overflow: visible !important;
                position: relative !important;
            }

            .id5 {
                font-size: 15px !important;
                margin-left: -5px !important;
            }

            .phieukham5 {
                font-size: 18px !important;
                margin-left: -110px !important;
            }

            .phongkham5 {
                margin-left: -20px !important;
            }

            .thoigiankham5 {
                font-size: 11px !important;
                margin-left: -70px !important;
            }

            .chinh5 h3 {
                font-size: 13px !important;
            }

            .tieude5 {
                width: 100% !important;
                margin-left: -220px !important;
                font-size: 22px !important;
                padding: 10px !important;
                text-align: center;
            }


            .nen5 {
                width: 350px !important;
                margin-left: -80px !important;
                top: 482px !important;
                height: 600px !important;
                position: absolute !important;
                transform: none !important;
                border: 2px solid #50c7c7 !important;
            }

            .nentrong {
                width: 100% !important;
                left: 2.5% !important;
                top: 180px;
                height: auto;
                padding-bottom: 20px;
                position: relative;
                display: none;
            }

            .than5 {
                width: 75% !important;
                margin-left: 0 !important;
                padding: 10px;
                margin-top: -30px;
                font-size: 13px !important;
                margin-left: -80px !important;
            }

            .logo5 img {
                margin-left: 0 !important;
                display: block;
                margin: 0 auto;
                display: none;
            }

            .bienvien5 {
                margin-left: 70px !important;
                margin-top: -50px !important;
                display: none;
            }

            .trai5, .phai5 {
                width: 100% !important;
                margin-left: 0 !important;
                margin-top: 20px !important;
                float: none;
            }

            .khung1, .khung2, .khung3, .khung4, .khung5, .khung6, .khung7 {
                width: 90% !important;
                margin-left: 5% !important;
                font-size: 12px !important;
            }

            .btn-xoa, .btn-in {
                margin-top: 10px;
            }

            .btn-in {
                position: absolute;
                margin-left: 160px !important;
                display: block;
                float: none;
                width: 15%;
                font-size: 11px !important;
            }

            .btn-xoa {
                position: absolute;
                margin-left: -50px !important;
                display: block;
                float: none;
                width: 20%;
                font-size: 11px !important;
            }

            .ghichu {
                margin: 30px 0px !important;
                padding: 20px;
                width: 100%;
                position: absolute;
                left: -140px !important;
            }

            .a {
                font-size: 9px !important;
            }

            .danh-sach-phieu-container {
                width: 30% !important;
                margin-left: -260px !important;
                position: relative;
                height: 640px !important;
                top: 43px !important;
                left: 0;
                margin-bottom: 20px;
            }

            .anhnenhuykham .anhhuykham {
                display: none;
            }
        }

        .ghichu {
            text-align: center;
            margin-top: -80px;
            margin-bottom: 5px;
            margin-left: 500px;
            z-index: 10;
            position: absolute;
        }

            .ghichu a {
                text-decoration: none;
                font-size: 13px;
            }

            .ghichu :hover {
                text-decoration: none;
            }


        .btn-xoa {
            background-color: #ff4d4d;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            transition: background-color 0.3s ease;
            font-weight: bolder;
            position: absolute;
        }

            .btn-xoa:hover {
                background-color: #FF0000;
                box-shadow: 0 6px 12px rgba(0, 0, 0, 0.3);
            }

        .btn-in {
            position: absolute;
            background-color: royalblue;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            transition: background-color 0.3s ease;
            font-weight: bolder;
            margin-left: 100px;
        }

            .btn-in:hover {
                background-color: #0056b3;
                box-shadow: 0 6px 12px rgba(0, 0, 0, 0.3);
            }

        /**********************************************/
        .tong5 {
            position: relative;
            margin-top: 10px;
            margin-left: 20px;
            height: 680px;
        }

        .anhnenhuykham {
            z-index: 2;
        }

            .anhnenhuykham .anhhuykham {
                width: 1260px !important;
                height: 700px;
                z-index: -1 !important;
                position: absolute;
            }

        .tieude5 {
            margin-top: -7px;
            font-size: 24px;
            background: #366A9A;
            color: white;
            padding: 10px 40px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            width: 1260px;
            display: inline-block;
            font-weight: bolder;
            height: 50px;
        }

        .logo5 img {
            margin-top: 30px;
            margin-left: -270px;
            height: 40px;
            width: 70px !important;
        }

        .than5 {
            margin-left: 500px;
        }

        .bienvien5 {
            margin-top: -45px;
            margin-left: -194px;
        }

        .lb3 {
            font-size: 11px;
        }

        .lb2 {
            font-size: 13px;
        }

        .nentrong {
            background-color: white;
            width: 840px;
            height: 330px;
            position: absolute;
            z-index: 0;
            left: 335px;
            top: 210px;
            border-radius: 5px;
        }

        .id5, .phieukham5, .thoigiankham5 {
            position: relative;
        }

        .trai5, .phai5 {
            position: relative;
            z-index: 1;
        }

        .id5, .phongkham5, .thoigiankham5 {
            position: relative;
            z-index: 1;
        }

        .tieudephieu5 {
            margin-left: 100px;
            margin-top: 20px;
            position: relative;
            z-index: 1;
        }

        .phieukham5 {
            color: #2B6477;
            font-size: 48px;
            font-weight: bolder;
            margin-left: -110px;
        }

        .id5 {
            font-size: 22px;
            margin-left: -15px;
            font-weight: bolder;
            padding: 0px 5px;
        }

        .thoigiankham5 {
            font-style: italic;
            font-size: 13px;
            margin-left: -15px;
        }

        .phong5 {
            margin-top: 0px;
            margin-left: 20px;
        }

        .trai5 {
            margin-left: -250px;
            margin-top: 20px;
        }

            .trai5 h3 {
                font-size: 16px;
            }

        .phai5 {
            margin-left: 200px;
            margin-top: -218px;
        }

            .phai5 h3 {
                font-size: 16px;
            }

        .khung1, .khung2, .khung3, .khung4, .khung5, .khung6, .khung7 {
            margin-bottom: 7px;
            width: 300px;
            height: 40px;
        }

        .khung1 {
            margin-left: 30px;
            border-radius: 5px;
            box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(0, 0, 0, 0.2);
            color: rgb(128, 128, 128);
        }

        .khung2 {
            margin-left: 29px;
            box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 5px;
            color: rgb(128, 128, 128);
        }

        .khung3 {
            margin-left: 38px;
            box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 5px;
            color: rgb(128, 128, 128);
        }

        .khung4 {
            margin-left: 8px;
            box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 5px;
            color: rgb(128, 128, 128);
        }

        .khung5 {
            margin-left: 10px;
            box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 5px;
            color: rgb(128, 128, 128);
            width: 708px;
        }

        .khung6 {
            margin-left: 20px;
            box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 5px;
            color: rgb(128, 128, 128);
        }

        .khung7 {
            margin-left: 31px;
            box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 5px;
            color: rgb(128, 128, 128);
        }

        .nut5 {
            margin-left: 380px;
            position: absolute;
        }

        .nen5 {
            background-color: rgb(234 241 244 / 73%);
            width: 950px;
            height: 600px;
            margin-top: -415px !important;
            margin-left: 200px;
            position: absolute;
            z-index: -1;
            border-radius: 20px;
            overflow: hidden;
        }


        @media print {
            .anhnenhuykham .anhhuykham, .btn-xoa, .btn-in,
            .danh-sach-phieu-container, .ZX, .ZY, .tieude5, .ghichu, .web, .nen5 {
                display: none !important;
            }

            #openChatBtn, #chatbotBox {
                display: none !important;
            }

            body, .tong5, .trai5, .phai5, .tieudephieu5 {
                white-space: nowrap !important;
            }

            .DC {
                display: block !important;
                margin-left: 110px !important;
            }

            body, .tong5 {
                background-color: white !important;
                color: black !important;
                margin: 0 !important;
                padding: 10px !important;
                width: 100% !important;
                height: auto !important;
                line-height: 1.5 !important;
            }

            @page {
                size: portrait;
                margin: 1cm;
            }

            .phongkham5 {
                margin-left: 0px !important;
            }

            .logo5 {
                position: fixed;
                top: 13px;
                left: 140px;
                width: 80px;
                height: auto;
            }

                .logo5 img {
                    width: 100%;
                    height: auto;
                }

            .web {
                display: none !important;
            }

            .tong5 {
                display: block !important;
                left: -200px !important;
                width: 550px !important;
                margin-top: 0 !important;
                font-size: 15px !important;
            }

            .nguoidangky {
                display: block !important;
                margin-left: -20px !important;
                margin-top: 20px !important;
                text-align: center;
            }

            .thoigiankham5 {
                display: inline-block !important;
                left: 190px !important;
            }

            .br {
                display: none !important;
            }

            .id5 {
                font-weight: normal !important;
                position: relative !important;
                left: 0 !important;
                display: inline-block !important;
                top: -30px !important;
                font-size: 15px !important;
                padding: 0 !important;
                left: 330px !important;
            }

            .tieudephieu5 {
                display: flex;
                flex-direction: column;
                align-items: flex-start;
                white-space: nowrap;
                text-align: left;
                padding-bottom: 10px;
                margin-top: -50px !important;
            }

                .tieudephieu5 .id5 {
                    display: inline !important;
                    white-space: nowrap !important;
                }

                .tieudephieu5 > * {
                    margin-bottom: 5px;
                    display: block;
                }

            .phieukham5 {
                display: block;
                left: 180px !important;
                font-size: 32px !important;
                margin: 30px 0 20px 0 !important;
                color: #000 !important;
                font-weight: bold;
                margin-bottom: 10px;
                top: -15px;
                padding: 20px 0px;
            }

            .tieudephieu5 {
                text-align: left;
                margin: 0 !important;
                padding: 0 !important;
            }

            .id5, .phong5, .thoigiankham5 {
                display: inline-block !important;
                margin-top: -55px !important;
                font-size: 15px !important;
            }

            .trai5, .phai5 {
                width: 100% !important;
                float: none !important;
                margin: 0px 0px !important;
                padding: 0 !important;
                margin-top: -25px !important;
            }

                .trai5 h3, .phai5 h3 {
                    display: inline !important;
                    margin: 0 !important;
                    padding: 0 !important;
                    font-weight: normal !important;
                }

                .trai5 > h3, .phai5 > h3 {
                    display: block !important;
                    margin-bottom: 8px !important;
                }

            .phai5 {
                float: right !important;
            }

            .tieudephieu5, .id5, .phong5, .thoigiankham5,
            .trai5, .phai5 {
                text-align: left !important;
                margin-left: 0 !important;
                float: none !important;
                width: 100% !important;
            }

            .nentrong, .than5 {
                page-break-inside: avoid;
            }

            .khung1, .khung2, .khung3,
            .khung4, .khung5, .khung6,
            .khung7 {
                display: inline !important;
                border: none !important;
                background: transparent !important;
                box-shadow: none !important;
                padding: 0 !important;
                margin: 0 0 0 5px !important;
                width: 250px !important;
                font-weight: bold !important;
            }

            .chinh5 {
                display: flex !important;
                flex-direction: row !important;
                flex-wrap: nowrap !important;
            }


            .logo5 img {
                display: block !important;
                margin: 0 auto 10px auto !important;
                width: 80px !important;
                height: auto !important;
            }

            .bienvien5 {
                text-align: center;
                margin: 10px 0 !important;
                font-size: 15px !important;
            }

            .nentrong, .than5 {
                page-break-inside: avoid;
            }

            .no-print {
                display: none !important;
            }
        }

        .danh-sach-phieu-container {
            position: fixed;
            left: 30px;
            top: 60px;
            width: 250px;
            height: 600px;
            background-color: #f8f9fa;
            border-right: 1px solid #dee2e6;
            overflow-y: auto;
            padding: -5px;
            z-index: 1000;
            position: absolute;
        }

        .grid-view-simple {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
        }

            .grid-view-simple th {
                background-color: #4876FF;
                color: white;
                padding: 3px;
                text-align: left;
                font-size: 14px;
            }

            .grid-view-simple td {
                padding: 8px;
                border-bottom: 1px solid #ddd;
            }

            .grid-view-simple tr.selected-row {
                background-color: #cce5ff !important;
                font-weight: bold;
            }

            .grid-view-simple tr:hover {
                background-color: #e6f2ff;
                cursor: pointer;
            }

        @media (max-width: 992px) {
            .danh-sach-phieu-container {
                width: 250px;
            }

            .tong5 {
                margin-left: 270px !important;
            }
        }

        @media (max-width: 768px) {
            .danh-sach-phieu-container {
                width: 200px;
                padding: 10px;
            }

            .grid-view-simple {
                font-size: 11px !important;
            }

                .grid-view-simple th {
                    font-size: 11px !important;
                }

            .tong5 {
                margin-left: 220px !important;
            }
        }

        .grid-view-simple tr {
            cursor: pointer;
        }

        .hidden-link {
            display: none;
        }

        .grid-view-simple tr.selected-row {
            background-color: #cce5ff !important;
            font-weight: bold;
        }

        .selected-row {
            background-color: #cce5ff !important;
            font-weight: bold;
        }

        .chon {
            text-decoration: none !important;
        }

            .chon:hover {
                text-decoration: none !important;
                color: red;
            }

        .danh-sach-phieu-container {
            background-color: #e7f3ffe8;
            border-radius: 5px;
        }

        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
            z-index: 9999;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .loading-content {
            text-align: center;
            color: white;
            font-size: 20px;
        }

        .spinner {
            border: 5px solid #f3f3f3;
            border-top: 5px solid #3498db;
            border-radius: 50%;
            width: 50px;
            height: 50px;
            animation: spin 1s linear infinite;
            margin: 0 auto 0px auto;
            position: relative;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .btn-xoa.loading {
            position: relative;
            color: transparent;
        }

            .btn-xoa.loading:after {
                content: "";
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                width: 20px;
                height: 20px;
                border: 3px solid rgba(255,255,255,0.3);
                border-radius: 50%;
                border-top-color: #fff;
                animation: spin 1s ease-in-out infinite;
            }

        @keyframes spin {
            to {
                transform: translate(-50%, -50%) rotate(360deg);
            }
        }

        .DC {
            display: none;
        }

        .nguoidangky {
            display: none;
        }
    </style>

    <div class="tong5">
        <div class="DC">
            <h4>BỆNH VIỆN HOSPITAL BANANA</h4>
            <p>Địa chỉ: 220 Phan Thanh, Hải Châu , TP.Đà Nẵng</p>
        </div>
        <div class="Tren1">
            <asp:Label ID="Label1" CssClass="tieude5" runat="server" Text="HUỶ KHÁM"></asp:Label>
        </div>
        <div class=" anhnenhuykham">
            <img class="anhhuykham" src="../img/anhnen_huykham.jpeg" />

            <div class="Trai1">

                <div class="danh-sach-phieu-container">
                    <asp:GridView ID="gvDanhSachPhieu" runat="server" AutoGenerateColumns="False"
                        CssClass="grid-view-simple"
                        OnRowCommand="gvDanhSachPhieu_RowCommand"
                        DataKeyNames="IDPhieu">
                        <Columns>

                            <asp:BoundField DataField="IDPhieu" HeaderText="Mã Phiếu" />
                            <asp:BoundField DataField="NgayKham" HeaderText="Ngày khám" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandName="SelectRow"
                                        CommandArgument='<%# Container.DataItemIndex %>' Text="Chọn" CssClass="chon" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
            <div class="than5">

                <div class="logo5">
                    <img style="width: 80px;" src="../img/logochinh.png" />
                </div>
                <div class="bienvien5">
                    <asp:Label ID="Label2" CssClass="lb2" runat="server" Text="Bệnh Viện Banana"></asp:Label>
                    <br class="br" />
                    <asp:Label ID="Label3" CssClass="lb3" runat="server" Text="Nhiệt Tình - Tâm Huyết - Tỉ Mỉ"></asp:Label>
                </div>
                <div class="tieudephieu5">
                    <asp:Label ID="Label4" CssClass="phieukham5" runat="server" Text="Phiếu Khám Bệnh"></asp:Label>
                    <div>
                        <asp:Label ID="Label7" CssClass="id5" runat="server" Text="Mã Phiếu:"></asp:Label>
                        <asp:Label ID="lbid" CssClass="id5" runat="server" Text=" PK89D962"></asp:Label>
                    </div>
                    <div class="phong5">
                        <asp:Label ID="Label5" CssClass="phongkham5" runat="server" Text="Phòng khám:"></asp:Label>
                        <asp:Label ID="lbphongkham" runat="server" Text="P001"></asp:Label>
                    </div>
                    <span class="thoigiankham5">
                        <asp:Label ID="Label6" runat="server" Text="Thời gian khám:"></asp:Label>
                        <asp:Label ID="lbthoigian" runat="server" Text="25/05/2025"></asp:Label>
                    </span>
                </div>
                <div class="chinh5">
                    <div class="nentrong"></div>
                    <div class="trai5">
                        <h3>Họ Và Tên:<asp:TextBox CssClass="khung1" ID="lbhoten" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                        <h3>Ngày Sinh:<asp:TextBox CssClass="khung2" ID="lbngaysinh" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                        <h3>Giới Tính:<asp:TextBox CssClass="khung3" ID="lbgioitinh" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>



                        <h3>Triệu Chứng:
                    <asp:TextBox CssClass="khung5" ID="lbtrieuchung" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                    </div>
                    <div class="phai5">
                        <h3>Email:<asp:TextBox ID="lbemail" CssClass="khung6" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                        <h3>SĐT:<asp:TextBox ID="lbsdt" CssClass="khung7" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>
                        <h3>Địa Chỉ:<asp:TextBox CssClass="khung4" ID="lbdiachi" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>
                    </div>
                </div>

            </div>
            <div class="nut22">
                <asp:Button ID="btnnut" runat="server" CssClass="btn-xoa" Text="Xoá đăng ký" OnClick="btnnut_Click"
                    OnClientClick=" confirmDelete();return false;" />

            </div>
            <button class="btn-in" onclick="window.print()">In</button>
        </div>
        <div class="ghichu">

            <asp:LinkButton CssClass="a" ID="lnkShowPhongKham" runat="server" Text="Nếu bạn muốn biết phòng khám cụ thể ở đâu hãy bấm vào đây !!!" OnClick="lnkShowPhongKham_Click"></asp:LinkButton>

        </div>
        <div class="nen5"></div>
        <div id="loadingOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background: rgba(0,0,0,0.5); z-index: 9999; display: flex; justify-content: center; align-items: center;">
            <div style="color: white; font-size: 20px;">
                <div class="spinner"></div>
                <p>Đang xử lý...</p>
            </div>
        </div>
        <div class="nguoidangky">
            <p class="text-end">Người đăng ký</p>
            <br>
            <br>
            <p class="text-end">(Ký và ghi rõ họ tên)</p>
        </div>
    </div>
    <script type="text/javascript">
        function showPhongKhamInfo(phongKhamInfo) {
            addStyles();
            const contentDiv = document.createElement('div');
            contentDiv.className = 'custom-alert';
            contentDiv.innerHTML = phongKhamInfo;

            swal({
                title: "Thông Tin Phòng Khám",
                content: contentDiv,
                icon: "info",
                buttons: {
                    cancel: "Đóng"
                },
                closeOnClickOutside: false,
            }).then(() => {
                window.scrollTo(0, 0);
            });
        }

        function addStyles() {
            const style = document.createElement('style');
            style.innerHTML = `
            .custom-alert {
                width: 600px; font-size: 18px; padding: 20px; text-align:left;margin-left: 30px;
            }
            .swal-modal {
                width: 600px;
            }
            `;
            document.head.appendChild(style);
        }

        function confirmDelete() {
            swal({
                title: "Bạn có chắc chắn muốn hủy đăng ký?",
                text: "Nếu bạn xác nhận, đăng ký sẽ bị xóa.",
                icon: "warning",
                buttons: ["Hủy bỏ", "Xác nhận"],
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    document.getElementById('loadingOverlay').style.display = 'flex';
                    __doPostBack('<%= btnnut.UniqueID %>', '');
                } else {
                    swal("Đăng ký vẫn được giữ lại!");
                    return false;
                }
            });
            return false;
        }

        function pageLoad() {
            const overlay = document.getElementById('loadingOverlay');
            if (overlay) overlay.style.display = 'none';

            const btn = document.querySelector('.btn-xoa.loading');
            if (btn) btn.classList.remove('loading');
        }

        function showExpiredAlert() {
            return swal({
                title: "Phiếu khám đã hết hạn",
                text: "Phiếu khám của bạn đã quá hạn. Vui lòng đăng ký lại!",
                icon: "warning",
                buttons: {
                    confirm: {
                        text: "Đăng ký lại",
                        value: true,
                        visible: true,
                        className: "btn-primary"
                    },
                    cancel: {
                        text: "Đóng",
                        value: false,
                        visible: true,
                        className: "btn-default"
                    }
                }
            }).then((value) => {
                if (value) {
                    window.location.href = "/pages/Doctor/Dang_Ky_Kham_Truc_Tiep.aspx";
                }
            });
        }

        function selectRow(rowIndex) {
            __doPostBack('<%= gvDanhSachPhieu.UniqueID %>', 'SelectRow$' + rowIndex);
        }
    </script>
</asp:Content>
