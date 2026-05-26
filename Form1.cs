using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        // ───────────── 遊戲狀態 ─────────────
        private string[] board = new string[9];
        private bool isPlayerTurn = true;
        private int playerScore = 0;
        private int aiScore = 0;
        private int drawScore = 0;
        private bool gameOver = false;
        private int[] winLine = null;

        // ───────────── 棋盤格子 ─────────────
        private Panel[] cells = new Panel[9];

        // ───────────── 棋子圖片 ─────────────
        private Image imgX = null;
        private Image imgO = null;

        // ───────────── 顏色主題 ─────────────
        private readonly Color CELL_NORMAL = Color.FromArgb(35, 35, 55);
        private readonly Color CELL_HOVER = Color.FromArgb(50, 50, 80);
        private readonly Color CELL_WIN = Color.FromArgb(30, 80, 60);
        private readonly Color COLOR_X = Color.FromArgb(255, 100, 100);
        private readonly Color COLOR_O = Color.FromArgb(80, 180, 255);
        private readonly Color COLOR_ACCENT = Color.FromArgb(255, 200, 50);
        private readonly Color GRID_LINE = Color.FromArgb(70, 70, 110);

        // ───────────── 音效 ─────────────
        private void PlayMove() => System.Threading.Tasks.Task.Run(() => Console.Beep(600, 80));
        private void PlayWin() => System.Threading.Tasks.Task.Run(() => { Console.Beep(523, 120); Console.Beep(659, 120); Console.Beep(784, 200); });
        private void PlayLose() => System.Threading.Tasks.Task.Run(() => { Console.Beep(400, 120); Console.Beep(350, 120); Console.Beep(300, 200); });
        private void PlayDraw() => System.Threading.Tasks.Task.Run(() => { Console.Beep(500, 100); Console.Beep(500, 100); });

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
            LoadImages();
            SetupUI();
            StartNewGame();
        }

        // ═══════════════════════════════════════
        //  載入圖片
        // ═══════════════════════════════════════
        private void LoadImages()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] candidates = {
                Path.Combine(baseDir, "Resources"),
                Path.Combine(baseDir, @"..\..\..\..\Resources"),
                Path.Combine(baseDir, @"..\..\..\Resources"),
            };

            string resDir = null;
            foreach (var c in candidates)
            {
                if (Directory.Exists(c)) { resDir = c; break; }
            }

            if (resDir != null)
            {
                string xPath = Path.Combine(resDir, "X.png");
                string oPath = Path.Combine(resDir, "O.png");
                if (File.Exists(xPath)) imgX = Image.FromFile(xPath);
                if (File.Exists(oPath)) imgO = Image.FromFile(oPath);
            }
        }

        // ═══════════════════════════════════════
        //  初始化 UI
        // ═══════════════════════════════════════
        private void SetupUI()
        {
            RoundPanel(scorePanel, 10);

            btnRestart.FlatAppearance.BorderColor = GRID_LINE;
            btnRestart.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 110);
            btnRestart.Click += (s, e) => StartNewGame();

            int cellSize = 110, gap = 10, offset = 5;
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
        }

        private void RoundPanel(Panel p, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(p.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(p.Width - radius * 2, p.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, p.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            p.Region = new Region(path);
        }

        // ═══════════════════════════════════════
        //  繪圖：優先用圖片，沒圖片則 GDI+ 備用
        // ═══════════════════════════════════════
        private void Cell_Paint(object sender, PaintEventArgs e)
        {
            var cell = (Panel)sender;
            int idx = (int)cell.Tag;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int w = cell.Width, h = cell.Height;
            int pad = 15;
            string val = board[idx];

            if (val == "X")
            {
                if (imgX != null)
                    g.DrawImage(imgX, pad, pad, w - pad * 2, h - pad * 2);
                else
                {
                    using var pen = new Pen(COLOR_X, 8f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
                    g.DrawLine(pen, pad, pad, w - pad, h - pad);
                    g.DrawLine(pen, w - pad, pad, pad, h - pad);
                }
            }
            else if (val == "O")
            {
                if (imgO != null)
                    g.DrawImage(imgO, pad, pad, w - pad * 2, h - pad * 2);
                else
                {
                    using var pen = new Pen(COLOR_O, 8f);
                    g.DrawEllipse(pen, pad, pad, w - pad * 2, h - pad * 2);
                }
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

            var timer = new System.Windows.Forms.Timer { Interval = 400 };
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            imgX?.Dispose();
            imgO?.Dispose();
            base.OnFormClosed(e);
        }
    }
}