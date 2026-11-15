namespace OEL_PMUD
{
    partial class FormOption
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
            lblSelectquestion = new Label();
            cbSelectquestion = new ComboBox();
            dgvOptions = new DataGridView();
            lblOptiontext = new Label();
            txtOptiontext = new TextBox();
            chkIscorrect = new CheckBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnClear = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvOptions).BeginInit();
            SuspendLayout();
            // 
            // lblSelectquestion
            // 
            lblSelectquestion.AutoSize = true;
            lblSelectquestion.Font = new Font("Segoe UI", 10F);
            lblSelectquestion.Location = new Point(12, 25);
            lblSelectquestion.Name = "lblSelectquestion";
            lblSelectquestion.Size = new Size(129, 23);
            lblSelectquestion.TabIndex = 0;
            lblSelectquestion.Text = "Select Question";
            // 
            // cbSelectquestion
            // 
            cbSelectquestion.Font = new Font("Segoe UI", 10F);
            cbSelectquestion.FormattingEnabled = true;
            cbSelectquestion.Location = new Point(147, 22);
            cbSelectquestion.Name = "cbSelectquestion";
            cbSelectquestion.Size = new Size(583, 31);
            cbSelectquestion.TabIndex = 1;
            cbSelectquestion.SelectedIndexChanged += cbSelectquestion_SelectedIndexChanged;
            // 
            // dgvOptions
            // 
            dgvOptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptions.Location = new Point(14, 171);
            dgvOptions.Name = "dgvOptions";
            dgvOptions.RowHeadersWidth = 51;
            dgvOptions.Size = new Size(716, 334);
            dgvOptions.TabIndex = 2;
            dgvOptions.CellClick += dgvOptions_CellClick;
            // 
            // lblOptiontext
            // 
            lblOptiontext.AutoSize = true;
            lblOptiontext.Font = new Font("Segoe UI", 10F);
            lblOptiontext.Location = new Point(12, 81);
            lblOptiontext.Name = "lblOptiontext";
            lblOptiontext.Size = new Size(98, 23);
            lblOptiontext.TabIndex = 3;
            lblOptiontext.Text = "Option Text";
            // 
            // txtOptiontext
            // 
            txtOptiontext.Font = new Font("Segoe UI", 10F);
            txtOptiontext.Location = new Point(147, 78);
            txtOptiontext.Name = "txtOptiontext";
            txtOptiontext.Size = new Size(583, 30);
            txtOptiontext.TabIndex = 4;
            // 
            // chkIscorrect
            // 
            chkIscorrect.AutoSize = true;
            chkIscorrect.Location = new Point(147, 124);
            chkIscorrect.Name = "chkIscorrect";
            chkIscorrect.Size = new Size(145, 24);
            chkIscorrect.TabIndex = 5;
            chkIscorrect.Text = "Is Correct Answer";
            chkIscorrect.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 10F);
            btnAdd.Location = new Point(770, 171);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(111, 41);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 10F);
            btnDelete.Location = new Point(770, 266);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(111, 43);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 10F);
            btnUpdate.Location = new Point(770, 218);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(111, 42);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(770, 315);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(111, 42);
            btnClear.TabIndex = 9;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 10F);
            btnBack.Location = new Point(770, 476);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(111, 29);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // FormOption
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(893, 517);
            Controls.Add(btnBack);
            Controls.Add(btnClear);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(chkIscorrect);
            Controls.Add(txtOptiontext);
            Controls.Add(lblOptiontext);
            Controls.Add(dgvOptions);
            Controls.Add(cbSelectquestion);
            Controls.Add(lblSelectquestion);
            Name = "FormOption";
            Text = "Manage Option";
            ((System.ComponentModel.ISupportInitialize)dgvOptions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSelectquestion;
        private ComboBox cbSelectquestion;
        private DataGridView dgvOptions;
        private Label lblOptiontext;
        private TextBox txtOptiontext;
        private CheckBox chkIscorrect;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnClear;
        private Button btnBack;
    }
}