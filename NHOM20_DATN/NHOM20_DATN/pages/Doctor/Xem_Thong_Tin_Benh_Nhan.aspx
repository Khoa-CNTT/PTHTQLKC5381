<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Doctor_MasterPage.Master" AutoEventWireup="true" CodeBehind="Xem_Thong_Tin_Benh_Nhan.aspx.cs" Inherits="NHOM20_DATN.Xem_Thong_Tin_Benh_Nhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f7f9;
            margin: 0;
            padding: 0;
        }

        .contain-ttbn{
            background-color: #d9d9d96e;
        }

        .bang-header {
            margin-left: -5px;
        }
        /* Container */
        .Phankhung {
            max-width: 1100px;
            margin: 20px auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            background-color: #ffffff;
        }

        /* Search Bar */
        .seach_bar {
            display: flex;
            align-items: center;
        }

            .seach_bar input[type="text"] {
                padding: 10px;
                border: 1px solid #ccc;
                border-radius: 4px;
                width: 280px;
                margin-right: 10px;
            }

        .btn_search {
            background-color: #007bff;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 10px;
            cursor: pointer;
            display: flex;
            margin-left: -5px;
            text-decoration: none
        }

        .doctor_tb tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .doctor_tb tr:nth-child(odd) {
            background-color: #ffffff;
        }

        .btn_search:hover {
            background-color: #0056b3;
        }

        /* Dropdowns */
        select {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            margin-right: 10px;
        }

        /* GridView */
        .doctor_tb {
            width: 1075px;
            border: none;
            margin-top: 20px;
            margin-left: -8px;
            font-size: 12px;
        }

            .doctor_tb th, .doctor_tb td {
                padding: 12px;
                text-align: left;
                border-bottom: 1px solid #ddd;
            }

            .doctor_tb th {
                background-color: #007bff;
                color: white;
            }

            .doctor_tb tr:hover {
                background-color: #f1f1f1;
            }

        /* Responsive */
        @media (max-width: 768px) {

            .seach_bar {
                flex-direction: column;
                align-items: flex-start;
            }

                .seach_bar input[type="text"], .btn_search, select {
                    width: 100%;
                    margin-bottom: 10px;
                }

            #ddlPhongKham {
                width: 200px !important;
            }

            .Phankhung {
                padding: 15px;
                width: 450px;
                margin-left: -40px;
                box-sizing: border-box;
                overflow: hidden;
            }

            .dropdown1, .dropdown2 {
                width: 100px;
                font-size: 10px;
                height: 50px;
            }

            .dropdown2 {
                margin-left: -15px;
            }

            .tk1 {
                width: 120px !important;
            }

            .btn-refresh {
                width: 40px;
                height: 40px;
                margin-left: -10px !important;
                margin-top: 5px;
            }

            .btn_search {
                width: 40px;
                height: 40px;
                margin-top: -52px;
                margin-left: 130px;
            }

            .doctor_tb {
                width: 700px;
                display: block;
                display: table;
            }

                .doctor_tb th {
                    white-space: nowrap;
                    font-size: 12px !important;
                }

                .doctor_tb td {
                    width: 1200px;
                    min-width: 100%;
                    white-space: nowrap;
                    font-size: 12px !important;
                }

            .boc {
                width: 500px;
                margin-left: -10px;
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
                display: block;
                padding-bottom: 10px;
            }

                .boc::-webkit-scrollbar-track {
                    background: #f1f1f1;
                    border-radius: 10px;
                }

                .boc::-webkit-scrollbar {
                    height: 10px;
                    background-color: #f5f5f5;
                }

                .boc::-webkit-scrollbar-thumb {
                    background-color: #007bff;
                    border-radius: 6px;
                    border: 2px solid #f5f5f5;
                }

                    .boc::-webkit-scrollbar-thumb:hover {
                        background: #0056b3;
                    }
        }

        @media (max-width: 992px) and (min-width: 769px) {
            .table-title {
                margin-left: -60px;
            }

            .Phankhung {
                padding: 15px;
                width: 700px;
                margin-left: 10px;
                box-sizing: border-box;
                overflow: hidden;
            }

            .dropdown1, .dropdown2 {
                width: 150px;
                font-size: 11px;
                height: 50px;
            }

            .dropdown2 {
                margin-left: -10px;
            }

            .tk1 {
                width: 240px !important;
            }

            .btn-refresh {
                width: 40px;
                height: 40px;
                margin-left: -10px !important;
                margin-top: 5px;
            }

            .btn_search {
                width: 40px;
                height: 40px;
            }

            .doctor_tb {
                width: 800px;
                display: block;
                display: table;
            }

                .doctor_tb th {
                    white-space: nowrap;
                    font-size: 12px !important;
                }

                .doctor_tb td {
                    width: 1200px;
                    min-width: 100%;
                    white-space: nowrap;
                    font-size: 12px !important;
                }

            .boc {
                width: 670px;
                margin-left: -10px;
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
                display: block;
                padding-bottom: 10px;
            }


                .boc::-webkit-scrollbar-track {
                    background: #f1f1f1;
                    border-radius: 10px;
                }

                .boc::-webkit-scrollbar {
                    height: 10px;
                    background-color: #f5f5f5;
                }

                .boc::-webkit-scrollbar-thumb {
                    background-color: #007bff;
                    border-radius: 6px;
                    border: 2px solid #f5f5f5;
                }

                    .boc::-webkit-scrollbar-thumb:hover {
                        background: #0056b3;
                    }
        }

        @media (max-width: 1200px) and (min-width: 993px) {
            .Phankhung {
                padding: 15px;
                width: 600px;
                margin-left: 20px;
                box-sizing: border-box;
                overflow: hidden;
            }

            .dropdown1, .dropdown2 {
                width: 130px;
                font-size: 11px;
                height: 50px;
            }

            .dropdown2 {
                margin-left: -10px;
            }

            .tk1 {
                width: 200px !important;
            }

            .btn-refresh {
                width: 40px;
                height: 40px;
                margin-left: -10px !important;
                margin-top: 5px;
            }

            .btn_search {
                width: 40px;
                height: 40px;
            }

            .doctor_tb {
                width: 800px;
                display: block;
                display: table;
            }

                .doctor_tb th {
                    white-space: nowrap;
                    font-size: 12px !important;
                }

                .doctor_tb td {
                    width: 1200px;
                    min-width: 100%;
                    white-space: nowrap;
                    font-size: 12px !important;
                }

            .boc {
                width: 670px;
                margin-left: -10px;
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
                display: block;
                padding-bottom: 10px;
            }


                .boc::-webkit-scrollbar-track {
                    background: #f1f1f1;
                    border-radius: 10px;
                }

                .boc::-webkit-scrollbar {
                    height: 10px;
                    background-color: #f5f5f5;
                }

                .boc::-webkit-scrollbar-thumb {
                    background-color: #007bff;
                    border-radius: 6px;
                    border: 2px solid #f5f5f5;
                }

                    .boc::-webkit-scrollbar-thumb:hover {
                        background: #0056b3;
                    }
        }

        .table-title {
            font-size: 24px;
            font-weight: bold;
            margin-top: 20px;
            margin-bottom: 20px;
            color: deepskyblue;
        }

        .pagination a, .pagination span {
            padding: 8px 12px;
            margin: -10px;
            border: 1px solid #ccc;
            background-color: cornflowerblue;
            color: white;
            text-decoration: none;
            border-radius: 4px;
        }

        #content {
            width: 75%;
            margin: 0 auto;
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 14px;
        }

        .pagination a:hover {
            background-color: #0056b3;
        }

        .btn-refresh {
            background-color: #007bff;
            color: white;
            padding: 10px 12px;
            border: none;
            border-radius: 10px;
            cursor: pointer;
            margin-left: -10px;
            text-decoration: none;
            display: inline-flex;
            align-items: center;
        }

            .btn-refresh:hover {
                background-color: #0056b3;
            }

            .btn-refresh i {
                font-size: 14px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="table-title">Thông Tin Bệnh Nhân</h2>
    <div class="Phankhung">

        <div class="bang">
            <div class="bang-header">

                <div class="seach_bar">
                    <asp:TextBox ID="txt_Searching" placeholder="Tìm kiếm" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="btn_Search" CssClass="btn_search" OnClick="btn_Search_Click" runat="server"><i class="fa-solid fa-magnifying-glass"></i> </asp:LinkButton>
                </div>
                <asp:DropDownList ID="ddlPhongKham" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlPhongKham_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlChuyenKhoa" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlChuyenKhoa_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn-refresh" OnClick="btnRefresh_Click">
    <i class="fas fa-sync-alt"></i>
                </asp:LinkButton>

            </div>
            <div class="boc">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    OnPageIndexChanging="gridDoctor_PageIndexChanging" AllowPaging="true" PageSize="7"
                    CssClass="doctor_tb" PagerStyle-CssClass="pagination">
                    <Columns>
                        <asp:BoundField DataField="IDBenhNhan" HeaderText="Mã Bệnh Nhân" ReadOnly="true" />
                        <asp:TemplateField HeaderText="Tên bệnh nhân">
                            <ItemTemplate>
                                <asp:Label ID="lblHoTen" runat="server" Text='<%# Eval("HoTen") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHoTen" runat="server" Text='<%# Bind("HoTen") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NgaySinh" HeaderText="Ngày Sinh" />
                        <asp:BoundField DataField="IDPhongKham" HeaderText="Phòng Khám" />
                        <asp:BoundField DataField="ChuyenKhoa" HeaderText="Chuyên khoa" />
                        <asp:BoundField DataField="GioiTinh" HeaderText="Giới Tính" />
                        <asp:BoundField DataField="SoDienThoai" HeaderText="Số điện thoại" />

                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <%-- <asp:BoundField DataField="TrangThai" HeaderText="TrangThai" />--%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
