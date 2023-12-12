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
    public partial class frmManageTables : Form
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

        public frmManageTables()
        {
            InitializeComponent();
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

        private void frmManageTables_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTabID.Text = "";
            txtTableCode.Text = "";
            txtTableName.Text = "";
            btnSubmit.Text = "SUBMIT";
            LoadData();
        }

        private void LoadData()
        {
            clsCN.FillDataGrid("SELECT TABLE_ID, Table_Code, Table_Name FROM ManageTables", TableDataGridView);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtTableCode.Text != "" & txtTableName.Text != "")
            {
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO ManageTables (Table_Code, Table_Name, Booked) VALUES ('" + txtTableCode.Text + "', '" + txtTableName.Text + "', 'N')");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE ManageTables  SET Table_Code ='" + txtTableCode.Text + "', Table_Name ='" + txtTableName.Text + "'  WHERE TABLE_ID ='" + txtTabID.Text + "' ");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information update Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTableCode.Focus();
                return;
            }
        }

        private void TableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                btnSubmit.Text = "UPDATE";
                txtTabID.Text = TableDataGridView.CurrentRow.Cells[2].Value.ToString();
                txtTableCode.Text = TableDataGridView.CurrentRow.Cells[3].Value.ToString();
                txtTableName.Text = TableDataGridView.CurrentRow.Cells[4].Value.ToString();
                txtTableCode.Focus();
            }
            else if (e.ColumnIndex == 1)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" DELETE  ManageTables  WHERE TABLE_ID ='" + TableDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Data Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
