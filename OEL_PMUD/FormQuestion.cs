using OEL_PMUD.Models;
using OEL_PMUD.Models.Data;
using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace OEL_PMUD
{
    public partial class FormQuestion : Form
    {
        public FormQuestion()
        {
            InitializeComponent();

            // Tự động thêm 80 câu hỏi mẫu (10 câu/chủ đề) vào SQL Server nếu bảng đang trống
            SeedQuestionsIfEmpty();

            // Load initial data
            LoadTopicComboBox();
            LoadQuestionData();
        }

        #region 1. LOAD DATA TO COMBOBOX AND DATAGRIDVIEW

        private void LoadTopicComboBox()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    var topics = db.Topics.ToList();
                    cbTopic.DataSource = topics;
                    cbTopic.DisplayMember = "Name";
                    cbTopic.ValueMember = "Id";
                    cbTopic.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading topics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadQuestionData()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    var questions = db.Questions
                        .Include(q => q.Topic)
                        .Select(q => new
                        {
                            ID = q.Id,
                            Content = q.Content,
                            TopicName = q.Topic != null ? q.Topic.Name : "N/A",
                            TopicId = q.Topicid,
                            QuestionType = q.Questiontype,
                            Difficulty = q.Difficulty,
                            CreatedAt = q.Createdat
                        })
                        .ToList();

                    dataGridView1.DataSource = questions;

                    // Customize columns header and visibility
                    if (dataGridView1.Columns["ID"] != null) dataGridView1.Columns["ID"].Visible = false;
                    if (dataGridView1.Columns["TopicId"] != null) dataGridView1.Columns["TopicId"].Visible = false;

                    if (dataGridView1.Columns["Content"] != null) dataGridView1.Columns["Content"].HeaderText = "Question Content";
                    if (dataGridView1.Columns["TopicName"] != null) dataGridView1.Columns["TopicName"].HeaderText = "Topic Name";
                    if (dataGridView1.Columns["QuestionType"] != null) dataGridView1.Columns["QuestionType"].HeaderText = "Question Type";
                    if (dataGridView1.Columns["Difficulty"] != null) dataGridView1.Columns["Difficulty"].HeaderText = "Difficulty";
                    if (dataGridView1.Columns["CreatedAt"] != null) dataGridView1.Columns["CreatedAt"].HeaderText = "Created At";

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 2. SEED 80 QUESTIONS INTO SQL SERVER AUTOMATICALLY

        private void SeedQuestionsIfEmpty()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    // Kiểm tra nếu bảng Questions chưa có dữ liệu và đã có sẵn Topics trong DB
                    if (!db.Questions.Any() && db.Topics.Any())
                    {
                        var topics = db.Topics.OrderBy(t => t.Id).ToList();
                        var random = new Random();
                        var sampleQuestions = new List<Question>();

                        string[] types = { "Multiple Choice", "True/False", "Fill in the blank" };

                        string[][] topicQuestionPools = new string[][]
                        {
                            // 1. Travel and Tourism
                            new string[] {
                                "What document is mandatory for international air travel?",
                                "Which term describes traveling to natural areas with conservation efforts?",
                                "What does 'all-inclusive resort' usually mean?",
                                "Which booking platform is commonly used for hotel reservations?",
                                "What is the primary function of a travel itinerary?",
                                "What does 'jet lag' refer to after a long flight?",
                                "Which term describes a short trip for pleasure or education?",
                                "What is a boarding pass used for at the airport?",
                                "What does 'customs declaration' involve when entering a country?",
                                "Which luggage type is permitted as cabin baggage on most flights?"
                            },
                            // 2. Education and School Life
                            new string[] {
                                "What is the main objective of a final-year academic project?",
                                "Which facility on campus is primarily used for research and reading?",
                                "What does GPA stand for in the academic grading system?",
                                "How do students usually submit their assignments digitally?",
                                "What is the role of an academic advisor in university?",
                                "Which exam assesses English proficiency for international communication?",
                                "What is a syllabus designed to outline for a course?",
                                "Why is collaborative teamwork important in university projects?",
                                "What does plagiarism mean in academic writing?",
                                "What is the purpose of a university lecture?"
                            },
                            // 3. Workspace and Business
                            new string[] {
                                "What is the primary purpose of a corporate meeting agenda?",
                                "Which department manages employee payroll and benefits?",
                                "What does the acronym ROI stand for in business investments?",
                                "How should a formal resignation letter be submitted?",
                                "What is the main goal of a marketing strategy?",
                                "What does B2B stand for in commercial transactions?",
                                "Why are Key Performance Indicators (KPIs) used in companies?",
                                "What is a supply chain in corporate operations?",
                                "How do you write a professional follow-up email after an interview?",
                                "What is the difference between revenue and net profit?"
                            },
                            // 4. Daily Life and Hobbies
                            new string[] {
                                "What are the health benefits of maintaining a daily morning routine?",
                                "Which hobby is known for enhancing creativity and manual dexterity?",
                                "How many hours of sleep are recommended for adults nightly?",
                                "What is the advantage of practicing mindfulness or meditation?",
                                "Which physical activity is best for cardiovascular endurance?",
                                "How does time management reduce daily stress?",
                                "What is a popular leisure activity for relaxing on weekends?",
                                "Why is balanced nutrition essential for daily energy?",
                                "How can reading books daily improve cognitive function?",
                                "What is the benefit of keeping a personal journal?"
                            },
                            // 5. Technology and Media
                            new string[] {
                                "What is the main benefit of cloud computing for enterprises?",
                                "Which programming language is widely used for Android app development?",
                                "What does DBMS stand for in software database management?",
                                "How does version control (like Git) help developers?",
                                "What is the primary function of an operating system?",
                                "Which cyber threat involves tricking users into revealing passwords?",
                                "What does API stand for in software engineering?",
                                "Why is responsive design important for web applications?",
                                "What is the difference between front-end and back-end development?",
                                "What is the role of a database foreign key constraint?"
                            },
                            // 6. Health and Fitness
                            new string[] {
                                "Why is regular hydration crucial during high-intensity workouts?",
                                "What is the primary goal of strength training exercises?",
                                "Which food group is the primary source of sustained energy?",
                                "How does aerobic exercise impact heart health?",
                                "What is the purpose of stretching before a workout session?",
                                "Why is adequate rest important for muscle recovery?",
                                "What does BMI stand for in health assessments?",
                                "Which nutrient is essential for repairing body tissues?",
                                "How does chronic stress negatively affect physical health?",
                                "What are the benefits of eating green vegetables daily?"
                            },
                            // 7. Environment and Nature
                            new string[] {
                                "Why is plastic recycling vital for marine ecosystems?",
                                "Which renewable energy source harnesses power from wind?",
                                "What causes global climate change according to scientists?",
                                "What is the significance of preserving endangered wildlife species?",
                                "How do trees contribute to reducing atmospheric carbon dioxide?",
                                "What does sustainable development mean for future generations?",
                                "Which practice helps conserve water resources at home?",
                                "What is the impact of deforestation on natural habitats?",
                                "Why are solar panels considered eco-friendly energy solutions?",
                                "What is the purpose of establishing national parks?"
                            },
                            // 8. Grammar and Vocabulary Fundamentals
                            new string[] {
                                "What is the grammatical function of an adjective in a sentence?",
                                "Choose the correct past tense of the irregular verb 'run'.",
                                "What defines a complex sentence in English grammar?",
                                "How do you correctly use punctuation in direct quotations?",
                                "What is a synonym for the word 'abundant'?",
                                "What is the difference between active and passive voice?",
                                "Which part of speech expresses an action or state of being?",
                                "What is an antonym for the word 'transparent'?",
                                "How do compound words form in English vocabulary?",
                                "What is the purpose of using transitional words in essays?"
                            }
                        };

                        for (int tIndex = 0; tIndex < topics.Count; tIndex++)
                        {
                            int currentTopicId = topics[tIndex].Id;
                            string[] questionsForThisTopic = topicQuestionPools[tIndex];

                            foreach (string qContent in questionsForThisTopic)
                            {
                                sampleQuestions.Add(new Question
                                {
                                    Content = qContent,
                                    Topicid = currentTopicId,
                                    Questiontype = types[random.Next(types.Length)],
                                    Difficulty = random.Next(1, 4),
                                    Createdat = DateTime.Now
                                });
                            }
                        }

                        db.Questions.AddRange(sampleQuestions);
                        db.SaveChanges(); // Lưu thẳng xuống SQL Server
                    }
                }
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi ngầm nếu có
            }
        }

        #endregion

        #region 3. CRUD OPERATIONS & VALIDATION

        private bool IsValidInput()
        {
            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.Show("Please enter the question content!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContent.Focus();
                return false;
            }

            if (cbTopic.SelectedValue == null)
            {
                MessageBox.Show("Please select a topic for the question!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbTopic.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsValidInput()) return;

            try
            {
                using (var db = new Appdbcontext())
                {
                    var newQ = new Question
                    {
                        Content = txtContent.Text.Trim(),
                        Topicid = Convert.ToInt32(cbTopic.SelectedValue),
                        Questiontype = txtQuestiontype.Text.Trim(),
                        Difficulty = int.TryParse(txtDifficulty.Text, out int diff) ? diff : (int?)null,
                        Createdat = DateTime.Now
                    };

                    db.Questions.Add(newQ);
                    db.SaveChanges();
                }

                MessageBox.Show("Question added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadQuestionData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding question: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a question from the table to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidInput()) return;

            try
            {
                int qId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);

                using (var db = new Appdbcontext())
                {
                    var q = db.Questions.FirstOrDefault(x => x.Id == qId);
                    if (q != null)
                    {
                        q.Content = txtContent.Text.Trim();
                        q.Topicid = Convert.ToInt32(cbTopic.SelectedValue);
                        q.Questiontype = txtQuestiontype.Text.Trim();
                        q.Difficulty = int.TryParse(txtDifficulty.Text, out int diff) ? diff : (int?)null;

                        db.SaveChanges();
                    }
                }

                MessageBox.Show("Question updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadQuestionData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating question: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a question to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this question?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    int qId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);

                    using (var db = new Appdbcontext())
                    {
                        var q = db.Questions.FirstOrDefault(x => x.Id == qId);
                        if (q != null)
                        {
                            db.Questions.Remove(q);
                            db.SaveChanges();
                        }
                    }

                    MessageBox.Show("Question deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQuestionData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting question: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtContent.Clear();
            txtQuestiontype.Clear();
            txtDifficulty.Clear();
            cbTopic.SelectedIndex = -1;
            dataGridView1.ClearSelection();
            txtContent.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtContent.Text = row.Cells["Content"].Value?.ToString() ?? "";
                txtQuestiontype.Text = row.Cells["QuestionType"].Value?.ToString() ?? "";
                txtDifficulty.Text = row.Cells["Difficulty"].Value?.ToString() ?? "";

                if (row.Cells["TopicId"].Value != null)
                {
                    cbTopic.SelectedValue = Convert.ToInt32(row.Cells["TopicId"].Value);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            try
            {
                using (var db = new Appdbcontext())
                {
                    var filtered = db.Questions
                        .Include(q => q.Topic)
                        .Where(q => q.Content.ToLower().Contains(keyword) ||
                                    (q.Questiontype != null && q.Questiontype.ToLower().Contains(keyword)) ||
                                    (q.Topic != null && q.Topic.Name.ToLower().Contains(keyword)))
                        .Select(q => new
                        {
                            ID = q.Id,
                            Content = q.Content,
                            TopicName = q.Topic != null ? q.Topic.Name : "N/A",
                            TopicId = q.Topicid,
                            QuestionType = q.Questiontype,
                            Difficulty = q.Difficulty,
                            CreatedAt = q.Createdat
                        })
                        .ToList();

                    dataGridView1.DataSource = filtered;
                }
            }
            catch (Exception ex)
            {
                // Handle search error if needed
            }
        }

        #endregion
    }
}