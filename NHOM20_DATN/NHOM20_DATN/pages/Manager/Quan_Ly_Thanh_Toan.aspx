<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Thanh_Toan.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Quan_Ly_Thanh_Toan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .text-center{
            font-weight : bold;

        }
        .contain-qltt{
             background-color: #d9d9d96e;
        }

        .filter-buttons {
            margin-bottom: 20px;
        }

        .card-doanhthu {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 12px;
            margin-top: 20px;
            text-align: center;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            font-size: 20px;
            font-weight: bold;
            color : red;
        }

        .table-responsive {
            margin-top: 20px;
        }

        .date-filter {
            margin-bottom: 20px;
        }

        h2 {
            margin-top: 20px;
            margin-bottom: 20px;
        }
    </style>


    <div class="container">
        <div class="container">
            <h2 class="text-center">Quản lý thanh toán</h2>

            <!-- Bộ lọc theo ngày -->
            <div class="row date-filter">
                <div class="col-md-4">
                    <label for="txtTuNgay" class="form-label">Từ ngày:</label>
                    <asp:TextBox ID="txtTuNgay" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="col-md-4">
                    <label for="txtDenNgay" class="form-label">Đến ngày:</label>
                    <asp:TextBox ID="txtDenNgay" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="col-md-4 d-flex align-items-end">
                    <asp:Button ID="btnLocNgay" runat="server" Text="Lọc theo ngày" CssClass="btn btn-info w-100" OnClick="btnLocNgay_Click" />
                </div>
            </div>

            <!-- Các nút lọc nhanh -->
            <div class="filter-buttons text-center">
                <asp:Button ID="btnHomNay" runat="server" Text="Hôm nay" CssClass="btn btn-primary m-1" OnClick="btnHomNay_Click" />
                <asp:Button ID="btnTuanNay" runat="server" Text="Tuần này" CssClass="btn btn-success m-1" OnClick="btnTuanNay_Click" />
                <asp:Button ID="btnThangNay" runat="server" Text="Tháng này" CssClass="btn btn-warning m-1" OnClick="btnThangNay_Click" />
                <asp:Button ID="btnTatCa" runat="server" Text="Tất cả" CssClass="btn btn-secondary m-1" OnClick="btnTatCa_Click" />
            </div>

            <div class="table-responsive">
                <asp:GridView ID="GridViewThanhToan" runat="server" AutoGenerateColumns="True"
                    CssClass="table table-bordered table-striped" AllowPaging="True" PageSize="10" />
            </div>

            <div class="card-doanhthu">
                <asp:Label ID="lblTongDoanhThu" runat="server" />
            </div>

            <div class="text-center my-4">
                <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất Excel" CssClass="btn btn-outline-success" OnClick="btnXuatExcel_Click" />
            </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

        <!-- Bootstrap JS -->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    </div>
</asp:Content>
