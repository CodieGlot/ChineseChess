using ChineseChessGame.constants;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChessGame.instances
{
    internal class Cannon : Piece
    {
        public Cannon(Texture2D piece, Texture2D border, Team team)
           : base(piece, border, team)
        { }

        // Mot nuoc di la hop le voi quan phao neu no chua co quan nao dung truoc do, hoac giua o do voi phao co dung 1 quan khac
        protected override void assignValidMoves(Piece[,] board)
        {
            this.validMoves = new List<int[]>();
            bool flag = true;
            for(int i=X+1;i<=8;i++)
            {
                if(flag)
                {
                    if (board[Y, i] is null) this.validMoves.Add(new int[] { i, Y });
                    else flag = false;
                }
                else
                {
                    if (board[Y,i] is not null && this.isValidMove(board, i, Y))
                    {
                        this.validMoves.Add(new int[] { i, Y });
                        break;
                    }
                }
            }
            flag = true;
            for (int i = X - 1; i >= 0; i--)
            {
                if (flag)
                {
                    if (board[Y, i] is null) this.validMoves.Add(new int[] { i, Y });
                    else flag = false;
                }
                else
                {
                    if (board[Y, i] is not null && this.isValidMove(board, i, Y))
                    {
                        this.validMoves.Add(new int[] { i, Y });
                        break;
                    }
                }
            }

            flag = true;
            for (int i = Y + 1; i <= 9; i++)
            {
                if (flag)
                {
                    if (board[i, X] is null) this.validMoves.Add(new int[] { X, i });
                    else flag = false;
                }
                else
                {
                    if (board[i, X] is not null && this.isValidMove(board, X, i))
                    {
                        this.validMoves.Add(new int[] { X, i });
                        break;
                    }
                }
            }

            flag = true;
            for (int i = Y - 1; i >= 0; i--)
            {
                if (flag)
                {
                    if (board[i, X] is null) this.validMoves.Add(new int[] { X, i });
                    else flag = false;
                }
                else
                {
                    if (board[i, X] is not null && this.isValidMove(board, X, i))
                    {
                        this.validMoves.Add(new int[] { X, i });
                        break;
                    }
                }
            }
        }
    }
}
