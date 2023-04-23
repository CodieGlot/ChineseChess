using ChineseChessGame.constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChessGame.instances
{
    internal class Soldier : Piece
    {
        public Soldier(Texture2D piece, Texture2D border, Team team)
           : base(piece, border, team)
        { }

        protected override void assignValidMoves(Piece[,] board)
        {
            this.validMoves = new List<int[]>();
 
            if (this.team == Team.BLACK)
            {
                if (this.isValidMove(board, X, Y + 1))
                {
                    this.validMoves.Add(new int[] { X, Y + 1 });
                }

                if (this.Y > 4)
                {
                    if (this.isValidMove(board, X - 1, Y))
                    {
                        this.validMoves.Add(new int[] { X - 1, Y });
                    }
                    if (this.isValidMove(board, X + 1, Y))
                    {
                        this.validMoves.Add(new int[] { X + 1, Y });
                    }
                }             
            }
            else
            {
                if (this.isValidMove(board, X, Y - 1))
                {
                    this.validMoves.Add(new int[] { X, Y - 1 });
                }
                
                if (this.Y <= 4)
                {
                    if (this.isValidMove(board, X - 1, Y))
                    {
                        this.validMoves.Add(new int[] { X - 1, Y });
                    }
                    if (this.isValidMove(board, X + 1, Y))
                    {
                        this.validMoves.Add(new int[] { X + 1, Y });
                    }
                }
            }
        }
    }
}
