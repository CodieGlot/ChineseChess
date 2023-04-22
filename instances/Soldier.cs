using ChineseChessGame.constants;
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
    internal class Soldier : Piece
    {
        public Soldier(Texture2D piece, Texture2D border, int x, int y, Team team) : base(piece, border, x, y, team)
        { }

        public new void Update(MouseState mouse, Team turn)
        {
            if (this.team == turn && this.isMouseOnPiece(mouse))
            {
                this.hasHighlightBorder = true;
                this.assignBorderCoords(this.X, this.Y);

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (!this.isSelected)
                    {
                        this.isSelected = true;
                        
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

        private void assignValidMoves()
        {

        }
    }
}
