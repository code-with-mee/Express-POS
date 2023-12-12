using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmSupplierList : Form
    {

        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmSupplierList()
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

        private void frmSupplierList_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
        }

        private void LoadData()
        {
            string sqlStr = "SELECT SUPP_ID, CompanyName, AgencyName, SupplierName, Address, Contact, Email, EntryDate, AcStatus  FROM   Supplier";
            clsCN.FillDataGrid(sqlStr, SupplierDataGridView);
            clsCN.ExecuteSQLQuery(sqlStr);
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                lblTotalSupplier.Text = "Total " + clsCN.sqlDT.Rows.Count + " Supplier(s) found.";
            }
            else { lblTotalSupplier.Text = "Supplier not found."; }
        }

        private void SupplierDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to edit record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" SELECT * FROM  Supplier  WHERE SUPP_ID ='" + SupplierDataGridView.CurrentRow.Cells[2].Value.ToString() + "' ");
                    if (clsCN.sqlDT.Rows.Count > 0)
                    {
                        frmNewSupplier frmNewSupplier = new frmNewSupplier();
                        frmNewSupplier.MdiParent = this.ParentForm;
                        frmNewSupplier.btnSubmit.Text = "UPDATE";
                        frmNewSupplier.txtSupplierID.Text = clsCN.sqlDT.Rows[0]["SUPP_ID"].ToString();
                        frmNewSupplier.txtCompanyName.Text = clsCN.sqlDT.Rows[0]["CompanyName"].ToString();
                        frmNewSupplier.txtSupplierName.Text = clsCN.sqlDT.Rows[0]["SupplierName"].ToString();
                        frmNewSupplier.txtAgencyName.Text = clsCN.sqlDT.Rows[0]["AgencyName"].ToString();
                        frmNewSupplier.txtAddress.Text = clsCN.sqlDT.Rows[0]["Address"].ToString();
                        frmNewSupplier.txtContact.Text = clsCN.sqlDT.Rows[0]["Contact"].ToString();
                        frmNewSupplier.txtEmail.Text = clsCN.sqlDT.Rows[0]["Email"].ToString();
                        frmNewSupplier.dtpEntryDate.Text = clsCN.sqlDT.Rows[0]["EntryDate"].ToString();
                        if (clsCN.sqlDT.Rows[0]["AcStatus"].ToString() == "Y") { frmNewSupplier.rbActive.Checked = true; }
                        else { frmNewSupplier.rbDeactive.Checked = true; }
                        Byte[] MyData = new byte[0];
                        MyData = (Byte[])clsCN.sqlDT.Rows[0]["SuppPhoto"];
                        MemoryStream stream = new MemoryStream(MyData);
                        stream.Position = 0;
                        frmNewSupplier.pictureBox1.BackgroundImage = Image.FromStream(stream);
                        frmNewSupplier.Show();
                    }

                }
            }
            else if (e.ColumnIndex == 1)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" DELETE  Supplier  WHERE SUPP_ID ='" + SupplierDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Information deleted sucessfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
