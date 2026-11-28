using Microsoft.EntityFrameworkCore;
using OEL_PMUD.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OEL_PMUD
{
    public partial class FormAdminResult : Form
    {
        private List<UserDto> _students = new();
        private List<ExamDto> _exams = new();
        private List<ResultDto> _allResults = new();
        private List<ResultDto> _filteredResults = new();
        private List<StudentStatDto> _studentStats = new();
        private bool _isLoading = false;

        public FormAdminResult()
        {
            InitializeComponent();

            this.Load += FormAdminResult_Load;
            btnApplyfilter.Click += (s, e) => ApplyFilter();
            txtSearchstudent.TextChanged += (s, e) => FilterStudentGrid();
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
        }

        private void FormAdminResult_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _isLoading = true;
                using var context = new Appdbcontext();

                // 1. Lấy danh sách đề thi
                _exams = context.Exams
                    .AsNoTracking()
                    .Select(e => new ExamDto
                    {
                        Id = e.Id,
                        Title = e.Title
                    })
                    .OrderBy(e => e.Id)
                    .ToList();

                if (_exams.Count == 0)
                {
                    _exams.Add(new ExamDto { Id = 1, Title = "Đề kiểm tra cuối kỳ - OEL_PMUD" });
                }

                // 2. Lấy học sinh từ DB; nếu thiếu thì bù đủ 20 học sinh mẫu STU01 - STU20
                var dbStudents = context.Users
                    .AsNoTracking()
                    .Where(u => u.Role == "Student" && u.Id != "GuestStudent")
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        FullName = u.Fullname,
                        Email = u.Email
                    })
                    .OrderBy(u => u.Id)
                    .ToList();

                var sample20 = GetSample20Students();
                var studentMap = new Dictionary<string, UserDto>(StringComparer.OrdinalIgnoreCase);

                foreach (var s in sample20)
                    studentMap[s.Id] = s;

                foreach (var u in dbStudents)
                    studentMap[u.Id] = u; // Ưu tiên thông tin thật nếu đã có trong DB

                _students = studentMap.Values
                    .OrderBy(u => u.Id)
                    .Take(20)
                    .ToList();

                var examMap = _exams.ToDictionary(x => x.Id, x => x.Title);
                int defaultExamId = _exams[0].Id;

                // 3. Lấy kết quả thi từ DB (bỏ GuestStudent để giao diện sạch sẽ)
                var dbResults = context.StudentResults
                    .AsNoTracking()
                    .Where(r => r.Studentid != "GuestStudent")
                    .ToList()
                    .Select(r =>
                    {
                        studentMap.TryGetValue(r.Studentid, out var stu);
                        examMap.TryGetValue(r.Examid, out var examName);

                        return new ResultDto
                        {
                            ResultId = r.Id,
                            StudentId = r.Studentid,
                            FullName = stu != null ? stu.FullName : r.Studentid,
                            Email = stu != null ? stu.Email : string.Empty,
                            ExamId = r.Examid,
                            ExamTitle = !string.IsNullOrWhiteSpace(examName) ? examName : $"Đề #{r.Examid}",
                            Score = r.Score,
                            TotalCorrect = r.Totalcorrect,
                            TimeSpentSeconds = r.Timespentseconds,
                            SubmittedAt = r.Submittedat
                        };
                    })
                    .ToList();

                // Nếu chưa có kết quả nào cho nhóm STU01-STU20 thì dùng kết quả mẫu của 20 HS
                bool hasStuResults = dbResults.Any(r => r.StudentId.StartsWith("STU", StringComparison.OrdinalIgnoreCase));
                _allResults = hasStuResults
                    ? dbResults
                    : GetSampleResultsFor20Students(_students, defaultExamId, examMap[defaultExamId]);

                _allResults = _allResults
                    .OrderByDescending(x => x.SubmittedAt)
                    .ToList();

                // Nạp ComboBox lọc đề thi
                var filterItems = _exams
                    .Select(e => new ExamFilterItem { ExamId = e.Id, Display = $"#{e.Id} - {e.Title}" })
                    .ToList();
                filterItems.Insert(0, new ExamFilterItem { ExamId = 0, Display = "-- Tất cả đề thi --" });

                cboExamfilter.DisplayMember = nameof(ExamFilterItem.Display);
                cboExamfilter.ValueMember = nameof(ExamFilterItem.ExamId);
                cboExamfilter.DataSource = filterItems;

                _isLoading = false;
                ApplyFilter();
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<UserDto> GetSample20Students()
        {
            return new List<UserDto>
            {
                new() { Id = "STU01", FullName = "Trần Minh Khoa", Email = "stu01@oel.edu.vn" },
                new() { Id = "STU02", FullName = "Nguyễn Hoàng Nam", Email = "stu02@oel.edu.vn" },
                new() { Id = "STU03", FullName = "Lê Thị Thu Hà", Email = "stu03@oel.edu.vn" },
                new() { Id = "STU04", FullName = "Phạm Gia Huy", Email = "stu04@oel.edu.vn" },
                new() { Id = "STU05", FullName = "Đỗ Ngọc Anh", Email = "stu05@oel.edu.vn" },
                new() { Id = "STU06", FullName = "Võ Minh Tuấn", Email = "stu06@oel.edu.vn" },
                new() { Id = "STU07", FullName = "Bùi Khánh Linh", Email = "stu07@oel.edu.vn" },
                new() { Id = "STU08", FullName = "Phan Quốc Bảo", Email = "stu08@oel.edu.vn" },
                new() { Id = "STU09", FullName = "Đặng Phương Thảo", Email = "stu09@oel.edu.vn" },
                new() { Id = "STU10", FullName = "Hoàng Đức Anh", Email = "stu10@oel.edu.vn" },
                new() { Id = "STU11", FullName = "Ngô Thanh Vy", Email = "stu11@oel.edu.vn" },
                new() { Id = "STU12", FullName = "Dương Hữu Nghĩa", Email = "stu12@oel.edu.vn" },
                new() { Id = "STU13", FullName = "Lý Bảo Trâm", Email = "stu13@oel.edu.vn" },
                new() { Id = "STU14", FullName = "Trịnh Quang Vinh", Email = "stu14@oel.edu.vn" },
                new() { Id = "STU15", FullName = "Mai Hồng Nhung", Email = "stu15@oel.edu.vn" },
                new() { Id = "STU16", FullName = "Vũ Tấn Phát", Email = "stu16@oel.edu.vn" },
                new() { Id = "STU17", FullName = "Đinh Quỳnh Chi", Email = "stu17@oel.edu.vn" },
                new() { Id = "STU18", FullName = "Hồ Trung Kiên", Email = "stu18@oel.edu.vn" },
                new() { Id = "STU19", FullName = "Tạ Phương Nhi", Email = "stu19@oel.edu.vn" },
                new() { Id = "STU20", FullName = "Tô Hữu Phước", Email = "stu20@oel.edu.vn" }
            };
        }

        private static List<ResultDto> GetSampleResultsFor20Students(
            List<UserDto> students, int examId, string examTitle)
        {
            // Điểm mẫu đa dạng cho các học sinh để thấy rõ Giỏi / Khá / TB / Yếu
            double[] sampleScores =
            {
                9.0, 8.5, 8.0, 7.5, 8.5,
                6.5, 7.0, 9.5, 5.5, 8.0,
                7.5, 4.5, 8.5, 7.0, 6.0,
                8.0, 7.5, 6.5, 8.0, 5.0
            };

            var results = new List<ResultDto>();
            DateTime baseDate = new DateTime(2026, 11, 20, 8, 30, 0);

            for (int i = 0; i < students.Count; i++)
            {
                var stu = students[i];
                double score = sampleScores[i % sampleScores.Length];
                int correct = (int)Math.Round(score * 2); // Thang điểm 10 tương ứng 20 câu
                int sec = 480 + (i * 22);

                results.Add(new ResultDto
                {
                    ResultId = i + 1,
                    StudentId = stu.Id,
                    FullName = stu.FullName,
                    Email = stu.Email,
                    ExamId = examId,
                    ExamTitle = examTitle,
                    Score = score,
                    TotalCorrect = correct,
                    TimeSpentSeconds = sec,
                    SubmittedAt = baseDate.AddHours(i * 4)
                });
            }

            // Thêm lần thi thứ 2 cho STU01 và STU02 để thấy rõ học sinh làm nhiều lượt
            if (students.Count >= 2)
            {
                results.Add(new ResultDto
                {
                    ResultId = 101,
                    StudentId = students[0].Id,
                    FullName = students[0].FullName,
                    Email = students[0].Email,
                    ExamId = examId,
                    ExamTitle = examTitle,
                    Score = 9.5,
                    TotalCorrect = 19,
                    TimeSpentSeconds = 450,
                    SubmittedAt = baseDate.AddDays(3)
                });

                results.Add(new ResultDto
                {
                    ResultId = 102,
                    StudentId = students[1].Id,
                    FullName = students[1].FullName,
                    Email = students[1].Email,
                    ExamId = examId,
                    ExamTitle = examTitle,
                    Score = 9.0,
                    TotalCorrect = 18,
                    TimeSpentSeconds = 510,
                    SubmittedAt = baseDate.AddDays(3).AddHours(2)
                });
            }

            return results;
        }

        private void ApplyFilter()
        {
            if (_isLoading) return;

            int selectedExamId = cboExamfilter.SelectedValue is int id ? id : 0;
            _filteredResults = selectedExamId > 0
                ? _allResults.Where(r => r.ExamId == selectedExamId).ToList()
                : _allResults.ToList();

            UpdateTopKpis();
            UpdateTab1Overview();
            BuildStudentStats();
            FilterStudentGrid();
        }

        private void UpdateTopKpis()
        {
            int totalStu = _students.Count;
            int testedStu = _filteredResults.Select(r => r.StudentId).Distinct().Count();
            int totalAttempts = _filteredResults.Count;
            double avgScore = totalAttempts > 0 ? _filteredResults.Average(r => r.Score) : 0;
            int passCount = _filteredResults.Count(r => r.Score >= 5.0);
            double passRate = totalAttempts > 0 ? (passCount * 100.0 / totalAttempts) : 0;

            lblKpistudents.Text = $"{testedStu} / {totalStu} HS";
            lblKpiattempts.Text = $"{totalAttempts} turn";
            lblKpiavgscore.Text = $"{avgScore:0.00}";
            lblKpipassrate.Text = $"{passRate:0.0}%";
        }

        private void UpdateTab1Overview()
        {
            var leaderboard = _filteredResults
                .GroupBy(r => new { r.StudentId, r.FullName })
                .Select(g => new
                {
                    g.Key.StudentId,
                    g.Key.FullName,
                    AttemptCount = g.Count(),
                    AvgScore = Math.Round(g.Average(x => x.Score), 2),
                    BestScore = g.Max(x => x.Score)
                })
                .OrderByDescending(x => x.AvgScore)
                .ThenByDescending(x => x.BestScore)
                .Select((x, idx) => new LeaderboardItem
                {
                    Rank = idx + 1,
                    StudentId = x.StudentId,
                    FullName = x.FullName,
                    Attempts = x.AttemptCount,
                    AvgScore = x.AvgScore,
                    BestScore = x.BestScore
                })
                .ToList();

            dgvLeaderboard.DataSource = null;
            dgvLeaderboard.DataSource = leaderboard;
            StyleGrid(dgvLeaderboard);
            if (dgvLeaderboard.Columns.Count > 0)
            {
                dgvLeaderboard.Columns[nameof(LeaderboardItem.Rank)].HeaderText = "Hạng";
                dgvLeaderboard.Columns[nameof(LeaderboardItem.StudentId)].HeaderText = "Mã HS";
                dgvLeaderboard.Columns[nameof(LeaderboardItem.FullName)].HeaderText = "Họ và tên";
                dgvLeaderboard.Columns[nameof(LeaderboardItem.Attempts)].HeaderText = "Số lượt";
                dgvLeaderboard.Columns[nameof(LeaderboardItem.AvgScore)].HeaderText = "Điểm TB";
                dgvLeaderboard.Columns[nameof(LeaderboardItem.BestScore)].HeaderText = "Cao nhất";
            }

            dgvRecentresults.DataSource = null;
            dgvRecentresults.DataSource = _filteredResults.Take(30).ToList();
            StyleGrid(dgvRecentresults);
            FormatResultGrid(dgvRecentresults, showStudent: true);
        }

        private void BuildStudentStats()
        {
            var grouped = _filteredResults
                .GroupBy(r => r.StudentId)
                .ToDictionary(g => g.Key, g => g.ToList());

            _studentStats = _students.Select(s =>
            {
                grouped.TryGetValue(s.Id, out var list);
                list ??= new List<ResultDto>();

                int count = list.Count;
                return new StudentStatDto
                {
                    StudentId = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    Attempts = count,
                    AvgScore = count > 0 ? Math.Round(list.Average(x => x.Score), 2) : 0,
                    BestScore = count > 0 ? list.Max(x => x.Score) : 0,
                    WorstScore = count > 0 ? list.Min(x => x.Score) : 0
                };
            }).ToList();
        }

        private void FilterStudentGrid()
        {
            string kw = txtSearchstudent.Text.Trim().ToLower();
            var list = string.IsNullOrWhiteSpace(kw)
                ? _studentStats
                : _studentStats.Where(s =>
                    s.StudentId.ToLower().Contains(kw) ||
                    s.FullName.ToLower().Contains(kw) ||
                    s.Email.ToLower().Contains(kw)).ToList();

            dgvStudents.DataSource = null;
            dgvStudents.DataSource = list;
            StyleGrid(dgvStudents);

            if (dgvStudents.Columns.Count > 0)
            {
                dgvStudents.Columns[nameof(StudentStatDto.StudentId)].HeaderText = "Mã HS";
                dgvStudents.Columns[nameof(StudentStatDto.FullName)].HeaderText = "Họ và tên";
                dgvStudents.Columns[nameof(StudentStatDto.Attempts)].HeaderText = "Lượt thi";
                dgvStudents.Columns[nameof(StudentStatDto.AvgScore)].HeaderText = "Điểm TB";
                dgvStudents.Columns[nameof(StudentStatDto.BestScore)].HeaderText = "Cao nhất";
                dgvStudents.Columns[nameof(StudentStatDto.Email)].Visible = false;
                dgvStudents.Columns[nameof(StudentStatDto.WorstScore)].Visible = false;
            }

            if (list.Count > 0)
                ShowStudentDetail(list[0]);
            else
                ClearStudentDetail();
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow?.DataBoundItem is StudentStatDto stu)
            {
                ShowStudentDetail(stu);
            }
        }

        private void ShowStudentDetail(StudentStatDto stu)
        {
            lblStudentname.Text = $"{stu.FullName} ({stu.StudentId})";
            lblStudentattempts.Text = $"Attempts: {stu.Attempts}";
            lblStudentavg.Text = $"Avg: {stu.AvgScore:0.00}";
            lblStudentbest.Text = $"Best: {stu.BestScore:0.00}  (Lowest: {stu.WorstScore:0.00})";

            var history = _filteredResults
                .Where(r => r.StudentId == stu.StudentId)
                .OrderByDescending(r => r.SubmittedAt)
                .ToList();

            dgvStudenthistory.DataSource = null;
            dgvStudenthistory.DataSource = history;
            StyleGrid(dgvStudenthistory);
            FormatResultGrid(dgvStudenthistory, showStudent: false);
        }

        private void ClearStudentDetail()
        {
            lblStudentname.Text = "--";
            lblStudentattempts.Text = "Attempts: 0";
            lblStudentavg.Text = "Avg: 0.00";
            lblStudentbest.Text = "Best: 0.00";
            dgvStudenthistory.DataSource = null;
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
        }

        private static void FormatResultGrid(DataGridView grid, bool showStudent)
        {
            if (grid.Columns.Count == 0) return;

            grid.Columns[nameof(ResultDto.ResultId)].HeaderText = "Mã KQ";
            grid.Columns[nameof(ResultDto.StudentId)].Visible = showStudent;
            grid.Columns[nameof(ResultDto.StudentId)].HeaderText = "Mã HS";
            grid.Columns[nameof(ResultDto.FullName)].Visible = showStudent;
            grid.Columns[nameof(ResultDto.FullName)].HeaderText = "Họ và tên";
            grid.Columns[nameof(ResultDto.Email)].Visible = false;
            grid.Columns[nameof(ResultDto.ExamId)].Visible = false;
            grid.Columns[nameof(ResultDto.ExamTitle)].HeaderText = "Đề thi";
            grid.Columns[nameof(ResultDto.Score)].HeaderText = "Điểm";
            grid.Columns[nameof(ResultDto.TotalCorrect)].HeaderText = "Số câu đúng";
            grid.Columns[nameof(ResultDto.TimeSpentSeconds)].HeaderText = "Thời gian (s)";
            grid.Columns[nameof(ResultDto.SubmittedAt)].HeaderText = "Ngày nộp";
            grid.Columns[nameof(ResultDto.SubmittedAt)].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private class UserDto
        {
            public string Id { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        private class ExamDto
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
        }

        private class ExamFilterItem
        {
            public int ExamId { get; set; }
            public string Display { get; set; } = string.Empty;
        }

        private class ResultDto
        {
            public int ResultId { get; set; }
            public string StudentId { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public int ExamId { get; set; }
            public string ExamTitle { get; set; } = string.Empty;
            public double Score { get; set; }
            public int TotalCorrect { get; set; }
            public int TimeSpentSeconds { get; set; }
            public DateTime SubmittedAt { get; set; }
        }

        private class LeaderboardItem
        {
            public int Rank { get; set; }
            public string StudentId { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public int Attempts { get; set; }
            public double AvgScore { get; set; }
            public double BestScore { get; set; }
        }

        private class StudentStatDto
        {
            public string StudentId { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public int Attempts { get; set; }
            public double AvgScore { get; set; }
            public double BestScore { get; set; }
            public double WorstScore { get; set; }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}