namespace StockCore.InfoSender
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
            this.components = new System.ComponentModel.Container();
            this.lbIP = new System.Windows.Forms.ListBox();
            this.btAction = new System.Windows.Forms.Button();
            this.btIPAdd = new System.Windows.Forms.Button();
            this.btIPDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnStatus = new System.Windows.Forms.Panel();
            this.tmAction = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbIP
            // 
            this.lbIP.FormattingEnabled = true;
            this.lbIP.Location = new System.Drawing.Point(12, 91);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(292, 290);
            this.lbIP.TabIndex = 0;
            // 
            // btAction
            // 
            this.btAction.BackColor = System.Drawing.Color.Transparent;
            this.btAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btAction.Location = new System.Drawing.Point(208, 421);
            this.btAction.Name = "btAction";
            this.btAction.Size = new System.Drawing.Size(106, 37);
            this.btAction.TabIndex = 1;
            this.btAction.Text = "Bắt đầu";
            this.btAction.UseVisualStyleBackColor = false;
            this.btAction.Click += new System.EventHandler(this.btAction_Click);
            // 
            // btIPAdd
            // 
            this.btIPAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btIPAdd.Location = new System.Drawing.Point(310, 91);
            this.btIPAdd.Name = "btIPAdd";
            this.btIPAdd.Size = new System.Drawing.Size(60, 27);
            this.btIPAdd.TabIndex = 1;
            this.btIPAdd.Text = "Thêm";
            this.btIPAdd.UseVisualStyleBackColor = true;
            this.btIPAdd.Click += new System.EventHandler(this.lbIPAdd_Click);
            // 
            // btIPDelete
            // 
            this.btIPDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btIPDelete.Location = new System.Drawing.Point(310, 124);
            this.btIPDelete.Name = "btIPDelete";
            this.btIPDelete.Size = new System.Drawing.Size(60, 27);
            this.btIPDelete.TabIndex = 1;
            this.btIPDelete.Text = "Xóa";
            this.btIPDelete.UseVisualStyleBackColor = true;
            this.btIPDelete.Click += new System.EventHandler(this.btIPDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh sách IP nhận thông tin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(125, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "CORE TRUYỀN THÔNG TIN THỊ TRƯỜNG";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(398, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Trạng thái";
            // 
            // pnStatus
            // 
            this.pnStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnStatus.Location = new System.Drawing.Point(459, 87);
            this.pnStatus.Name = "pnStatus";
            this.pnStatus.Size = new System.Drawing.Size(29, 21);
            this.pnStatus.TabIndex = 3;
            // 
            // tmAction
            // 
            this.tmAction.Interval = 1000;
            this.tmAction.Tick += new System.EventHandler(this.tmAction_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 481);
            this.Controls.Add(this.pnStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btIPDelete);
            this.Controls.Add(this.btIPAdd);
            this.Controls.Add(this.btAction);
            this.Controls.Add(this.lbIP);
            this.Name = "MainForm";
            this.Text = "Stock core";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbIP;
        private System.Windows.Forms.Button btAction;
        private System.Windows.Forms.Button btIPAdd;
        private System.Windows.Forms.Button btIPDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnStatus;
        private System.Windows.Forms.Timer tmAction;
    }
}

