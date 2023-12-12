namespace ExpressPOS
{
    partial class frmReportExpencesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportExpencesView));
            this.ExpensesListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsExpressPOS = new ExpressPOS.Dataset.dsExpressPOS();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ExpensesListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExpressPOS)).BeginInit();
            this.SuspendLayout();
            // 
            // ExpensesListBindingSource
            // 
            this.ExpensesListBindingSource.DataMember = "ExpensesList";
            this.ExpensesListBindingSource.DataSource = this.dsExpressPOS;
            // 
            // dsExpressPOS
            // 
            this.dsExpressPOS.DataSetName = "dsExpressPOS";
            this.dsExpressPOS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ExpensesList";
            reportDataSource1.Value = this.ExpensesListBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ExpressPOS.Report.Reports.ExpensesVoucher.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(797, 409);
            this.reportViewer1.TabIndex = 1;
            // 
            // frmReportExpencesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 409);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportExpencesView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportExpencesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ExpensesListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExpressPOS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Dataset.dsExpressPOS dsExpressPOS;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ExpensesListBindingSource;
    }
}