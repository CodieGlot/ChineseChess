using ChineseChessGame.constants;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChessGame.instances
{
    internal class General : Piece
    {
        public override Boolean isGeneral()
        {
            return true;
        }
        public General(Texture2D piece, Texture2D border, Team team)
           : base(piece, border, team)
        { }
        public Boolean isChecked(Piece[,] board)
        {
            return true;
        }
        public override Boolean SpecialChecked(Piece[,] board)
        {
            for(int j = 0; j != 10; j++)
            {
                for(int i = 0; i != 9; i++)
                {
                    if (board[j, i] is not null && board[j, i].team != this.team && board[j, i].isGeneral())
                    {
                        if (i == this.X)
                        {
                            if (this.team == Team.BLACK)
                            {
                                for (int k = this.Y + 1; k != j; k++)
                                {
                                    if (board[k, i] is not null)
                                    return false;
                                }
                                return true;
                            }
                            else
                            {
                                for (int k = j + 1; k != this.Y; k++)
                                {
                                    if (board[k, i] is not null)
                                    return false;
                                }
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
        protected override void assignValidMoves(Piece[,] board)
        {
            this.validMoves = new List<int[]>();
            if (this.team == Team.BLACK) {
                int XValidMin = 3, XValidMax = 5, YValidMin = 0, YValidMax = 2;
                if (YValidMin <= Y - 1 && Y - 1 <= YValidMax && this.isValidMove(board, X, Y - 1))
                {
                    this.validMoves.Add(new int[] { X, Y - 1 });
                }
                if (YValidMin <= Y + 1 && Y + 1 <= YValidMax && this.isValidMove(board, X, Y + 1))
                {
                    this.validMoves.Add(new int[] { X, Y + 1 });
                }
                if (XValidMin <= X - 1 && X - 1 <= XValidMax && this.isValidMove(board, X - 1, Y))
                {
                    this.validMoves.Add(new int[] { X - 1, Y });
                }
                if (XValidMin <= X + 1 && X + 1 <= XValidMax && this.isValidMove(board, X + 1, Y))
                {
                    this.validMoves.Add(new int[] { X + 1, Y });
                }
            }
            else if(this.team==Team.RED)
            {
                int XValidMin = 3, XValidMax = 5, YValidMin = 7, YValidMax = 9;
                if (YValidMin <= Y - 1 && Y - 1 <= YValidMax && this.isValidMove(board, X, Y - 1))
                {
                    this.validMoves.Add(new int[] { X, Y - 1 });
                }
                if (YValidMin <= Y + 1 && Y + 1 <= YValidMax && this.isValidMove(board, X, Y + 1))
                {
                    this.validMoves.Add(new int[] { X, Y + 1 });
                }
                if (XValidMin <= X - 1 && X - 1 <= XValidMax && this.isValidMove(board, X - 1, Y))
                {
                    this.validMoves.Add(new int[] { X - 1, Y });
                }
                if (XValidMin <= X + 1 && X + 1 <= XValidMax && this.isValidMove(board, X + 1, Y))
                {
                    this.validMoves.Add(new int[] { X + 1, Y });
                }
            }
        }

        
    }
}
