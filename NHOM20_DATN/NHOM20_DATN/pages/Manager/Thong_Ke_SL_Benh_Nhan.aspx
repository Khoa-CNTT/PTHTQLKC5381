<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Thong_Ke_SL_Benh_Nhan.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Thong_Ke_SL_Benh_Nhan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    select  * from BenhNhan bn, PhieuKham pk
where bn.IDBenhNhan= pk.IDBenhNhan








</asp:Content>
