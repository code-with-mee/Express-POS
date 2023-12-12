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
    public partial class frm_R_Customer : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_R_Customer()
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

        private void frm_R_Customer_Load(object sender, EventArgs e)
        {
            clsCN.ExecuteSQLQuery("SELECT  CUST_ID  FROM   Customer   ORDER BY CUST_ID");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                txtFrom.Text = clsCN.sqlDT.Rows[0]["CUST_ID"].ToString();
            }
            else { txtFrom.Text = "0"; }

            clsCN.ExecuteSQLQuery("SELECT  CUST_ID  FROM   Customer   ORDER BY CUST_ID DESC");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                txtTo.Text = clsCN.sqlDT.Rows[0]["CUST_ID"].ToString();
            }
            else { txtTo.Text = "0"; }

        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            clsCN.PrintCustomerList(" SELECT  *  FROM   Customer " +
                                    " WHERE   (CUST_ID >= '" + clsCN.num_repl(txtFrom.Text) + "' AND CUST_ID <= '" + clsCN.num_repl(txtTo.Text) + "') ");
        }
    }
}
