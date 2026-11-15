namespace OEL_PMUD
{
    partial class FormQuiz
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
            btnStartquiz = new Button();
            lblTime = new Label();
            lblQuestionnumber = new Label();
            lblQuestioncontent = new Label();
            pnlOptionscontainer = new Panel();
            btnPrevious = new Button();
            btnNext = new Button();
            btnSubmitexam = new Button();
            cbSelecttopicc = new ComboBox();
            SuspendLayout();
            // 
            // btnStartquiz
            // 
            btnStartquiz.Font = new Font("Segoe UI", 10F);
            btnStartquiz.Location = new Point(741, 65);
            btnStartquiz.Name = "btnStartquiz";
            btnStartquiz.Size = new Size(139, 47);
            btnStartquiz.TabIndex = 1;
            btnStartquiz.Text = "Start Quiz";
            btnStartquiz.UseVisualStyleBackColor = true;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 10F);
            lblTime.Location = new Point(25, 20);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(129, 23);
            lblTime.TabIndex = 2;
            lblTime.Text = "Time Left: 60:00";
            // 
            // lblQuestionnumber
            // 
            lblQuestionnumber.AutoSize = true;
            lblQuestionnumber.Font = new Font("Segoe UI", 10F);
            lblQuestionnumber.Location = new Point(25, 55);
            lblQuestionnumber.Name = "lblQuestionnumber";
            lblQuestionnumber.Size = new Size(127, 23);
            lblQuestionnumber.TabIndex = 3;
            lblQuestionnumber.Text = "Question 0 of 0";
            // 
            // lblQuestioncontent
            // 
            lblQuestioncontent.AutoSize = true;
            lblQuestioncontent.Font = new Font("Segoe UI", 10F);
            lblQuestioncontent.Location = new Point(25, 92);
            lblQuestioncontent.Name = "lblQuestioncontent";
            lblQuestioncontent.Size = new Size(235, 23);
            lblQuestioncontent.TabIndex = 4;
            lblQuestioncontent.Text = "Question content goes here...";
            // 
            // pnlOptionscontainer
            // 
            pnlOptionscontainer.AutoScroll = true;
            pnlOptionscontainer.Font = new Font("Segoe UI", 10F);
            pnlOptionscontainer.Location = new Point(25, 118);
            pnlOptionscontainer.Name = "pnlOptionscontainer";
            pnlOptionscontainer.Size = new Size(855, 278);
            pnlOptionscontainer.TabIndex = 5;
            // 
            // btnPrevious
            // 
            btnPrevious.Font = new Font("Segoe UI", 10F);
            btnPrevious.Location = new Point(25, 413);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(116, 42);
            btnPrevious.TabIndex = 6;
            btnPrevious.Text = "< Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Font = new Font("Segoe UI", 10F);
            btnNext.Location = new Point(147, 413);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(116, 42);
            btnNext.TabIndex = 7;
            btnNext.Text = "Next >";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // btnSubmitexam
            // 
            btnSubmitexam.Font = new Font("Segoe UI", 10F);
            btnSubmitexam.Location = new Point(721, 402);
            btnSubmitexam.Name = "btnSubmitexam";
            btnSubmitexam.Size = new Size(159, 80);
            btnSubmitexam.TabIndex = 8;
            btnSubmitexam.Text = "Submit Exam";
            btnSubmitexam.UseVisualStyleBackColor = true;
            // 
            // cbSelecttopicc
            // 
            cbSelecttopicc.Font = new Font("Segoe UI", 10F);
            cbSelecttopicc.FormattingEnabled = true;
            cbSelecttopicc.Location = new Point(158, 54);
            cbSelecttopicc.Name = "cbSelecttopicc";
            cbSelecttopicc.Size = new Size(373, 31);
            cbSelecttopicc.TabIndex = 9;
            // 
            // FormQuiz
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 494);
            Controls.Add(cbSelecttopicc);
            Controls.Add(btnSubmitexam);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(pnlOptionscontainer);
            Controls.Add(lblQuestioncontent);
            Controls.Add(lblQuestionnumber);
            Controls.Add(lblTime);
            Controls.Add(btnStartquiz);
            Name = "FormQuiz";
            Text = "FormQuiz";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnStartquiz;
        private Label lblTime;
        private Label lblQuestionnumber;
        private Label lblQuestioncontent;
        private Panel pnlOptionscontainer;
        private Button btnPrevious;
        private Button btnNext;
        private Button btnSubmitexam;
        private ComboBox cbSelecttopicc;
    }
}