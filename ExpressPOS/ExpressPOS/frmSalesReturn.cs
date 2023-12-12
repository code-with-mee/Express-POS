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
    public partial class frmSalesReturn : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmSalesReturn()
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

        private void btnSearchInv_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
                clsCN.ExecuteSQLQuery(" SELECT * FROM SaleDetails WHERE  (INVOICE_NO = '" + clsCN.num_repl(txtInvoiceNo.Text) + "') ");
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    // Load Item from sales details table.
                    clsCN.FillDataGrid(" SELECT   SaleDetails.id, SaleDetails.INVOICE_NO, SaleDetails.PRODUCT_ID, Product.ProductName, SaleDetails.QTY, SaleDetails.RetailPrice " +
                                                " FROM  SaleDetails LEFT OUTER JOIN  Product ON SaleDetails.PRODUCT_ID = Product.PRODUCT_ID   WHERE        (SaleDetails.INVOICE_NO = '" + clsCN.num_repl(txtInvoiceNo.Text) + "') ", dgvSalesItem);
                    // Load Item from sales return table.
                    clsCN.FillDataGrid(" SELECT        SalesReturn.RETURN_ID, SalesReturn.INVOICE_NO, SalesReturn.PRODUCT_ID, Product.ProductName, SalesReturn.QTY, SalesReturn.PRICE, SalesReturn.ReturnTAX " +
                                       " FROM            SalesReturn LEFT OUTER JOIN   Product ON SalesReturn.PRODUCT_ID = Product.PRODUCT_ID   WHERE        (SalesReturn.INVOICE_NO = '" + clsCN.num_repl(txtInvoiceNo.Text) + "') ", dgvSalesReturnItem);
                }
                else { MessageBox.Show("Invoice number not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Please enter invoice number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }


        private void frmSalesReturn_Load(object sender, EventArgs e)
        {
            gbReturnItem.Visible = false;
            LoadReturnList();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gbReturnItem.Visible = false;
        }

        private void dgvSalesItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                txtProductID.Text = dgvSalesItem.CurrentRow.Cells[3].Value.ToString();
                txtProductInvoice.Text = dgvSalesItem.CurrentRow.Cells[2].Value.ToString();
                txtReturnQty.Text = dgvSalesItem.CurrentRow.Cells[5].Value.ToString();
                gbReturnItem.Visible = true;
                txtReturnQty.Focus();
            }
        }

        private void LoadReturnList() {
            clsCN.FillDataGrid(" SELECT DISTINCT INVOICE_NO, ReturnDate   FROM            SalesReturn " +
                               " WHERE        (ReturnDate >= '" + dtpFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND ReturnDate <= '" + dtpTo.Value.Date.ToString("MM/dd/yyyy") + "') ", ReturnDataGridView);
        }

        private void ReturnProduct(string INVOICE_NO, string PRODUCT_ID, double R_QTY)
        {
            clsCN.ExecuteSQLQuery(" SELECT  *  FROM    SaleDetails   WHERE    (INVOICE_NO = '" + INVOICE_NO + "') AND (PRODUCT_ID = '" + PRODUCT_ID + "') AND (QTY >= 0) ");
            if (clsCN.sqlDT.Rows.Count > 0) {
                double QTY = clsCN.num_repl(clsCN.sqlDT.Rows[0]["QTY"].ToString());
                double CostPrice = clsCN.num_repl(clsCN.sqlDT.Rows[0]["CostPrice"].ToString());
                double RetailPrice = clsCN.num_repl(clsCN.sqlDT.Rows[0]["RetailPrice"].ToString());
                double taxAmount1 = clsCN.num_repl(clsCN.sqlDT.Rows[0]["taxAmount1"].ToString());
                double taxAmount2 = clsCN.num_repl(clsCN.sqlDT.Rows[0]["taxAmount2"].ToString());
                double taxAmount3 = clsCN.num_repl(clsCN.sqlDT.Rows[0]["taxAmount3"].ToString());

                //Calculate unit value....
                double Unit_CostPrice = CostPrice / QTY;
                double Unit_RetailPrice = RetailPrice / QTY;
                double Unit_taxAmount1 = taxAmount1 / QTY;
                double Unit_taxAmount2 = taxAmount2 / QTY;
                double Unit_taxAmount3 = taxAmount3 / QTY;

                //Insert sales return table
                clsCN.ExecuteSQLQuery(" INSERT INTO SalesReturn ( INVOICE_NO, PRODUCT_ID, QTY, PRICE, ReturnTAX, ReturnDate) VALUES " +
                                      " ( '" + INVOICE_NO + "', '" + PRODUCT_ID + "', '" + R_QTY + "', '" + Unit_RetailPrice * R_QTY + "', '" + ((Unit_taxAmount1 + Unit_taxAmount2 + Unit_taxAmount3) * R_QTY) + "', '" +  DateTime.Now.ToString("MM/dd/yyyy")  + "' ) ");

                //Update sales table
                clsCN.ExecuteSQLQuery(" UPDATE SaleDetails  SET    QTY=QTY-'" + R_QTY + "', CostPrice=CostPrice-'" + Unit_CostPrice * R_QTY + "', RetailPrice=RetailPrice-'" + Unit_RetailPrice * R_QTY + "', " +
                                      "  taxAmount1=taxAmount1-'" + Unit_taxAmount1 * R_QTY + "', taxAmount2=taxAmount2 -'" + Unit_taxAmount2 * R_QTY + "', taxAmount3=taxAmount3-'" + Unit_taxAmount3 * R_QTY + "' " +
                                      " WHERE   (INVOICE_NO = '" + INVOICE_NO + "') AND (PRODUCT_ID = '" + PRODUCT_ID + "') ");

                btnSearchInv.PerformClick();
                gbReturnItem.Visible = false;
            }
            else { MessageBox.Show("Quantity not sufficient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtReturnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Allow any number of decimal places
            char ch = e.KeyChar;
            if (ch == 46 && txtReturnQty.Text.IndexOf('.') != -1)
            { e.Handled = true; return; }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            { e.Handled = true; }
            ///
        }

        private void btnAddMoreQty_Click(object sender, EventArgs e)
        {
            ReturnProduct(txtProductInvoice.Text, txtProductID.Text, clsCN.num_repl(txtReturnQty.Text));
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadReturnList();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            LoadReturnList();
        }

        private void ReturnDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                clsCN.PrintReturnInvoice(" SELECT        SalesReturn.RETURN_ID, SalesReturn.INVOICE_NO, SalesReturn.PRODUCT_ID, Product.ProductName, Product.UnitOfMeasure, SalesReturn.QTY, SalesReturn.PRICE, SalesReturn.ReturnTAX, SalesReturn.ReturnDate " +
                                         " FROM            SalesReturn LEFT OUTER JOIN  Product ON SalesReturn.PRODUCT_ID = Product.PRODUCT_ID   WHERE        (SalesReturn.INVOICE_NO = '" + ReturnDataGridView.CurrentRow.Cells[1].Value.ToString() + "') ");
            }
        }

    }
}
