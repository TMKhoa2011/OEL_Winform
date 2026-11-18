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
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show($"Login Success {user.Fullname}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();

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

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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