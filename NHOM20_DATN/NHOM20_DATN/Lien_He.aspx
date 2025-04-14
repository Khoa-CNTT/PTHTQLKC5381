<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Lien_He.aspx.cs" Inherits="NHOM20_DATN.Lien_He" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <style>
           .containerr {
    font-family: Arial, sans-serif;
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
    position: relative;
    /* Màu nền chung cho toàn bộ container */
    box-shadow: 0px 0px 15px rgba(0, 0, 0, 0.1);
    /* Tạo bóng cho container */
    border-radius: 15px;
    /* Bo góc cho container */
}

.header {
    text-align: center;
    margin-bottom: 30px;
    background-color: white;
    /* Nền của faq-header */
    color: rgb(126, 116, 116);
    /* Màu chữ của header */
    padding: 20px;
    border-radius: 15px 15px 0 0;
    /* Bo góc trên cho header */
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
}

.header h1 {
    font-size: 2rem;
    margin: 0;
    color: #007BFF;
}

.header h2 {
    font-size: 1rem;
}
.tab-body {
    background-color: white; /* Đặt màu nền cho .tab-body */
    padding: 20px; /* Thêm padding nếu cần */
    box-sizing: border-box; /* Đảm bảo padding không ảnh hưởng đến kích thước */
    height: 38em;
    width: 1000px;
    margin-left: 90px;
    border-radius: 10px;
}
.tab{
    margin-bottom: 20px;
}
.tab p{
    font-size: 15px;
    color: rgb(206, 192, 192);
    
}
.tab-ta input {
    width: 400px;
    border-radius: 5px;
    border: 1px solid #ddd; 
    box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1); 
    font-size: 16px; 
    outline: none; 
    height: 35px; 
}
.tab-ta input:focus {
    border-color: #007BFF; 
    box-shadow: 0 0 5px rgba(0, 123, 255, 0.5); 
}
.tab-ta .text{
    height: 95px; 
}
.tabs {
    float: left;
    height: 100%;
    margin-right: 40px;
    margin-bottom: 20px;
}
.activee{
    font-weight: bold;
}
.active-1{
    margin-left: 20px;
    font-weight: bold;
}
.button-container {
    text-align: center;
    margin-top: 20px; 
}

.submit-btn {
    background-color: #007BFF; 
    color: white; 
    border: none; 
    border-radius: 5px; 
    padding: 5px 20px; 
    font-size: 16px; 
    cursor: pointer; 
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); 
    transition: background-color 0.3s ease;
}

.submit-btn:hover {
    background-color: #0056b3; 
}

.submit-btn:active {
    background-color: #00408a; 
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2); 
}
.suutap {
    display: flex;
    overflow: hidden; 
    width: 100%; 
}
.suutap {
    display: flex;
    overflow: hidden; 
    width: 100%; 
    position: relative;
}

.suutap_mot {
    position: relative;
    flex: 0 0 33.33%; 
    transition: transform 0.3s ease; 
    margin: 0 10px;
    margin-left: 20px;
    margin-top: 30px;
    opacity: 0.7;
}

.suutap_mot:hover {
    transform: scale(1.1); 
    z-index: 1; 
}

.suutap_mot img {
    width: 100%;
    border-radius: 10px; 
    height: 200px;
    display: block; 
    transition: opacity 0.3s ease;
}

.suutap_mot:hover img {
    opacity: 0.5; /* Làm sáng ảnh khi rê chuột */
}

.backgrou {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 0;
    transition: opacity 0.3s ease;
}

.suutap_mot:hover .backgrou {
    opacity: 0; /* Làm nền mờ đi khi rê chuột */
}

.suutap_hai {
    position: absolute;
    bottom: -100%; /* Ẩn mô tả bên dưới */
    left: 20px;
    color: white;
    text-shadow: 0 0 5px rgba(0, 0, 0, 0.5);
    transition: bottom 0.3s ease; /* Di chuyển mô tả từ dưới lên */
}

.suutap_mot:hover .suutap_hai {
    bottom: 20px; /* Hiển thị mô tả khi rê chuột */
}
.container-than h1{
    
    font-size: 20px;
    text-align: center;
 margin-left: 0; /* Reset margin */
 margin-top: 20px;
}
@media screen and (max-width: 1200px) {
    .containerr {
        padding: 15px;
    }

    .header h1 {
        font-size: 1.5rem;
    }

    .header h2 {
        font-size: 0.9rem;
    }

    .tab-body {
        width: auto;
        margin-left: 0;
        padding: 10px;
    }

    .tabs {
        display: block;
        width: 100%;
    }

    .tab {
        margin-bottom: 15px;
        width: 100%;
    }

    .tab-ta input {
        width: 100%;
    }

    .bando iframe {
        width: 100%; /* Đảm bảo bản đồ chiếm hết chiều rộng trên thiết bị nhỏ */
        height: 300px; /* Điều chỉnh chiều cao cho phù hợp */
    }

    .button-container {
        text-align: center;
    }

    .submit-btn {
        width: 100%;
        padding: 10px;
    }

    .suutap_mot {
        flex: 1 1 100%;
        margin-left: 0;
        margin-right: 0;
    }
}

/* For Mobile Phones */


.phone-fixed {
    position: fixed;
    right: 20px;
    top: 80%;
    transform: translateY(-50%);
    background-color: #007BFF;
    color: white;
    padding: 10px;
    border-radius: 50%;
    cursor: pointer;
    z-index: 1000;
    transition: all 0.3s ease;
}

.phone-fixed:hover .phone-background {
    right: 60px; 
    opacity: 1;
    visibility: visible;
}

.phone-icon {
    font-size: 20px;
    display: block;
}

.phone-background {
    position: absolute;
    right: 0;
    top: 50%;
    transform: translateY(-50%);
    background-color: rgba(0, 123, 255, 0.9);
    padding: 10px;
    border-radius: 5px;
    color: white;
    white-space: nowrap;
    opacity: 0;
    visibility: hidden;
    transition: all 0.3s ease;
}

.phone-background p {
    margin: 0;
    font-size: 20px;
    margin-bottom: 7px;
}
.red_text{
    color: red;
}

.tab-ta{
    margin-bottom: 15px;
}


input[type=number]::-webkit-inner-spin-button, 
input[type=number]::-webkit-outer-spin-button { 
  -webkit-appearance: none; 
  margin: 0; 
}
    </style>
            <div class="containerr">
    <div class="header">
        <h1>Liên hệ với chúng tôi BANANA</h1>
        <h2>Banana rất hân hạnh được hợp tác cùng với các cơ sở y tế và các quý bác sĩ để tiếp cận hàng triệu bệnh
            nhân qua nền tảng Banana. Đặc biệt, chúng tôi cung cấp chính sách chia sẻ doanh thu hấp dẫn dành cho các
            Cộng Tác Viên tham gia phát triển mạng lưới cơ sở y tế.</h2>
    </div>
    <div class="tab-body">
        <div class="tabs">
            <div class="tab activee">Thông tin chi tiết</div>
            <div class="tab active-1">Hỗ trợ đặt khám
                <p>
                    220 Phan Thanh - Thành phố Đà Nẵng
                </p>
            </div>
            <div class="tab active-1">BANANA - Đặt lịch khám
                <p>
                    Điện thoại: 1900-4567
                </p>
            </div>
              <div class="bando">
                <iframe
                    src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2855.431423551432!2d108.20749375056734!3d16.06028127260758!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x314219b416e135cd%3A0x401b57932be120d9!2zMjIwIFBoYW4gVGhhbmgsIFRo4bqhYyBHacOhbiwgVGhhbmggS2jDqiwgxJDDoCBO4bq1bmcgNTUwMDAwLCBWaeG7h3QgTmFt!5e0!3m2!1svi!2s!4v1713282438118!5m2!1svi!2s"
                    width="450" height="290" style="border:0;" allowfullscreen="" loading="lazy"
                    referrerpolicy="no-referrer-when-downgrade" ></iframe>
            </div>
        </div>
        <div class="tabs">
            <div class="tab ">Họ và Tên *
                <div class="tab-ta">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ControlToValidate="txtName" CssClass="red_text" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>

            </div>
            
            <div class="tab ">Email *
                <div class="tab-ta">
                   <asp:TextBox ID="txtMail" TextMode="Email" runat="server"></asp:TextBox>
                </div>
           
            <asp:RequiredFieldValidator ControlToValidate="txtName" CssClass="red_text" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>
                 </div>

            <div class="tab ">Số điện thoại *
                <div class="tab-ta">
                    <asp:TextBox ID="txtPhone" TextMode="Number" runat="server"></asp:TextBox>
                </div>
            
            <asp:RequiredFieldValidator ControlToValidate="txtPhone"  CssClass="red_text" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>
             </div>

            <div class="tab ">Ghi chú *
                <div class="tab-ta">
                    <asp:TextBox ID="txtDes" CssClass="text" runat="server"></asp:TextBox>
                </div>
           
           <asp:RequiredFieldValidator ControlToValidate="txtDes"  CssClass="red_text" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>
         </div>
            
            <div class="button-container">
                <asp:Button ID="btnSubmit" CssClass="submit-btn" runat="server" Text="Gửi" OnClick="btnSubmit_Click" />
            </div>
        </div>

    </div>
    <div class="container-than">
        <h1 style="font-family: Sans-serif; font-size: 40px; font-weight: bold; background: linear-gradient(to right, #00c6fb, #005bea); -webkit-background-clip: text; color: transparent;">
            Hệ thống các khoa ở BANANA
        </h1>
    </div>
    <div class="suutap">
        <div class="suutap_mot">
            <div class="backgrou">

            </div>
            <img src="./img/th.jpg" alt="">
            <div class="suutap_hai">
                <h2>Khoa Răng - Hàm - Mặt</h2>
                <p>Khoa Răng Hàm Mặt cung cấp các dịch vụ chẩn đoán và điều trị các bệnh lý về răng, hàm, mặt với sự
                    chú trọng vào sức khỏe và thẩm mỹ.</p>
            </div>
        </div>
        <div class="suutap_mot">
            <div class="backgrou">

            </div>
            <div class="IMG">
                <img src="./img/tj.jpg" alt="">
            </div>
            <div class="suutap_hai">
                <h2>Khoa Tai - Mũi - Họng</h2>
                <p>Khoa Tai Mũi Họng chuyên điều trị và chăm sóc các bệnh lý liên quan đến tai, mũi và họng, từ các
                    vấn đề đơn giản đến phức tạp.</p>
            </div>
        </div>
        <div class="suutap_mot">
            <div class="backgrou">

            </div>
            <img src="./img/110.jpg" alt="">
            <div class="suutap_hai">
                <h2>Khoa tim mạch</h2>
                <p>Khoa Tim Mạch chuyên chẩn đoán, điều trị và 
                    quản lý các bệnh lý liên quan đến tim.</p>
            </div>
        </div>
         <div class="phone-fixed">
        <span class="phone-icon"><i class='bx bx-phone'></i></span>
        <div class="phone-background">
            <p>Tiếng Việt : 123-456-7890</p>
            <p>English : 123-456-7890</p>
            <p>Korean : 123-456-7890</p>
            <p>Chinese : 123-456-7890</p>
        </div>
    </div>
    
    </div>
</div>
    <script>
         function showAlert(message, iconType) {
     Swal.fire({
         title: message,
         icon: iconType,
         confirmButtonText: 'OK'
     });
 }
    </script>
</asp:Content>
