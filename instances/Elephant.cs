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
                if(this.isValidMove(board,X-2,Y-2))
                {
                    if(board[Y - 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y - 2 });
                }
                
                if (this.isValidMove(board, X + 2, Y-2))
                {
                    if (board[Y - 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y - 2 });
                }

                if(this.Y<4&&this.isValidMove(board,X-2,Y+2))
                {
                    if (board[Y + 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y + 2 });
                }

                if (this.Y < 4 && this.isValidMove(board, X + 2, Y + 2))
                {
                    if (board[Y + 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y + 2 });
                }
            }
            
            else
            {
                if (this.isValidMove(board, X - 2, Y + 2))
                {
                    if (board[Y + 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y + 2 });
                }

                if (this.isValidMove(board, X + 2, Y + 2))
                {
                    if (board[Y + 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y + 2 });
                }

                if (this.Y > 5 && this.isValidMove(board, X - 2, Y - 2))
                {
                    if (board[Y - 1, X - 1] is null) this.validMoves.Add(new int[] { X - 2, Y - 2 });
                }

                if (this.Y > 5 && this.isValidMove(board, X + 2, Y - 2))
                {
                    if (board[Y - 1, X + 1] is null) this.validMoves.Add(new int[] { X + 2, Y - 2 });
                }
            }
        }
    }
}
