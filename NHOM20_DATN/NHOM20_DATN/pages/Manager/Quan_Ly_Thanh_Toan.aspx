<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Thanh_Toan.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Quan_Ly_Thanh_Toan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
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

        <div class="card-doanhthu">
            <canvas id="myChart" width="400" height="400"></canvas>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
        <script>
            // Tạo một biểu đồ khi trang tải
            var ctx = document.getElementById('myChart').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: [], // Mảng ngày
                    datasets: [{
                        label: 'Doanh Thu',
                        data: [], // Mảng doanh thu
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });

            function updateChart(data, labels) {
                myChart.data.labels = labels;
                myChart.data.datasets[0].data = data;
                myChart.update();
            }

            // Hàm để gọi API từ code-behind ASP.NET và cập nhật biểu đồ
            function fetchChartData() {
                // Đảm bảo gọi đến backend (code-behind) và lấy dữ liệu chart (sử dụng AJAX)
                $.ajax({
                    type: 'POST',
                    url: 'Quan_Ly_Thanh_Toan.aspx/GetChartData', // Gọi đến hàm CodeBehind
                    data: {},
                    success: function(response) {
                        var data = response.d.data;
                        var labels = response.d.labels;
                        updateChart(data, labels); // Cập nhật dữ liệu cho biểu đồ
                    }
                });
            }

            // Gọi hàm lấy dữ liệu khi trang tải
            fetchChartData();
        </script>

        <!-- Bootstrap JS -->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    </div>
</asp:Content>
