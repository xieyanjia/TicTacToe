namespace TicTacToe
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.scorePanel = new System.Windows.Forms.Panel();
            this.lblPlayerScore = new System.Windows.Forms.Label();
            this.lblDrawScore = new System.Windows.Forms.Label();
            this.lblAiScore = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.boardPanel = new System.Windows.Forms.Panel();
            this.btnRestart = new System.Windows.Forms.Button();
            this.scorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // scorePanel
            // 
            this.scorePanel.BackColor = System.Drawing.Color.FromArgb(25, 25, 40);
            this.scorePanel.Controls.Add(this.lblPlayerScore);
            this.scorePanel.Controls.Add(this.lblDrawScore);
            this.scorePanel.Controls.Add(this.lblAiScore);
            this.scorePanel.Location = new System.Drawing.Point(40, 90);
            this.scorePanel.Name = "scorePanel";
            this.scorePanel.Size = new System.Drawing.Size(440, 70);
            this.scorePanel.TabIndex = 0;
            // 
            // lblPlayerScore
            // 
            this.lblPlayerScore.Font = new System.Drawing.Font("Microsoft JhengHei", 11F, System.Drawing.FontStyle.Bold);
            this.lblPlayerScore.ForeColor = System.Drawing.Color.FromArgb(255, 100, 100);
            this.lblPlayerScore.Location = new System.Drawing.Point(20, 5);
            this.lblPlayerScore.Name = "lblPlayerScore";
            this.lblPlayerScore.Size = new System.Drawing.Size(120, 60);
            this.lblPlayerScore.TabIndex = 0;
            this.lblPlayerScore.Text = "你 (X)\n0";
            this.lblPlayerScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDrawScore
            // 
            this.lblDrawScore.Font = new System.Drawing.Font("Microsoft JhengHei", 11F, System.Drawing.FontStyle.Bold);
            this.lblDrawScore.ForeColor = System.Drawing.Color.FromArgb(255, 200, 50);
            this.lblDrawScore.Location = new System.Drawing.Point(160, 5);
            this.lblDrawScore.Name = "lblDrawScore";
            this.lblDrawScore.Size = new System.Drawing.Size(120, 60);
            this.lblDrawScore.TabIndex = 1;
            this.lblDrawScore.Text = "平局\n0";
            this.lblDrawScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAiScore
            // 
            this.lblAiScore.Font = new System.Drawing.Font("Microsoft JhengHei", 11F, System.Drawing.FontStyle.Bold);
            this.lblAiScore.ForeColor = System.Drawing.Color.FromArgb(80, 180, 255);
            this.lblAiScore.Location = new System.Drawing.Point(300, 5);
            this.lblAiScore.Name = "lblAiScore";
            this.lblAiScore.Size = new System.Drawing.Size(120, 60);
            this.lblAiScore.TabIndex = 2;
            this.lblAiScore.Text = "AI (O)\n0";
            this.lblAiScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft JhengHei", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 200, 50);
            this.lblTitle.Location = new System.Drawing.Point(160, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "井字棋";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSub
            // 
            this.lblSub.Font = new System.Drawing.Font("Consolas", 11F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(120, 120, 160);
            this.lblSub.Location = new System.Drawing.Point(150, 58);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(220, 25);
            this.lblSub.TabIndex = 2;
            this.lblSub.Text = "TIC-TAC-TOE";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft JhengHei", 14F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(255, 100, 100);
            this.lblStatus.Location = new System.Drawing.Point(40, 170);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(440, 36);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "你的回合";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boardPanel
            // 
            this.boardPanel.BackColor = System.Drawing.Color.Transparent;
            this.boardPanel.Location = new System.Drawing.Point(80, 215);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(360, 360);
            this.boardPanel.TabIndex = 4;
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(50, 50, 80);
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.btnRestart.ForeColor = System.Drawing.Color.FromArgb(220, 220, 240);
            this.btnRestart.Location = new System.Drawing.Point(180, 590);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(160, 45);
            this.btnRestart.TabIndex = 5;
            this.btnRestart.Text = "重新開始";
            this.btnRestart.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(15, 15, 25);
            this.ClientSize = new System.Drawing.Size(520, 650);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.boardPanel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.scorePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "井字棋 Tic-Tac-Toe";
            this.scorePanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel scorePanel;
        private System.Windows.Forms.Label lblPlayerScore;
        private System.Windows.Forms.Label lblDrawScore;
        private System.Windows.Forms.Label lblAiScore;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Button btnRestart;
    }
}