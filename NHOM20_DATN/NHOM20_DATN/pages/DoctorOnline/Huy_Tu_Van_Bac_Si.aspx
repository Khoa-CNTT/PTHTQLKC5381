<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/MasterPage/DoctorOnline_MasterPage.Master" AutoEventWireup="true" CodeBehind="Huy_Tu_Van_Bac_Si.aspx.cs" Inherits="NHOM20_DATN.pages.DoctorOnline.Huy_Tu_Van_Bac_Si" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
    <style>
        .card-header h5 i, .card-header h4 i {
            margin-right: 8px;
        }

        .card-body ul {
            padding-left: 20px;
            line-height: 1.8;
        }

        .btn {
            transition: all 0.2s ease-in-out;
        }

            .btn:hover {
                transform: scale(1.03);
            }

        img.custom-img {
            max-width: 100%;
            border-radius: 12px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 mb-5">
        <div class="row">
            <!-- Lưu ý bên trái -->
            <div class="col-md-6 mb-4">
                <div class="card border-info shadow-sm">
                    <div class="card-header bg-info text-white">
                        <h5 class="mb-0"><i class="fas fa-exclamation-triangle"></i>Lưu ý khi hủy tư vấn</h5>
                    </div>
                    <div class="card-body">
                        <ul>
                            <li>Chỉ được phép hủy <strong>trước 10 tiếng</strong> so với thời gian tư vấn.</li>
                            <li>Bác sĩ cần ghi rõ <strong>lý do</strong> hủy để bệnh nhân nắm được tình hình.</li>
                            <li>Hệ thống sẽ tự động gửi <strong>email thông báo</strong> đến bệnh nhân.</li>
                            <li>Hủy quá nhiều lần không lý do có thể ảnh hưởng <strong>uy tín cá nhân</strong>.</li>
                        </ul>
                        <div class="text-center">
                            <img src="../../img/camketbacsi.png" alt="Lưu ý" class="custom-img mt-3" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Form hủy bên phải -->
            <div class="col-md-6">
                <div class="card shadow-lg rounded-3 border-0">
                    <div class="card-header bg-danger text-white">
                        <h4 class="mb-0"><i class="fas fa-times"></i> Hủy tư vấn</h4>
                    </div>
                    <div class="card-body">

                        <asp:Label ID="lblThongBao" runat="server" CssClass="alert alert-danger d-block" Visible="false"></asp:Label>

                        <div class="form-group">
                            <label for="txtIDTuVan">Nhập mã số tư vấn:</label>
                            <asp:TextBox ID="txtIDTuVan" runat="server" CssClass="form-control" />
                        </div>

                        <div class="text-center mt-3">
                            <asp:Button ID="btnTim" runat="server" Text="Tìm tư vấn" CssClass="btn btn-outline-primary px-4" OnClick="btnTim_Click" />
                        </div>

                        <asp:Panel ID="pnlThongTin" runat="server" Visible="false" CssClass="mt-4">
                            <asp:Label ID="lblThongTin" runat="server" CssClass="alert alert-info d-block" Font-Bold="true" />

                            <div class="form-group mt-3">
                                <label for="txtLyDo">Lý do hủy tư vấn:</label>
                                <asp:TextBox ID="txtLyDo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>

                            <div class="text-center mt-3">
                                <asp:Button ID="btnHuy" runat="server" Text="Xác nhận hủy" CssClass="btn btn-danger px-4" OnClick="btnHuy_Click" />
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
