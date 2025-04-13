<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Thong_Tin_Tu_Van.aspx.cs" Inherits="NHOM20_DATN.Patient.Thong_Tin_Tu_Van" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <style>
        .container {
            margin-top: 50px;
        }

        .thongtin {
            background-color: #ffffff;
            border-radius: 20px;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            padding: 20px;
        }

        .text-primary {
            color: #007bff;
        }

        .btn-primary {
            background-color: #007bff;
            border: none;
        }

            .btn-primary:hover {
                background-color: #0056b3;
            }

        .overlay {
            display: none; /* Ban đầu ẩn overlay */
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Màu nền đen mờ */
            z-index: 9999; /* Hiển thị trên tất cả */
            text-align: center;
            color: white;
            font-size: 18px;
        }

        .overlay-content {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        .loading-spinner {
            border: 8px solid #f3f3f3; /* Màu nền của spinner */
            border-radius: 50%;
            border-top: 8px solid #3498db; /* Màu của hiệu ứng xoay */
            width: 50px;
            height: 50px;
            animation: spin 2s linear infinite; /* Hiệu ứng xoay liên tục */
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>

    <div class="container chitiet">
        <div class="row">
            <!-- Khung bên trái: Thông tin bác sĩ -->
            <div class="col-md-6">
                <div class="thongtin">
                    <h3 class="text-primary mb-4">Thông tin bác sĩ</h3>
                    <p>
                        <strong>ID Bác sĩ:</strong>
                        <asp:Label ID="lblIDBacSi" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Tên bác sĩ:</strong>
                        <asp:Label ID="lblTenBacSi" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Chuyên khoa:</strong>
                        <asp:Label ID="lblChuyenKhoa" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Trình độ:</strong>
                        <asp:Label ID="lblTrinhDo" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Email:</strong>
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Vai trò:</strong>
                        <asp:Label ID="lblVaiTro" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <!-- Khung bên phải: Thông tin bệnh nhân -->
            <div class="col-md-6">
                <div class="thongtin">
                    <h3 class="text-primary mb-4">Thông tin bệnh nhân</h3>
                    <p>
                        <strong>ID Bệnh Nhân:</strong>
                        <asp:Label ID="lblIDBenhNhan" runat="server" Text=""></asp:Label>
                    </p>
                    <div class="mb-3">
                        <label for="txtNgay" class="form-label"><strong>Ngày:</strong></label>
                        <asp:TextBox ID="txtNgay" runat="server" CssClass="form-control" TextMode="Date" placeholder="Chọn ngày"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtNgay" Display="Dynamic" ForeColor="Red" ErrorMessage="Bạn phải chọn ngày" ValidationGroup="DangKyGroup" runat="server">Bạn phải chọn ngày</asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label for="ddlGio" class="form-label"><strong>Giờ:</strong></label>
                        <asp:DropDownList ID="ddlGio" runat="server" CssClass="form-control" ValidationGroup="DangKyGroup">
                            <asp:ListItem Text="Chọn giờ" Value="" />
                            <%-- Giờ sáng: 7:00 AM to 11:00 AM --%>
                            <asp:ListItem Text="07:00" Value="07:00" />
                            <asp:ListItem Text="07:30" Value="07:30" />
                            <asp:ListItem Text="08:00" Value="08:00" />
                            <asp:ListItem Text="08:30" Value="08:30" />
                            <asp:ListItem Text="09:00" Value="09:00" />
                            <asp:ListItem Text="09:30" Value="09:30" />
                            <asp:ListItem Text="10:00" Value="10:00" />
                            <asp:ListItem Text="10:30" Value="10:30" />
                            <asp:ListItem Text="11:00" Value="11:00" />

                            <%-- Giờ chiều: 2:00 PM to 5:00 PM --%>
                            <asp:ListItem Text="14:00" Value="14:00" />
                            <asp:ListItem Text="14:30" Value="14:30" />
                            <asp:ListItem Text="15:00" Value="15:00" />
                            <asp:ListItem Text="15:30" Value="15:30" />
                            <asp:ListItem Text="16:00" Value="16:00" />
                            <asp:ListItem Text="16:30" Value="16:30" />
                            <asp:ListItem Text="17:00" Value="17:00" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlGio" InitialValue="" Display="Dynamic" ForeColor="Red"
                            ErrorMessage="Bạn phải chọn giờ" ValidationGroup="DangKyGroup" runat="server">
                            Bạn phải chọn giờ
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="mb-3">
                        <label for="txtTrieuChung" class="form-label"><strong>Triệu chứng:</strong></label>
                        <asp:TextBox ID="txtTrieuChung" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Nhập triệu chứng"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtTrieuChung" Display="Dynamic"
                            ForeColor="Red" ErrorMessage="Bạn phải nhập triệu chứng"
                            runat="server" ValidationGroup="DangKyGroup">
                            Bạn phải nhập triệu chứng
                        </asp:RequiredFieldValidator>
                    </div>
                    <br />
                    <div class="overlay" id="loadingOverlay">
                        <div class="overlay-content">
                            <div class="loading-spinner"></div>
                            <p>Vui lòng chờ trong giây lát...</p>
                        </div>
                    </div>
                    <asp:Button ID="btnDangKy" runat="server" Text="Đăng Ký" CssClass="btn btn-primary btn-block"
                        OnClick="btnDangKy_Click" OnClientClick="showOverlay(); return true;"
                        ValidationGroup="DangKyGroup" />
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>

    <script>
        function showOverlay() {
            document.getElementById("loadingOverlay").style.display = "block"; // Hiển thị overlay
        }

        function hideOverlay() {
            document.getElementById("loadingOverlay").style.display = "none"; // Ẩn overlay khi hoàn thành
        }
    </script>

</asp:Content>
