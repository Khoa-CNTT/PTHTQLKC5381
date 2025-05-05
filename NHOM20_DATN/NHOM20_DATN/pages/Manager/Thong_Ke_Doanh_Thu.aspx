<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Thong_Ke_Doanh_Thu.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Thong_Ke_Doanh_Thu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Thống kê doanh thu</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        body {
            background-color: #f8f9fa;
        }

        .card {
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .form-select {
            max-width: 700px;
            display: inline-block;
        }

        .header-title {
            font-size: 24px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body>
        <div class="container py-5">
            <div class="card p-4">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <div class="header-title">
                        <i class="fa-solid fa-chart-column me-2 text-primary"></i>
                        Thống kê doanh thu
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlLoaiThongKe" runat="server" CssClass="form-select me-2" AutoPostBack="true" OnSelectedIndexChanged="ddlLoaiThongKe_SelectedIndexChanged">
                            <asp:ListItem Text="Theo tuần" Value="week" />
                            <asp:ListItem Text="Theo tháng" Value="month" />
                            <asp:ListItem Text="Theo năm" Value="year" />
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlNam" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlLoaiThongKe_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <canvas id="chartDoanhThu" height="100"></canvas>
                <canvas id="doanhThuChart" width="400" height="200"></canvas>

            </div>
        </div>

        <script>
            // Lấy dữ liệu JSON từ server (C#)
            const labels = <%= LabelsJson %>;  // Nhãn của biểu đồ (Tháng 1, Tháng 2,...)
            const data = <%= DataJson %>;      // Dữ liệu doanh thu theo từng tuần/tháng/năm

            // Biểu đồ cột
            const ctx1 = document.getElementById('chartDoanhThu').getContext('2d');
            new Chart(ctx1, {
                type: 'bar',  // Loại biểu đồ: cột
                data: {
                    labels: labels,  // Nhãn (X-axis)
                    datasets: [{
                        label: 'Doanh thu (VNĐ)', // Tiêu đề biểu đồ
                        data: data,  // Dữ liệu doanh thu
                        backgroundColor: 'rgba(54, 162, 235, 0.7)', // Màu nền cột
                        borderColor: 'rgba(54, 162, 235, 1)', // Màu viền cột
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            display: true,
                            position: 'top'
                        }
                    },
                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                callback: function (value) {
                                    return value.toLocaleString('vi-VN') + ' VNĐ';  // Hiển thị giá trị với định dạng VNĐ
                                }
                            }
                        }
                    }
                }
            });

            // Biểu đồ miền
            const ctx2 = document.getElementById('doanhThuChart').getContext('2d');
            new Chart(ctx2, {
                type: 'line',  // Loại biểu đồ: line (biểu đồ miền)
                data: {
                    labels: labels,  // Nhãn (X-axis)
                    datasets: [{
                        label: 'Doanh Thu', // Tiêu đề biểu đồ
                        data: data,  // Dữ liệu doanh thu
                        fill: true,   // Để vẽ biểu đồ miền (không phải chỉ là đường)
                        borderColor: 'rgba(75, 192, 192, 1)', // Màu đường viền
                        backgroundColor: 'rgba(75, 192, 192, 0.2)', // Màu nền bên dưới đường
                        tension: 0.4  // Độ cong của đường
                    }]
                },
                options: {
                    responsive: true,  // Đảm bảo biểu đồ responsive với kích thước màn hình
                    scales: {
                        y: {
                            beginAtZero: true,  // Đảm bảo trục Y bắt đầu từ 0
                            ticks: {
                                callback: function (value) {
                                    return value.toLocaleString('vi-VN') + ' VNĐ';  // Hiển thị giá trị với định dạng VNĐ
                                }
                            }
                        }
                    }
                }
            });
        </script>
        <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    </body>
</asp:Content>
