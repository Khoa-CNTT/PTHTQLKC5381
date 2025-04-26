<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage_TuVanVien.Master" AutoEventWireup="true" CodeBehind="Ho_Tro_Dat_Lich_Kham.aspx.cs" Inherits="NHOM20_DATN.Consultant.Ho_Tro_Dat_Lich_Kham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }

        .page-bg {
            background-color: #f7f7f7;
            padding: 10px 0;
            margin-top: -40px;
            margin-left: -25px;
        }

        .breadcrumb {
            font-size: 20px;
            margin-left: 300px;
            color: #666;
            max-width: 900px;
            padding: 0 20px;
        }

            .breadcrumb a {
                text-decoration: none;
                color: #3b40cc;
                margin-right: 5px;
                font-weight: bold;
            }

                .breadcrumb a:hover {
                    text-decoration: underline;
                }

            .breadcrumb span {
                color: #333;
                margin-left: 10px;
            }

        h2 {
            color: black;
            margin-bottom: 15px;
            font-weight: bold;
            font-size: 25px;
        }

        h3 {
            margin-top: 15px;
            margin-bottom: 7px;
            color: black;
            font-size: 18px;
            font-weight: bold;
        }

        .thanhngang {
            border-bottom: 4px solid #40f689;
            margin-bottom: 15px;
            width: 50px;
        }

        .main-container {
            width: 1100px !important;
            margin-left: 400px;
            background-color: #fff;
            border-radius: 4px;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            margin-left: 50px !important;
        }

        .form-group {
            margin-bottom: 15px;
        }

            .form-group label {
                font-weight: 600;
                margin-bottom: 5px;
                display: inline-block;
            }

        .form-control {
            width: 100%;
            padding: 8px 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .radio-group {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .btn-submit {
            display: inline-block;
            background-color: #5f65ff;
            color: #fff;
            padding: 10px 25px;
            border: none;
            border-radius: 4px;
            font-size: 18px;
            cursor: pointer;
            margin-top: 10px;
            margin-left: 15px;
            width: 200px;
            
        }
            .btn-submit:hover {
                background-color: #4247c2;
            }
            .btn-submitt {
            display: inline-block;
            background-color: #5f65ff;
            color: #fff;
            padding: 10px 25px;
            border: none;
            border-radius: 4px;
            font-size: 14px;
            cursor: pointer;
            margin-top: 10px;
            width: 110px;
            font-weight:bold;
            font-family:'Times New Roman', Times, serif
            
        }
            .btn-submitt:hover {
                background-color: #4247c2;
            }
        .red_text {
            color: red;
            font-size: 14px;
            display: block;
            margin-top: 5px;
        }

        .modall {
            display: none;
            position: fixed;
            z-index: 1000;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.6);
            backdrop-filter: blur(4px);
        }

        .modall-content {
            background: #ffffff;
            margin: 5% auto;
            padding: 30px;
            border-radius: 16px;
            width: 90%;
            max-width: 500px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
            text-align: center;
            animation: fadeIn 0.3s ease-in-out;
        }

        @keyframes fadeIn {
            from {
                transform: translateY(-20px);
                opacity: 0;
            }

            to {
                transform: translateY(0);
                opacity: 1;
            }
        }

        .image-row {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-bottom: 15px;
        }

        .hinhbv, .hinhbs {
            width: 70px;
            height: 70px;
            object-fit: cover;
            border-radius: 12px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
        }

        .header {
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 10px;
            font-size: 1.3rem;
            font-weight: 600;
            color: #333;
            margin-bottom: 20px;
        }

        .modal-body {
            font-size: 1rem;
            color: #555;
            margin-bottom: 25px;
            line-height: 1.5;
        }

        .close {
            background: linear-gradient(135deg, #007BFF, #0056b3);
            color: white;
            border: none;
            padding: 12px 24px;
            border-radius: 8px;
            font-size: 1rem;
            cursor: pointer;
            transition: background 0.3s ease;
        }

            .close:hover {
                background: linear-gradient(135deg, #0056b3, #003d80);
            }

        .question {
            color: #e91e63;
            cursor: pointer;
            font-size: 15px;
            margin-left: 20px;
            transition: color 0.2s;
        }

            .question:hover {
                color: #c2185b;
            }

        @media (max-width: 600px) {
            .row {
                flex-direction: column;
            }

            .col-6, .col-4 {
                flex: 0 0 100%;
            }
        }

        label {
            color: #3b40cc;
        }

        input[readonly] {
            background-color: #f0f0f0;
            color: #a0a0a0;
            border: 1px solid #d0d0d0;
            cursor: not-allowed;
        }

            input[readonly]:focus {
                caret-color: transparent;
            }

        .form-control {
            background-color: #fafafa;
            border: 1px solid #ccc;
            color: #333;
            padding: 8px 10px;
            border-radius: 4px;
        }

        select.form-control {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            background: url("data:image/svg+xml;utf8,<svg fill='%23333' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>") no-repeat right 0.75rem center / 1rem 1rem #fafafa;
            padding-right: 2rem;
            cursor: pointer;
        }

        .date-selection {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
        }

        .date-box {
            border: 1px solid #ccc;
            background-color: #f8f8f8;
            border-radius: 6px;
            padding: 10px 16px;
            text-align: center;
            cursor: pointer;
            min-width: 80px;
            transition: background-color 0.3s, color 0.3s;
        }

            .date-box:hover {
                background-color: #eaeaea;
            }

            .date-box.active {
                background-color: #3b40cc;
                color: #fff;
                border-color: #3b40cc;
            }

            .date-box .date {
                display: block;
                font-weight: bold;
                font-size: 16px;
            }

            .date-box .day {
                display: block;
                font-size: 14px;
                margin-top: 4px;
                color: #666;
            }

        .page-header {
            background: linear-gradient(to right, #3b40cc, #5a67f2);
            color: white;
            padding: 30px 0;
            text-align: center;
            margin-bottom: 40px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.15);
            border-bottom: 4px solid #40f689;
            border-radius: 0 0 12px 12px;
        }

            .page-header h1 {
                font-size: 36px;
                font-weight: 900;
                text-transform: uppercase;
                letter-spacing: 2px;
                text-shadow: 1px 1px 2px rgba(0,0,0,0.2);
            }

        .main-container {
            width: 900px;
            margin: 0 auto;
            background-color: #fff;
            border-radius: 16px;
            padding: 40px;
            box-shadow: 0 6px 16px rgba(0,0,0,0.1);
            transition: all 0.3s;
            margin-left: 400px;
        }

        .section-title {
            color: #3b40cc;
            margin: 40px 0 20px;
            font-size: 24px;
            font-weight: bold;
            position: relative;
            padding-bottom: 10px;
        }

            .section-title:after {
                content: "";
                position: absolute;
                bottom: 0;
                left: 0;
                width: 60px;
                height: 5px;
                background-color: #40f689;
                border-radius: 3px;
            }

        .page-title {
            font-size: 28px;
            font-weight: bold;
            color: #3b40cc;
            text-align: center;
            margin-bottom: 30px;
            margin-left: 450px;
            position: absolute;
            padding:10px;
            font-family: serif;
        }

            .page-title::after {
                content: "";
                display: block;
                width: 200px;
                height: 5px;
                background-color: #40f689;
                margin: 10px auto 0;
                border-radius: 5px;
            }

        .row {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .col-66 {
            flex: 0 0 48%;
        }

        .col-4 {
            flex: 0 0 31%;
        }

        .col-12 {
            flex: 0 0 100%;
        }

        h3 {
            font-size: 20px;
            margin: 30px 0 10px;
            color: #3b40cc;
        }

        .TichThanhToan {
            margin: 20px 0;
        }

            .TichThanhToan table {
                width: 100%;
            }

            .TichThanhToan td {
                padding-right: 40px;
                font-size: 16px;
            }

                .TichThanhToan td label {
                    padding-left: 5px;
                }

        .radio-group input[type="radio"] {
            margin-right: 5px;
        }


        .radio-group td {
            padding: 5px 15px;
        }

        .swal2-popup {
            font-family: Arial, sans-serif;
        }

        .swal2-title {
            color: #3b40cc;
        }

        .swal2-success {
            border-color: #40f689;
        }

            .swal2-success [class^="swal2-success-line"] {
                background-color: #40f689;
            }

            .swal2-success .swal2-success-ring {
                border-color: rgba(64, 246, 137, 0.3);
            }

        .swal2-confirm {
            background-color: #3b40cc !important;
        }

            .swal2-confirm:focus {
                box-shadow: 0 0 0 3px rgba(59, 64, 204, 0.5) !important;
            }

        .swal2-popup {
            font-family: Arial, sans-serif;
            max-width: 600px;
        }

        .swal2-title {
            color: #3b40cc;
            font-size: 24px;
        }

        .swal2-content {
            font-size: 16px;
            text-align: left;
        }

        .swal2-confirm {
            background-color: #3b40cc !important;
            padding: 10px 24px !important;
        }

        .swal2-cancel {
            background-color: #6c757d !important;
            padding: 10px 24px !important;
        }

        .date-selection {
            display: flex;
            gap: 10px;
            margin-bottom: 15px;
            flex-wrap: wrap;
        }

        .date-box {
            border: 1px solid #ccc;
            background-color: #f8f8f8;
            border-radius: 6px;
            padding: 10px 16px;
            text-align: center;
            cursor: pointer;
            min-width: 80px;
            transition: all 0.3s ease;
        }

            .date-box:hover {
                background-color: #eaeaea;
                transform: translateY(-2px);
                box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            }

            .date-box.active {
                background-color: #3b40cc;
                color: #fff;
                border-color: #3b40cc;
                box-shadow: 0 2px 8px rgba(59, 64, 204, 0.3);
            }

            .date-box .date {
                display: block;
                font-weight: bold;
                font-size: 16px;
            }

            .date-box .day {
                display: block;
                font-size: 14px;
                margin-top: 4px;
            }

            .date-box.active .day {
                color: rgba(255,255,255,0.8);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-bg">

        <div class="page-title">Hỗ trợ đặt lịch khám</div>

        <div class="main-container">
            <h3>Tìm kiếm bệnh nhân</h3>
            <div class="thanhngang"></div>
            <div class="row">
                <div class="form-group col-66">
                    <label for="txtemail1">Email bệnh nhân</label>
                    <asp:TextBox ID="txtemail1" runat="server" CssClass="form-control"
                        placeholder="Nhập email bệnh nhân"></asp:TextBox>
                </div>
                <div class="form-group col-1" style="display: flex; margin-left: -40px ;align-items: flex-end;">
                    <asp:Button ID="btnTimKiemBN" runat="server" Text="Tìm kiếm"
                        CssClass="btn-submitt" OnClick="btnTimKiemBN_Click" />
                </div>
            </div>

            <h3>Nội dung chi tiết đăng ký lịch khám</h3>
            <div class="thanhngang">
            </div>
            <div class="row">

                <div class="form-group col-66">
                    <label for="ddlChuyenKhoa">Chuyên khoa</label>
                    <asp:DropDownList ID="ddlChuyenKhoa" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlChuyenKhoa_SelectedIndexChanged"
                        CssClass="form-control">
                        <asp:ListItem Value="" Text="Chọn chuyên khoa"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-66">
                    <label for="ddlPhongKham">Phòng khám</label>
                    <asp:DropDownList ID="ddlPhongKham" runat="server" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem Value="" Text="Chọn phòng khám"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-66">
                    <label for="ddlBacSi">Bác sĩ</label>
                    <asp:DropDownList ID="ddlBacSi" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBacSi_SelectedIndexChanged"
                        CssClass="form-control">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlBacSi"
                        Display="Dynamic" ErrorMessage="Vui lòng chọn bác sĩ" Operator="NotEqual"
                        ValueToCompare="Chọn bác sĩ" CssClass="red_text">
                    </asp:CompareValidator>
                </div>
                <div class="form-group col-66" style="display: flex; align-items: flex-end; left: 650px !important;position: absolute !important;  ">
                </div>
            </div>

            <h3>Thời gian khám</h3>
            <div class="thanhngang">
            </div>
            <asp:TextBox ID="txtNgayKham" runat="server" TextMode="Date"
                AutoPostBack="true" Style="display: none" OnTextChanged="txtNgayKham_TextChanged" />
            <div class="row">
                <asp:Repeater ID="rptNgayKham" runat="server">
                    <HeaderTemplate>
                        <div class="date-selection">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class='date-box <%# Eval("ActiveClass") %>'
                            onclick='selectDate("<%# Eval("NgayValue") %>", this)'>
                            <span class="date"><%# Eval("NgayThang") %></span>
                            <span class="day"><%# Eval("Thu") %></span>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                <div class="form-group col-4">
                    <label for="ddlbuoikham">Buổi khám</label>
                    <asp:DropDownList ID="ddlbuoikham" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbuoikham_SelectedIndexChanged"
                        CssClass="form-control">
                        <asp:ListItem Value="" Text="Chọn buổi khám"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-4">
                    <label for="DDLgiokham">Giờ khám</label>
                    <asp:DropDownList ID="DDLgiokham" runat="server" AutoPostBack="true"
                        CssClass="form-control">
                        <asp:ListItem Value="" Text="Chọn giờ Khám"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <h3>Thông tin khách hàng</h3>
            <div class="thanhngang">
            </div>
            <div class="row">
                <div class="form-group col-66">
                    <label for="txtHoTen">Họ và tên</label>
                    <asp:TextBox ID="txtHoTen" runat="server" placeholder="Họ và tên"
                        CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group col-66">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-66">
                    <label for="txtNgaySinh">Ngày tháng năm sinh</label>
                    <asp:TextBox ID="txtNgaySinh" runat="server" TextMode="Date"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-66">
                    <label for="txtSoDienThoai">Số điện thoại</label>
                    <asp:TextBox ID="txtSoDienThoai" runat="server" placeholder="Số điện thoại"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-66">
                    <label>Giới tính</label>
                    <div class="radio-group">
                        <asp:RadioButtonList ID="gtRadioList" RepeatDirection="Horizontal" runat="server" Enabled="false">
                            <asp:ListItem Value="Nam">Nam</asp:ListItem>
                            <asp:ListItem Value="Nu">Nữ</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group col-66">
                    <label for="txtCCCD">Căn cước công dân</label>
                    <asp:TextBox ID="txtCCCD" runat="server" placeholder="Căn cước công dân"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-12">
                    <label for="txtDiaChi">Địa chỉ</label>
                    <asp:TextBox ID="txtDiaChi" runat="server" placeholder="Nhập địa chỉ"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-12">
                    <label for="txtTrieuChung">Triệu chứng</label>
                    <asp:TextBox ID="txtTrieuChung" runat="server" TextMode="MultiLine" Rows="4"
                        placeholder="Nhập triệu chứng" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="row">
                <div class="form-group col-66">
                    <asp:Button ID="btnDangKy" Text="Đăng ký" runat="server"
                        OnClick="btnDangKy_Click" OnClientClick="return confirmRegistration();" CssClass="btn-submit" />
                </div>

                <div class="form-group col-66" style="display: flex; align-items: center;">
                    <%--<div class="TichThanhToan">
                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="ChuaThanhToan">Chưa thanh toán</asp:ListItem>
                            <asp:ListItem Value="DaThanhToan">Đã thanh toán</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <div id="question-modal" class="modall">
        <div class="modall-content">
            <div class="image-row">
                <div class="hinhanhbv1">
                    <img src="../img/logochinh.png" alt="logo" class="hinhbv">
                </div>
            </div>
            <div class="header" style="font-size: 1.2rem; font-weight: bold; margin-bottom: 10px;">
                <img src="../img/bga.png" alt="" class="hinhbs">
                BANANA XIN CHÀO QUÝ KHÁCH
            </div>
            <div class="modal-body" id="modal-text" style="font-size: 1rem; color: #333; margin-bottom: 20px;">
            </div>
            <button class="close" onclick="closeModal()">Đóng</button>
        </div>
    </div>

    <script>
        var questions = document.querySelectorAll('.question');
        questions.forEach(function (question) {
            question.addEventListener('click', function () {
                var answer = this.getAttribute('data-answer');
                document.getElementById('modal-text').textContent = answer;
                document.getElementById('question-modal').style.display = 'block';
            });
        });
        function closeModal() {
            document.getElementById('question-modal').style.display = 'none';
        }
        window.onclick = function (event) {
            var modal = document.getElementById('question-modal');
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        }
        function selectDate(selectedDate, element) {
            // Cập nhật giá trị cho txtNgayKham
            var txtNgayKhamElem = document.getElementById('<%= txtNgayKham.ClientID %>');
        txtNgayKhamElem.value = selectedDate;

        // Xóa active của tất cả các box
        var boxes = document.querySelectorAll('.date-box');
        boxes.forEach(function (box) {
            box.classList.remove('active');
        });

        // Thêm active cho box được chọn
        element.classList.add('active');

        // Kích hoạt sự kiện OnTextChanged
        __doPostBack('<%= txtNgayKham.UniqueID %>', '');
        }
        function showAlert(message, iconType) {
            Swal.fire({
                title: message,

                icon: iconType,
                confirmButtonText: 'OK'
            });
        }
        function scrollToPatientInfo() {
            var element = document.querySelector('h3:contains("Thông tin khách hàng")');
            if (element) {
                element.scrollIntoView({ behavior: 'smooth' });
            }
            Swal.fire({
                title: 'Xác nhận đăng ký',
                text: "Bạn có chắc chắn muốn đăng ký lịch khám này?",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Đồng ý',
                cancelButtonText: 'Hủy bỏ'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack('<%= btnDangKy.UniqueID %>', '');
            }
        });
            return false;
        }
    </script>
    <script>
        function confirmRegistration() {
            Swal.fire({
                title: 'Xác nhận đăng ký',
                text: "Bạn có chắc chắn muốn đăng ký lịch khám này?",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Đồng ý',
                cancelButtonText: 'Hủy bỏ'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Gọi server-side click event khi người dùng xác nhận
                    __doPostBack('<%= btnDangKy.UniqueID %>', '');
            }
        });
            return false;
        }
    </script>
    <script>
        function validateForm() {
            // Kiểm tra các trường bắt buộc
            var isValid = true;
            var errorFields = [];

            // Kiểm tra mã bệnh nhân
            if (document.getElementById('<%= txtemail1.ClientID %>').value.trim() === '') {
            errorFields.push('Email bệnh nhân');
            isValid = false;
        }

        // Kiểm tra chuyên khoa
        if (document.getElementById('<%= ddlChuyenKhoa.ClientID %>').value === 'Chọn chuyên khoa') {
            errorFields.push('Chuyên khoa');
            isValid = false;
        }

        // Kiểm tra bệnh viện/phòng khám
        if (document.getElementById('<%= ddlPhongKham.ClientID %>').value === '') {
            errorFields.push('Phòng khám');
            isValid = false;
        }

        // Kiểm tra bác sĩ
        if (document.getElementById('<%= ddlBacSi.ClientID %>').value === '0') {
            errorFields.push('Bác sĩ');
            isValid = false;
        }

        // Kiểm tra ngày khám
        if (document.getElementById('<%= txtNgayKham.ClientID %>').value.trim() === '') {
            errorFields.push('Ngày khám');
            isValid = false;
        }

        // Kiểm tra buổi khám
        if (document.getElementById('<%= ddlbuoikham.ClientID %>').value === '') {
            errorFields.push('Buổi khám');
            isValid = false;
        }


        // Kiểm tra triệu chứng
        if (document.getElementById('<%= txtTrieuChung.ClientID %>').value.trim() === '') {
                errorFields.push('Triệu chứng');
                isValid = false;
            }

            // Nếu có lỗi, hiển thị thông báo
            if (!isValid) {
                var errorMessage = 'Vui lòng điền đầy đủ thông tin. Các trường chưa chọn: ' + errorFields.join(', ');
                Swal.fire({
                    title: 'Thiếu thông tin',
                    text: errorMessage,
                    icon: 'warning',
                    confirmButtonText: 'OK'
                });
            }

            return isValid;
        }
    </script>
</asp:Content>
