using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseChessGame.constants;

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ChineseChessGame.instances
{
    internal class Piece
    {
        protected Texture2D piece;
        protected Rectangle pieceRect;

        public List<int[]> validMoves;

        protected Texture2D border;
        protected Rectangle borderRect;

        protected Boolean hasHighlightBorder = false;
        public Boolean isSelected = false;

        protected int X, Y;
        public Team team;

        public Piece(Texture2D piece, Texture2D border, Team team)
        {
            this.piece = piece;
            this.pieceRect = new Rectangle(0, 0, BOARD.PieceSize, BOARD.PieceSize);

            this.border = border;
            this.borderRect = new Rectangle(0, 0, BOARD.BorderSize, BOARD.BorderSize);

            this.team = team;
        }

        public void Update(MouseState mouse, Boolean hasClicked, Team turn, Piece[,] board, int x, int y)
        {
            this.X = x;
            this.Y = y;

            int[] pieceCoords = this.getPieceCoords(x, y);
            this.pieceRect.X = pieceCoords[0];
            this.pieceRect.Y = pieceCoords[1];

            this.assignValidMoves(board);
            this.limitValidMoves(board);
            this.assignBorderCoords(this.X, this.Y);

            if (this.team == turn && this.isMouseOnPiece(mouse))
            {
                if (!hasClicked && mouse.LeftButton == ButtonState.Pressed)
                {
                    if (!this.isSelected)
                    {
                        int[] coords = this.getSelectedPieceCoords(board);
                        if (coords is not null)
                        {
                            board[coords[1], coords[0]].isSelected = false;
                        }
                    }

                    this.isSelected = !this.isSelected;
                }
                
                if (!this.isSelected)
                {
                    this.hasHighlightBorder = true;
                }
            }
            else
            {
                this.hasHighlightBorder = false;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(piece, pieceRect, Color.White);
        }

        public void DrawPieceBorder(SpriteBatch sb)
        {
            if (this.isSelected)
            {
                this.drawPieceBorder(sb, BOARD.SelectedColor);
                this.drawValidMoves(sb);
            }
            else if (this.hasHighlightBorder)
            {
                this.drawPieceBorder(sb, BOARD.BorderColor);
            }
        }
        protected Boolean isMouseOnPiece(MouseState mouse)
        {
            return (mouse.X >= pieceRect.X && mouse.X < pieceRect.X + BOARD.PieceSize) &&
                (mouse.Y >= pieceRect.Y && mouse.Y < pieceRect.Y + BOARD.PieceSize);
        }

        protected int[] getPositionClicked(MouseState mouse)
        {
            int coordX = mouse.X - BOARD.BoardMarginLeft, coordY = mouse.Y - BOARD.BoardMarginTop;
            int x, y;
            int remX, remY;

            if (coordX < 0)
            {
                if (coordX < -BOARD.BorderSize / 2) return null;
                else x = 0;
            }

            if (coordX >= BOARD.BoardWidth)
            {
                if (coordX > BOARD.BoardWidth + BOARD.BorderSize / 2) return null;
                else x = 8;
            }

            if (coordY < 0)
            {
                if (coordY < -BOARD.BorderSize / 2) return null;
                else y = 0;
            }

            if (coordY >= BOARD.BoardHeight)
            {
                if (coordY > BOARD.BoardHeight + BOARD.BorderSize / 2) return null;
                else y = 9;
            }

            x = Math.DivRem(coordX, BOARD.CellGap, out remX);
            y = Math.DivRem(coordY, BOARD.CellGap, out remY);

            if (remX >= BOARD.CellGap - BOARD.BorderSize / 2) x++;
            else if (remX > BOARD.BorderSize / 2) return null;

            if (remY >= BOARD.CellGap - BOARD.BorderSize / 2) y++;
            else if (remY > BOARD.BorderSize / 2) return null;

            return new int[] { x, y };
        }
        public int[] getValidMoveClicked(MouseState mouse, Boolean hasClicked)
        {
            if (!hasClicked && mouse.LeftButton == ButtonState.Pressed && this.isSelected)
            {
                int[] coords = this.getPositionClicked(mouse);

                if (coords is null) return null;

                foreach (int[] move in this.validMoves)
                {
                    if (coords[0] == move[0] && coords[1] == move[1])
                    {
                        return coords;
                    }
                }
            }

            return null;
        }

        protected int[] getSelectedPieceCoords(Piece[,] board)
        {
            for (int y = 0; y != 10; y++)
            {
                for (int x = 0; x != 9; x++)
                {
                    if (board[y, x] is not null && board[y, x].isSelected)
                    {
                        return new int[] { x, y };
                    }
                }
            }
            return null;
        }
        protected int[] getPieceCoords(int x, int y)
        {
            int cordX = BOARD.BoardMarginLeft + BOARD.CellGap * x - BOARD.PieceSize / 2;
            int cordY = BOARD.BoardMarginTop + BOARD.CellGap * y - BOARD.PieceSize / 2;

            return new int[] { cordX, cordY };
        }
        protected void assignBorderCoords(int x, int y)
        {
            int[] coords = this.getPieceCoords(x, y);

            this.borderRect.X = coords[0] - 5;
            this.borderRect.Y = coords[1] - 5;
        }

        protected virtual void assignValidMoves(Piece[,] board) { }
        protected void drawValidMoves(SpriteBatch sb)
        {
            if (this.validMoves is null) return;

            foreach (int[] move in this.validMoves)
            {
                this.assignBorderCoords(move[0], move[1]);
                this.drawPieceBorder(sb, BOARD.AvailPosColor);
            }
        }

        protected Boolean isValidMove(Piece[,] board, int x, int y)
        {
            if (x < 0 || x > 8 || y < 0 || y > 9) return false;

            return board[y, x] is null || (this.team != board[y, x].team);
        }
         
        protected void drawPieceBorder(SpriteBatch sb, Color color)
        {
            int x, y;

            x = this.borderRect.X; y = this.borderRect.Y;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x + BOARD.FrameSize, y), color);
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y + BOARD.FrameSize), color);

            x = this.borderRect.X; y = this.borderRect.Y + BOARD.BorderSize;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x + BOARD.FrameSize, y), color);
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y - BOARD.FrameSize), color);

            x = this.borderRect.X + BOARD.BorderSize; y = this.borderRect.Y;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x - BOARD.FrameSize, y), color);
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y + BOARD.FrameSize), color);

            x = this.borderRect.X + BOARD.BorderSize; y = this.borderRect.Y + BOARD.BorderSize;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x - BOARD.FrameSize, y), color);
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y - BOARD.FrameSize), color);
        }
        protected void drawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;

            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            sb.Draw(this.border,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    3),
                null,
                color,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }
        // Quet ban co kiem tra xem co quan nao cua team dich dang check general hay ko
        // neu co, thu tat ca cac validMove cua Piece hien tai
        // neu validMove do van lam cho tuong bi checked thi loai bo
        public virtual Boolean isGeneral()
        {
            return false;
        }

        protected Boolean hasCheckedMate(Piece[,] board, Team team)
        {
            // ta se kiem tra tat ca valid Move cua Piece
            // neu ton tai mot validMove nao do ma doi tuong la general thi return true
            // khong thi return false
            if (this is null || this.validMoves is null) return false;
            for (var i = 0; i < this.validMoves.Count; i++)
            {
                int x = this.validMoves[i][0];
                int y = this.validMoves[i][1];
                if (board[y, x] is not null && board[y, x].isGeneral() && board[y,x].team!=this.team)
                {
                    return true;
                }
            }
            return false;
        }

        protected Piece[,] boardCloning(Piece[,] board)
        {
            Piece[,] clone = new Piece[10, 9];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                    if (board[i, j] is not null) clone[i, j] = (Piece) board[i, j].MemberwiseClone();
            return clone;

        }
        protected void limitValidMoves(Piece[,] board)
        {
            
            //if (board[Y, X] == null) return;
            for(int i=0;i< this.validMoves.Count;++i)
            {
                bool flag = false;
                Piece[,] clone = boardCloning(board);
                int m= this.validMoves[i][0];
                int n = this.validMoves[i][1];
                //clone[Y, X].assignValidMoves(clone);
                clone[n, m] = clone[Y, X].MemberwiseClone() as Piece;
                clone[Y, X] = null;
                for(int j=0;j<10;j++)
                {
                    
                    for(int k=0;k<9;k++)
                    {
                        if (clone[j, k] is not null) clone[j, k].assignValidMoves(clone);
                        if (clone[j,k] is not null && clone[j, k].hasCheckedMate(clone, clone[j,k].team) && clone[j,k].team != this.team)
                        {       
                            flag = true;
                            this.validMoves.RemoveAt(i);
                            --i;
                            break;
                        }
                    }
                    if (flag) break;
                }
                clone[Y, X] = (Piece) clone[n,m].MemberwiseClone();
                clone[n, m] = null;
            }
        }
        
    }
}
