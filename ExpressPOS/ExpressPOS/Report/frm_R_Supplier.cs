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
    public partial class frm_R_Supplier : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_R_Supplier()
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

        private void frm_R_Supplier_Load(object sender, EventArgs e)
        {
            clsCN.ExecuteSQLQuery("SELECT  SUPP_ID  FROM  Supplier  ORDER BY SUPP_ID");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                txtFrom.Text = clsCN.sqlDT.Rows[0]["SUPP_ID"].ToString();
            }
            else { txtFrom.Text = "0"; }

            clsCN.ExecuteSQLQuery("SELECT  SUPP_ID  FROM  Supplier  ORDER BY SUPP_ID DESC");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                txtTo.Text = clsCN.sqlDT.Rows[0]["SUPP_ID"].ToString();
            }
            else { txtTo.Text = "0"; }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            clsCN.PrintSupplierList(" SELECT   SUPP_ID, CompanyName, AgencyName, SupplierName, Address, Contact, Email, EntryDate  FROM  Supplier " +
                                    " WHERE        (SUPP_ID >= '" + clsCN.num_repl(txtFrom.Text) + "' AND SUPP_ID <= '" + clsCN.num_repl(txtTo.Text) + "') ");
        }
    }
}
