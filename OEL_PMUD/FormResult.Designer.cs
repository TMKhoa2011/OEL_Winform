namespace OEL_PMUD
{
    partial class FormResult
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
            lblExamhistory = new Label();
            dgvResults = new DataGridView();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // lblExamhistory
            // 
            lblExamhistory.AutoSize = true;
            lblExamhistory.Font = new Font("Segoe UI", 10F);
            lblExamhistory.Location = new Point(275, 24);
            lblExamhistory.Name = "lblExamhistory";
            lblExamhistory.Size = new Size(196, 23);
            lblExamhistory.TabIndex = 0;
            lblExamhistory.Text = "EXAM RESULTS HISTORY";
            // 
            // dgvResults
            // 
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(2, 72);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersWidth = 51;
            dgvResults.Size = new Size(797, 318);
            dgvResults.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.Location = new Point(673, 396);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(115, 42);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // FormResult
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(dgvResults);
            Controls.Add(lblExamhistory);
            Name = "FormResult";
            Text = "FormResult";
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblExamhistory;
        private DataGridView dgvResults;
        private Button btnClose;
    }
}