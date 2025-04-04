<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Thong_Tin_Ca_Nhan.aspx.cs" Inherits="NHOM20_DATN.Patient.Quan_Ly_Thong_Tin_Ca_Nhan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <style>
    .khungthongtin {
        background: linear-gradient(135deg, #6a11cb, #2575fc);
        border-radius: 12px;
        padding: 30px;
        color: white;
    }
    .text{
        color: #2575fc;
        font-weight:bold;
    }

    .profile-header {
        color: white;
    }

    .profile-container {
        background: rgba(255, 255, 255, 0.1);
        border-radius: 15px;
        padding: 2rem;
    }

    .personal-info-card {
        background-color: white;
        border-radius: 12px;
        transition: transform 0.3s ease, box-shadow 0.3s ease;
    }

        .personal-info-card:hover {
            transform: scale(1.02);
            box-shadow: 0 12px 20px rgba(0, 0, 0, 0.2);
        }

    .form-control {
        border-radius: 25px;
        padding: 10px 15px;
        background-color: #f8f9fa;
    }

    .btn-gradient {
        background: linear-gradient(45deg, #ff6b6b, #f94d6a);
        color: white;
        border: none;
        transition: background 0.3s ease;
    }

        .btn-gradient:hover {
            background: linear-gradient(45deg, #f94d6a, #ff6b6b);
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container mt-5">
    <div class="profile-container card p-4 shadow-lg khungthongtin">
        <div class="profile-header text-center mb-4">
            <h2 class="text">Quản lý thông tin cá nhân</h2>
            <img src="img/icon_nguoidung.png" style="max-width: 100px;" class="rounded-circle img-thumbnail mb-3" alt="Avatar">
        </div>
        <asp:DataList ID="dlThongTinCaNhan" runat="server" RepeatColumns="1" >
            <ItemTemplate>
                <div class="card mb-4 personal-info-card shadow-lg p-4">
                    <div class="form-group mb-3">
                        <label class="form-label">Họ tên:</label>
                        <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control rounded-pill" Text='<%# Eval("HoTen") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvlHoTen" runat="server" ControlToValidate="txtHoTen" ErrorMessage="Vui lòng nhập họ tên." CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <div class="form-group mb-3">
                        <label class="form-label">Ngày sinh:</label>
                        <asp:TextBox ID="txtNgaySinh" runat="server" CssClass="form-control rounded-pill" Text='<%# Eval("NgaySinh", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNgaySinh" runat="server" ControlToValidate="txtNgaySinh" ErrorMessage="Vui lòng chọn ngày sinh." CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <div class="form-group mb-3">
                        <label class="form-label">Giới tính:</label><br />
                        <div class="d-inline-block">
                            <asp:RadioButton ID="rdoNam" runat="server" GroupName="GioiTinh" Text="Nam" CssClass="form-check-input" Checked='<%# Eval("GioiTinh").ToString() == "Nam" %>' />
                        </div>
                        <div class="d-inline-block ms-3">
                            <asp:RadioButton ID="rdoNu" runat="server" GroupName="GioiTinh" Text="Nữ" CssClass="form-check-input" Checked='<%# Eval("GioiTinh").ToString() == "Nu" %>' />
                        </div>
                        <div class="d-inline-block ms-3">
                            <asp:RadioButton ID="rdoKhac" runat="server" GroupName="GioiTinh" Text="Khác" CssClass="form-check-input" Checked='<%# Eval("GioiTinh").ToString() == "Khac" %>' />
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <label class="form-label">Căn cước công dân:</label>
                        <asp:TextBox ID="txtCanCuocCongDan" runat="server" CssClass="form-control rounded-pill" Text='<%# Eval("CanCuocCongDan") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revCanCuocCongDan" runat="server" ControlToValidate="txtCanCuocCongDan" CssClass="text-danger" ErrorMessage="Căn cước công dân phải đúng 12 chữ số." 
                                                        ValidationExpression="^\d{12}$">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rlvCanCuocCongDan" runat="server" ControlToValidate="txtCanCuocCongDan" ErrorMessage="Vui lòng nhập căn cước công dân" CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <div class="form-group mb-3">
                        <label class="form-label">Địa chỉ:</label>
                        <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control rounded-pill" Text='<%# Eval("DiaChi") %>'></asp:TextBox>
                    </div>
                    <div class="form-group mb-3">
                        <label class="form-label">Số điện thoại:</label>
                        <asp:TextBox ID="txtSoDienThoai" runat="server" CssClass="form-control rounded-pill" Text='<%# Eval("SoDienThoai") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="rvSoDienThoai" runat="server" ControlToValidate="txtSoDienThoai" CssClass="text-danger" 
                        ErrorMessage="Số điện thoại không hợp lệ. Vui lòng nhập 10 chữ số, bắt đầu bằng 03, 05, 07, 08, hoặc 09."
                        ValidationExpression="^(03|05|07|08|09)\d{8}$">
                        </asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator ID="rfvSDT" runat="server" ControlToValidate="txtSoDienThoai" ErrorMessage="Vui lòng nhập số điện thoại" CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <div class="form-group mb-3">
                        <label class="form-label">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control rounded-pill" Text='<%# Eval("Email") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" Display="Dynamic" runat="server" ForeColor="Red"
                         ErrorMessage="Địa chỉ email không hợp lệ"
                         ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$">
                         Địa chỉ email không hợp lệ
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Vui lòng nhập email" CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <asp:Button ID="btnCapNhat" runat="server" CssClass="btn btn-gradient w-100 py-2 text-uppercase" Text="Cập nhật" CommandArgument='<%# Eval("ID") %>' OnClick="btnCapNhat_Click" />
                    <asp:Button ID="btnHuyCapNhat" runat="server" CssClass="btn btn-secondary w-100 mt-2 py-2 text-uppercase" Text="Hủy cập nhật" OnClick="btnHuyCapNhat_Click" />
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>
</asp:Content>
