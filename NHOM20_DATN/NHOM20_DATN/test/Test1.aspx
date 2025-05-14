<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test1.aspx.cs" Inherits="NHOM20_DATN.test.Test1" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <style>
        textarea { width: 100%; height: 100px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="max-width: 600px; margin: auto;">
            <h2>Nhập chẩn đoán</h2>
            <asp:TextBox ID="txtChanDoan" runat="server" TextMode="MultiLine" Rows="4" Width="100%" />

            <br /><br />
            <asp:Button ID="btnGoiY" runat="server" Text="Gợi ý đơn thuốc" OnClick="btnGoiY_Click" />

            <br /><br />
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red" />
            
            <h3>Đơn thuốc được gợi ý:</h3>
            <asp:TextBox ID="txtDonThuoc" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="6" Width="100%" />
        </div>
    </form>
</body>
</html>
