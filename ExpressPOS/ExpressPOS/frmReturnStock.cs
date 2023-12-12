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
    public partial class frmReturnStock : Form
    {

        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmReturnStock()
        {
            InitializeComponent();
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

        private void LoadData()
        {
            string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, " +
                               " Product.Quantity  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID WHERE (Product.ProdStatus = 'Y')  ORDER BY Product.ProductName";
            clsCN.FillDataGrid(sqlStr, ProductDataGridView);
        }


        private void frmReturnStock_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            clsCN.FillComboBox(" SELECT  CAT_ID, Cat_Name  FROM  Categories  ORDER BY Cat_Name", "CAT_ID", "Cat_Name", cmbCategory);
            clsCN.FillComboBoxColumn(" SELECT  SUPP_ID, SupplierName  FROM  Supplier  ORDER BY SupplierName", "SUPP_ID", "SupplierName", cmbSupplier);
            LoadData();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            { LoadData(); }
            else
            {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, " +
                                " Product.Quantity  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID WHERE (Product.ProdStatus = 'Y') AND   (Product.ProductName LIKE '" + clsCN.str_repl(txtProductName.Text) + "%')  ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarcode.Text))
            { LoadData(); }
            else
            {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, " +
                                " Product.Quantity  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID WHERE (Product.ProdStatus = 'Y') AND   (Product.UPC_EAN LIKE '" + clsCN.str_repl(txtBarcode.Text) + "%')  ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
            }
        }

        private void btnFilterByCategory_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedValue == null | cmbCategory.SelectedIndex == -1)
            { MessageBox.Show("Please select a category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, " +
                    " Product.Quantity  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID WHERE (Product.ProdStatus = 'Y') AND   (Product.CAT_ID = '" + cmbCategory.SelectedValue.ToString() + "')  ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ProductDataGridView.RowCount > 0)
            {
                string product_list = string.Empty;
                foreach (DataGridViewRow row in ProductDataGridView.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Column6"].Value);
                    if (isSelected)
                    {
                        product_list += Environment.NewLine;
                        product_list += row.Cells["Column1"].Value.ToString();
                    }
                }
                /////////////////////////
                if (product_list != "")
                {
                    //////////////////////
                    foreach (DataGridViewRow Row in ProductDataGridView.Rows)
                    {
                        if (Row.Cells[0].Value != null)
                        {
                            if ((bool)(Row.Cells[0].Value) == true)
                            {
                                string product_id = ProductDataGridView.Rows[Row.Index].Cells["Column1"].Value.ToString();
                                double stock_qty = clsCN.num_repl(ProductDataGridView.Rows[Row.Index].Cells["Column11"].Value.ToString());

                                double return_qty;
                                try { return_qty = clsCN.num_repl(ProductDataGridView.Rows[Row.Index].Cells["Column5"].Value.ToString()); }
                                catch { return_qty = 0; }

                                int supplier_id = Convert.ToInt32(ProductDataGridView.Rows[Row.Index].Cells["cmbSupplier"].Value);

                                clsCN.ExecuteSQLQuery("SELECT *  FROM Product  WHERE PRODUCT_ID= '" + product_id + "' ");
                                double stock_unit_cost = Convert.ToDouble(clsCN.sqlDT.Rows[0]["CostPrice"]);

                                double total_unit = stock_qty - return_qty;
                                double total_return_cost = return_qty * stock_unit_cost;


                                clsCN.ExecuteSQLQuery(" UPDATE Product SET Quantity = '" + total_unit + "'  WHERE PRODUCT_ID = '" + product_id + "' ");
                                clsCN.ExecuteSQLQuery(" INSERT INTO StockMovement (PRODUCT_ID, SUPP_ID , EntryDate, Quantity, TotalCost, Stock) VALUES ('" + product_id + "', '" + supplier_id + "', '" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "', '" + return_qty + "', '" + total_return_cost + "', 'Return') ");
                            }
                        }
                    }
                    LoadData();
                    MessageBox.Show("Stock updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //////////////////////
                }
                else { MessageBox.Show("You have not picked any products.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                /////////////////////////
            }
            else { MessageBox.Show("Product Empty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}
