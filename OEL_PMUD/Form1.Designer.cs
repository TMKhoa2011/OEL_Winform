namespace OEL_PMUD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            lblTopicname = new Label();
            lblDescription = new Label();
            txtTopicname = new TextBox();
            txtDescription = new TextBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 150);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(868, 336);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // lblTopicname
            // 
            lblTopicname.AutoSize = true;
            lblTopicname.Font = new Font("Segoe UI", 12F);
            lblTopicname.Location = new Point(12, 35);
            lblTopicname.Name = "lblTopicname";
            lblTopicname.Size = new Size(115, 28);
            lblTopicname.TabIndex = 1;
            lblTopicname.Text = "Topic Name";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 12F);
            lblDescription.Location = new Point(12, 85);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(112, 28);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description";
            // 
            // txtTopicname
            // 
            txtTopicname.Font = new Font("Segoe UI", 12F);
            txtTopicname.Location = new Point(133, 35);
            txtTopicname.Name = "txtTopicname";
            txtTopicname.Size = new Size(348, 34);
            txtTopicname.TabIndex = 3;
            txtTopicname.TextChanged += txtTopicname_TextChanged;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 12F);
            txtDescription.Location = new Point(133, 85);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(348, 27);
            txtDescription.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 12F);
            btnAdd.Location = new Point(12, 509);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(118, 52);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 12F);
            btnDelete.Location = new Point(154, 509);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(122, 52);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 12F);
            btnUpdate.Location = new Point(303, 509);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(117, 52);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 12F);
            lblSearch.Location = new Point(503, 84);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(70, 28);
            lblSearch.TabIndex = 8;
            lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(579, 82);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(301, 34);
            txtSearch.TabIndex = 9;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 12F);
            btnClear.Location = new Point(448, 509);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(125, 52);
            btnClear.TabIndex = 10;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(892, 584);
            Controls.Add(btnClear);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(txtDescription);
            Controls.Add(txtTopicname);
            Controls.Add(lblDescription);
            Controls.Add(lblTopicname);
            Controls.Add(dataGridView1);
            Font = new Font("Segoe UI", 8.25F);
            Name = "Form1";
            Text = "Manage Topics";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label lblTopicname;
        private Label lblDescription;
        private TextBox txtTopicname;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnClear;
    }
}