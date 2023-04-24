using ChineseChessGame.constants;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChessGame.instances
{
    internal class Chariot : Piece
    {
        public Chariot(Texture2D piece, Texture2D border, Team team)
           : base(piece, border, team)
        { }

        protected override void assignValidMoves(Piece[,] board)
        {
            this.validMoves = new List<int[]>();
            
            for (int i = X + 1; i <= 8; i++)
            {
                if (this.isValidMove(board, i, Y))
                {
                    this.validMoves.Add(new int[] { i, Y });
                }
                if (board[Y, i] is not null) break;
            }
            
            for (int i = X - 1; i >= 0; i--)
            {
                if(this.isValidMove(board,i,Y))
                {
                    this.validMoves.Add(new int[] { i, Y });
                }
                if (board[Y,i] is not null) break;
            }

            
            for (int i = Y + 1; i <= 9; i++)
            {
                if (this.isValidMove(board, X, i))
                {
                    this.validMoves.Add(new int[] { X, i });
                }
                if (board[i, X] is not null) break;
            }

            
            for (int i = Y - 1; i >= 0; i--)
            {
                if (this.isValidMove(board, X, i))
                {
                    this.validMoves.Add(new int[] { X, i });
                }
                if (board[i, X] is not null) break;
            }
        
        }
    }
}
