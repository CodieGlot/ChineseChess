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

        private MouseState oldMouse;
        private Boolean hasClicked;

        private Song bgm;

        private Texture2D background;
        private Rectangle backgroundRect;

        private SpriteFont textFont;

        private Texture2D line;
        private Texture2D border;

        private Vector2 bannerPos;

        private Piece[,] board;
        private Team turn = Team.RED;

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
            graphics.PreferredBackBufferWidth = WINDOW.WindowWidth;
            graphics.PreferredBackBufferHeight = WINDOW.WindowHeight;
            graphics.ApplyChanges();

            // WINDOW SETUP
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;
            Window.Title = WINDOW.title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>("textures/background");
            backgroundRect = new Rectangle(0, 0, WINDOW.WindowWidth, WINDOW.WindowHeight);

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
            if (bannerPos.X > WINDOW.WindowWidth) bannerPos.X = 0;

            this.hasClicked = false;
            
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed)
            {
                hasClicked = true;
            }
            oldMouse = mouse;

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

            spriteBatch.DrawString(textFont, WINDOW.banner, bannerPos, Color.White);

            this.drawBoard();

            //End Drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void initBoard()
        {
            board = new Piece[10,9];

            // CHANGE TO CHILD CLASSES WHEN DONE

            board[0, 0] = new Piece(Content.Load<Texture2D>("pieces/chariot-black"), border, Team.BLACK);
            board[0, 1] = new Piece(Content.Load<Texture2D>("pieces/horse-black"), border, Team.BLACK);
            board[0, 2] = new Piece(Content.Load<Texture2D>("pieces/elephant-black"), border, Team.BLACK);
            board[0, 3] = new Piece(Content.Load<Texture2D>("pieces/advisor-black"), border, Team.BLACK);
            board[0, 4] = new Piece(Content.Load<Texture2D>("pieces/general-black"), border, Team.BLACK);
            board[0, 5] = new Piece(Content.Load<Texture2D>("pieces/advisor-black"), border, Team.BLACK);
            board[0, 6] = new Piece(Content.Load<Texture2D>("pieces/elephant-black"), border, Team.BLACK);
            board[0, 7] = new Piece(Content.Load<Texture2D>("pieces/horse-black"), border, Team.BLACK);
            board[0, 8] = new Piece(Content.Load<Texture2D>("pieces/chariot-black"), border, Team.BLACK);
            board[2, 1] = new Piece(Content.Load<Texture2D>("pieces/cannon-black"), border, Team.BLACK);
            board[2, 7] = new Piece(Content.Load<Texture2D>("pieces/cannon-black"), border, Team.BLACK);
            board[3, 0] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 2] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 4] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 6] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 8] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);

            board[9, 0] = new Piece(Content.Load<Texture2D>("pieces/chariot-red"), border, Team.RED);
            board[9, 1] = new Piece(Content.Load<Texture2D>("pieces/horse-red"), border, Team.RED);
            board[9, 2] = new Piece(Content.Load<Texture2D>("pieces/elephant-red"), border, Team.RED);
            board[9, 3] = new Piece(Content.Load<Texture2D>("pieces/advisor-red"), border, Team.RED);
            board[9, 4] = new Piece(Content.Load<Texture2D>("pieces/general-red"), border, Team.RED);
            board[9, 5] = new Piece(Content.Load<Texture2D>("pieces/advisor-red"), border, Team.RED);
            board[9, 6] = new Piece(Content.Load<Texture2D>("pieces/elephant-red"), border, Team.RED);
            board[9, 7] = new Piece(Content.Load<Texture2D>("pieces/horse-red"), border, Team.RED);
            board[9, 8] = new Piece(Content.Load<Texture2D>("pieces/chariot-red"), border, Team.RED);
            board[7, 1] = new Piece(Content.Load<Texture2D>("pieces/cannon-red"), border, Team.RED);
            board[7, 7] = new Piece(Content.Load<Texture2D>("pieces/cannon-red"), border, Team.RED);
            board[6, 0] = new Soldier(Content.Load<Texture2D>("pieces/soldier-red"), border, Team.RED);
            board[6, 2] = new Soldier(Content.Load<Texture2D>("pieces/soldier-red"), border, Team.RED);
            board[6, 4] = new Soldier(Content.Load<Texture2D>("pieces/soldier-red"), border, Team.RED);
            board[6, 6] = new Soldier(Content.Load<Texture2D>("pieces/soldier-red"), border, Team.RED);
            board[6, 8] = new Soldier(Content.Load<Texture2D>("pieces/soldier-red"), border, Team.RED);
        }

        private void updateBoard()
        {
            for (int y = 0; y != 10; y++)
            {
                for (int x = 0; x != 9; x++)
                {
                    if (board[y, x] is not null)
                    {
                        board[y, x].Update(mouse, hasClicked, turn, board, x, y);
                    }
                }
            }
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

            for (int y = 0; y != 10; y++)
            {
                for (int x = 0; x != 9; x++)
                {
                    if (board[y, x] is not null)
                    {
                        board[y, x].DrawPieceBorder(spriteBatch);
                    }
                }
            }
        }

        private void drawBoardBorder()
        {
            for (int i = 0; i != 10; i++)
            {
                int x = BOARD.BoardMarginLeft, y = BOARD.BoardMarginTop + BOARD.CellGap * i;
                this.drawBoardLine(spriteBatch, new Vector2(x, y), new Vector2(x + BOARD.BoardWidth, y));
            }

            for (int i = 0; i != 9; i++)
            {
                int x = BOARD.BoardMarginLeft + BOARD.CellGap * i; 
                int y1 = BOARD.BoardMarginTop, y2 = BOARD.BoardMarginTop + BOARD.CellGap * 5;

                if (i == 0 || i == 8) this.drawBoardLine(spriteBatch, new Vector2(x, y1), new Vector2(x, y1 + 5 * BOARD.CellGap));
                else this.drawBoardLine(spriteBatch, new Vector2(x, y1), new Vector2(x, y1 + 4 * BOARD.CellGap));

                this.drawBoardLine(spriteBatch, new Vector2(x, y2), new Vector2(x, y2 + 4 * BOARD.CellGap));
            }

            int startX, startY, endX, endY;

            startX = BOARD.BoardMarginLeft + BOARD.CellGap * 3;
            startY = BOARD.BoardMarginTop;
            endX = BOARD.BoardMarginLeft + BOARD.CellGap * 5;
            endY = BOARD.BoardMarginTop + BOARD.CellGap * 2;
            this.drawBoardLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));

            startX = BOARD.BoardMarginLeft + BOARD.CellGap * 5;
            startY = BOARD.BoardMarginTop;
            endX = BOARD.BoardMarginLeft + BOARD.CellGap * 3;
            endY = BOARD.BoardMarginTop + BOARD.CellGap * 2;
            this.drawBoardLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));

            startX = BOARD.BoardMarginLeft + BOARD.CellGap * 3;
            startY = BOARD.BoardMarginTop + BOARD.CellGap * 7;
            endX = BOARD.BoardMarginLeft + BOARD.CellGap * 5;
            endY = BOARD.BoardMarginTop + BOARD.CellGap * 9;
            this.drawBoardLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));

            startX = BOARD.BoardMarginLeft + BOARD.CellGap * 5;
            startY = BOARD.BoardMarginTop + BOARD.CellGap * 7;
            endX = BOARD.BoardMarginLeft + BOARD.CellGap * 3;
            endY = BOARD.BoardMarginTop + BOARD.CellGap * 9;
            this.drawBoardLine(spriteBatch, new Vector2(startX, startY), new Vector2(endX, endY));
        }

        public void drawBoardLine(SpriteBatch sb, Vector2 start, Vector2 end)
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
                WINDOW.LineColor,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }
    }
}