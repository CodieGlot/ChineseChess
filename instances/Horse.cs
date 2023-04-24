using ChineseChessGame.constants;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChessGame.instances
{
    internal class Horse : Piece
    {
        public Horse(Texture2D piece, Texture2D border, Team team)
           : base(piece, border, team)
        { }
        protected override void assignValidMoves(Piece[,] board)
        {
            this.validMoves = new List<int[]>();
            
            if(isValidMove(board,X,Y-1))
            {
                if(isValidMove(board,X+1,Y-2)) this.validMoves.Add(new int[] {X+1,Y-2});
                if (isValidMove(board, X - 1, Y - 2)) this.validMoves.Add(new int[] { X - 1, Y - 2 });
            }

            if(isValidMove(board,X+1,Y))
            {
                if (isValidMove(board, X + 2, Y - 1)) this.validMoves.Add(new int[] { X + 2, Y - 1 });
                if (isValidMove(board, X + 2, Y + 1)) this.validMoves.Add(new int[] { X + 2, Y + 1 });
            }

            if(isValidMove(board,X,Y+1))
            {
                if (isValidMove(board, X + 1, Y + 2)) this.validMoves.Add(new int[] { X + 1, Y + 2 });
                if (isValidMove(board, X - 1, Y + 2)) this.validMoves.Add(new int[] { X - 1, Y + 2 });
            }

            if(isValidMove(board,X-1,Y))
            {
                if (isValidMove(board, X - 2, Y + 1)) this.validMoves.Add(new int[] { X - 2, Y + 1 });
                if (isValidMove(board, X - 2, Y - 1)) this.validMoves.Add(new int[] { X - 2, Y - 1 });
            }
        }
    }
}
