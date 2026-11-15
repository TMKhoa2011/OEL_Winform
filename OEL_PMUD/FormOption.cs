using System;
using System.Linq;
using System.Windows.Forms;
using OEL_PMUD.Models;
using OEL_PMUD.Models.Data;

namespace OEL_PMUD
{
    public partial class FormOption : Form
    {
        private Appdbcontext _context;

        public FormOption()
        {
            InitializeComponent();
            _context = new Appdbcontext();

            // Gọi trực tiếp ở constructor để đảm bảo dữ liệu luôn được load khi mở form
            LoadQuestionsToComboBox();
        }

        private void FormOption_Load(object sender, EventArgs e)
        {
            // Dự phòng sự kiện Load của Form
            LoadQuestionsToComboBox();
        }

        // 1. Nạp danh sách câu hỏi vào ComboBox
        private void LoadQuestionsToComboBox()
        {
            try
            {
                var questions = _context.Questions.ToList();

                // Tạm thời gỡ sự kiện SelectedIndexChanged để tránh việc trigger nhầm khi binding dữ liệu
                cbSelectquestion.SelectedIndexChanged -= cbSelectquestion_SelectedIndexChanged;

                cbSelectquestion.DataSource = questions;
                cbSelectquestion.DisplayMember = "Content";
                cbSelectquestion.ValueMember = "Id";
                cbSelectquestion.SelectedIndex = -1;       // Để trống lúc ban đầu

                // Gắn lại sự kiện
                cbSelectquestion.SelectedIndexChanged += cbSelectquestion_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Sự kiện khi người dùng chọn câu hỏi trên ComboBox
        private void cbSelectquestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelectquestion.SelectedIndex != -1 && cbSelectquestion.SelectedValue != null)
            {
                if (int.TryParse(cbSelectquestion.SelectedValue.ToString(), out int questionId))
                {
                    LoadOptionsForQuestion(questionId);

                    // Hiển thị loại câu hỏi để giáo viên dễ nhận biết và nhập đáp án cho đúng
                    var selectedQ = cbSelectquestion.SelectedItem as Question;
                    if (selectedQ != null)
                    {
                        string qType = selectedQ.Questiontype ?? "Multiple Choice";
                        // Bạn có thể gán vào một Label trên form (ví dụ lblQuestionType.Text = "Type: " + qType;)
                    }
                }
            }
        }

        // 3. Hàm load danh sách đáp án thuộc về câu hỏi được chọn lên DataGridView
        private void LoadOptionsForQuestion(int questionId)
        {
            try
            {
                var options = _context.Options
                    .Where(o => o.Questionid == questionId)
                    .Select(o => new
                    {
                        ID = o.Id,
                        OptionText = o.Optiontext,
                        IsCorrect = o.Iscorrect
                    })
                    .ToList();

                dgvOptions.DataSource = options;

                // Tùy chỉnh cột
                if (dgvOptions.Columns["ID"] != null) dgvOptions.Columns["ID"].Visible = false;
                if (dgvOptions.Columns["OptionText"] != null) dgvOptions.Columns["OptionText"].HeaderText = "Option Content";
                if (dgvOptions.Columns["IsCorrect"] != null) dgvOptions.Columns["IsCorrect"].HeaderText = "Is Correct?";

                dgvOptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading options: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Click vào dòng trong bảng để đưa dữ liệu lên ô nhập
        private void dgvOptions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOptions.Rows[e.RowIndex];
                txtOptiontext.Text = row.Cells["OptionText"].Value?.ToString() ?? "";
                chkIscorrect.Checked = row.Cells["IsCorrect"].Value != null && Convert.ToBoolean(row.Cells["IsCorrect"].Value);
            }
        }

        private bool IsValidInput()
        {
            if (cbSelectquestion.SelectedIndex == -1 || cbSelectquestion.SelectedValue == null)
            {
                MessageBox.Show("Please select a question first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSelectquestion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtOptiontext.Text))
            {
                MessageBox.Show("Please enter option content!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOptiontext.Focus();
                return false;
            }

            return true;
        }

        // 5. Nút Thêm đáp án
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsValidInput()) return;

            try
            {
                int qId = Convert.ToInt32(cbSelectquestion.SelectedValue);

                var newOpt = new Option
                {
                    Optiontext = txtOptiontext.Text.Trim(),
                    Iscorrect = chkIscorrect.Checked,
                    Questionid = qId
                };

                _context.Options.Add(newOpt);
                _context.SaveChanges();

                MessageBox.Show("Option added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOptionsForQuestion(qId);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding option: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 6. Nút Sửa đáp án
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvOptions.CurrentRow == null)
            {
                MessageBox.Show("Please select an option from the table to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidInput()) return;

            try
            {
                int optId = Convert.ToInt32(dgvOptions.CurrentRow.Cells["ID"].Value);
                int qId = Convert.ToInt32(cbSelectquestion.SelectedValue);

                var opt = _context.Options.FirstOrDefault(x => x.Id == optId);
                if (opt != null)
                {
                    opt.Optiontext = txtOptiontext.Text.Trim();
                    opt.Iscorrect = chkIscorrect.Checked;
                    _context.SaveChanges();
                }

                MessageBox.Show("Option updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOptionsForQuestion(qId);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating option: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 7. Nút Xóa đáp án
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOptions.CurrentRow == null)
            {
                MessageBox.Show("Please select an option to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this option?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    int optId = Convert.ToInt32(dgvOptions.CurrentRow.Cells["ID"].Value);
                    int qId = Convert.ToInt32(cbSelectquestion.SelectedValue);

                    var opt = _context.Options.FirstOrDefault(x => x.Id == optId);
                    if (opt != null)
                    {
                        _context.Options.Remove(opt);
                        _context.SaveChanges();
                    }

                    MessageBox.Show("Option deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOptionsForQuestion(qId);
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting option: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 8. Nút Clear làm mới
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtOptiontext.Clear();
            chkIscorrect.Checked = false;
            dgvOptions.ClearSelection();
            txtOptiontext.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}