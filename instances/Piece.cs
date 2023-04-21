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

        protected const int PieceWidth = 25, PieceHeight = 25; 

        public Piece(Texture2D piece)
        {
            this.piece = piece;
            this.pieceRect = new Rectangle(50, 50, PieceWidth, PieceHeight);
        }



        public void Update()
        {
            mouse = Mouse.GetState();

            if (this.isMouseOnPiece(mouse)) {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    pieceRect.X += 5;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(piece, pieceRect, Color.White);
        }

        protected Boolean isMouseOnPiece(MouseState mouse)
        {
            return (mouse.X >= pieceRect.X && mouse.X < pieceRect.X + PieceWidth) &&
                (mouse.Y >= pieceRect.Y && mouse.Y < pieceRect.Y + PieceHeight);
        }

        protected void highlightBorder()
        {
            
        }
    }
}
