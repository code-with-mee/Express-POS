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
    public partial class frmPrintBarcode : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public frmPrintBarcode()
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

        private void frmPrintBarcode_Load(object sender, EventArgs e)
        {
            clsCN.FillComboBox(" SELECT  *  FROM Product WHERE (ProdStatus = 'Y') ORDER BY ProductName", "PRODUCT_ID", "ProductName", cmbProducts);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string CompanyName= null;
            if (chkCompanyName.Checked) { clsCN.ExecuteSQLQuery("SELECT * FROM BusinessInfo");
            if (clsCN.sqlDT.Rows.Count > 0) { CompanyName = clsCN.sqlDT.Rows[0]["BusinessName"].ToString();}
            else{CompanyName="";} }


            if (cmbProducts.SelectedValue == null | cmbProducts.SelectedIndex == -1)
            { MessageBox.Show("Please select a product.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (string.IsNullOrEmpty(txtQuantity.Text))
            { MessageBox.Show("Please enter barcode label quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                int  i, cnt, xHold, holdi;
                holdi = 0;
                cnt = 1;
                xHold = 0;

                for (i = 0; i < clsCN.num_repl(txtQuantity.Text); i++)
                {
                /////////////////////
                    if (cnt == 1) {
                        clsCN.ExecuteSQLQuery("INSERT INTO PrintBarcode (COMPANY_NAME,BARCODE_1,PRODUCT_NAME_1 ,PRICE_1) VALUES ('" + CompanyName + "','" + txtBarcode.Text + "', '" + clsCN.str_repl(cmbProducts.Text) + "','" + txtPrice.Text + "') ");
                        clsCN.ExecuteSQLQuery("SELECT * FROM PrintBarcode ORDER BY id DESC");
                        xHold = Convert.ToInt32(clsCN.sqlDT.Rows[0]["id"]);
                    }
                    else if (cnt == 2) {
                        clsCN.ExecuteSQLQuery("UPDATE  PrintBarcode SET COMPANY_NAME='" + CompanyName + "',BARCODE_2='" + txtBarcode.Text + "',PRODUCT_NAME_2='" + clsCN.str_repl(cmbProducts.Text) + "' , PRICE_2= '" + txtPrice.Text + "' WHERE id= '" + xHold + "' ");
                        holdi = holdi + 1;
                    }
                    else {
                        if (((cnt - 1) / (2)) == 1)
                        {
                            clsCN.ExecuteSQLQuery("INSERT INTO PrintBarcode (COMPANY_NAME,BARCODE_1,PRODUCT_NAME_1 ,PRICE_1) VALUES ('" + CompanyName + "','" + txtBarcode.Text + "', '" + clsCN.str_repl(cmbProducts.Text) + "','" + txtPrice.Text + "') ");
                            clsCN.ExecuteSQLQuery("SELECT * FROM PrintBarcode ORDER BY id DESC");
                            xHold = Convert.ToInt32(clsCN.sqlDT.Rows[0]["id"]);
                            cnt = 1;
                        }
                    }
                ///////////////////
                    cnt = cnt + 1;
                }
                txtBarcode.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
                MessageBox.Show("Added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult msg = new DialogResult();
            msg = MessageBox.Show("Do you really want to clear barcode?", "Clear Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == DialogResult.Yes)
            {
                clsCN.ExecuteSQLQuery("DELETE FROM PrintBarcode");
                MessageBox.Show("Barocode deleted Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbProducts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cmbProducts.SelectedValue == null) | !(cmbProducts.SelectedIndex == -1))
            { clsCN.ExecuteSQLQuery("SELECT * FROM Product WHERE  PRODUCT_ID = '"+ clsCN.num_repl(cmbProducts.SelectedValue.ToString()) +"' ");
            if (clsCN.sqlDT.Rows.Count > 0) {
                txtBarcode.Text = clsCN.sqlDT.Rows[0]["UPC_EAN"].ToString();
                txtPrice.Text = clsCN.sqlDT.Rows[0]["RetailPrice"].ToString();
            }
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            clsCN.ExecuteSQLQuery(" SELECT * FROM    PrintBarcode ");
            if (clsCN.sqlDT.Rows.Count > 0) {
                clsCN.PrintBarcode("SELECT  id, COMPANY_NAME, BARCODE_1, BARCODE_2, PRODUCT_NAME_1, PRODUCT_NAME_2, PRICE_1, PRICE_2  FROM  PrintBarcode ");
            }
            else { MessageBox.Show("Barocode not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

 
    }
}
