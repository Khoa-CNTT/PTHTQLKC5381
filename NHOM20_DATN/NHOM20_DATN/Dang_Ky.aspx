<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dang_Ky.aspx.cs" Inherits="NHOM20_DATN.Dang_Ky" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        
#content_container .regist_container {
    text-align: center;
    width: 100%;
    height: 38em;
    background-image: url('/img/background_login.jpg');
    background-size: cover;
    background-position: center;
    padding: 1.3em 1em;
    background-repeat: no-repeat;
    position: relative;
    overflow-x: visible;
}

#content_container .container_alert {
    background-color: rgba(181, 227, 227, 0.773);
    padding: 0.5em 3em;
    display: flex;
    justify-content: space-between;
}

    #content_container .container_alert i {
        margin: 0 15px;
    }

    #content_container .container_alert a {
        text-decoration: none;
        font-weight: 600;
        color: #3b3a3a;
    }

#content_container .regist_container .regist_form {
    background-color: rgba(245, 250, 255, 0.986);
    position: absolute;
    width: 32%;
    max-width: 600px;
    border-radius: 1em;
    display: flex;
    flex-flow: column;
    align-items: center;
    left: 0;
    right: 0;
    margin: auto;
    margin-top: 1em;
}


#content_container .regist_form h2 {
    color: rgb(59, 194, 252);
}

#content_container .regist_form .inp_mxn {
    display: flex;
    justify-content: space-between;
    flex-wrap: wrap;
}


#content_container .regist_form .inp_mdn,
.inp_mk,
.inp_mxn {
    margin: 1em 0;
    width: 80%;
}

#content_container .regist_form h2 {
    margin: 1em 0;
}

#content_container .regist_form input {
    width: 100%;
    padding: 0.5em 1em;
    border-radius: 1.5em;
    border: 1px solid #ccc;
    background-color: #e5e5e582;
}

#content_container .regist_form .inp_mxn input {
    width: 50%;
}

#content_container .regist_form .inp_mxn span {
    padding: 1em 2em;
    background-color: rgb(59, 194, 252);
}




#content_container .inp_submit .inp_btn input {
    padding: 1em 2em;
    background-color: rgb(87, 205, 255);
    border: none;
    border-radius: 2em;
    cursor: pointer;
    color: aliceblue;
    font-weight: 500;
}

    #content_container .inp_submit .inp_btn input:hover {
        background-color: rgb(14, 181, 253);
    }

#content_container .regist_form .inp_submit {
    padding: 1em;
}

#content_container .regist_form .inp_mxn,
.inp_btn p,
button {
    font-weight: 600;
    color: aliceblue;
}

#content_container .regist_form .inp_regis {
    margin-bottom: 2em;
}


#content_container .regist_form .inp_sdt {
    width: 60%;
    margin: 1em;
}

#content_container .d_flex {
    display: flex;
    align-items: center;
    gap: 2em;
}

.mg_tb_1em {
    margin: 1em 0;
}

.mg_left_1em {
    margin-left: 1em;
}

.mg_right_2em {
    margin-right: 2em;
}

.width_50per {
    width: 50%;
}

#content_container .inp_gender input {
    width: 3em;
    height: 2em;
}

#content_container .inp_gender p {
    width: 50%;
}


/* remove up/down number btn */
input[type=number]::-webkit-inner-spin-button,
input[type=number]::-webkit-outer-spin-button {
    -webkit-appearance: none;
    margin: 0;
}


@media screen and (min-device-width: 1380px) {
    #content_container .regist_form .date_inp {
        padding: 1.3em 1em;
    }
}

@media screen and (max-width: 1380px) {

    #content_container .regist_form .inp_mxn {
        display: block;
    }
}

#content_container .inp_mxn span {
    width: 100%;
}

}

@media screen and (max-width: 1100px) {


    #content_container .regist_container .regist_form {
        width: 81%;
    }
}

@media screen and (max-width: 900px) {
    #content_container .regist_container .regist_form {
        width: 81%;
    }
}


@media screen and (max-device-width: 768px) {
    #content_container .regist_container {
        height: 34em;
    }

    #content_container .container_alert {
        padding: 1.5em 1em;
    }

    #content_container .regist_form input {
        padding: 0.5em 1em;
    }

    #content_container .d_flex

    #content_container .regist_form .inp_mxn input {
        padding: 1.3em 0;
        padding-left: 1em;
    }

    #content_container .regist_form .inp_sdt {
        display: block;
        margin: 0;
    }

        #content_container .regist_form .inp_sdt input {
            margin-bottom: 1em;
        }

    #content_container .regist_form .inp_mk {
        margin: 0;
        margin-bottom: 1em;
    }

    #content_container .inp_gender {
        align-items: unset;
    }
}

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div id="content_container">
      <div class="regist_container">
          <div class="regist_form">
              <h2>ĐĂNG KÝ</h2>
              <div class="inp_mk">
                  <asp:TextBox ID="txtUsername" placeholder="Tên đăng nhập" runat="server"></asp:TextBox>
              </div>
              <asp:RequiredFieldValidator ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server">Không thể để trống</asp:RequiredFieldValidator>

              <div class="inp_mk">
                  <asp:TextBox ID="txtEmail" placeholder="Email" runat="server"></asp:TextBox>
              </div>
              <asp:RequiredFieldValidator ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server" ForeColor="Red">Không thể để trống</asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" Display="Dynamic" runat="server" ForeColor="Red"
                  ErrorMessage="Địa chỉ email không hợp lệ"
                  ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$">
Địa chỉ email không hợp lệ
              </asp:RegularExpressionValidator>



              <div class="inp_mk">
                  <asp:TextBox ID="txtPassword" placeholder="Mật khẩu" TextMode="Password" runat="server"></asp:TextBox>
              </div>
              <asp:RegularExpressionValidator ControlToValidate="txtPassword" runat="server" ErrorMessage="Mật khẩu phải có ít nhất 10 kí tự, bao gồm 1 chữ cái in hoa và 1 ký tự đặc biệt" Display="Dynamic" ValidationExpression="^(?=.*[A-Z])(?=.*[\W]).{10,}$" ForeColor="Red"></asp:RegularExpressionValidator>
              <asp:RequiredFieldValidator ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server" ForeColor="Red">Không thể để trống</asp:RequiredFieldValidator>
              <div class="inp_mk">
                  <asp:TextBox ID="txtReEnterPassword" placeholder="Nhập lại mật khẩu" TextMode="Password" runat="server"></asp:TextBox>
              </div>
              <asp:RequiredFieldValidator ControlToValidate="txtReEnterPassword" Display="Dynamic" ErrorMessage="Không thể để trống" runat="server" ForeColor="Red">Không thể để trống</asp:RequiredFieldValidator>

              <asp:CompareValidator ControlToValidate="txtReEnterPassword" ControlToCompare="txtPassword" ErrorMessage="Không thể để trống" Display="Dynamic" runat="server" ForeColor="Red">Mật khẩu không khớp vui lòng nhập lại</asp:CompareValidator>
              <asp:RegularExpressionValidator ControlToValidate="txtReEnterPassword" runat="server" ErrorMessage="Mật khẩu phải có ít nhất 10 kí tự, bao gồm 1 chữ cái in hoa và 1 ký tự đặc biệt" Display="Dynamic" ValidationExpression="^(?=.*[A-Z])(?=.*[\W]).{10,}$" ForeColor="Red"></asp:RegularExpressionValidator>

              <div class="inp_submit">
                  <div class="inp_btn">
                      <asp:Button ID="Button1" runat="server" Text="ĐĂNG KÝ" OnClick="btnRegister_Click" />
                  </div>
              </div>
              <div class="inp_regis">
                  <a href="Dang_Nhap.aspx">Đăng nhập</a> nếu có tài khoản 
         
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
