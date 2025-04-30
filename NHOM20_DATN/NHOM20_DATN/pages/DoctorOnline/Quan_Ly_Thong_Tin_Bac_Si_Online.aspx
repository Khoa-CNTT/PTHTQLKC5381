<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/DoctorOnline_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Thong_Tin_Bac_Si_Online.aspx.cs" Inherits="NHOM20_DATN.pages.DoctorOnline.Quan_Ly_Thong_Tin_Bac_Si_Online" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background: linear-gradient(to right, #e0f7fa, #ffffff);
        }

        .info-container {
            background-color: #ffffff;
            border-radius: 16px;
            box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
            padding: 30px;
            max-width: 800px;
            margin: 30px auto;
        }

            .info-container h2 {
                color: #00796b;
                font-weight: bold;
                margin-bottom: 25px;
            }

        .table-bordered th {
            background-color: #00796b;
            color: white;
            font-weight: 600;
            text-align: center;
        }

        .table-bordered td {
            vertical-align: middle;
        }

        .btn {
            border-radius: 20px !important;
            padding: 6px 20px;
            font-weight: 500;
        }

            .btn:hover {
                opacity: 0.9;
            }

        .table td, .table th {
            border: 1px solid #dee2e6;
        }

        .table input[type="text"] {
            width: 100%;
            padding: 5px;
            border-radius: 8px;
            border: 1px solid #ccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="info-container">
        <h2 class="text-center">Thông tin cá nhân - Bác sĩ Online</h2>

        <asp:DetailsView ID="dvThongTin" runat="server" AutoGenerateRows="False" DefaultMode="Edit"
            CssClass="table table-bordered table-striped"
            OnItemUpdating="dvThongTin_ItemUpdating"
            OnModeChanging="dvThongTin_ModeChanging">
            <Fields>
                <asp:BoundField DataField="HoTen" HeaderText="👨‍⚕️ Họ tên" ReadOnly="True" />
                <asp:BoundField DataField="ChuyenKhoa" HeaderText="🏥 Chuyên khoa" ReadOnly="True" />
                <asp:BoundField DataField="TrinhDo" HeaderText="🎓 Trình độ" />
                <asp:BoundField DataField="DiaChiPhongKham" HeaderText="📍 Địa chỉ phòng khám" />
                <asp:BoundField DataField="SoDienThoai" HeaderText="📱 Số điện thoại" />
                <asp:BoundField DataField="Email" HeaderText="📧 Email" />
                <asp:BoundField DataField="VaiTro" HeaderText="🧩 Vai trò" ReadOnly="True" />
                <asp:ImageField DataImageUrlField="HinhAnh" HeaderText="🖼 Ảnh đại diện" ReadOnly="True" ControlStyle-Width="100px" ControlStyle-Height="100px" />

                <asp:CommandField ShowEditButton="True" UpdateText="💾 Lưu" CancelText="❌ Hủy" EditText="✏️ Sửa" />
            </Fields>
        </asp:DetailsView>
    </div>
</asp:Content>
