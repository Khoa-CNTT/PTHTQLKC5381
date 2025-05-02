<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Xem_Lich_Su_Kham.aspx.cs" Inherits="NHOM20_DATN.Xem_Lich_Su_Kham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;600;700&family=Poppins:wght@300;400;500&display=swap" rel="stylesheet" />
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <style>
        :root {
            --primary: #4361ee;
            --primary-light: #4895ef;
            --secondary: #3f37c9;
            --accent: #4cc9f0;
            --success: #4ad66d;
            --warning: #f8961e;
            --danger: #f72585;
            --light: #f8f9fa;
            --dark: #212529;
            --bg-gradient: linear-gradient(135deg, #4361ee 0%, #3a0ca3 100%);
            --card-shadow: 0 10px 30px -15px rgba(0, 0, 0, 0.2);
        }
        
        body {
            font-family: 'Poppins', sans-serif;
            background-color: #f5f7fa;
           
        }
        
        .health-header {
            background: var(--bg-gradient);
            color: white;
            padding: 2rem 1rem;
            border-radius: 12px;
            margin-bottom: 2rem;
            position: relative;
            overflow: hidden;
            box-shadow: var(--card-shadow);
            animation: fadeInDown 0.8s ease-out;
        }
        
        .health-header::before {
            content: "";
            position: absolute;
            top: -50%;
            left: -50%;
            width: 200%;
            height: 200%;
            background: radial-gradient(circle, rgba(255,255,255,0.1) 0%, rgba(255,255,255,0) 70%);
            animation: pulse 15s infinite linear;
        }
        
        .health-header h2 {
            font-family: 'Montserrat', sans-serif;
            font-weight: 700;
            font-size: 2.2rem;
            margin-bottom: 0.5rem;
            position: relative;
            text-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        
        .health-header p {
            font-size: 1.1rem;
            max-width: 700px;
            margin: 0 auto;
            position: relative;
            opacity: 0.9;
        }
        
        .health-tip {
            background: white;
            border-left: 4px solid var(--success);
            padding: 1rem;
            border-radius: 8px;
            margin-bottom: 1.5rem;
            box-shadow: 0 4px 6px rgba(0,0,0,0.05);
            animation: slideInLeft 0.8s ease-out;
            position: relative;
            overflow: hidden;
        }
        
        .health-tip::after {
            content: "💡";
            position: absolute;
            right: 15px;
            top: 15px;
            font-size: 1.5rem;
            opacity: 0.1;
        }
        
        .health-tip h4 {
            color: var(--primary);
            margin-top: 0;
            margin-bottom: 0.5rem;
            font-weight: 600;
        }
        
        .table-wrapper {
            background: white;
            border-radius: 12px;
            padding: 1.5rem;
            box-shadow: var(--card-shadow);
            margin-bottom: 2rem;
            animation: fadeInUp 0.8s ease-out;
        }
        
        .table-lichsu {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            font-family: 'Poppins', sans-serif;
        }
        
        .table-lichsu th {
            background: var(--primary) !important;
            color: white;
            padding: 12px 15px;
            font-weight: 600;
            text-transform: uppercase;
            font-size: 0.85rem;
            letter-spacing: 0.5px;
            position: sticky;
            top: 0;
        }
        
        .table-lichsu td {
            padding: 12px 15px;
            border-bottom: 1px solid #eee;
            transition: all 0.3s ease;
        }
        
        .table-lichsu tr:nth-child(even) {
            background-color: #f8f9fa;
        }
        
        .table-lichsu tr:hover td {
            background-color: var(--primary-light) !important;
            color: white;
            transform: translateX(5px);
        }
        
        .table-lichsu tr:last-child td {
            border-bottom: none;
        }
        
        /* Modal styles */
        .modal {
            display: none;
            position: fixed;
            z-index: 2000;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: hidden;
            background-color: rgba(0,0,0,0.5);
            animation: fadeIn 0.3s ease-out;
        }
        
        .modal-content {
            background: white;
            margin: 5% auto;
            padding: 30px;
            border-radius: 12px;
            max-width: 600px;
            box-shadow: 0 10px 50px rgba(0,0,0,0.3);
            position: relative;
            animation: slideInDown 0.4s ease-out;
        }
        
        .close {
            position: absolute;
            right: 25px;
            top: 25px;
            font-size: 1.5rem;
            color: #aaa;
            transition: all 0.3s;
        }
        
        .close:hover {
            color: var(--danger);
            transform: rotate(90deg);
        }
        
        .info-group {
            margin-bottom: 1rem;
        }
        
        .info-label {
            font-weight: 600;
            color: var(--primary);
            margin-bottom: 0.3rem;
            display: flex;
            align-items: center;
        }
        
        .info-label:before {
            content: "▸";
            margin-right: 8px;
            color: var(--accent);
        }
        
        .info-value {
            background: #f8f9fa;
            padding: 12px;
            border-radius: 6px;
            border-left: 3px solid var(--accent);
        }
        
        /* Animations */
        @keyframes fadeInDown {
            from { opacity: 0; transform: translateY(-30px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        @keyframes slideInLeft {
            from { opacity: 0; transform: translateX(-30px); }
            to { opacity: 1; transform: translateX(0); }
        }
        
        @keyframes slideInDown {
            from { opacity: 0; transform: translateY(-50px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        @keyframes pulse {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        
        @keyframes fadeIn {
            from { opacity: 0; }
            to { opacity: 1; }
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            .health-header h2 {
                font-size: 1.8rem;
            }
            
            .health-header p {
                font-size: 1rem;
            }
            
            .table-wrapper {
                padding: 1rem;
            }
            
            .table-lichsu th, 
            .table-lichsu td {
                padding: 8px 10px;
                font-size: 0.85rem;
            }
            
            .modal-content {
                width: 90%;
                padding: 20px;
            }
        }
       #detailModal{
           z-index:9999;
       }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="health-header text-center">
        <h2>📋 Lịch Sử Khám Bệnh</h2>
        <p>Theo dõi lịch sử khám bệnh giúp bạn và bác sĩ có cái nhìn tổng quan về tình trạng sức khỏe của bạn theo thời gian</p>
    </div>
    
    <div class="health-tip">
        <h4>Mẹo sức khỏe hôm nay</h4>
        <p id="dailyHealthTip">Uống đủ nước mỗi ngày giúp cơ thể loại bỏ độc tố và duy trì chức năng của các cơ quan quan trọng.</p>
    </div>
    
    <div class="table-wrapper">
      <asp:GridView 
    ID="gvLichSuKham" runat="server"
    AutoGenerateColumns="false"
    CssClass="table-lichsu"
    BorderStyle="None"
    GridLines="None"
    OnRowDataBound="gvLichSuKham_RowDataBound">
    <Columns>
       
        <asp:BoundField DataField="HoTenBenhNhan" HeaderText="Bệnh nhân" />
        <asp:BoundField DataField="HoTenBacSi" HeaderText="Bác sĩ" />
        <asp:BoundField DataField="ChanDoan" HeaderText="Chẩn đoán" />
         <asp:BoundField DataField="NgayCapNhat" HeaderText="Ngày khám" 
                 DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
        <asp:BoundField DataField="HuongDieuTri" HeaderText="Hướng điều trị" />
        <asp:BoundField DataField="ChanDoanHoSo" Visible="false" />
        <asp:BoundField DataField="DonThuoc" Visible="false" />
    </Columns>
</asp:GridView>
    </div>
  
    <!-- Modal -->
    <div id="detailModal" class="modal">
        <div class="modal-content">
            <span id="closeModal" class="close">&times;</span>
            <h3 style="color: var(--primary); margin-top: 0;">🔍 Chi tiết lịch sử khám</h3>
            <div id="modalBody"></div>
        </div>
    </div>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            // Health tips rotation
            const healthTips = [
                "Ngủ đủ 7-8 tiếng mỗi đêm giúp cơ thể phục hồi và tăng cường hệ miễn dịch.",
                "30 phút vận động mỗi ngày giúp cải thiện sức khỏe tim mạch và tinh thần.",
                "Ăn nhiều rau xanh và trái cây cung cấp vitamin và chất xơ cần thiết cho cơ thể.",
                "Hít thở sâu và thiền định giúp giảm căng thẳng và cải thiện sức khỏe tinh thần.",
                "Khám sức khỏe định kỳ 6 tháng/lần giúp phát hiện sớm các vấn đề sức khỏe."
            ];

            // Set random health tip
            document.getElementById('dailyHealthTip').textContent =
                healthTips[Math.floor(Math.random() * healthTips.length)];

            // Modal handling
            var modal = document.getElementById('detailModal');
            var closeBtn = document.getElementById('closeModal');

            closeBtn.onclick = function () {
                modal.style.display = 'none';
            };

            window.onclick = function (e) {
                if (e.target === modal) modal.style.display = 'none';
            };
        });

        function showDetail(id, phieu, ngay, chanDoan, huong, donThuoc) {
            var body = document.getElementById('modalBody');
            body.innerHTML =
                `
        <div class="info-group">
            <span class="info-label">Bệnh nhân</span>
            <div class="info-value">${id}</div>
        </div>
        <div class="info-group">
            <span class="info-label">Bác sĩ</span>
            <div class="info-value">${phieu}</div>
        </div>
        <div class="info-group">
            <span class="info-label">Chẩn đoán</span>
            <div class="info-value">${chanDoan}</div>
        </div>
        <div class="info-group">
            <span class="info-label">Ngày khám</span>
            <div class="info-value">${ngay}</div>
        </div>
        <div class="info-group">
            <span class="info-label">Hướng điều trị</span>
            <div class="info-value">${huong}</div>
        </div>
        <div class="info-group">
            <span class="info-label">Đơn thuốc</span>
            <div class="info-value">${donThuoc}</div>
        </div>`;

            document.getElementById('detailModal').style.display = 'block';
        }
    </script>
</asp:Content>