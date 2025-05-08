using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHOM20_DATN.test
{
    public partial class TestDangBai : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Khôi phục lại các TextBox đã thêm
                RestoreTextBoxes();
            }
        }

        protected void btnAddTextBox_Click(object sender, EventArgs e)
        {
            ViewState["CountTextBox"] = "";
            // Tạo TextBox mới
            TextBox newTextBox = new TextBox();
            newTextBox.ID = "textbox";
            newTextBox.TextMode = TextBoxMode.MultiLine;
           
            PanelTextBoxes.Controls.Add(newTextBox);
        }

        private void RestoreTextBoxes()
        {
            // Khôi phục lại các TextBox cũ
            for (int i = 0; i < PanelTextBoxes.Controls.Count; i++)
            {
                TextBox textBox = new TextBox
                {
                    ID = "TextBox" + i,
                    CssClass = "form-control",
      
                };
                PanelTextBoxes.Controls.Add(textBox);
            }
        }
    }
}