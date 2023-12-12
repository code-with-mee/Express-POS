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
    public partial class frmExpenceOverhead : Form
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

        public frmExpenceOverhead()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtHeadID.Text = "";
            txtHeadName.Text = "";
            btnSubmit.Text = "SUBMIT";
            LoadData();
        }

        private void frmExpenceOverhead_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
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
            clsCN.FillDataGrid(" SELECT        OVERHEAD_ID, OverheadName  FROM            ExpensesOverhead ", ExpenceDataGridView);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtHeadName.Text != "")
            {
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO ExpensesOverhead (OverheadName) VALUES ('" + txtHeadName.Text + "')");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE ExpensesOverhead  SET OverheadName ='" + txtHeadName.Text + "'  WHERE OVERHEAD_ID ='" + txtHeadID.Text + "' ");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information update Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHeadName.Focus();
                return;
            }
        }

        private void ExpenceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                btnSubmit.Text = "UPDATE";
                txtHeadID.Text = ExpenceDataGridView.CurrentRow.Cells[2].Value.ToString();
                txtHeadName.Text = ExpenceDataGridView.CurrentRow.Cells[3].Value.ToString();
                txtHeadName.Focus();
            }
            else if (e.ColumnIndex == 1)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" DELETE  ExpensesOverhead  WHERE OVERHEAD_ID ='" + ExpenceDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Data Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
