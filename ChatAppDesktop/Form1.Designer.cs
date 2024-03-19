namespace ChatAppDesktop
{
    partial class Form1
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
            this.messageRTB = new System.Windows.Forms.RichTextBox();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.messageLbl = new System.Windows.Forms.Label();
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.messageTB = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageRTB
            // 
            this.messageRTB.Cursor = System.Windows.Forms.Cursors.No;
            this.messageRTB.Location = new System.Drawing.Point(472, 36);
            this.messageRTB.Name = "messageRTB";
            this.messageRTB.ReadOnly = true;
            this.messageRTB.Size = new System.Drawing.Size(300, 351);
            this.messageRTB.TabIndex = 0;
            this.messageRTB.Text = "";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLbl.Location = new System.Drawing.Point(54, 72);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(124, 29);
            this.usernameLbl.TabIndex = 1;
            this.usernameLbl.Text = "Username";
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLbl.Location = new System.Drawing.Point(54, 124);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(112, 29);
            this.messageLbl.TabIndex = 2;
            this.messageLbl.Text = "Message";
            // 
            // usernameTB
            // 
            this.usernameTB.Location = new System.Drawing.Point(184, 72);
            this.usernameTB.Multiline = true;
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(162, 29);
            this.usernameTB.TabIndex = 3;
            // 
            // messageTB
            // 
            this.messageTB.Location = new System.Drawing.Point(184, 124);
            this.messageTB.Multiline = true;
            this.messageTB.Name = "messageTB";
            this.messageTB.Size = new System.Drawing.Size(162, 77);
            this.messageTB.TabIndex = 4;
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(184, 207);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(84, 38);
            this.sendBtn.TabIndex = 5;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 451);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.messageTB);
            this.Controls.Add(this.usernameTB);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.messageRTB);
            this.Name = "Form1";
            this.Text = "Form1";
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox messageRTB;
        private System.Windows.Forms.Label usernameLbl;
        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.TextBox messageTB;
        private System.Windows.Forms.Button sendBtn;
    }
}

