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
                width: 860px !important;
                 margin-left: 350px !important;
            }
            .btn-xoa {
                margin-top: 190px !important;
                margin-left: 550px !important;
            }

            .btn-in {
                height: 45px;
                width: 150px;
                margin-top: 190px !important;
                margin-left: 850px !important;
                position: absolute;
            }
            .ghichu{
                margin-left: 600px !important;
            }
           
        }


        /* Quy tắc cho màn hình trung bình */
        @media (max-width: 1200px) and (min-width: 993px) {
            .tong5{
                font-size:13px !important;
            }
            .tieude5{
                width: 900px !important;
                
            }
            .logo5{
                margin-left: 70px !important; 
            }
            .tieudephieu5, .bienvien5{
                margin-left: 190px !important; 
            }
            .ghichu{
                margin-left:300px !important;
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
                height: 650px;
                background-color: #f8f9fa;
                border-right: 1px solid #dee2e6;
                overflow-y: auto;
                padding: -5px;
                z-index: 1000;
                position:absolute;
            }
            .grid-view-simple {
    
                font-size: 11px !important;
            }

            .grid-view-simple th {
    
                font-size: 11px !important;
            }
            .ghichu {
                margin-top: 100px;
            }
            .trai5{
                margin-left:10px !important;
            }
            .phai5{
                margin-left:310px !important;
            }
            .khung1, .khung2, .khung3, .khung4, .khung5, .khung6, .khung7 {
                width:170px !important;
                font-size: 12px !important;
            }
            .btn-xoa {
                width: 120px;
                height: 30px;
                margin-top: 200px !important;
                margin-left: 250px !important;
                font-size: 12px !important;
            }
            .thoigiankham5 {
                margin-left: -40px !important;
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
                margin-top: 200px !important;
                margin-left: 550px !important;
                font-size: 12px !important;
                position: absolute;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .khung1, .khung2, .khung3, .khung4, .khung5, .khung6, .khung7 {
                width:160px !important;
                font-size: 13px !important;
                
            }
           .phai5 h3{
               font-size:12px !important;
           }
            .phai5{
                margin-left: 30px !important;
            }
            .danh-sach-phieu-container {
                position: fixed;
                margin-left: -290px;
                top: 60px;
                width: 140px !important;
                height: 650px;
                background-color: #f8f9fa;
                border-right: 1px solid #dee2e6;
                overflow-y: auto;
                padding: -5px;
                z-index: 1000;
                position:absolute;
            }
            .grid-view-simple {
    
                font-size: 11px !important;
            }

            .grid-view-simple th {
    
                font-size: 11px !important;
            }
            .trai5{
                margin-left:-250px !important;
                
            }
            .trai5 h3{
                font-size:12px !important;
            }
            .tong5 {
                font-size: 10px !important;
            }

            .tieude5 {
                width: 680px !important;
                margin-left: -260px;
                 
            }
            .tieudephieu5, .bienvien5{
                margin-left: -100px !important; 
            }
            .logo5{
                margin-left: -220px !important;
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
                margin-left: 160px !important;
            }

            .ghichu {
                margin-top: -80px !important;
                margin-left:230px !important;
            }



            .btn-xoa {
                width: 120px;
                height: 30px;
                margin-top: 200px !important;
                margin-left: 0px !important;
                font-size: 12px !important;
            }

            .btn-in {
                width: 120px;
                height: 30px;
                margin-top: 200px !important;
                margin-left: 200px !important;
                font-size: 12px !important;
                position: absolute;
            }
        }

        /* Quy tắc cho màn hình rất nhỏ */
        @media (max-width: 768px) {
            .danh-sach-phieu-container {
                position: fixed;
                margin-left: -260px;
                top: 60px;
                width: 120px !important;
                height: 650px;
                background-color: #f8f9fa;
                border-right: 1px solid #dee2e6;
                overflow-y: auto;
                padding: -5px;
                z-index: 1000;
                position:absolute;
            }
            .grid-view-simple {
    
                font-size: 9px !important;
            }

            .grid-view-simple th {
    
                font-size: 9px !important;
            }
            .bienvien5{
                margin-left:-100px;
                
            }
            .logo5{
                margin-left:-10px;
            }
            .khung1, .khung2, .khung3, .khung4, .khung5, .khung6, .khung7 {
                width: 100px !important;
                font-size: 10px !important;
            }
            .tieudephieu5, .bienvien5{
                margin-left: -20px !important; 
            }
            .logo5{
                margin-left: -150px !important;
            }
            .trai5 {
                margin-left: -120px !important;
            }
                .trai5 h3 {
                    font-size: 9px !important;
                }

            .phai5 {
                margin-left: 100px !important;
            }

                .phai5 h3 {
                    font-size: 9px !important;
                }

            .tong5 {
                font-size: 10px !important;
            }

            .tieude5 {
                width: 520px !important;
                 margin-left:-220px !important;
            }


            .than5 {
                margin-left: 50px !important;
                font-size: 10px !important;
            }
            
            .thoigiankham5 {
                margin-left: -50px !important;
                font-size:11px !important;
            }

            .nen5 {
                width: 400px !important;
                margin-left: 120px !important;
            }

            .ghichu {
                margin-top: -80px !important;
                margin-left: 40px !important;
                font-size:8px !important;
            }

            .btn-xoa {
                width: 100px;
                height: 30px;
                margin-top: 200px !important;
                margin-left: -50px !important;
                font-size: 9px !important;
            }

            .btn-in {
                width: 100px;
                height: 30px;
                margin-top: 200px !important;
                margin-left: 150px !important;
                font-size: 9px !important;
                position: absolute;
            }
            .ghichu {
                text-align: center;
                margin-top: -80px;
                margin-bottom: 5px;
                margin-left: 200px !important;
                z-index: 10; 
                position: absolute;
            }

                .ghichu a {
                    text-decoration: none;
                    font-size: 9px !important;
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
            background-color: #ff4d4d; /* Màu đỏ */
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

        .tong5 {
            position: relative;
            margin-top: 10px;
            margin-left: 20px;
            height: 760px;
        }


        .tieude5 {
            margin-top: -7px;
            font-size: 24px;
            background: linear-gradient(to right,#4876FF, #97FFFF);
            color: #98F5FF;
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
            margin-left: 30px;
        }

        .than5 {
            margin-left: 500px;
        }

        .bienvien5 {
            margin-top: -60px;
            margin-left: 115px;
        }

        .lb3 {
            font-size: 14px;
        }

        .lb2 {
            font-size: 24px;
        }

        .tieudephieu5 {
            margin-left: 100px;
            margin-top: 20px;
        }

        .phieukham5 {
            color: deepskyblue;
            font-size: 22px;
            font-weight: bolder;
        }

        .id5 {
            font-size: 40px;
            margin-left: -5px;
            font-weight: bolder;
        }

        .thoigiankham5 {
            font-style: italic;
            font-size: 13px;
            margin-left: -35px;
        }

        .phong5 {
            margin-top: 15px;
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
            margin-top: -275px;
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
            margin-left: 51px;
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
            background-color: #F0F0F0;
            width: 950px;
            height: 650px;
            margin-top: -700px;
            margin-left: 200px;
            position: absolute;
            z-index: -1;
            border-radius: 20px;
            overflow: hidden;
        }

        @media print {
            .tong5 {
                margin-left: 20px;
                height: 500px;
                width: 1270px;
                margin-top: 100px;
            }

            button, .btn-xoa, .btn-in, .danh-sach-phieu-container{
                display: none;
            }

            .ZX, .ZY, .tieude5, .ghichu, .web {
                display: none;
            }

            .trai5 {
                margin-left: -280px !important;
            }

            .phai5 {
                margin-top: -325px !important;
                margin-left: 200px !important;
            }

                .trai5 h3, .phai5 h3 {
                    font-size: 24px !important;
                }

            .khung1, .khung2, .khung3, .khung4, .khung5, .khung6, .khung7 {
                margin-bottom: 7px !important; /* Dãn cách giữa các khung */
                width: 300px !important;
                height: 50px !important;
                font-size: 24px !important;
            }

            .logo5, .tieudephieu5, .bienvien5, .phong5, .thoigiankham5 {
                font-size: 26px;
            }

            .phong5 {
                margin-left: -30px;
            }

            .thoigiankham5 {
                margin-left: -150px !important;
                font-size:22px !important;
            }

            .khung4 {
                margin-left: 70px;
            }

            .khung3 {
                margin-left: 50px;
            }

            .khung2 {
                margin-left: 35px;
            }

            .khung1 {
                margin-left: 40px;
            }

            .khung7 {
                margin-left: 37px;
            }
        }

        .danh-sach-phieu-container {
            position: fixed;
            left: 30px;
            top: 60px;
            width: 250px;
            height: 650px;
            background-color: #f8f9fa;
            border-right: 1px solid #dee2e6;
            overflow-y: auto;
            padding: -5px;
            z-index: 1000;
            position:absolute;
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
                font-size: 12px;
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
            color:red;
        }


    </style>
    <div class="tong5">

        <div class="Tren1">
        <asp:Label ID="Label1" CssClass="tieude5" runat="server" Text="HUỶ KHÁM"></asp:Label>
            </div>
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
                <img style="width: 80px;" src="img/logochinh.png" />
            </div>
            <div class="bienvien5">
                <asp:Label ID="Label2" CssClass="lb2" runat="server" Text="Bệnh Viện Banana"></asp:Label>
                <br />
                <asp:Label ID="Label3" CssClass="lb3" runat="server" Text="Nhiệt Tình - Tâm Huyết - Tỉ Mỉ"></asp:Label>
            </div>
            <div class="tieudephieu5">
                <asp:Label ID="Label4" CssClass="phieukham5" runat="server" Text="Phiếu Khám Bệnh"></asp:Label>
                <br />
                <asp:Label ID="lbid" CssClass="id5" runat="server" Text="P014"></asp:Label>
                <div class="phong5">
                    <asp:Label ID="Label5" CssClass="phongkham5" runat="server" Text="Phòng khám:"></asp:Label>
                    <asp:Label ID="lbphongkham" runat="server" Text="P001"></asp:Label>
                </div>
                <span class="thoigiankham5">
                    <asp:Label ID="Label6" runat="server" Text="Thời gian khám:"></asp:Label>
                    <asp:Label ID="lbthoigian" runat="server" Text="25/05/2024 12:00:00 AM"></asp:Label>
                </span>
            </div>
            <div class="chinh5">
                <div class="trai5">
                    <h3>Họ Và Tên:<asp:TextBox CssClass="khung1" ID="lbhoten" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                    <h3>Ngày Sinh:<asp:TextBox CssClass="khung2" ID="lbngaysinh" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                    <h3>Giới Tính:<asp:TextBox CssClass="khung3" ID="lbgioitinh" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                    <h3>Địa Chỉ:<asp:TextBox CssClass="khung4" ID="lbdiachi" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                    <h3>Triệu Chứng:
                    <asp:TextBox CssClass="khung5" ID="lbtrieuchung" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                </div>
                <div class="phai5">
                    <h3>Email:<asp:TextBox ID="lbemail" CssClass="khung6" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

                    <h3>SĐT:<asp:TextBox ID="lbsdt" CssClass="khung7" runat="server" Text="Dữ liệu hiển thị" ReadOnly="True"></asp:TextBox></h3>

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

    <script type="text/javascript">
        function showPhongKhamInfo(phongKhamInfo) {
            addStyles();
            const contentDiv = document.createElement('div');
            contentDiv.className = 'custom-alert';
            contentDiv.innerHTML = phongKhamInfo;

            // Hiển thị SweetAlert
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
            setTimeout(function () {
                const swalModal = document.querySelector('.swal-modal');
                if (swalModal) {
                    swalModal.scrollTop = 0;

                    const titleElement = document.querySelector('.swal-title');
                    if (titleElement) {
                        titleElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
                    }

                    swalModal.scrollIntoView({ behavior: 'smooth', block: 'start' });
                }
            }, 100);
        }
        function addStyles() {
            const style = document.createElement('style');
            style.innerHTML = `
        .custom-alert {
           width: 600px; font-size: 18px; padding: 20px; text-align:left;margin-left: 30px;
        }
        .swal-modal{
             width: 600px;
        }
       
    `;
            document.head.appendChild(style);
        }


    </script>

    <script type="text/javascript">
        function confirmDelete() {
            return swal({
                title: "Bạn có chắc chắn muốn hủy đăng ký?",
                text: "Nếu bạn xác nhận, đăng ký sẽ bị xóa.",
                icon: "warning",
                buttons: ["Hủy bỏ", "Xác nhận"],
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    __doPostBack('<%= btnnut.UniqueID %>', '');
                } else {
                    swal("Đăng ký vẫn được giữ lại!");
                    return false;
                }
            });
        }
    </script>
    <script>
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
                    window.location.href = "DangKyKhamTrucTiep.aspx";
                }
            });
        }
    </script>
    <script type="text/javascript">
            function selectRow(rowIndex) {
                __doPostBack('<%= gvDanhSachPhieu.UniqueID %>', 'SelectRow$' + rowIndex);
            }
    </script>
</asp:Content>
