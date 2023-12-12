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
    public partial class frm_R_StockMovement : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_R_StockMovement()
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

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") {
                MessageBox.Show("Please select movement status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                clsCN.PrintStockMovementStatus(" SELECT        StockMovement.SM_ID, StockMovement.PRODUCT_ID, StockMovement.SUPP_ID, StockMovement.EntryDate, StockMovement.Quantity, StockMovement.TotalCost, StockMovement.Stock, Supplier.CompanyName, " +
                                        " Supplier.AgencyName, Product.ProductName, Product.UPC_EAN, Product.UnitOfMeasure  FROM            StockMovement LEFT OUTER JOIN " +
                                        " Product ON StockMovement.PRODUCT_ID = Product.PRODUCT_ID LEFT OUTER JOIN  Supplier ON StockMovement.SUPP_ID = Supplier.SUPP_ID " +
                                        " WHERE         (StockMovement.EntryDate >= '" + dateFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND StockMovement.EntryDate <= '" + dateTo.Value.Date.ToString("MM/dd/yyyy") + "') AND (StockMovement.Stock = N'" + comboBox1.Text.ToString() + "') ", comboBox1.Text.ToString() + " From :" + dateFrom.Value.Date.ToString("MMM-dd-yyyy") + ", To :" + dateTo.Value.Date.ToString("MMM-dd-yyyy"));
            }
            

        }


    }
}
