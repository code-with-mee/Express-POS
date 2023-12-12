namespace ExpressPOS.Report.ReportViewer
{
    partial class frm_Rv_Employee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Rv_Employee));
            this.ExpressDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsExpressPOS = new ExpressPOS.Dataset.dsExpressPOS();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ExpressDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExpressPOS)).BeginInit();
            this.SuspendLayout();
            // 
            // ExpressDataSetBindingSource
            // 
            this.ExpressDataSetBindingSource.DataMember = "Employee";
            this.ExpressDataSetBindingSource.DataSource = this.dsExpressPOS;
            // 
            // dsExpressPOS
            // 
            this.dsExpressPOS.DataSetName = "dsExpressPOS";
            this.dsExpressPOS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Employee";
            reportDataSource1.Value = this.ExpressDataSetBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ExpressPOS.Reports.Employee.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(719, 313);
            this.reportViewer1.TabIndex = 2;
            // 
            // frm_Rv_Employee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 313);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Rv_Employee";
            this.Text = "Report Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Rv_Employee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ExpressDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExpressPOS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        public System.Windows.Forms.BindingSource ExpressDataSetBindingSource;
        public Dataset.dsExpressPOS dsExpressPOS;
    }
}