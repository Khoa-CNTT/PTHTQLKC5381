<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dang_Ky_Kham_Truc_Tiep.aspx.cs" Inherits="NHOM20_DATN.Dang_Ky_Kham_Truc_Tiep" %>

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
            background-color: #f7f7f7; /* màu nền bao quanh form */
            padding: 10px 0;
        }

        .breadcrumb {
            font-size: 14px;
            margin: 20px auto;
            color: #666;
            max-width: 900px;
            padding: 0 20px; /* canh lề ngang khi màn to */
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

        /* TIÊU ĐỀ CHUNG */
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
        /* CONTAINER CHÍNH */
        .main-container {
            max-width: 900px;
            margin: 0 auto;
            background-color: #fff;
            border-radius: 4px;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1); /* đổ bóng nhẹ */
        }

        /* FORM GROUP  */
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

        /* Radio, checkbox */
        .radio-group {
            display: flex;
            align-items: center;
            gap: 10px;
        }
        /* BUTTON */
        .btn-submit {
            display: inline-block;
            background-color: #3b40cc;
            color: #fff;
            padding: 10px 25px;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 10px;
        }

            .btn-submit:hover {
                background-color: #2a2fa3;
            }

        /* VALIDATION */
        .red_text {
            color: red;
            font-size: 14px;
            display: block; /* để xuống dòng */
            margin-top: 5px;
        }

        /* MODAL */
        .modall {
            display: none;
            position: fixed;
            z-index: 1000;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
        }

        .modall-content {
            background-color: white;
            margin: 10% auto;
            padding: 20px;
            border-radius: 10px;
            width: 80%;
            max-width: 600px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.3);
            text-align: center;
        }

        .modall .close {
            background-color: #007BFF;
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            margin-top: 10px;
        }

        .question {
            color: red;
            cursor: pointer;
            font-size: 15px;
            margin-left: 20px;
        }

        .image-row {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-bottom: 20px;
            gap: 10px;
        }

        .hinhbv, .hinhbs {
            max-width: 80px;
            height: auto;
            border-radius: 10px;
        }

        /* CHO MÀN HÌNH NHỎ */
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
        /* Để ô readonly xám nhẹ */
        input[readonly] {
            background-color: #f0f0f0;
            color: #a0a0a0;
            border: 1px solid #d0d0d0;
            cursor: not-allowed;
        }

            input[readonly]:focus {
                caret-color: transparent; /* Ẩn con trỏ */
            }

        .form-control {
            background-color: #fafafa; /* Màu nền mờ cho tất cả input, select */
            border: 1px solid #ccc;
            color: #333;
            padding: 8px 10px;
            border-radius: 4px;
        }

        select.form-control {
            /* Xóa mũi tên mặc định trên một số trình duyệt */
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            /* Thêm hình mũi tên của dropdow cho vừa mắt */
            background: url("data:image/svg+xml;utf8,<svg fill='%23333' height='24' viewBox='0 0 24 24' width='24' xmlns='http://www.w3.org/2000/svg'><path d='M7 10l5 5 5-5z'/></svg>") no-repeat right 0.75rem center / 1rem 1rem #fafafa;
            /* Chừa khoảng trống bên phải để không đè lên mũi tên */
            padding-right: 2rem;
            cursor: pointer;
        }

        .date-selection {
            display: flex;
            gap: 10px;
            flex-wrap: wrap; /* Tự xuống hàng nếu màn hình nhỏ */
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-bg">
        <div class="breadcrumb">
            <a href="#">Trang chủ</a>
            >      <span>Đăng ký khám</span>
        </div>
        <!-- Container chính -->
        <div class="main-container">


            <!-- Nội dung chi tiết đặt hẹn -->
            <h3>Nội dung chi tiết đặt hẹn</h3>
            <div class="thanhngang">
            </div>
            <div class="row">
                <!-- Bệnh viện/phòng khám -->
               
                <!-- Chuyên khoa -->
                <div class="form-group col-6">
                    <label for="ddlChuyenKhoa">Chuyên khoa</label>
                    <asp:DropDownList ID="ddlChuyenKhoa" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlChuyenKhoa_SelectedIndexChanged"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
                     <!-- Bệnh viện/phòng khám -->
                 <div class="form-group col-6">
     <label for="ddlPhongKham">Bệnh viện/phòng khám</label>
     <asp:DropDownList ID="ddlPhongKham" runat="server" AutoPostBack="true" CssClass="form-control">
     </asp:DropDownList>
 </div>
            </div>
            <div class="row">
                <!-- Bác sĩ -->
                <div class="form-group col-6">
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
                <!-- Có thể thêm cột trống hoặc tùy ý -->
                <div class="form-group col-6" style="display: flex; align-items: flex-end;">
                </div>
            </div>

            <!-- Thời gian khám -->
            <h3>Thời gian khám</h3>
            <div class="thanhngang">
            </div>
            <asp:TextBox ID="txtNgayKham" runat="server" TextMode="Date"
                AutoPostBack="true" OnTextChanged="txtNgayKham_TextChanged"
                Style="position: absolute; left: -1000px; width: 0; height: 0;" />
            <div class="row">
                <asp:Repeater ID="rptNgayKham" runat="server">
                    <HeaderTemplate>
                        <div class="date-selection">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <!-- Khi click vào date-box, giá trị (yyyy-MM-dd) sẽ được gán vào txtNgayKham -->
                        <div class="date-box" onclick="selectDate('<%# Eval("NgayValue") %>', this)">
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
                    </asp:DropDownList>
                </div>
                <div class="form-group col-4">
                    <label for="DDLgiokham">Giờ khám</label>
                    <asp:DropDownList ID="DDLgiokham" runat="server" AutoPostBack="true"
                        CssClass="form-control">
                        <asp:ListItem Value="" Text="Giờ Khám"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <!-- Thông tin khách hàng -->
            <h3>Thông tin khách hàng</h3>
            <div class="thanhngang">
            </div>
            <div class="row">
                <div class="form-group col-6">
                    <label for="txtHoTen">Họ và tên</label>
                    <asp:TextBox ID="txtHoTen" runat="server" placeholder="Họ và tên"
                        CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group col-6">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-6">
                    <label for="txtNgaySinh">Ngày tháng năm sinh</label>
                    <asp:TextBox ID="txtNgaySinh" runat="server" TextMode="Date"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-6">
                    <label for="txtSoDienThoai">Số điện thoại</label>
                    <asp:TextBox ID="txtSoDienThoai" runat="server" placeholder="Số điện thoại"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-6">
                    <label>Giới tính</label>
                    <div class="radio-group">
                        <asp:RadioButtonList ID="gtRadioList" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="Nam">Nam</asp:ListItem>
                            <asp:ListItem Value="Nu">Nữ</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group col-6">
                    <label for="txtCCCD">Căn cước công dân</label>
                    <asp:TextBox ID="txtCCCD" runat="server" placeholder="Căn cước công dân"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-12">
                    <label for="txtDiaChi">Địa chỉ</label>
                    <asp:TextBox ID="txtDiaChi" runat="server" placeholder="Địa chỉ"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-12">
                    <label for="txtTrieuChung">Triệu chứng</label>
                    <asp:TextBox ID="txtTrieuChung" runat="server" TextMode="MultiLine" Rows="4"
                        placeholder="Triệu chứng" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Ghi chú/Chính sách -->
            <p style="font-size: 14px; color: #555;">
                * Lưu ý: Hệ thống có gửi Email hoặc SĐT. Vui lòng... (các ghi chú nếu có)
            </p>

            <div class="row">
                <div class="form-group col-6">
                    <!-- Nút đăng ký -->
                    <asp:Button ID="btnDangKy" Text="Đăng ký" runat="server"
                        OnClick="btnDangKy_Click" CssClass="btn-submit" />
                </div>
                <!--<button type="submit" id="btnPayment" class="btn btn-primary">Thanh toán với VNPay</button>-->
                <div class="form-group col-6" style="display: flex; align-items: center;">
                    <!-- Link câu hỏi -->
                    <a class="question" data-answer="Hãy chọn phần dịch vụ ...">Bạn muốn biết phiếu khám?</a>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL -->
    <div id="question-modal" class="modall">
        <div class="modall-content">
            <div class="image-row">
                <div class="hinhanhbv1">
                    <img src="./IMG/logochinh.png" alt="logo" class="hinhbv">
                </div>
            </div>
            <div class="header" style="font-size: 1.2rem; font-weight: bold; margin-bottom: 10px;">
                <img src="./IMG/bga.png" alt="" class="hinhbs">
                BANANA XIN CHÀO QUÝ KHÁCH
            </div>
            <div class="modal-body" id="modal-text" style="font-size: 1rem; color: #333; margin-bottom: 20px;">
            </div>
            <button class="close" onclick="closeModal()">Đóng</button>
        </div>
    </div>

    <script>
        // Xử lý hiển thị modal khi click vào câu hỏi
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
            // Cập nhật giá trị cho txtNgayKham (control ẩn)
            var txtNgayKhamElem = document.getElementById('<%= txtNgayKham.ClientID %>');
            txtNgayKhamElem.value = selectedDate;

            // Nếu muốn trigger OnChange thì gọi __doPostBack để kích hoạt sự kiện OnTextChanged
            __doPostBack('<%= txtNgayKham.UniqueID %>', '');

            // Xử lý active cho các thẻ: xóa active của tất cả rồi thêm active cho thẻ được chọn
            var boxes = document.querySelectorAll('.date-box');
            boxes.forEach(function (box) {
                box.classList.remove('active');
            });
            element.classList.add('active');
        }
        function showAlert(message, iconType) {
            Swal.fire({
                title: message,

                icon: iconType,
                confirmButtonText: 'OK'
            });
        }
    </script>
</asp:Content>
