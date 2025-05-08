<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Thong_Ke_Benh_Nhan_Tu_Van.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Thong_Ke_Benh_Nhan_Tu_Van" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../style/manager/thongkeTV_style.css" rel='stylesheet'>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="contain-all-tk">
        <div class="title-function">
            <h3>Thống kê bệnh nhân tư vấn</h3>
        </div>
        <div id="contain-action">
            <%-- Filter fromdate todate --%>
            <div class="contain-btn-fromto">
                <div class="row date-filter">
                    <div class="col-md-4">
                        <label for="txtTuNgay" class="form-label">Từ ngày:</label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-4">
                        <label for="txtDenNgay" class="form-label">Đến ngày:</label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-4 d-flex align-items-end">
                        <asp:Button ID="btnFilter" runat="server" Text="Lọc theo ngày" CssClass="btn btn-outline-success" OnClick="btnFilter_Click" />
                    </div>
                </div>
            </div>
            <%--  Button action --%>
            <div id="contain-btn">
                <div class="contain-btn-today ">
                    <asp:Button ID="btnToday" OnClick="btnToday_Click" runat="server" Text="Hôm Nay" />
                </div>
                <div class="contain-btn-thisweek ">
                    <asp:Button ID="btnThisWeek" OnClick="btnThisWeek_Click" runat="server" Text="Tuần Này" />
                </div>
                <div class="contain-btn-thismonth ">
                    <asp:Button ID="btnThisMonth" OnClick="btnThisMonth_Click" runat="server" Text="Tháng Này" />
                </div>
                <div class="contain-btn-thisyear ">
                    <asp:Button ID="btnThisYear" OnClick="btnThisYear_Click" runat="server" Text="Năm Nay" />
                </div>
                <div class="contain-btn-all ">
                    <asp:Button ID="btnAll" OnClick="btnAll_Click" runat="server" Text="Tất cả" />
                </div>
                <div class="contain-btn-TV">
                    <a href="Thong_Ke_SL_Benh_Nhan.aspx">Thống Kê Bệnh Nhân</a>
                </div>
            </div>

        </div>

        <%-- =============== LIST==================== --%>
        <div id="container-list">
            <asp:GridView ID="GridView_All" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped"
                OnPageIndexChanging="GridView_All_PageIndexChanging"
                AllowPaging="True" PageSize="5">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ID" title='<%#Eval("IDTuVan")%>' runat="server" Text='<%#Eval("IDTuVan")%>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID Phiếu">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IDPK" title='<%#Eval("IDBacSi")%>' runat="server" Text='<%#Eval("IDBacSi")%>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID Bệnh Nhân">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IDBN" title='<%#Eval("IDBenhNhan")%>' runat="server" Text='<%#Eval("IDBenhNhan")%>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày ">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Ngay" title='<%#DateTime.TryParse(Eval("Ngay")?.ToString(), out DateTime ngaycn) ? ngaycn.ToString("dd/MM/yyyy") : "" %>' runat="server" Text='<%#DateTime.TryParse(Eval("Ngay")?.ToString(), out DateTime ngaycnT) ? ngaycnT.ToString("dd/MM/yyyy") : "" %>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Giờ">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Gio" title='<%#Eval("Gio")%>' runat="server" Text='<%#Eval("Gio")%>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trạng Thái">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TrangThai" title='<%#Eval("TrangThai")%>' runat="server" Text='<%#Eval("TrangThai")%>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <%-- =============== CHART================= --%>
        <div id="container-chart">
            <div id="chart-item" style="width: 75%; overflow-x: auto;">
                <canvas id="myChart"></canvas>
            </div>

            <div id="count-total-contain">
                <div class="count-total-patient">
                    <h3>Tổng số bệnh nhân:
                        <asp:Label ID="lbl_totalPatient" runat="server" Text=""></asp:Label></h3>
                    <span>
                        <p>
                            Tổng Số Bệnh Nhân Tư Vấn:
                            <asp:Label ID="lbl_allPk" runat="server" Text=""></asp:Label>
                        </p>
                    </span>
                </div>
                <div class="text-center my-4">
                    <asp:Button ID="btnXuatExcel" runat="server" Text="Xuất Excel" CssClass="btn btn-outline-success" OnClick="btnXuatExcel_Click" />
                </div>
            </div>
        </div>
    </div>


    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="/js/library/ChartJs.js"></script>

    <script>
        var labels = <%= labelsJson %>;
        var data = <%= dkDataJson %>;


        var chart = new Chart(document.getElementById('myChart'), {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Đã Đăng Ký',
                        data: data,
                        backgroundColor: 'rgba(54, 162, 235, 0.7)'
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
    <script>
        function showAlert(notice, warn) {
            Swal.fire({
                title: notice,
                icon: warn,
                confirmButtonText: 'OK'
            });
        }
    </script>






</asp:Content>
