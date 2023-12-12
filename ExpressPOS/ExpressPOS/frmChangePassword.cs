using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmChangePassword : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmChangePassword()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text) | string.IsNullOrEmpty(txtNewPassword.Text) | string.IsNullOrEmpty(txtRepassword.Text))
            { MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (!(txtNewPassword.Text == txtRepassword.Text))
            { MessageBox.Show("Password and re-password does not match.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                clsCN.ExecuteSQLQuery(" SELECT * FROM  Users  WHERE  (USER_ID = '" + GlobalVariables.UserID + "') AND (Password = '" + txtCurrentPassword.Text + "') ");
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    clsCN.ExecuteSQLQuery("UPDATE Users SET Password = '" + txtNewPassword.Text + "'  WHERE  (USER_ID = '" + GlobalVariables.UserID + "')  ");
                    txtCurrentPassword.Text = "";
                    txtNewPassword.Text="";
                    txtRepassword.Text="";
                    MessageBox.Show("Your password has been changed successfully! Thank you.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { MessageBox.Show("Password does not match.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

    }
}
