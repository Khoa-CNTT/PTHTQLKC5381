<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage_TuVanVien.Master" AutoEventWireup="true" CodeBehind="Tu_Van_Suc_Khoe_Ban_Dau.aspx.cs" Inherits="NHOM20_DATN.Consultant.Tu_Van_Suc_Khoe_Ban_Dau" %>

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
                <h3 class="mb-4 fw-bold text-dark">
                    <i class="fas fa-comments text-primary me-2"></i>Danh sách câu hỏi cần phản hồi
                </h3>

                <asp:Repeater ID="rptCauHoi" runat="server">
                    <ItemTemplate>
                        <div class="card custom-card mb-4">
                            <div class="card-header fw-semibold d-flex justify-content-between align-items-center">
                                <span>🧑 Bệnh nhân <%# Eval("HoTen") %></span>
                                <small class="text-light fst-italic">
                                    <i class="far fa-clock me-1"></i><%# Eval("ThoiGian", "{0:dd/MM/yyyy HH:mm}") %>
                                </small>
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- Timer để reload -->
    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick" />
</asp:Content>
