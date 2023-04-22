using ChineseChessGame.instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Runtime.CompilerServices;
using ChineseChessGame.constants;

namespace ChineseChessGame
{
    public class ChineseChess : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState kb;
        private MouseState mouse;

        private Song bgm;

        private Texture2D background;
        private Rectangle backgroundRect;

        private SpriteFont textFont;

        private Texture2D line;
        private Texture2D border;

        private Vector2 bannerPos;

        private Piece[,] board;

        public ChineseChess()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // GRAPHICS SETUP
            graphics.PreferredBackBufferWidth = window_constants.WindowWidth;
            graphics.PreferredBackBufferHeight = window_constants.WindowHeight;
            graphics.ApplyChanges();

            // WINDOW SETUP
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;
            Window.Title = window_constants.title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>("textures/background");
            backgroundRect = new Rectangle(0, 0, window_constants.WindowWidth, window_constants.WindowHeight);

            bgm = Content.Load<Song>("audio/bgm");
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;

            line = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            line.SetData<Color>(new[] { Color.White });

            border = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            border.SetData<Color>(new[] { Color.White });

            textFont = Content.Load<SpriteFont>("fonts/textFont");
            bannerPos = new Vector2(10, 10);

            this.initBoard();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            kb = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (kb.IsKeyDown(Keys.Escape)) this.Exit();

            bannerPos.X += 2;
            if (bannerPos.X > window_constants.WindowWidth) bannerPos.X = 0;

            this.updateBoard();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Start Drawing

            spriteBatch.Draw(background, backgroundRect, Color.DarkGray);
            this.drawBoardBorder();

            spriteBatch.DrawString(textFont, window_constants.banner, bannerPos, Color.White);

            this.drawBoard();

            //End Drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void initBoard()
        {
            board = new Piece[10,9];

            // CHANGE TO CHILD CLASSES WHEN DONE

            board[0, 0] = new Piece(Content.Load<Texture2D>("pieces/chariot-black"), border, locatePieceCoord(0,0));
            board[0, 1] = new Piece(Content.Load<Texture2D>("pieces/horse-black"), border, locatePieceCoord(1, 0));
            board[0, 2] = new Piece(Content.Load<Texture2D>("pieces/elephant-black"), border, locatePieceCoord(2, 0));
            board[0, 3] = new Piece(Content.Load<Texture2D>("pieces/advisor-black"), border, locatePieceCoord(3, 0));
            board[0, 4] = new Piece(Content.Load<Texture2D>("pieces/general-black"), border, locatePieceCoord(4, 0));
            board[0, 5] = new Piece(Content.Load<Texture2D>("pieces/advisor-black"), border, locatePieceCoord(5, 0));
            board[0, 6] = new Piece(Content.Load<Texture2D>("pieces/elephant-black"), border, locatePieceCoord(6, 0));
            board[0, 7] = new Piece(Content.Load<Texture2D>("pieces/horse-black"), border, locatePieceCoord(7, 0));
            board[0, 8] = new Piece(Content.Load<Texture2D>("pieces/chariot-black"), border, locatePieceCoord(8, 0));
            board[2, 1] = new Piece(Content.Load<Texture2D>("pieces/cannon-black"), border, locatePieceCoord(1, 2));
            board[2, 7] = new Piece(Content.Load<Texture2D>("pieces/cannon-black"), border, locatePieceCoord(7, 2));
            board[3, 0] = new Piece(Content.Load<Texture2D>("pieces/soldier-black"), border, locatePieceCoord(0, 3));
            board[3, 2] = new Piece(Content.Load<Texture2D>("pieces/soldier-black"), border, locatePieceCoord(2, 3));
            board[3, 4] = new Piece(Content.Load<Texture2D>("pieces/soldier-black"), border, locatePieceCoord(4, 3));
            board[3, 6] = new Piece(Content.Load<Texture2D>("pieces/soldier-black"), border, locatePieceCoord(6, 3));
            board[3, 8] = new Piece(Content.Load<Texture2D>("pieces/soldier-black"), border, locatePieceCoord(8, 3));

            board[9, 0] = new Piece(Content.Load<Texture2D>("pieces/chariot-red"), border, locatePieceCoord(0, 9));
            board[9, 1] = new Piece(Content.Load<Texture2D>("pieces/horse-red"), border, locatePieceCoord(1, 9));
            board[9, 2] = new Piece(Content.Load<Texture2D>("pieces/elephant-red"), border, locatePieceCoord(2, 9));
            board[9, 3] = new Piece(Content.Load<Texture2D>("pieces/advisor-red"), border, locatePieceCoord(3, 9));
            board[9, 4] = new Piece(Content.Load<Texture2D>("pieces/general-red"), border, locatePieceCoord(4, 9));
            board[9, 5] = new Piece(Content.Load<Texture2D>("pieces/advisor-red"), border, locatePieceCoord(5, 9));
            board[9, 6] = new Piece(Content.Load<Texture2D>("pieces/elephant-red"), border, locatePieceCoord(6, 9));
            board[9, 7] = new Piece(Content.Load<Texture2D>("pieces/horse-red"), border, locatePieceCoord(7, 9));
            board[9, 8] = new Piece(Content.Load<Texture2D>("pieces/chariot-red"), border, locatePieceCoord(8, 9));
            board[7, 1] = new Piece(Content.Load<Texture2D>("pieces/cannon-red"), border, locatePieceCoord(1, 7));
            board[7, 7] = new Piece(Content.Load<Texture2D>("pieces/cannon-red"), border, locatePieceCoord(7, 7));
            board[6, 0] = new Piece(Content.Load<Texture2D>("pieces/soldier-red"), border, locatePieceCoord(0, 6));
            board[6, 2] = new Piece(Content.Load<Texture2D>("pieces/soldier-red"), border, locatePieceCoord(2, 6));
            board[6, 4] = new Piece(Content.Load<Texture2D>("pieces/soldier-red"), border, locatePieceCoord(4, 6));
            board[6, 6] = new Piece(Content.Load<Texture2D>("pieces/soldier-red"), border, locatePieceCoord(6, 6));
            board[6, 8] = new Piece(Content.Load<Texture2D>("pieces/soldier-red"), border, locatePieceCoord(8, 6));
        }

        private void updateBoard()
        {
            for (int y = 0; y != 10; y++)
            {
                for (int x = 0; x != 9; x++)
                {
                    if (board[y, x] is not null)
                    {
                        board[y, x].Update(mouse);
                    }
                }
            }
        }

        private int[] locatePieceCoord(int x, int y)
        {
            int cordX = board_constants.BoardMarginLeft + board_constants.PiecesGap * x;
            int cordY = board_constants.BoardMarginTop + board_constants.PiecesGap * y;

            return new int[] { cordX, cordY };
        }

        private void drawBoard()
        {
            for (int y = 0; y != 10; y++)
            {
                for (int x = 0; x != 9; x++)
                {
                    if (board[y, x] is not null)
                    {
                        board[y, x].Draw(spriteBatch);
                    }
                }
            }
        }

        private void drawBoardBorder()
        {
            for (int i = 0; i != 10; i++)
            {
                int x = board_constants.BoardMarginLeft, y = board_constants.BoardMarginTop + board_constants.PiecesGap * i;
                this.drawLine(spriteBatch, new Vector2(x, y), new Vector2(x + board_constants.BoardWidth, y));
            }

            for (int i = 0; i != 9; i++)
            {
                int x = board_constants.BoardMarginLeft + board_constants.PiecesGap * i; 
                int y1 = board_constants.BoardMarginTop, y2 = board_constants.BoardMarginTop + board_constants.PiecesGap * 5;

                if (i == 0 || i == 8) this.drawLine(spriteBatch, new Vector2(x, y1), new Vector2(x, y1 + 5 * board_constants.PiecesGap));
                else this.drawLine(spriteBatch, new Vector2(x, y1), new Vector2(x, y1 + 4 * board_constants.PiecesGap));

                this.drawLine(spriteBatch, new Vector2(x, y2), new Vector2(x, y2 + 4 * board_constants.PiecesGap));
            }

            int startX, startY, endX, endY;

            startX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 3;
            startY = board_constants.BoardMarginTop;
            endX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 5;
            endY = board_constants.BoardMarginTop + board_constants.PiecesGap * 2;
            this.drawLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));

            startX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 5;
            startY = board_constants.BoardMarginTop;
            endX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 3;
            endY = board_constants.BoardMarginTop + board_constants.PiecesGap * 2;
            this.drawLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));

            startX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 3;
            startY = board_constants.BoardMarginTop + board_constants.PiecesGap * 7;
            endX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 5;
            endY = board_constants.BoardMarginTop + board_constants.PiecesGap * 9;
            this.drawLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));

            startX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 5;
            startY = board_constants.BoardMarginTop + board_constants.PiecesGap * 7;
            endX = board_constants.BoardMarginLeft + board_constants.PiecesGap * 3;
            endY = board_constants.BoardMarginTop + board_constants.PiecesGap * 9;
            this.drawLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));
        }

        public void drawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;

            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            sb.Draw(this.line,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    3),
                null,
                window_constants.LineColor,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);

        } 
    }
}