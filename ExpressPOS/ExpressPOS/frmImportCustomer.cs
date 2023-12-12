using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ExpressPOS
{
    public partial class frmImportCustomer : Form
    {

        clsConnectionNode clsCN = new clsConnectionNode();

        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

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

        public frmImportCustomer()
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

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {
                OpenFileDialog.Title = "Microsoft Excel File...";
                OpenFileDialog.Filter = "Excel Files (xlsx)|*.xlsx;";
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = OpenFileDialog.FileName;
                    ////////////
                    try
                    {
                        ///////////////////////////
                        string conStr, sheetName;
                        conStr = string.Format(Excel07ConString, txtFilePath.Text, "YES");
                        //Get the name of the First Sheet.
                        using (OleDbConnection con = new OleDbConnection(conStr))
                        {
                            using (OleDbCommand cmd = new OleDbCommand())
                            {
                                cmd.Connection = con;
                                con.Open();
                                DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                con.Close();
                            }
                        }
                        //Read Data from the First Sheet.
                        using (OleDbConnection con = new OleDbConnection(conStr))
                        {
                            using (OleDbCommand cmd = new OleDbCommand())
                            {
                                using (OleDbDataAdapter oda = new OleDbDataAdapter())
                                {
                                    DataTable dt = new DataTable();
                                    cmd.CommandText = "SELECT * From [" + sheetName + "]";
                                    cmd.Connection = con;
                                    con.Open();
                                    oda.SelectCommand = cmd;
                                    oda.Fill(dt);
                                    con.Close();
                                    //Populate DataGridView.
                                    CustomerDataGridView.DataSource = dt;
                                }
                            }
                        }
                        ///////////////////////////
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    ////////////
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomerDataGridView.RowCount > 0)
                {
                    DialogResult msg = new DialogResult();
                    msg = MessageBox.Show("Total " + CustomerDataGridView.RowCount.ToString() + " customer(s) found. Click Yes to save this data.", "Import Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        int i = 0;
                        for (i = 0; i <= CustomerDataGridView.RowCount - 1; i++)
                        {
                            string Cust_Name = CustomerDataGridView.Rows[i].Cells["Cust_Name"].Value.ToString();
                            string Address = CustomerDataGridView.Rows[i].Cells["Address"].Value.ToString();
                            string Contact = CustomerDataGridView.Rows[i].Cells["Contact"].Value.ToString();
                            string Email = CustomerDataGridView.Rows[i].Cells["Email"].Value.ToString();
                            string EntryDate = CustomerDataGridView.Rows[i].Cells["EntryDate"].Value.ToString();
                            string Status = CustomerDataGridView.Rows[i].Cells["Status"].Value.ToString();


                            clsCN.ExecuteSQLQuery("INSERT INTO Customer (Cust_Name, Address, Contact, Email, EntryDate, Status) VALUES ('" + clsCN.str_repl(Cust_Name) + "', '" + clsCN.str_repl(Address) + "', '" + clsCN.str_repl(Contact) + "', '" + clsCN.str_repl(Email) + "', '" + EntryDate + "' ,'" + clsCN.str_repl(Status) + "')");
                            clsCN.ExecuteSQLQuery("SELECT  CUST_ID   FROM   Customer  ORDER BY CUST_ID DESC");
                            string CustID = clsCN.sqlDT.Rows[0]["CUST_ID"].ToString();
                            frmNewCustomer frmNewCustomer = new frmNewCustomer();
                            clsCN.CutomerPhotoUpload(CustID, frmNewCustomer.pictureBox1);
                        }
                        MessageBox.Show("Import sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No product(s) were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmImportCustomer_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
        }
    }
}
