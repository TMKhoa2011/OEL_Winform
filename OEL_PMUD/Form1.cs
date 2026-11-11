using OEL_PMUD.Models;
using OEL_PMUD.Models.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OEL_PMUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Tự động thêm đúng 8 chủ đề tiếng Anh chuẩn vào database nếu đang trống
            SeedSampleDataIfEmpty();

            // Nạp dữ liệu lên DataGridView khi khởi chạy
            LoadTopicData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        #region 1. NẠP DỮ LIỆU BẢNG TOPIC (READ)

        public void LoadTopicData()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    var topics = db.Topics
                                   .Select(t => new
                                   {
                                       ID = t.Id,
                                       TopicName = t.Name,
                                       Description = t.Description,
                                       CreatedAt = t.Createdat
                                   })
                                   .ToList();

                    dataGridView1.DataSource = topics;

                    if (dataGridView1.Columns["TopicName"] != null)
                        dataGridView1.Columns["TopicName"].HeaderText = "Topic Name";
                    if (dataGridView1.Columns["Description"] != null)
                        dataGridView1.Columns["Description"].HeaderText = "Description";
                    if (dataGridView1.Columns["CreatedAt"] != null)
                        dataGridView1.Columns["CreatedAt"].HeaderText = "Created At";

                    // Tự động dàn đều các cột lấp đầy toàn bộ khung bảng (Cách 2)
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message,
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 2. CÁC PHƯƠNG THỨC XỬ LÝ NGHIỆP VỤ (CRUD)

        public bool AddTopic(string name, string description)
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    var newTopic = new Topic
                    {
                        Name = name,
                        Description = description,
                        Createdat = DateTime.Now
                    };

                    db.Topics.Add(newTopic);
                    db.SaveChanges();
                }

                LoadTopicData();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding topic: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateTopic(int topicId, string newName, string newDescription)
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    var topic = db.Topics.FirstOrDefault(t => t.Id == topicId);
                    if (topic != null)
                    {
                        topic.Name = newName;
                        topic.Description = newDescription;

                        db.SaveChanges();
                        LoadTopicData();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Topic not found for updating!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating topic: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteTopic(int topicId)
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    var topic = db.Topics.FirstOrDefault(t => t.Id == topicId);
                    if (topic != null)
                    {
                        db.Topics.Remove(topic);
                        db.SaveChanges();
                        LoadTopicData();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Topic not found for deletion!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting topic: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region 3. HÀM TẠO 8 CHỦ ĐỀ CHUẨN TỪ TÀI LIỆU (SEED DATA)

        private void SeedSampleDataIfEmpty()
        {
            try
            {
                using (var db = new Appdbcontext())
                {
                    if (!db.Topics.Any())
                    {
                        var listTopics = new Topic[]
                        {
                            new Topic { Name = "Travel and Tourism", Description = "Topics about traveling, hotels, and tourism services", Createdat = DateTime.Now },
                            new Topic { Name = "Education and School Life", Description = "Topics about studying, school activities, and exams", Createdat = DateTime.Now },
                            new Topic { Name = "Workspace and Business", Description = "Topics about office work, business, and corporate communication", Createdat = DateTime.Now },
                            new Topic { Name = "Daily Life and Hobbies", Description = "Topics about daily routines, leisure activities, and interests", Createdat = DateTime.Now },
                            new Topic { Name = "Technology and Media", Description = "Topics about modern technology, devices, and social media", Createdat = DateTime.Now },
                            new Topic { Name = "Health and Fitness", Description = "Topics about physical health, sports, and well-being", Createdat = DateTime.Now },
                            new Topic { Name = "Enviroment and Nature", Description = "Topics about nature preservation, climate, and ecology", Createdat = DateTime.Now },
                            new Topic { Name = "Grammar and Vocabulary Fundamentals", Description = "Core English grammar rules and essential vocabulary sets", Createdat = DateTime.Now }
                        };

                        db.Topics.AddRange(listTopics);
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                // Bỏ qua nếu lỗi khởi tạo ban đầu
            }
        }

        #endregion

        #region 4. SỰ KIỆN GIAO DIỆN (UI EVENTS)

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtTopicname.Text = row.Cells["TopicName"].Value?.ToString() ?? "";
                txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTopicname.Text))
            {
                MessageBox.Show("Please enter the topic name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = AddTopic(txtTopicname.Text, txtDescription.Text);
            if (success)
            {
                MessageBox.Show("Topic added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTopicname.Clear();
                txtDescription.Clear();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a topic from the table to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTopicname.Text))
            {
                MessageBox.Show("Topic name cannot be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);

            bool success = UpdateTopic(id, txtTopicname.Text, txtDescription.Text);
            if (success)
            {
                MessageBox.Show("Topic updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a topic from the table to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this topic?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                bool success = DeleteTopic(id);
                if (success)
                {
                    MessageBox.Show("Topic deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTopicname.Clear();
                    txtDescription.Clear();
                }
            }
        }

        #endregion
    }
}