using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ChineseChessGame.constants
{

    internal static class window_constants
    {
        public static string title = "Chinese Chess Game";

        public static int WindowWidth = 1600, WindowHeight = 900;
        public static Color LineColor = Color.DarkBlue;

        public static string banner = "Chinese Chess Game UwU";
    }
    internal static class board_constants
    {
        public static int BoardMarginLeft = 550, BoardMarginTop = 100;

        public static int PieceSize = 60;
        public static int PiecesGap = 80;

        public static int BoardWidth = PiecesGap * 8, BoardHeight = PiecesGap * 9;

        public static int BorderSize = 64;
        public static Color BorderColor = Color.Green;
    }
}
