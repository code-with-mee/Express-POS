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
    public partial class frm_R_Sales : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_R_Sales()
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

        private void frm_R_Sales_Load(object sender, EventArgs e)
        {
            LoadCheckValue();
            clsCN.FillComboBox(" SELECT  *  FROM Product WHERE (ProdStatus = 'Y') AND (Inventory = 'Y') ORDER BY ProductName", "PRODUCT_ID", "ProductName", cmbProducts);
        }

        private void LoadCheckValue() {
            if (rbProductWiseSales.Checked)
            { cmbProducts.Enabled = true; }
            else { cmbProducts.Enabled = false ; }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LoadCheckValue();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LoadCheckValue();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (rbDateWiseSales.Checked)
            {
                clsCN.PrintSalesDetails(" SELECT        Sale.Sales_Date, SaleDetails.INVOICE_NO, SaleDetails.PRODUCT_ID, Product.ProductName, SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.CostPrice, SaleDetails.RetailPrice " +
                                        " FROM            Product RIGHT OUTER JOIN  SaleDetails ON Product.PRODUCT_ID = SaleDetails.PRODUCT_ID LEFT OUTER JOIN  Sale ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO " +
                                        " WHERE        (Sale.Sales_Date >= '" + dateFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND Sale.Sales_Date <= '" + dateTo.Value.Date.ToString("MM/dd/yyyy") + "') ", "From :" + dateFrom.Value.Date.ToString("MMM-dd-yyyy") + ", To :" + dateTo.Value.Date.ToString("MMM-dd-yyyy"));
            }
            else if (rbProductWiseSales.Checked) {
                if (cmbProducts.SelectedValue == null | cmbProducts.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Product");
                }
                else {
                    clsCN.PrintSalesDetails(" SELECT        Sale.Sales_Date, SaleDetails.INVOICE_NO, SaleDetails.PRODUCT_ID, Product.ProductName, SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.CostPrice, SaleDetails.RetailPrice " +
                            " FROM            Product RIGHT OUTER JOIN  SaleDetails ON Product.PRODUCT_ID = SaleDetails.PRODUCT_ID LEFT OUTER JOIN  Sale ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO " +
                            " WHERE        (Sale.Sales_Date >= '" + dateFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND Sale.Sales_Date <= '" + dateTo.Value.Date.ToString("MM/dd/yyyy") + "') AND (SaleDetails.PRODUCT_ID = '" + cmbProducts.SelectedValue.ToString() + "') ", "From :" + dateFrom.Value.Date.ToString("MMM-dd-yyyy") + ", To :" + dateTo.Value.Date.ToString("MMM-dd-yyyy"));
                }
            }
        }

    }
}
