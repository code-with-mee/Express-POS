using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace ExpressPOS
{
    public partial class frmAuthentication : Form
    {

        clsConnectionNode clsCN = new clsConnectionNode() ;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg, int wParam, int lParam);
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

        public frmAuthentication()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
            
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Environment.Exit(0);
        }

        private void FormHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnMinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
                
        }

        private void frmAuthentication_Load(object sender, EventArgs e)
        {
            lblWaring.Visible = false ;
            clsCN.ExecuteSQLQuery(" SELECT   *  FROM    Users  ");
            if (clsCN.sqlDT.Rows.Count > 0) { }
            else {
                frmManageUsers frmManageUsers = new frmManageUsers();
                frmManageUsers.ShowDialog();
            }
            txtUserName.Select();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                clsCN.ExecuteSQLQuery(" SELECT   *  FROM    Users   WHERE    (UserName = '" + txtUserName.Text + "') AND (Password = '" + txtPassword.Text + "') ");
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    GlobalVariables.UserID = clsCN.sqlDT.Rows[0]["USER_ID"].ToString();
                    GlobalVariables.UserName = clsCN.sqlDT.Rows[0]["UserName"].ToString();
                    GlobalVariables.UserType = clsCN.sqlDT.Rows[0]["UserType"].ToString();
                    frmMDIParent frmMDIParent = new frmMDIParent();
                    frmMDIParent.Show();
                    ////////////// User permission
                    if (GlobalVariables.UserType == "Admin") {
                        frmMDIParent.btnCutomer.Enabled = true;
                        frmMDIParent.btnSuppliers.Enabled = true;
                        frmMDIParent.btnInventory.Enabled = true;
                        frmMDIParent.btnInvoices.Enabled = true;
                        frmMDIParent.btnReports.Enabled = true;
                        frmMDIParent.btnExtras.Enabled = true;
                    }
                    else if (GlobalVariables.UserType == "Manager") {
                        frmMDIParent.btnCutomer.Enabled = false;
                        frmMDIParent.btnSuppliers.Enabled = false;
                        frmMDIParent.btnInventory.Enabled = true;
                        frmMDIParent.btnInvoices.Enabled = true;
                        frmMDIParent.btnReports.Enabled = false;
                        frmMDIParent.btnExtras.Enabled = false;
                    }
                    else if (GlobalVariables.UserType == "Sales") {
                        frmMDIParent.btnCutomer.Enabled = false;
                        frmMDIParent.btnSuppliers.Enabled = false;
                        frmMDIParent.btnInventory.Enabled = false;
                        frmMDIParent.btnInvoices.Enabled = true;
                        frmMDIParent.btnReports.Enabled = false;
                        frmMDIParent.btnExtras.Enabled = false;
                    }

                    /////////////////////////////
                    this.Hide();
                }
                else {
                    lblWaring.Visible = true;
                    lblWaring.Text = "No such user."; }
            }
            catch 
            {
                lblWaring.Visible = true;
                lblWaring.Text = "Error executing sql statement for data.";
            }
        }



        
    }
}
