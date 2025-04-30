<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Huy_Tu_Van.aspx.cs" Inherits="NHOM20_DATN.Patient.Huy_Tu_Van" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <style>
        .form-container {
            background-color: white;
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
            max-width: 500px;
            width: 100%;
            text-align: center;
        }

        h3.text-primary {
            font-weight: bold;
            color: #007bff;
            margin-bottom: 30px;
            font-size: 24px;
            position: relative;
        }

            h3.text-primary::after {
                content: '';
                width: 50px;
                height: 3px;
                background-color: #007bff;
                display: block;
                margin: 10px auto;
                border-radius: 5px;
            }

        .header-icon {
            margin-right: 10px;
        }

        .form-group label {
            font-weight: bold;
            color: #555;
            text-align: left;
            display: block;
        }

        .form-control {
            border-radius: 50px;
            padding: 15px;
            font-size: 16px;
            border: 2px solid #ddd;
            transition: border-color 0.3s ease-in-out;
        }

            .form-control:focus {
                border-color: #007bff;
                box-shadow: none;
            }

        .btn-danger {
            background: linear-gradient(135deg, #f85032 0%, #e73827 100%);
            border: none;
            padding: 10px 25px;
            border-radius: 50px;
            color: white;
            font-weight: bold;
            transition: background 0.3s ease-in-out;
        }

            .btn-danger:hover {
                background: linear-gradient(135deg, #e73827 0%, #f85032 100%);
            }

        .text-danger {
            font-size: 14px;
            color: #e74c3c;
            margin-top: 10px;
        }

        .message {
            font-size: 16px;
            color: #28a745;
            font-weight: bold;
            margin-bottom: 15px;
        }

        .btn-danger:hover {
            box-shadow: 0 5px 15px rgba(255, 87, 34, 0.3);
        }

        .form-group {
            margin-bottom: 20px;
            text-align: left;
        }

        .note {
            background: #fff3cd;
            border-left: 4px solid #ffc107;
            padding: 15px;
            margin-top: 20px;
            border-radius: 10px;
            font-size: 14px;
            color: #856404;
        }

        .help {
            margin-top: 15px;
            font-size: 14px;
        }

            .help a {
                color: #ff5858;
                text-decoration: none;
                font-weight: 600;
            }

        .sub-text {
            color: #777;
            font-size: 14px;
            margin-bottom: 25px;
            font-weight: bold;
        }

        .cancel-header {
            font-size: 24px;
            font-weight: bold;
            color: #57C785;
            display: flex;
            align-items: center;
            gap: 10px; /* Khoảng cách giữa icon và text */
        }

        .cancel-icon {
            font-size: 28px;
            color: #f857a6; /* Màu sắc icon */
            transition: transform 0.3s ease-in-out;
        }

            .cancel-icon:hover {
                transform: rotate(20deg); /* Hiệu ứng xoay icon khi hover */
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <div class="container text-center">
            <div class="row">
                <div class="form-container col-xl-6">
                    <h3 class="cancel-header">
                        <img src="../img/icon_lich.png" style="width: 30px; height: 30px;" />Hủy Tư Vấn
                    </h3>
                    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger"></asp:Label>
                    <hr />
                    <p class="sub-text">Nhập mã tư vấn bạn muốn hủy.
                        <br />
                        Chúng tôi rất tiếc khi phải hủy lịch hẹn của bạn.</p>
                    <br />
                    <div class="form-group">
                        <label for="lblIDTuVan">Nhập mã ID tư vấn:</label><br />
                        <asp:TextBox ID="txtIDTuVan" runat="server" CssClass="form-control" placeholder="Nhập mã ID tư vấn"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtIDTuVan" Display="Dynamic" ErrorMessage="Bạn phải nhập mã ID tư vấn." runat="server" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group text-center">
                        <asp:Button ID="btnHuy" runat="server" Text="Hủy Tư Vấn" CssClass="btn btn-danger" OnClick="btnHuy_Click" />
                    </div>
                    <div class="note">
                        <strong>Lưu ý:</strong> Việc hủy tư vấn cần được thực hiện trước 24h kể từ thời điểm tư vấn. Nếu bạn đã quá hạn, vui lòng liên hệ tổng đài hỗ trợ.
                    </div>

                    <div class="help">
                        Cần trợ giúp? Gọi ngay <a href="tel:1900xxxx">1900-3456</a>
                    </div>
                </div>
                <div class="col-xl-6">
                    <img style="width: 125%" src="../img/camketbacsi.png" />
                </div>
            </div>
        </div>
        <div id="successMessage" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%,-50%); background-color: #d4edda; color: #155724; padding: 20px 40px; border-radius: 10px; box-shadow: 0 2px 8px rgba(0,0,0,0.2); z-index: 9999; font-size: 18px; text-align: center;">
            Hủy tư vấn thành công.
        </div>
    </div>
    <script>
        function showOverlay() {
            document.getElementById("loadingOverlay").style.display = "block"; // Hiển thị overlay
        }

        function hideOverlay() {
            document.getElementById("loadingOverlay").style.display = "none"; // Ẩn overlay khi hoàn thành
        }
    </script>
</asp:Content>
