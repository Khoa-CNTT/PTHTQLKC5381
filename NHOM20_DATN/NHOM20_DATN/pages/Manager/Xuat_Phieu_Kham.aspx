<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Managerment_MasterPage.Master" AutoEventWireup="true" CodeBehind="Xuat_Phieu_Kham.aspx.cs" Inherits="NHOM20_DATN.Xuat_Phieu_Kham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;600;700&family=Poppins:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        :root {
            --primary: #4361ee;
            --primary-light: #4895ef;
            --secondary: #3f37c9;
            --success: #4ad66d;
            --warning: #f8961e;
            --danger: #f72585;
            --light: #f8f9fa;
            --dark: #212529;
            --card-shadow: 0 10px 30px -15px rgba(0, 0, 0, 0.1);
        }
        body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f8f9fa;
        color: #333;
        line-height: 1.6;
       
    }
        .contain-xpk{
             background-color: #d9d9d96e;
        }
    
    /* Container chính */
  .phieu-kham-container {
        background: white;
        width: 650px;
        margin: 10px auto;
        font-size: 13px;
        line-height: 1;
        padding: 15px;
        border-radius: 8px;
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
        position: relative;
        overflow: hidden;
        display: block !important;
        visibility: hidden;
    }

    
    /* Hiệu ứng góc */
    .phieu-kham-container::before {
        content: '';
        position: absolute;
        top: 0;
        right: 0;
        width: 120px;
        height: 100px;
        background: linear-gradient(45deg, #4361ee, #4895ef);
        clip-path: polygon(0 0, 100% 0, 100% 100%);
        z-index: 1;
    }
    
    /* Header */
    .phieu-kham-header {
        text-align: center;
        margin-bottom: 30px;
        position: relative;
        z-index: 2;
    }
    
    .phieu-kham-header h2 {
        color: #4361ee;
        font-size: 28px;
        font-weight: 700;
        margin-bottom: 5px;
        letter-spacing: 1px;
    }
    
    .phieu-kham-header p {
        color: #6c757d;
        font-size: 16px;
    }
    
    /* Nút in */
    .btn-in {
        position: absolute;
        top: 20px;
        right: 20px;
        padding: 10px 20px;
        background: linear-gradient(to right, #4cc9f0, #4361ee);
        color: white;
        border: none;
        border-radius: 50px;
        font-weight: 600;
        cursor: pointer;
        box-shadow: 0 4px 15px rgba(67, 97, 238, 0.3);
        transition: all 0.3s ease;
        z-index: 3;
    }
    
    .btn-in:hover {
        transform: translateY(-2px);
        box-shadow: 0 6px 20px rgba(67, 97, 238, 0.4);
    }
    
    /* Bảng thông tin */
    .info-table {
        width: 100%;
        border-collapse: separate;
        border-spacing: 0;
        margin: 25px 0;
    }
    
    .info-table th {
        background-color: #f8f9fa;
        padding: 15px;
        text-align: left;
        font-weight: 600;
        color: #495057;
        border-bottom: 2px solid #e9ecef;
        width: 30%;
    }
    
    .info-table td {
        padding: 15px;
        border-bottom: 1px solid #e9ecef;
        color: #212529;
    }
    
    .info-table tr:hover td {
        background-color: #f8faff;
    }
    
    /* Footer */
    .phieu-kham-footer {
        margin-top: 10px;
        text-align: right;
        padding-top: 1px;
        margin-bottom:20px;
    }
    
    .phieu-kham-footer p {
        margin-bottom: 10px;
    }

  @media (max-width: 768px) {
    .phieu-kham-container {
        width: 95% !important;
        margin: 10px auto !important;
        padding: 15px !important;
        font-size: 12px !important;
    }

    /* Ẩn hiệu ứng góc trên mobile */
    .phieu-kham-container::before {
        display: none !important;
    }

    /* Điều chỉnh header */
    .benh-vien-header h1 {
        font-size: 16px !important;
        margin: 5px 0 !important;
    }

    .benh-vien-header p {
        font-size: 11px !important;
        margin: 2px 0 !important;
    }

    /* Căn chỉnh nút in */
    .btn-in {
        position: relative !important;
        top: auto !important;
        right: auto !important;
        margin: 10px auto !important;
        display: block;
        width: 120px;
        padding: 8px 15px;
    }

    /* Chuyển bảng thành dạng block */
    .info-table th,
    .info-table td {
        display: block;
        width: 100% !important;
        padding: 8px !important;
        text-align: left !important;
    }

    .info-table th {
        background-color: #f8f9fa !important;
        border-bottom: none !important;
        padding-bottom: 2px !important;
    }

    .info-table tr {
        margin-bottom: 10px;
        border-bottom: 1px solid #ddd;
    }

    /* Footer responsive */
    .phieu-kham-footer {
        flex-direction: column;
        text-align: left !important;
        margin-top: 20px !important;
    }

    .benhnhan {
        position: static !important;
        margin: 10px 0 !important;
        text-align: center;
    }

    .bottomphai {
        margin-right: 0 !important;
        text-align: center !important;
    }
}

/* Tablet: 769px - 1024px */
@media (min-width: 769px) and (max-width: 1024px) {
    .phieu-kham-container {
        width: 80% !important;
        font-size: 13px !important;
    }

    .phieu-kham-header h2 {
        font-size: 22px !important;
    }
}
    /* Animation */
    @keyframes fadeIn {
        from { opacity: 0; transform: translateY(20px); }
        to { opacity: 1; transform: translateY(0); }
    }
    
    .phieu-kham-container {
        animation: fadeIn 0.6s ease-out;
    }
    
    /* Ẩn khi in */
    @media print {
        .no-print {
            display: none;
        }
        
        body {
            padding: 0;
            margin: 0;
            background: white;
        }
        
        .phieu-kham-container {
            box-shadow: none;
            border: none;
            padding: 0;
        }
        
        .phieu-kham-container::before {
            display: none;
        }
    }
        #content {
            font-family: 'Poppins', sans-serif;
            background-color: #f5f7fa;
        }
        
        .page-header {
            text-align: center;
            margin: 2rem 0;
            animation: fadeInDown 0.8s ease-out;
        }
        
        .page-header h1 {
            font-family: 'Montserrat', sans-serif;
            font-weight: 700;
            color: var(--primary);
            font-size: 2.2rem;
            margin-bottom: 0.5rem;
        }
        
        .page-header p {
            color: #6c757d;
            font-size: 1.1rem;
        }
        
        .search-container {
            background: white;
            padding: 2rem;
            border-radius: 12px;
            box-shadow: var(--card-shadow);
            margin-bottom: 2rem;
            animation: fadeInUp 0.8s ease-out;
            max-width: 800px;
            margin: 0 auto 2rem;
        }
        
        .search-box {
            display: flex;
            gap: 10px;
            justify-content: center;
            align-items: center;
        }
        
        .search-box input[type="text"] {
            flex: 1;
            max-width: 500px;
            padding: 12px 20px;
            border: 2px solid #e9ecef;
            border-radius: 8px;
            font-size: 1rem;
            transition: all 0.3s ease;
            box-shadow: 0 2px 5px rgba(0,0,0,0.05);
        }
        
        .search-box input[type="text"]:focus {
            border-color: var(--primary-light);
            box-shadow: 0 0 0 3px rgba(67, 97, 238, 0.2);
            outline: none;
        }
        
        .search-button {
            padding: 12px 24px;
            background-color: var(--primary);
            color: white;
            border: none;
            border-radius: 8px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        
        .search-button:hover {
            background-color: var(--secondary);
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0,0,0,0.15);
        }
        
        .modern-gridview {
            width: 80%;
            border-collapse: separate;
            border-spacing: 0;
            background: white;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: var(--card-shadow);
            animation: fadeInUp 0.8s ease-out;
            margin-bottom: 2rem;
            margin-left:100px;
        }
        
        .modern-gridview th {
            background: var(--primary) !important;
            color: white;
            padding: 16px 12px;
            font-weight: 600;
            text-transform: uppercase;
            font-size: 0.85rem;
            letter-spacing: 0.5px;
            border: none;
            font-family: 'Montserrat', sans-serif;
        }
        
        .modern-gridview td {
            padding: 14px 12px;
            border-bottom: 1px solid #f1f3f5;
            transition: all 0.3s ease;
            color: #495057;
        }
        
        .modern-gridview tr:nth-child(even) {
            background-color: #f8fafb;
        }
        
        .modern-gridview tr:hover td {
            background-color: #f1f5ff;
            color: var(--primary);
        }
        
        .modern-gridview tr:last-child td {
            border-bottom: none;
        }
        
        .btn-chon {
            padding: 8px 16px;
            background-color: var(--success);
            color: white;
            border: none;
            border-radius: 6px;
            font-weight: 500;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            min-width: 80px;
        }
        
        .btn-chon:hover {
            background-color: #3ac45d;
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0,0,0,0.15);
        }
        
        /* Animations */
        @keyframes fadeInDown {
            from { opacity: 0; transform: translateY(-20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            .search-box {
                flex-direction: column;
            }
            
            .search-box input[type="text"] {
                width: 100%;
                max-width: 100%;
            }
            
            .page-header h1 {
                font-size: 1.8rem;
            }
            
            .modern-gridview th, 
            .modern-gridview td {
                padding: 12px 8px;
                font-size: 0.9rem;
            }
        }
        .benhnhan{
            position:absolute;
            margin-right:100px;
            margin-left:40px;
            margin-top:30px;
            font-weight:bold;
        }
        .benh-vien-header {
    text-align: center;
    margin-bottom: 20px;
    
}

.benh-vien-header h1 {
        font-size: 18px;
        margin-bottom: 3px;
        color: #4361ee;
        text-align: center;
    }

 .benh-vien-header p {
        font-size: 13px;
        margin: 2px 0;
        text-align: center;
    }

.separator-line {
    border-top: 1px solid #000;
    margin: 10px 0;
}

.phieu-kham-header h2 {
    text-align: center;
    text-transform: uppercase;
    margin-top: 10px;
    
}
@media print {
  
  body * {
    visibility: hidden !important;
  }

  
  #pnlPhieuKham,
  #pnlPhieuKham * {
    visibility: visible !important;
    box-shadow: none !important;
  }

  
  #pnlPhieuKham {
    position: absolute !important;
    top: 0 !important;
    left: 50% !important;
    transform: translateX(-50%) !important;
    width: 650px;          
    max-width: 100% !important; 
    padding: 15px 0 !important;
    background: white !important;
  }

 
  .no-print {
    display: none !important;
  }

   .phieu-kham-container::before {
        display: block !important; /* Hiển thị khi in */
        background: linear-gradient(45deg, #4361ee, #4895ef) !important;
        width: 120px !important;
        height: 100px !important;
        clip-path: polygon(0 0, 100% 0, 100% 100%) !important;
        z-index: 9999 !important;
    }

    /* Đảm bảo không bị che bởi nội dung */
    .phieu-kham-header,
    .benh-vien-header {
        position: relative;
        z-index: 99999 !important;
    }
  @page {
    size: A4 portrait;
    margin: 10mm;
  }
}
        
        .bottomphai{
            margin-right:20px;
        }

        .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 1000;
    display: none;
}
   .phieu-kham-footer {
        margin-top: 1px;
        padding-top: 1px;
       
    }

/* Hiệu ứng modal */
.phieu-kham-container.modal-mode {
    visibility: visible; /* Hiển thị khi ở chế độ modal */
    position: fixed;
    top: 50%;
    left: 50%;  
    transform: translate(-50%, -50%);
    z-index: 1001;
    max-height:100%;
    
    box-shadow: 0 5px 30px rgba(0, 0, 0, 0.3);
    animation: modalFadeIn 0.3s ease-out;
}

@keyframes modalFadeIn {
    from {
        opacity: 0;
        transform: translate(-50%, -60%);
    }
    to {
        opacity: 1;
        transform: translate(-50%, -50%);
    }
}

/* Nút đóng modal */
.close-modal {
    position: absolute;
    top: 15px;
    right: 15px;
    background: var(--danger);
    color: white;
    width: 30px;
    height: 30px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    font-weight: bold;
    z-index: 2;
}.phieu-kham-container:not(.modal-mode) {
    display: none !important;
}
   .control {
     
      padding: 13px 16px;
      margin-top: 6px;
      border: 2px solid #e9ecef;
      border-radius: var(--border-radius);
      font-size: 14px;
      background-color: white;
      transition: var(--transition);
      box-shadow: 0 1px 3px rgba(0, 0, 0, 0.05);
      border-radius:8px;
      margin-bottom:7px;    
  }

  .control:focus {
      border-color: var(--primary-color);
      box-shadow: 0 0 0 3px rgba(67, 97, 238, 0.2);
      outline: none;
      transform: translateY(-2px);
  }

  .control:hover {
      border-color: var(--primary-light);
  }
  .mmm{
      margin-bottom:100px;
  }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>📋 Xuất Phiếu Khám Bệnh</h1>
        <p>Tìm kiếm và chọn bệnh nhân để xuất phiếu khám bệnh</p>
    </div>
    
    <div class="search-container">
        <div class="search-box">
            <asp:TextBox ID="txtTenBenhNhan" runat="server" Placeholder="Nhập tên bệnh nhân cần tìm..." CssClass="form-control" />
              <asp:TextBox ID="txtNgayKham" CssClass="control" runat="server" TextMode="Date"></asp:TextBox>
            <asp:Button ID="btnTim" runat="server" Text="Tìm kiếm" CssClass="search-button" OnClick="btnTim_Click" />
        </div>
    </div>

    <asp:GridView ID="gvBenhNhan" runat="server" AutoGenerateColumns="False"
        CssClass="modern-gridview"
        GridLines="None" 
        OnRowCommand="gvBenhNhan_RowCommand" 
        AllowPaging="false" 
        AllowSorting="false">
        <Columns>
            <asp:BoundField DataField="HoTen" HeaderText="Họ tên" />
            <asp:BoundField DataField="NgayKham" HeaderText="Ngày Khám" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="GioiTinh" HeaderText="Giới tính" />
            <asp:TemplateField HeaderText="Thao tác">
                <ItemTemplate>
                    <asp:Button ID="btnChon" runat="server" 
                        CommandName="ChonBenhNhan" 
                        CommandArgument='<%# Eval("IDPhieu") %>' 
                        Text="Chọn" 
                        CssClass="btn-chon" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div style="text-align: center; padding: 20px; color: #6c757d;">
                <i class="fas fa-info-circle" style="font-size: 24px; margin-bottom: 10px;"></i>
                <p>Không tìm thấy bệnh nhân nào phù hợp</p>
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
    <div class="modal-overlay" id="modalOverlay"></div>
     <asp:Panel ID="pnlPhieuKham" runat="server"  ClientIDMode="Static" Visible="false" CssClass="phieu-kham-container">
         <div class="close-modal no-print" onclick="closeModal()">×</div>

         <div class="benh-vien-header">
        <h1>BỆNH VIỆN BANANA</h1>
        <p>Địa chỉ: 220 Phan Thanh - Thành phố Đà Nẵng</p>
        <p>Điện thoại: 1900 3456 - 1900 4567</p>
        <p>Email: bananahospitaldanang@gmail.com</p>
        <div class="separator-line"></div>
    </div>
        <div class="phieu-kham-header">
            <button class="btn-in no-print" onclick="window.print()">In phiếu</button>
            <h2>PHIẾU KHÁM BỆNH</h2>
            <p>Mã phiếu: <asp:Label ID="lblMaPhieu" runat="server"></asp:Label></p>
        </div>
        
        <div class="phieu-kham-content">
            <table class="info-table">
                <tr>
                    <th>Họ tên bệnh nhân:</th>
                    <td><asp:Label ID="lblHoTen" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Ngày sinh:</th>
                    <td><asp:Label ID="lblNgaySinh" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Tuổi:</th>
                    <td><asp:Label ID="lblTuoi" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Giới tính:</th>
                    <td><asp:Label ID="lblGioiTinh" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Số điện thoại:</th>
                    <td><asp:Label ID="lblSoDienThoai" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Địa chỉ:</th>
                    <td><asp:Label ID="lblDiaChi" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Ngày khám:</th>
                    <td><asp:Label ID="lblNgayKham" runat="server"></asp:Label></td>
                </tr>
              
                <tr>
                    <th>Triệu chứng:</th>
                    <td><asp:Label ID="lblTrieuChung" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Bác sĩ khám:</th>
                    <td><asp:Label ID="lblBacSi" runat="server"></asp:Label></td>
                </tr>
              
            </table>
        </div>
        
        <div class="phieu-kham-footer">
            <p class="benhnhan">Bệnh nhân</p>
            <div class="bottomphai">
                  <p>Đà Nẵng, ngày <asp:Label ID="lblNgay" runat="server"></asp:Label> tháng <asp:Label ID="lblThang" runat="server"></asp:Label> năm <asp:Label ID="lblNam" runat="server"></asp:Label></p>
  <p><strong>Bác sĩ khám bệnh</strong></p>
  
            </div>
          
        </div>
    </asp:Panel>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
   <script>
       function showModal() {
           var overlay = document.getElementById('modalOverlay');
           var modal = document.getElementById('pnlPhieuKham');

           overlay.style.display = 'block';
           modal.classList.add('modal-mode');
           modal.style.visibility = 'visible'; // Thêm dòng này
           document.body.style.overflow = 'hidden';
       }

       function closeModal() {
           document.getElementById('modalOverlay').style.display = 'none';
           document.getElementById('pnlPhieuKham').classList.remove('modal-mode');
           document.body.style.overflow = ''; // Cho phép scroll lại
       }

       // Đóng modal khi click ra ngoài
       document.getElementById('modalOverlay').addEventListener('click', closeModal);

   </script>
</asp:Content>