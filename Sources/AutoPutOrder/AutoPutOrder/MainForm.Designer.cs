namespace AutoPutOrder
{
    partial class MainForm
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
            this.txtBAcc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSAcc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.txtListSymbol = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnStatus = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBAcc
            // 
            this.txtBAcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBAcc.Location = new System.Drawing.Point(177, 101);
            this.txtBAcc.Name = "txtBAcc";
            this.txtBAcc.Size = new System.Drawing.Size(199, 20);
            this.txtBAcc.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tài khoản đặt lệnh mua";
            // 
            // txtSAcc
            // 
            this.txtSAcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSAcc.Location = new System.Drawing.Point(177, 155);
            this.txtSAcc.Name = "txtSAcc";
            this.txtSAcc.Size = new System.Drawing.Size(199, 20);
            this.txtSAcc.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tài khoản đặt lệnh bán";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.Location = new System.Drawing.Point(97, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(295, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "ĐẨY LỆNH CHỨNG KHOÁN TỰ ĐỘNG";
            // 
            // txtDuration
            // 
            this.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDuration.Location = new System.Drawing.Point(177, 208);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(199, 20);
            this.txtDuration.TabIndex = 3;
            this.txtDuration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuration_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Thời gian dãn cách";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(392, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Mili giây";
            // 
            // btOK
            // 
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btOK.Location = new System.Drawing.Point(206, 315);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(87, 37);
            this.btOK.TabIndex = 5;
            this.btOK.Text = "Bắt đầu";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // txtListSymbol
            // 
            this.txtListSymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtListSymbol.Location = new System.Drawing.Point(177, 261);
            this.txtListSymbol.Name = "txtListSymbol";
            this.txtListSymbol.Size = new System.Drawing.Size(199, 20);
            this.txtListSymbol.TabIndex = 4;
            this.txtListSymbol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuration_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Danh sách mã CK";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(392, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Phân cách bởi \";\"";
            // 
            // pnStatus
            // 
            this.pnStatus.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnStatus.Location = new System.Drawing.Point(1, 406);
            this.pnStatus.Name = "pnStatus";
            this.pnStatus.Size = new System.Drawing.Size(487, 10);
            this.pnStatus.TabIndex = 6;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbStatus.Location = new System.Drawing.Point(207, 379);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(86, 13);
            this.lbStatus.TabIndex = 7;
            this.lbStatus.Text = "Đang đẩy lệnh...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 415);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.pnStatus);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtListSymbol);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtSAcc);
            this.Controls.Add(this.txtBAcc);
            this.Name = "MainForm";
            this.Text = "Auto put order";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBAcc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSAcc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.TextBox txtListSymbol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnStatus;
        private System.Windows.Forms.Label lbStatus;
    }
}

