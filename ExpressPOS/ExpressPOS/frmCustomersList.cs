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
    public partial class frmCustomersList : Form
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


        public frmCustomersList()
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

        private void FormHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void frmCustomersList_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
        }

        private void LoadData()
        {
            string sqlStr = "SELECT  CUST_ID, Cust_Name, Address, Contact, Email, EntryDate, Status  FROM     Customer";
            clsCN.FillDataGrid(sqlStr, CustomerDataGridView);
            clsCN.ExecuteSQLQuery(sqlStr);
            if (clsCN.sqlDT.Rows.Count > 0) {
                lblTotalCustomer.Text = "Total " + clsCN.sqlDT.Rows.Count + " Customer(s) found.";
            }
            else { lblTotalCustomer.Text = "Customer not found."; }
        }


        private void CustomerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to edit record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" SELECT * FROM  Customer  WHERE CUST_ID ='" + CustomerDataGridView.CurrentRow.Cells[2].Value.ToString() + "' ");
                    if (clsCN.sqlDT.Rows.Count > 0) {
                        frmNewCustomer frmNewCustomer = new frmNewCustomer();
                        frmNewCustomer.MdiParent = this.ParentForm;
                        frmNewCustomer.btnSubmit.Text = "UPDATE";
                        frmNewCustomer.txtCustomerID.Text = clsCN.sqlDT.Rows[0]["CUST_ID"].ToString();
                        frmNewCustomer.txtCustomerName.Text = clsCN.sqlDT.Rows[0]["Cust_Name"].ToString();
                        frmNewCustomer.txtAddress.Text = clsCN.sqlDT.Rows[0]["Address"].ToString();
                        frmNewCustomer.txtContact.Text = clsCN.sqlDT.Rows[0]["Contact"].ToString();
                        frmNewCustomer.txtEmail.Text = clsCN.sqlDT.Rows[0]["Email"].ToString();
                        frmNewCustomer.dtpEntryDate.Text = clsCN.sqlDT.Rows[0]["EntryDate"].ToString();
                        if (clsCN.sqlDT.Rows[0]["Status"].ToString() == "Y") { frmNewCustomer.rbActive.Checked = true; }
                        else { frmNewCustomer.rbDeactive.Checked = true; }
                        Byte[] MyData = new byte[0];
                        MyData = (Byte[])clsCN.sqlDT.Rows[0]["CustPhoto"];
                        MemoryStream stream = new MemoryStream(MyData);
                        stream.Position = 0;
                        frmNewCustomer.pictureBox1.BackgroundImage = Image.FromStream(stream);
                        frmNewCustomer.Show();
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" DELETE  Customer  WHERE CUST_ID ='" + CustomerDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Information deleted sucessfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
