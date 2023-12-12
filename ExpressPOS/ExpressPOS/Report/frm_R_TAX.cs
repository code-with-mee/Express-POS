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
    public partial class frm_R_TAX : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_R_TAX()
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

        private void frm_R_TAX_Load(object sender, EventArgs e)
        {

        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            clsCN.PrintTaxReprot(" SELECT        Sale.Sales_Date, Sale.INVOICE_NO, TAX_2.Tax_Name AS Tax_Name_1, SaleDetails.taxAmount1, TAX_1.Tax_Name AS Tax_Name_2, SaleDetails.taxAmount2, TAX.Tax_Name AS Tax_Name_3, SaleDetails.taxAmount3 " +
                                 " FROM            TAX AS TAX_1 LEFT OUTER JOIN   SaleDetails ON TAX_1.TAX_ID = SaleDetails.taxName2 LEFT OUTER JOIN  TAX ON SaleDetails.taxName3 = TAX.TAX_ID LEFT OUTER JOIN " +
                                 " TAX AS TAX_2 ON SaleDetails.taxName1 = TAX_2.TAX_ID LEFT OUTER JOIN  Sale ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO " +
                                 " WHERE        (Sale.Sales_Date >= '" + dateFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND Sale.Sales_Date <= '" + dateTo.Value.Date.ToString("MM/dd/yyyy") + "') ", "FROM :" + dateFrom.Value.Date.ToString("MMM-dd-yyyy") + ", TO :" + dateTo.Value.Date.ToString("MMM-dd-yyyy"));
        }
    }
}
