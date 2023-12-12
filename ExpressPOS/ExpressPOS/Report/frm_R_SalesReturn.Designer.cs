namespace ExpressPOS.Report
{
    partial class frm_R_SalesReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_R_SalesReturn));
            this.label2 = new System.Windows.Forms.Label();
            this.btnMinimizeForm = new System.Windows.Forms.Button();
            this.btnFormClose = new System.Windows.Forms.Button();
            this.FormHeader = new System.Windows.Forms.PictureBox();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.rbProductWiseSales = new System.Windows.Forms.RadioButton();
            this.rbDateWiseSales = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.FormHeader)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 29);
            this.label2.TabIndex = 141;
            this.label2.Text = "Sales Return";
            // 
            // btnMinimizeForm
            // 
            this.btnMinimizeForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(93)))), ((int)(((byte)(180)))));
            this.btnMinimizeForm.BackgroundImage = global::ExpressPOS.Properties.Resources.Minimize_WF;
            this.btnMinimizeForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMinimizeForm.FlatAppearance.BorderSize = 0;
            this.btnMinimizeForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizeForm.Location = new System.Drawing.Point(304, 0);
            this.btnMinimizeForm.Name = "btnMinimizeForm";
            this.btnMinimizeForm.Size = new System.Drawing.Size(36, 36);
            this.btnMinimizeForm.TabIndex = 140;
            this.btnMinimizeForm.UseVisualStyleBackColor = false;
            this.btnMinimizeForm.Click += new System.EventHandler(this.btnMinimizeForm_Click);
            // 
            // btnFormClose
            // 
            this.btnFormClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(93)))), ((int)(((byte)(180)))));
            this.btnFormClose.BackgroundImage = global::ExpressPOS.Properties.Resources.No_01;
            this.btnFormClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFormClose.FlatAppearance.BorderSize = 0;
            this.btnFormClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormClose.Location = new System.Drawing.Point(343, 0);
            this.btnFormClose.Name = "btnFormClose";
            this.btnFormClose.Size = new System.Drawing.Size(36, 36);
            this.btnFormClose.TabIndex = 139;
            this.btnFormClose.UseVisualStyleBackColor = false;
            this.btnFormClose.Click += new System.EventHandler(this.btnFormClose_Click);
            // 
            // FormHeader
            // 
            this.FormHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(93)))), ((int)(((byte)(180)))));
            this.FormHeader.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormHeader.Location = new System.Drawing.Point(0, 0);
            this.FormHeader.Name = "FormHeader";
            this.FormHeader.Size = new System.Drawing.Size(379, 37);
            this.FormHeader.TabIndex = 138;
            this.FormHeader.TabStop = false;
            this.FormHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormHeader_MouseDown);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(93)))), ((int)(((byte)(180)))));
            this.btnPrintPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrintPreview.FlatAppearance.BorderSize = 0;
            this.btnPrintPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPreview.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintPreview.ForeColor = System.Drawing.Color.White;
            this.btnPrintPreview.Image = global::ExpressPOS.Properties.Resources.Check_02;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(137, 204);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(102, 33);
            this.btnPrintPreview.TabIndex = 143;
            this.btnPrintPreview.Text = "PREVIEW";
            this.btnPrintPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintPreview.UseVisualStyleBackColor = false;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateFrom);
            this.groupBox1.Controls.Add(this.dateTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Location = new System.Drawing.Point(46, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 68);
            this.groupBox1.TabIndex = 142;
            this.groupBox1.TabStop = false;
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "MMM-dd-yyyy";
            this.dateFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(18, 34);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(124, 22);
            this.dateFrom.TabIndex = 119;
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "MMM-dd-yyyy";
            this.dateTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(162, 34);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(124, 22);
            this.dateTo.TabIndex = 118;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(159, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 111;
            this.label1.Text = "Date To";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(15, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 14);
            this.label14.TabIndex = 109;
            this.label14.Text = "Date From";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(235)))));
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox2.Location = new System.Drawing.Point(376, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 274);
            this.pictureBox2.TabIndex = 144;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(235)))));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox1.Location = new System.Drawing.Point(-12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 274);
            this.pictureBox1.TabIndex = 145;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(235)))));
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox3.Location = new System.Drawing.Point(-12, 244);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(391, 19);
            this.pictureBox3.TabIndex = 146;
            this.pictureBox3.TabStop = false;
            // 
            // cmbProducts
            // 
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(64, 113);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(268, 21);
            this.cmbProducts.TabIndex = 153;
            // 
            // rbProductWiseSales
            // 
            this.rbProductWiseSales.AutoSize = true;
            this.rbProductWiseSales.Location = new System.Drawing.Point(46, 94);
            this.rbProductWiseSales.Name = "rbProductWiseSales";
            this.rbProductWiseSales.Size = new System.Drawing.Size(182, 17);
            this.rbProductWiseSales.TabIndex = 152;
            this.rbProductWiseSales.Text = "Date && product wise sales return\r\n";
            this.rbProductWiseSales.UseVisualStyleBackColor = true;
            this.rbProductWiseSales.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbDateWiseSales
            // 
            this.rbDateWiseSales.AutoSize = true;
            this.rbDateWiseSales.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbDateWiseSales.Checked = true;
            this.rbDateWiseSales.Location = new System.Drawing.Point(46, 73);
            this.rbDateWiseSales.Name = "rbDateWiseSales";
            this.rbDateWiseSales.Size = new System.Drawing.Size(132, 17);
            this.rbDateWiseSales.TabIndex = 151;
            this.rbDateWiseSales.TabStop = true;
            this.rbDateWiseSales.Text = "Date wise sales return";
            this.rbDateWiseSales.UseVisualStyleBackColor = true;
            this.rbDateWiseSales.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // frm_R_SalesReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 247);
            this.Controls.Add(this.cmbProducts);
            this.Controls.Add(this.rbProductWiseSales);
            this.Controls.Add(this.rbDateWiseSales);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnPrintPreview);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnMinimizeForm);
            this.Controls.Add(this.btnFormClose);
            this.Controls.Add(this.FormHeader);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_R_SalesReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Return";
            this.Load += new System.EventHandler(this.frm_R_SalesReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FormHeader)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMinimizeForm;
        private System.Windows.Forms.Button btnFormClose;
        private System.Windows.Forms.PictureBox FormHeader;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.RadioButton rbProductWiseSales;
        private System.Windows.Forms.RadioButton rbDateWiseSales;
    }
}