using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        // ───────────── 遊戲狀態 ─────────────
        private string[] board = new string[9];   // "", "X", "O"
        private bool isPlayerTurn = true;          // true = 玩家(X)，false = AI(O)
        private int playerScore = 0;
        private int aiScore = 0;
        private int drawScore = 0;
        private bool gameOver = false;

        // ───────────── UI 元件 ─────────────
        private Panel[] cells = new Panel[9];
        private Label lblStatus;
        private Label lblPlayerScore;
        private Label lblAiScore;
        private Label lblDrawScore;
        private Button btnRestart;
        private Panel boardPanel;

        // ───────────── 顏色主題 ─────────────
        private readonly Color BG_DARK      = Color.FromArgb(15, 15, 25);
        private readonly Color BG_PANEL     = Color.FromArgb(25, 25, 40);
        private readonly Color CELL_NORMAL  = Color.FromArgb(35, 35, 55);
        private readonly Color CELL_HOVER   = Color.FromArgb(50, 50, 80);
        private readonly Color CELL_WIN     = Color.FromArgb(30, 80, 60);
        private readonly Color COLOR_X      = Color.FromArgb(255, 100, 100);
        private readonly Color COLOR_O      = Color.FromArgb(80, 180, 255);
        private readonly Color COLOR_ACCENT = Color.FromArgb(255, 200, 50);
        private readonly Color COLOR_TEXT   = Color.FromArgb(220, 220, 240);
        private readonly Color GRID_LINE    = Color.FromArgb(70, 70, 110);

        // ───────────── 音效 (beep 模擬) ─────────────
        private void PlayMove()  => System.Threading.Tasks.Task.Run(() => Console.Beep(600, 80));
        private void PlayWin()   => System.Threading.Tasks.Task.Run(() => { Console.Beep(523, 120); Console.Beep(659, 120); Console.Beep(784, 200); });
        private void PlayLose()  => System.Threading.Tasks.Task.Run(() => { Console.Beep(400, 120); Console.Beep(350, 120); Console.Beep(300, 200); });
        private void PlayDraw()  => System.Threading.Tasks.Task.Run(() => { Console.Beep(500, 100); Console.Beep(500, 100); });

        // ───────────── 勝利組合 ─────────────
        private readonly int[][] WIN_LINES =
        {
            new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
            new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
            new[]{0,4,8}, new[]{2,4,6}
        };

        public Form1()
        {
            InitializeComponent();
            BuildUI();
            StartNewGame();
        }

        // ═══════════════════════════════════════
        //  UI 建構
        // ═══════════════════════════════════════
        private void BuildUI()
        {
            this.Text = "井字棋 Tic-Tac-Toe";
            this.Size = new Size(520, 680);
            this.MinimumSize = new Size(520, 680);
            this.MaximumSize = new Size(520, 680);
            this.BackColor = BG_DARK;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10f);

            // ── 標題 ──
            var lblTitle = new Label
            {
                Text = "井字棋",
                Font = new Font("Microsoft JhengHei", 26f, FontStyle.Bold),
                ForeColor = COLOR_ACCENT,
                AutoSize = true,
                Location = new Point(0, 20),
            };
            lblTitle.Left = (520 - lblTitle.PreferredWidth) / 2;
            this.Controls.Add(lblTitle);

            var lblSub = new Label
            {
                Text = "TIC-TAC-TOE",
                Font = new Font("Consolas", 11f, FontStyle.Regular),
                ForeColor = Color.FromArgb(120, 120, 160),
                AutoSize = true,
                Location = new Point(0, 60),
            };
            lblSub.Left = (520 - lblSub.PreferredWidth) / 2;
            this.Controls.Add(lblSub);

            // ── 計分板 ──
            var scorePanel = new Panel
            {
                Size = new Size(440, 70),
                Location = new Point(40, 90),
                BackColor = BG_PANEL,
            };
            RoundPanel(scorePanel, 10);
            this.Controls.Add(scorePanel);

            lblPlayerScore = MakeScoreLabel("你 (X)\n0", COLOR_X, new Point(20, 5));
            lblDrawScore   = MakeScoreLabel("平局\n0",  COLOR_ACCENT, new Point(160, 5));
            lblAiScore     = MakeScoreLabel("AI (O)\n0", COLOR_O, new Point(300, 5));
            scorePanel.Controls.Add(lblPlayerScore);
            scorePanel.Controls.Add(lblDrawScore);
            scorePanel.Controls.Add(lblAiScore);

            // ── 狀態列 ──
            lblStatus = new Label
            {
                Text = "你的回合",
                Font = new Font("Microsoft JhengHei", 14f, FontStyle.Bold),
                ForeColor = COLOR_X,
                AutoSize = false,
                Size = new Size(440, 36),
                Location = new Point(40, 170),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };
            this.Controls.Add(lblStatus);

            // ── 棋盤 ──
            boardPanel = new Panel
            {
                Size = new Size(360, 360),
                Location = new Point(80, 215),
                BackColor = Color.Transparent,
            };
            boardPanel.Paint += BoardPanel_Paint;
            this.Controls.Add(boardPanel);

            int cellSize = 110;
            int gap = 10;
            int offset = 5;
            for (int i = 0; i < 9; i++)
            {
                int row = i / 3, col = i % 3;
                var cell = new Panel
                {
                    Size = new Size(cellSize, cellSize),
                    Location = new Point(offset + col * (cellSize + gap),
                                        offset + row * (cellSize + gap)),
                    BackColor = CELL_NORMAL,
                    Cursor = Cursors.Hand,
                    Tag = i,
                };
                RoundPanel(cell, 12);
                cell.Paint += Cell_Paint;
                cell.Click += Cell_Click;
                cell.MouseEnter += Cell_MouseEnter;
                cell.MouseLeave += Cell_MouseLeave;
                boardPanel.Controls.Add(cell);
                cells[i] = cell;
            }

            // ── 重新開始按鈕 ──
            btnRestart = new Button
            {
                Text = "重新開始",
                Font = new Font("Microsoft JhengHei", 12f, FontStyle.Bold),
                Size = new Size(160, 45),
                Location = new Point(180, 590),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(50, 50, 80),
                ForeColor = COLOR_TEXT,
                Cursor = Cursors.Hand,
            };
            btnRestart.FlatAppearance.BorderColor = GRID_LINE;
            btnRestart.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 110);
            btnRestart.Click += (s, e) => StartNewGame();
            this.Controls.Add(btnRestart);
        }

        private Label MakeScoreLabel(string text, Color color, Point loc)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Microsoft JhengHei", 11f, FontStyle.Bold),
                ForeColor = color,
                AutoSize = false,
                Size = new Size(120, 60),
                Location = loc,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };
        }

        private void RoundPanel(Panel p, int radius)
        {
            // 圓角用 Region
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(p.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(p.Width - radius * 2, p.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, p.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            p.Region = new Region(path);
        }

        // ═══════════════════════════════════════
        //  繪圖事件
        // ═══════════════════════════════════════
        private void BoardPanel_Paint(object sender, PaintEventArgs e)
        {
            // 棋盤背景已由 cell 覆蓋，不需額外畫線
        }

        private int[] winLine = null;

        private void Cell_Paint(object sender, PaintEventArgs e)
        {
            var cell = (Panel)sender;
            int idx = (int)cell.Tag;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int w = cell.Width, h = cell.Height;
            int pad = 22;

            string val = board[idx];

            if (val == "X")
            {
                using var pen = new Pen(COLOR_X, 8f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
                g.DrawLine(pen, pad, pad, w - pad, h - pad);
                g.DrawLine(pen, w - pad, pad, pad, h - pad);
            }
            else if (val == "O")
            {
                using var pen = new Pen(COLOR_O, 8f);
                g.DrawEllipse(pen, pad, pad, w - pad * 2, h - pad * 2);
            }
        }

        // ═══════════════════════════════════════
        //  互動事件
        // ═══════════════════════════════════════
        private void Cell_Click(object sender, EventArgs e)
        {
            if (!isPlayerTurn || gameOver) return;
            var cell = (Panel)sender;
            int idx = (int)cell.Tag;
            if (board[idx] != "") return;

            PlaceMove(idx, "X");
            PlayMove();

            if (CheckEnd("X")) return;

            isPlayerTurn = false;
            lblStatus.Text = "AI 思考中…";
            lblStatus.ForeColor = COLOR_O;

            // 小延遲讓 AI 看起來在思考
            var timer = new Timer { Interval = 400 };
            timer.Tick += (ts, te) =>
            {
                timer.Stop();
                AiMove();
            };
            timer.Start();
        }

        private void Cell_MouseEnter(object sender, EventArgs e)
        {
            var cell = (Panel)sender;
            int idx = (int)cell.Tag;
            if (board[idx] == "" && isPlayerTurn && !gameOver)
                cell.BackColor = CELL_HOVER;
        }

        private void Cell_MouseLeave(object sender, EventArgs e)
        {
            var cell = (Panel)sender;
            int idx = (int)cell.Tag;
            bool isWinCell = winLine != null && Array.IndexOf(winLine, idx) >= 0;
            cell.BackColor = isWinCell ? CELL_WIN : CELL_NORMAL;
        }

        // ═══════════════════════════════════════
        //  遊戲邏輯
        // ═══════════════════════════════════════
        private void PlaceMove(int idx, string mark)
        {
            board[idx] = mark;
            cells[idx].Invalidate();
        }

        private void AiMove()
        {
            int best = Minimax_BestMove();
            PlaceMove(best, "O");
            PlayMove();

            if (!CheckEnd("O"))
            {
                isPlayerTurn = true;
                lblStatus.Text = "你的回合";
                lblStatus.ForeColor = COLOR_X;
            }
        }

        private bool CheckEnd(string mark)
        {
            int[] line = GetWinLine(mark);
            if (line != null)
            {
                winLine = line;
                foreach (int i in line)
                    cells[i].BackColor = CELL_WIN;

                if (mark == "X")
                {
                    playerScore++;
                    lblPlayerScore.Text = $"你 (X)\n{playerScore}";
                    lblStatus.Text = "🎉 你贏了！";
                    lblStatus.ForeColor = COLOR_X;
                    PlayWin();
                }
                else
                {
                    aiScore++;
                    lblAiScore.Text = $"AI (O)\n{aiScore}";
                    lblStatus.Text = "😢 AI 贏了！";
                    lblStatus.ForeColor = COLOR_O;
                    PlayLose();
                }
                gameOver = true;
                return true;
            }

            // 平局
            bool full = true;
            foreach (var v in board) if (v == "") { full = false; break; }
            if (full)
            {
                drawScore++;
                lblDrawScore.Text = $"平局\n{drawScore}";
                lblStatus.Text = "🤝 平局！";
                lblStatus.ForeColor = COLOR_ACCENT;
                PlayDraw();
                gameOver = true;
                return true;
            }
            return false;
        }

        private int[] GetWinLine(string mark)
        {
            foreach (var line in WIN_LINES)
                if (board[line[0]] == mark && board[line[1]] == mark && board[line[2]] == mark)
                    return line;
            return null;
        }

        private void StartNewGame()
        {
            board = new string[9];
            for (int i = 0; i < 9; i++) board[i] = "";
            winLine = null;
            gameOver = false;
            isPlayerTurn = true;

            foreach (var cell in cells)
            {
                cell.BackColor = CELL_NORMAL;
                cell.Invalidate();
            }

            lblStatus.Text = "你的回合";
            lblStatus.ForeColor = COLOR_X;
        }

        // ═══════════════════════════════════════
        //  Minimax AI
        // ═══════════════════════════════════════
        private int Minimax_BestMove()
        {
            int bestVal = int.MinValue, bestIdx = -1;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != "") continue;
                board[i] = "O";
                int val = Minimax(false, 0);
                board[i] = "";
                if (val > bestVal) { bestVal = val; bestIdx = i; }
            }
            return bestIdx;
        }

        private int Minimax(bool isMax, int depth)
        {
            if (GetWinLine("O") != null) return 10 - depth;
            if (GetWinLine("X") != null) return depth - 10;
            bool hasMoves = false;
            foreach (var v in board) if (v == "") { hasMoves = true; break; }
            if (!hasMoves) return 0;

            if (isMax)
            {
                int best = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (board[i] != "") continue;
                    board[i] = "O";
                    best = Math.Max(best, Minimax(false, depth + 1));
                    board[i] = "";
                }
                return best;
            }
            else
            {
                int best = int.MaxValue;
                for (int i = 0; i < 9; i++)
                {
                    if (board[i] != "") continue;
                    board[i] = "X";
                    best = Math.Min(best, Minimax(true, depth + 1));
                    board[i] = "";
                }
                return best;
            }
        }
    }
}
