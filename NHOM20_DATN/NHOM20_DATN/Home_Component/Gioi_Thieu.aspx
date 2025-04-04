<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Gioi_Thieu.aspx.cs" Inherits="NHOM20_DATN.Home_Component.Gioi_Thieu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <style>
       @layer library, reset, base, demo;
       @import 'https://unpkg.com/open-props@2.0.0-beta.5' layer(library);
       @import url('https://fonts.googleapis.com/css2?family=Inter:wght@300..700&display=swap');

       .content {
           margin-top: -50px;
       }

       @layer reset {

           *,
           ::before,
           ::after {
               box-sizing: border-box;
           }

           :where(:not(dialog)) {
               margin: 0;
           }
       }

       @layer base {
           html {
               --nav-block-size: 74px;
               --brand-gradient: linear-gradient(227deg, #1400c7 0%, #00bbff 100%);

               @media (prefers-reduced-motion: no-preference) {
                   scroll-behavior: smooth;
               }
           }

           body {
               font-family: 'Inter', sans-serif;
               min-block-size: 100dvb;
           }
       }

       @layer demo {
           .navbar {
               min-block-size: var(--nav-block-size);
               display: grid;
               position: fixed;
               inset-block-start: 0;
               inset-inline: 0;
               background-color: white;
               z-index: var(--layer-3);
               place-items: center end;
               padding-block: var(--size-3);
               padding-inline: var(--size-5);

               @media (width >=600px) {
                   padding-inline:6dvi;
               }
           }

           .nav-cta-btn {
               font-size: var(--font-size-1);
               font-weight: var(--font-weight-7);
               inline-size: max-content;
               white-space: nowrap;
               text-decoration: none;
               padding-block: var(--size-3);
               padding-inline: var(--size-7);
               border: var(--border-size-2) solid transparent;
               color: black;
               position: relative;
               isolation: isolate;
               background: linear-gradient(white, white) padding-box, var(--brand-gradient) border-box;
               border-radius: var(--radius-6);
               overflow: hidden;
               &::before

       {
           content: '';
           display: block;
           position: absolute;
           inset: 0;
           background: var(--brand-gradient);
           mix-blend-mode: screen;
       }

       &:hover {
           color: white;
           &::before

       {
           background: var(--brand-gradient) padding-box, var(--brand-gradient) border-box;
           background-repeat: no-repeat;
           background-size: contain;
           mix-blend-mode: normal;
           z-index: -1;
       }

       }
       }

       .section {
           --section-block-size: max(400px, 100dvb);
           display: grid;
           background-color: white;
           min-block-size: var(--section-block-size);
           position: relative;
           block-size: 100%;
           display: grid;
           >*

       {
           grid-area: 1/1;
       }

       }

       .section-wrapper {
           position: relative;
           display: grid;

           @media (width >=960px) {
               grid-template-columns: 1fr 1fr;
           }

           @media (width < 960px) {
               padding-block-start: calc(var(--nav-block-size) + var(--size-7));
               padding-block-end: var(--size-7);
           }
       }

       .video {
           display: block;
           inline-size: 100%;
           block-size: 100%;
           object-fit: cover;
           position: relative;
           z-index: -1;
       }

       .content-wrapper {
           display: grid;

           @media (width < 960px) {
               gap: var(--size-7);
           }
       }

       .meta {
           display: grid;
           gap: var(--size-3);
       }

       .content {
           display: grid;
           inline-size: 100%;
           place-items: center;
           padding-block: var(--size-7);
           padding-inline: var(--size-5);

           @media (width >=960px) {
               padding: var(--size-10);
               min-block-size: 100cqb;
               place-items: center end;
           }

           @media (width < 960px) {
               gap: var(--size-5);
           }
       }

       .mobile-visual {
           inline-size: 100%;
           aspect-ratio: var(--ratio-square);

           @media (width >=960px) {
               display: none;
           }
       }

       .headline {
           font-size: var(--font-size-7);
           font-weight: var(--font-weight-4);
           max-inline-size: var(--size-content-1);
           text-wrap: pretty;

           @media (width < 960px) {
               font-size: var(--font-size-6);
           }
       }

       .desc {
           font-size: var(--font-size-4);
           line-height: 1.5;
           max-inline-size: 40ch;
           text-wrap: pretty;

           @media (width < 960px) {
               font-size: var(--font-size-3);
           }
       }

       .visual {
           display: grid;
           position: sticky;
           block-size: var(--section-block-size);
           inset-block-start: 0;
           container-type: size;

           @media (width < 960px) {
               display: none;
           }
       }

       .video-visual {
           inline-size: 100%;
           block-size: var(--section-block-size);
           display: block;
           position: sticky;
           inset-block-start: 0;
           isolation: isolate;
           filter: hue-rotate(210deg);
       }

       .card-wrapper {
           container-type: size;
           display: grid;
           place-items: center;
           overflow: clip;
           >*

       {
           grid-area: 1/1;
       }

       }

       .card {
           aspect-ratio: var(--ratio-square);
           inline-size: 70cqi;
           border-radius: var(--radius-3);
           scale: 0.4;
       }

       .card-img {
           display: block;
           inline-size: 100%;
           block-size: 100%;
           object-fit: cover;
       }

       .card-1 {
           scale: 1;
       }

       .card-2 {
           translate: -35cqi 30cqb;
           opacity: 0.3;
       }

       .card-3 {
           translate: 0cqi 50cqb;
           opacity: 0.5;
       }

       .card-4 {
           translate: 45cqi 40cqb;
           opacity: 0.5;
       }

       .content-1 {
           --_text-gradient: linear-gradient(227deg, #1400c7 0%, #00bbff 100%);
       }

       .content-2 {
           --_text-gradient: linear-gradient(227deg, #28dc28 0%, #00bbff 100%);
       }

       .content-3 {
           --_text-gradient: linear-gradient(227deg, #1400c7 0%, #b800b1 100%);
       }

       .content-4 {
           --_text-gradient: linear-gradient(227deg, #b800b1 0%, #f50000 100%);
       }

       .text-highlight {
           background: var(--_text-gradient);
           -webkit-background-clip: text;
           -webkit-text-fill-color: transparent;
           background-clip: text;
           text-fill-color: transparent;
       }

       }

       @supports (animation-timeline: scroll()) {
           body {
               timeline-scope: --content-1, --content-2, --content-3, --content-4;
           }

           .section {
               view-timeline-name: --section;
           }

           .content-1 {
               view-timeline-name: --content-1;
           }

           .content-2 {
               view-timeline-name: --content-2;
           }

           .content-3 {
               view-timeline-name: --content-3;
           }

           .content-4 {
               view-timeline-name: --content-4;
           }

           .card {
               animation-timing-function: linear;
               animation-fill-mode: forwards;
           }

           .card-1 {
               animation-timeline: --content-1;
               animation-name: slide-up-first-card;
           }

           .card-2 {
               animation-timeline: --content-2;
               animation-name: slide-up-card;
           }

           .card-3 {
               animation-timeline: --content-3;
               animation-name: slide-up-card;
           }

           .card-4 {
               animation-timeline: --content-4;
               animation-name: slide-up-card;
           }

           .video-visual {
               animation-timeline: --section;
               animation-range-end: exit 110%;
               animation-name: update-hue;
               animation-timing-function: step-end;
               animation-fill-mode: forwards;
           }

           @keyframes update-hue {
               0% {
                   filter: hue-rotate(210deg);
               }

               25% {
                   filter: hue-rotate(150deg);
               }

               45% {
                   filter: hue-rotate(300deg);
               }

               60% {
                   filter: hue-rotate(4deg);
               }
           }

           @keyframes slide-up-first-card {
               50% {
                   translate: 0;
                   opacity: 1;
               }

               90% {
                   translate: 0 -50cqi;
                   scale: 0.6;
               }

               100% {
                   translate: 0 -100cqi;
                   opacity: 0;
               }
           }

           @keyframes slide-up-card {
               50% {
                   opacity: 1;
                   translate: 0;
                   scale: 1;
               }

               90% {
                   opacity: 0.5;
                   scale: 0.6;
                   translate: 0 -50cqb;
               }

               100% {
                   translate: 0 -100cqi;
                   opacity: 0;
               }
           }
       }
   </style>
   <div class="content">
       <div class="section">
           <div class="video-visual">
               <video class="video" autoplay loop muted poster="" role="none" aria-label="background gradient animation">
                   <source
                       src="https://raw.githubusercontent.com/mobalti/open-props-interfaces/main/dynamic-content-lockups-v2/assets/bg-gradient-animation.mp4"
                       type="video/mp4" />
               </video>
           </div>
           <div class="section-wrapper">
               <div class="content-wrapper">
                   <div class="content content-1">
                       <div class="mobile-visual">
                           <img class="card-img"
                               src="img/benhvien.jpg" />
                       </div>
                       <div class="meta">
                           <h2 class="headline">Giới thiệu chung<span class="text-highlight"><br />
                               Bệnh viện Banana</span>
                           </h2>
                           <p class="desc">
                               Với không gian xanh, yên tĩnh, thoáng mát và an ninh tại trung tâm thành phố Đà Nẵng, 
                              Bệnh viện Banana được đánh giá là một địa điểm lý tưởng phục vụ cho công tác
                              khám, điều trị và nghĩ dưỡng của người bệnh. Đặc biệt, Bệnh viện Banana là sự lựa chọn
                              hàng đầu của các mẹ bầu để đón bé yêu chào đời.

                              Bệnh viện Banana với đội ngũ bác sĩ có kinh nghiệm và bằng cấp cao, có nhiều kinh
                              nghiệm qua thời gian học tập và làm việc tại các bệnh viện trong và ngoài nước, trang
                              thiết bị hiện đại, cùng các phác đồ điều trị hiệu quả, khoa học mang đến dịch vụ khám,
                              điều trị, chăm sóc sức khỏe cao cấp, toàn diện với chi phí hợp lý. Hơn thế nữa, bệnh
                              viện còn áp dụng tiêu chuẩn JCI của Mỹ lấy bệnh nhân làm trung tâm, tập trung đặc
                              biệt vào sự an toàn, chăm sóc y tế trong quản lý và vận hành để nâng cao chất lượng
                              dịch vụ của mình.
                           </p>
                       </div>
                   </div>
                   <div class="content content-2">
                       <div class="mobile-visual">
                           <img class="card-img"
                               src="img/sumenh.jpeg" />
                       </div>
                       <div class="meta">
                           <h2 class="headline">Tầm nhìn - 
                          <span class="text-highlight">Sứ mệnh</span>
                           </h2>
                           <p class="desc">
                               Mục tiêu của Bệnh viện Banana là trở thành địa chỉ khám chữa bệnh tin cậy cho người dân 
                              trong nước nói chung, và các tỉnh phía Trung Trung Bộ nói riêng. Chúng 
                              tôi chú trọng vào việc đầu tư trang bị y tế hiện đại tiêu chuẩn quốc tế, kết hợp với kiến thức của đội 
                              ngũ bác sĩ giỏi nhằm đưa đến kết quả khám bệnh chính xác nhất và điều trị bệnh tốt nhất cho người dân. 
                              Đồng thời, tạo một môi trường y tế an toàn, thân thiện, chất lượng cao hơn cho tất cả mọi người.
                               <br />
                               <br />
                               Bệnh viện cam kết vận dụng các thành tựu khoa học, không ngừng đào tạo, nghiên cứu và cập nhật các phương pháp 
                              điều trị để mang đến các dịch vụ chăm sóc y tế chất lượng, chăm sóc người bệnh chuẩn mực và đẳng cấp
                           </p>
                       </div>
                   </div>
                   <div class="content content-3">
                       <div class="mobile-visual">
                           <img class="card-img"
                               src="img/dientich.jpeg" />
                       </div>
                       <div class="meta">
                           <h2 class="headline">Quy mô
                          <span class="text-highlight">Tổng diện tích</span>
                           </h2>
                           <p class="desc">
                               Bệnh viện Banana được xây dựng trên diện tích 12,76 ha tại phường Hòa Minh, quận Liên Chiểu, TP Đà nẵng. Chính 
                              thức đưa vào hoạt động từ ngày 30/12/2018, BIH với qui mô 1.200 giường cung cấp dịch vụ khám bệnh, 
                              chữa bệnh đa khoa.
                           </p>
                       </div>
                   </div>
                   <div class="content content-4">
                       <div class="mobile-visual">
                           <img class="card-img"
                               src="img/thanhtuu.jpg" />
                       </div>
                       <div class="meta">
                           <h2 class="headline">Thành tựu hơn <span class="text-highlight">3 năm hoạt động</span>
                           </h2>
                           <p class="desc">
                               Hơn 6 năm hoạt động, bệnh viện đã khám và điều trị ngoại trú cho hơn 300.000 lượt bệnh nhân,
                              trong đó có hơn 50.000 lượt điều trị nội trú. Và khám sức khỏe định kỳ cho người lao động ở hơn
                              100 cơ quan, doanh nghiệp trong và ngoài tỉnh.
                              <br />
                               <br />
                               <b>Đặc biệt khoa Ngoại đã phẩu thuật thành công các ca mổ khó như: Cắt dạ dày, đại tràng do u hay
                              ung thư,...thay khớp háng toàn phần và bán phần dù chỉ mới bước đầu triển khai nhưng đã thành công 100%.</b>
                           </p>
                       </div>
                   </div>
               </div>
               <div class="visual">
                   <div class="card-wrapper">
                       <div class="card card-1">
                           <img class="card-img"
                               src="img/benhvien.jpg"
                               alt="Fantasy warrior  - Video game character" />
                       </div>
                       <div class="card card-2">
                           <img class="card-img"
                               src="img/sumenh.jpeg"
                               alt="Green haired ninja in armor - Video game character" />
                       </div>
                       <div class="card card-3">
                           <img class="card-img"
                               src="img/dientich.jpeg"
                               alt="Female warrior in armor - Video game character" />
                       </div>
                       <div class="card card-4">
                           <img class="card-img"
                               src="img/thanhtuu.jpg"
                               alt="Agile warrior - Video game character" />
                       </div>
                   </div>
               </div>
           </div>
       </div>
   </div>
</asp:Content>
