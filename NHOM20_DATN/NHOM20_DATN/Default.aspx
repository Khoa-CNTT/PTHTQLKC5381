<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NHOM20_DATN.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>
        @import url('https://fonts.googleapis.com/css2?family=Great+Vibes&display=swap');

        .chucnang {
            padding-top: 40px;
            margin-left: 20px;
        }

            .chucnang .card {
                border-radius: 20px;
                transition: transform 0.5s ease-in-out;
            }

                .chucnang .card:hover {
                    border: 1px solid #3c85e0;
                    transform: scale(1.1);
                }

        .cum1 {
            background-image: linear-gradient(#fff1eb, #ace0f9);
        }

        .gioithieuchung {
            margin-top: 100px;
            padding-right: 50px;
        }

        #benhvien:before {
            mix-blend-mode: screen;
        }

        .quangcao1 {
            width: 100%;
            border-radius: 20px;
        }

        .noidung1 {
            margin-top: 50px;
            background-image: linear-gradient(#93a5cf, #e4efe9);
        }

        .noidungchinh {
            opacity: 0; /* Ẩn phần tử ban đầu */
            transform: translateY(50px); /* Dịch chuyển xuống 50px */
            transition: opacity 0.6s ease-out, transform 1.2s ease-out; /* Thiết lập hiệu ứng chuyển động */
            margin-top: 80px;
            margin-left: -30px;
        }

            .noidungchinh h5 {
                margin-top: 50px;
            }

            .noidungchinh.visible {
                opacity: 1; /* Hiển thị phần tử */
                transform: translateY(0); /* Trả về vị trí ban đầu */
            }

            .noidungchinh img {
                margin-left: 30px;
                -webkit-transform: scale(1.5);
                transform: scale(1.5);
                -webkit-transition: .3s ease-in-out;
                transition: .3s ease-in-out;
            }

                .noidungchinh img:hover {
                    margin-left: 0;
                }

        .noidung2 {
            background-image: linear-gradient(#a1c4fd, #c2e9fb);
        }

            .noidung2 img {
                width: 200px;
            }

        .noidung3 {
            padding-top: 50px;
            background-image: linear-gradient(to right,#f3e7e9, #e3eeff, #e3eeff);
        }

            .noidung3 p {
                text-align: justify;
            }

            .noidung3 hr {
                background-color: #f9290c;
                min-height: 3px;
                max-width: 300px;
            }

        .thongtin_lienket {
            min-height: 400px;
        }

            .thongtin_lienket img {
                width: 150px;
            }

                .thongtin_lienket img:hover {
                    transform-style: preserve-3d;
                    transform: rotateY(-180deg);
                    transition: transform 650ms ease-in-out, filter 650ms ease-in-out;
                }


        .lienket {
            padding-top: 50px;
        }


            .lienket .row {
                margin: 10px 10px 0 0;
            }

        .danhgia {
            position: relative;
        }

        .slide {
            position: absolute;
            top: 3%;
        }

        .khungdanhgia .binhluan {
            border-bottom: 1px solid white;
        }

        .carousel-inner {
            top: 10%;
        }

        .khungdanhgia {
            background-color: #1686c4;
            opacity: 0.8;
            color: #ffffff;
            border-radius: 30px;
            margin-top: 20px;
            padding-top: 10px;
            max-width: 100%;
        }
        /* Khi màn hình nhỏ */
        @media (max-width: 767px) {
            .khungdanhgia {
                padding: 10px; /* Giảm padding để khung nhỏ gọn hơn */
                margin: 5px auto; /* Điều chỉnh khoảng cách giữa các khung */
            }
        }

        .tieude_danhgia {
            background: linear-gradient(to right,#ffffff, #e3eeff);
            font-weight: bold;
            -webkit-background-clip: text;
            color: transparent;
        }

        .noidung_danhgia {
            top: 5%;
        }

        .hoten img {
            width: 40px;
            border-radius: 100px;
        }
    </style>
    <div class="body">
        <div class="content">
            <div class="container text-center">
                <div class="background">
                    <div class="row">
                        <div class="col-xl-12">
                            <img style="width: 100%;" src="img/background.png" />
                        </div>
                    </div>
                </div>
                <div class="cum1">
                    <div class="chucnang">
                        <div class="row">
                            <div class="col-6 col-md-3">
                                <div class="card" style="width: 190px; height: 220px; background-color: white">
                                    <img src="img/lichkham.png" style="width: 80px; margin-left: 60px;" class="card-img-top" alt="...">
                                    <div class="card-body">
                                        <h5 class="card-title">Đặt khám
                                        <br />
                                            trực tiếp</h5>
                                        <a href="DangKyKhamTrucTiep.aspx" style="margin-top: 10px;" class="btn btn-primary">Tại đây</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 col-md-3">
                                <div class="card" style="width: 190px; height: 220px; background-color: white">
                                    <img src="img/videocall.png" style="width: 80px; margin-left: 60px;" class="card-img-top" alt="...">
                                    <div class="card-body">
                                        <h5 class="card-title">Tư vấn sức khỏe
                                        <br />
                                            trực tuyến</h5>
                                        <a href="ThongTinTuVan.aspx" style="margin-top: 10px;" class="btn btn-primary">Tại đây</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 col-md-3">
                                <div class="card" style="width: 190px; height: 220px; background-color: white">
                                    <img src="img/huykham.png" style="width: 80px; margin-left: 60px;" class="card-img-top" alt="...">
                                    <div class="card-body">
                                        <h5 class="card-title">Hủy khám</h5>
                                        <a href="Huy_Kham.aspx" style="margin-top: 30px;" class="btn btn-primary">Tại đây</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 col-md-3">
                                <div class="card" style="width: 190px; height: 220px; background-color: white">
                                    <img src="img/quytrinh.png" style="width: 80px; margin-left: 60px;" class="card-img-top" alt="...">
                                    <div class="card-body">
                                        <h5 class="card-title">Xem quy
                                            <br />
                                            trình khám</h5>
                                        <a href="#" style="margin-top: 5px;" class="btn btn-primary">Tại đây</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="gioithieuchung">
                        <div class="row">
                            <div class="col">
                                <img style="width: 400px; margin-top: 60px; border: 10px solid #f4f4f4;" src="img/benhvien.jpg" />
                            </div>
                            <div class="col">
                                <p style="font-family: Sans-serif; font-size: 40px; font-weight: bold; background: linear-gradient(to right, #00c6fb, #005bea); -webkit-background-clip: text; color: transparent;">BỆNH VIỆN ĐA KHOA ĐA CHỨC NĂNG BANANA</p>
                                <p style="font-family: 'Great Vibes', cursive; font-size: 30px; color: #ff69b4; text-shadow: 2px 2px 4px #fff1eb; letter-spacing: 2px;">Tạo dựng niềm tin bằng chất lượng</p>
                                <p style="text-align: justify;">
                                    Bệnh viện Đa khoa Quốc tế Banana được Sở Y tế Đà Nẵng cấp phép hoạt động, có chức năng và nhiệm vụ cung cấp các dịch vụ chăm sóc sức khỏe chất lượng cao cho cộng đồng. Trong đó, phòng khám đặc biệt chú trọng đến việc thăm 
                    khám và điều trị các bệnh lý Nam Khoa; Phụ khoa; Hậu môn trực tràng; Các bệnh lây truyền qua đường tình dục. Thực hiện chăm sóc sức khỏe sinh sản và giới tính cho cả nam và nữ, điều trị vô sinh – hiếm muộn. Ngoài ra, các bệnh 
                    lý về Da liễu, Xương khớp, Hô hấp, Tiêu hóa…cũng được phòng khám quan tâm và đầu tư mạnh mẽ. Hiện tại, bệnh viện đã ký hợp tác toàn diện về chuyên môn, cơ sở vật chất, trang thiết bị y tế, nhân lực… với Bệnh viện 199 – Bộ Công an. 
                    Bệnh viện 199 là Bệnh viện đầu ngành tuyến Trung ương của Bộ Công an, là cơ sở uy tín có chức năng và nhiệm vụ chăm sóc sức khoẻ cho lãnh đạo, cán bộ chiến sĩ, nhân dân trên địa bàn Đà Nẵng và duyên hải Miền trung Tây Nguyên
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="noidung1">
                        <div style="color: #ffffff; font-size: 16px;" class="quangcao1">
                            <div class="row">
                                <div class="col-xl-4">
                                    <img style="width: 300px; border-radius: 20px;" src="img/Aologo.png" />
                                    <h3>Đặt khám nhanh</h3>
                                </div>
                                <div class="col-xl-8">
                                    <p style="font-size: 80px; font-weight: bold;">Banana</p>
                                    <p style="font-size: 16px; text-align: justify; padding: 20px 25px;">
                                        Cung cấp dịch vụ đặt lịch khám bệnh tại cơ sở và chăm sóc sức khỏe trực tuyến nhanh chóng, hàng đầu, hiệu quả giúp người dùng tự lựa chọn
                         dịch vụ và bác sĩ theo nhu cầu của mình.
                                    </p>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="noidungchinh">
                        <div class="row">
                            <div class="col-xl-4">
                                <img style="width: 230px; border-radius: 20px;" src="img/thoigian.jpg" />
                                <h5>Vì thời gian của bạn là vô giá</h5>
                                <p>
                                    Bệnh nhân chủ động chọn thông
                    <br />
                                    tin đặt khám (ngày khám)
                                </p>
                            </div>
                            <div class="col-xl-4">
                                <img style="width: 250px; height: 147px; border-radius: 20px;" src="img/vundapsuckhoe.jpg" />
                                <h5>Vun đắp sức khỏe cho mọi nhà</h5>
                                <p>
                                    Bệnh nhân sẽ nhận phiếu khám trực
                    <br />
                                    tuyến ngay trên phần mềm
                                </p>
                            </div>
                            <div class="col-xl-4">
                                <img style="width: 230px; border-radius: 20px;" src="img/videocall.jpg" />
                                <h5>Vươn tay kéo gần mọi khoảng cách</h5>
                                <p>
                                    Người dùng chọn được chuyên gia tư vấn
                    <br />
                                    qua cuộc gọi trực tuyến
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="noidung2">
                        <div class="row">
                            <div class="col-xl-12">
                                <p style="font-size: 60px; font-weight: bold; margin-top: 20px; background: linear-gradient(to right, #dd3e54, #6be585); -webkit-background-clip: text; color: transparent;">Những gương mặt tiêu biểu</p>
                                <p style="font-size: 25px; font-weight: bold; color: #9e9e9e;">Là những tiến sĩ, bác sĩ có chuyên môn cao cùng sự tận tụy chăm sóc cùng người bệnh</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="col">
                                    <img src="img/bs_minhhung.png" />
                                </div>
                                <div class="col">
                                    <h5>TS.BS ĐỖ MINH HÙNG</h5>
                                    <p>Giám đốc trung tâm Trung tâm Nội soi và Phẫu thuật nội soi tiêu hóa</p>
                                    <p>Bệnh viện Đa khoa Banana TP.ĐN</p>
                                    <p>TS.BS Đỗ Minh Hùng đã có gần 30 năm kinh nghiệm chẩn đoán và điều trị các bệnh lý tiêu hóa, đặc biệt là chuyên ngành Tiêu hóa - Gan mật. Với hàng chục năm làm việc tại các bệnh viện lớn như Bệnh viện Bình Dân, Bệnh viện FV, Bệnh viện AIH, Bệnh viện...</p>
                                </div>
                            </div>
                            <div class="col">
                                <div class="col">
                                    <img src="img/bs_ngocphuong.png" />
                                </div>
                                <div class="col">
                                    <h5>TS.BS CAM NGỌC PHƯỢNG</h5>
                                    <p>Giám đốc Trung tâm Sơ sinh Và Phục Hồi Sức Khỏe Sinh Sản</p>
                                    <p>Bệnh viện Đa khoa Banana TP.ĐN</p>
                                    <p>Với sự kỳ diệu của y học hiện đại, bằng tình yêu thương vô bờ bến và tâm huyết cống hiến với nghề, suốt hơn 30 qua, TS.BS Cam Ngọc Phượng không nhớ mình đã dốc sức chăm sóc, ôm ấp, vỗ về, yêu thương và trao tặng cuộc sống kỳ diệu cho bao nhiêu trẻ...</p>
                                </div>
                            </div>
                            <div class="col">
                                <div class="col">
                                    <img src="img/bs_truongkhanh.png" />
                                </div>
                                <div class="col">
                                    <h5>TTƯT.TS.BS VŨ TRƯỜNG KHANH</h5>
                                    <p>Trưởng khoa Khoa Tiêu hóa - Gan mật - Tụy</p>
                                    <p>Bệnh viện Đa khoa Banana TP.ĐN</p>
                                    <p>Với gần 25 năm công tác trong ngành Y, TTƯT.TS.BS Vũ Trường Khanh được biết đến là một trong những chuyên gia hàng đầu tại Việt Nam trong lĩnh vực Tiêu hóa – Gan Mật – Tụy.</p>
                                </div>
                            </div>
                            <div class="col">
                                <div class="col">
                                    <img src="img/bs_phamnguyenvinh.png" />
                                </div>
                                <div class="col">
                                    <h5>PGS.TS.BS PHẠM NGUYỄN VINH</h5>
                                    <p>Giám đốc Trung tâm Sản Phụ khoa</p>
                                    <p>Bệnh viện Đa khoa Banana TP.ĐN</p>
                                    <p>PGS.TS.BS Phạm Nguyễn Vinh là một trong những chuyên gia đầu ngành trong lĩnh vực Nội tim mạch tại Việt Nam. Sau khi tốt nghiệp Bác sĩ Y khoa chuyên khoa Tim mạch trường Đại học Y khoa Sài Gòn, bác sĩ Phạm Nguyễn Vinh sang Pháp tu nghiệp trong hai...</p>
                                </div>
                            </div>
                            <div class="col">
                                <div class="col">
                                    <img src="img/bs_nguyenbamynhi.png" />
                                </div>
                                <div class="col">
                                    <h5>TS.BS NGUYỄN BÁ Ý NHI</h5>
                                    <p>Giám đốc Trung tâm Gây mê hồi sức, Điều trị Đau và Chăm sóc giảm nhẹ</p>
                                    <p>Bệnh viện Đa khoa Banana TP.ĐN</p>
                                    <p>Bác sĩ Nguyễn Bá Mỹ Nhi - nguyên Phó Giám đốc phụ trách chuyên môn Bệnh viện Phụ sản Từ Dũ, Giám đốc chuyên môn Trung tâm Sản Phụ khoa - Bệnh viện Đa khoa Tâm Anh TP.Hồ Chí Minh là một trong những chuyên gia hàng đầu trong lĩnh vực gây mê hồi sức với...</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="noidung3">
                        <div class="row">
                            <h2 style="margin-bottom: 40px; background: linear-gradient(to right,#3ab5b0, #3d99be,#56317a); font-weight: bold; -webkit-background-clip: text; color: transparent;">CAM KẾT CỦA CHÚNG TÔI</h2>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-4">
                                <img style="width: 400px;" src="img/camketbacsi.png" />
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <h5>CÔNG KHAI, MINH BẠCH</h5>
                                        <hr />
                                        <p>Toàn bộ chi phí thăm khám, điều trị tại phòng khám đều được niêm yết rõ ràng tại sảnh của phòng khám. Bệnh nhân có thể lựa chọn phương pháp điều trị phù hợp với tài chính của mình.</p>
                                    </div>
                                    <div class="col-sm-6">
                                        <h5>CHI PHÍ ĐÚNG QUY ĐỊNH</h5>
                                        <hr />
                                        <p>Chi phí của các dịch vụ thăm khám và điều trị của phòng khám đều nằm trong khung quy định cho phép của Bộ Y Tế. Thực hiện thanh toán bảo hiểm y tế theo đúng quy định.</p>
                                    </div>
                                    <div class="col-sm-6">
                                        <h5>NHIỀU CHƯƠNG TRÌNH ƯU ĐÃI</h5>
                                        <hr />
                                        <p>Phòng khám thường xuyên triển khai các chương trình ưu đãi, giúp người bệnh được tiếp cận và trải nghiệm dịch vụ chuẩn quốc tế với chi phí ưu đãi nhất.</p>
                                    </div>
                                    <div class="col-sm-6">
                                        <h5>NÓI KHÔNG VỚI TIÊU CỰC</h5>
                                        <hr />
                                        <p>Phòng khám cam kết không thu thêm chi phí nào ngoài những thỏa thuận ban đầu. Thái độ của nhân viên y tế nhiệt tình, thân thiện với người bệnh.</p>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="danhgia">
                    <div class="ratio ratio-16x9 video_danhgia">
                        <video class="embed-responsive-item" autoplay loop muted>
                            <source src="img/videobg_2.mp4" type="video/mp4">
                        </video>

                    </div>
                    <div id="testimonialCarousel" class="carousel slide" data-bs-ride="carousel">
                        <h3 class="tieude_danhgia">ĐÁNH GIÁ CỦA KHÁCH HÀNG</h3>
                        <div style="margin-top: 30px;" class="carousel-inner">
                            <!-- Slide đầu tiên -->
                            <div class="carousel-item active">
                                <div class="row">
                                    <!-- Testimonial 1 -->
                                    <div class="col-lg-6 col-md-12 ">
                                        <div class="card khungdanhgia">
                                            <div class="binhluan">
                                                <p style="text-align: justify;">
                                                    "Đặt lịch khám bên này rất gọn, có ngày giờ cụ thể luôn lên là được khám liền không rườm rà gì mấy. An tâm đặt cho gia đình, không mất thời gian."
           
                                                </p>
                                            </div>
                                            <div class="hoten">
                                                <img style="margin-top: -30px" src="img/anhtuan.jpg" />
                                                <p style="padding: 10px 10px 20px 10px"><b>Anh Tuấn</b></p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Testimonial 2 -->
                                    <div class="col-lg-6 col-md-12 d-none d-lg-block">
                                        <div class="card khungdanhgia">
                                            <div class="binhluan">
                                                <p style="text-align: justify;">
                                                    "Dịch vụ tư vấn bác sĩ qua video tiện thật. Mình bị stress, không muốn gặp đám đông nên tìm hiểu tư vấn từ xa với bác sĩ tâm lý ở đây."
           
                                                </p>
                                            </div>
                                            <div class="hoten">
                                                <img style="margin-top: -30px" src="img/anhtuan.jpg" />
                                                <p style="padding: 10px 10px 20px 10px"><b>Mai Vy</b></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Slide thứ hai -->
                            <div class="carousel-item">
                                <div class="row">
                                    <!-- Testimonial 3 -->
                                    <div class="col-lg-6 col-md-12">
                                        <div class="card khungdanhgia">
                                            <div class="binhluan">
                                                <p style="text-align: justify;">
                                                    "Tôi ở tỉnh nên đi khám trong thành phố rất mất thời gian, Nhờ Banana mà tôi kết nối được với bác sĩ giỏi để tư vấn khám 
                                                    qua video cho con. Tôi thấy dịch vụ này rất hay, nên được nhiều người biết hơn."
           
                                                </p>
                                            </div>
                                            <div class="hoten">
                                                <img style="margin-top: -30px" src="img/anhtuan.jpg" />
                                                <p style="padding: 10px 10px 20px 10px"><b>Mộc Trà</b></p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Testimonial 4 -->
                                    <div class="col-lg-6 col-md-12 d-none d-lg-block">
                                        <div class="card khungdanhgia">
                                            <div class="binhluan">
                                                <p style="text-align: justify;">
                                                    "Bên Banana này mình thấy ok về khâu tư vấn này. Đợt rồi mình đặt bị nhầm lịch gọi lên có bạn hỗ trợ tư vấn rất nhiệt tình. Tầm trung niên như mình xài 
                                                    mấy cái công nghệ hay nhầm lẫn, may sao bên đây hỗ trợ đổi lại."
           
                                                </p>
                                            </div>
                                            <div class="hoten">
                                                <img style="margin-top: -30px" src="img/anhtuan.jpg" />
                                                <p style="padding: 10px 10px 20px 10px"><b>Phương Vy</b></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Nút điều khiển -->
                        <button class="carousel-control-prev" type="button" data-bs-target="#testimonialCarousel" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#testimonialCarousel" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>
                </div>
                <div class="lienket">
                    <h2 style="margin-bottom: 40px; background: linear-gradient(to right,#f12711, #f5af19); font-weight: bold; -webkit-background-clip: text; color: transparent;">CÁC ĐƠN VỊ LIÊN KẾT</h2>
                    <div class="thongtin_lienket">
                        <div class="row">
                            <span class="col-6 col-xl-3 mt-4">
                                <img src="img/BV_TWHUE.jpg" />
                            </span>
                            <span class=" col-6 col-xl-3 mt-4">
                                <img src="img/BV_XanhPon.png" />
                            </span>
                            <span class=" col-6 col-xl-3 mt-4">
                                <img src="img/BV_QuocTeBD.jpg" />
                            </span>
                            <span class=" col-6 col-xl-3 mt-4">
                                <img src="img/BV_DaKhoaHaNoi.jpg" />
                            </span>
                            <span class=" col-6 col-xl-3 mt-4">
                                <img src="img/BV_19_8.png" />
                            </span>
                            <span class="col-6 col-xl-3 mt-4">
                                <img src="img/BV_CoTruyenHN.png" />
                            </span>
                            <span class="col-6 col-xl-3 mt-4">
                                <img src="img/BV_HoanMy.jpg" />
                            </span>
                            <span class="col-6 col-xl-3 mt-4">
                                <img src="img/BV_119.png" />
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var chucnangchinh = document.querySelector('.noidungchinh');

            function onScroll() {
                var windowHeight = window.innerHeight;
                var elementTop = chucnangchinh.getBoundingClientRect().top;

                if (elementTop <= windowHeight) {
                    chucnangchinh.classList.add('visible');
                    window.removeEventListener('scroll', onScroll);
                }
            }
            window.addEventListener('scroll', onScroll);
        });
    </script>
</asp:Content>
