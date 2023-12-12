using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmProductList : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public frmProductList()
        {
            InitializeComponent();
        }

        private void frmProductList_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            clsCN.FillComboBox(" SELECT  CAT_ID, Cat_Name  FROM  Categories  ORDER BY Cat_Name", "CAT_ID", "Cat_Name", cmbCategory);
            LoadData();
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void LoadData()
        {
            string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, Product.CostPrice, Product.RetailPrice, TAX.Tax_Name AS TaxName1, Product.TaxRate1, TAX_1.Tax_Name AS TaxName2,  Product.TaxRate2, TAX_2.Tax_Name AS TaxName3,  " +
                            " Product.TaxRate3, Product.Quantity, Product.UnitOfMeasure, Product.ReorderLevel, Product.ProdStatus FROM            Product LEFT OUTER JOIN  TAX AS TAX_2 ON Product.TaxName3 = TAX_2.TAX_ID LEFT OUTER JOIN " +
                            " Categories ON Product.CAT_ID = Categories.CAT_ID LEFT OUTER JOIN TAX AS TAX_1 ON Product.TaxName2 = TAX_1.TAX_ID LEFT OUTER JOIN TAX ON Product.TaxName1 = TAX.TAX_ID WHERE (Product.Inventory = 'Y')  ORDER BY Product.ProductName ";
            clsCN.FillDataGrid(sqlStr, ProductDataGridView);
            clsCN.ExecuteSQLQuery(sqlStr);
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                lblCountProduct.Text = "Total " + clsCN.sqlDT.Rows.Count.ToString() + " Product(s) found.";
            }
            else { lblCountProduct.Text = "Total 0 Product found."; }
        }

        private void btnFilterByCategory_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedValue == null | cmbCategory.SelectedIndex == -1)
            { MessageBox.Show("Please select a category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, Product.CostPrice, Product.RetailPrice, TAX.Tax_Name AS TaxName1, Product.TaxRate1, TAX_1.Tax_Name AS TaxName2,  Product.TaxRate2, TAX_1.Tax_Name AS TaxName3,  Product.TaxRate3, Product.Quantity, Product.UnitOfMeasure, Product.ReorderLevel, Product.ProdStatus FROM  " +
                                " Product LEFT OUTER JOIN  TAX AS TAX_1 ON Product.TaxName2 = TAX_1.TAX_ID LEFT OUTER JOIN TAX ON Product.TaxName1 = TAX.TAX_ID LEFT OUTER JOIN  Categories ON Product.CAT_ID = Categories.CAT_ID  WHERE (Product.CAT_ID = '" + cmbCategory.SelectedValue.ToString() + "') AND (Product.Inventory = 'Y')  ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
                clsCN.ExecuteSQLQuery(sqlStr);
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    lblCountProduct.Text = "Total " + clsCN.sqlDT.Rows.Count.ToString() + " Product(s) found.";
                }
                else { lblCountProduct.Text = "Total 0 Product found."; }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            { LoadData(); }
            else {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, Product.CostPrice, Product.RetailPrice, TAX.Tax_Name AS TaxName1, Product.TaxRate1, TAX_1.Tax_Name AS TaxName2,  Product.TaxRate2, TAX_2.Tax_Name AS TaxName3,    " +
                                " Product.TaxRate3, Product.Quantity, Product.UnitOfMeasure, Product.ReorderLevel, Product.ProdStatus FROM            Product LEFT OUTER JOIN  TAX AS TAX_2 ON Product.TaxName3 = TAX_2.TAX_ID LEFT OUTER JOIN " +
                                " Categories ON Product.CAT_ID = Categories.CAT_ID LEFT OUTER JOIN TAX AS TAX_1 ON Product.TaxName2 = TAX_1.TAX_ID LEFT OUTER JOIN TAX ON Product.TaxName1 = TAX.TAX_ID  WHERE   (Product.ProductName LIKE '" + clsCN.str_repl(txtProductName.Text) + "%') AND (Product.Inventory = 'Y') ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
                clsCN.ExecuteSQLQuery(sqlStr);
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    lblCountProduct.Text = "Total " + clsCN.sqlDT.Rows.Count.ToString() + " Product(s) found.";
                }
                else { lblCountProduct.Text = "Total 0 Product found."; }
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarcode.Text))
            { LoadData(); }
            else
            {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, Product.CostPrice, Product.RetailPrice, TAX.Tax_Name AS TaxName1, Product.TaxRate1, TAX_1.Tax_Name AS TaxName2,  Product.TaxRate2, TAX_2.Tax_Name AS TaxName3,    " +
                                " Product.TaxRate3, Product.Quantity, Product.UnitOfMeasure, Product.ReorderLevel, Product.ProdStatus FROM            Product LEFT OUTER JOIN  TAX AS TAX_2 ON Product.TaxName3 = TAX_2.TAX_ID LEFT OUTER JOIN " +
                                " Categories ON Product.CAT_ID = Categories.CAT_ID LEFT OUTER JOIN TAX AS TAX_1 ON Product.TaxName2 = TAX_1.TAX_ID LEFT OUTER JOIN TAX ON Product.TaxName1 = TAX.TAX_ID  WHERE   (Product.UPC_EAN LIKE '" + clsCN.str_repl(txtBarcode.Text) + "%') AND (Product.Inventory = 'Y') ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
                clsCN.ExecuteSQLQuery(sqlStr);
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    lblCountProduct.Text = "Total " + clsCN.sqlDT.Rows.Count.ToString() + " Product(s) found.";
                }
                else { lblCountProduct.Text = "Total 0 Product found."; }
            }
        }

        private void ProductDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to edit record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" SELECT * FROM  Product  WHERE PRODUCT_ID ='" + ProductDataGridView.CurrentRow.Cells[2].Value.ToString() + "' ");
                    if (clsCN.sqlDT.Rows.Count > 0)
                    {
                        frmProductInformation frmProductInformation = new frmProductInformation();
                        frmProductInformation.MdiParent = this.ParentForm;
                        frmProductInformation.Show();
                        frmProductInformation.btnSubmit.Text = "UPDATE";
                        frmProductInformation.txtProdID.Text = clsCN.sqlDT.Rows[0]["PRODUCT_ID"].ToString();
                        frmProductInformation.txtProductName.Text = clsCN.sqlDT.Rows[0]["ProductName"].ToString();
                        frmProductInformation.txtBarcode.Text = clsCN.sqlDT.Rows[0]["UPC_EAN"].ToString();
                        frmProductInformation.cmbCategory.SelectedValue = clsCN.sqlDT.Rows[0]["CAT_ID"].ToString();
                        frmProductInformation.txtUOM.Text = clsCN.sqlDT.Rows[0]["UnitOfMeasure"].ToString();
                        frmProductInformation.txtQuantity.Text = clsCN.sqlDT.Rows[0]["Quantity"].ToString();
                        frmProductInformation.txtCostPrice.Text = clsCN.sqlDT.Rows[0]["CostPrice"].ToString();
                        frmProductInformation.txtRetailPrice.Text = clsCN.sqlDT.Rows[0]["RetailPrice"].ToString();
                        frmProductInformation.txtReOrderQty.Text = clsCN.sqlDT.Rows[0]["ReorderLevel"].ToString();
                        frmProductInformation.cmbTax1.SelectedValue = clsCN.sqlDT.Rows[0]["TaxName1"].ToString();
                        frmProductInformation.cmbTax2.SelectedValue = clsCN.sqlDT.Rows[0]["TaxName2"].ToString();
                        frmProductInformation.cmbTax3.SelectedValue = clsCN.sqlDT.Rows[0]["TaxName3"].ToString();
                        frmProductInformation.txtTaxRate1.Text = clsCN.sqlDT.Rows[0]["TaxRate1"].ToString();
                        frmProductInformation.txtTaxRate2.Text = clsCN.sqlDT.Rows[0]["TaxRate2"].ToString();
                        frmProductInformation.txtTaxRate3.Text = clsCN.sqlDT.Rows[0]["TaxRate3"].ToString();
                        if (clsCN.sqlDT.Rows[0]["ProdStatus"].ToString() == "Y") { frmProductInformation.rbActive.Checked = true; }
                        else { frmProductInformation.rbDeactive.Checked = true; }
                        Byte[] MyData = new byte[0];
                        MyData = (Byte[])clsCN.sqlDT.Rows[0]["ProductPhoto"];
                        MemoryStream stream = new MemoryStream(MyData);
                        stream.Position = 0;
                        frmProductInformation.pictureBox1.BackgroundImage = Image.FromStream(stream);
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" DELETE  Product  WHERE PRODUCT_ID ='" + ProductDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Data Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


    }
}
