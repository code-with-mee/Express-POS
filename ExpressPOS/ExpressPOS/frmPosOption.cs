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
    public partial class frmPosOption : Form
    {
       clsConnectionNode clsCN = new clsConnectionNode();
       public string Invoice_No { get; set; }
       public const int WM_NCLBUTTONDOWN = 0xA1;
       public const int HT_CAPTION = 0x2;
       [DllImportAttribute("user32.dll")]
       public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
       [DllImportAttribute("user32.dll")]
       public static extern bool ReleaseCapture();

        public frmPosOption()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }

        private void frmPosOption_Load(object sender, EventArgs e)
        {
            clsCN.FillComboBox(" SELECT  *  FROM Customer  WHERE (Status = 'Y')   ORDER BY Cust_Name", "CUST_ID", "Cust_Name", cmbCustomer);
            clsCN.FillComboBox(" SELECT  *  FROM ManageTables   ORDER BY Table_Code", "TABLE_ID", "Table_Code", cmbTable);
            clsCN.FillComboBox(" SELECT  *    FROM    Users", "USER_ID", "UserName", cmbSalesMan);

            clsCN.ExecuteSQLQuery(" SELECT        CUST_ID, USER_ID, TABLE_ID  FROM            Sale   WHERE        (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
            if (clsCN.sqlDT.Rows.Count > 0) {
                try
                {
                    cmbCustomer.SelectedValue = clsCN.sqlDT.Rows[0]["CUST_ID"].ToString();
                    cmbTable.SelectedValue = clsCN.sqlDT.Rows[0]["TABLE_ID"].ToString();
                    cmbSalesMan.SelectedValue = clsCN.sqlDT.Rows[0]["USER_ID"].ToString();
                }
                catch { }
            }
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            clsCN.ExecuteSQLQuery(" UPDATE Sale SET CUST_ID = '" + clsCN.fltr_combo(cmbCustomer).ToString() + "',  USER_ID = '" + clsCN.fltr_combo(cmbSalesMan).ToString() + "', TABLE_ID = '" + clsCN.fltr_combo(cmbTable).ToString() + "'   WHERE        (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
            MessageBox.Show("Information update Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
