namespace OEL_PMUD
{
    partial class FormForgotPassword
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
            lblResetpassword = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPhonenumber = new Label();
            txtPhonenumber = new TextBox();
            lblNewpassword = new Label();
            txtNewpassword = new TextBox();
            btnResetpassword = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // lblResetpassword
            // 
            lblResetpassword.AutoSize = true;
            lblResetpassword.Font = new Font("Segoe UI", 10F);
            lblResetpassword.Location = new Point(280, 30);
            lblResetpassword.Name = "lblResetpassword";
            lblResetpassword.Size = new Size(150, 23);
            lblResetpassword.TabIndex = 0;
            lblResetpassword.Text = "RESET PASSWORD";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(56, 86);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(140, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Registered Email:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(202, 83);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(410, 30);
            txtEmail.TabIndex = 2;
            // 
            // lblPhonenumber
            // 
            lblPhonenumber.AutoSize = true;
            lblPhonenumber.Font = new Font("Segoe UI", 10F);
            lblPhonenumber.Location = new Point(56, 141);
            lblPhonenumber.Name = "lblPhonenumber";
            lblPhonenumber.Size = new Size(131, 23);
            lblPhonenumber.TabIndex = 3;
            lblPhonenumber.Text = "Phone Number:";
            // 
            // txtPhonenumber
            // 
            txtPhonenumber.Font = new Font("Segoe UI", 10F);
            txtPhonenumber.Location = new Point(202, 138);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.Size = new Size(410, 30);
            txtPhonenumber.TabIndex = 4;
            // 
            // lblNewpassword
            // 
            lblNewpassword.AutoSize = true;
            lblNewpassword.Font = new Font("Segoe UI", 10F);
            lblNewpassword.Location = new Point(56, 199);
            lblNewpassword.Name = "lblNewpassword";
            lblNewpassword.Size = new Size(123, 23);
            lblNewpassword.TabIndex = 5;
            lblNewpassword.Text = "New Password:";
            // 
            // txtNewpassword
            // 
            txtNewpassword.Font = new Font("Segoe UI", 10F);
            txtNewpassword.Location = new Point(202, 196);
            txtNewpassword.Name = "txtNewpassword";
            txtNewpassword.Size = new Size(410, 30);
            txtNewpassword.TabIndex = 6;
            // 
            // btnResetpassword
            // 
            btnResetpassword.Font = new Font("Segoe UI", 10F);
            btnResetpassword.Location = new Point(299, 248);
            btnResetpassword.Name = "btnResetpassword";
            btnResetpassword.Size = new Size(162, 48);
            btnResetpassword.TabIndex = 7;
            btnResetpassword.Text = "Reset Password";
            btnResetpassword.UseVisualStyleBackColor = true;
            btnResetpassword.Click += btnReset_Click;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 10F);
            btnBack.Location = new Point(655, 400);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(133, 38);
            btnBack.TabIndex = 8;
            btnBack.Text = "Back to Login";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // FormForgotPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnResetpassword);
            Controls.Add(txtNewpassword);
            Controls.Add(lblNewpassword);
            Controls.Add(txtPhonenumber);
            Controls.Add(lblPhonenumber);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(lblResetpassword);
            Name = "FormForgotPassword";
            Text = "FormForgotPassword";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblResetpassword;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhonenumber;
        private TextBox txtPhonenumber;
        private Label lblNewpassword;
        private TextBox txtNewpassword;
        private Button btnResetpassword;
        private Button btnBack;
    }
}