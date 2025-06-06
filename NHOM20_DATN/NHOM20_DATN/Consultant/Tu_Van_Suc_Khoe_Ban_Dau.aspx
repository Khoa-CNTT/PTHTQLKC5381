﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage_TuVanVien.Master" AutoEventWireup="true" CodeBehind="Tu_Van_Suc_Khoe_Ban_Dau.aspx.cs" Inherits="NHOM20_DATN.Consultant.Tu_Van_Suc_Khoe_Ban_Dau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .custom-card {
            border: none;
            border-radius: 16px;
            box-shadow: 0 4px 18px rgba(0, 0, 0, 0.06);
            transition: all 0.3s ease;
        }

            .custom-card:hover {
                transform: scale(1.01);
                box-shadow: 0 6px 20px rgba(0, 0, 0, 0.08);
            }

            .custom-card .card-header {
                background: linear-gradient(135deg, #3b82f6, #1e40af);
                color: white;
                border-top-left-radius: 16px;
                border-top-right-radius: 16px;
            }

        .custom-btn {
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }

            .custom-btn i {
                font-size: 14px;
            }

        .custom-textarea {
            border-radius: 12px;
        }

        @media (max-width: 576px) {
            .custom-card {
                margin-bottom: 1.5rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container mt-5">
                <div class="row">
                    <!-- CỘT TRÁI: DANH SÁCH CÂU HỎI -->
                    <div class="col-lg-6 mb-5">
                        <h4 class="fw-bold text-primary mb-4">
                            <img src="../img/chat.png" style="width: 32px;" />
                            Câu hỏi cần phản hồi
                        </h4>

                        <asp:Repeater ID="rptCauHoi" runat="server">
                            <ItemTemplate>
                                <div class="card custom-card mb-4">
                                    <div class="card-header d-flex justify-content-between">
                                        <span>🧑 <%# Eval("HoTen") %></span>
                                        <small><i class="far fa-clock me-1"></i><%# Eval("ThoiGian", "{0:dd/MM/yyyy HH:mm}") %></small>
                                    </div>
                                    <div class="card-body">
                                        <p><strong>Câu hỏi:</strong> <%# Eval("CauHoi") %></p>
                                        <asp:TextBox ID="txtTraLoi" runat="server" TextMode="MultiLine"
                                            CssClass="form-control custom-textarea mb-3" Rows="3"
                                            placeholder="Nhập phản hồi tại đây..."></asp:TextBox>
                                        <asp:Button ID="btnTraLoi" runat="server" Text="Gửi phản hồi"
                                            CssClass="btn btn-success btn-sm custom-btn"
                                            CommandArgument='<%# Eval("ID") %>' OnClick="btnTraLoi_Click" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- CỘT PHẢI: LỊCH SỬ TƯ VẤN -->
                    <div class="col-lg-6">
                        <h4 class="fw-bold text-primary mb-4">
                            <img src="../img/clock.png" style="width: 32px;" />
                            Lịch sử tư vấn
                        </h4>

                        <div class="mb-3">
                            <label class="form-label fw-bold">Lọc theo ngày:</label>
                            <div class="d-flex gap-2">
                                <asp:TextBox ID="txtTuNgay" runat="server" CssClass="form-control" placeholder="Từ ngày (dd/MM/yyyy)" TextMode="Date"></asp:TextBox>
                                <asp:TextBox ID="txtDenNgay" runat="server" CssClass="form-control" placeholder="Đến ngày (dd/MM/yyyy)" TextMode="Date"></asp:TextBox>
                                <asp:Button ID="btnLocNgay" runat="server" CssClass="btn btn-primary" Text="Lọc" OnClick="btnLocNgay_Click" />
                            </div>
                        </div>

                        <asp:Repeater ID="rptLichSu" runat="server">
                            <ItemTemplate>
                                <div class="card custom-card mb-3">
                                    <div class="card-header d-flex justify-content-between">
                                        <span><strong>Câu hỏi:</strong> <%# Eval("CauHoi") %></span>
                                        <small><i class="far fa-clock me-1"></i><%# Eval("ThoiGian", "{0:dd/MM/yyyy HH:mm}") %></small>
                                    </div>
                                    <div class="card-body">
                                        <p><strong>Trả lời:</strong> <%# string.IsNullOrEmpty(Eval("TraLoi").ToString()) ? "Chưa có phản hồi" : Eval("TraLoi") %></p>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- Timer để reload -->
    <asp:Timer ID="Timer1" runat="server" Interval="80000" OnTick="Timer1_Tick" />
</asp:Content>
