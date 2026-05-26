# 井字棋 Tic-Tac-Toe

A Windows Forms Tic-Tac-Toe game with an unbeatable Minimax AI opponent.

## 📸 Screenshot

> *(Add screenshot here after running)*

## 🎮 Features

- **Player vs AI** — AI uses Minimax algorithm (plays perfectly)
- **Score tracking** — tracks wins, losses, and draws across rounds
- **Sound effects** — move sound, win fanfare, lose sound, draw sound
- **Dark theme UI** — polished dark interface with hover effects
- **Highlighted win line** — winning cells turn green

## 🕹️ How to Play

1. You play as **X** (red), AI plays as **O** (blue)
2. Click any empty cell to place your mark
3. First to get 3 in a row (horizontal, vertical, or diagonal) wins
4. Click **重新開始** to start a new round

## 🚀 How to Run

### Requirements
- Windows 10/11
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or Visual Studio 2022

### Run with .NET CLI
```bash
cd TicTacToe
dotnet run
```

### Run with Visual Studio
1. Open `TicTacToe.sln`
2. Press `F5`

## 🤖 AI Logic

The AI uses the **Minimax algorithm** — it evaluates all possible future moves and always picks the optimal one. This means the AI is unbeatable; the best outcome for the player is a draw with perfect play.

## 📁 Project Structure

```
TicTacToe/
├── TicTacToe.sln
└── TicTacToe/
    ├── Program.cs          # Entry point
    ├── Form1.cs            # Main game logic + UI
    ├── Form1.Designer.cs   # Form designer stub
    └── TicTacToe.csproj    # Project config
```

## 🛠️ Tech Stack

- C# / .NET 6.0
- Windows Forms (WinForms)
- GDI+ for custom drawing
- `Console.Beep()` for sound effects
