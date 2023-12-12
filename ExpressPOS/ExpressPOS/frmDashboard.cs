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
    public partial class frmDashboard : Form
    {
        private frmMDIParent frmMDIParent;

        clsConnectionNode clsCN = new clsConnectionNode();

        public frmDashboard()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }


        public frmDashboard(frmMDIParent frmMDIParent)
        {
            // TODO: Complete member initialization
            this.frmMDIParent = frmMDIParent;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshMyForm();
        }

        private void RefreshMyForm()
        {

            //Total users
            clsCN.ExecuteSQLQuery("SELECT  *   FROM  Users");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT        COUNT(USER_ID) AS TotalUser  FROM            Users");
                lblTotalUsers.Text = clsCN.sqlDT.Rows[0]["TotalUser"].ToString();
            }
            else { lblTotalUsers.Text = "0"; }
            //Category
            clsCN.ExecuteSQLQuery("SELECT  *   FROM  Categories");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT        COUNT(CAT_ID) AS TotalCategories  FROM            Categories");
                lblTotalCategories.Text = clsCN.sqlDT.Rows[0]["TotalCategories"].ToString();
            }
            else { lblTotalCategories.Text = "0"; }

            //Employee
            clsCN.ExecuteSQLQuery("SELECT  *   FROM  Employee");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT        COUNT(EMP_ID) AS TotalEmployees  FROM    Employee");
                lblTotalEmployees.Text = clsCN.sqlDT.Rows[0]["TotalEmployees"].ToString();
            }
            else { lblTotalEmployees.Text = "0"; }
            //Customer
            clsCN.ExecuteSQLQuery("SELECT  *   FROM  Customer");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT        COUNT(CUST_ID) AS TotalCustomers  FROM     Customer");
                lblCustomer.Text = clsCN.sqlDT.Rows[0]["TotalCustomers"].ToString();
            }
            else { lblCustomer.Text = "0"; }
            //Supplier
            clsCN.ExecuteSQLQuery("SELECT  *   FROM  Supplier");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT        COUNT(SUPP_ID) AS TotalSuppliers  FROM   Supplier");
                lblSupplier.Text = clsCN.sqlDT.Rows[0]["TotalSuppliers"].ToString();
            }
            else { lblSupplier.Text = "0"; }

            //Product
            clsCN.ExecuteSQLQuery("SELECT  *   FROM  Product");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT        COUNT(PRODUCT_ID) AS TotalProducts  FROM   Product");
                lblTotalProduct.Text = clsCN.sqlDT.Rows[0]["TotalProducts"].ToString();
            }
            else { lblTotalProduct.Text = "0"; }

            //Invoice
            clsCN.ExecuteSQLQuery("SELECT  *  FROM   Sale");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT        COUNT(INVOICE_NO) AS TotalInvoice  FROM   Sale");
                lblTotalInvoice.Text = clsCN.sqlDT.Rows[0]["TotalInvoice"].ToString();
            }
            else { lblTotalInvoice.Text = "0"; }

            //Hold Invoice
            clsCN.ExecuteSQLQuery("SELECT  * FROM  Sale  WHERE  (Status = 'H') ");
            if (clsCN.sqlDT.Rows.Count > 0)
            {
                clsCN.ExecuteSQLQuery("SELECT  COUNT(INVOICE_NO) AS Expr1   FROM   Sale WHERE  (Status = N'H') ");
                lblTotalHoldProduct.Text = clsCN.sqlDT.Rows[0]["Expr1"].ToString();
            }
            else { lblTotalHoldProduct.Text = "0"; }
            // end

        }


        private void frmDashboard_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            RefreshMyForm();
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 300000;
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Start();
        }

    }
}
