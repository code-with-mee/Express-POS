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
    public partial class frmManageTax : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmManageTax()
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTaxID.Text = "";
            txtTaxName.Text = "";
            btnSubmit.Text = "SUBMIT";
            LoadData();
        }

        private void LoadData() { 
            clsCN.FillDataGrid(" SELECT  TAX_ID, Tax_Name  FROM  TAX  ORDER BY Tax_Name ", TaxDataGridView);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
           if (txtTaxName.Text != "") {
               if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO TAX (Tax_Name) VALUES ('" + txtTaxName.Text + "')");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE TAX  SET Tax_Name ='" + txtTaxName.Text + "'  WHERE TAX_ID ='" + txtTaxID.Text + "' ");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information update Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTaxName.Focus();
                return;
            }
        }

        private void TaxDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          if (e.ColumnIndex == 0) {
                btnSubmit.Text = "UPDATE";
                txtTaxID.Text = TaxDataGridView.CurrentRow.Cells[2].Value.ToString();
                txtTaxName.Text = TaxDataGridView.CurrentRow.Cells[3].Value.ToString();
                txtTaxName.Focus();
            }
            else if (e.ColumnIndex == 1) {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes) {
                    clsCN.ExecuteSQLQuery(" DELETE  TAX  WHERE TAX_ID ='" + TaxDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Data Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void frmManageTax_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
        }
    }
}
