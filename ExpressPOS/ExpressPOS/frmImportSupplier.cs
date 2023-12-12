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
    public partial class frmImportSupplier : Form
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

        public frmImportSupplier()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
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
                                    SupplierDataGridView.DataSource = dt;
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
                if (SupplierDataGridView.RowCount > 0)
                {
                    DialogResult msg = new DialogResult();
                    msg = MessageBox.Show("Total " + SupplierDataGridView.RowCount.ToString() + " supplier(s) found. Click Yes to save this data.", "Import Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        int i = 0;
                        for (i = 0; i <= SupplierDataGridView.RowCount - 1; i++)
                        {
                            string CompanyName = SupplierDataGridView.Rows[i].Cells["CompanyName"].Value.ToString();
                            string AgencyName = SupplierDataGridView.Rows[i].Cells["AgencyName"].Value.ToString();
                            string SupplierName = SupplierDataGridView.Rows[i].Cells["SupplierName"].Value.ToString();
                            string Address = SupplierDataGridView.Rows[i].Cells["Address"].Value.ToString();
                            string Contact = SupplierDataGridView.Rows[i].Cells["Contact"].Value.ToString();
                            string Email = SupplierDataGridView.Rows[i].Cells["Email"].Value.ToString();
                            string EntryDate = SupplierDataGridView.Rows[i].Cells["EntryDate"].Value.ToString();
                            string AcStatus = SupplierDataGridView.Rows[i].Cells["AcStatus"].Value.ToString();

                            clsCN.ExecuteSQLQuery("INSERT INTO Supplier (CompanyName, AgencyName, SupplierName, Address, Contact, Email, EntryDate, AcStatus) VALUES ('" + clsCN.str_repl(CompanyName) + "','" + clsCN.str_repl(AgencyName) + "','" + clsCN.str_repl(SupplierName) + "', '" + clsCN.str_repl(Address) + "', '" + clsCN.str_repl(Contact) + "', '" + clsCN.str_repl(Email) + "', '" + EntryDate + "' ,'" + clsCN.str_repl(AcStatus) + "')");
                            clsCN.ExecuteSQLQuery("SELECT  SUPP_ID   FROM   Supplier  ORDER BY SUPP_ID DESC");
                            string SuppID = clsCN.sqlDT.Rows[0]["SUPP_ID"].ToString();
                            frmNewSupplier frmNewSupplier = new frmNewSupplier();
                            clsCN.SupplierPhotoUpload(SuppID, frmNewSupplier.pictureBox1);
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

        private void frmImportSupplier_Load(object sender, EventArgs e)
        {

        }
    }
}
