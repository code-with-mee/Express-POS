using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmManageEmployee : Form
    {

        clsConnectionNode clsCN = new clsConnectionNode();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmManageEmployee()
        {
            InitializeComponent();
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormHeader_MouseMove(object sender, MouseEventArgs e)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = ExpressPOS.Properties.Resources.upload_empty;
            btnSubmit.Text = "SUBMIT";
            txtEmployeeID.Text = "";
            txtEmployeeName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            rbActive.Checked = true;
            dtpEntryDate.Value = DateTime.Now;
            LoadData();
        }

        private void lnkBrowseImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {
                OpenFileDialog.Title = "Browse image";
                OpenFileDialog.Filter = "Image Files (JPEG,GIF,BMP,PNG)|*.jpg;*.jpeg;*.gif;*.bmp;*.png;";

                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PictureBox PictureBox1 = new PictureBox();
                    pictureBox1.Location = new System.Drawing.Point(399, 25);
                    pictureBox1.BackgroundImage = new Bitmap(OpenFileDialog.FileName);
                    
                }
            }
        }

        private void frmManageEmployee_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
        }

        private void LoadData()
        {
            string sqlStr = "SELECT EMP_ID, EmployeeName, Address, Contact, Email, EntryDate, Status   FROM    Employee";
            clsCN.FillDataGrid(sqlStr, EmployeeDataGridView);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string chkVAL = null;
            if (rbActive.Checked)
            { chkVAL = "Y"; }
            else
            { chkVAL = "N"; }

            if (txtEmployeeName.Text != "" & txtAddress.Text != "")
            {

                //////----------Insert & Update Statement----------//////
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO Employee (EmployeeName, Address, Contact, Email, EntryDate, Status) VALUES ('" + txtEmployeeName.Text + "', '" + txtAddress.Text + "', '" + txtContact.Text + "', '" + txtEmail.Text + "', '" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "' ,'" + chkVAL + "')");
                    clsCN.ExecuteSQLQuery("SELECT  EMP_ID   FROM   Employee  ORDER BY EMP_ID DESC");
                    string EMP_ID = clsCN.sqlDT.Rows[0]["EMP_ID"].ToString();
                    clsCN.EmployeePhotoUpload(EMP_ID, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information save sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    clsCN.ExecuteSQLQuery("UPDATE Employee SET  EmployeeName='" + txtEmployeeName.Text + "', Address='" + txtAddress.Text + "', Contact='" + txtContact.Text + "', Email='" + txtEmail.Text + "', EntryDate='" + dtpEntryDate.Value.Date.ToString("MM/dd/yyyy") + "', Status='" + chkVAL + "' WHERE EMP_ID='" + txtEmployeeID.Text + "' ");
                    clsCN.EmployeePhotoUpload(txtEmployeeID.Text, pictureBox1);
                    btnReset.PerformClick();
                    MessageBox.Show("Information update sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //////----------End Insert & Update Statement----------//////

            }
            else
            {
                MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmployeeName.Focus();
                return;
            }

        }

        private void EmployeeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to edit record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" SELECT * FROM  Employee  WHERE EMP_ID ='" + EmployeeDataGridView.CurrentRow.Cells[2].Value.ToString() + "' ");
                    if (clsCN.sqlDT.Rows.Count > 0)
                    {
                        btnSubmit.Text = "UPDATE";
                        txtEmployeeID.Text = clsCN.sqlDT.Rows[0]["EMP_ID"].ToString();
                        txtEmployeeName.Text = clsCN.sqlDT.Rows[0]["EmployeeName"].ToString();
                        txtAddress.Text = clsCN.sqlDT.Rows[0]["Address"].ToString();
                        txtContact.Text = clsCN.sqlDT.Rows[0]["Contact"].ToString();
                        txtEmail.Text = clsCN.sqlDT.Rows[0]["Email"].ToString();
                        dtpEntryDate.Text = clsCN.sqlDT.Rows[0]["EntryDate"].ToString();
                        if (clsCN.sqlDT.Rows[0]["Status"].ToString() == "Y") { rbActive.Checked = true; }
                        else { rbDeactive.Checked = true; }
                        Byte[] MyData = new byte[0];
                        MyData = (Byte[])clsCN.sqlDT.Rows[0]["EmployeePhoto"];
                        MemoryStream stream = new MemoryStream(MyData);
                        stream.Position = 0;
                        pictureBox1.BackgroundImage = Image.FromStream(stream);
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
                    clsCN.ExecuteSQLQuery(" DELETE  Employee  WHERE EMP_ID ='" + EmployeeDataGridView.CurrentRow.Cells[2].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("Data Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
