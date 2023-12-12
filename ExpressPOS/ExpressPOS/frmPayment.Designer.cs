namespace ExpressPOS
{
    partial class frmPayment
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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("CASH", 1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("VISA", 4);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("MASTER CARD", 3);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("AMEX", 0);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("DISCOVER", 2);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("PAYPAL", 5);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayment));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtPayAmount = new System.Windows.Forms.TextBox();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOrderTotal = new System.Windows.Forms.TextBox();
            this.txtTotalTax = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtFright = new System.Windows.Forms.TextBox();
            this.txtGTotal = new System.Windows.Forms.TextBox();
            this.txtChange = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtPaymentMethod = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button15 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.btnFormClose = new System.Windows.Forms.Button();
            this.FormHeader = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.rbPOSInvoice = new System.Windows.Forms.RadioButton();
            this.rbTAXInvoice = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listViewItem7.ToolTipText = "CASH";
            listViewItem8.ToolTipText = "VISA";
            listViewItem9.ToolTipText = "MASTER CARD";
            listViewItem10.ToolTipText = "AMEX";
            listViewItem11.ToolTipText = "DISCOVER";
            listViewItem12.ToolTipText = "PAYPAL";
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.listView1.Location = new System.Drawing.Point(12, 91);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(390, 193);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 23;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "pos_amex.png");
            this.imageList1.Images.SetKeyName(1, "pos_cash.png");
            this.imageList1.Images.SetKeyName(2, "pos_discover.png");
            this.imageList1.Images.SetKeyName(3, "pos_master_card.png");
            this.imageList1.Images.SetKeyName(4, "pos_visa_card.png");
            this.imageList1.Images.SetKeyName(5, "paypal.png");
            // 
            // txtPayAmount
            // 
            this.txtPayAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtPayAmount.ForeColor = System.Drawing.Color.Red;
            this.txtPayAmount.Location = new System.Drawing.Point(134, 373);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.Size = new System.Drawing.Size(100, 22);
            this.txtPayAmount.TabIndex = 27;
            this.txtPayAmount.TextChanged += new System.EventHandler(this.txtPayAmount_TextChanged);
            this.txtPayAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPayAmount_KeyPress);
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNo.Location = new System.Drawing.Point(12, 62);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.ReadOnly = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(390, 23);
            this.txtInvoiceNo.TabIndex = 40;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(11, 44);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(69, 16);
            this.label22.TabIndex = 39;
            this.label22.Text = "INVOICE #";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "PAYMENT METHOD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "ORDER TOTAL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "FREIGHT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 349);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "TOTAL TAX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "DISCOUNT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 378);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "CHANGE";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(240, 349);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "G. TOTAL";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 406);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "COMMENT";
            // 
            // txtOrderTotal
            // 
            this.txtOrderTotal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderTotal.Location = new System.Drawing.Point(134, 316);
            this.txtOrderTotal.Name = "txtOrderTotal";
            this.txtOrderTotal.ReadOnly = true;
            this.txtOrderTotal.Size = new System.Drawing.Size(100, 22);
            this.txtOrderTotal.TabIndex = 51;
            this.txtOrderTotal.TextChanged += new System.EventHandler(this.txtOrderTotal_TextChanged);
            // 
            // txtTotalTax
            // 
            this.txtTotalTax.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalTax.Location = new System.Drawing.Point(134, 344);
            this.txtTotalTax.Name = "txtTotalTax";
            this.txtTotalTax.ReadOnly = true;
            this.txtTotalTax.Size = new System.Drawing.Size(100, 22);
            this.txtTotalTax.TabIndex = 52;
            this.txtTotalTax.TextChanged += new System.EventHandler(this.txtTotalTax_TextChanged);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(309, 316);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(93, 22);
            this.txtDiscount.TabIndex = 54;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscount_KeyPress);
            // 
            // txtFright
            // 
            this.txtFright.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFright.Location = new System.Drawing.Point(309, 289);
            this.txtFright.Name = "txtFright";
            this.txtFright.Size = new System.Drawing.Size(93, 22);
            this.txtFright.TabIndex = 53;
            this.txtFright.TextChanged += new System.EventHandler(this.txtFright_TextChanged);
            this.txtFright.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFright_KeyPress);
            // 
            // txtGTotal
            // 
            this.txtGTotal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGTotal.Location = new System.Drawing.Point(309, 344);
            this.txtGTotal.Name = "txtGTotal";
            this.txtGTotal.ReadOnly = true;
            this.txtGTotal.Size = new System.Drawing.Size(93, 22);
            this.txtGTotal.TabIndex = 55;
            // 
            // txtChange
            // 
            this.txtChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtChange.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChange.Location = new System.Drawing.Point(309, 373);
            this.txtChange.Name = "txtChange";
            this.txtChange.ReadOnly = true;
            this.txtChange.Size = new System.Drawing.Size(93, 22);
            this.txtChange.TabIndex = 56;
            this.txtChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtComment
            // 
            this.txtComment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComment.Location = new System.Drawing.Point(134, 401);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(268, 22);
            this.txtComment.TabIndex = 57;
            // 
            // txtPaymentMethod
            // 
            this.txtPaymentMethod.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentMethod.Location = new System.Drawing.Point(134, 289);
            this.txtPaymentMethod.Name = "txtPaymentMethod";
            this.txtPaymentMethod.ReadOnly = true;
            this.txtPaymentMethod.Size = new System.Drawing.Size(100, 22);
            this.txtPaymentMethod.TabIndex = 58;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(235)))));
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox3.Location = new System.Drawing.Point(0, 430);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(538, 15);
            this.pictureBox3.TabIndex = 143;
            this.pictureBox3.TabStop = false;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Azure;
            this.button15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button15.ForeColor = System.Drawing.Color.Black;
            this.button15.Image = global::ExpressPOS.Properties.Resources.printer_32_1;
            this.button15.Location = new System.Drawing.Point(408, 176);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(118, 108);
            this.button15.TabIndex = 61;
            this.button15.Text = "Print Preview";
            this.button15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button15.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.Azure;
            this.button13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.Color.Black;
            this.button13.Image = global::ExpressPOS.Properties.Resources.save_new_64;
            this.button13.Location = new System.Drawing.Point(408, 61);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(118, 109);
            this.button13.TabIndex = 59;
            this.button13.Text = "Complete";
            this.button13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(139)))), ((int)(((byte)(4)))));
            this.pictureBox9.Location = new System.Drawing.Point(12, 61);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(390, 3);
            this.pictureBox9.TabIndex = 41;
            this.pictureBox9.TabStop = false;
            // 
            // btnFormClose
            // 
            this.btnFormClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(93)))), ((int)(((byte)(180)))));
            this.btnFormClose.BackgroundImage = global::ExpressPOS.Properties.Resources.No_01;
            this.btnFormClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFormClose.FlatAppearance.BorderSize = 0;
            this.btnFormClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormClose.Location = new System.Drawing.Point(500, 1);
            this.btnFormClose.Name = "btnFormClose";
            this.btnFormClose.Size = new System.Drawing.Size(36, 36);
            this.btnFormClose.TabIndex = 16;
            this.btnFormClose.UseVisualStyleBackColor = false;
            this.btnFormClose.Click += new System.EventHandler(this.btnFormClose_Click);
            // 
            // FormHeader
            // 
            this.FormHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(93)))), ((int)(((byte)(180)))));
            this.FormHeader.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormHeader.Location = new System.Drawing.Point(0, 0);
            this.FormHeader.Name = "FormHeader";
            this.FormHeader.Size = new System.Drawing.Size(538, 37);
            this.FormHeader.TabIndex = 15;
            this.FormHeader.TabStop = false;
            this.FormHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormHeader_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(235)))));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox1.Location = new System.Drawing.Point(535, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 401);
            this.pictureBox1.TabIndex = 144;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(235)))));
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox2.Location = new System.Drawing.Point(-7, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(10, 402);
            this.pictureBox2.TabIndex = 145;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(122, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 14);
            this.label3.TabIndex = 146;
            this.label3.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 378);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 147;
            this.label10.Text = "PAYMENT AMOUNT";
            // 
            // rbPOSInvoice
            // 
            this.rbPOSInvoice.AutoSize = true;
            this.rbPOSInvoice.Checked = true;
            this.rbPOSInvoice.Location = new System.Drawing.Point(419, 378);
            this.rbPOSInvoice.Name = "rbPOSInvoice";
            this.rbPOSInvoice.Size = new System.Drawing.Size(95, 17);
            this.rbPOSInvoice.TabIndex = 148;
            this.rbPOSInvoice.TabStop = true;
            this.rbPOSInvoice.Text = "POS Receipt";
            this.rbPOSInvoice.UseVisualStyleBackColor = true;
            // 
            // rbTAXInvoice
            // 
            this.rbTAXInvoice.AutoSize = true;
            this.rbTAXInvoice.Location = new System.Drawing.Point(419, 401);
            this.rbTAXInvoice.Name = "rbTAXInvoice";
            this.rbTAXInvoice.Size = new System.Drawing.Size(93, 17);
            this.rbTAXInvoice.TabIndex = 149;
            this.rbTAXInvoice.Text = "TAX Invoice";
            this.rbTAXInvoice.UseVisualStyleBackColor = true;
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 434);
            this.ControlBox = false;
            this.Controls.Add(this.rbTAXInvoice);
            this.Controls.Add(this.rbPOSInvoice);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.txtPaymentMethod);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.txtChange);
            this.Controls.Add(this.txtGTotal);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtFright);
            this.Controls.Add(this.txtTotalTax);
            this.Controls.Add(this.txtOrderTotal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInvoiceNo);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtPayAmount);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnFormClose);
            this.Controls.Add(this.FormHeader);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFormClose;
        private System.Windows.Forms.PictureBox FormHeader;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtPayAmount;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOrderTotal;
        private System.Windows.Forms.TextBox txtTotalTax;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtFright;
        private System.Windows.Forms.TextBox txtGTotal;
        private System.Windows.Forms.TextBox txtChange;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtPaymentMethod;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbPOSInvoice;
        private System.Windows.Forms.RadioButton rbTAXInvoice;
    }
}