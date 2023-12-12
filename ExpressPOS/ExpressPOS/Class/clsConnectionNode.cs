using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Microsoft.Reporting.WinForms;
using System.Collections;
using ExpressPOS.Report.ReportViewer;
using System.Data.SqlClient;

namespace ExpressPOS
{
    class clsConnectionNode
    {
        public string CnString;
        public DataTable sqlDT = new DataTable();


        private String BusinessName;
        private string BusAddress;
        private string BusContactNo;
        private string BusEmail;
        private string BusVatReg;

        // Initializing Database Connection
        public bool DBConnectionInitializing()
        {
            bool functionReturnValue = false;
            CnString = "Data Source=BLOCKCHAINCAMBO\\SQLDEV19;Initial Catalog=EZ_DB;Integrated Security=True";
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = CnString;
                sqlCon.Open();
                functionReturnValue = true;
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                functionReturnValue = false;
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error establishing the database connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }
            return functionReturnValue;
        }


        public DataTable ExecuteSQLQuery(string SQLQuery)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(CnString);
                SqlDataAdapter sqlDA = new SqlDataAdapter(SQLQuery, sqlCon);
                SqlCommandBuilder sqlCB = new SqlCommandBuilder(sqlDA);
                sqlDT.Reset();
                sqlDA.Fill(sqlDT);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlDT;
        }


        public void FillDataGrid(string sql, DataGridView dgv)
        {
            SqlConnection conn = new SqlConnection(CnString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                dgv.DataSource = dt;
                adp.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        // DataGridView ComboBox 
        public void FillComboBoxColumn(string sql, string Value_Member, string Display_Member, DataGridViewComboBoxColumn combo)
        {
                SqlConnection connection =new SqlConnection(CnString);
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter(); 
                DataSet ds = new DataSet();
                try { 
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(ds);
                    adapter.Dispose(); 
                    command.Dispose(); 
                    connection.Close();
                    combo.DataSource = ds.Tables[0];
                    combo.DisplayMember = Display_Member;
                    combo.ValueMember = Value_Member;
                }
                catch (Exception ex) 
                {
                    System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
        }


        // FillCombo Box Dinmically
        public void FillComboBox(string sql, string Value_Member, string Display_Member, ComboBox combo)
        {
            SqlConnection connection = new SqlConnection(CnString);
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();
                combo.DataSource = ds.Tables[0];
                combo.DisplayMember = Display_Member;
                combo.ValueMember = Value_Member;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        public double num_repl(string a) 
        {
            double n;
            bool isNumeric = double.TryParse(a, out n);
            return n;
        }


        public object str_repl(string str)
        {
            return str.Replace("'" , "");
        }

        public object fltr_combo(ComboBox cmbo)
        {
            if (cmbo.SelectedIndex == -1)
            {
                return 0;
            }
            return cmbo.SelectedValue;
        }

        //Genarate Auto Barcode
        public string GenarateAutoBarcode(string barcode)
        {
            int val = 0;
            ExecuteSQLQuery("SELECT * FROM Barcode");
            if (sqlDT.Rows.Count > 0) {
                barcode = sqlDT.Rows[0]["Barcode"].ToString();
                val = Int32.Parse(barcode) + 1 ;
                ExecuteSQLQuery("UPDATE  Barcode  SET  Barcode='" + val + "' ");
                barcode = val.ToString();
            }
            else {
                ExecuteSQLQuery("INSERT INTO Barcode (Barcode) VALUES ('100000') ");
                barcode = "100000";
            }
            return barcode;
        }


        //Start Customer photo upload
        public void CutomerPhotoUpload(string CUST_ID, PictureBox pctBox)
        {
            SqlConnection con = new SqlConnection(CnString);
            con.Open();
            string sql = "UPDATE  Customer SET   CustPhoto=@Photo WHERE CUST_ID='" + CUST_ID + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            MemoryStream ms = new MemoryStream();
            pctBox.BackgroundImage.Save(ms, pctBox.BackgroundImage.RawFormat);
            byte[] data = ms.GetBuffer();
            SqlParameter p = new SqlParameter("@Photo", SqlDbType.Image);
            p.Value = data;
            cmd.Parameters.Add(p);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        // End Customer photo upload..

        //Start Supplier photo upload
        public void SupplierPhotoUpload(string SUPP_ID, PictureBox pctBox)
        {
            SqlConnection con = new SqlConnection(CnString);
            con.Open();
            string sql = "UPDATE  Supplier SET   SuppPhoto=@Photo WHERE SUPP_ID='" + SUPP_ID + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            MemoryStream ms = new MemoryStream();
            pctBox.BackgroundImage.Save(ms, pctBox.BackgroundImage.RawFormat);
            byte[] data = ms.GetBuffer();
            SqlParameter p = new SqlParameter("@Photo", SqlDbType.Image);
            p.Value = data;
            cmd.Parameters.Add(p);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        // End Supplier photo upload..


        //Start Product photo upload
        public void ProductPhotoUpload(string PRODUCT_ID, PictureBox pctBox)
        {
            SqlConnection con = new SqlConnection(CnString);
            con.Open();
            string sql = "UPDATE  Product SET   ProductPhoto=@Photo WHERE PRODUCT_ID='" + PRODUCT_ID + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            MemoryStream ms = new MemoryStream();
            pctBox.BackgroundImage.Save(ms, pctBox.BackgroundImage.RawFormat);
            byte[] data = ms.GetBuffer();
            SqlParameter p = new SqlParameter("@Photo", SqlDbType.Image);
            p.Value = data;
            cmd.Parameters.Add(p);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        // End Product photo upload..

        //Start Employee photo upload
        public void EmployeePhotoUpload(string EMP_ID, PictureBox pctBox)
        {
            SqlConnection con = new SqlConnection(CnString);
            con.Open();
            string sql = "UPDATE  Employee SET   EmployeePhoto=@Photo WHERE EMP_ID='" + EMP_ID + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            MemoryStream ms = new MemoryStream();
            pctBox.BackgroundImage.Save(ms, pctBox.BackgroundImage.RawFormat);
            byte[] data = ms.GetBuffer();
            SqlParameter p = new SqlParameter("@Photo", SqlDbType.Image);
            p.Value = data;
            cmd.Parameters.Add(p);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        // End Employee photo upload..


        //Report view..................
        public void PrintBarcode(string sql)
        {
            frmReportView frmReportView = new frmReportView();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frmReportView.dsExpressPOS.PrintBarcode.Clear();
            da.Fill(frmReportView.dsExpressPOS.PrintBarcode);
            frmReportView.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frmReportView.reportViewer1.RefreshReport();
            frmReportView.Show();
        }

        public void CompanyInfo() {
            ExecuteSQLQuery("SELECT   *    FROM   BusinessInfo");
            if (sqlDT.Rows.Count > 0)
            {
                BusinessName = sqlDT.Rows[0]["BusinessName"].ToString();
                BusAddress = sqlDT.Rows[0]["Address"].ToString();
                BusContactNo = sqlDT.Rows[0]["ContactNo"].ToString();
                BusEmail = sqlDT.Rows[0]["Email"].ToString();
                BusVatReg = sqlDT.Rows[0]["VatRegNo"].ToString();
            }
            else {
                BusinessName = "Company Name";
                BusAddress = "Company Address";
                BusContactNo = "Contact No";
                BusEmail = "Your Email";
                BusVatReg = "00-000-00-00";
            }
        }


        public void PrintExpencesReceipt(string sql)
        {
            CompanyInfo();
            frmReportExpencesView frmReportExpencesView = new frmReportExpencesView();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frmReportExpencesView.dsExpressPOS.ExpensesList.Clear();
            da.Fill(frmReportExpencesView.dsExpressPOS.ExpensesList);
            ReportParameter BusName = new ReportParameter("BusinessName", BusinessName);
            ReportParameter Address = new ReportParameter("Address", BusAddress);
            ReportParameter ContactNo = new ReportParameter("ContactNo", BusContactNo);
            ReportParameter Email = new ReportParameter("Email", BusEmail);
            ReportParameter VatReg = new ReportParameter("VatReg", BusVatReg);
            frmReportExpencesView.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frmReportExpencesView.reportViewer1.LocalReport.ReportPath = @"Reports\ExpensesVoucher.rdlc";
            frmReportExpencesView.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { BusName, Address, ContactNo, Email, VatReg });
            frmReportExpencesView.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frmReportExpencesView.reportViewer1.RefreshReport();
            frmReportExpencesView.Show();
        }

        public void PrintSalesInvoice(string sqlSTR)
        {
            CompanyInfo();
            frmPrintSaleInvoice frmPrintSaleInvoice = new frmPrintSaleInvoice();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sqlSTR, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frmPrintSaleInvoice.dsExpressPOS.SalesInvoice.Clear();
            da.Fill(frmPrintSaleInvoice.dsExpressPOS.SalesInvoice);
            ReportParameter BusName = new ReportParameter("BusinessName", BusinessName);
            ReportParameter Address = new ReportParameter("Address", BusAddress);
            ReportParameter ContactNo = new ReportParameter("ContactNo", BusContactNo);
            ReportParameter Email = new ReportParameter("Email", BusEmail);
            ReportParameter VatReg = new ReportParameter("VatReg", BusVatReg);
            frmPrintSaleInvoice.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frmPrintSaleInvoice.reportViewer1.LocalReport.ReportPath = @"Reports\Invoice.rdlc";
            frmPrintSaleInvoice.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { BusName, Address, ContactNo, Email, VatReg });
            frmPrintSaleInvoice.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frmPrintSaleInvoice.reportViewer1.RefreshReport();
            frmPrintSaleInvoice.Show();
        }

        public void PrintPOSReceipt(string sqlSTR)
        {
            CompanyInfo();
            frmPrintSaleReceipt frmPrintSaleReceipt = new frmPrintSaleReceipt();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sqlSTR, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frmPrintSaleReceipt.dsExpressPOS.SalesInvoice.Clear();
            da.Fill(frmPrintSaleReceipt.dsExpressPOS.SalesInvoice);
            ReportParameter BusName = new ReportParameter("BusinessName", BusinessName);
            ReportParameter Address = new ReportParameter("Address", BusAddress);
            ReportParameter ContactNo = new ReportParameter("ContactNo", BusContactNo);
            ReportParameter Email = new ReportParameter("Email", BusEmail);
            ReportParameter VatReg = new ReportParameter("VatReg", BusVatReg);
            frmPrintSaleReceipt.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frmPrintSaleReceipt.reportViewer1.LocalReport.ReportPath = @"Reports\SaleReceipt.rdlc";
            frmPrintSaleReceipt.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { BusName, Address, ContactNo, Email, VatReg });
            frmPrintSaleReceipt.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frmPrintSaleReceipt.reportViewer1.RefreshReport();
            frmPrintSaleReceipt.Show();
        }


        public void PrintCustomerList(string sql)
        {
            frm_Rv_Customer frm_Rv_Customer = new frm_Rv_Customer();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_Customer.dsExpressPOS.Customer.Clear();
            da.Fill(frm_Rv_Customer.dsExpressPOS.Customer);
            frm_Rv_Customer.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_Customer.reportViewer1.LocalReport.ReportPath = @"Reports\Customer.rdlc";
            frm_Rv_Customer.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_Customer.reportViewer1.RefreshReport();
            frm_Rv_Customer.Show();
        }

        public void PrintSupplierList(string sql)
        {
            frm_Rv_Supplier frm_Rv_Supplier = new frm_Rv_Supplier();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_Supplier.dsExpressPOS.Supplier.Clear();
            da.Fill(frm_Rv_Supplier.dsExpressPOS.Supplier);
            frm_Rv_Supplier.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_Supplier.reportViewer1.LocalReport.ReportPath = @"Reports\Supplier.rdlc";
            frm_Rv_Supplier.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_Supplier.reportViewer1.RefreshReport();
            frm_Rv_Supplier.Show();
        }

        public void PrintEmployeeList(string sql)
        {
            frm_Rv_Employee frm_Rv_Employee = new frm_Rv_Employee();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_Employee.dsExpressPOS.Employee.Clear();
            da.Fill(frm_Rv_Employee.dsExpressPOS.Employee);
            frm_Rv_Employee.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_Employee.reportViewer1.LocalReport.ReportPath = @"Reports\Employee.rdlc";
            frm_Rv_Employee.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_Employee.reportViewer1.RefreshReport();
            frm_Rv_Employee.Show();
        }

        public void PrintReturnInvoice(string sql)
        {
            frm_Rv_ReturnInvoice frm_Rv_ReturnInvoice = new frm_Rv_ReturnInvoice();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_ReturnInvoice.dsExpressPOS.Return.Clear();
            da.Fill(frm_Rv_ReturnInvoice.dsExpressPOS.Return);
            frm_Rv_ReturnInvoice.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_ReturnInvoice.reportViewer1.LocalReport.ReportPath = @"Reports\ReturnInvoice.rdlc";
            frm_Rv_ReturnInvoice.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_ReturnInvoice.reportViewer1.RefreshReport();
            frm_Rv_ReturnInvoice.Show();
        }

        public void PrintProductList(string sql)
        {
            frm_Rv_Product frm_Rv_Product = new frm_Rv_Product();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_Product.dsExpressPOS.ProductList.Clear();
            da.Fill(frm_Rv_Product.dsExpressPOS.ProductList);
            frm_Rv_Product.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_Product.reportViewer1.LocalReport.ReportPath = @"Reports\ProductList.rdlc";
            frm_Rv_Product.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_Product.reportViewer1.RefreshReport();
            frm_Rv_Product.Show();
        }

        public void PrintExpencesList(string sql, string Date_From)
        {
            frm_Rv_Expences frm_Rv_Expences = new frm_Rv_Expences();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_Expences.dsExpressPOS.ExpensesList.Clear();
            da.Fill(frm_Rv_Expences.dsExpressPOS.ExpensesList);
            ReportParameter DateFrom = new ReportParameter("DateFrom", Date_From);
            frm_Rv_Expences.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_Expences.reportViewer1.LocalReport.ReportPath = @"Reports\ExpensesList.rdlc";
            frm_Rv_Expences.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { DateFrom });
            frm_Rv_Expences.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_Expences.reportViewer1.RefreshReport();
            frm_Rv_Expences.Show();
        }

        public void PrintStockMovementStatus(string sql, string Date_From)
        {
            frm_Rv_StockMovement frm_Rv_StockMovement = new frm_Rv_StockMovement();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_StockMovement.dsExpressPOS.StockMovement.Clear();
            da.Fill(frm_Rv_StockMovement.dsExpressPOS.StockMovement);
            ReportParameter DateFrom = new ReportParameter("DateFrom", Date_From);
            frm_Rv_StockMovement.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_StockMovement.reportViewer1.LocalReport.ReportPath = @"Reports\StockMovement.rdlc";
            frm_Rv_StockMovement.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { DateFrom });
            frm_Rv_StockMovement.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_StockMovement.reportViewer1.RefreshReport();
            frm_Rv_StockMovement.Show();
        }


        public void PrintSalesDetails(string sql, string Date_From)
        {
            frm_Rv_Sales frm_Rv_Sales = new frm_Rv_Sales();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_Sales.dsExpressPOS.Sales.Clear();
            da.Fill(frm_Rv_Sales.dsExpressPOS.Sales);
            ReportParameter DateFrom = new ReportParameter("DateFrom", Date_From);
            frm_Rv_Sales.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_Sales.reportViewer1.LocalReport.ReportPath = @"Reports\SaleDetails.rdlc";
            frm_Rv_Sales.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { DateFrom });
            frm_Rv_Sales.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_Sales.reportViewer1.RefreshReport();
            frm_Rv_Sales.Show();
        }

        public void PrintSalesReturnDetails(string sql, string Date_From)
        {
            frm_Rv_SalesReturn frm_Rv_SalesReturn = new frm_Rv_SalesReturn();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_SalesReturn.dsExpressPOS.Return.Clear();
            da.Fill(frm_Rv_SalesReturn.dsExpressPOS.Return);
            ReportParameter DateFrom = new ReportParameter("DateFrom", Date_From);
            frm_Rv_SalesReturn.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_SalesReturn.reportViewer1.LocalReport.ReportPath = @"Reports\SaleReturn.rdlc";
            frm_Rv_SalesReturn.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { DateFrom });
            frm_Rv_SalesReturn.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_SalesReturn.reportViewer1.RefreshReport();
            frm_Rv_SalesReturn.Show();
        }

        public void PrintTaxReprot(string sql, string Date_From)
        {
            frm_Rv_TAX frm_Rv_TAX = new frm_Rv_TAX();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, new SqlConnection(CnString));
            da = new SqlDataAdapter(cmd);
            frm_Rv_TAX.dsExpressPOS.TAX.Clear();
            da.Fill(frm_Rv_TAX.dsExpressPOS.TAX);
            ReportParameter DateFrom = new ReportParameter("DateFrom", Date_From);
            frm_Rv_TAX.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            frm_Rv_TAX.reportViewer1.LocalReport.ReportPath = @"Reports\TAX.rdlc";
            frm_Rv_TAX.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { DateFrom });
            frm_Rv_TAX.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            frm_Rv_TAX.reportViewer1.RefreshReport();
            frm_Rv_TAX.Show();
        }

        //////////////////////////////////////////////////
    }

}
