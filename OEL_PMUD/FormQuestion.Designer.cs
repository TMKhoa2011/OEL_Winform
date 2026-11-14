namespace OEL_PMUD
{
    partial class FormQuestion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbTopic = new ComboBox();
            lblTopic = new Label();
            lblQuestioncontent = new Label();
            txtContent = new TextBox();
            lblQuestiontype = new Label();
            txtQuestiontype = new TextBox();
            lblDifficulty = new Label();
            txtDifficulty = new TextBox();
            dataGridView1 = new DataGridView();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // cbTopic
            // 
            cbTopic.Font = new Font("Segoe UI", 10F);
            cbTopic.FormattingEnabled = true;
            cbTopic.Location = new Point(76, 25);
            cbTopic.Name = "cbTopic";
            cbTopic.Size = new Size(356, 31);
            cbTopic.TabIndex = 0;
            // 
            // lblTopic
            // 
            lblTopic.AutoSize = true;
            lblTopic.Font = new Font("Segoe UI", 10F);
            lblTopic.Location = new Point(12, 28);
            lblTopic.Name = "lblTopic";
            lblTopic.Size = new Size(49, 23);
            lblTopic.TabIndex = 1;
            lblTopic.Text = "Topic";
            // 
            // lblQuestioncontent
            // 
            lblQuestioncontent.AutoSize = true;
            lblQuestioncontent.Font = new Font("Segoe UI", 10F);
            lblQuestioncontent.Location = new Point(12, 80);
            lblQuestioncontent.Name = "lblQuestioncontent";
            lblQuestioncontent.Size = new Size(146, 23);
            lblQuestioncontent.TabIndex = 2;
            lblQuestioncontent.Text = "Question Content";
            // 
            // txtContent
            // 
            txtContent.Font = new Font("Segoe UI", 10F);
            txtContent.Location = new Point(164, 77);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(574, 34);
            txtContent.TabIndex = 3;
            // 
            // lblQuestiontype
            // 
            lblQuestiontype.AutoSize = true;
            lblQuestiontype.Font = new Font("Segoe UI", 10F);
            lblQuestiontype.Location = new Point(12, 125);
            lblQuestiontype.Name = "lblQuestiontype";
            lblQuestiontype.Size = new Size(119, 23);
            lblQuestiontype.TabIndex = 4;
            lblQuestiontype.Text = "Question Type";
            // 
            // txtQuestiontype
            // 
            txtQuestiontype.Font = new Font("Segoe UI", 10F);
            txtQuestiontype.Location = new Point(164, 125);
            txtQuestiontype.Name = "txtQuestiontype";
            txtQuestiontype.Size = new Size(574, 30);
            txtQuestiontype.TabIndex = 5;
            // 
            // lblDifficulty
            // 
            lblDifficulty.AutoSize = true;
            lblDifficulty.Font = new Font("Segoe UI", 10F);
            lblDifficulty.Location = new Point(12, 172);
            lblDifficulty.Name = "lblDifficulty";
            lblDifficulty.Size = new Size(76, 23);
            lblDifficulty.TabIndex = 6;
            lblDifficulty.Text = "Difficulty";
            // 
            // txtDifficulty
            // 
            txtDifficulty.Font = new Font("Segoe UI", 10F);
            txtDifficulty.Location = new Point(164, 172);
            txtDifficulty.Name = "txtDifficulty";
            txtDifficulty.Size = new Size(574, 30);
            txtDifficulty.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 222);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(726, 337);
            dataGridView1.TabIndex = 8;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F);
            lblSearch.Location = new Point(438, 28);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(61, 23);
            lblSearch.TabIndex = 9;
            lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(505, 25);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(333, 30);
            txtSearch.TabIndex = 10;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 10F);
            btnAdd.Location = new Point(744, 222);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 53);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 10F);
            btnUpdate.Location = new Point(744, 313);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 55);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 10F);
            btnDelete.Location = new Point(744, 408);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 53);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 10F);
            btnClear.Location = new Point(744, 508);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 51);
            btnClear.TabIndex = 14;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // FormQuestion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 571);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(dataGridView1);
            Controls.Add(txtDifficulty);
            Controls.Add(lblDifficulty);
            Controls.Add(txtQuestiontype);
            Controls.Add(lblQuestiontype);
            Controls.Add(txtContent);
            Controls.Add(lblQuestioncontent);
            Controls.Add(lblTopic);
            Controls.Add(cbTopic);
            Name = "FormQuestion";
            Text = "FormQuestion";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbTopic;
        private Label lblTopic;
        private Label lblQuestioncontent;
        private TextBox txtContent;
        private Label lblQuestiontype;
        private TextBox txtQuestiontype;
        private Label lblDifficulty;
        private TextBox txtDifficulty;
        private DataGridView dataGridView1;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
    }
}