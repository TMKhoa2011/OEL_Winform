using OEL_PMUD.Models;
using OEL_PMUD.Models.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OEL_PMUD
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string account = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email/phone and password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new Appdbcontext())
                {
                    // Kiểm tra Email hoặc Phone khớp với Passwordhash trong bảng Users
                    var user = db.Users.FirstOrDefault(u => (u.Email == account || u.Phone == account) && u.Passwordhash == password);

                    if (user != null)
                    {
                        Session.CurrentUser = user;

                        MessageBox.Show($"Login successful! Welcome, {user.Fullname}", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        txtPassword.Clear();

                        string role = user.Role?.Trim().ToLower() ?? "";
                        if (role == "admin" || role == "teacher")
                        {
                            FormQuestion frmQ = new FormQuestion();
                            frmQ.ShowDialog();
                        }
                        else
                        {
                            FormQuiz frmQuiz = new FormQuiz();
                            frmQuiz.ShowDialog();
                        }

                        Session.CurrentUser = null;
                        this.Show();
                        txtUsername.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalid account or password!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormForgotPassword frmForgot = new FormForgotPassword();
            frmForgot.ShowDialog();
        }
    }
}