using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; 
using Excel = Microsoft.Office.Interop.Excel;

namespace ExpressPOS
{
    public partial class frmImportProduct : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

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


        public frmImportProduct()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }


        private void btnSaveData_Click(object sender, EventArgs e)
        {
           
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {
                OpenFileDialog.Title = "Microsoft Excel File...";
                OpenFileDialog.Filter = "Excel Files (xlsx)|*.xlsx;";
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = OpenFileDialog.FileName;
                    ////////////
                    try
                    {
                        ///////////////////////////
                        string conStr, sheetName;
                        conStr = string.Format(Excel07ConString, txtFilePath.Text, "YES");
                        //Get the name of the First Sheet.
                        using (OleDbConnection con = new OleDbConnection(conStr))
                        {
                            using (OleDbCommand cmd = new OleDbCommand())
                            {
                                cmd.Connection = con;
                                con.Open();
                                DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                con.Close();
                            }
                        }
                        //Read Data from the First Sheet.
                        using (OleDbConnection con = new OleDbConnection(conStr))
                        {
                            using (OleDbCommand cmd = new OleDbCommand())
                            {
                                using (OleDbDataAdapter oda = new OleDbDataAdapter())
                                {
                                    DataTable dt = new DataTable();
                                    cmd.CommandText = "SELECT * From [" + sheetName + "]";
                                    cmd.Connection = con;
                                    con.Open();
                                    oda.SelectCommand = cmd;
                                    oda.Fill(dt);
                                    con.Close();
                                    //Populate DataGridView.
                                    ProductDataGridView.DataSource = dt;
                                }
                            }
                        }
                        ///////////////////////////
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    ////////////
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try {
                if (ProductDataGridView.RowCount > 0)
                {
                    DialogResult msg = new DialogResult();
                    msg = MessageBox.Show("Total " + ProductDataGridView.RowCount.ToString () + " product(s) found. Click Yes to save this data.", "Import Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        int i = 0;
                        for (i = 0; i <= ProductDataGridView.RowCount - 1; i++)
                        {
                            string PRODUCT_NAME;
                            try { PRODUCT_NAME = ProductDataGridView.Rows[i].Cells["PRODUCT_NAME"].Value.ToString(); }
                            catch { PRODUCT_NAME = ""; }

                            string UPC_EAN ;
                            try { UPC_EAN = ProductDataGridView.Rows[i].Cells["UPC_EAN"].Value.ToString(); }
                            catch { UPC_EAN = ""; }

                            double CAT_ID ;
                            try { CAT_ID = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["CAT_ID"].Value.ToString()); }
                            catch { CAT_ID = 0; }

                            double COST;
                            try{ COST = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["COST"].Value.ToString()); }
                            catch{ COST=0; }

                            double RETAIL;
                            try{ RETAIL = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["RETAIL"].Value.ToString()); }
                            catch{ RETAIL= 0;}

                            double TAX_NAME_1;
                            try{ TAX_NAME_1 = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["TAX_NAME_1"].Value.ToString()); }
                            catch{TAX_NAME_1=0;}


                            double TAX_RATE_1;
                            try{ TAX_RATE_1 = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["TAX_RATE_1"].Value.ToString()); }
                            catch{TAX_RATE_1 =0;}

                            double TAX_NAME_2;
                            try{ TAX_NAME_2 = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["TAX_NAME_2"].Value.ToString()); }
                            catch{TAX_NAME_2 =0;}

                            double TAX_RATE_2 ;
                            try{ TAX_RATE_2 = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["TAX_RATE_2"].Value.ToString()); }
                            catch{ TAX_RATE_2=0;}

                            double  TAX_NAME_3;
                            try { TAX_NAME_3 = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["TAX_NAME_3"].Value.ToString()); }
                            catch{TAX_NAME_3 =0;}

                            double TAX_RATE_3 ;
                            try  { TAX_RATE_3 = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["TAX_RATE_3"].Value.ToString()); }
                            catch{ TAX_RATE_3 =0; }

                            double QUANTITY ;
                            try  { QUANTITY = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["QUANTITY"].Value.ToString()); }
                            catch{QUANTITY =0;}

                            string UNIT_OF_MEASURE ;
                            try  {  UNIT_OF_MEASURE = ProductDataGridView.Rows[i].Cells["UNIT_OF_MEASURE"].Value.ToString(); }
                            catch{ UNIT_OF_MEASURE =""; }


                            double REORDER_LEVEL;
                            try{ REORDER_LEVEL = clsCN.num_repl(ProductDataGridView.Rows[i].Cells["REORDER_LEVEL"].Value.ToString()); }
                            catch{ REORDER_LEVEL =0; }

                            string STATUS;
                            try  { STATUS = ProductDataGridView.Rows[i].Cells["STATUS"].Value.ToString(); }
                            catch{ STATUS = "N"; }

                            string INVENTORY ;
                            try{  INVENTORY = ProductDataGridView.Rows[i].Cells["INVENTORY"].Value.ToString(); }
                            catch{  INVENTORY = "Y"; }

                            frmProductInformation frmProductInformation = new frmProductInformation();

                            clsCN.ExecuteSQLQuery("INSERT INTO Product (ProductName, UPC_EAN, CAT_ID, CostPrice, RetailPrice, TaxName1, TaxRate1, TaxName2, TaxRate2, Quantity, UnitOfMeasure, ReorderLevel, ProdStatus, Inventory, TaxName3, TaxRate3) VALUES ('" + PRODUCT_NAME + "', '" + UPC_EAN + "' , '" + CAT_ID + "', '" + COST + "', '" + RETAIL + "', '" + TAX_NAME_1 + "', '" + TAX_RATE_1 + "', '" + TAX_NAME_2 + "', '" + TAX_RATE_2 + "', '" + QUANTITY + "', '" + UNIT_OF_MEASURE + "', '" + REORDER_LEVEL + "', '" + STATUS + "', '"+ INVENTORY +"', '" + TAX_NAME_3 + "', '" + TAX_RATE_3 + "' )");
                            clsCN.ExecuteSQLQuery("SELECT  PRODUCT_ID  FROM   Product    ORDER BY PRODUCT_ID DESC");
                            string PRODUCT_ID = clsCN.sqlDT.Rows[0]["PRODUCT_ID"].ToString();
                            clsCN.ProductPhotoUpload(PRODUCT_ID, frmProductInformation.pictureBox1);
                        }

                        MessageBox.Show("Product(s) Uploded sucessfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No product(s) were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


       
    }
}
