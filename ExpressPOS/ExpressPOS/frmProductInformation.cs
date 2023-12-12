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
    public partial class frmProductInformation : Form
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

        public frmProductInformation()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
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

        //public static void Test()
        //{
        //    using (var image = Image.FromFile(@"c:\logo.png"))
        //    using (var newImage = ScaleImage(image, 300, 400))
        //    {
        //        newImage.Save(@"c:\test.png", ImageFormat.Png);
        //    }
        //}

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }


        private void frmProductInformation_Load(object sender, EventArgs e)
        {
            clsCN.FillComboBox(" SELECT  CAT_ID, Cat_Name  FROM  Categories  ORDER BY Cat_Name", "CAT_ID", "Cat_Name", cmbCategory);
            clsCN.FillComboBox(" SELECT  TAX_ID, Tax_Name  FROM  TAX  ORDER BY Tax_Name", "TAX_ID", "Tax_Name", cmbTax1);
            clsCN.FillComboBox(" SELECT  TAX_ID, Tax_Name  FROM  TAX  ORDER BY Tax_Name", "TAX_ID", "Tax_Name", cmbTax2);
            clsCN.FillComboBox(" SELECT  TAX_ID, Tax_Name  FROM  TAX  ORDER BY Tax_Name", "TAX_ID", "Tax_Name", cmbTax3);
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
                if (btnSubmit.Text == "SUBMIT")
                {
                    if (chkAutoBarcode.Checked)
                    {
                        string barcode = null;
                        clsCN.ExecuteSQLQuery("INSERT INTO Product (ProductName, UPC_EAN, CAT_ID, CostPrice, RetailPrice, TaxName1, TaxRate1, TaxName2, TaxRate2, Quantity, UnitOfMeasure, ReorderLevel, ProdStatus, Inventory, TaxName3, TaxRate3) VALUES ('" + txtProductName.Text + "', '" + clsCN.GenarateAutoBarcode(barcode) + "' , '" + cmbCategory.SelectedValue.ToString() + "', '" + clsCN.num_repl(txtCostPrice.Text) + "', '" + clsCN.num_repl(txtRetailPrice.Text) + "', '" + clsCN.fltr_combo(cmbTax1).ToString() + "', '" + clsCN.num_repl(txtTaxRate1.Text) + "', '" + clsCN.fltr_combo(cmbTax2).ToString() + "', '" + clsCN.num_repl(txtTaxRate2.Text) + "', '" + clsCN.num_repl(txtQuantity.Text) + "', '" + txtUOM.Text + "', '" + clsCN.num_repl(txtReOrderQty.Text) + "', '" + chkVAL + "', 'Y', '" + clsCN.fltr_combo(cmbTax3).ToString() + "', '" + clsCN.num_repl(txtTaxRate3.Text) + "' )");
                        clsCN.ExecuteSQLQuery("SELECT  PRODUCT_ID  FROM   Product    ORDER BY PRODUCT_ID DESC");
                        string PRODUCT_ID = clsCN.sqlDT.Rows[0]["PRODUCT_ID"].ToString();
                        clsCN.ProductPhotoUpload(PRODUCT_ID, pictureBox1);
                        btnReset.PerformClick();
                        MessageBox.Show("Information save sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        clsCN.ExecuteSQLQuery("INSERT INTO Product (ProductName, UPC_EAN, CAT_ID, CostPrice, RetailPrice, TaxName1, TaxRate1, TaxName2, TaxRate2, Quantity, UnitOfMeasure, ReorderLevel, ProdStatus, Inventory, TaxName3, TaxRate3) VALUES ('" + txtProductName.Text + "', '" + txtBarcode.Text + "' , '" + cmbCategory.SelectedValue.ToString() + "', '" + clsCN.num_repl(txtCostPrice.Text) + "', '" + clsCN.num_repl(txtRetailPrice.Text) + "', '" + clsCN.fltr_combo(cmbTax1).ToString() + "', '" + clsCN.num_repl(txtTaxRate1.Text) + "', '" + clsCN.fltr_combo(cmbTax2).ToString() + "', '" + clsCN.num_repl(txtTaxRate2.Text) + "', '" + clsCN.num_repl(txtQuantity.Text) + "', '" + txtUOM.Text + "', '" + clsCN.num_repl(txtReOrderQty.Text) + "', '" + chkVAL + "', 'Y', '" + clsCN.fltr_combo(cmbTax3).ToString() + "', '" + clsCN.num_repl(txtTaxRate3.Text) + "' )");
                        clsCN.ExecuteSQLQuery("SELECT  PRODUCT_ID  FROM   Product    ORDER BY PRODUCT_ID DESC");
                        string PROD_ID = clsCN.sqlDT.Rows[0]["PRODUCT_ID"].ToString();
                        clsCN.ProductPhotoUpload(PROD_ID, pictureBox1);
                        btnReset.PerformClick();
                        MessageBox.Show("Information save sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE Product SET  ProductName='" + txtProductName.Text + "', UPC_EAN='" + txtBarcode.Text + "', CAT_ID='" + cmbCategory.SelectedValue.ToString() + "', CostPrice='" + clsCN.num_repl(txtCostPrice.Text) + "', RetailPrice='" + clsCN.num_repl(txtRetailPrice.Text) + "', TaxName1='" + clsCN.fltr_combo(cmbTax1).ToString() + "', TaxRate1='" + clsCN.num_repl(txtTaxRate1.Text) + "', TaxName2='" + clsCN.fltr_combo(cmbTax2).ToString() + "', TaxRate2='" + clsCN.num_repl(txtTaxRate2.Text) + "', Quantity='" + clsCN.num_repl(txtQuantity.Text) + "', UnitOfMeasure='" + txtUOM.Text + "', ReorderLevel='" + clsCN.num_repl(txtReOrderQty.Text) + "', ProdStatus='" + chkVAL + "', TaxName3='" + clsCN.fltr_combo(cmbTax3).ToString() + "', TaxRate3='" + clsCN.num_repl(txtTaxRate3.Text) + "'  WHERE PRODUCT_ID='" + txtProdID.Text + "' ");
                    clsCN.ProductPhotoUpload(txtProdID.Text, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information update sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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

        private void lnkBrowseImage_Click(object sender, EventArgs e)
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
            rbActive.Checked = true;
            txtProdID.Text = "";
            txtProductName.Text = "";
            txtBarcode.Text = "";
            chkAutoBarcode.Checked = true;
            txtUOM.Text = "";
            txtQuantity.Text = "";
            txtCostPrice.Text = "";
            txtRetailPrice.Text = "";
            txtReOrderQty.Text = "";
            txtTaxRate1.Text = "";
            txtTaxRate2.Text = "";
            txtTaxRate3.Text = "";
            cmbCategory.SelectedIndex = -1;
        }
    }
}
