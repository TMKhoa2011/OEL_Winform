namespace OEL_PMUD
{
    partial class FormLogin
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
            lblSystemlogin = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnQuit = new Button();
            lnkForgotpassword = new LinkLabel();
            SuspendLayout();
            // 
            // lblSystemlogin
            // 
            lblSystemlogin.AutoSize = true;
            lblSystemlogin.Font = new Font("Segoe UI", 12F);
            lblSystemlogin.Location = new Point(329, 24);
            lblSystemlogin.Name = "lblSystemlogin";
            lblSystemlogin.Size = new Size(128, 28);
            lblSystemlogin.TabIndex = 0;
            lblSystemlogin.Text = "System Login";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(296, 94);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(87, 23);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(389, 91);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(313, 30);
            txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(296, 149);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(80, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(389, 146);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(313, 30);
            txtPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 10F);
            btnLogin.Location = new Point(464, 205);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(136, 48);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnQuit
            // 
            btnQuit.Font = new Font("Segoe UI", 10F);
            btnQuit.Location = new Point(655, 397);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(133, 41);
            btnQuit.TabIndex = 6;
            btnQuit.Text = "Quit";
            btnQuit.UseVisualStyleBackColor = true;
            btnQuit.Click += btnExit_Click;
            // 
            // lnkForgotpassword
            // 
            lnkForgotpassword.AutoSize = true;
            lnkForgotpassword.Font = new Font("Segoe UI", 10F);
            lnkForgotpassword.Location = new Point(559, 179);
            lnkForgotpassword.Name = "lnkForgotpassword";
            lnkForgotpassword.Size = new Size(143, 23);
            lnkForgotpassword.TabIndex = 7;
            lnkForgotpassword.TabStop = true;
            lnkForgotpassword.Text = "Forgot Password?";
            lnkForgotpassword.LinkClicked += lnkForgotPassword_LinkClicked;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lnkForgotpassword);
            Controls.Add(btnQuit);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(lblSystemlogin);
            Name = "FormLogin";
            Text = "FormLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSystemlogin;
        private Label lblUsername;
        private TextBox txtUsername
            ;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnQuit;
        private LinkLabel lnkForgotpassword;
    }
}