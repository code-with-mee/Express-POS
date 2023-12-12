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
    public partial class frmInvoiceList : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmInvoiceList()
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sqlStr = " SELECT INVOICE_NO, Sales_Date, PaymentMethod, Comment   FROM    Sale " +
                            " WHERE        (Sales_Date >= '" + dtpFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND Sales_Date <= '" + dtpTo.Value.Date.ToString("MM/dd/yyyy") + "') ";
            clsCN.FillDataGrid(sqlStr, ProductDataGridView);
            clsCN.ExecuteSQLQuery(sqlStr);
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                lblCountProduct.Text = "Total " + clsCN.sqlDT.Rows.Count.ToString() + " invoice(s) found.";
            }
            else { lblCountProduct.Text = "Total 0 invoice found."; }
        }

        private void frmInvoiceList_Load(object sender, EventArgs e)
        {

        }

        private void ProductDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                        /////////////////////////
            if (e.ColumnIndex == 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to edit record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    frmPointOfSales frmPointOfSales = new frmPointOfSales();
                    frmPointOfSales.Show();
                    frmPointOfSales.txtInvoiceNo.Text = ProductDataGridView.CurrentRow.Cells[2].Value.ToString();
                    frmPointOfSales.btnRefresh.PerformClick();
                }
            }
            else if (e.ColumnIndex == 1) {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to print preview record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    if (rbPOSInvoice.Checked)
                    {
                        clsCN.PrintPOSReceipt(" SELECT Sale.INVOICE_NO, Sale.CUST_ID, Customer.Cust_Name, Customer.Address, Customer.Contact, Sale.Sales_Date, Sale.USER_ID, Users.UserName, Sale.TABLE_ID, ManageTables.Table_Code, ManageTables.Table_Name, " +
                                                " Sale.TaxTotal, Sale.Freight, Sale.OrderTotal, Sale.Discount, Sale.TotalAmount, Sale.PaymentAmount, Sale.ChangeAmount, Sale.PaymentMethod, Sale.Status, Sale.Comment, SaleDetails.PRODUCT_ID, Product.ProductName, " +
                                                " SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.CostPrice, SaleDetails.RetailPrice, TAX_2.Tax_Name AS TAX_1, SaleDetails.taxAmount1 AS TAX_RATE_1, TAX_1.Tax_Name AS TAX_2, " +
                                                " SaleDetails.taxAmount2 AS TAX_RATE_2, TAX.Tax_Name AS TAX_3, SaleDetails.taxAmount3 AS TAX_RATE_3, SaleDetails.Notes FROM            TAX RIGHT OUTER JOIN " +
                                                " SaleDetails ON TAX.TAX_ID = SaleDetails.taxName3 LEFT OUTER JOIN TAX AS TAX_2 ON SaleDetails.taxName1 = TAX_2.TAX_ID LEFT OUTER JOIN   TAX AS TAX_1 ON SaleDetails.taxName2 = TAX_1.TAX_ID RIGHT OUTER JOIN " +
                                                " Product ON SaleDetails.PRODUCT_ID = Product.PRODUCT_ID RIGHT OUTER JOIN Sale LEFT OUTER JOIN ManageTables ON Sale.TABLE_ID = ManageTables.TABLE_ID LEFT OUTER JOIN " +
                                                " Users ON Sale.USER_ID = Users.USER_ID LEFT OUTER JOIN Customer ON Sale.CUST_ID = Customer.CUST_ID ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO   WHERE        (Sale.INVOICE_NO = '" + ProductDataGridView.CurrentRow.Cells[2].Value.ToString() + "')   ");
                    }
                    else {
                        clsCN.PrintSalesInvoice(" SELECT Sale.INVOICE_NO, Sale.CUST_ID, Customer.Cust_Name, Customer.Address, Customer.Contact, Sale.Sales_Date, Sale.USER_ID, Users.UserName, Sale.TABLE_ID, ManageTables.Table_Code, ManageTables.Table_Name, " +
                                                " Sale.TaxTotal, Sale.Freight, Sale.OrderTotal, Sale.Discount, Sale.TotalAmount, Sale.PaymentAmount, Sale.ChangeAmount, Sale.PaymentMethod, Sale.Status, Sale.Comment, SaleDetails.PRODUCT_ID, Product.ProductName, " +
                                                " SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.CostPrice, SaleDetails.RetailPrice, TAX_2.Tax_Name AS TAX_1, SaleDetails.taxAmount1 AS TAX_RATE_1, TAX_1.Tax_Name AS TAX_2, " +
                                                " SaleDetails.taxAmount2 AS TAX_RATE_2, TAX.Tax_Name AS TAX_3, SaleDetails.taxAmount3 AS TAX_RATE_3, SaleDetails.Notes FROM            TAX RIGHT OUTER JOIN " +
                                                " SaleDetails ON TAX.TAX_ID = SaleDetails.taxName3 LEFT OUTER JOIN TAX AS TAX_2 ON SaleDetails.taxName1 = TAX_2.TAX_ID LEFT OUTER JOIN   TAX AS TAX_1 ON SaleDetails.taxName2 = TAX_1.TAX_ID RIGHT OUTER JOIN " +
                                                " Product ON SaleDetails.PRODUCT_ID = Product.PRODUCT_ID RIGHT OUTER JOIN Sale LEFT OUTER JOIN ManageTables ON Sale.TABLE_ID = ManageTables.TABLE_ID LEFT OUTER JOIN " +
                                                " Users ON Sale.USER_ID = Users.USER_ID LEFT OUTER JOIN Customer ON Sale.CUST_ID = Customer.CUST_ID ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO   WHERE        (Sale.INVOICE_NO = '" + ProductDataGridView.CurrentRow.Cells[2].Value.ToString() + "')   ");
                    }
                }
            }
        }
    }
}
