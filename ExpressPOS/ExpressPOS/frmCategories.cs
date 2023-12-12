using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; 

namespace ExpressPOS
{
    public partial class frmCategories : Form
    {

        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmCategories()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
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

        private void frmCategories_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCatID.Text = "";
            txtCategoryName.Text = "";
            btnSubmit.Text = "SUBMIT";
            LoadData();
        }

        private void LoadData() { 
        clsCN.FillDataGrid(" SELECT  CAT_ID, Cat_Name  FROM  Categories  ORDER BY Cat_Name ", CategoryDataGridView);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text != "") {
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO Categories (Cat_Name) VALUES ('" + txtCategoryName.Text + "')");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE Categories  SET Cat_Name ='" + txtCategoryName.Text + "'  WHERE CAT_ID ='" + txtCatID.Text + "' ");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information update Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCategoryName.Focus();
                return;
            }
        }

        private void CategoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) {
                btnSubmit.Text = "UPDATE";
                txtCatID.Text = CategoryDataGridView.CurrentRow.Cells[2].Value.ToString();
                txtCategoryName.Text = CategoryDataGridView.CurrentRow.Cells[3].Value.ToString();
                txtCategoryName.Focus();
            }
            else if (e.ColumnIndex == 1) {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes) {
                    clsCN.ExecuteSQLQuery(" DELETE  Categories  WHERE CAT_ID ='" + CategoryDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Data Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


    }
}
