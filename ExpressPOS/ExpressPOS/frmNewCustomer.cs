using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; 

namespace ExpressPOS
{
    public partial class frmNewCustomer : Form
    {

        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public frmNewCustomer()
        {
            InitializeComponent();
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Parent = null;
        }

        private void btnMinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void frmNewCustomer_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
        }

        private void lnkBrowseImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {
                OpenFileDialog.Title = "Browse image";
                OpenFileDialog.Filter = "Image Files (JPEG,GIF,BMP,PNG)|*.jpg;*.jpeg;*.gif;*.bmp;*.png;";

                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PictureBox PictureBox1 = new PictureBox();
                    pictureBox1.BackgroundImage = new Bitmap(OpenFileDialog.FileName);
                    this.Controls.Add(pictureBox1);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = ExpressPOS.Properties.Resources.upload_empty;
            btnSubmit.Text = "SUBMIT";
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            txtAddress.Text="";
            txtContact.Text="";
            txtEmail.Text="";
            rbActive.Checked=true ;
            dtpEntryDate.Value = DateTime.Now;
        }


        private ToolTip hint;

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            hint = new ToolTip();
            hint.IsBalloon = true;
            hint.ToolTipTitle = "Recommended Image";
            hint.ToolTipIcon = ToolTipIcon.Info;
            hint.Show(string.Empty, pictureBox1);
            hint.Show("The recommended image dimensions are 300px. X 300px.", pictureBox1, pictureBox1.Height, 0);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //try { hint.Dispose(); }
            //catch { }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string chkVAL = null;
            if (rbActive.Checked)
            { chkVAL = "Y"; }
            else
            { chkVAL = "N"; }

            if (txtCustomerName.Text != "" & txtAddress.Text != "")
            {
             //////----------Insert & Update Statement----------//////
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO Customer (Cust_Name, Address, Contact, Email, EntryDate, Status) VALUES ('" + txtCustomerName.Text + "', '" + txtAddress.Text + "', '" + txtContact.Text + "', '" + txtEmail.Text + "', '" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "' ,'" + chkVAL + "')");
                    clsCN.ExecuteSQLQuery("SELECT  CUST_ID   FROM   Customer  ORDER BY CUST_ID DESC");
                    string CustID = clsCN.sqlDT.Rows[0]["CUST_ID"].ToString();
                    clsCN.CutomerPhotoUpload(CustID, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information save sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE Customer SET  Cust_Name='" + txtCustomerName.Text + "', Address='" + txtAddress.Text + "', Contact='" + txtContact.Text + "', Email='" + txtEmail.Text + "', EntryDate='" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "', Status='" + chkVAL + "' WHERE CUST_ID='" + txtCustomerID.Text + "' ");
                    clsCN.CutomerPhotoUpload(txtCustomerID.Text, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information update sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               //////----------End Insert & Update Statement----------//////
            }
            else
            {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomerName.Focus();
                return;
            }
        }

    }
}
