namespace OEL_PMUD
{
    partial class FormAdminResult
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
            gbTop = new GroupBox();
            lblKpipassrate = new Label();
            lblKpiavgscore = new Label();
            lblKpiattempts = new Label();
            lblKpistudents = new Label();
            btnApplyfilter = new Button();
            cboExamfilter = new ComboBox();
            tabControl1 = new TabControl();
            tabOverview = new TabPage();
            lblRecentattempts = new Label();
            lblTopstudent = new Label();
            dgvRecentresults = new DataGridView();
            dgvLeaderboard = new DataGridView();
            tabBystudent = new TabPage();
            lblStudentbest = new Label();
            lblStudentavg = new Label();
            lblStudentattempts = new Label();
            lblStudentname = new Label();
            txtSearchstudent = new TextBox();
            dgvStudenthistory = new DataGridView();
            dgvStudents = new DataGridView();
            gbTop.SuspendLayout();
            tabControl1.SuspendLayout();
            tabOverview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentresults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).BeginInit();
            tabBystudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudenthistory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            // 
            // gbTop
            // 
            gbTop.Controls.Add(lblKpipassrate);
            gbTop.Controls.Add(lblKpiavgscore);
            gbTop.Controls.Add(lblKpiattempts);
            gbTop.Controls.Add(lblKpistudents);
            gbTop.Controls.Add(btnApplyfilter);
            gbTop.Controls.Add(cboExamfilter);
            gbTop.Font = new Font("Segoe UI", 10F);
            gbTop.Location = new Point(12, 12);
            gbTop.Name = "gbTop";
            gbTop.Size = new Size(1040, 168);
            gbTop.TabIndex = 0;
            gbTop.TabStop = false;
            gbTop.Text = "Filters & General Statistics";
            // 
            // lblKpipassrate
            // 
            lblKpipassrate.AutoSize = true;
            lblKpipassrate.Font = new Font("Segoe UI", 13F);
            lblKpipassrate.Location = new Point(604, 91);
            lblKpipassrate.Name = "lblKpipassrate";
            lblKpipassrate.Size = new Size(60, 30);
            lblKpipassrate.TabIndex = 5;
            lblKpipassrate.Text = "0.0%";
            // 
            // lblKpiavgscore
            // 
            lblKpiavgscore.AutoSize = true;
            lblKpiavgscore.Font = new Font("Segoe UI", 13F);
            lblKpiavgscore.Location = new Point(435, 91);
            lblKpiavgscore.Name = "lblKpiavgscore";
            lblKpiavgscore.Size = new Size(54, 30);
            lblKpiavgscore.TabIndex = 4;
            lblKpiavgscore.Text = "0.00";
            // 
            // lblKpiattempts
            // 
            lblKpiattempts.AutoSize = true;
            lblKpiattempts.Font = new Font("Segoe UI", 13F);
            lblKpiattempts.Location = new Point(276, 91);
            lblKpiattempts.Name = "lblKpiattempts";
            lblKpiattempts.Size = new Size(70, 30);
            lblKpiattempts.TabIndex = 3;
            lblKpiattempts.Text = "0 turn";
            // 
            // lblKpistudents
            // 
            lblKpistudents.AutoSize = true;
            lblKpistudents.Font = new Font("Segoe UI", 13F);
            lblKpistudents.Location = new Point(105, 91);
            lblKpistudents.Name = "lblKpistudents";
            lblKpistudents.Size = new Size(104, 30);
            lblKpistudents.TabIndex = 2;
            lblKpistudents.Text = "0 / 20 HS";
            // 
            // btnApplyfilter
            // 
            btnApplyfilter.Location = new Point(585, 38);
            btnApplyfilter.Name = "btnApplyfilter";
            btnApplyfilter.Size = new Size(120, 31);
            btnApplyfilter.TabIndex = 1;
            btnApplyfilter.Text = "Filter";
            btnApplyfilter.UseVisualStyleBackColor = true;
            // 
            // cboExamfilter
            // 
            cboExamfilter.FormattingEnabled = true;
            cboExamfilter.Location = new Point(105, 39);
            cboExamfilter.Name = "cboExamfilter";
            cboExamfilter.Size = new Size(462, 31);
            cboExamfilter.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabOverview);
            tabControl1.Controls.Add(tabBystudent);
            tabControl1.Font = new Font("Segoe UI", 10F);
            tabControl1.Location = new Point(12, 186);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1040, 393);
            tabControl1.TabIndex = 1;
            // 
            // tabOverview
            // 
            tabOverview.Controls.Add(lblRecentattempts);
            tabOverview.Controls.Add(lblTopstudent);
            tabOverview.Controls.Add(dgvRecentresults);
            tabOverview.Controls.Add(dgvLeaderboard);
            tabOverview.Location = new Point(4, 32);
            tabOverview.Name = "tabOverview";
            tabOverview.Padding = new Padding(3);
            tabOverview.Size = new Size(1032, 357);
            tabOverview.TabIndex = 0;
            tabOverview.Text = "Overview and Rankings";
            tabOverview.UseVisualStyleBackColor = true;
            // 
            // lblRecentattempts
            // 
            lblRecentattempts.AutoSize = true;
            lblRecentattempts.Location = new Point(6, 181);
            lblRecentattempts.Name = "lblRecentattempts";
            lblRecentattempts.Size = new Size(137, 23);
            lblRecentattempts.TabIndex = 3;
            lblRecentattempts.Text = "Recent Attempts";
            // 
            // lblTopstudent
            // 
            lblTopstudent.AutoSize = true;
            lblTopstudent.Location = new Point(6, 3);
            lblTopstudent.Name = "lblTopstudent";
            lblTopstudent.Size = new Size(108, 23);
            lblTopstudent.TabIndex = 2;
            lblTopstudent.Text = "Top Students";
            // 
            // dgvRecentresults
            // 
            dgvRecentresults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecentresults.Location = new Point(6, 207);
            dgvRecentresults.Name = "dgvRecentresults";
            dgvRecentresults.RowHeadersWidth = 51;
            dgvRecentresults.Size = new Size(1020, 144);
            dgvRecentresults.TabIndex = 1;
            // 
            // dgvLeaderboard
            // 
            dgvLeaderboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLeaderboard.Location = new Point(6, 29);
            dgvLeaderboard.Name = "dgvLeaderboard";
            dgvLeaderboard.RowHeadersWidth = 51;
            dgvLeaderboard.Size = new Size(1020, 149);
            dgvLeaderboard.TabIndex = 0;
            // 
            // tabBystudent
            // 
            tabBystudent.Controls.Add(lblStudentbest);
            tabBystudent.Controls.Add(lblStudentavg);
            tabBystudent.Controls.Add(lblStudentattempts);
            tabBystudent.Controls.Add(lblStudentname);
            tabBystudent.Controls.Add(txtSearchstudent);
            tabBystudent.Controls.Add(dgvStudenthistory);
            tabBystudent.Controls.Add(dgvStudents);
            tabBystudent.Location = new Point(4, 32);
            tabBystudent.Name = "tabBystudent";
            tabBystudent.Padding = new Padding(3);
            tabBystudent.Size = new Size(1032, 357);
            tabBystudent.TabIndex = 1;
            tabBystudent.Text = "Student statics";
            tabBystudent.UseVisualStyleBackColor = true;
            // 
            // lblStudentbest
            // 
            lblStudentbest.AutoSize = true;
            lblStudentbest.Location = new Point(627, 146);
            lblStudentbest.Name = "lblStudentbest";
            lblStudentbest.Size = new Size(82, 23);
            lblStudentbest.TabIndex = 6;
            lblStudentbest.Text = "Best: 0.00";
            // 
            // lblStudentavg
            // 
            lblStudentavg.AutoSize = true;
            lblStudentavg.Location = new Point(627, 113);
            lblStudentavg.Name = "lblStudentavg";
            lblStudentavg.Size = new Size(79, 23);
            lblStudentavg.TabIndex = 5;
            lblStudentavg.Text = "Avg: 0.00";
            // 
            // lblStudentattempts
            // 
            lblStudentattempts.AutoSize = true;
            lblStudentattempts.Location = new Point(627, 81);
            lblStudentattempts.Name = "lblStudentattempts";
            lblStudentattempts.Size = new Size(98, 23);
            lblStudentattempts.TabIndex = 4;
            lblStudentattempts.Text = "Attempts: 0";
            // 
            // lblStudentname
            // 
            lblStudentname.AutoSize = true;
            lblStudentname.Location = new Point(627, 44);
            lblStudentname.Name = "lblStudentname";
            lblStudentname.Size = new Size(24, 23);
            lblStudentname.TabIndex = 3;
            lblStudentname.Text = "--";
            // 
            // txtSearchstudent
            // 
            txtSearchstudent.Location = new Point(6, 8);
            txtSearchstudent.Name = "txtSearchstudent";
            txtSearchstudent.Size = new Size(509, 30);
            txtSearchstudent.TabIndex = 2;
            // 
            // dgvStudenthistory
            // 
            dgvStudenthistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudenthistory.Location = new Point(627, 185);
            dgvStudenthistory.Name = "dgvStudenthistory";
            dgvStudenthistory.RowHeadersWidth = 51;
            dgvStudenthistory.Size = new Size(399, 166);
            dgvStudenthistory.TabIndex = 1;
            // 
            // dgvStudents
            // 
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Location = new Point(6, 44);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.Size = new Size(615, 307);
            dgvStudents.TabIndex = 0;
            // 
            // FormAdminResult
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 582);
            Controls.Add(tabControl1);
            Controls.Add(gbTop);
            Name = "FormAdminResult";
            Text = "FormAdminResult";
            gbTop.ResumeLayout(false);
            gbTop.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabOverview.ResumeLayout(false);
            tabOverview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentresults).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).EndInit();
            tabBystudent.ResumeLayout(false);
            tabBystudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudenthistory).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbTop;
        private Button btnApplyfilter;
        private ComboBox cboExamfilter;
        private Label lblKpipassrate;
        private Label lblKpiavgscore;
        private Label lblKpiattempts;
        private Label lblKpistudents;
        private TabControl tabControl1;
        private TabPage tabOverview;
        private TabPage tabBystudent;
        private Label lblRecentattempts;
        private Label lblTopstudent;
        private DataGridView dgvRecentresults;
        private DataGridView dgvLeaderboard;
        private DataGridView dgvStudenthistory;
        private DataGridView dgvStudents;
        private TextBox txtSearchstudent;
        private Label lblStudentbest;
        private Label lblStudentavg;
        private Label lblStudentattempts;
        private Label lblStudentname;
    }
}