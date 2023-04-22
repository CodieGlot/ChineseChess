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
        public Soldier(Texture2D piece, Texture2D border, int x, int y, Team team)
           : base(piece, border, x, y, team)
        { }

        protected override void assignValidMoves(Piece[,] board)
        {
            if (this.team == Team.BLACK)
            {
                if (this.Y < 5)
                {
                    this.validMoves = new int[,] { { X, Y + 1 } };
                }
                else
                {
                    if (this.X == 0)
                    {
                        this.validMoves = new int[,] { { X, Y + 1 }, { X + 1, Y } };
                    }
                    else if (this.X == 8)
                    {
                        this.validMoves = new int[,] { { X, Y + 1 }, { X - 1, Y } };
                    }
                    else
                    {
                        this.validMoves = new int[,] { { X, Y + 1 }, { X + 1, Y }, { X - 1, Y } };
                    }
                }
            }
            else
            {
                if (this.Y > 5)
                {
                    this.validMoves = new int[,] { { X, Y - 1 } };
                }
                else
                {
                    if (this.X == 0)
                    {
                        this.validMoves = new int[,] { { X, Y - 1 }, { X + 1, Y } };
                    }
                    else if (this.X == 8)
                    {
                        this.validMoves = new int[,] { { X, Y - 1 }, { X - 1, Y } };
                    }
                    else
                    {
                        this.validMoves = new int[,] { { X, Y - 1 }, { X + 1, Y }, { X - 1, Y } };
                    }
                }
            }
        }
    }
}
