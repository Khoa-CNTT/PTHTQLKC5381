<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Huong_Dan_Quy_Trinh_Di_Kham.aspx.cs" Inherits="NHOM20_DATN.Huong_Dan_Quy_Trinh_Di_Kham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .anhnen1 img {
            width: 100%;
            height: 200px;
            filter: brightness(60%);
        }

        .anhnen1 {
            position: relative;
        }

            .anhnen1 h2 {
                font-family: "Times New Roman", Times, serif;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                position: absolute;
                text-align: center;
                width: 100%;
                color: rgb(233, 230, 230);
                text-shadow: 4px 4px 8px rgba(0, 0, 0, 1);
            }

        .trangchu1 {
            display: flex;
            padding-top: 20px;
            margin-left: 5%;
        }

            .trangchu1 a {
                text-decoration: none;
                color: black;
            }

            .trangchu1 h3 {
                font-size: 18px;
            }

            .trangchu1 .mau1 {
                color: dodgerblue;
            }

            .trangchu1 i {
                padding: 0 10px;
                font-size: 20px;
                margin-top: 2px;
            }

        .gach1 {
            border: 1px solid rgb(231, 231, 231);
            width: 100%;
        }

        .quy1 h3 {
            margin-top: 30px;
            margin-bottom: 30px;
            text-align: center;
            font-size: 30px;
            font-weight: bold;
            background: linear-gradient(to right, rgb(0, 235, 12), rgb(0, 229, 254), blue);
            -webkit-background-clip: text;
            color: transparent;
        }


        .batdau {
            position: relative;
            margin-left: 70px;
            padding-left: 20px;
            margin-top: 20px;
            border-left: 2px solid #e0e0e0;
        }

        .buoc {
            position: relative;
            margin-bottom: 20px;
        }

        .vongtron {
            width: 16px;
            height: 16px;
            background-color: transparent;
            border-radius: 50%;
            position: absolute;
            left: -29px;
            top: 0px;
        }

            .vongtron::before {
                content: "\2713";
                font-size: 30px;
                color: dodgerblue;
                position: absolute;
                left: 0;
                top: 0;
            }

        .nhan {
            margin-left: 60px;
            font-size: 28px;
            color: dodgerblue;
        }

        .batdau ul li {
            margin-left: 80px;
            list-style: none;
            font-size: 18px;
            cursor: default;
            font-weight: 350;
        }

        .buoc p {
            margin-left: 90px;
            color: rgb(176, 3, 0);
        }

        .batdau ul li i {
            font-size: 16px;
            transform: scaleX(0.9);
        }

        .batdau ul li:hover {
            list-style: none;
            font-size: 18px;
            color: dodgerblue;
        }

        .batdau {
            width: 80%;
        }

            .batdau hr {
                width: 80%;
                margin-left: 80px;
                margin-top: 20px;
                margin-bottom: 20px;
            }

        #hotline {
            position: fixed;
            bottom: 20px;
            right: 20px;
            background-color: dodgerblue;
            color: white;
            padding: 10px 20px;
            border-radius: 50px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
            transition: opacity 0.5s;
            font-weight: bolder;
            border-top: 4px solid rgb(30, 129, 228);
            opacity: 0;
            visibility: hidden;
            z-index: 2000;
            bottom: 100px;
        }

            #hotline h2 {
                font-size: 16px;
                text-align: center;
            }

            #hotline a {
                text-decoration: none;
                color: rgb(255, 255, 255);
            }

                #hotline a:hover {
                    text-decoration: none;
                    color: rgb(6, 255, 6);
                }

        .hotline-hidden {
            opacity: 0;
            visibility: hidden;
        }

        .hotline-visible h2 {
            font-size: 14px;
            text-align: center;
            font-weight: bolder;
            margin-bottom: -15px;
        }

        .hotline-visible i.bx-phone-call {
            font-size: 18px;
            margin-top: 15px;
        }

        .hotline-visible {
            opacity: 0;
            visibility: hidden;
        }

        @keyframes shake {
            0% {
                transform: translateX(0);
            }

            25% {
                transform: translateX(-5px);
            }

            50% {
                transform: translateX(5px);
            }

            75% {
                transform: translateX(-5px);
            }

            100% {
                transform: translateX(0);
            }
        }

        #hotline {
            animation: shake 0.5s infinite;
        }

            #hotline.moved-left {
                right: 0;
                transform: translateX(0);
            }

            #hotline.moved-right {
                right: 20px;
                transform: translateX(0);
            }

        .dau .gach {
            border-bottom: 1px solid rgb(196, 196, 196);
            font-size: 30px;
            padding: 10px 10px;
            color: dodgerblue;
        }

        @media (min-width: 1201px) {


            .anhnen1 h2 {
                font-size: 24px;
            }

            .trangchu1 h3 {
                font-size: 16px;
            }
        }

        /* Quy tắc cho màn hình trung bình */
        @media (max-width: 1200px) and (min-width: 993px) {

            .anhnen1 {
                padding: 10px;
            }

            .dau .anh {
                margin-left: -65px;
            }

                .dau .anh img {
                    width: 750px;
                    height: 550px;
                }

            .trangchu1 .mau1 {
                margin-left: 25px;
                margin-top: 10px;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .batdau ul li {
                margin-left: 70px;
            }

            .dau .anh {
                margin-left: -150px;
            }

                .dau .anh img {
                    width: 650px;
                    height: 450px;
                }

            .batdau ul li:hover {
                margin-left: 70px;
            }

            .anhnen1 h2 {
                font-size: -60px;
            }

            .trenluuy {
                margin-left: -120px;
            }

            .trangchu1 .mau {
                margin-left: 80px;
                font-size: 40px;
                margin-top: 50px;
            }

            .trenluuy .luuy {
                width: 550px;
                margin-left: 210px;
            }
        }

        /* Quy tắc cho màn hình rất nhỏ */
        @media (max-width: 768px) {

            .anhnen1 img {
                height: auto;
            }

            .mau1 {
                margin-left: -60px !important;
            }

            .anhnen1 h2 {
                font-size: 18px;
            }

            .trangchu1 h3 {
                font-size: 14px;
            }

            .batdau {
                margin-left: 10px;
                width: 100%;
            }

                .batdau ul li {
                    margin-left: 10px;
                }

            .nhan {
                margin-left: -12px;
            }

            .batdau hr {
                width: 100%;
                margin-left: 25px;
                margin-top: 20px;
                margin-bottom: 20px;
            }

            .hotline-hidden {
                height: 50px;
                width: 150px;
            }

                .hotline-hidden a h2 {
                    font-size: 7px;
                    margin-bottom: -10px;
                    margin-top: -5px;
                }

                .hotline-hidden a {
                    font-size: 9px;
                    text-align: center;
                }

                .hotline-hidden i.bx-phone-call {
                    font-size: 8px;
                    margin-bottom: 15px;
                }

            .buoc p {
                margin-left: 10px;
                color: rgb(176, 3, 0);
            }

            .trangchu1 h3 {
                font-size: 12px;
                margin-top: 0px;
            }

            .trangchu1 i.bxs-chevron-right {
                font-size: 20px;
                margin-top: -1px;
            }

            .trangchu1 .mau1 {
                margin-left: 40px;
                font-size: 24px;
                margin-top: 70px;
            }

            .trenluuy {
                margin-left: -160px;
            }

                .trenluuy .luuy {
                    width: 400px;
                }

            .luuy h4 {
                font-size: 18px;
            }

            .luuy p {
                font-size: 16px;
            }

            .dau .anh {
                margin-left: -165px;
            }

                .dau .anh img {
                    width: 450px;
                    height: 250px;
                }

            .buoc .vongtron {
                margin-left: 0px;
            }

            .buoc .nhan {
                margin-left: 20px;
            }
        }

        .dau img {
            height: 600px;
            width: 800px;
            margin-left: 170px;
            border: 2px solid #87CEFA;
            margin-top: 20px;
        }

        .luuy {
            border: 1px solid rgb(203, 203, 203);
            margin-left: 200px;
            margin-top: 30px;
            padding: 5px 5px;
            width: 600px;
            margin-bottom: 30px;
        }

        .than ul li {
            margin-left: 120px;
            list-style: none;
            font-size: 18px;
            cursor: default;
            font-weight: 350;
        }

        #tong1 {
            opacity: 0;
            transition: opacity 0.2s ease-in;
        }

            #tong1.show {
                opacity: 1;
            }
        .containerrr {
            text-align: left;
        }
    </style>
    <div class="containerrr">
        <div class="anhnen1">
            <img src="./../img/abs.jpeg" alt="">
            <h2 style="font-weight: bolder; box-shadow: 50px; font-size: 30px;">Quy Trình Đi Khám</h2>
        </div>
        <div class="trangchu1">
            <a href="">
                <h3>Trang Chủ</h3>
            </a>
            <i class='bx bxs-chevron-right'></i>
            <h3>Hướng Dẫn</h3>
            <i class='bx bxs-chevron-right'></i>
            <h3 class="mau1">Quy Trình Đi Khám</h3>
        </div>
        <div class="gach1"></div>
        <div class="quy1">
            <h3>Quy Trình Đi Khám</h3>
        </div>
        <div class="batdau">
            <!-- <h4>Bước 1: Đặt lịch khám</h4> -->
            <div class="buoc">
                <div class="vongtron"></div>
                <div class="nhan">Bước 1: Xác Nhận Đăng Ký Khám Bệnh</div>
            </div>
            <!-- <h5>1. Đăng nhập hệ thống</h5> -->
            <ul>
                <li><i class='bx bxs-chevron-right'></i>Mô tả: Nếu bạn đã đăng ký khám bệnh trước,
                hãy đến Bộ phận Bảo Hiểm Y Tế (BHYT) để xác nhận đăng ký và nhận số thứ tự khám.</li>
                <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Trình thẻ BHYT và giấy tờ tùy thân tại quầy
                đăng ký. Ngồi chờ và lắng nghe khi số thứ tự của bạn được gọi.</li>
            </ul>

            <!-- <h4>Bước 2: Thanh toán tiền khám</h4> -->
            <hr>
            <div class="buoc">
                <div class="vongtron"></div>
                <div class="nhan">Bước 2: Gọi Số và Kiểm Tra Thẻ BHYT</div>
            </div>
            <ul>
                <li><i class='bx bxs-chevron-right'></i>Mô tả: Bộ phận BHYT sẽ gọi số thứ tự của bạn và kiểm tra thẻ
                BHYT cùng các giấy tờ cần thiết.
                </li>
                <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Đảm bảo bạn có thẻ BHYT và giấy tờ tùy thân sẵn sàng.
                Sau khi thông tin được kiểm tra và nhập vào hệ thống, bạn sẽ được hướng dẫn đến phòng khám chuyên
                khoa phù hợp.</li>
            </ul>
            <!-- <h4>Bước 3: Xác nhận lịch hẹn</h4> -->
            <hr>
            <div class="buoc">
                <div class="vongtron"></div>
                <div class="nhan">Bước 3: Khám Chuyên Khoa và Tư Vấn Bệnh Lý</div>
            </div>
            <ul>
                <li><i class='bx bxs-chevron-right'></i>Mô tả: Bác sĩ chuyên khoa sẽ thực hiện việc khám bệnh, tư vấn
                bệnh lý và quyết định phương án điều trị.</li>
                <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Bạn có thể nhận được chỉ định cho các xét nghiệm cận
                lâm sàng, yêu cầu nhập viện (nếu cần), hoặc kê đơn thuốc để điều trị ngoại trú.</li>
            </ul>
            <!-- <h4>Bước 4: Khám và thực hiện cận lâm sàng</h4> -->
            <hr>
            <div class="buoc">
                <div class="vongtron"></div>
                <div class="nhan">Bước 4: Thực Hiện Các Chỉ Định Cận Lâm Sàng</div>
            </div>
            <ul>
                <li><i class='bx bxs-chevron-right'></i>Mô tả: Bạn sẽ được thực hiện các chỉ định cận lâm sàng như xét
                nghiệm, siêu âm, X-quang, CT, MRI, điện tim, nội soi, v.v.</li>
                <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Sau khi hoàn tất các xét nghiệm, nhận kết quả và trở
                lại phòng khám để bác sĩ đưa ra kết luận và tư vấn điều trị tiếp theo.</li>
            </ul>
            <!-- <h4>Bước 5: Nhận thuốc</h4> -->
            <hr>
            <div class="buoc">
                <div class="vongtron"></div>
                <div class="nhan">Bước 5: Làm Thủ Tục Thanh Toán</div>
            </div>
            <ul>
                <li><i class='bx bxs-chevron-right'></i>Mô tả: Quay lại Bộ phận BHYT để làm thủ tục thanh toán chi phí
                khám bệnh nếu có.</li>
                <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Tổ thanh toán BHYT sẽ thu tiền (nếu có chi phí phát
                sinh), cấp biên lai, trả lại thẻ BHYT và hướng dẫn bạn đến khoa Dược để nhận thuốc.
                </li>
            </ul>
            <!-- <h4>Bước 6: Đặt lịch tái khám</h4> -->
            <hr>
            <div class="buoc">
                <div class="vongtron"></div>
                <div class="nhan">Bước 6: Cấp Thuốc và Hướng Dẫn</div>
            </div>
            <ul>
                <li><i class='bx bxs-chevron-right'></i>Mô tả: Khoa Dược sẽ tiếp nhận đơn thuốc, cấp thuốc và hướng dẫn
                cách sử dụng thuốc cho bạn.
                </li>
                <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Đảm bảo bạn nhận đúng loại thuốc và được giải thích
                rõ cách sử dụng.</li>
            </ul>
            <div class="buoc">
                <div class="vongtron"></div>
                <div class="nhan">Bước 7: Kiểm Tra Thuốc Được Cấp</div>
            </div>
            <ul>
                <li><i class='bx bxs-chevron-right'></i>Mô tả: Kiểm tra thuốc nhận được so với đơn thuốc của bác sĩ để
                đảm bảo đúng loại và liều lượng.
                </li>
                <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Nếu phát hiện bất kỳ sự không khớp nào, thông báo
                ngay cho nhân viên y tế để được hỗ trợ.</li>
            </ul>
        </div>
        <div id="hotline" class="hotline-hidden">
            <a href="tel:19003456">
                <h2>Cần Hỗ Trợ</h2>
                <i class='bx bx-phone-call'></i>Hotline: 1900 3456
            </a>
        </div>
        <div class="trenluuy">
            <div class="luuy">
                <h4 style="color: red;">Lưu ý : </h4>
                <p>- Đảm bảo bạn đến đúng thời gian đã đăng ký để tránh chờ đợi lâu.</p>
                <p>- Luôn tuân thủ các hướng dẫn của nhân viên y tế để quy trình diễn ra thuận lợi.</p>
            </div>
        </div>
    </div>


    <script>
        window.addEventListener('scroll', function () {
            var hotline = document.getElementById('hotline');
            var position = hotline.getBoundingClientRect();
            if (window.scrollY > 100) {
                hotline.style.opacity = '1';
                hotline.style.visibility = 'visible';
            } else {
                hotline.style.opacity = '0';
                hotline.style.visibility = 'hidden';
            }
        });
    </script>
    <script>
        window.addEventListener('scroll', function () {
            var fadeInElements = document.querySelectorAll('.batdau');

            fadeInElements.forEach(function (element) {
                var position = element.getBoundingClientRect();
                if (position.top < window.innerHeight && position.bottom >= 0) {
                    element.classList.add('show');
                }
            });
        });
    </script>
    <script>
        window.addEventListener('scroll', function () {
            var fadeInElements = document.querySelectorAll('#tong1');

            fadeInElements.forEach(function (element) {
                var position = element.getBoundingClientRect();
                if (position.top < window.innerHeight && position.bottom >= 0) {
                    element.classList.add('show');
                }
            });
        });
    </script>
    <script>
        window.addEventListener('scroll', function () {
            var fadeInElements = document.querySelectorAll('.nen1');

            fadeInElements.forEach(function (element) {
                var position = element.getBoundingClientRect();
                if (position.top < window.innerHeight && position.bottom >= 0) {
                    element.classList.add('show');
                }
            });
        });
    </script>
    <script>
        window.addEventListener('scroll', function () {
            var fadeInElements = document.querySelectorAll('.trangchu');

            fadeInElements.forEach(function (element) {
                var position = element.getBoundingClientRect();
                if (position.top < window.innerHeight && position.bottom >= 0) {
                    element.classList.add('show');
                }
            });
        });
    </script>
    <script>
        window.addEventListener('scroll', function () {
            var fadeInElements = document.querySelectorAll('.dau');

            fadeInElements.forEach(function (element) {
                var position = element.getBoundingClientRect();
                if (position.top < window.innerHeight && position.bottom >= 0) {
                    element.classList.add('show');
                }
            });
        });
    </script>
</asp:Content>
