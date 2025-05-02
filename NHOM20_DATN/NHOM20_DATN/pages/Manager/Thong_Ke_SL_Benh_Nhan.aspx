<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Thong_Ke_SL_Benh_Nhan.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Thong_Ke_SL_Benh_Nhan" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../style/manager/thongkeBN_style.css" rel='stylesheet'>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="contain-all-tk">
        <div id="contain-action">
             <div class="contain-btn-fromto">
    
             </div>

            <div class="contain-btn-today">
                <asp:Button ID="btnToday" OnClick="btnToday_Click" runat="server" Text="Hôm Nay" />
            </div>

            <div class="contain-btn-thisweek">
                <asp:Button ID="btnThisWeek" OnClick="btnThisWeek_Click" runat="server" Text="Tuần Này" />
            </div>
            <div class="contain-btn-today">
                <asp:Button ID="btnThisMonth" OnClick="btnThisMonth_Click" runat="server" Text="Tháng Này" />
            </div>
            <div class="contain-btn-today">
                <asp:Button ID="btnThisYear" OnClick="btnThisYear_Click" runat="server" Text="Năm Nay" />
            </div>



        </div>

        <%-- =============== LIST==================== --%>
        <div id="container-list">
            <asp:GridView ID="GridView_All" runat="server" AutoGenerateColumns="True"
                CssClass="table table-bordered table-striped"
                OnPageIndexChanging="GridView_All_PageIndexChanging"
                AllowPaging="True" PageSize="5">
            <Columns>
                <asp:TemplateField HeaderText="Ngày Khám">
    <ItemTemplate>
        <asp:Label ID="lbl_Ngay" title='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngaycn) ? ngaycn.ToString("dd/MM/yyyy") : "" %>' runat="server" Text='<%#DateTime.TryParse(Eval("NgayKham")?.ToString(), out DateTime ngaycnT) ? ngaycnT.ToString("dd/MM/yyyy") : "" %>'></asp:Label><br />
    </ItemTemplate>
</asp:TemplateField>
                <asp:TemplateField HeaderText="Trạng Thái">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Ngay" title='' runat="server" Text=''></asp:Label><br />
                    </ItemTemplate>
                </asp:TemplateField>






                </Columns>
            </asp:GridView>
        </div>

        <%-- =============== CHART================= --%>
        <div id="container-chart">
            <div id="chart-item">
                <canvas id="myChart"></canvas>
            </div>

            <div id="count-total-contain">
                <div class="count-total-patient">
                    <h3>Tổng số bệnh nhân:
                        <asp:Label ID="lbl_totalPatient" runat="server" Text=""></asp:Label></h3>
                    <span>
                        <p>Tổng phiếu:
                            <asp:Label ID="lbl_allPk" runat="server" Text=""></asp:Label>
                        </p>
                    </span>
                    <span>
                        <p>Tổng Đã Đăng Ký:
                            <asp:Label ID="lbl_DaDangKy" runat="server" Text=""></asp:Label>
                        </p>
                    </span>
                    <span>
                        <p>Tổng Đã Hủy: 
                            <asp:Label ID="lbl_DaHuy" runat="server" Text=""></asp:Label></p>
                    </span>
                </div>
            </div>
        </div>
    </div>


    <script src="../../js/library/ChartJs.js"></script>

    <script>
        var labels = <%= labelsJson %>;
        var huyData = <%= huyDataJson %>;
        var dkData = <%= dkDataJson %>;

        var chart = new Chart(document.getElementById('myChart'), {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Đã Đăng Ký',
                        data: dkData,
                        backgroundColor: 'rgba(54, 162, 235, 0.7)'
                    },
                    {
                        label: 'Đã Hủy',
                        data: huyData,
                        backgroundColor: 'rgba(255, 99, 132, 0.7)'
                    }
                ]
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
