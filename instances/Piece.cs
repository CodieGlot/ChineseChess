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

        protected List<int[]> validMoves;

        protected Texture2D border;
        protected Rectangle borderRect;

        protected Boolean hasHighlightBorder = false;
        protected Boolean isSelected = false;

        protected int X, Y;
        protected Team team;

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
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(piece, pieceRect, Color.White);

            if (this.isSelected)
            {
                this.drawPieceBorder(spriteBatch, BOARD.SelectedColor);
                this.drawValidMoves(spriteBatch, BOARD.AvailPosColor);
            }
            else if (this.hasHighlightBorder)
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
        protected void drawValidMoves(SpriteBatch sb, Color color)
        {
            if (this.validMoves is null) return;

            foreach (int[] move in this.validMoves)
            {
                int[] coords = this.getPieceCoords(move[0], move[1]);
                this.pieceRect.X = coords[0];
                this.pieceRect.Y = coords[1];
                sb.Draw(piece, pieceRect, Color.White * 0.5f);

                this.assignBorderCoords(move[0], move[1]);
                this.drawPieceBorder(sb, BOARD.AvailPosColor);
            }
        }

        protected Boolean isValidMove(Piece[,] board, int x, int y)
        {
            if (x < 0 || x > 8 || y < 0 || Y > 9) return false;

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
    }
}
