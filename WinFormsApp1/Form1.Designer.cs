namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRet = new Button();
            btnSave = new Button();
            display = new RichTextBox();
            user = new TextBox();
            message = new TextBox();
            SuspendLayout();
            // 
            // btnRet
            // 
            btnRet.Location = new Point(167, 345);
            btnRet.Name = "btnRet";
            btnRet.Size = new Size(132, 40);
            btnRet.TabIndex = 0;
            btnRet.Text = "retrieve";
            btnRet.UseVisualStyleBackColor = true;
            btnRet.Click += btnRet_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(167, 285);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(132, 40);
            btnSave.TabIndex = 1;
            btnSave.Text = "save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // display
            // 
            display.Location = new Point(493, 57);
            display.Name = "display";
            display.Size = new Size(157, 199);
            display.TabIndex = 3;
            display.Text = "";
            // 
            // user
            // 
            user.Location = new Point(131, 79);
            user.Multiline = true;
            user.Name = "user";
            user.Size = new Size(206, 50);
            user.TabIndex = 4;
            // 
            // message
            // 
            message.Location = new Point(131, 135);
            message.Multiline = true;
            message.Name = "message";
            message.Size = new Size(206, 50);
            message.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(message);
            Controls.Add(user);
            Controls.Add(display);
            Controls.Add(btnSave);
            Controls.Add(btnRet);
            Name = "Form1";
            Text = "Form1";
          
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRet;
        private Button btnSave;
        private RichTextBox display;
        private TextBox user;
        private TextBox message;
    }
}
