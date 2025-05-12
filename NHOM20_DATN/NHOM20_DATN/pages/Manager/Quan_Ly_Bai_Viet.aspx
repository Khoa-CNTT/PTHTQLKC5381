<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Quan_Ly_Bai_Viet.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Quan_Ly_Bai_Viet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="../../style/manager/QLBaiviet.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
    <div id="container_content">
        <h1>QUẢN LÝ BÀI VIẾT</h1>
        <%-- Contain search and create news --%>
        <div class="contain_function d_flex">
            <div class="search_bar">
                <asp:TextBox ID="txt_Searching" CssClass="input_search" placeholder="Tìm kiếm" runat="server"></asp:TextBox>
                <asp:LinkButton ID="btn_Search" CssClass="btn_search" OnClick="btn_Search_Click" runat="server"><i class="fa-solid fa-magnifying-glass"></i> </asp:LinkButton>
            </div>
            <div id="contain_add">
                <%--<asp:LinkButton ID="btn_add_news" runat="server" OnClientClick="clear_open_formAddNews(); return false;" class="btn_add_news"><i class="fa-solid fa-plus"></i>Thêm bài viết</asp:LinkButton>--%>
                <%--<a href="#"><i class="fa-solid fa-plus"></i>Thêm bài viết</a>--%>
                <button id="btn_add_news" class="btn_add_news" onclick="open_formAddNews(); return false;"><i class="fa-solid fa-plus"></i>Thêm bài viết </button>
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
                            <a href="/Home_Component/Tin_Tuc.aspx?maBV=<%# Eval("IDBaiViet") %>">
                                <!--Link news-->
                                <img src='<%#  Eval("HinhAnh").ToString().Split(',')[0] %>' alt="">
                            </a>

                            <div class="news_des">
                                <!--Caption-->
                                <a href="/Home_Component/Tin_Tuc.aspx?maBV=<%# Eval("IDBaiViet") %>">
                                    <h3 title="<%# Eval("TieuDe") %>"><%# Eval("TieuDe") %></h3>
                                </a>
                                <!--Caption-->
                                <!--date-->
                                <p><i class="fa-regular fa-calendar-days" style="margin-right: 5px;"></i><%# DateTime.Parse( Eval("NgayDang").ToString()).ToString("dd/MM/yyyy") %></p>
                                <!--date-->
                                <!--Edit news-->
                                <div class="contain_btn_news">
                                    <!--detail-->
                                    <div class="detail_news"><a href="/Home_Component/Tin_Tuc.aspx?maBV=<%# Eval("IDBaiViet") %>">Xem thêm</a></div>
                                    <!--edit -->
                                    <%--<asp:Button class="edit_btn" CssClass="edit_btn" ID="edit_btn" OnClick="edit_News" runat="server" Text="Sửa" />--%>
                                    <button id="edit_btn" class="edit_btn" onclick="loadDataFromServer(<%# Eval("IDBaiViet") %>);open_formAddNews(); return false;">Sửa</button>
                                    <!--delete-->
                                    <asp:Button ID="delete_btn" CssClass="delete_btn" OnClientClick='<%# "showCancelDialog(\"" + Eval("IDBaiViet") + "\"); return false;" %>'  runat="server" Text="Xóa" />
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
    <div id="container-addNews" class="">
        <div id="form_add_News">
            <h2>Cập Nhật Bài Viết</h2>
            <div id="btn_close_add" class="btn-close-add" onclick="callCloseForm()"><i class="fa-solid fa-xmark"></i></div>
     
            <asp:HiddenField ID="id_content" Value="" runat="server" />
            <asp:HiddenField ID="create_date" runat="server" />
            <asp:HiddenField ID="imageUrl" runat="server" />
            <div class="form-group">
                <b for="TieudeTxt">Tiêu Đề</b>
                <%--<asp:TextBox ID="tieude_txt" class="form-control" runat="server" placeholder=""></asp:TextBox>--%>
                <%-- Here --%>
                  <div><input id="TieudeTxt" class="form-control" type="text" placeholder="Nhập tiêu đề ... " /></div>
                <%-- Here --%>
            </div>
           <%-- <div class="form-group">
                <b for="noiDung_txt">Nội Dung</b>
                <asp:TextBox ID="noiDung_txt" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
            </div>
            <div class="upfile-group">
                <asp:FileUpload ID="fileImg" class="file-img" runat="server" />
                <asp:HiddenField ID="imgHidden" runat="server" />
            </div>--%>
            <%-- here --%>
            <div id="dynamicInputs"></div>
            <%-- Here --%>
            <div class="contain-action-update">
                <p style="margin: 0;"> <b>Thao tác </b></p>
                    <button type="button" class="btn-addnews" title="Thêm nội dung" onclick="addTextInput()"><i class="fa-solid fa-plus"></i> <i class="fa-solid fa-align-left"></i></button>
    <button id ="addFileBtn" class="btn-addimg" type="button" title="Thêm ảnh" onclick="addFileInput()"><i class="fa-solid fa-plus"></i> <i class="fa-solid fa-image"></i></button>
            </div>

            <%--  --%>
            <div class="btn_submit_news">
                <%--<asp:Button ID="Button_Addnews" class="btn-addnews" runat="server" Text="Cập Nhật" OnClick="Addnews_click" />--%>
                <button id="ContentPlaceHolder1_Button_Addnews" type="button" class="btn-addnews" onclick="checkAddOrUpdate(); ">Cập Nhật</button>
                <asp:Button ID="Button_Close_Addnews" class="btn-closenews" runat="server" Text="Đóng" OnClick="Closenews_click" />
            </div>
        </div>
    </div>


    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="/js/QLbaiviet/QLbv/Qlbv_script.js"></script>
    <script src="/js/QLbaiviet/QLbv/QLBV_update_script.js"></script>
<script>
    //close form add new 
    const btn_close = document.querySelector("#btn_close_add");
    const formNew = document.querySelector("#container-addNews");
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

    function callCloseForm() {
        __doPostBack('closeForm');
    }

    //open form function
    function open_formAddNews() {
        formNew.classList.add("d_block")
    }

  <%--  function clear_open_formAddNews() {
        //document.getElementById('<%= tieude_txt.ClientID %>').value = "";
      //  document.getElementById('<%= noiDung_txt.ClientID %>').value = "";
        open_formAddNews();
    }--%>

    function showAlert(notice, warn) {
        Swal.fire({
            title: notice,
            icon: warn,
            confirmButtonText: 'OK'
        });
    }
    function showCancelDialog(id) {
        Swal.fire({
            title: 'Bạn Có Muốn Xóa Bài Viết Không',
            showCancelButton: true,
            icon: 'warning', 
            confirmButtonText: 'Tiếp tục',
            cancelButtonText: 'Hủy bỏ'
        }).then((result) => {
            if (result.isConfirmed) {
                __doPostBack('deleteNews', id);
            }
        });
    }
   


</script>
</asp:Content>
