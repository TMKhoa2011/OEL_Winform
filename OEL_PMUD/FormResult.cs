using OEL_PMUD.Models.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OEL_PMUD
{
    public partial class FormResult : Form
    {
        public FormResult()
        {
            InitializeComponent();
            btnClose.Click += btnClose_Click;
            LoadStudentResults();
        }

        private void LoadStudentResults()
        {
            if (Session.CurrentUser == null)
            {
                MessageBox.Show("No active user session found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStudentId = Session.CurrentUser.Id.ToString();
            this.Text = $"Exam Results - {Session.CurrentUser.Fullname}";

            try
            {
                using (var db = new Appdbcontext())
                {
                    var results = db.StudentResults
                        .Where(r => r.Studentid == currentStudentId)
                        .OrderByDescending(r => r.Submittedat)
                        .ToList()
                        .Select((r, index) => new
                        {
                            No = index + 1,
                            ExamId = r.Examid,
                            Score = r.Score,
                            CorrectAnswers = r.Totalcorrect,
                            TimeSpent = FormatDuration(r.Timespentseconds),
                            SubmittedAt = r.Submittedat.ToString("dd/MM/yyyy HH:mm") ?? ""
                        })
                        .ToList();

                    dgvResults.DataSource = results;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading results: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatDuration(int? seconds)
        {
            int totalSec = seconds ?? 0;
            int mins = totalSec / 60;
            int secs = totalSec % 60;
            return $"{mins:D2}:{secs:D2}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}