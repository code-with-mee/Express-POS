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
    public partial class frmGiftItem : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmGiftItem()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }

        private void frmGiftItem_Load(object sender, EventArgs e)
        {
            clsCN.FillComboBox(" SELECT  CAT_ID, Cat_Name  FROM  Categories  ORDER BY Cat_Name", "CAT_ID", "Cat_Name", cmbCategory);
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
            try { hint.Dispose(); }
            catch { }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string chkVAL = null;
            if (rbActive.Checked)
            { chkVAL = "Y"; }
            else
            { chkVAL = "N"; }

            if (string.IsNullOrEmpty(txtProductName.Text) | string.IsNullOrEmpty(txtUOM.Text) )
            { MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (cmbCategory.SelectedValue == null | cmbCategory.SelectedIndex == -1)
            { MessageBox.Show("Please select a category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
            ///// Start                
                if (btnSubmit.Text == "SUBMIT")
                {
                    if (chkAutoBarcode.Checked)
                    {
                        string barcode = null;
                        clsCN.ExecuteSQLQuery("INSERT INTO Product (ProductName, UPC_EAN, CAT_ID, CostPrice, RetailPrice, TaxName1, TaxRate1, TaxName2, TaxRate2, TaxName3, TaxRate3, Quantity, UnitOfMeasure, ReorderLevel, ProdStatus, Inventory) VALUES ('" + txtProductName.Text + "', '" + clsCN.GenarateAutoBarcode(barcode) + "' , '" + cmbCategory.SelectedValue.ToString() + "', '0', '0', '0', '0', '0', '0', '0', '0', '" + clsCN.num_repl(txtQuantity.Text) + "', '" + txtUOM.Text + "', '0', '" + chkVAL + "', 'N' )");
                        clsCN.ExecuteSQLQuery("SELECT  PRODUCT_ID  FROM   Product    ORDER BY PRODUCT_ID DESC");
                        string PRODUCT_ID = clsCN.sqlDT.Rows[0]["PRODUCT_ID"].ToString();
                        clsCN.ProductPhotoUpload(PRODUCT_ID, pictureBox1);
                        btnReset.PerformClick();
                        MessageBox.Show("Information save sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        clsCN.ExecuteSQLQuery("INSERT INTO Product (ProductName, UPC_EAN, CAT_ID, CostPrice, RetailPrice, TaxName1, TaxRate1, TaxName2, TaxRate2, TaxName3, TaxRate3, Quantity, UnitOfMeasure, ReorderLevel, ProdStatus, Inventory) VALUES ('" + txtProductName.Text + "', '" + txtBarcode.Text + "' , '" + cmbCategory.SelectedValue.ToString() + "', '0', '0', '0', '0', '0', '0', '0', '0', '" + clsCN.num_repl(txtQuantity.Text) + "', '" + txtUOM.Text + "', '0', '" + chkVAL + "', 'N' )");
                        clsCN.ExecuteSQLQuery("SELECT  PRODUCT_ID  FROM   Product    ORDER BY PRODUCT_ID DESC");
                        string PROD_ID = clsCN.sqlDT.Rows[0]["PRODUCT_ID"].ToString();
                        clsCN.ProductPhotoUpload(PROD_ID, pictureBox1);
                        btnReset.PerformClick();
                        MessageBox.Show("Information save sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE Product SET  ProductName='" + txtProductName.Text + "', UPC_EAN='" + txtBarcode.Text + "', CAT_ID='" + cmbCategory.SelectedValue.ToString() + "', CostPrice='0', RetailPrice='0', TaxName1='0', TaxRate1='0', TaxName2='0', TaxRate2='0', TaxName3='0', TaxRate3='0', Quantity='" + clsCN.num_repl(txtQuantity.Text) + "', UnitOfMeasure='" + txtUOM.Text + "', ReorderLevel='0', ProdStatus='" + chkVAL + "', Inventory='N'  WHERE PRODUCT_ID='" + txtProdID.Text + "'");
                    clsCN.ProductPhotoUpload(txtProdID.Text, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information update sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            //// End
            }

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
            rbActive.Checked = true; txtProdID.Text = "";
            txtProductName.Text = "";
            txtBarcode.Text = "";
            chkAutoBarcode.Checked = true;
            txtUOM.Text = "";
            txtQuantity.Text = "";
            cmbCategory.SelectedIndex = -1;
        }

        private void btnListView_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmGiftItemList"] as frmGiftItemList == null)
            {
                frmGiftItemList frmGiftItemList = new frmGiftItemList();
                frmGiftItemList.MdiParent = this.ParentForm;
                frmGiftItemList.Show();
            }
        }
    }
}
