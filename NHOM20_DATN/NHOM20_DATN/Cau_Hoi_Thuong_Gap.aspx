<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cau_Hoi_Thuong_Gap.aspx.cs" Inherits="NHOM20_DATN.Cau_Hoi_Thuong_Gap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
 <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                    <style>         
body {
    margin: 0; 
    padding: 0;
    width: 100%;
}

.faq-container {
    font-family: Arial, sans-serif;
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
    position: relative;
    background-color: #f7f9fc;
    box-shadow: 0px 0px 15px rgba(0, 0, 0, 0.1);
    border-radius: 15px;
}

.faq-header {
    text-align: center;
    margin-bottom: 30px;
    background-color: white;
    color: rgb(126, 116, 116);
    padding: 20px;
    border-radius: 15px 15px 0 0;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
}

.faq-header h1 {
    font-size: 2rem;
    margin: 0;
    color: #007BFF;
}

.faq-header h2 {
    font-size: 1rem;
}

.faq-tabs {
    display: flex;
    
    margin-bottom: 20px;
    background-color: white;
    padding: 10px;
    border-radius: 10px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
}

.tab {
    padding: 10px 15px;
    font-size: 16px;
    font-weight: bold;
    color: #007BFF;
    cursor: pointer;
    transition: all 0.3s ease;
    border-bottom: 2px solid transparent;
    background-color: #e6f7ff;
    border-radius: 20px;
    transform: scale(1); 
}

.tab:hover {
    color: #0c3b6d;
    transform: scale(1.1); 
    background-color: #cceeff;
}

.tab.active {
    color: white;
    background-color: #007BFF;
    border-bottom: 2px solid #007BFF;
}

.faq-content {
    display: flex;
    justify-content: space-between;
    background-color: #ffffff;
    padding: 20px;
    border-radius: 0 0 15px 15px;
    align-items: flex-start;
}

.faq-questions {
    width: 70%;
    padding-left: 40px;
    margin-right: 15px;
}
/*
.faq-categories {
    width: 30%;
    padding: 20px;
    background-color: #e3f2fd;
    border-radius: 10px;
    height: auto;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
    margin-right: 10px;
}

.category {
    font-size: 16px;
    color: #555;
    padding: 10px 0;
    font-weight: bold;
    cursor: pointer;
    transition: color 0.3s ease;
    margin-bottom: 15px;
    background-color: #e3f2fd;
    border-radius: 10px;
}*/
.faq-categories {
    width: 30%;
    padding: 20px;
    height: auto;
    border-radius: 10px;
    background-color: #e3f2fd;
    margin-right: 10px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
    display: flex;
    flex-direction: column;
    gap: 10px; 
}

.category {
    font-size: 16px;
    color: #555;
    padding: 10px 0; 
    font-weight: bold;
    cursor: pointer;
    transition: color 0.3s ease;
    text-align: left; 
    background-color: #e3f2fd;
    border-radius: 10px;
    margin-bottom: 15px;
}

/*.category:hover {
    color: #333; 
}*/
.acti {
    background-color: white;
    color: #007BFF;
    
}

.category:hover {
   /* color: #333;*/
    background-color: #b3e5fc;
}
.hieuung1 a:hover{
    color: #333;
}
.hieuung:hover {
    /*background-color: #007BFF;*/
    border-radius: 10px;
    transform: translateX(20px);
    transition: transform 0.3s ease, background-color 0.3s ease;
}

.question {
    font-size: 16px;
    color: #333;
    padding: 10px 0;
    background-color: #f0f0f0;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-bottom: 10px;
    border-radius: 10px;
}

.question:hover {
    background-color: #85e2f2;
    color: white;
    
}

.tab-content {
    display: none;
}

.tab-content.active {
    display: flex;
}

.backgrou {
    position: relative;
}

.backgrou img {
    border-radius: 16px;
    width: 100%;
    height: auto;
}

.container-iconn {
    display: flex;
    position: absolute;
    margin-top: -100px;
    justify-content: center;
    width: 100%;
}

.iconn {
    margin-left: 10px;
    opacity: 0.8;
    transition: all 0.3s ease;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    padding: 20px;
    border-radius: 10px;
    background-color: #007BFF;
    color: white;
    text-align: center;
}

.icon-2 {
    background-color: yellow;
}

.icon-3 {
    background-color: greenyellow;
}

.iconn:hover {
    opacity: 1;
    cursor: pointer;
    transform: translateY(-10px);
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.3);
}
/* Smartphones,  768px) */
@media (max-width: 767px) {
    .faq-tabs {
        flex-direction: column;
        text-align: center;
    }

    .tab {
        margin-bottom: 10px;
    }

    .faq-content {
        flex-direction: column;
    }

    .faq-questions, .faq-categories {
        width: 100%;
        margin-bottom: 20px;
    }

    .chat-box {
        width: 90%;
        bottom: 10px;
        right: 5%;
    }

   
    .iconn {
        margin-top:50px;
        margin-left: 5px;
        padding: 10px;
        font-size: 14px;
    }
}

/*  (Tablets, 768px) */
@media (min-width: 768px) {
    .faq-container {
        padding: 20px;
    }

    .faq-tabs {
        flex-direction: row;
    }

    .faq-content {
        flex-direction: row;
    }

    .faq-questions {
        width: 70%;
    }

    .faq-categories {
        width: 30%;
    }

    .chat-box {
        width: 30%;
        bottom: 20px;
        right: 20px;
    }
}

/* (Desktops, 1200px ) */
@media (min-width: 1200px) {
    .faq-container {
        padding: 40px;
    }

    .faq-header h1 {
        font-size: 2.5rem;
    }

    .faq-questions {
        width: 75%;
    }

    /*.faq-categories {
        width: 25%;
    }
*/
    .iconn {
        padding: 20px;
        font-size: 18px;
    }
}

.faq-tabs .hinhanh3{
    width:200px;
    height:50px;
    margin-left:120px;
}
.faq-tabs .hinhanh1{
    width:200px;
    height:50px;
   
}
.faq-tabs .hinhanh2{
    width:200px;
    height:50px;
   
}
.modall {
    display: none;
    position: fixed;
    z-index: 1000;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
}

.modall-content {
    background-color: white;
    margin: 15% auto;
    padding: 20px;
    border-radius: 10px;
    width: 80%;
    max-width: 600px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.3);
}

.header {
    font-size: 1.5rem;
    font-weight: bold;
    margin-bottom: 10px;
}

.modal-body {
    font-size: 1rem;
    color: #333;
    margin-bottom: 20px;
}

.close {
    background-color: #007BFF;
    color: white;
    border: none;
    padding: 10px 20px;
    cursor: pointer;
    border-radius: 5px;
    float: right;
}

.close:hover {
    background-color: #0056b3;
}
 .hinhbv {
            width: 80px;
            margin-left: 20px;
            margin-top: 20px;
        }
        .hinhbs{
            width: 70px;
            height: 70px;
            /*margin-top: 10px;
            margin-left: 200px;*/
        }
  .modall-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}
.iconn a{
    text-decoration:none;
}
.image-row {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-bottom: 20px;
}

.image-row img {
    max-width: 100px; /* Điều chỉnh kích thước hình ảnh */
    margin: 0 10px;   /* Khoảng cách giữa hai hình ảnh */
}

.hinhbv, .hinhbs {
    max-width: 100px;
    height: auto;
    border-radius: 10px; /* Nếu muốn thêm border radius */
}    
    </style>
                  <div class="faq-container">
        <div class="faq-header">
            <h1>BANANA CÓ THỂ HỖ TRỢ GÌ CHO BẠN</h1>
            <h2>Giải đáp các vấn đề mà khách hàng gặp của dịch vụ của chúng tôi</h2>
        </div>
        <div class="faq-tabs">
            <div class="tab active">Câu hỏi thường gặp</div>
            <img src="./img/cauhoi.jpeg" alt="" class="hinhanh3">
            <img src="./img/cauhoi.jpeg" alt="" class="hinhanh1">
            <img src="./img/cauhoi.jpeg" alt="" class="hinhanh2">
        </div>
        <div class="faq-content">
            <div id="app-settings" class="tab-content active">
                <div class="faq-categories">
                    <div class="category acti hieuung">Vấn đề chung</div>
                    <div class="category acti hieuung hieuung1"> <a href="Huong_Dan_Dat_Lich_Kham.aspx" style="text-decoration:none; color:#007BFF;">Về quy trình đặt khám</a></div>
                </div>
                <div class="faq-questions">
                    <div class="question " data-answer="Lợi ích của website là giúp bạn tiết kiệm thời gian,của khách hàng một cách tiết kiệm nhất không phải chờ hàng dài khi đến bệnh viện.">Lợi ích khi sử dụng ứng dụng đăng ký khám bệnh trực tuyến này là gì?</div>
                    <div class="question" data-answer="Bạn cần tạo tài khoản, đăng nhập và điền thông tin cần thiết về bệnh nhân, sau đó chọn lịch khám phù hợp.">Làm sao để sử dụng được website đăng ký khám bệnh trực tuyến?</div>
                    <div class="question" data-answer="Việc đăng ký là hoàn toàn miễn phí, nhưng chi phí khám bệnh sẽ tính theo bảng giá của bệnh viện.">Đăng ký khám bệnh online có mất phí không?</div>
                    <div class="question" data-answer="Có, bạn có thể đăng ký thay cho người thân bằng cách điền đầy đủ thông tin bệnh nhân cần khám.">Tôi có thể dùng website để đăng ký và lấy số thứ tự khám cho bệnh nhân khác không?</div>
                    <div class="question" data-answer="Ứng dụng hỗ trợ đăng ký khám bất kỳ thời gian nào, kể cả ngoài giờ hành chính.">website có hỗ trợ đăng ký khám 24/7 không?</div>
                </div>
            </div>
    </div>
                 <div id="question-modal" class="modall">
   <div class="modall-content">
    <div class="image-row">
        <div class="hinhanhbv1">
            <img src="./IMG/logochinh.png" alt="" class="hinhbv">
        </div>
       <%-- <div class="hinhanhbs1">
            <img src="./IMG/bga.png" alt="" class="hinhbs"> 
        </div>--%>
    </div>
    <div class="header"><img src="./IMG/bga.png" alt="" class="hinhbs"> BANANA XIN CHÀO QUÝ KHÁCH</div>
    <div class="modal-body" id="modal-text">Nội dung sẽ được hiển thị tại đây.</div>
    <button class="close" onclick="closeModal()">Đóng</button>
</div>
</div>
    <div class="backgrou">
        <img src="./img/breadcrumb-3.jpg" alt=""  >
        <div class="container-iconn">
            <div class="iconn icon1">
                <i class='bx bx-phone'></i> Gọi Tổng Đài
               </div>
             <div class="iconn icon-2">
    <a href="#" onclick="checkLogin('../Patient/Dang_Ky_Kham_Truc_Tiep.aspx')">
        <i class='bx bx-notepad'></i> Đặt lịch hẹn
    </a>
</div>

<div class="iconn icon-3">
    <a href="#" onclick="checkLogin('Tu_Van_Suc_Khoe_Truc_Tuyen')">
        <i class='bx bxs-user'></i> Tìm bác sĩ
    </a>
</div>
        </div>
     
    </div>

</div>
    <script>
        var questions = document.querySelectorAll('.question');
        questions.forEach(function (question) {
            question.addEventListener('click', function () {
                var answer = this.getAttribute('data-answer');
                document.getElementById('modal-text').textContent = answer;
                document.getElementById('question-modal').style.display = 'block';
            });
        });
        function closeModal() {
            document.getElementById('question-modal').style.display = 'none';
        }
        window.onclick = function (event) {
            var modal = document.getElementById('question-modal');
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        }
        function showAlert(message, iconType) {
            Swal.fire({
                title: message,
                icon: iconType,
                confirmButtonText: 'OK'
            });
        }
        function checkLogin(redirectUrl) {
            var isLoggedIn = '<%= Session["UserID"] != null ? "true" : "false" %>';

             if (isLoggedIn === "true") {
                 window.location.href = redirectUrl;
             } else {
                 showAlert('Vui lòng đăng nhập trước khi tiếp tục.', 'warning');
             }
         }
    </script>
</asp:Content>
