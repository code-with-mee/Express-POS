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
    public partial class frmCurrentStock : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmCurrentStock()
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

        private void LoadData()
        {
            string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name, " +
                            " Product.Quantity, Product.CostPrice  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID WHERE (Product.ProdStatus = 'Y')  ORDER BY Product.ProductName";
            clsCN.FillDataGrid(sqlStr, ProductDataGridView);
            clsCN.ExecuteSQLQuery(sqlStr);
            if (clsCN.sqlDT.Rows.Count > 0) {
                clsCN.ExecuteSQLQuery("SELECT  SUM(CostPrice * Quantity) AS Expr1   FROM   Product");
                txtTotalValue.Text = "Total = " + clsCN.sqlDT.Rows[0]["Expr1"].ToString();
            }
            else { txtTotalValue.Text = "Total = 0.00"; }
        }

        private void frmCurrentStock_Load(object sender, EventArgs e)
        {
            clsCN.FillComboBox(" SELECT  CAT_ID, Cat_Name  FROM  Categories  ORDER BY Cat_Name", "CAT_ID", "Cat_Name", cmbCategory);
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            { LoadData(); }
            else
            {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name,  " +
                                 " Product.Quantity, Product.CostPrice  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID  WHERE   (Product.ProductName LIKE '" + clsCN.str_repl(txtProductName.Text) + "%') AND (Product.ProdStatus = 'Y') ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
                clsCN.ExecuteSQLQuery(sqlStr);
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    clsCN.ExecuteSQLQuery("SELECT  SUM(CostPrice * Quantity) AS Expr1   FROM   Product WHERE   (ProductName LIKE '" + clsCN.str_repl(txtProductName.Text) + "%') AND (ProdStatus = 'Y')");
                    txtTotalValue.Text = "Total = " + clsCN.sqlDT.Rows[0]["Expr1"].ToString();
                }
                else { txtTotalValue.Text = "Total = 0.00"; }
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarcode.Text))
            { LoadData(); }
            else
            {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name,  " +
                                 " Product.Quantity, Product.CostPrice  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID  WHERE   (Product.UPC_EAN LIKE '" + clsCN.str_repl(txtBarcode.Text) + "%') AND (Product.ProdStatus = 'Y') ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
                clsCN.ExecuteSQLQuery(sqlStr);
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    clsCN.ExecuteSQLQuery("SELECT  SUM(CostPrice * Quantity) AS Expr1   FROM   Product WHERE   (UPC_EAN LIKE '" + clsCN.str_repl(txtBarcode.Text) + "%') AND (ProdStatus = 'Y')");
                    txtTotalValue.Text = "Total = " + clsCN.sqlDT.Rows[0]["Expr1"].ToString();
                }
                else { txtTotalValue.Text = "Total = 0.00"; }
            }
        }

        private void btnFilterByCategory_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedValue == null | cmbCategory.SelectedIndex == -1)
            { MessageBox.Show("Please select a category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                string sqlStr = " SELECT        Product.PRODUCT_ID, Product.ProductName, Product.UPC_EAN, Categories.Cat_Name,  " +
                                 " Product.Quantity, Product.CostPrice  FROM            Product LEFT OUTER JOIN Categories ON Product.CAT_ID = Categories.CAT_ID  WHERE   (Product.CAT_ID = '" + cmbCategory.SelectedValue.ToString() + "') AND (Product.ProdStatus = 'Y') ORDER BY Product.ProductName";
                clsCN.FillDataGrid(sqlStr, ProductDataGridView);
                clsCN.ExecuteSQLQuery(sqlStr);
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    clsCN.ExecuteSQLQuery("SELECT  SUM(CostPrice * Quantity) AS Expr1   FROM   Product WHERE   (CAT_ID = '" + cmbCategory.SelectedValue.ToString() + "')  AND (ProdStatus = 'Y')");
                    txtTotalValue.Text = "Total = " + clsCN.sqlDT.Rows[0]["Expr1"].ToString();
                }
                else { txtTotalValue.Text = "Total = 0.00"; }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;
                for (j = 0; j < ProductDataGridView.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = ProductDataGridView.Columns[j].HeaderText;
                }
                StartRow++;
                for (i = 0; i < ProductDataGridView.Rows.Count; i++)
                {
                    for (j = 0; j < ProductDataGridView.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = ProductDataGridView[j, i].Value == null ? "" : ProductDataGridView[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
