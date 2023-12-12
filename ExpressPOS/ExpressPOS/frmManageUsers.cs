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
    public partial class frmManageUsers : Form
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

        public frmManageUsers()
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

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            LoadData();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string chkVAL = null;
            if (rbActive.Checked)
            {chkVAL = "Y";}
            else
            {chkVAL = "N";}

            if ( string.IsNullOrEmpty(txtUserName.Text) |  string.IsNullOrEmpty(txtPassword.Text) |  string.IsNullOrEmpty(txtRePassword.Text))
            { MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (!(txtPassword.Text == txtRePassword.Text))
            { MessageBox.Show("Password and re-password does not match.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else if (!(cmbUserType.Text == "Admin" | cmbUserType.Text == "Manager" | cmbUserType.Text == "Sales"))
            { MessageBox.Show("Information is not provided properly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else {
                if (btnSubmit.Text == "SUBMIT")
                {
                    clsCN.ExecuteSQLQuery("INSERT INTO Users (UserName, Password, UserType, Status) VALUES ('" + txtUserName.Text + "', '" + txtPassword.Text + "', '" + cmbUserType.Text + "',  '" + chkVAL + "')");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information save Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnSubmit.Text == "UPDATE")
                {
                    //clsCN.ExecuteSQLQuery("UPDATE ManageTables  SET Table_Code ='" + txtTableCode.Text + "', Table_Name ='" + txtTableName.Text + "'  WHERE TABLE_ID ='" + txtTabID.Text + "' ");
                    LoadData();
                    btnReset.PerformClick();
                    MessageBox.Show("Information update Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            
            }
        }


        private void LoadData()
        {
            clsCN.FillDataGrid("SELECT  USER_ID, UserName, UserType, Status   FROM   Users", TableDataGridView);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtRePassword.Text = "";
            cmbUserType.SelectedIndex = -1;
            rbActive.Checked = true;
            btnSubmit.Text = "SUBMIT";
        }

        private void TableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Do you really want to delete record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    clsCN.ExecuteSQLQuery(" DELETE  Users  WHERE USER_ID ='" + TableDataGridView.CurrentRow.Cells[1].Value.ToString() + "'");
                    LoadData();
                    MessageBox.Show("User Delete Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


    }
}
