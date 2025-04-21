<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage_TuVanVien.Master" AutoEventWireup="true" CodeBehind="Tu_Van_Suc_Khoe_Ban_Dau.aspx.cs" Inherits="NHOM20_DATN.Consultant.Tu_Van_Suc_Khoe_Ban_Dau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="mb-4">Danh sách câu hỏi cần phản hồi</h3>

        <asp:Repeater ID="rptCauHoi" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-header fw-bold text-primary">
                        🧑 <%# Eval("HoTen") %> - <span class="text-muted" style="font-size: 0.9rem;"><%# Eval("ThoiGian", "{0:dd/MM/yyyy HH:mm}") %></span>
                    </div>
                    <div class="card-body">
                        <p><strong>Câu hỏi:</strong> <%# Eval("CauHoi") %></p>
                        <asp:TextBox ID="txtTraLoi" runat="server" TextMode="MultiLine" CssClass="form-control mb-2" Rows="3" placeholder="Nhập phản hồi tại đây..."></asp:TextBox>
                        <asp:Button ID="btnTraLoi" runat="server" Text="Gửi phản hồi" CssClass="btn btn-success btn-sm"
                            CommandArgument='<%# Eval("ID") %>' OnClick="btnTraLoi_Click" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
