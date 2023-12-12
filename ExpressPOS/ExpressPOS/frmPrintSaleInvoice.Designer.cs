namespace ExpressPOS
{
    partial class frmPrintSaleInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dsExpressPOS = new ExpressPOS.Dataset.dsExpressPOS();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SalesInvoiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dsExpressPOS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesInvoiceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dsExpressPOS
            // 
            this.dsExpressPOS.DataSetName = "dsExpressPOS";
            this.dsExpressPOS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "SalesInvoice";
            reportDataSource1.Value = this.SalesInvoiceBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ExpressPOS.Report.Reports.Invoice.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(765, 404);
            this.reportViewer1.TabIndex = 0;
            // 
            // SalesInvoiceBindingSource
            // 
            this.SalesInvoiceBindingSource.DataMember = "SalesInvoice";
            this.SalesInvoiceBindingSource.DataSource = this.dsExpressPOS;
            // 
            // frmPrintSaleInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 404);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmPrintSaleInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrintSaleInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsExpressPOS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesInvoiceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Dataset.dsExpressPOS dsExpressPOS;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SalesInvoiceBindingSource;
    }
}