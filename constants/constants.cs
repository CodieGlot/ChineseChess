using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseChessGame.instances;
using Microsoft.Xna.Framework;

namespace ChineseChessGame.constants
{

    internal static class WINDOW
    {
        public static string title = "Chinese Chess Game";

        public static int WindowWidth = 1600, WindowHeight = 900;
        public static Color LineColor = Color.DarkBlue;

        public static string banner = "Chinese Chess Game UwU";
    }
    internal static class BOARD
    {
        public static int BoardMarginLeft = 550, BoardMarginTop = 100;

        public static int PieceSize = 60;

        public static int CellGap = 80;

        public static int BoardWidth = CellGap * 8, BoardHeight = CellGap * 9;

        public static int BorderSize = 66, FrameSize = 20;
        public static Color BorderColor = Color.LightYellow, AvailPosColor = Color.Green, SelectedColor = Color.Red;
    }

    internal static class BUTTON
    {
        public static int SwitchBgmX = WINDOW.WindowWidth - 50, SwitchBgmY = WINDOW.WindowHeight - 100;

        public static int StopBgmX = WINDOW.WindowWidth - 50, StopBgmY = WINDOW.WindowHeight - 50;

        public static int UndoMoveX = WINDOW.WindowWidth - 50, UndoMoveY = WINDOW.WindowHeight - 150;

        public static int ResetGameX = WINDOW.WindowWidth - 50, ResetGameY = 330;

        public static int ButtonSize = 30;
    }

    public enum Team {
        RED,
        BLACK
    }
    internal class Turn
    {
        public int[] start;
        public int[] end;
        public Piece deletedPiece;

        public Turn(int[] start, int[] end, Piece deletedPiece)
        {
            this.start = start;
            this.end = end;
            this.deletedPiece = deletedPiece;
        }
    }
}
