using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmExportProduct : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmExportProduct()
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

        private void frmExportProduct_Load(object sender, EventArgs e)
        {
            string sqlStr = " SELECT        ProductName AS PRODUCT_NAME, UPC_EAN, CAT_ID, CostPrice AS COST, RetailPrice AS RETAIL, TaxName1 AS TAX_NAME_1, TaxRate1 AS TAX_RATE_1, TaxName2 AS TAX_NAME_2,  " +
                            " TaxRate2 AS TAX_RATE_2, TaxName3 AS TAX_NAME_3, TaxRate3 AS TAX_RATE_3, Quantity AS QUANTITY, UnitOfMeasure AS UNIT_OF_MEASURE, ReorderLevel AS REORDER_LEVEL, ProdStatus AS STATUS,  " +
                            "  Inventory AS INVENTORY  FROM    Product ";
            clsCN.FillDataGrid(sqlStr, ProductDataGridView);
        }

        private void btnExport_Click(object sender, EventArgs e)
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
