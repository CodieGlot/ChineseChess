using ChineseChessGame.instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Runtime.CompilerServices;

namespace ChineseChessGame
{
    public class ChineseChess : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private const int WindowWidth = 1600, WindowHeight = 900;
        private const int BoardMarginLeft = 550, BoardMarginTop = 100;
        private const int PiecesGap = 80;

        private const int BoardWidth = PiecesGap * 8, BoardHeight = PiecesGap * 9;

        private KeyboardState kb;
        private MouseState mouse;

        private Song bgm;

        private Texture2D background;
        private Rectangle backgroundRect;

        private SpriteFont textFont;

        private Texture2D line;

        private const string banner = "Chinese Chess Game UwU";
        private Vector2 bannerPos;

        private Texture2D pieceBorder;

        private Piece piece;

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
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            // WINDOW SETUP
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;
            Window.Title = "Chinese Chess Game";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>("textures/background");
            backgroundRect = new Rectangle(0, 0, WindowWidth, WindowHeight);

            bgm = Content.Load<Song>("audio/bgm");
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;

            line = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            line.SetData<Color>(new[] { Color.White });

            textFont = Content.Load<SpriteFont>("fonts/textFont");
            bannerPos = new Vector2(10, 10);

            pieceBorder = Content.Load<Texture2D>("textures/border");

            this.initPieces();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Escape)) this.Exit();

            bannerPos.X += 2;
            if (bannerPos.X > WindowWidth) bannerPos.X = 0;

            piece.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Start Drawing

            spriteBatch.Draw(background, backgroundRect, Color.DarkGray);

            spriteBatch.DrawString(textFont, banner, bannerPos, Color.White);

            this.drawBoardBorder();

            piece.Draw(spriteBatch);

            //End Drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void initPieces()
        {
            piece = new Piece(Content.Load<Texture2D>("sprites/player"), pieceBorder);
        }

        private void drawBoardBorder()
        {
            for (int i = 0; i != 10; i++)
            {
                int x = BoardMarginLeft, y = BoardMarginTop + PiecesGap * i;
                this.drawLine(spriteBatch, new Vector2(x, y), new Vector2(x + BoardWidth, y));
            }

            for (int i = 0; i != 9; i++)
            {
                int x = BoardMarginLeft + PiecesGap * i, y1 = BoardMarginTop, y2 = BoardMarginTop + PiecesGap * 5;

                if (i == 0 || i == 8) this.drawLine(spriteBatch, new Vector2(x, y1), new Vector2(x, y1 + 5 * PiecesGap));
                else this.drawLine(spriteBatch, new Vector2(x, y1), new Vector2(x, y1 + 4 * PiecesGap));

                this.drawLine(spriteBatch, new Vector2(x, y2), new Vector2(x, y2 + 4 * PiecesGap));
            }
        }

        void drawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;

            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            sb.Draw(this.line,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    5),
                null,
                Color.DarkBlue,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);

        }
    }
}