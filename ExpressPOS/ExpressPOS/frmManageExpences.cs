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
    public partial class frmManageExpences : Form
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
        public frmManageExpences()
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtReceiptNo.Text = "";
            txtAmount.Text = "";
            txtNote.Text = "";
            btnSubmit.Text = "SUBMIT";
            dtpEntryDate.Value = DateTime.Now;
            LoadData();
        }

        private void LoadData() {
            clsCN.FillDataGrid(" SELECT        Expenses.ReceiptNo, ExpensesOverhead.OverheadName, Expenses.ReceiptDate, Expenses.ExpensesAmount, Expenses.ExpensesNote " +
                               " FROM            Expenses LEFT OUTER JOIN  ExpensesOverhead ON Expenses.OVERHEAD_ID = ExpensesOverhead.OVERHEAD_ID " +
                               " WHERE        (Expenses.ReceiptDate >= '" + dtpFrom.Value.Date.ToString("MM/dd/yyyy") + "' AND Expenses.ReceiptDate <= '" + dtpTo.Value.Date.ToString("MM/dd/yyyy") + "') ", TableDataGridView);
        }

        private void frmManageExpences_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
            clsCN.FillComboBox("SELECT  OVERHEAD_ID, OverheadName  FROM   ExpensesOverhead  ORDER BY OverheadName", "OVERHEAD_ID", "OverheadName", cmbExpensesName);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmount.Text))
            { MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (cmbExpensesName.SelectedValue == null | cmbExpensesName.SelectedIndex == -1)
            { MessageBox.Show("Please select a category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO Expenses (ReceiptDate, OVERHEAD_ID, ExpensesAmount, ExpensesNote) VALUES ('" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "', '" + cmbExpensesName.SelectedValue.ToString() + "' ,'" + clsCN.num_repl(txtAmount.Text) + "','" + txtNote.Text + "')");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE") {
                    clsCN.ExecuteSQLQuery("UPDATE  Expenses SET ReceiptDate='" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "', OVERHEAD_ID='" + cmbExpensesName.SelectedValue.ToString() + "', ExpensesAmount='" + clsCN.num_repl(txtAmount.Text) + "', ExpensesNote='" + txtNote.Text + "'  WHERE ReceiptNo ='" + txtReceiptNo.Text + "' ");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmExpenceOverhead"] as frmExpenceOverhead == null)
            {
                frmExpenceOverhead frmExpenceOverhead = new frmExpenceOverhead();
                frmExpenceOverhead.MdiParent = this.ParentForm;
                frmExpenceOverhead.Show();
            }
        }

        private void TableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /////////////////////////
            if (e.ColumnIndex == 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to edit record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" SELECT * FROM  Expenses  WHERE ReceiptNo ='" + TableDataGridView.CurrentRow.Cells[3].Value.ToString() + "' ");
                    if (clsCN.sqlDT.Rows.Count > 0)
                    {
                        btnSubmit.Text = "UPDATE";
                        txtReceiptNo.Text = clsCN.sqlDT.Rows[0]["ReceiptNo"].ToString();
                        cmbExpensesName.SelectedValue = clsCN.sqlDT.Rows[0]["OVERHEAD_ID"].ToString();
                        dtpEntryDate.Text = clsCN.sqlDT.Rows[0]["ReceiptDate"].ToString();
                        txtAmount.Text = clsCN.sqlDT.Rows[0]["ExpensesAmount"].ToString();
                        txtNote.Text = clsCN.sqlDT.Rows[0]["ExpensesNote"].ToString();
                        tabControl1.SelectedTab = tabPage1;
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" DELETE  Expenses  WHERE ReceiptNo ='" + TableDataGridView.CurrentRow.Cells[3].Value.ToString() + "' ");
                    LoadData();
                    MessageBox.Show("Data Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (e.ColumnIndex == 2)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to print record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.PrintExpencesReceipt(" SELECT        Expenses.ReceiptNo, Expenses.ReceiptDate, ExpensesOverhead.OverheadName, Expenses.ExpensesAmount, Expenses.ExpensesNote " +
                                          " FROM            Expenses LEFT OUTER JOIN  ExpensesOverhead ON Expenses.OVERHEAD_ID = ExpensesOverhead.OVERHEAD_ID   WHERE        (Expenses.ReceiptNo = '" + TableDataGridView.CurrentRow.Cells[3].Value.ToString() + "') ");
                }
            }
            /////////////////////////

        }
    }
}
