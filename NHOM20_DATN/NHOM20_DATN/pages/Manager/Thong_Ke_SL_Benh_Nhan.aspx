<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Thong_Ke_SL_Benh_Nhan.aspx.cs" Inherits="NHOM20_DATN.pages.Manager.Thong_Ke_SL_Benh_Nhan" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Chart ID="Chart1" runat="server" Width="600px" Height="400px">
    <Series>
        <asp:Series Name="Nam" ChartType="Line" />
        <asp:Series Name="Nữ" ChartType="Line" />
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1" />

    </ChartAreas>
</asp:Chart>






</asp:Content>
