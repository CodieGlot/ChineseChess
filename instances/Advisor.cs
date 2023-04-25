using ChineseChessGame.constants;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChessGame.instances
{
    internal class Advisor : Piece
    {
        public Advisor(Texture2D piece, Texture2D border, Team team)
           : base(piece, border, team)
        { }

        protected override void assignValidMoves(Piece[,] board)
        {
            this.validMoves = new List<int[]>();

            if (X < 5)
            {

                if (this.isValidMove(board, X + 1, Y + 1))
                {
                    if(this.isValidMove(board, X - 1, Y - 1))
                    {
                        this.validMoves.Add(new int[] { X - 1, Y - 1 });
                    }
                    if(this.isValidMove(board,X-1,Y+1))
                    {
                        this.validMoves.Add(new int[] { X - 1, Y + 1 });
                    }
                }
            {

                }
                if (this.isValidMove(board, X + 1, Y - 1))
                {
                    this.validMoves.Add(new int[] { X + 1, Y - 1 });
                }
            }

            if (X > 3)
            {
                if (this.isValidMove(board, X - 1, Y - 1))
                {
                    this.validMoves.Add(new int[] { X - 1, Y - 1 });
                }
                if (this.isValidMove(board, X - 1, Y + 1))
                {
                    this.validMoves.Add(new int[] { X - 1, Y + 1 });
                }
            }
        } 
    }
}
