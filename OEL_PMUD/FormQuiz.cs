using Microsoft.EntityFrameworkCore;
using OEL_PMUD.Models;
using OEL_PMUD.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OEL_PMUD
{
    public partial class FormQuiz : Form
    {
        private List<Question> _questions = new List<Question>();
        private int _currentIndex = 0;
        private Dictionary<int, string> _studentAnswers = new Dictionary<int, string>();
        private System.Windows.Forms.Timer _quizTimer;
        private const int TotalExamSeconds = 3600; // 60 phút làm bài
        private int _timeLeftSeconds = TotalExamSeconds;

        public FormQuiz()
        {
            InitializeComponent();

            // Hiển thị tên thí sinh đang đăng nhập nếu có Session
            if (Session.CurrentUser != null)
            {
                this.Text = $"Online Exam - Candidate: {Session.CurrentUser.Fullname}";
            }

            // Dời vị trí lblTime sang bên phải ComboBox để không bị đè nhau
            lblTime.Location = new System.Drawing.Point(220, 23);

            LoadTopics();
            SetupTimer();

            // Gắn sự kiện cho các control
            btnStartquiz.Click += btnStartquiz_Click;
            btnPrevious.Click += btnPrevious_Click;
            btnNext.Click += btnNext_Click;
            btnSubmitexam.Click += btnSubmitexam_Click;
            this.FormClosing += FormQuiz_FormClosing;

            // Khóa nút khi chưa bắt đầu thi
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;
            btnSubmitexam.Enabled = false;
        }

        private void LoadTopics()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    var topics = db.Topics.ToList();
                    cbSelecttopicc.DataSource = topics;
                    cbSelecttopicc.DisplayMember = "Name";
                    cbSelecttopicc.ValueMember = "Id";
                    cbSelecttopicc.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading topics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupTimer()
        {
            _quizTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _quizTimer.Tick += QuizTimer_Tick;
        }

        private void btnStartquiz_Click(object sender, EventArgs e)
        {
            if (cbSelecttopicc.SelectedValue == null)
            {
                MessageBox.Show("Please select a topic to start the quiz!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedTopicId = Convert.ToInt32(cbSelecttopicc.SelectedValue);

            try
            {
                using (var db = new Appdbcontext())
                {
                    _questions = db.Questions
                        .Where(q => q.Topicid == selectedTopicId)
                        .Include(q => q.Options)
                        .ToList();
                }

                if (_questions.Count == 0)
                {
                    MessageBox.Show("No questions available for this topic yet!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _currentIndex = 0;
                _studentAnswers.Clear();
                _timeLeftSeconds = TotalExamSeconds;
                UpdateTimerLabel();
                _quizTimer.Start();

                cbSelecttopicc.Enabled = false;
                btnStartquiz.Enabled = false;
                btnNext.Enabled = true;
                btnSubmitexam.Enabled = true;

                DisplayCurrentQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting quiz: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (_questions.Count == 0) return;

            var q = _questions[_currentIndex];
            lblQuestionnumber.Text = $"Question {_currentIndex + 1} of {_questions.Count}";
            lblQuestioncontent.Text = q.Content;

            pnlOptionscontainer.Controls.Clear();

            string qType = q.Questiontype?.Trim().ToLower() ?? "";

            if (qType.Contains("true") || qType.Contains("false") || qType.Contains("multiple"))
            {
                int yOffset = 15;
                foreach (var opt in q.Options)
                {
                    var rb = new RadioButton
                    {
                        Text = opt.Optiontext,
                        Location = new System.Drawing.Point(20, yOffset),
                        AutoSize = true,
                        Font = new System.Drawing.Font("Segoe UI", 10F)
                    };

                    if (_studentAnswers.ContainsKey(q.Id) && _studentAnswers[q.Id] == opt.Optiontext)
                    {
                        rb.Checked = true;
                    }

                    rb.CheckedChanged += (s, ev) =>
                    {
                        if (rb.Checked)
                        {
                            _studentAnswers[q.Id] = opt.Optiontext;
                        }
                    };

                    pnlOptionscontainer.Controls.Add(rb);
                    yOffset += 40;
                }
            }
            else
            {
                var lblHint = new Label
                {
                    Text = "Type your answer below:",
                    Location = new System.Drawing.Point(20, 15),
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 10F)
                };
                var txtAnswer = new TextBox
                {
                    Location = new System.Drawing.Point(20, 45),
                    Width = 450,
                    Font = new System.Drawing.Font("Segoe UI", 10F)
                };

                if (_studentAnswers.ContainsKey(q.Id))
                {
                    txtAnswer.Text = _studentAnswers[q.Id];
                }

                txtAnswer.TextChanged += (s, ev) =>
                {
                    _studentAnswers[q.Id] = txtAnswer.Text.Trim();
                };

                pnlOptionscontainer.Controls.Add(lblHint);
                pnlOptionscontainer.Controls.Add(txtAnswer);
            }

            btnPrevious.Enabled = (_currentIndex > 0);
            btnNext.Enabled = (_currentIndex < _questions.Count - 1);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                DisplayCurrentQuestion();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _questions.Count - 1)
            {
                _currentIndex++;
                DisplayCurrentQuestion();
            }
        }

        private void UpdateTimerLabel()
        {
            int minutes = _timeLeftSeconds / 60;
            int seconds = _timeLeftSeconds % 60;
            lblTime.Text = $"Time Left: {minutes:D2}:{seconds:D2}";
        }

        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            if (_timeLeftSeconds > 0)
            {
                _timeLeftSeconds--;
                UpdateTimerLabel();
            }
            else
            {
                _quizTimer.Stop();
                MessageBox.Show("Time's up! Your exam will be submitted automatically.", "Time Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SubmitExam();
            }
        }

        private void btnSubmitexam_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to submit your exam?", "Confirm Submit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _quizTimer.Stop();
                SubmitExam();
            }
        }

        private void SubmitExam()
        {
            int totalScore = 0;
            int maxScore = _questions.Count;
            int timeSpent = TotalExamSeconds - _timeLeftSeconds;
            string currentStudentId = Session.CurrentUser?.Id.ToString() ?? "GuestStudent";

            try
            {
                using (var db = new Appdbcontext())
                {
                    foreach (var q in _questions)
                    {
                        if (!_studentAnswers.ContainsKey(q.Id)) continue;

                        string studentAns = _studentAnswers[q.Id].Trim().ToLower();
                        string qType = q.Questiontype?.Trim().ToLower() ?? "";

                        var correctOptions = db.Options.Where(o => o.Questionid == q.Id && o.Iscorrect == true).ToList();

                        if (qType.Contains("fill") || qType.Contains("blank") || qType.Contains("short"))
                        {
                            bool isCorrect = correctOptions.Any(opt => studentAns.Contains(opt.Optiontext.Trim().ToLower()));
                            if (isCorrect) totalScore++;
                        }
                        else
                        {
                            bool isCorrect = correctOptions.Any(opt => opt.Optiontext.Trim().ToLower() == studentAns);
                            if (isCorrect) totalScore++;
                        }
                    }

                    int defaultExamId = db.Exams.FirstOrDefault()?.Id ?? 1;

                    var resultRecord = new StudentResult
                    {
                        Studentid = currentStudentId,
                        Examid = defaultExamId,
                        Score = totalScore,
                        Totalcorrect = totalScore,
                        Timespentseconds = timeSpent,
                        Submittedat = DateTime.Now
                    };

                    db.StudentResults.Add(resultRecord);
                    db.SaveChanges();
                }

                MessageBox.Show($"Exam Submitted Successfully!\nCandidate: {Session.CurrentUser?.Fullname ?? "Guest"}\nYour Score: {totalScore} / {maxScore}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting exam: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormQuiz_FormClosing(object sender, FormClosingEventArgs e)
        {
            _quizTimer?.Stop();
        }

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            FormResult frmResult = new FormResult();
            frmResult.ShowDialog();
        }
    }
}