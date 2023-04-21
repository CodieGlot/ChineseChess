using ChineseChessGame.instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChineseChessGame
{
    public class ChineseChess : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private const int WindowWidth = 1600, WindowHeight = 900;

        public KeyboardState kb;
        public MouseState mouse;

        private Texture2D background;
        private Rectangle backgroundRect;

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

            piece = new Piece(Content.Load<Texture2D>("sprites/player"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Escape)) this.Exit();

            piece.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Start

            spriteBatch.Draw(background, backgroundRect, Color.DarkGray);

            piece.Draw(spriteBatch);

            //End
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}