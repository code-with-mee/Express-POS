using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExpressPOS.Report;

namespace ExpressPOS
{
    public partial class frmMDIParent : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();

        public frmMDIParent()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCloseApps_Click(object sender, EventArgs e)
        {
            DialogResult msg = new DialogResult();
            msg = MessageBox.Show("This will sure that you would like to quit ExpressPOS? Click Yes to quit ExpressPOS or no to keep ExpressPOS running.", "QUIT EXPRESSPOS?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == DialogResult.Yes)
            {
                this.Dispose();
                Environment.Exit(0);
            }

        }

        private void frmMDIParent_Load(object sender, EventArgs e)
        {
            clsCN.DBConnectionInitializing();
            clsCN.ExecuteSQLQuery("SELECT * FROM BusinessInfo");
            if (clsCN.sqlDT.Rows.Count > 0) {
                if (clsCN.sqlDT.Rows[0]["StatusBar"].ToString() == "Y") {
                    frmDashboard frmDashboard = new frmDashboard();
                    frmDashboard.MdiParent = this;
                    frmDashboard.Show(); } }
            tsslUserName.Text = "Username: " + GlobalVariables.UserName + " , As: " + GlobalVariables.UserType;
        }

        private void btnCutomer_Click(object sender, EventArgs e)
        {
            cmsCustomers.Show(btnCutomer, new Point(210, 0)); 
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            cmsSuppliers.Show(btnSuppliers, new Point(210, 0)); 
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            cmsInventory.Show(btnInventory, new Point(210, 0)); 
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            cmsInvoice.Show(btnInvoices, new Point(210, 0));
        }

        private void btnExtras_Click(object sender, EventArgs e)
        {
            cmsExtras.Show(btnExtras, new Point(210, 0));
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            // General Configaration form
            if (System.Windows.Forms.Application.OpenForms["frmGeneralConfiguration"] as frmGeneralConfiguration == null)
            {
            frmGeneralConfiguration  frmGeneralConfiguration = new frmGeneralConfiguration();
            frmGeneralConfiguration.MdiParent = this;
            frmGeneralConfiguration.Show();
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            // Categories form

            if (System.Windows.Forms.Application.OpenForms["frmCategories"] as frmCategories == null)
            {
            frmCategories frmCategories = new frmCategories();
            frmCategories.MdiParent = this;
            frmCategories.Show();
            }
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            //Manage Tables
            if (System.Windows.Forms.Application.OpenForms["frmManageTables"] as frmManageTables == null)
            {
            frmManageTables frmManageTables = new frmManageTables();
            frmManageTables.MdiParent = this;
            frmManageTables.Show();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Add new customer  
            if (System.Windows.Forms.Application.OpenForms["frmNewCustomer"] as frmNewCustomer == null)
            {
            frmNewCustomer frmNewCustomer = new frmNewCustomer();
            frmNewCustomer.MdiParent = this;
            frmNewCustomer.Show();
            }
        }


        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmManageUsers"] as frmManageUsers == null)
            {
            frmManageUsers frmManageUsers = new frmManageUsers();
            frmManageUsers.MdiParent = this;
            frmManageUsers.Show();
            }
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmManageEmployee"] as frmManageEmployee == null)
            {
            frmManageEmployee frmManageEmployee = new frmManageEmployee();
            frmManageEmployee.MdiParent = this;
            frmManageEmployee.Show();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmCustomersList"] as frmCustomersList == null)
            {
            frmCustomersList frmCustomersList = new frmCustomersList();
            frmCustomersList.MdiParent = this;
            frmCustomersList.Show();
            }
        }


        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmProductInformation"] as frmProductInformation == null)
            {
                frmProductInformation frmProductInformation = new frmProductInformation();
                frmProductInformation.MdiParent = this;
                frmProductInformation.Show();
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmProductList"] as frmProductList == null)
            {
                frmProductList frmProductList = new frmProductList();
                frmProductList.MdiParent = this;
                frmProductList.Show();
            }
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmImportProduct"] as frmImportProduct == null)
            {
            frmImportProduct frmImportProduct = new frmImportProduct();
            frmImportProduct.MdiParent = this;
            frmImportProduct.Show();
            }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmAddStock"] as frmAddStock == null)
            {
            frmAddStock frmAddStock = new frmAddStock();
            frmAddStock.MdiParent = this;
            frmAddStock.Show();
            }
        }

        private void mANAGEEXPENCESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmManageExpences"] as frmManageExpences == null)
            {
            frmManageExpences frmManageExpences = new frmManageExpences();
            frmManageExpences.MdiParent=this ;
            frmManageExpences.Show();
            }
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            frmPointOfSales frmPointOfSales = new frmPointOfSales();
            frmPointOfSales.ShowDialog();
        }

        private void cURRENTSTOCKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmCurrentStock"] as frmCurrentStock == null)
            {
            frmCurrentStock frmCurrentStock = new frmCurrentStock();
            frmCurrentStock.MdiParent = this;
            frmCurrentStock.Show();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmImportCustomer"] as frmImportCustomer == null)
            {
            frmImportCustomer frmImportCustomer = new frmImportCustomer();
            frmImportCustomer.MdiParent = this;
            frmImportCustomer.Show();
            }
        }


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmExportCustomer"] as frmExportCustomer == null)
            {
            frmExportCustomer frmExportCustomer = new frmExportCustomer();
            frmExportCustomer.MdiParent = this;
            frmExportCustomer.Show();
            }
        }

        private void nEWSUPPIERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmNewSupplier"] as frmNewSupplier == null)
            {
            frmNewSupplier frmNewSupplier = new frmNewSupplier();
            frmNewSupplier.MdiParent = this;
            frmNewSupplier.Show();
            }
        }

        private void sUPPLIERSLISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmSupplierList"] as frmSupplierList == null)
            {
            frmSupplierList frmSupplierList = new frmSupplierList();
            frmSupplierList.MdiParent = this;
            frmSupplierList.Show();
            }
        }

        private void iMPORTDATAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmImportSupplier"] as frmImportSupplier == null)
            {
            frmImportSupplier frmImportSupplier = new frmImportSupplier();
            frmImportSupplier.MdiParent = this;
            frmImportSupplier.Show();
            }
        }

        private void eXPORTDATAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmExportSupplier"] as frmExportSupplier == null)
            {
            frmExportSupplier frmExportSupplier = new frmExportSupplier();
            frmExportSupplier.MdiParent = this;
            frmExportSupplier.Show();
            }
        }

        private void mANAGETAXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmManageTax"] as frmManageTax == null)
            {
            frmManageTax frmManageTax = new frmManageTax();
            frmManageTax.MdiParent = this;
            frmManageTax.Show();
            }
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmPrintBarcode"] as frmPrintBarcode == null)
            {
                frmPrintBarcode frmPrintBarcode = new frmPrintBarcode();
                frmPrintBarcode.MdiParent = this;
                frmPrintBarcode.Show();
            }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmReturnStock"] as frmReturnStock == null)
            {
                frmReturnStock frmReturnStock = new frmReturnStock();
                frmReturnStock.MdiParent = this;
                frmReturnStock.Show();
            }
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmInvoiceList"] as frmInvoiceList == null)
            {
            frmInvoiceList frmInvoiceList = new frmInvoiceList();
            frmInvoiceList.MdiParent = this;
            frmInvoiceList.Show();
            }
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmSalesReturn"] as frmSalesReturn == null)
            {
            frmSalesReturn frmSalesReturn = new frmSalesReturn();
            frmSalesReturn.MdiParent = this;
            frmSalesReturn.Show();
            }
        }

        private void gIFTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmGiftItem"] as frmGiftItem == null)
            {
            frmGiftItem frmGiftItem = new frmGiftItem();
            frmGiftItem.MdiParent = this;
            frmGiftItem.Show();
            }
        }

        private void eXPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["frmExportProduct"] as frmExportProduct == null)
            {
            frmExportProduct frmExportProduct = new frmExportProduct();
            frmExportProduct.MdiParent = this;
            frmExportProduct.Show();
            }
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            DialogResult msg = new DialogResult();
            msg = MessageBox.Show("This will sure that you would like to lock ExpressPOS?", "LOCK EXPRESSPOS?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == DialogResult.Yes)
            {
                frmAuthentication frmAuthentication = new frmAuthentication();
                frmAuthentication.Show();
                this.Hide();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword();
            frmChangePassword.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            cmsReports.Show(btnReports, new Point(210, 0));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //Customer  List
            if (System.Windows.Forms.Application.OpenForms["frm_R_Customer"] as frm_R_Customer == null)
            {
                frm_R_Customer frm_R_Customer = new frm_R_Customer();
                frm_R_Customer.MdiParent = this;
                frm_R_Customer.Show();
            }
        }

        private void sUPPLIERLISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Supplier  List
            if (System.Windows.Forms.Application.OpenForms["frm_R_Supplier"] as frm_R_Supplier == null)
            {
                frm_R_Supplier frm_R_Supplier = new frm_R_Supplier();
                frm_R_Supplier.MdiParent = this;
                frm_R_Supplier.Show();
            }
        }

        private void eMPLOYEELISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Employee  List
            if (System.Windows.Forms.Application.OpenForms["frm_R_Employee"] as frm_R_Employee == null)
            {
                frm_R_Employee frm_R_Employee = new frm_R_Employee();
                frm_R_Employee.MdiParent = this;
                frm_R_Employee.Show();
            }
        }

        private void pRODUCTLISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List of item
            if (System.Windows.Forms.Application.OpenForms["frm_R_Product"] as frm_R_Product == null)
            {
                frm_R_Product frm_R_Product = new frm_R_Product();
                frm_R_Product.MdiParent = this;
                frm_R_Product.Show();
            }
        }

        private void eXPENSESLISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Expences List
            if (System.Windows.Forms.Application.OpenForms["frm_R_Expences"] as frm_R_Expences == null)
            {
                frm_R_Expences frm_R_Expences = new frm_R_Expences();
                frm_R_Expences.MdiParent = this;
                frm_R_Expences.Show();
            }
        }

        private void sTOCKINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stock Movement
            if (System.Windows.Forms.Application.OpenForms["frm_R_StockMovement"] as frm_R_StockMovement == null)
            {
                frm_R_StockMovement frm_R_StockMovement = new frm_R_StockMovement();
                frm_R_StockMovement.MdiParent = this;
                frm_R_StockMovement.Show();
            }
        }

        private void sALESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sales Report
            if (System.Windows.Forms.Application.OpenForms["frm_R_Sales"] as frm_R_Sales == null)
            {
                frm_R_Sales frm_R_Sales = new frm_R_Sales();
                frm_R_Sales.MdiParent = this;
                frm_R_Sales.Show();
            }
        }

        private void sALESRETURNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sales Return Report
            if (System.Windows.Forms.Application.OpenForms["frm_R_SalesReturn"] as frm_R_SalesReturn == null)
            {
                frm_R_SalesReturn frm_R_SalesReturn = new frm_R_SalesReturn();
                frm_R_SalesReturn.MdiParent = this;
                frm_R_SalesReturn.Show();
            }
        }

        private void tAXREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TAX Report
            if (System.Windows.Forms.Application.OpenForms["frm_R_TAX"] as frm_R_TAX == null)
            {
                frm_R_TAX frm_R_TAX = new frm_R_TAX();
                frm_R_TAX.MdiParent = this;
                frm_R_TAX.Show();
            }
        }


    }
}
