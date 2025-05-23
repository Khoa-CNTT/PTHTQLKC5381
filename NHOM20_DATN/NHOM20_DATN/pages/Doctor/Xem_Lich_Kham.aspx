<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Doctor_MasterPage.Master" AutoEventWireup="true" CodeBehind="Xem_Lich_Kham.aspx.cs" Inherits="NHOM20_DATN.Xem_Lich_Kham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../style/doctor/doctor_appointment.css" rel='stylesheet'>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
<%--<asp:HiddenField runat="server" ID="lastSortDirection" />
<asp:HiddenField runat="server" ID="lastSortExpression" />--%>
<div id="list_here">

    <div class="d_flex">
        <div class="seach_bar">
            <asp:TextBox ID="txt_searching" placeholder="Tìm Kiếm" runat="server"></asp:TextBox>
            <asp:LinkButton ID="btn_search" CssClass="btn_search" OnClick="btn_search_Click" runat="server">
                <i class="fa-solid fa-magnifying-glass"></i> 
            </asp:LinkButton>
        </div>
        <div class="filter_bar">
            <asp:DropDownList OnSelectedIndexChanged="ddl_specialty_selectedindexchanged"
                CssClass="pd_tb_2px ddl_filter" ID="filter_specialty" runat="server"
                AutoPostBack="true" >
                <asp:ListItem Text="Trạng Thái" Value=""></asp:ListItem>
                <asp:ListItem Text="Đã Khám" Value="DaKham"></asp:ListItem>
                <asp:ListItem Text="Đã Đăng Ký" Value="DaDangKy"></asp:ListItem>
                <asp:ListItem Text="Đã Hủy" Value="DaHuy"></asp:ListItem>
            </asp:DropDownList>

        </div>
    </div>
        

    <%-- List --%>
    <div class="table_doctor">
        <asp:GridView ID="gridAppointment" runat="server" AutoGenerateColumns="False" CellPadding="4"
             OnRowCommand="gridAppointment_RowCommand"
            CssClass="gridview-table" ForeColor="#333333" GridLines="None"
             OnPageIndexChanging="gridAppointment_PageIndexChanging" AllowPaging="true" PageSize="5">
            <Columns>

                <%-- ==============     ID   =============== --%>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbl_IDPhieu" title='<%#Eval("IDPhieu") %>' runat="server" Text='<%#Eval("IDPhieu") %>'></asp:Label><br />
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- ==============     NGÀY GIỜ   =============== --%>
                <asp:TemplateField HeaderText="Ngày/Giờ">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Ngay" title='<%#(DateTime.Parse(Eval("NgayKham").ToString())).ToString("dd/MM/yyyy") %>' runat="server" Text='<%#(DateTime.Parse(Eval("NgayKham").ToString())).ToString("dd/MM/yyyy") %>'></asp:Label><br />
                        <asp:Label ID="lbl_ThoiGian" title='<%#(DateTime.Parse(Eval("ThoiGianKham").ToString())).ToString("HH:mm") %>' runat="server" Text='<%#(DateTime.Parse(Eval("ThoiGianKham").ToString())).ToString("HH:mm")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- ==============     Họ tên    =============== --%>
                <asp:TemplateField HeaderText="Họ tên">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Hoten" title='<%#Eval("Hoten") %>' runat="server" Text='<%#Eval("Hoten") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- ==============     SĐT    =============== --%>
                <asp:TemplateField HeaderText="SĐT">
                    <ItemTemplate>
                        <asp:Label ID="lbl_SDT" title='<%#Eval("SoDienThoai") %>' runat="server" Text='<%#Eval("SoDienThoai") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- ==============     Trạng thái    =============== --%>
                <asp:TemplateField HeaderText="Trạng Thái">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DaDangKy"
                            CssClass="lblDadangky"
                            title="Đã Đăng Ký"
                            BackColor="#58abed"
                            ForeColor="White"
                            runat="server"
                            Text="Đã Đăng Ký"
                            Visible='<%# Eval("TrangThai").ToString() == "DaDangKy" %>'>
                        </asp:Label>
                        <asp:Label ID="lbl_ChoKham"
                            CssClass="lblDangcho"
                            title="Đã Khám"
                            BackColor="#ff9200"
                            ForeColor="White"
                            runat="server"
                            Text="Đã Khám"
                            Visible='<%# Eval("TrangThai").ToString() == "DaKham" %>'>
                    </asp:Label>
                        <asp:Label ID="lbl_DaHuy"
                            CssClass="lblDahuy"
                            BackColor="#ff3e3e"
                            ForeColor="White"
                            title="Đã hủy"
                            runat="server"
                            Text="Đã hủy"
                            Visible='<%# Eval("TrangThai").ToString() == "DaHuy" %>'>
                     </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Đổi giờ --%>
               <asp:TemplateField HeaderText="Đổi giờ">
  <ItemTemplate>
    <asp:LinkButton 
      ID="btn_ChangeTime" 
      runat="server"
      CommandName="DoiGio"
      CommandArgument='<%# Eval("NgayKham") + "," + Eval("IDPhieu") + "," + Eval("ThoiGianKham") %>'
      OnClientClick="showLoading();"
      Visible='<%# Eval("TrangThai").ToString() == "DaDangKy" %>'>
        <i class="fa-regular fa-clock"></i>
    </asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
                <%-- Xem thông tin --%>
                <asp:TemplateField HeaderText="Xem thông tin ">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_ViewDetail" CommandArgument='<%#Eval("IDPhieu") %>' CommandName="XemTT" runat="server">
                         <i class="fa-solid fa-id-card"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Xóa --%>
              <asp:TemplateField Visible="true">
    <ItemTemplate>
        <asp:LinkButton ID="btn_Delete" 
            OnClientClick='<%# "showCancelDialog(\"" + Eval("IDPhieu") + "\", \"" + Eval("NgayKham") + "\"); return false;" %>' 
            runat="server">
            <i class="fa-regular fa-trash-can"></i>
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>


            </Columns>
            <PagerStyle BackColor="" ForeColor="" HorizontalAlign="Left" CssClass="pagination" />
        </asp:GridView>

        

    </div>
    <asp:HiddenField ID="hiddenOldTime" runat="server" />
    <asp:HiddenField ID="hiddenOldDay" runat="server" />
    <asp:HiddenField ID="hiddenIdPk" runat="server" />
    <div id="chTime_container">
        <asp:Panel ID="pn_AT" runat="server" Visible="false"  CssClass="pnl_AT">
            <div class="chTime_content">

                <h2>Đổi giờ khám phiếu: <br />
                    <asp:Label ID="lbl_idPk" ForeColor="#00a2ff" runat="server" Text=""></asp:Label>

                </h2>
                <div class="wrapper_list">
                    <asp:DropDownList ID="ddl_aT"
                        runat="server"
                        onmousedown="this.size=5;"
                        onclick="this.size=5;"
                        onfocusout="this.size=1;"
                        ondblclick="this.size=1;"
                        AppendDataBoundItems="True">
                    </asp:DropDownList>


                </div>
                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" OnClientClick="showLoading();" Text="Lưu" />
                <asp:Button ID="btn_Close" runat="server" OnClick="btn_Close_Click" Text="Đóng" />

            </div>
        </asp:Panel>
    </div>
    <asp:Panel runat="server" ID="pnlPatientModal" CssClass="modal fade" aria-hidden="true" ClientIDMode="Static">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Thông tin bệnh nhân</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <dl class="row">
          <dt class="col-sm-4">Họ tên:</dt>
          <dd class="col-sm-8"><asp:Label runat="server" ID="lblModal_HoTen" /></dd>

          <dt class="col-sm-4">Ngày sinh:</dt>
          <dd class="col-sm-8"><asp:Label runat="server" ID="lblModal_NgaySinh" /></dd>

          <dt class="col-sm-4">Giới tính:</dt>
          <dd class="col-sm-8"><asp:Label runat="server" ID="lblModal_GioiTinh" /></dd>

          <dt class="col-sm-4">Số điện thoại:</dt>
          <dd class="col-sm-8"><asp:Label runat="server" ID="lblModal_SDT" /></dd>

          <dt class="col-sm-4">Địa chỉ:</dt>
          <dd class="col-sm-8"><asp:Label runat="server" ID="lblModal_DiaChi" /></dd>

          <!-- thêm các trường chi tiết khác nếu cần -->
        </dl>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Đóng</button>
      </div>
    </div>
  </div>
</asp:Panel>
</div>
<div
  id="loadingOverlay"
  style="
    position: fixed;
    inset: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    background: rgba(255,255,255,0.7);
    backdrop-filter: blur(5px);
    -webkit-backdrop-filter: blur(5px);
    z-index: 9999;
  "
>
  <div
    class="loading-content"
    style="
      background: #fff;
      padding: 1.5em 2em;
      border-radius: 0.5em;
      box-shadow: 0 0 20px rgba(0,0,0,0.2);
      text-align: center;
    "
  >
    <div
      class="spinner"
      style="
        width: 3rem;
        height: 3rem;
        margin: 0 auto 0.5em;
        border: 0.5rem solid #f3f3f3;
        border-top-color: #3498db;
        border-radius: 50%;
        animation: spin 1s linear infinite;
      "
    ></div>
    <div
      class="loading-text"
      style="
        font-weight: 600;
        color: #333;
      "
    >
      Đang xử lý...
    </div>
  </div>
</div>

      <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script>
    function showAlert(notice, warn) {
        Swal.fire({
            title: notice,
            icon: warn,
            confirmButtonText: 'OK',
            allowOutsideClick: false
        });
    }
    function showCancelDialog(idPk, dayWork) {
        Swal.fire({
            title: 'Lý do hủy lịch',
            input: 'text',
            inputPlaceholder: 'Nhập lý do hủy lịch...',
            showCancelButton: true,
            confirmButtonText: 'Tiếp tục',
            cancelButtonText: 'Hủy bỏ',
            inputValidator: (value) => {
                if (!value) return 'Vui lòng nhập lý do hủy!';
            }
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    title: 'Xác nhận hủy lịch',
                    text: "Bạn có chắc chắn muốn hủy lịch này?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Đồng ý',
                    cancelButtonText: 'Hủy bỏ'
                }).then((confirmation) => {
                    if (confirmation.isConfirmed) {
                        // Hiển thị loading
                        document.getElementById('loadingOverlay').style.display = 'flex';

                        // Gửi request
                        __doPostBack('CancelAppointment', `${idPk}|${dayWork}|${result.value}`);
                    }
                });
            }
        });
    }
    function showLoading() {
        document.getElementById('loadingOverlay').style.display = 'flex';
    }  
    // Tự động ẩn loading khi trang load xong
    document.addEventListener('DOMContentLoaded', function () {
        document.querySelector('#loadingOverlay').style.display = 'none';
    });

    // Xử lý khi có postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        document.querySelector('#loadingOverlay').style.display = 'none';
    });
    
</script>
</asp:Content>
