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
            this.pieceRect = new Rectangle(0, 0, board_constants.PieceSize, board_constants.PieceSize);

            this.border = border;
            this.borderRect = new Rectangle(0, 0, board_constants.BorderSize, board_constants.BorderSize);

            this.pieceRect.X = coords[0] - board_constants.PieceSize / 2;
            this.pieceRect.Y = coords[1] - board_constants.PieceSize / 2;
        }

        public void Update(MouseState mouse)
        {

            if (this.isMouseOnPiece(mouse)) {

                this.hasHighlightBorder = true;
                this.locateBorder();

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
                spriteBatch.Draw(border, borderRect, board_constants.BorderColor);
            }

            spriteBatch.Draw(piece, pieceRect, Color.White);
        }

        protected Boolean isMouseOnPiece(MouseState mouse)
        {
            return (mouse.X >= pieceRect.X && mouse.X < pieceRect.X + board_constants.PieceSize) &&
                (mouse.Y >= pieceRect.Y && mouse.Y < pieceRect.Y + board_constants.PieceSize);
        }

        void locateBorder()
        {
            this.borderRect.X = this.pieceRect.X - 2;
            this.borderRect.Y = this.pieceRect.Y - 2;
        }
    }
}
