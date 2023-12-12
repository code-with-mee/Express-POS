using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmPointOfSales : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public frmPointOfSales()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape)) { btnBack2Home.PerformClick(); }
            else if (keyData == (Keys.F2)) { btnNewInvoice.PerformClick(); }
            else if (keyData == (Keys.F3)) { btnHoldInvoice.PerformClick(); }
            else if (keyData == (Keys.F4)) { btnGiftItem.PerformClick(); }
            else if (keyData == (Keys.F5)) { btnRefresh.PerformClick(); }
            else if (keyData == (Keys.F6)) { btnPayments.PerformClick(); }
            else if (keyData == (Keys.F7)) { btnOptions.PerformClick(); }
            else if (keyData == (Keys.Control | Keys.T)) { btnTable.PerformClick(); }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmPointOfSales_Load(object sender, EventArgs e)
        {
            clsCN.FillComboBox(" SELECT  *  FROM Product WHERE (ProdStatus = 'Y') AND (Inventory = 'Y') ORDER BY ProductName", "PRODUCT_ID", "ProductName", cmbProducts);
            clsCN.FillComboBox(" SELECT  *  FROM Categories  ORDER BY Cat_Name", "CAT_ID", "Cat_Name", cmbCategory);
            txtInvoiceNo.Text = GlobalVariables.TEMP_INVOICE_NO;
            gbAddItem.Visible = false;
        }


        private void LoadProductWithImages(string CAT_ID)
        {
            ItemPanelView.Controls.Clear();
            clsCN.ExecuteSQLQuery("SELECT  *  FROM Product WHERE (ProdStatus = 'Y')  AND CAT_ID ='" + clsCN.num_repl(CAT_ID) + "' ORDER BY ProductName ");
            if (clsCN.sqlDT.Rows.Count > 0) {
            int i;
            for (i = 0; i <= clsCN.sqlDT.Rows.Count - 1; i++)
            {
                Button picturebx = new Button();
                picturebx.BackColor = Color.White;
                picturebx.TextImageRelation = TextImageRelation.ImageBeforeText;
                picturebx.TextAlign = ContentAlignment.MiddleLeft;
                picturebx.ImageAlign = ContentAlignment.MiddleLeft;
                picturebx.Name = clsCN.sqlDT.Rows[i]["PRODUCT_ID"].ToString();
                picturebx.Text = clsCN.sqlDT.Rows[i]["ProductName"].ToString();
                picturebx.Text += Environment.NewLine + "Barcode: " + clsCN.sqlDT.Rows[i]["UPC_EAN"];
                picturebx.Text += Environment.NewLine + "Price: " + clsCN.sqlDT.Rows[i]["RetailPrice"];
                picturebx.Size = new System.Drawing.Size(150, 80);
                var data = (Byte[])clsCN.sqlDT.Rows[i]["ProductPhoto"];
                var stream = new MemoryStream(data);
                picturebx.Image = resizeImage(Image.FromStream(stream), new Size(45, 45));
                picturebx.Click += Product_Click;
                ItemPanelView.Controls.Add(picturebx);
            }
            }
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void Product_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
            Button button = sender as Button;
            AddItemToCart(txtInvoiceNo.Text, button.Name, 1);
            }
            else { MessageBox.Show("Empty Invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnBack2Home_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
            frmPayment frmPayment = new frmPayment();
            frmPayment.txtInvoiceNo.Text = txtInvoiceNo.Text;
            frmPayment.ShowDialog();
            }
            else { MessageBox.Show("Empty invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        private void cmbCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cmbCategory.SelectedValue == null) | !(cmbCategory.SelectedIndex == -1))
            {
                LoadProductWithImages(cmbCategory.SelectedValue.ToString());
            }
        }

        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            DialogResult msg = new DialogResult();
            msg = MessageBox.Show("Do you want create a new invoice?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == DialogResult.Yes)
            {
                DateTime today = DateTime.Today;
                clsCN.ExecuteSQLQuery(" INSERT INTO Sale (CUST_ID ,  Sales_Date , USER_ID  , TABLE_ID , TaxTotal , Freight , OrderTotal , Discount , TotalAmount , PaymentAmount , ChangeAmount , PaymentMethod , Status , Comment ) VALUES (0 ,  '" + today.ToString("MM/dd/yyyy") + "' , '" + GlobalVariables.UserID + "'  , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 'DUE' , 'N' , '-') ");
                clsCN.ExecuteSQLQuery("SELECT  INVOICE_NO   FROM   Sale  ORDER BY INVOICE_NO DESC");
                txtInvoiceNo.Text  = clsCN.sqlDT.Rows[0]["INVOICE_NO"].ToString();
                LoadCartItem(txtInvoiceNo.Text);
                txtBarcode.Focus();
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (txtInvoiceNo.Text != "")
                {
                    if (txtBarcode.Text != "")
                    {
                        clsCN.ExecuteSQLQuery("SELECT  *  FROM     Product   WHERE    (UPC_EAN = '" + clsCN.str_repl(txtBarcode.Text) + "')   ORDER BY PRODUCT_ID");
                        if (clsCN.sqlDT.Rows.Count > 0)
                        { AddItemToCart(txtInvoiceNo.Text, clsCN.sqlDT.Rows[0]["PRODUCT_ID"].ToString(), 1); txtBarcode.Text = ""; }
                        else
                        { txtBarcode.Text = "";
                            MessageBox.Show("Barcode not found." + Environment.NewLine + "Try Again.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                else { MessageBox.Show("No invoice number found." + Environment.NewLine + "Please create a new invoice by clicking NEW SALE button." , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }


        private void AddItemToCart(string INVOICE_NO, string PRODUCT_ID, double QTY) {
            clsCN.ExecuteSQLQuery("SELECT *  FROM    Product  WHERE   (PRODUCT_ID = '" + PRODUCT_ID + "') AND (Quantity >= '"+ clsCN.num_repl(QTY.ToString()) +"')");
            if (clsCN.sqlDT.Rows.Count > 0) {
                double CostPrice = clsCN.num_repl(clsCN.sqlDT.Rows[0]["CostPrice"].ToString());
                double RetailPrice = clsCN.num_repl(clsCN.sqlDT.Rows[0]["RetailPrice"].ToString());

                //////// Get tax 1(If applied)
                string taxName1 = clsCN.sqlDT.Rows[0]["TaxName1"].ToString();
                double taxRate1;
                if (clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxName1"].ToString()) > 0)
                {
                    taxRate1=clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxRate1"].ToString());
                }
                else { taxRate1 = 0; }
                //////// Get tax 2 (If applied)
                string taxName2 = clsCN.sqlDT.Rows[0]["TaxName2"].ToString();
                double taxRate2;
                if (clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxName2"].ToString()) > 0)
                {
                    taxRate2 = clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxRate2"].ToString());
                }
                else { taxRate2 = 0; }
                //////// Get tax 3 (If applied)
                string taxName3 = clsCN.sqlDT.Rows[0]["TaxName3"].ToString();
                double taxRate3;
                if (clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxName3"].ToString()) > 0)
                {
                    taxRate3 = clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxRate3"].ToString());
                }
                else { taxRate3 = 0; }

                clsCN.ExecuteSQLQuery(" SELECT *  FROM  SaleDetails   WHERE        (INVOICE_NO = '" + INVOICE_NO + "') AND (PRODUCT_ID = '" + PRODUCT_ID + "') ");
                if (clsCN.sqlDT.Rows.Count > 0) {
                    clsCN.ExecuteSQLQuery("UPDATE SaleDetails  SET QTY= QTY+'" + QTY + "', CostPrice=CostPrice+'" + (QTY * CostPrice) + "', RetailPrice=RetailPrice+'" + (QTY * RetailPrice) + "',  taxAmount1=taxAmount1+'" + (QTY * RetailPrice) * taxRate1 / 100 + "',  taxAmount2=taxAmount2+'" + (QTY * RetailPrice) * taxRate2 / 100 + "',  taxAmount3=taxAmount3+'" + (QTY * RetailPrice) * taxRate3 / 100 + "' WHERE        (INVOICE_NO = '" + INVOICE_NO + "') AND (PRODUCT_ID = '" + PRODUCT_ID + "') ");
                    clsCN.ExecuteSQLQuery(" UPDATE Product SET Quantity=Quantity -'" + QTY + "' WHERE PRODUCT_ID='" + PRODUCT_ID + "' ");
                }
                else {
                    clsCN.ExecuteSQLQuery("INSERT INTO SaleDetails (INVOICE_NO, QTY, CostPrice, RetailPrice, taxName1, taxAmount1, taxName2, taxAmount2, taxName3, taxAmount3, Notes, PRODUCT_ID) VALUES ('" + INVOICE_NO + "', '" + QTY.ToString() + "', '" + (QTY * CostPrice) + "',  '" + (QTY * RetailPrice) + "', '" + taxName1.ToString() + "', '" + (QTY * RetailPrice) * taxRate1 / 100 + "', '" + taxName2.ToString() + "', '" + (QTY * RetailPrice) * taxRate2 / 100 + "', '" + taxName3.ToString() + "', '" + (QTY * RetailPrice) * taxRate3 / 100 + "' , '-', '" + PRODUCT_ID + "')");
                    clsCN.ExecuteSQLQuery(" UPDATE Product SET Quantity=Quantity -'" + QTY + "' WHERE PRODUCT_ID='" + PRODUCT_ID + "' ");
                }

                LoadCartItem(INVOICE_NO);

            ////////////
            }
            else { MessageBox.Show("There is no stock of this quantity.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void LoadCartItem(string INVOICE_NO) {
            clsCN.FillDataGrid(" SELECT  SaleDetails.id, Product.PRODUCT_ID, Product.ProductName, SaleDetails.QTY, Product.UnitOfMeasure, SaleDetails.RetailPrice " +
                               " FROM   SaleDetails LEFT OUTER JOIN  Product ON SaleDetails.PRODUCT_ID = Product.PRODUCT_ID  WHERE        (SaleDetails.INVOICE_NO = '" + INVOICE_NO + "') ", ItemDataGridView);
            double OrderTotal = 0, TaxTotal = 0;
            clsCN.ExecuteSQLQuery(" SELECT        Customer.Cust_Name, ManageTables.Table_Code, Sale.Freight, Sale.Discount, Sale.PaymentMethod  FROM            Sale LEFT OUTER JOIN  " +
                                  " ManageTables ON Sale.TABLE_ID = ManageTables.TABLE_ID LEFT OUTER JOIN    Customer ON Sale.CUST_ID = Customer.CUST_ID  WHERE        (Sale.INVOICE_NO = '" + INVOICE_NO + "')  ");
            txtDiscount.Text = clsCN.sqlDT.Rows[0]["Discount"].ToString();
            txtCustomerName.Text = clsCN.sqlDT.Rows[0]["Cust_Name"].ToString();
            txtTableNo.Text = clsCN.sqlDT.Rows[0]["Table_Code"].ToString();
            txtPaymentMethod.Text = clsCN.sqlDT.Rows[0]["PaymentMethod"].ToString();

            clsCN.ExecuteSQLQuery(" SELECT  taxAmount1, taxAmount2, taxAmount3  FROM  SaleDetails   WHERE  (INVOICE_NO = '" + INVOICE_NO + "') ");
            if (clsCN.sqlDT.Rows.Count > 0) {
                clsCN.ExecuteSQLQuery("SELECT   SUM(taxAmount1 + taxAmount2 + taxAmount3) AS Expr1  FROM   SaleDetails   WHERE        (INVOICE_NO = '" + INVOICE_NO + "')");
                TaxTotal = clsCN.num_repl(clsCN.sqlDT.Rows[0]["Expr1"].ToString());
                clsCN.ExecuteSQLQuery("SELECT   SUM(RetailPrice) AS Expr1 FROM   SaleDetails   WHERE  (INVOICE_NO = '" + INVOICE_NO + "') ");
                OrderTotal = clsCN.num_repl(clsCN.sqlDT.Rows[0]["Expr1"].ToString());
            }
            clsCN.ExecuteSQLQuery("UPDATE Sale SET  OrderTotal='" + OrderTotal + "', TaxTotal='" + TaxTotal + "' WHERE  (INVOICE_NO = '" + INVOICE_NO + "')  ");
            txtOrderTotal.Text = OrderTotal.ToString();
            txtTaxTotal.Text = TaxTotal.ToString();

            clsCN.ExecuteSQLQuery(" SELECT   SUM(TaxTotal + Freight + OrderTotal - Discount) AS Expr1  FROM   Sale   WHERE  (INVOICE_NO = '" + INVOICE_NO + "') ");
            txtGTotal.Text = clsCN.sqlDT.Rows[0]["Expr1"].ToString();


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            { LoadCartItem(txtInvoiceNo.Text); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gbAddItem.Visible = false;
        }

        private void ItemDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                txtProductID.Text = ItemDataGridView.CurrentRow.Cells[3].Value.ToString();
                gbAddItem.Visible = true;
                txtAddMoreQty.Focus();
            }
            else if (e.ColumnIndex == 1)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete this item?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" UPDATE Product SET Quantity = Quantity + '" + clsCN.num_repl(ItemDataGridView.CurrentRow.Cells[5].Value.ToString()) + "'   WHERE (PRODUCT_ID = '" + ItemDataGridView.CurrentRow.Cells[3].Value.ToString() + "') ");
                    clsCN.ExecuteSQLQuery(" DELETE SaleDetails  WHERE    (id = '" + ItemDataGridView.CurrentRow.Cells[2].Value.ToString() + "') ");
                    btnRefresh.PerformClick();
                }
            }
        }

        private void btnAddMoreQty_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
            /////////////////////
            if (txtAddMoreQty.Text != "")
            {
                AddItemToCart(txtInvoiceNo.Text, txtProductID.Text, clsCN.num_repl(txtAddMoreQty.Text));
                gbAddItem.Visible = false;
                txtAddMoreQty.Text = "";
            }
                /////////////////////
            }
            else { MessageBox.Show("Empty invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        private void txtAddMoreQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (!(cmbProducts.SelectedValue == null) | !(cmbProducts.SelectedIndex == -1))
            {
                gbAddItem.Visible = true;
                txtAddMoreQty.Focus();
                txtAddMoreQty.Text = "1";
                txtProductID.Text = cmbProducts.SelectedValue.ToString();
            }
            else { MessageBox.Show("You have not selected any product.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
            frmTableDiagram frmTableDiagram = new frmTableDiagram();
            frmTableDiagram.txtInvoiceNo.Text = txtInvoiceNo.Text;
            frmTableDiagram.ShowDialog();
            }
            else { MessageBox.Show("Empty invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnPrintView_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "") {
                        DialogResult msg = new DialogResult();
            msg = MessageBox.Show("Do you really want to print view this invoice?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == DialogResult.Yes)
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
            else { MessageBox.Show("Empty invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
            frmPosOption frmPosOption = new frmPosOption();
            frmPosOption.txtInvoiceNo.Text = txtInvoiceNo.Text;
            frmPosOption.ShowDialog();
            }
            else { MessageBox.Show("Empty invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnGiftItem_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
                frmSalesGift frmSalesGift = new frmSalesGift();
                frmSalesGift.txtInvoiceNo.Text = txtInvoiceNo.Text;
                frmSalesGift.ShowDialog();

            }
            else { MessageBox.Show("Empty invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnHoldList_Click(object sender, EventArgs e)
        {
            frmHoldInvoiceList frmHoldInvoiceList = new frmHoldInvoiceList();
            GlobalVariables.TEMP_INVOICE_NO = txtInvoiceNo.Text;
            frmHoldInvoiceList.ShowDialog();
            txtInvoiceNo.Text = frmHoldInvoiceList.Invoice_No;
        }

        private void btnHoldInvoice_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to hold this invoice?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" UPDATE Sale SET  Status='H' WHERE        (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
                    DateTime today = DateTime.Today;
                    clsCN.ExecuteSQLQuery(" INSERT INTO Sale (CUST_ID ,  Sales_Date , USER_ID  , TABLE_ID , TaxTotal , Freight , OrderTotal , Discount , TotalAmount , PaymentAmount , ChangeAmount , PaymentMethod , Status , Comment ) VALUES (0 ,  '" + today.ToString("MM/dd/yyyy") + "' , '" + GlobalVariables.UserID + "'  , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 'DUE' , 'N' , '-') ");
                    clsCN.ExecuteSQLQuery("SELECT  INVOICE_NO   FROM   Sale  ORDER BY INVOICE_NO DESC");
                    txtInvoiceNo.Text = clsCN.sqlDT.Rows[0]["INVOICE_NO"].ToString();
                    LoadCartItem(txtInvoiceNo.Text);
                    MessageBox.Show("New invoice has been created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBarcode.Focus();
                }
            }
            else { MessageBox.Show("Empty invoice.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != "")
            {
                btnRefresh.PerformClick();
            }
        }





    }
}
