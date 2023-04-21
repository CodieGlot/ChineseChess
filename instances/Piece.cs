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
    internal class Piece
    {
        protected MouseState mouse;

        protected Texture2D piece;
        protected Rectangle pieceRect;

        protected Texture2D border;
        protected Rectangle borderRect;

        protected const int PieceWidth = 50, PieceHeight = 50;

        protected Boolean hasHighlightBorder = true;

        public Piece(Texture2D piece, Texture2D border)
        {
            this.piece = piece;
            this.pieceRect = new Rectangle(50, 50, PieceWidth, PieceHeight);

            this.border = border;
            this.borderRect = new Rectangle(0, 0, PieceWidth + 4, PieceHeight + 4);
        }

        public void Update()
        {
            mouse = Mouse.GetState();

            if (this.isMouseOnPiece(mouse)) {
                this.hasHighlightBorder = true;
                this.createBorder();

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    pieceRect.X += 5;
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
                spriteBatch.Draw(border, borderRect, Color.White);
            }
            
            spriteBatch.Draw(piece, pieceRect, Color.White);
        }

        protected Boolean isMouseOnPiece(MouseState mouse)
        {
            return (mouse.X >= pieceRect.X && mouse.X < pieceRect.X + PieceWidth) &&
                (mouse.Y >= pieceRect.Y && mouse.Y < pieceRect.Y + PieceHeight);
        }

        protected void createBorder()
        {
            this.borderRect.X = this.pieceRect.X - 2;
            this.borderRect.Y = this.pieceRect.Y - 2;
        }
    }
}
