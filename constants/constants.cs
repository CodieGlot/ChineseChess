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

    internal static class BGM
    {
        public static int SwitchBgmX = WINDOW.WindowWidth - 50, SwitchBgmY = WINDOW.WindowHeight - 100;

        public static int StopBgmX = WINDOW.WindowWidth - 50, StopBgmY = WINDOW.WindowHeight - 50;   

        public static int ButtonSize = 30;
    }

    public enum Team {
        RED,
        BLACK
    }
    internal class Turn
    {
        int[] start;
        int[] end;
        Piece piece;

        public Turn(int[] start, int[] end, Piece piece = null)
        {
            this.start = start;
            this.end = end;
            this.piece = piece;
        }
    }
}
