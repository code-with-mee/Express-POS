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
    public partial class frmPayment : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmPayment()
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtPaymentMethod.Text = item.SubItems[0].Text;
            }
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            LoadSales();
        }

        private void LoadSales() {
            clsCN.ExecuteSQLQuery(" SELECT *  FROM  Sale  WHERE  (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "')");
            if (clsCN.sqlDT.Rows.Count > 0) {
                txtPaymentMethod.Text = clsCN.sqlDT.Rows[0]["PaymentMethod"].ToString();
                txtGTotal.Text = clsCN.sqlDT.Rows[0]["TotalAmount"].ToString();
                txtComment.Text = clsCN.sqlDT.Rows[0]["Comment"].ToString();
                txtPayAmount.Text = clsCN.sqlDT.Rows[0]["PaymentAmount"].ToString();
                txtFright.Text = clsCN.sqlDT.Rows[0]["Freight"].ToString();
                txtDiscount.Text = clsCN.sqlDT.Rows[0]["Discount"].ToString();
                txtChange.Text = clsCN.sqlDT.Rows[0]["ChangeAmount"].ToString();
            }

            double OrderTotal = 0, TaxTotal = 0;

            clsCN.ExecuteSQLQuery(" SELECT  taxAmount1, taxAmount2, taxAmount3  FROM  SaleDetails   WHERE  (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT   SUM(taxAmount1 + taxAmount2 + taxAmount3) AS Expr1  FROM   SaleDetails   WHERE        (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "')");
                TaxTotal = clsCN.num_repl(clsCN.sqlDT.Rows[0]["Expr1"].ToString());
                clsCN.ExecuteSQLQuery("SELECT   SUM(RetailPrice) AS Expr1 FROM   SaleDetails   WHERE  (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
                OrderTotal = clsCN.num_repl(clsCN.sqlDT.Rows[0]["Expr1"].ToString());
            }
            clsCN.ExecuteSQLQuery("UPDATE Sale SET  OrderTotal='" + OrderTotal + "', TaxTotal='" + TaxTotal + "' WHERE  (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "')  ");
            txtOrderTotal.Text = OrderTotal.ToString();
            txtTotalTax.Text = TaxTotal.ToString();
        }

        private void txtPayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Allow any number of decimal places
            char ch = e.KeyChar;
            if (ch == 46 && txtPayAmount.Text.IndexOf('.') != -1)
            { e.Handled = true; return; }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            { e.Handled = true; }
            ///
        }

        private void txtFright_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Allow any number of decimal places
            char ch = e.KeyChar;
            if (ch == 46 && txtFright.Text.IndexOf('.') != -1)
            { e.Handled = true; return; }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            { e.Handled = true; }
            //////////////////////////////
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Allow any number of decimal places
            char ch = e.KeyChar;
            if (ch == 46 && txtDiscount.Text.IndexOf('.') != -1)
            { e.Handled = true; return; }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            { e.Handled = true; }
            //////////////////////////////
        }

        private void txtFright_TextChanged(object sender, EventArgs e)
        {
            clsCN.num_repl(txtFright.Text);
            CalculateData();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            clsCN.num_repl(txtDiscount.Text);
            CalculateData();
        }

        private void CalculateData() {
            try {
                txtGTotal.Text = (double.Parse(txtOrderTotal.Text) + double.Parse(txtTotalTax.Text) + double.Parse(txtFright.Text) - double.Parse(txtDiscount.Text)).ToString();
                txtChange.Text = (double.Parse(txtPayAmount.Text) - double.Parse(txtGTotal.Text)).ToString();
            }
            catch (Exception) {  }
        }

        private void txtPayAmount_TextChanged(object sender, EventArgs e)
        {
            clsCN.num_repl(txtPayAmount.Text);
            CalculateData();
        }

        private void txtOrderTotal_TextChanged(object sender, EventArgs e)
        {
            CalculateData();
        }

        private void txtTotalTax_TextChanged(object sender, EventArgs e)
        {
            CalculateData();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (rbPOSInvoice.Checked)
            {
                clsCN.PrintPOSReceipt(" SELECT Sale.INVOICE_NO, Sale.CUST_ID, Customer.Cust_Name, Customer.Address, Customer.Contact, Sale.Sales_Date, Sale.USER_ID, Users.UserName, Sale.TABLE_ID, ManageTables.Table_Code, ManageTables.Table_Name, " +
                        " Sale.TaxTotal, Sale.Freight, Sale.OrderTotal, Sale.Discount, Sale.TotalAmount, Sale.PaymentAmount, Sale.ChangeAmount, Sale.PaymentMethod, Sale.Status, Sale.Comment, SaleDetails.PRODUCT_ID, Product.ProductName, " +
                        " SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.CostPrice, SaleDetails.RetailPrice, TAX_2.Tax_Name AS TAX_1, SaleDetails.taxAmount1 AS TAX_RATE_1, TAX_1.Tax_Name AS TAX_2, " +
                        " SaleDetails.taxAmount2 AS TAX_RATE_2, TAX.Tax_Name AS TAX_3, SaleDetails.taxAmount3 AS TAX_RATE_3, SaleDetails.Notes FROM            TAX RIGHT OUTER JOIN " +
                        " SaleDetails ON TAX.TAX_ID = SaleDetails.taxName3 LEFT OUTER JOIN TAX AS TAX_2 ON SaleDetails.taxName1 = TAX_2.TAX_ID LEFT OUTER JOIN   TAX AS TAX_1 ON SaleDetails.taxName2 = TAX_1.TAX_ID RIGHT OUTER JOIN " +
                        " Product ON SaleDetails.PRODUCT_ID = Product.PRODUCT_ID RIGHT OUTER JOIN Sale LEFT OUTER JOIN ManageTables ON Sale.TABLE_ID = ManageTables.TABLE_ID LEFT OUTER JOIN " +
                        " Users ON Sale.USER_ID = Users.USER_ID LEFT OUTER JOIN Customer ON Sale.CUST_ID = Customer.CUST_ID ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO   WHERE        (Sale.INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "')   ");
            }
            else
            {
                clsCN.PrintSalesInvoice(" SELECT Sale.INVOICE_NO, Sale.CUST_ID, Customer.Cust_Name, Customer.Address, Customer.Contact, Sale.Sales_Date, Sale.USER_ID, Users.UserName, Sale.TABLE_ID, ManageTables.Table_Code, ManageTables.Table_Name, " +
                        " Sale.TaxTotal, Sale.Freight, Sale.OrderTotal, Sale.Discount, Sale.TotalAmount, Sale.PaymentAmount, Sale.ChangeAmount, Sale.PaymentMethod, Sale.Status, Sale.Comment, SaleDetails.PRODUCT_ID, Product.ProductName, " +
                        " SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.CostPrice, SaleDetails.RetailPrice, TAX_2.Tax_Name AS TAX_1, SaleDetails.taxAmount1 AS TAX_RATE_1, TAX_1.Tax_Name AS TAX_2, " +
                        " SaleDetails.taxAmount2 AS TAX_RATE_2, TAX.Tax_Name AS TAX_3, SaleDetails.taxAmount3 AS TAX_RATE_3, SaleDetails.Notes FROM            TAX RIGHT OUTER JOIN " +
                        " SaleDetails ON TAX.TAX_ID = SaleDetails.taxName3 LEFT OUTER JOIN TAX AS TAX_2 ON SaleDetails.taxName1 = TAX_2.TAX_ID LEFT OUTER JOIN   TAX AS TAX_1 ON SaleDetails.taxName2 = TAX_1.TAX_ID RIGHT OUTER JOIN " +
                        " Product ON SaleDetails.PRODUCT_ID = Product.PRODUCT_ID RIGHT OUTER JOIN Sale LEFT OUTER JOIN ManageTables ON Sale.TABLE_ID = ManageTables.TABLE_ID LEFT OUTER JOIN " +
                        " Users ON Sale.USER_ID = Users.USER_ID LEFT OUTER JOIN Customer ON Sale.CUST_ID = Customer.CUST_ID ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO   WHERE        (Sale.INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "')   ");
            }
        }


        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to complete this invoice?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                /////////////////////////////////
                    clsCN.ExecuteSQLQuery(" SELECT * FROM Sale  WHERE  (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
                    string TABLE_ID = clsCN.sqlDT.Rows[0]["TABLE_ID"].ToString();
                    clsCN.ExecuteSQLQuery(" UPDATE ManageTables SET Booked='N' WHERE  (TABLE_ID = '" + TABLE_ID + "')  ");
                    clsCN.ExecuteSQLQuery(" UPDATE Sale SET PaymentMethod='" + clsCN.str_repl(txtPaymentMethod.Text) + "', OrderTotal='" + clsCN.num_repl(txtOrderTotal.Text) + "', TaxTotal='" + clsCN.num_repl(txtTotalTax.Text) + "', Freight='" + clsCN.num_repl(txtFright.Text) + "', Discount='" + clsCN.num_repl(txtDiscount.Text) + "',  " +
                                          " TotalAmount='" + clsCN.num_repl(txtGTotal.Text) + "', PaymentAmount='" + clsCN.num_repl(txtPayAmount.Text) + "', ChangeAmount='" + clsCN.num_repl(txtChange.Text) + "', Comment='" + clsCN.str_repl(txtComment.Text) + "', Status='N'  WHERE        (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
                    
                    MessageBox.Show("Payment accepted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clsCN.PrintPOSReceipt(" SELECT Sale.INVOICE_NO, Sale.CUST_ID, Customer.Cust_Name, Customer.Address, Customer.Contact, Sale.Sales_Date, Sale.USER_ID, Users.UserName, Sale.TABLE_ID, ManageTables.Table_Code, ManageTables.Table_Name, " +
                                          " Sale.TaxTotal, Sale.Freight, Sale.OrderTotal, Sale.Discount, Sale.TotalAmount, Sale.PaymentAmount, Sale.ChangeAmount, Sale.PaymentMethod, Sale.Status, Sale.Comment, SaleDetails.PRODUCT_ID, Product.ProductName, " +
                                          " SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.CostPrice, SaleDetails.RetailPrice, TAX_2.Tax_Name AS TAX_1, SaleDetails.taxAmount1 AS TAX_RATE_1, TAX_1.Tax_Name AS TAX_2, " +
                                          " SaleDetails.taxAmount2 AS TAX_RATE_2, TAX.Tax_Name AS TAX_3, SaleDetails.taxAmount3 AS TAX_RATE_3, SaleDetails.Notes FROM            TAX RIGHT OUTER JOIN " +
                                          " SaleDetails ON TAX.TAX_ID = SaleDetails.taxName3 LEFT OUTER JOIN TAX AS TAX_2 ON SaleDetails.taxName1 = TAX_2.TAX_ID LEFT OUTER JOIN   TAX AS TAX_1 ON SaleDetails.taxName2 = TAX_1.TAX_ID RIGHT OUTER JOIN " +
                                          " Product ON SaleDetails.PRODUCT_ID = Product.PRODUCT_ID RIGHT OUTER JOIN Sale LEFT OUTER JOIN ManageTables ON Sale.TABLE_ID = ManageTables.TABLE_ID LEFT OUTER JOIN " +
                                          " Users ON Sale.USER_ID = Users.USER_ID LEFT OUTER JOIN Customer ON Sale.CUST_ID = Customer.CUST_ID ON SaleDetails.INVOICE_NO = Sale.INVOICE_NO   WHERE        (Sale.INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "')   ");
                    /////////////////////////////////
                }
        }

    }
}
