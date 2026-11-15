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

            // 1. Tự động thêm 80 câu hỏi mẫu (10 câu/chủ đề) với phân loại chuẩn xác vào SQL Server nếu bảng đang trống
            SeedQuestionsIfEmpty();

            // 2. Tự động sinh đáp án thực tế tương ứng dựa trên nội dung câu hỏi nếu bảng Options đang trống
            SeedOptionsIfEmpty();

            // 3. Load initial data
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

        #region 2. SEED 80 QUESTIONS & REAL OPTIONS AUTOMATICALLY

        private void SeedQuestionsIfEmpty()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    if (!db.Questions.Any() && db.Topics.Any())
                    {
                        var topics = db.Topics.OrderBy(t => t.Id).ToList();
                        var random = new Random();
                        var sampleQuestions = new List<Question>();

                        var topicQuestionPools = new[]
                        {
                            // 1. Travel and Tourism
                            new[] {
                                Tuple.Create("What document is mandatory for international air travel?", "Fill in the blank"),
                                Tuple.Create("Which term describes traveling to natural areas with conservation efforts?", "Multiple Choice"),
                                Tuple.Create("What does 'all-inclusive resort' usually mean?", "Multiple Choice"),
                                Tuple.Create("Which booking platform is commonly used for hotel reservations?", "Multiple Choice"),
                                Tuple.Create("International flights do not require a passport.", "True/False"),
                                Tuple.Create("What does 'jet lag' refer to after a long flight?", "Fill in the blank"),
                                Tuple.Create("Which term describes a short trip for pleasure or education?", "Multiple Choice"),
                                Tuple.Create("A boarding pass is required to pass airport security and board the plane.", "True/False"),
                                Tuple.Create("What does 'customs declaration' involve when entering a country?", "Fill in the blank"),
                                Tuple.Create("Which luggage type is permitted as cabin baggage on most flights?", "Multiple Choice")
                            },
                            // 2. Education and School Life
                            new[] {
                                Tuple.Create("What is the main objective of a final-year academic project?", "Fill in the blank"),
                                Tuple.Create("Which facility on campus is primarily used for research and reading?", "Multiple Choice"),
                                Tuple.Create("What does GPA stand for in the academic grading system?", "Fill in the blank"),
                                Tuple.Create("How do students usually submit their assignments digitally?", "Fill in the blank"),
                                Tuple.Create("An academic advisor helps students plan their course schedules.", "True/False"),
                                Tuple.Create("Which exam assesses English proficiency for international communication?", "Multiple Choice"),
                                Tuple.Create("What is a syllabus designed to outline for a course?", "Fill in the blank"),
                                Tuple.Create("Collaborative teamwork is discouraged in university projects.", "True/False"),
                                Tuple.Create("What does plagiarism mean in academic writing?", "Fill in the blank"),
                                Tuple.Create("What is the purpose of a university lecture?", "Fill in the blank")
                            },
                            // 3. Workspace and Business
                            new[] {
                                Tuple.Create("What is the primary purpose of a corporate meeting agenda?", "Fill in the blank"),
                                Tuple.Create("Which department manages employee payroll and benefits?", "Multiple Choice"),
                                Tuple.Create("What does the acronym ROI stand for in business investments?", "Fill in the blank"),
                                Tuple.Create("A formal resignation letter should never be submitted in writing.", "True/False"),
                                Tuple.Create("What is the main goal of a marketing strategy?", "Fill in the blank"),
                                Tuple.Create("What does B2B stand for in commercial transactions?", "Fill in the blank"),
                                Tuple.Create("Key Performance Indicators (KPIs) are used to measure company performance.", "True/False"),
                                Tuple.Create("What is a supply chain in corporate operations?", "Fill in the blank"),
                                Tuple.Create("How do you write a professional follow-up email after an interview?", "Fill in the blank"),
                                Tuple.Create("What is the difference between revenue and net profit?", "Multiple Choice")
                            },
                            // 4. Daily Life and Hobbies
                            new[] {
                                Tuple.Create("What are the health benefits of maintaining a daily morning routine?", "Fill in the blank"),
                                Tuple.Create("Which hobby is known for enhancing creativity and manual dexterity?", "Multiple Choice"),
                                Tuple.Create("Adults are generally recommended to get 7 to 9 hours of sleep nightly.", "True/False"),
                                Tuple.Create("What is the advantage of practicing mindfulness or meditation?", "Fill in the blank"),
                                Tuple.Create("Which physical activity is best for cardiovascular endurance?", "Multiple Choice"),
                                Tuple.Create("Time management helps increase daily stress levels.", "True/False"),
                                Tuple.Create("What is a popular leisure activity for relaxing on weekends?", "Multiple Choice"),
                                Tuple.Create("Balanced nutrition is essential for maintaining daily energy.", "True/False"),
                                Tuple.Create("How can reading books daily improve cognitive function?", "Fill in the blank"),
                                Tuple.Create("What is the benefit of keeping a personal journal?", "Fill in the blank")
                            },
                            // 5. Technology and Media
                            new[] {
                                Tuple.Create("What is the main benefit of cloud computing for enterprises?", "Fill in the blank"),
                                Tuple.Create("Which programming language is widely used for Android app development?", "Multiple Choice"),
                                Tuple.Create("What does DBMS stand for in software database management?", "Fill in the blank"),
                                Tuple.Create("Version control systems like Git help developers track code changes.", "True/False"),
                                Tuple.Create("What is the primary function of an operating system?", "Fill in the blank"),
                                Tuple.Create("Phishing is a cyber threat that involves tricking users into revealing passwords.", "True/False"),
                                Tuple.Create("What does API stand for in software engineering?", "Fill in the blank"),
                                Tuple.Create("Responsive design is unimportant for modern web applications.", "True/False"),
                                Tuple.Create("What is the difference between front-end and back-end development?", "Multiple Choice"),
                                Tuple.Create("What is the role of a database foreign key constraint?", "Fill in the blank")
                            },
                            // 6. Health and Fitness
                            new[] {
                                Tuple.Create("Regular hydration is crucial during high-intensity workouts.", "True/False"),
                                Tuple.Create("What is the primary goal of strength training exercises?", "Fill in the blank"),
                                Tuple.Create("Which food group is the primary source of sustained energy?", "Multiple Choice"),
                                Tuple.Create("Aerobic exercise has a negative impact on heart health.", "True/False"),
                                Tuple.Create("What is the purpose of stretching before a workout session?", "Fill in the blank"),
                                Tuple.Create("Adequate rest is important for muscle recovery after exercise.", "True/False"),
                                Tuple.Create("What does BMI stand for in health assessments?", "Fill in the blank"),
                                Tuple.Create("Which nutrient is essential for repairing body tissues?", "Multiple Choice"),
                                Tuple.Create("Chronic stress can negatively affect physical health.", "True/False"),
                                Tuple.Create("What are the benefits of eating green vegetables daily?", "Fill in the blank")
                            },
                            // 7. Environment and Nature
                            new[] {
                                Tuple.Create("Plastic recycling is vital for protecting marine ecosystems.", "True/False"),
                                Tuple.Create("Which renewable energy source harnesses power from wind?", "Multiple Choice"),
                                Tuple.Create("Human activities are a major driver of global climate change.", "True/False"),
                                Tuple.Create("What is the significance of preserving endangered wildlife species?", "Fill in the blank"),
                                Tuple.Create("Trees contribute to reducing atmospheric carbon dioxide.", "True/False"),
                                Tuple.Create("What does sustainable development mean for future generations?", "Fill in the blank"),
                                Tuple.Create("Which practice helps conserve water resources at home?", "Multiple Choice"),
                                Tuple.Create("Deforestation has a positive impact on natural animal habitats.", "True/False"),
                                Tuple.Create("Solar panels are considered eco-friendly energy solutions.", "True/False"),
                                Tuple.Create("What is the purpose of establishing national parks?", "Fill in the blank")
                            },
                            // 8. Grammar and Vocabulary Fundamentals
                            new[] {
                                Tuple.Create("What is the grammatical function of an adjective in a sentence?", "Fill in the blank"),
                                Tuple.Create("Choose the correct past tense of the irregular verb 'run'.", "Multiple Choice"),
                                Tuple.Create("A complex sentence consists of an independent clause and at least one dependent clause.", "True/False"),
                                Tuple.Create("How do you correctly use punctuation in direct quotations?", "Fill in the blank"),
                                Tuple.Create("What is a synonym for the word 'abundant'?", "Multiple Choice"),
                                Tuple.Create("Active voice emphasizes the receiver of the action rather than the doer.", "True/False"),
                                Tuple.Create("Which part of speech expresses an action or state of being?", "Multiple Choice"),
                                Tuple.Create("What is an antonym for the word 'transparent'?", "Multiple Choice"),
                                Tuple.Create("Compound words form by combining two or more separate words.", "True/False"),
                                Tuple.Create("What is the purpose of using transitional words in essays?", "Fill in the blank")
                            }
                        };

                        for (int tIndex = 0; tIndex < topics.Count; tIndex++)
                        {
                            int currentTopicId = topics[tIndex].Id;
                            var questionsForThisTopic = topicQuestionPools[tIndex];

                            foreach (var item in questionsForThisTopic)
                            {
                                sampleQuestions.Add(new Question
                                {
                                    Content = item.Item1,
                                    Topicid = currentTopicId,
                                    Questiontype = item.Item2,
                                    Difficulty = random.Next(1, 4),
                                    Createdat = DateTime.Now
                                });
                            }
                        }

                        db.Questions.AddRange(sampleQuestions);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi ngầm nếu có
            }
        }

        private void SeedOptionsIfEmpty()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    if (!db.Options.Any() && db.Questions.Any())
                    {
                        var questions = db.Questions.ToList();
                        var sampleOptions = new List<Option>();

                        foreach (var q in questions)
                        {
                            string content = q.Content;
                            string qType = q.Questiontype?.Trim().ToLower() ?? "";

                            // 1. Dạng Đúng / Sai (True/False)
                            if (qType.Contains("true") || qType.Contains("false"))
                            {
                                bool isTrueAnswer = true;
                                if (content.Contains("do not require a passport") ||
                                    content.Contains("discouraged") ||
                                    content.Contains("never be submitted") ||
                                    content.Contains("negative impact") ||
                                    content.Contains("unimportant") ||
                                    content.Contains("increase daily stress"))
                                {
                                    isTrueAnswer = false;
                                }

                                sampleOptions.Add(new Option { Optiontext = "True", Iscorrect = isTrueAnswer, Questionid = q.Id });
                                sampleOptions.Add(new Option { Optiontext = "False", Iscorrect = !isTrueAnswer, Questionid = q.Id });
                            }
                            // 2. Dạng Điền từ / Tự luận ngắn (Fill in the blank)
                            else if (qType.Contains("fill") || qType.Contains("blank") || qType.Contains("short"))
                            {
                                string keyword = "Passport";
                                if (content.Contains("mandatory for international air travel")) keyword = "Passport";
                                else if (content.Contains("jet lag")) keyword = "Fatigue and sleep schedule disruption";
                                else if (content.Contains("customs declaration")) keyword = "Carried goods and currency";
                                else if (content.Contains("final-year academic project")) keyword = "Practical application and research";
                                else if (content.Contains("GPA stand for")) keyword = "Grade Point Average";
                                else if (content.Contains("submit their assignments")) keyword = "Learning management system";
                                else if (content.Contains("syllabus designed")) keyword = "Course objectives and schedule";
                                else if (content.Contains("plagiarism mean")) keyword = "Copying intellectual property";
                                else if (content.Contains("university lecture")) keyword = "Knowledge delivery";
                                else if (content.Contains("meeting agenda")) keyword = "Discussion topics";
                                else if (content.Contains("ROI stand for")) keyword = "Return on Investment";
                                else if (content.Contains("marketing strategy")) keyword = "Promoting products";
                                else if (content.Contains("B2B stand for")) keyword = "Business to Business";
                                else if (content.Contains("supply chain")) keyword = "Goods distribution network";
                                else if (content.Contains("follow-up email")) keyword = "Gratitude and recap";
                                else if (content.Contains("morning routine")) keyword = "Productivity and focus";
                                else if (content.Contains("mindfulness or meditation")) keyword = "Stress reduction";
                                else if (content.Contains("reading books daily")) keyword = "Vocabulary and cognitive skills";
                                else if (content.Contains("personal journal")) keyword = "Self-reflection";
                                else if (content.Contains("cloud computing")) keyword = "Remote data access";
                                else if (content.Contains("DBMS stand for")) keyword = "Database Management System";
                                else if (content.Contains("operating system")) keyword = "Manage hardware and software";
                                else if (content.Contains("API stand for")) keyword = "Application Programming Interface";
                                else if (content.Contains("foreign key constraint")) keyword = "Referential integrity";
                                else if (content.Contains("strength training")) keyword = "Build muscle mass";
                                else if (content.Contains("stretching before")) keyword = "Improve flexibility";
                                else if (content.Contains("BMI stand for")) keyword = "Body Mass Index";
                                else if (content.Contains("green vegetables")) keyword = "Vitamins and fiber";
                                else if (content.Contains("preserving endangered wildlife")) keyword = "Maintain biodiversity";
                                else if (content.Contains("sustainable development")) keyword = "Resource balance for future";
                                else if (content.Contains("adjective in a sentence")) keyword = "Modify a noun";
                                else if (content.Contains("punctuation in direct quotations")) keyword = "Quotation marks";
                                else if (content.Contains("transitional words")) keyword = "Connect ideas smoothly";

                                sampleOptions.Add(new Option { Optiontext = keyword, Iscorrect = true, Questionid = q.Id });
                            }
                            // 3. Dạng Trắc nghiệm (Multiple Choice)
                            else
                            {
                                if (content.Contains("natural areas with conservation efforts"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Ecotourism", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Mass tourism", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Business travel", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Space tourism", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("all-inclusive resort"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Meals, drinks, and activities included", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Only room accommodation included", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "No food or beverage provided", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Pay per individual facility usage", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("booking platform is commonly used"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Booking.com", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "GitHub", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Spotify", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Adobe Photoshop", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("short trip for pleasure or education"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Excursion", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Migration", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Commute", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Permanent relocation", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("luggage type is permitted as cabin baggage"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Small carry-on backpack", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Oversized shipping trunk", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Heavy industrial machinery", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Large furniture crate", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("facility on campus is primarily used for research"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "University Library", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Cafeteria", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Parking lot", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Sports stadium", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("English proficiency for international communication"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "TOEIC", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "C++ Certification", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Driving license test", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Cooking diploma", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("department manages employee payroll"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Human Resources (HR)", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Software Development team", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Maintenance department", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Customer Support", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("difference between revenue and net profit"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Net profit subtracts total operating expenses from revenue", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "They are identical financial terms", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Revenue is always lower than profit", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Profit does not account for costs", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("hobby is known for enhancing creativity and manual dexterity"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Handicrafts and crafting", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Watching television", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Casual jogging", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Listening to music", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("physical activity is best for cardiovascular endurance"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Running and swimming", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Sleeping all day", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Sitting at a desk", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Playing video games", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("popular leisure activity for relaxing on weekends"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Reading books or gardening", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Debugging production servers", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Filling tax forms", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Heavy weightlifting", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("programming language is widely used for Android app development"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Dart (Flutter) / Kotlin", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "HTML static markup", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "CSS style sheets", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Plain text files", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("difference between front-end and back-end"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Front-end handles UI; back-end handles server logic and database", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "They are exactly the same thing", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Back-end only designs buttons", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Front-end manages SQL queries", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("food group is the primary source of sustained energy"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Complex carbohydrates", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Plastic materials", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Pure industrial salt", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Tap water only", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("nutrient is essential for repairing body tissues"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Protein", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Sand", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Paper", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Ink", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("renewable energy source harnesses power from wind"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Wind turbine generators", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Coal power plants", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Diesel engines", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Gasoline generators", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("practice helps conserve water resources at home"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Fixing leaky faucets and taking shorter showers", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Leaving garden hoses running overnight", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Washing empty pavements with a hose daily", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Ignoring plumbing leaks", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("past tense of the irregular verb 'run'"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Ran", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Runned", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Running", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Runs", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("synonym for the word 'abundant'") || content.Contains("abundant"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Plentiful", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Scarce", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Tiny", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Barren", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("part of speech expresses an action or state of being"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Verb", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Noun", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Adjective", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Preposition", Iscorrect = false, Questionid = q.Id });
                                }
                                else if (content.Contains("antonym for the word 'transparent'"))
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Opaque", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Clear", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Translucent", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Bright", Iscorrect = false, Questionid = q.Id });
                                }
                                else
                                {
                                    sampleOptions.Add(new Option { Optiontext = "Primary correct alternative", Iscorrect = true, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Alternative option B", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Alternative option C", Iscorrect = false, Questionid = q.Id });
                                    sampleOptions.Add(new Option { Optiontext = "Alternative option D", Iscorrect = false, Questionid = q.Id });
                                }
                            }
                        }

                        db.Options.AddRange(sampleOptions);
                        db.SaveChanges();
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

                    if (dataGridView1.Columns["ID"] != null) dataGridView1.Columns["ID"].Visible = false;
                    if (dataGridView1.Columns["TopicId"] != null) dataGridView1.Columns["TopicId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void btnManageoptions_Click(object sender, EventArgs e)
        {
            FormOption frm = new FormOption();
            frm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}