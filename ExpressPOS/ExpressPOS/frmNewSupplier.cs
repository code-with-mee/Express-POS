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
    public partial class frmNewSupplier : Form
    {

        private static frmNewSupplier _instance;
        public static frmNewSupplier GetInstance()
        {
            if (_instance == null) _instance = new frmNewSupplier();
            return _instance;
        }

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

        public frmNewSupplier()
        {
            InitializeComponent();
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void frmNewSupplier_Load(object sender, EventArgs e)
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

        private ToolTip hint;

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            hint = new ToolTip();
            hint.InitialDelay = 0;
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = ExpressPOS.Properties.Resources.upload_empty;
            btnSubmit.Text = "SUBMIT";
            txtSupplierID.Text = "";
            txtCompanyName.Text = "";
            txtAgencyName.Text = "";
            txtSupplierName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            rbActive.Checked = true;
            dtpEntryDate.Value = DateTime.Now;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string chkVAL = null;
            if (rbActive.Checked)
            { chkVAL = "Y"; }
            else
            { chkVAL = "N"; }

            if (txtCompanyName.Text != "" & txtSupplierName.Text != "" & txtAddress.Text != "")
            {
                //////----------Insert & Update Statement----------//////
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO Supplier (CompanyName, AgencyName, SupplierName, Address, Contact, Email, EntryDate, AcStatus) VALUES ('" + txtCompanyName.Text + "','" + txtAgencyName.Text + "','" + txtSupplierName.Text + "', '" + txtAddress.Text + "', '" + txtContact.Text + "', '" + txtEmail.Text + "', '" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "' ,'" + chkVAL + "')");
                    clsCN.ExecuteSQLQuery("SELECT  SUPP_ID   FROM   Supplier  ORDER BY SUPP_ID DESC");
                    string SuppID = clsCN.sqlDT.Rows[0]["SUPP_ID"].ToString();
                    clsCN.SupplierPhotoUpload(SuppID, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information save sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE Supplier SET  CompanyName='" + txtCompanyName.Text + "', AgencyName='" + txtAgencyName.Text + "', SupplierName='" + txtSupplierName.Text + "', Address='" + txtAddress.Text + "', Contact='" + txtContact.Text + "', Email='" + txtEmail.Text + "', EntryDate='" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "', AcStatus='" + chkVAL + "'  WHERE SUPP_ID='" + txtSupplierID.Text + "' ");
                    clsCN.SupplierPhotoUpload(txtSupplierID.Text, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information update sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                }
                //////----------End Insert & Update Statement----------//////
            }
            else
            {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCompanyName.Focus();
                return;
            }
        }
    }
}
