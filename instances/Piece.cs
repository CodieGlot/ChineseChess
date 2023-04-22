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

namespace ChineseChessGame.instances
{
    internal class Piece
    {
        protected Texture2D piece;
        protected Rectangle pieceRect;

        protected int[,] validMoves;

        protected Texture2D border;
        protected Rectangle borderRect;

        protected Boolean hasHighlightBorder = false;
        protected Boolean isSelected = false;

        protected int X, Y;
        protected Team team;

        public Piece(Texture2D piece, Texture2D border, int x, int y, Team team)
        {
            this.piece = piece;
            this.pieceRect = new Rectangle(0, 0, BOARD.PieceSize, BOARD.PieceSize);

            this.border = border;
            this.borderRect = new Rectangle(0, 0, BOARD.BorderSize, BOARD.BorderSize);

            this.X = x;
            this.Y = y;

            int[] coords = this.getPieceCoords(x, y);
            this.pieceRect.X = coords[0];
            this.pieceRect.Y = coords[1];

            this.team = team;
        }

        public void Update(MouseState mouse, Team turn, Piece[,] board)
        {
            if (this.team == turn && this.isMouseOnPiece(mouse))
            {
                this.hasHighlightBorder = true;
                this.assignBorderCoords(this.X, this.Y);

                if (mouse.LeftButton == ButtonState.Released)
                {
                    if (!this.isSelected)
                    {
                        int[] coords = this.getSelectedPieceCoords(board);
                        if (coords is not null)
                        {
                            board[coords[1], coords[0]].isSelected = false;
                        }

                        this.isSelected = true;
                        this.assignValidMoves(board);
                    }
                    else
                    {
                        this.isSelected = false;
                    }
                }
            }
            else
            {
                this.hasHighlightBorder = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(piece, pieceRect, Color.White);

            
            if (this.hasHighlightBorder)
            {
                this.drawPieceBorder(spriteBatch, BOARD.BorderColor);
            }
        }
        protected Boolean isMouseOnPiece(MouseState mouse)
        {
            return (mouse.X >= pieceRect.X && mouse.X < pieceRect.X + BOARD.PieceSize) &&
                (mouse.Y >= pieceRect.Y && mouse.Y < pieceRect.Y + BOARD.PieceSize);
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
        protected void drawValidMoves(SpriteBatch sb, int[,] validMoves, Color color)
        {
            for (int i = 0; i != this.validMoves.GetLength(0); i++)
            {
                this.assignBorderCoords(validMoves[i, 0], validMoves[i, 1]);
                this.drawPieceBorder(sb, color);
            }
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
    }
}
