using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseChessGame.constants;

namespace ChineseChessGame.instances
{
    internal class Piece
    {
        protected Texture2D piece;
        protected Rectangle pieceRect;

        protected Texture2D border;
        protected Rectangle borderRect;

        protected Boolean hasHighlightBorder = true;

        public Piece(Texture2D piece, Texture2D border, int[] coords)
        {
            this.piece = piece;
            this.pieceRect = new Rectangle(0, 0, BOARD.PieceSize, BOARD.PieceSize);

            this.border = border;
            this.borderRect = new Rectangle(0, 0, BOARD.BorderSize, BOARD.BorderSize);

            this.pieceRect.X = coords[0] - BOARD.PieceSize / 2;
            this.pieceRect.Y = coords[1] - BOARD.PieceSize / 2;
        }

        public void Update(MouseState mouse)
        {

            if (this.isMouseOnPiece(mouse)) {

                this.hasHighlightBorder = true;
                this.getBorderCoords();

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    // ADD ACTIONS HERE
                }
            }
            else
            {
                this.hasHighlightBorder = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.hasHighlightBorder)
            {
                this.drawPieceBorder(spriteBatch);
            }

            spriteBatch.Draw(piece, pieceRect, Color.White);
        }

        protected Boolean isMouseOnPiece(MouseState mouse)
        {
            return (mouse.X >= pieceRect.X && mouse.X < pieceRect.X + BOARD.PieceSize) &&
                (mouse.Y >= pieceRect.Y && mouse.Y < pieceRect.Y + BOARD.PieceSize);
        }

        protected void getBorderCoords()
        {
            this.borderRect.X = this.pieceRect.X - 3;
            this.borderRect.Y = this.pieceRect.Y - 3;
        }

        protected void drawPieceBorder(SpriteBatch sb)
        {
            int x, y;

            x = this.borderRect.X; y = this.borderRect.Y;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x + BOARD.FrameSize, y));
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y + BOARD.FrameSize));

            x = this.borderRect.X; y = this.borderRect.Y + BOARD.BorderSize;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x + BOARD.FrameSize, y));
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y - BOARD.FrameSize));

            x = this.borderRect.X + BOARD.BorderSize; y = this.borderRect.Y;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x - BOARD.FrameSize, y));
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y + BOARD.FrameSize));

            x = this.borderRect.X + BOARD.BorderSize; y = this.borderRect.Y + BOARD.BorderSize;
            this.drawLine(sb, new Vector2(x, y), new Vector2(x - BOARD.FrameSize, y));
            this.drawLine(sb, new Vector2(x, y), new Vector2(x, y - BOARD.FrameSize));
        }
        protected void drawLine(SpriteBatch sb, Vector2 start, Vector2 end)
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
                BOARD.BorderColor,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }
    }
}
