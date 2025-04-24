<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Bai_Viet.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Quan_Ly_Bai_Viet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="../../style/manager/QLBaiviet.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
    <div id="container_content">
        <h1>BÀI VIẾT SỨC KHỎE</h1>
        <%-- Contain search and create news --%>
        <div class="contain_function d_flex">
            <div class="search_bar">
                <asp:TextBox ID="txt_Searching" CssClass="input_search" placeholder="Tìm kiếm" runat="server"></asp:TextBox>
                <asp:LinkButton ID="btn_Search" CssClass="btn_search" OnClick="btn_Search_Click" runat="server"><i class="fa-solid fa-magnifying-glass"></i> </asp:LinkButton>
            </div>
            <div id="contain_add">
                <asp:LinkButton id="btn_add_news" runat="server" OnClientClick="clear_open_formAddNews(); return false;" class="btn_add_news"><i class="fa-solid fa-plus"></i>Thêm bài viết</asp:LinkButton> <%--<a href="#"><i class="fa-solid fa-plus"></i>Thêm bài viết</a>--%>
            </div>
        </div>
        <%-- Contain search and create news --%>
        <div class="slide_bar">
            <asp:DataList ID="ds_baiviet" runat="server">
                <ItemTemplate>
                    <div class="content_item">
                        <%-- Show list data news --%>
                        <div class="title_news">
                            <%-- ID content --%>
                            <asp:HiddenField ID="id_Content" Value='<%# Eval("IDBaiViet") %>' runat="server" />
                            <%-- Image --%>
                            <a href="TinTuc_d_1.aspx?maBV=<%# Eval("IDBaiViet") %>">
                                <!--Link news-->
                                <img src="<%# Eval("HinhAnh") %>" alt="Không có ảnh">
                            </a>

                            <div class="news_des">
                                <!--Caption-->
                                <a href="TinTuc_d_1.aspx?maBV=<%# Eval("IDBaiViet") %>">
                                    <h3 title="<%# Eval("TieuDe") %>"><%# Eval("TieuDe") %></h3>
                                </a>
                                <!--Caption-->
                                <!--date-->
                                <p><i class="fa-regular fa-calendar-days" style="margin-right: 5px;"></i><%# DateTime.Parse( Eval("NgayDang").ToString()).ToString("dd/MM/yyyy") %></p>
                                <!--date-->
                                <!--Edit news-->
                                <div class="contain_btn_news">
                                    <!--detail-->
                                    <div class="detail_news"><a href="TinTuc_d_1.aspx?maBV=<%# Eval("IDBaiViet") %>">Xem thêm</a></div>
                                    <!--edit -->
                                    <asp:Button class="edit_btn" CssClass="edit_btn" ID="edit_btn" OnClick="edit_News" runat="server" Text="Sửa" />
                                    <!--delete-->
                                    <asp:Button ID="delete_btn" CssClass="delete_btn" OnClick="delete_News" runat="server" Text="Xóa" />
                                </div>
                                <!--Edit news-->
                            </div>
                        </div>
                </ItemTemplate>
            </asp:DataList>
            <%-- Show list data news ! --%>
        </div>
    </div>
</div>

<%-- Add news --%>
<div id="form_add_News">
    <div id="btn_close_add" class="btn-close-add" onclick="close_formAddNews()"><i class="fa-solid fa-xmark"></i></div>
    <asp:HiddenField ID="id_content" Value="" runat="server" />
    <asp:HiddenField ID="create_date" runat="server" />
    <asp:HiddenField ID="imageUrl" runat="server" />
    <div class="form-group">
        <b for="tieude_txt">Tiêu Đề</b>
        <asp:TextBox ID="tieude_txt" class="form-control" runat="server" placeholder=""></asp:TextBox>
    </div>
    <div class="form-group">
        <b for="noiDung_txt">Nội Dung</b>
        <asp:TextBox ID="noiDung_txt" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
    </div>
    <div class="upfile-group">
        <asp:FileUpload ID="fileImg" class="file-img" runat="server" />
        <asp:HiddenField ID="imgHidden"  runat="server" />
    </div>
    <div class="btn_submit_news">
    <asp:Button ID="Button_Addnews" class="btn-addnews" runat="server" Text="Cập Nhật" OnClick="Addnews_click" />
</div>
</div>
<%-- Add news --%>

<%-- ======Notification========== --%>
<div id="alert-succeed" class="alert alert-primary " role="alert">
    A simple primary alert with <a href="#" class="alert-link">an example link</a>. Give it a click if you like.
</div>


 <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<script>
    //close form add new 
    const btn_close = document.querySelector("#btn_close_add");
    const formNew = document.querySelector("#form_add_News");
    //open button
    const btn_open = document.querySelector("#btn_add_news");
    //noidung
    const noidung = document.querySelector("#ContentPlaceHolder1_noiDung_txt");
    //tieude 
    const tieude = document.querySelector("#ContentPlaceHolder1_tieude_txt");


    //close form function
    function close_formAddNews() {
        formNew.classList.remove("d_block")
    }
    //open form function
    function open_formAddNews() {
        formNew.classList.add("d_block")
    }

    function clear_open_formAddNews() {
        document.getElementById('<%= tieude_txt.ClientID %>').value = "";
        document.getElementById('<%= noiDung_txt.ClientID %>').value = "";
        open_formAddNews();
    }

    function showAlert(notice, warn) {
        Swal.fire({
            title: notice,
            icon: warn,
            confirmButtonText: 'OK'
        });
    }
</script>
</asp:Content>
