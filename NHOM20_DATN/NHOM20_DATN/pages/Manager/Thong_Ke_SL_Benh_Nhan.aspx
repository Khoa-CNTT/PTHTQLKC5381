<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Thong_Ke_SL_Benh_Nhan.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Thong_Ke_SL_Benh_Nhan" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../style/manager/thongkeBN_style.css" rel='stylesheet'>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- =============== LIST==================== --%>
    <div id="container-list">

    </div>

    <%-- =============== CHART================= --%>
    <div id="container-chart">
        <div id="chart-item"><canvas id="myChart" ></canvas></div>
  
        <div></div>
    </div>



    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <script>
var ctx = document.getElementById('myChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'bar', // hoặc 'line', 'pie', 'doughnut', ...
    data: {
        labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
        datasets: [{
            label: 'Số bệnh nhân',
            data: [25, 32, 18, 40],
            backgroundColor: 'rgba(54, 162, 235, 0.6)'
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});
    </script>

</asp:Content>
