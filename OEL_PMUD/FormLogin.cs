using OEL_PMUD.Models;
using OEL_PMUD.Models.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OEL_PMUD
{
    public partial class FormLogin : Form
    {
        private bool _isLoggingIn = false;

        public FormLogin()
        {
            InitializeComponent();

            this.AcceptButton = btnLogin;
            this.Load += FormLogin_Load;

            // Tránh gắn trùng sự kiện Click nếu Designer hoặc code khác đã gắn sẵn
            btnLogin.Click -= btnLogin_Click;
            btnLogin.Click += btnLogin_Click;
        }

        private void FormLogin_Load(object? sender, EventArgs e)
        {
            // Làm ấm kết nối EF Core + SQL Server ở nền để lần bấm Login đầu không bị khựng lâu
            _ = Task.Run(() =>
            {
                try
                {
                    using var warmDb = new Appdbcontext();
                    _ = warmDb.Users.Any();
                }
                catch
                {
                    // Bỏ qua lỗi khởi tạo ngầm; lỗi thật sẽ báo rõ khi bấm Login
                }
            });
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            if (_isLoggingIn) return;

            string account = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email/phone and password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isLoggingIn = true;
            btnLogin.Enabled = false;
            Cursor = Cursors.WaitCursor;

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
                            using var frm1 = new Form1();
                            frm1.ShowDialog();
                        }
                        else
                        {
                            using var frmQuiz = new FormQuiz();
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
            finally
            {
                Cursor = Cursors.Default;
                btnLogin.Enabled = true;
                _isLoggingIn = false;
            }
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkForgotPassword_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            using var frmForgot = new FormForgotPassword();
            frmForgot.ShowDialog();
        }
    }
}