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

            if(this.team==Team.RED)
            {
                if(X==4 && Y==8)
                {
                    if(this.isValidMove(board,X+1,Y+1)) this.validMoves.Add(new int[] {X+1,Y+1});
                    if (this.isValidMove(board, X + 1, Y - 1)) this.validMoves.Add(new int[] { X + 1, Y - 1 });
                    if (this.isValidMove(board, X - 1, Y - 1)) this.validMoves.Add(new int[] { X - 1, Y - 1 });
                    if (this.isValidMove(board, X - 1, Y + 1)) this.validMoves.Add(new int[] { X - 1, Y + 1 });
                }
                else
                {
                    if (this.isValidMove(board, 4, 8)) this.validMoves.Add(new int[] { 4, 8 });
                }
            }
            else
            {
                if (X == 4 && Y == 1)
                {
                    if (this.isValidMove(board, X + 1, Y + 1)) this.validMoves.Add(new int[] { X + 1, Y + 1 });
                    if (this.isValidMove(board, X + 1, Y - 1)) this.validMoves.Add(new int[] { X + 1, Y - 1 });
                    if (this.isValidMove(board, X - 1, Y - 1)) this.validMoves.Add(new int[] { X - 1, Y - 1 });
                    if (this.isValidMove(board, X - 1, Y + 1)) this.validMoves.Add(new int[] { X - 1, Y + 1 });
                }
                else
                {
                    if (this.isValidMove(board, 4, 1)) this.validMoves.Add(new int[] { 4, 1 });
                }
            }
        } 
    }
}
