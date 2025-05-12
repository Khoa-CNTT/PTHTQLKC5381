<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="List_Tin_Tuc.aspx.cs" Inherits="NHOM20_DATN.Home_Component.List_Tin_Tuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/news_content_style.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--body-->
    <div id="container">
        <div id="container_content">
            <h1>TIN TỨC</h1>

            <div class="slide_bar">
                <asp:DataList ID="ListNews" CssClass="table_news" runat="server">
                    <ItemTemplate>
                        <div class="content_item">
                            <div class="title_news">
                                <a href="/Home_Component/Tin_Tuc.aspx?maBV=<%# Eval("IDBaiViet") %>">
                                    <img src='<%# Eval("HinhAnh").ToString().Split(',')[0] %>' alt="Ảnh Minh Họa">
                                </a>
                                <div class="news_des">
                                    <a href="/Home_Component/Tin_Tuc.aspx?maBV=<%# Eval("IDBaiViet") %>">
                                        <h3><%# Eval("TieuDe") %></h3>
                                    </a>
                                    <p>
                                        <i class="fa-regular fa-calendar-days"></i>
                                        <%#DateTime.TryParse(Eval("NgayDang")?.ToString(), out DateTime ngaycn) ? ngaycn.ToString("dd/MM/yyyy") : "" %>
                                    </p>
                                    <div class="detail_news"><a href="/Home_Component/Tin_Tuc.aspx?maBV=<%# Eval("IDBaiViet") %>">Xem thêm</a></div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>


</asp:Content>
