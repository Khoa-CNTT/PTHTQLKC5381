<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tu_Van_Suc_Khoe_Truc_Tuyen.aspx.cs" Inherits="NHOM20_DATN.Patient.Tu_Van_Suc_Khoe_Truc_Tuyen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .quangcao {
            background-color: white;
        }

        .bangtuvan {
            background-color: #F3F3F3;
            border-radius: 25px;
            margin: 50px 30px;
            padding-left: 30px;
        }

            .bangtuvan p {
                background: linear-gradient(to right, #00c6fb, #005bea);
                -webkit-background-clip: text;
                color: transparent;
            }

        #button_chat {
            max-width: 200px;
            background-color: #ffc800;
            color: aliceblue;
            border-radius: 10px;
            border: none;
            font-size: 20px;
            font-weight: bold;
        }

        #khungbacsi {
            position: relative;
            background-color: white;
        }

        .cham_ngon {
            position: absolute;
            top: 20%;
            left: 5%;
            width: 220px;
            height: 100px;
            border-radius: 30px;
        }

            .cham_ngon h5 {
                background: linear-gradient(to right, #0250c5, #d43f8d);
                -webkit-background-clip: text;
                color: transparent;
            }

        .background_bacsi {
            width: 100%;
        }

        .animation {
            font-size: 24px;
            font-weight: bold;
            width: 100%;
            background: linear-gradient(to right, #fefefe, #fefefe);
            -webkit-background-clip: text;
            color: #0250c5;
            overflow: hidden;
            white-space: nowrap;
            border-right: 5px solid white; /* Hiệu ứng con trỏ */
            animation: typing 6s steps(30, end) infinite, blink-caret 0.75s step-end infinite;
            margin-left: 20%;
        }


        @keyframes typing {
            0% {
                width: 0;
            }

            50% {
                width: 60%; /* Chữ chạy ra */
            }

            100% {
                width: 0; /* Chữ thu lại */
            }
        }

        @keyframes blink-caret {
            from, to {
                border-color: transparent;
            }

            50% {
                border-color: white;
            }
        }

        @media (max-width: 768px) {
            .animation {
                font-size: 15px; /* Giảm kích thước chữ trên thiết bị nhỏ hơn */
                margin-left: 2%; /* Giảm khoảng cách lề trái để phù hợp với màn hình nhỏ */
                animation: typing 5s steps(20, end) infinite, blink-caret 0.75s step-end infinite;
            }

            .animation {
                animation: typing 5s steps(30, end) infinite, blink-caret 0.75s step-end infinite;
            }
        }

        .loiich .card:hover {
            border-radius: 20%;
            transition: 0.5s ease-in-out;
        }

        .loiich .card img {
            width: 270px;
            height: 200px;
            padding-top: 5px;
            border-radius: 50px;
        }

        .loiich .card {
            margin: 0 5px;
            left: 10%;
            opacity: 0; /* Ẩn thẻ card */
            transform: translateY(20px); /* Di chuyển xuống dưới */
            transition: opacity 2s ease, transform 2s ease; /* Hiệu ứng chuyển tiếp */
        }

            .loiich .card.visible {
                opacity: 1; /* Hiện thẻ card */
                transform: translateY(0); /* Trở về vị trí ban đầu */
            }

        .noidungchinh {
            background-image: linear-gradient(to right, #cae6f2, #c2e9fb);
        }

        .tieude_tuvan {
            background-color: white;
            border-radius: 30px;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            align-items: center;
            margin: 20px 50px;
        }

            .tieude_tuvan h5 {
                text-align: center;
                margin-top: 3%;
                background: linear-gradient(to right, #667eea, #764ba2);
                -webkit-background-clip: text;
                color: transparent;
                font-weight: bold;
            }

        .khungbacsi {
            border: none;
            border-radius: 20px;
            overflow: hidden;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            background: linear-gradient(145deg, #ffffff, #f0f4f8);
        }

            .khungbacsi:hover {
                transform: translateY(-6px);
                box-shadow: 0 12px 36px rgba(0, 0, 0, 0.1);
            }

        .hinhbacsi {
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 24px;
        }

        .bacsi {
            width: 120px;
            height: 120px;
            border-radius: 50%;
            object-fit: cover;
            box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
        }

        .card-body {
            padding: 24px;
        }

        .table td {
            padding: 6px 0;
            font-size: 15px;
            color: #333;
        }

        .btn_tuvan {
            background: linear-gradient(to right, #0d47a1, #1976d2);
            color: white;
            border: none;
            border-radius: 30px;
            padding: 8px 20px;
            font-weight: 600;
            transition: all 0.3s ease;
        }

            .btn_tuvan:hover {
                background: linear-gradient(to right, #1565c0, #1e88e5);
                transform: scale(1.05);
            }

        .col.mb-4 {
            display: flex;
            justify-content: center;
        }

        .khungbacsi {
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            border-radius: 16px;
            margin: 20px 80px;
            vertical-align: top;
            white-space: normal;
        }

        .scroll-wrapper {
            overflow-x: auto;
            white-space: nowrap;
            padding-bottom: 1rem;
        }
        .video_danhgia {
            margin-bottom : 30px;
        }

    </style>

    <div class="content">
        <div class="container">

            <div class="quangcao">
                <div class="row">
                    <div class="col-md-6 col-xl-6 ml-2">
                        <div class="bangtuvan">
                            <h3 style="background: linear-gradient(to right, #00c6fb, #005bea); -webkit-background-clip: text; color: transparent; font-weight: bold;">Tư vấn khám bệnh qua video</h3>
                            <p>Chăm sóc sức khoẻ từ xa kết nối với Bác sĩ qua cuộc gọi Video và Nhắn Tin mọi lúc mọi nơi</p>
                            <hr />
                            <div class="row">
                                <div class="col">
                                    <p>Liên hệ <b>chuyên gia</b> để tư vấn thêm</p>
                                </div>
                                <div class="col">
                                    <img style="width: 30px" src="../img/telephone.png" />
                                    <p>1900 3456 hoặc</p>
                                </div>
                                <div class="col">
                                    <button id="button_chat">Chat ngay</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="khungbacsi" class="col-md-6 col-xl-6">
                        <div class="cham_ngon">
                            <h5>"Sức khỏe của bạn, sứ mệnh của chúng tôi"</h5>
                        </div>
                        <img class="img-fluid shadow background_bacsi" style="margin: 50px 0px;" src="../img/bacsituvan.png" />
                    </div>
                </div>

            </div>
            <div class="noidungchinh">
                <div class="row">
                    <div class="col">
                        <p class="animation">
                            Tại sao tư vấn sức khỏe trực tuyến tại Banana là
            <panel>lựa chọn tối ưu!</panel>
                        </p>
                    </div>
                </div>
                <div class="row loiich">
                    <div class="card" style="width: 20rem;">
                        <img src="../img/chiphi.jpeg" class="card-img-top" alt="chi phí">
                        <div class="card-body">
                            <p class="card-text">Tư vấn sức khỏe trực tuyến tại Banana bạn sẽ không tốn bất cứ chi phí nào.</p>
                        </div>
                    </div>
                    <div class="card" style="width: 20rem;">
                        <img src="../img/laixe.jpg" class="card-img-top" alt="di chuyển">
                        <div class="card-body">
                            <p class="card-text">Bệnh nhân không cần phải di chuyển đến bệnh viện hoặc phòng khám, có thể nhận tư vấn ngay tại nhà qua các thiết bị kết nối internet.</p>
                        </div>
                    </div>
                    <div class="card" style="width: 20rem;">
                        <img src="../img/xephang.png" class="card-img-top" alt="chờ đợi">
                        <div class="card-body">
                            <p class="card-text">Việc tư vấn sức khỏe trực tuyến thường diễn ra nhanh chóng, bệnh nhân không cần xếp hàng chờ đợi như khi đến khám tại bệnh viện.</p>
                        </div>
                    </div>
                </div>
                <div class="tieude_tuvan">
                    <div class="row">
                        <div class="col-md-4">
                            <img style="width: 200px;" src="../img/icon_lichkham.png" />
                        </div>
                        <div class="col-md-8">
                            <h5>Ngay với những chuyên gia hàng đầu của chúng tôi!</h5>
                        </div>

                    </div>
                </div>
            </div>

            <div class="container py-4">
                <div class="scroll-wrapper">
                    <asp:DataList ID="dl_bacsi" RepeatDirection="Horizontal" runat="server">
                        <ItemTemplate>
                            <div class="card khungbacsi d-inline-block mx-2" style="width: 400px;">
                                <div class="row g-0 align-items-center">
                                    <div class="col-4 hinhbacsi">
                                        <asp:Image ID="Image1" runat="server" CssClass="bacsi img-fluid" ImageUrl='<%# Eval("HinhAnh") %>' />
                                    </div>
                                    <div class="col-8">
                                        <div class="card-body p-2">
                                            <table class="table table-borderless mb-2">
                                                <tr>
                                                    <td><strong>ID bác sĩ:</strong>
                                                        <asp:Label ID="lbl_idbacsi" runat="server" Text='<%# Eval("IDBacSi")%>' /></td>
                                                </tr>
                                                <tr>
                                                    <td><strong>Tên bác sĩ:</strong>
                                                        <asp:Label ID="lbl_hoten" runat="server" Text='<%# Eval("HoTen") %>' /></td>
                                                </tr>
                                                <tr>
                                                    <td><strong>Trình độ:</strong>
                                                        <asp:Label ID="lbl_trinhdo" runat="server" Text='<%# Eval("TrinhDo") %>' /></td>
                                                </tr>
                                                <tr>
                                                    <td><strong>Email:</strong>
                                                        <asp:Label ID="lbl_email" runat="server" Text='<%# Eval("Email") %>' /></td>
                                                </tr>
                                                <tr>
                                                    <td><strong>Chuyên khoa:</strong>
                                                        <asp:Label ID="lbl_chuyenkhoa" runat="server" Text='<%# Eval("TenChuyenKhoa") %>' /></td>
                                                </tr>
                                                <tr>
                                                    <td><strong>Vai trò:</strong>
                                                        <asp:Label ID="lbl_vaitro" runat="server" Text='<%# Eval("VaiTro") %>' /></td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="btn_tuvan" CssClass="btn btn-success btn-sm w-100" runat="server" Text="Tư vấn ngay" OnClick="btn_tuvan_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>

            <div class="ratio ratio-16x9 video_danhgia position-relative">
                <video id="video_danhgia" autoplay muted loop>
                    <source src="../img/Lợi ích khám trực tuyến.mp4" type="video/mp4">
                </video>

                <!-- Nút điều khiển -->
                <div class="position-absolute top-0 end-0 p-2">
                    <button type="button" id="btnToggleMute" class="btn btn-sm btn-primary me-2">🔇 Bật âm</button>
                    <button type="button" id="btnTogglePlay" class="btn btn-sm btn-secondary">⏸ Tạm dừng</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const cards = document.querySelectorAll('.card');

            function showCardsOnScroll() {
                const triggerBottom = window.innerHeight / 5 * 4; // Điểm kích hoạt

                cards.forEach(card => {
                    const cardTop = card.getBoundingClientRect().top; // Vị trí thẻ card

                    if (cardTop < triggerBottom) {
                        card.classList.add('visible'); // Thêm lớp để hiển thị
                    }
                });
            }

            window.addEventListener('scroll', showCardsOnScroll);
            showCardsOnScroll(); // Kiểm tra khi tải trang
        });
    </script>
    <script>
        const video = document.getElementById('video_danhgia');
        const btnMute = document.getElementById('btnToggleMute');
        const btnPlay = document.getElementById('btnTogglePlay');

        // Bật / tắt âm thanh
        btnMute.addEventListener('click', () => {
            video.muted = !video.muted;
            btnMute.textContent = video.muted ? '🔇 Bật âm' : '🔊 Tắt âm';
        });

        // Tạm dừng / phát video
        btnPlay.addEventListener('click', () => {
            if (video.paused) {
                video.play();
                btnPlay.textContent = '⏸ Tạm dừng';
            } else {
                video.pause();
                btnPlay.textContent = '▶️ Tiếp tục';
            }
        });
    </script>
</asp:Content>
