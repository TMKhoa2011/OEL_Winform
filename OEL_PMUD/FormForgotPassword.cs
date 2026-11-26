using OEL_PMUD.Models.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OEL_PMUD
{
    public partial class FormForgotPassword : Form
    {
        public FormForgotPassword()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string phone = txtPhonenumber.Text.Trim();
            string newPassword = txtNewpassword.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please fill in all required fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new Appdbcontext())
                {
                    // Đối chiếu cả Email và Phone trong database
                    var user = db.Users.FirstOrDefault(u => u.Email == email && u.Phone == phone);

                    if (user != null)
                    {
                        // Cập nhật mật khẩu mới
                        user.Passwordhash = newPassword;
                        db.SaveChanges();

                        MessageBox.Show("Password reset successful! You can now log in with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No account found matching the provided Email and Phone number!", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
