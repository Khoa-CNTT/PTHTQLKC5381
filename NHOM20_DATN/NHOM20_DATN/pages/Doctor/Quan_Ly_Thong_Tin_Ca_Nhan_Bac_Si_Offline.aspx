<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Doctor_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Thong_Tin_Ca_Nhan_Bac_Si_Offline.aspx.cs" Inherits="NHOM20_DATN.pages.Doctor.Quan_Ly_Thong_Tin_Ca_Nhan_Bac_Si_Offline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700&display=swap');

        body {
            background: linear-gradient(145deg, #e3f2fd, #ffffff);
            
            color: #333;
            margin: 0;
        }

        .content{
            font-family: 'Inter', sans-serif;
            margin-left:100px;
        }    
             .contain-tttv{
                 background-color: #d9d9d96e;
             }
        .info-container {
            background-color: #ffffff;
            border-radius: 20px;
            box-shadow: 0 8px 30px rgba(0, 0, 0, 0.1);
            padding: 50px;
            max-width: 960px;
            margin: 20px auto;
            margin-left: 50px;
            transition: all 0.3s ease-in-out;
        }

            .info-container h2 {
                font-size: 28px;
                font-weight: 700;
                color: #0d47a1;
                margin-bottom: 35px;
                text-align: center;
            }

        .table th {
            background-color: #0d47a1;
            color: #ffffff;
            font-weight: 600;
            font-size: 15px;
            text-align: center;
            padding: 12px;
            border: none;
        }

        .table td {
            vertical-align: middle;
            font-size: 14px;
            padding: 12px;
            border-top: 1px solid #e0e0e0;
        }

        .table input[type="text"] {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 10px;
            transition: border-color 0.3s;
        }

            .table input[type="text"]:focus {
                border-color: #1976d2;
                outline: none;
            }

        .btn {
            border-radius: 40px !important;
            padding: 10px 24px;
            font-weight: 600;
            font-size: 14px;
            color: #fff;
            background: linear-gradient(to right, #2196f3, #0d47a1);
            border: none;
            transition: all 0.3s ease;
        }

            .btn:hover {
                background: linear-gradient(to right, #1976d2, #0d47a1);
                transform: scale(1.03);
            }

        .table-striped tbody tr:hover {
            background-color: #f1f8ff;
        }

        .image-rounded {
            border-radius: 50%;
            object-fit: cover;
            width: 110px;
            height: 110px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="info-container">
            <h2>Thông tin cá nhân - Bác sĩ</h2>

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
                    <asp:ImageField DataImageUrlField="HinhAnh" HeaderText="🖼 Ảnh đại diện" ReadOnly="True">
                        <ControlStyle CssClass="image-rounded" />
                    </asp:ImageField>
                    <asp:CommandField ShowEditButton="True" UpdateText="💾 Lưu" CancelText="❌ Hủy" EditText="✏️ Sửa" />
                </Fields>
            </asp:DetailsView>
        </div>
    </div>

</asp:Content>
