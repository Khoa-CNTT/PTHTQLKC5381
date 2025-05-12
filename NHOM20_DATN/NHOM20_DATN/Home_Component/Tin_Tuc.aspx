<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tin_Tuc.aspx.cs" Inherits="NHOM20_DATN.Home_Component.Tin_Tuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="../style/new_dt.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <!-- body -->
<section>
    <div id="container">
        <div class="row">
            <div class="post_info">
                <div class="title">
                    <h1 id="title_txt" runat="server"></h1><!-- title -->
                </div>
                <div class="title_date color_A0A0A0">
                    <p id="date_txt" runat="server"><i class="fa-regular fa-calendar-days"></i></p><!-- Date -->
                </div>
            </div>
            <div class="detail_main">
                <div class="detail_content">
                    <figure class="">
                        <div class="detail_img">
                            <img id="imgContent" runat="server"  alt=""/>
                        </div>
                        <div>
                            
                        </div>
                               <asp:Literal ID="noiDung_literal" runat="server"></asp:Literal>

                        <!-- Content -->
                    </figure>
           



                 
                </div>
            </div>


        </div>
    
    </div>
</section>
</asp:Content>
