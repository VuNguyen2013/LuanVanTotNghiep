namespace Updater
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
            this.pnHoseStatus = new System.Windows.Forms.Panel();
            this.pnHNXStatus = new System.Windows.Forms.Panel();
            this.pnUpComStatus = new System.Windows.Forms.Panel();
            this.HOSE = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btAction = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tmStatus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pnHoseStatus
            // 
            this.pnHoseStatus.BackColor = System.Drawing.Color.Transparent;
            this.pnHoseStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnHoseStatus.Location = new System.Drawing.Point(57, 74);
            this.pnHoseStatus.Name = "pnHoseStatus";
            this.pnHoseStatus.Size = new System.Drawing.Size(29, 26);
            this.pnHoseStatus.TabIndex = 0;
            // 
            // pnHNXStatus
            // 
            this.pnHNXStatus.BackColor = System.Drawing.Color.Transparent;
            this.pnHNXStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnHNXStatus.Location = new System.Drawing.Point(148, 74);
            this.pnHNXStatus.Name = "pnHNXStatus";
            this.pnHNXStatus.Size = new System.Drawing.Size(29, 26);
            this.pnHNXStatus.TabIndex = 0;
            // 
            // pnUpComStatus
            // 
            this.pnUpComStatus.BackColor = System.Drawing.Color.Transparent;
            this.pnUpComStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnUpComStatus.Location = new System.Drawing.Point(247, 74);
            this.pnUpComStatus.Name = "pnUpComStatus";
            this.pnUpComStatus.Size = new System.Drawing.Size(29, 26);
            this.pnUpComStatus.TabIndex = 0;
            // 
            // HOSE
            // 
            this.HOSE.AutoSize = true;
            this.HOSE.Location = new System.Drawing.Point(13, 81);
            this.HOSE.Name = "HOSE";
            this.HOSE.Size = new System.Drawing.Size(37, 13);
            this.HOSE.TabIndex = 1;
            this.HOSE.Text = "HOSE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "HNX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "UPCOM";
            // 
            // btAction
            // 
            this.btAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btAction.Location = new System.Drawing.Point(107, 250);
            this.btAction.Name = "btAction";
            this.btAction.Size = new System.Drawing.Size(90, 38);
            this.btAction.TabIndex = 1;
            this.btAction.Text = "Bắt đầu";
            this.btAction.UseVisualStyleBackColor = true;
            this.btAction.Click += new System.EventHandler(this.btAction_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(114, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "UPDATER";
            // 
            // txtServerIP
            // 
            this.txtServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerIP.Location = new System.Drawing.Point(112, 171);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(134, 20);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Leave += new System.EventHandler(this.txtServerIP_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "IP Server sở";
            // 
            // tmStatus
            // 
            this.tmStatus.Interval = 600;
            this.tmStatus.Tick += new System.EventHandler(this.tmStatus_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 313);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.btAction);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HOSE);
            this.Controls.Add(this.pnUpComStatus);
            this.Controls.Add(this.pnHNXStatus);
            this.Controls.Add(this.pnHoseStatus);
            this.Name = "MainForm";
            this.Text = "Updater";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnHoseStatus;
        private System.Windows.Forms.Panel pnHNXStatus;
        private System.Windows.Forms.Panel pnUpComStatus;
        private System.Windows.Forms.Label HOSE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer tmStatus;

    }
}

