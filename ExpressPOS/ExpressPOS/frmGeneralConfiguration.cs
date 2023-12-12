using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; 

namespace ExpressPOS
{
    public partial class frmGeneralConfiguration : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }


        public frmGeneralConfiguration()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }

        private void FormHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmGeneralConfiguration_Load(object sender, EventArgs e)
        {
            clsCN.ExecuteSQLQuery("SELECT * FROM BusinessInfo");
            if (clsCN.sqlDT.Rows.Count > 0) {
                txtBusinessName.Text = clsCN.sqlDT.Rows[0]["BusinessName"].ToString();
                txtAddress.Text = clsCN.sqlDT.Rows[0]["Address"].ToString();
                txtContactNo.Text = clsCN.sqlDT.Rows[0]["ContactNo"].ToString();
                txtEmail.Text = clsCN.sqlDT.Rows[0]["Email"].ToString();
                txtVatReg.Text = clsCN.sqlDT.Rows[0]["VatRegNo"].ToString();
                txtSlogan.Text = clsCN.sqlDT.Rows[0]["Slogan"].ToString();
                if (clsCN.sqlDT.Rows[0]["StatusBar"].ToString() == "Y") { chkStatusBar.Checked = true; }
                else   { chkStatusBar.Checked=false;  }
            }


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            string chkVAL = null;
            if (chkStatusBar.Checked)
            { chkVAL = "Y"; }
            else
            { chkVAL = "N"; }

            if (txtBusinessName.Text != "" && txtAddress.Text != "" && txtContactNo.Text != "") {
                clsCN.ExecuteSQLQuery("SELECT * FROM BusinessInfo");
                if (clsCN.sqlDT.Rows.Count > 0) {
                    clsCN.ExecuteSQLQuery("  UPDATE BusinessInfo SET BusinessName='" + txtBusinessName.Text + "', Address='" + txtAddress.Text + "', ContactNo='" + txtContactNo.Text + "', Email='" + txtEmail.Text + "', VatRegNo='" + txtVatReg.Text + "', Slogan ='" + txtSlogan.Text + "', StatusBar='"+ chkVAL +"' ");
                    MessageBox.Show("Information update Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    clsCN.ExecuteSQLQuery("INSERT INTO  BusinessInfo  ( BusinessName, Address, ContactNo, Email, VatRegNo, Slogan, StatusBar) VALUES ('" + txtBusinessName.Text + "', '" + txtAddress.Text + "', '" + txtContactNo.Text + "', '" + txtEmail.Text + "', '" + txtVatReg.Text + "', '" + txtSlogan.Text + "', '" + chkVAL + "' ) ");
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBusinessName.Focus();
                return;
            }
        }


        private ToolTip hint;

        private void chkStatusBar_MouseHover(object sender, EventArgs e)
        {
            hint = new ToolTip();
            hint.IsBalloon = true;
            hint.ToolTipTitle = "Configuration";
            hint.ToolTipIcon = ToolTipIcon.Info;
            hint.Show(string.Empty, chkStatusBar);
            hint.Show("Overall experience. (ExpressPOS restart required.)", chkStatusBar, 0, -72);
        }

        private void chkStatusBar_MouseLeave(object sender, EventArgs e)
        {
            try { hint.Dispose(); }
            catch { }
        }



    }
}
