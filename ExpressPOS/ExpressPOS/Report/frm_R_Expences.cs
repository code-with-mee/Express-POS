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
    public partial class frm_R_Expences : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_R_Expences()
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
            clsCN.PrintExpencesList(" SELECT  Expenses.ReceiptNo, Expenses.ReceiptDate, ExpensesOverhead.OverheadName, Expenses.ExpensesAmount, Expenses.ExpensesNote " +
                                    " FROM   Expenses LEFT OUTER JOIN ExpensesOverhead ON Expenses.OVERHEAD_ID = ExpensesOverhead.OVERHEAD_ID " +
                                    " WHERE        (Expenses.ReceiptDate >= '" + dateFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND Expenses.ReceiptDate <= '" + dateTo.Value.Date.ToString("MM/dd/yyyy") + "') ", "FROM :" + dateFrom.Value.Date.ToString("MMM-dd-yyyy") + ", TO :" + dateTo.Value.Date.ToString("MMM-dd-yyyy"));
        }
    }
}
