using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace ExpressPOS.Report
{
    public partial class frm_R_Product : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_R_Product()
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

        private void btnMinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frm_R_Product_Load(object sender, EventArgs e)
        {
            clsCN.FillComboBox(" SELECT  CAT_ID, Cat_Name  FROM  Categories  ORDER BY Cat_Name", "CAT_ID", "Cat_Name", cmbCategory);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (rbAllProduct.Checked) {
                clsCN.PrintProductList(" SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Product.CAT_ID, Categories.Cat_Name, Product.CostPrice, Product.RetailPrice, Product.Quantity, Product.UnitOfMeasure, Product.ReorderLevel, " +
                                       " Product.ProdStatus FROM            Product LEFT OUTER JOIN  Categories ON Product.CAT_ID = Categories.CAT_ID WHERE        (Product.Inventory = 'Y') ");
            }
            else if (rbCategoryWiseProduct.Checked) {
                if (cmbCategory.SelectedValue == null | cmbCategory.SelectedIndex == -1) {
                    MessageBox.Show("Please select a category");
                }
                else { 
                 clsCN.PrintProductList(" SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Product.CAT_ID, Categories.Cat_Name, Product.CostPrice, Product.RetailPrice, Product.Quantity, Product.UnitOfMeasure, Product.ReorderLevel, " +
                                                       " Product.ProdStatus FROM            Product LEFT OUTER JOIN  Categories ON Product.CAT_ID = Categories.CAT_ID WHERE    (Product.CAT_ID = '" + cmbCategory.SelectedValue.ToString() + "') ");
                }
            }
            else if (rbAllGiftItem.Checked) {
                clsCN.PrintProductList(" SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Product.CAT_ID, Categories.Cat_Name, Product.CostPrice, Product.RetailPrice, Product.Quantity, Product.UnitOfMeasure, Product.ReorderLevel, " +
                                       " Product.ProdStatus FROM            Product LEFT OUTER JOIN  Categories ON Product.CAT_ID = Categories.CAT_ID WHERE        (Product.Inventory = 'N') ");
            }
        }
    }
}
