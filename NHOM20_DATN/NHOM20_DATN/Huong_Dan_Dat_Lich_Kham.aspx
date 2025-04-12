<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Huong_Dan_Dat_Lich_Kham.aspx.cs" Inherits="NHOM20_DATN.Huong_Dan_Dat_Lich_Kham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .anhnen img {
            width: 100%;
            min-height: 100px;
            height: 500px;
            filter: brightness(60%);
        }

        .anhnen {
            text-align: center;
            position: relative;
        }

            .anhnen h2 {
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

        .trangchu {
            display: flex;
            padding-top: 20px;
            margin-left: 5%;
        }

            .trangchu a {
                text-decoration: none;
                color: black;
            }

            .trangchu h3 {
                font-size: 18px;
            }

            .trangchu .mau {
                color: dodgerblue;
            }

            .trangchu i {
                padding: 0 10px;
                font-size: 20px;
                margin-top: 2px;
            }

        .gach {
            border: 1px solid rgb(231, 231, 231);
            width: 100%;
        }

        .quy h3 {
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
            background-color: dodgerblue;
            border-radius: 50%;
            position: absolute;
            left: -29px;
            top: 13px;
        }

        .nhan {
            margin-left: 60px;
            font-size: 28px;
            color: dodgerblue;
        }

        .than h5 {
            margin-left: 100px;
            font-size: 22px;
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

        .than {
            opacity: 0;
            transition: opacity 0.4s ease-in;
        }

            .than.show {
                opacity: 1;
            }

        .buoc p {
            margin-left: 90px;
            color: rgb(176, 3, 0);
        }

        .than ul li i {
            font-size: 16px;
            transform: scaleX(0.9);
        }

        .than ul li:hover {
            margin-left: 120px;
            list-style: none;
            font-size: 18px;
            color: dodgerblue;
        }

        .than {
            width: 80%;
        }

            .than hr {
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


        @media (min-width: 1201px) {


            .anhnen h2 {
                font-size: 24px;
            }

            .trangchu h3 {
                font-size: 16px;
            }

            .than h5 {
                font-size: 20px;
            }
        }


        /* Quy tắc cho màn hình trung bình */
        @media (max-width: 1200px) and (min-width: 993px) {

            .anhnen {
                padding: 10px;
            }
        }

        /* Quy tắc cho màn hình nhỏ */
        @media (max-width: 992px) and (min-width: 769px) {
            .than {
                width: 100%;
            }

                .than ul li {
                    margin-left: 90px;
                }

                    .than ul li:hover {
                        margin-left: 90px;
                    }

            .anhnen h2 {
                font-size: 20px;
            }

            .than {
                margin-left: -40px;
                width: 100%;
            }


                .than ul li {
                    margin-left: 30px;
                }

                    .than ul li:hover {
                        font-size: 18px;
                        margin-left: 30px;
                    }

                .than h5 {
                    margin-left: 55px;
                }

            .nhan {
                margin-left: 40px;
            }

            .luuy {
                width: 500px;
            }
        }

        /* Quy tắc cho màn hình rất nhỏ */
        @media (max-width: 768px) {
            .buoc .vongtron {
                margin-left: 0px;
            }

            .buoc .nhan {
                margin-left: 20px;
            }

            .anhnen img {
                height: auto;
            }

            .anhnen h2 {
                font-size: 16px;
            }

            .than {
                margin-left: -40px;
                width: 100%;
            }

                .than h5 {
                    margin-left: 25px;
                }

                .than ul li {
                    margin-left: 20px;
                }

                    .than ul li:hover {
                        font-size: 18px;
                        margin-left: 20px;
                    }

            .nhan {
                margin-left: -12px;
            }

            .than hr {
                width: 100%;
                margin-left: 25px;
                margin-top: 20px;
                margin-bottom: 20px;
            }

            .hotline-visible {
                height: 40px;
                width: 140px;
            }

                .hotline-visible a h2 {
                    font-size: 8px;
                    margin-bottom: -10px;
                }

                .hotline-visible a {
                    font-size: 8px;
                    margin-top: 0px;
                    text-align: center;
                }

                .hotline-visible i.bx-phone-call {
                    font-size: 8px;
                    margin-bottom: 15px;
                }

            .buoc p {
                margin-left: 10px;
                color: rgb(176, 3, 0);
            }
        }

        .luuy {
            border: 1px solid rgb(203, 203, 203);
            margin-left: 150px;
            margin-top: 30px;
            padding: 5px 20px;
            margin-bottom: 30px;
        }

        .containerr {
            text-align: left;
        }
    </style>
    <div class="containerr">
        <div class="anhnen">
            <img src="../img/ahd1.jpeg" alt="">
            <h2 style="font-weight: bolder; box-shadow: 50px; font-size: 30px;">Quy Trình Đặt Lịch Khám Bệnh</h2>
        </div>
        <div class="trangchu">
            <a href="">
                <h3>Trang Chủ</h3>
            </a>
            <i class='bx bxs-chevron-right'></i>
            <h3>Hướng Dẫn</h3>
            <i class='bx bxs-chevron-right'></i>
            <h3 class="mau">Đặt Lịch Khám</h3>
        </div>
        <div class="gach"></div>
        <div class="quy">
            <h3>Quy Trình Đặt Lịch Khám Bệnh</h3>
        </div>
        <div class="than">

            <div class="batdau">
                <!-- <h4>Bước 1: Đặt lịch khám</h4> -->
                <div class="buoc">
                    <div class="vongtron"></div>
                    <div class="nhan">Bước 1: Đặt lịch khám</div>
                </div>
                <h5>1. Đăng nhập hệ thống</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Hướng dẫn đăng nhập: Truy cập trang đăng nhập của hệ thống.
                    Nhập tên đăng nhập và mật khẩu của bạn, sau đó nhấn nút "Đăng nhập".</li>
                </ul>
                <h5>2. Chọn hình thức đặt khám:</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Tùy chọn Theo Ngày: Bạn chọn ngày cụ thể mà bạn muốn khám.
                    Hệ thống sẽ hiển thị các bác sĩ có sẵn vào ngày đó, nhập thông tin tức là chọn ngày từ lịch và
                    hệ thống sẽ hiện lên danh sách các bác sĩ có mặt.</li>
                    <li><i class='bx bxs-chevron-right'></i>Tùy chọn Theo bác sĩ: Bạn chọn bác sĩ cụ thể mà bạn muốn
                    khám, và hệ thống sẽ hiển thị các ngày và giờ bác sĩ đó có sẵn,nhập thông tin nghĩa là chọn tên
                    bác sĩ từ danh sách và hệ thống sẽ hiển thị các khoảng thời gian còn trống.</li>
                </ul>
                <h5>3. Nhập thông tin bệnh nhân:</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Hướng dẫn nhập số hồ sơ bệnh nhân: Nếu bạn đã có hồ sơ bệnh
                    nhân, nhập số hồ sơ để hệ thống tự động điền thông tin cá nhân của bạn. hoặc tạo mới (họ tên,
                    ngày sinh, giới tính, CMND/CCCD, số điện thoại, địa chỉ, email, lý do khám ).</li>
                </ul>
                <h5>4. Chọn thông tin khám:</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Lựa chọn chuyên khoa (Chọn chuyên khoa mà bạn cần khám ),
                    bác sĩ (nếu chưa chọn theo bác sĩ ở bước trước), ngày khám và giờ khám (Chọn ngày và giờ phù hợp
                    từ lịch trống ), và xác
                    định xem có sử dụng BHYT không(Nếu có, bạn sẽ cần cung cấp thêm thông tin về thẻ BHYT ).</li>
                </ul>

                <!-- <h4>Bước 3: Xác nhận lịch hẹn</h4> -->
                <hr>
                <div class="buoc">
                    <div class="vongtron"></div>
                    <div class="nhan">Bước 3: Xác nhận lịch hẹn</div>
                </div>
                <h5>1. Nhận thông báo:</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Phiếu khám điện tử: Bạn sẽ nhận được phiếu khám qua email,
                    SMS, hoặc trực tiếp trên ứng dụng.</li>
                </ul>
                <h5>2. Chuẩn bị đến khám:</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Hướng dẫn: Đến đúng giờ và địa điểm đã đặt lịch,nếu bạn có
                    BHYT, hãy chuẩn bị thẻ BHYT.</li>
                </ul>

                <!-- <h4>Bước 6: Đặt lịch tái khám</h4> -->
                <hr>
                <div class="buoc">
                    <div class="vongtron"></div>
                    <div class="nhan">Bước 4: Đặt lịch tái khám</div>
                </div>
                <h5>1. Sử dụng phần mềm đặt lịch tái khám:</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Quy trình đặt tái khám: Thực hiện quy trình tương tự như
                    Bước 1 và Bước 2 để đặt lịch tái khám.
                    </li>
                    <li><i class='bx bxs-chevron-right'></i>Nhập thông tin bệnh nhân dựa trên số hồ sơ hiện có.</li>
                </ul>
                <h5>Nhập thông tin bệnh nhân:</h5>
                <ul>
                    <li><i class='bx bxs-chevron-right'></i>Dựa trên số hồ sơ: Sử dụng số hồ sơ hiện có để tự động điền
                    thông tin.</li>
                </ul>
            </div>
            <div id="hotline" class="hotline-hidden">
                <a href="tel:19003456">
                    <h2>Cần Hỗ Trợ</h2>
                    <i class='bx bx-phone-call'></i>Hotline: 1900 3456
                </a>
            </div>
            <div class="luuy">
                <h4 style="color: red;">*Lưu ý : </h4>
                <p>
                    - Đảm bảo tính bảo mật: Người dùng phải đăng nhập vào hệ thống để đảm bảo thông tin cá nhân được bảo
                vệ.
                </p>
                <p>- Thông báo tự động: Hệ thống cần gửi thông báo về lịch hẹn qua email và tin nhắn SMS.</p>
                <p>- Hỗ trợ khách hàng: Cung cấp mục liên hệ hoặc hỗ trợ kỹ thuật khi cần.</p>
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
            var fadeInElements = document.querySelectorAll('.than');

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
            var fadeInElements = document.querySelectorAll('.container');

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
            var fadeInElements = document.querySelectorAll('.anhnen');

            fadeInElements.forEach(function (element) {
                var position = element.getBoundingClientRect();
                if (position.top < window.innerHeight && position.bottom >= 0) {
                    element.classList.add('show');
                }
            });
        });
    </script>
    <script>
        document.getElementById("hotline").addEventListener("click", function () {
            window.location.href = "tel:19003456";
        });
    </script>

</asp:Content>
