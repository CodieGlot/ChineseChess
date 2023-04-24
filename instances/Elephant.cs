using ChineseChessGame.constants;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChessGame.instances
{
    internal class Elephant : Piece
    {
        public Elephant(Texture2D piece, Texture2D border, Team team)
           : base(piece, border, team)
        { }

        protected override void assignValidMoves(Piece[,] board)
        {
            this.validMoves = new List<int[]>();

            if(this.team==Team.BLACK)
            {
                if(isValidMove(board,X-2,Y-2))
                {
                    if(board[Y - 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y - 2 });
                }
                
                if (isValidMove(board, X + 2, Y-2))
                {
                    if (board[Y - 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y - 2 });
                }

                if(Y<4&&isValidMove(board,X-2,Y+2))
                {
                    if (board[Y + 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y + 2 });
                }

                if (Y < 4 && isValidMove(board, X + 2, Y + 2))
                {
                    if (board[Y + 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y + 2 });
                }
            }
            
            else
            {
                if (isValidMove(board, X - 2, Y + 2))
                {
                    if (board[Y + 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y - 2 });
                }

                if (isValidMove(board, X + 2, Y + 2))
                {
                    if (board[Y + 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y + 2 });
                }

                if (Y > 5 && isValidMove(board, X - 2, Y - 2))
                {
                    if (board[Y - 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y - 2 });
                }

                if (Y > 5 && isValidMove(board, X + 2, Y - 2))
                {
                    if (board[Y - 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y - 2 });
                }
            }
        }
    }
}
