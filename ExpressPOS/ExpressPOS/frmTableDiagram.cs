using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmTableDiagram : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmTableDiagram()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }


        private void AddItemToCart(string INVOICE_NO, string PRODUCT_ID, double QTY)
        {
            clsCN.ExecuteSQLQuery("SELECT *  FROM    Product  WHERE   (PRODUCT_ID = '" + PRODUCT_ID + "') AND (Quantity >= '" + clsCN.num_repl(QTY.ToString()) + "')");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                double CostPrice = clsCN.num_repl(clsCN.sqlDT.Rows[0]["CostPrice"].ToString());
                double RetailPrice = clsCN.num_repl(clsCN.sqlDT.Rows[0]["RetailPrice"].ToString());

                //////// Get tax 1(If applied)
                string taxName1 = clsCN.sqlDT.Rows[0]["TaxName1"].ToString();
                double taxRate1;
                if (clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxName1"].ToString()) > 0)
                {
                    taxRate1 = clsCN.num_repl(clsCN.sqlDT.Rows[0]["TaxRate1"].ToString());
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
                if (clsCN.sqlDT.Rows.Count > 0)
                {
                    clsCN.ExecuteSQLQuery("UPDATE SaleDetails  SET QTY= QTY+'" + QTY + "', CostPrice=CostPrice+'" + (QTY * CostPrice) + "', RetailPrice=RetailPrice+'" + (QTY * RetailPrice) + "',  taxAmount1=taxAmount1+'" + (QTY * RetailPrice) * taxRate1 / 100 + "',  taxAmount2=taxAmount2+'" + (QTY * RetailPrice) * taxRate2 / 100 + "',  taxAmount3=taxAmount3+'" + (QTY * RetailPrice) * taxRate3 / 100 + "' WHERE        (INVOICE_NO = '" + INVOICE_NO + "') AND (PRODUCT_ID = '" + PRODUCT_ID + "') ");
                    clsCN.ExecuteSQLQuery(" UPDATE Product SET Quantity=Quantity -'" + QTY + "' WHERE PRODUCT_ID='" + PRODUCT_ID + "' ");
                }
                else
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO SaleDetails (INVOICE_NO, QTY, CostPrice, RetailPrice, taxName1, taxAmount1, taxName2, taxAmount2, taxName3, taxAmount3, Notes, PRODUCT_ID) VALUES ('" + INVOICE_NO + "', '" + QTY.ToString() + "', '" + (QTY * CostPrice) + "',  '" + (QTY * RetailPrice) + "', '" + taxName1.ToString() + "', '" + (QTY * RetailPrice) * taxRate1 / 100 + "', '" + taxName2.ToString() + "', '" + (QTY * RetailPrice) * taxRate2 / 100 + "', '" + taxName3.ToString() + "', '" + (QTY * RetailPrice) * taxRate3 / 100 + "' , '-', '" + PRODUCT_ID + "')");
                    clsCN.ExecuteSQLQuery(" UPDATE Product SET Quantity=Quantity -'" + QTY + "' WHERE PRODUCT_ID='" + PRODUCT_ID + "' ");
                }

                ////////////
            }
            else { MessageBox.Show("There is no stock of this quantity.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadTable() {
            //////////////////////
            TablePanelView.Controls.Clear();
            clsCN.ExecuteSQLQuery(" SELECT  * FROM  ManageTables ");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                int i;
                for (i = 0; i <= clsCN.sqlDT.Rows.Count - 1; i++)
                {
                    Button picturebx = new Button();
                    picturebx.TextImageRelation = TextImageRelation.ImageBeforeText;
                    picturebx.TextAlign = ContentAlignment.MiddleLeft;
                    picturebx.ImageAlign = ContentAlignment.MiddleLeft;
                    picturebx.Name = clsCN.sqlDT.Rows[i]["TABLE_ID"].ToString();
                    picturebx.Text += Environment.NewLine + "Code: " + clsCN.sqlDT.Rows[i]["Table_Code"];
                    picturebx.Text += Environment.NewLine + "Name: " + clsCN.sqlDT.Rows[i]["Table_Name"];
                    if (clsCN.sqlDT.Rows[i]["Booked"].ToString() == "Y") { picturebx.BackColor = Color.MistyRose; }
                    else {picturebx.BackColor = Color.White; }
                    picturebx.Size = new System.Drawing.Size(190, 95);
                    picturebx.Image = resizeImage(ExpressPOS.Properties.Resources.restaurant, new Size(80, 80));
                    picturebx.Click += Table_Click;
                    TablePanelView.Controls.Add(picturebx);
                }
            }
            //////////////////////
        }

        private void frmTableDiagram_Load(object sender, EventArgs e)
        {
            LoadTable();
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void Table_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            clsCN.ExecuteSQLQuery(" UPDATE ManageTables SET  Booked ='Y'  WHERE        (TABLE_ID = '" + button.Name.ToString() +"') ");
            clsCN.ExecuteSQLQuery(" UPDATE Sale SET  TABLE_ID ='" + button.Name + "'  WHERE        (INVOICE_NO = '" + clsCN.str_repl(txtInvoiceNo.Text) + "') ");
            LoadTable();
            MessageBox.Show("Table Booked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
