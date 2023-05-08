using ChineseChessGame.instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Runtime.CompilerServices;
using ChineseChessGame.constants;
using System.Collections.Generic;

namespace ChineseChessGame
{
    public class ChineseChess : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState kb;
        private MouseState mouse;

        private List<Song> Button;
        private int songIndex = 0;

        private Texture2D toggleButton;
        private Rectangle toggleButtonRect;
        private Boolean isButtonOn = true;

        private Texture2D undoMove;
        private Rectangle undoRect;

        private Texture2D resetGame;
        private Rectangle resetGameRect;

        private Texture2D switchButton;
        private Rectangle switchButtonRect;

        private MouseState oldMouse;
        private Boolean hasClicked;

        private Texture2D background;
        private Rectangle backgroundRect;

        private SpriteFont textFont;

        private Texture2D line;
        private Texture2D border;

        private Vector2 bannerPos;

        private string turnText;
        private Vector2 turnPos;
        private Color turnColor;

        private string stateText;
        private Vector2 statePos;
        private Color stateColor;

        private Piece[,] board;

        private Team turn = Team.RED;
        private List<Turn> turnsLog = new List<Turn>();

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

            line = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            line.SetData<Color>(new[] { Color.White });

            border = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            border.SetData<Color>(new[] { Color.White });

            textFont = Content.Load<SpriteFont>("fonts/textFont");
            bannerPos = new Vector2(10, 10);
            turnPos = new Vector2(WINDOW.WindowWidth - 300, 100);
            statePos = new Vector2(WINDOW.WindowWidth - 250, 150);
            
            this.initBoard();
            this.initButtons();
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

            if (this.hasClickedRect(mouse, this.switchButtonRect, hasClicked))
            {
                this.switchButtonAction();
            }
            else if (this.hasClickedRect(mouse, this.toggleButtonRect, hasClicked))
            {
                this.toggleButtonAction();
            }
            else if (this.hasClickedRect(mouse, this.undoRect, hasClicked))
            {
                this.revertToPreviousMove();
            }
            else if (this.hasClickedRect(mouse, this.resetGameRect, hasClicked))
            {
                this.resetGameAction();
            }

            if (turn == Team.RED)
            {
                turnText = "RED TEAM TURN";
                turnColor = Color.DarkRed;
            }
            else
            {
                turnText = "BLACK TEAM TURN";
                turnColor = Color.Black;
            }

            

            stateColor = turnColor;

            this.updateBoard();

            if (!isEndGame(board))
            {
                stateText = "'PLAYING'";
            }
            else
            {
                stateText = "'GAME OVER'";
            }

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
            spriteBatch.DrawString(textFont, turnText, turnPos, turnColor);

            spriteBatch.DrawString(textFont, "Switch Bgm", new Vector2(BUTTON.SwitchBgmX - 160, BUTTON.SwitchBgmY - 10), Color.White);
            spriteBatch.Draw(switchButton, switchButtonRect, Color.White);

            spriteBatch.DrawString(textFont, "Stop Bgm", new Vector2(BUTTON.StopBgmX - 140, BUTTON.StopBgmY - 10), Color.White);
            spriteBatch.Draw(toggleButton, toggleButtonRect, Color.White);

            spriteBatch.DrawString(textFont, "Undo Move", new Vector2(BUTTON.UndoMoveX - 140, BUTTON.UndoMoveY - 10), Color.White);
            spriteBatch.Draw(undoMove, undoRect, Color.White);

            spriteBatch.DrawString(textFont, "Reset Game", new Vector2(BUTTON.ResetGameX - 160, BUTTON.ResetGameY - 10), Color.White);
            spriteBatch.Draw(resetGame, resetGameRect, Color.White);
            spriteBatch.DrawString(textFont, stateText, statePos, stateColor); 
            this.drawBoard();

            //End Drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void initBoard()
        {
            board = new Piece[10,9];

            board[0, 0] = new Chariot(Content.Load<Texture2D>("pieces/chariot-black"), border, Team.BLACK);
            board[0, 1] = new Horse(Content.Load<Texture2D>("pieces/horse-black"), border, Team.BLACK);
            board[0, 2] = new Elephant(Content.Load<Texture2D>("pieces/elephant-black"), border, Team.BLACK);
            board[0, 3] = new Advisor(Content.Load<Texture2D>("pieces/advisor-black"), border, Team.BLACK);
            board[0, 4] = new General(Content.Load<Texture2D>("pieces/general-black"), border, Team.BLACK);
            board[0, 5] = new Advisor(Content.Load<Texture2D>("pieces/advisor-black"), border, Team.BLACK);
            board[0, 6] = new Elephant(Content.Load<Texture2D>("pieces/elephant-black"), border, Team.BLACK);
            board[0, 7] = new Horse(Content.Load<Texture2D>("pieces/horse-black"), border, Team.BLACK);
            board[0, 8] = new Chariot(Content.Load<Texture2D>("pieces/chariot-black"), border, Team.BLACK);
            board[2, 1] = new Cannon(Content.Load<Texture2D>("pieces/cannon-black"), border, Team.BLACK);
            board[2, 7] = new Cannon(Content.Load<Texture2D>("pieces/cannon-black"), border, Team.BLACK);
            board[3, 0] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 2] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 4] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 6] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);
            board[3, 8] = new Soldier(Content.Load<Texture2D>("pieces/soldier-black"), border, Team.BLACK);

            board[9, 0] = new Chariot(Content.Load<Texture2D>("pieces/chariot-red"), border, Team.RED);
            board[9, 1] = new Horse(Content.Load<Texture2D>("pieces/horse-red"), border, Team.RED);
            board[9, 2] = new Elephant(Content.Load<Texture2D>("pieces/elephant-red"), border, Team.RED);
            board[9, 3] = new Advisor(Content.Load<Texture2D>("pieces/advisor-red"), border, Team.RED);
            board[9, 4] = new General(Content.Load<Texture2D>("pieces/general-red"), border, Team.RED);
            board[9, 5] = new Advisor(Content.Load<Texture2D>("pieces/advisor-red"), border, Team.RED);
            board[9, 6] = new Elephant(Content.Load<Texture2D>("pieces/elephant-red"), border, Team.RED);
            board[9, 7] = new Horse(Content.Load<Texture2D>("pieces/horse-red"), border, Team.RED);
            board[9, 8] = new Chariot(Content.Load<Texture2D>("pieces/chariot-red"), border, Team.RED);
            board[7, 1] = new Cannon(Content.Load<Texture2D>("pieces/cannon-red"), border, Team.RED);
            board[7, 7] = new Cannon(Content.Load<Texture2D>("pieces/cannon-red"), border, Team.RED);
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

            for (int y = 0; y != 10; y++)
            {
                for (int x = 0; x != 9; x++)
                {
                    if (board[y, x] is not null)
                    {
                        int[] coords = board[y, x].getValidMoveClicked(mouse, hasClicked);

                        if (coords != null)
                        {
                            turnsLog.Add(new Turn(new int[] { x, y }, coords, board[coords[1], coords[0]]));
                            turn = (turn == Team.RED) ? Team.BLACK : Team.RED;

                            board[y, x].isSelected = false;

                            board[coords[1], coords[0]] = board[y, x];
                            board[y, x] = null;
                        }
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

        private void drawBoardLine(SpriteBatch sb, Vector2 start, Vector2 end)
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

        private Boolean hasClickedRect(MouseState mouse, Rectangle rect, Boolean hasClicked)
        {
            return !hasClicked && (mouse.LeftButton == ButtonState.Pressed) 
                && (mouse.X >= rect.X && mouse.X < rect.X + rect.Width)
                && (mouse.Y >= rect.Y && mouse.Y < rect.Y + rect.Height);
        }
        private void initButtons()
        {
            Button = new List<Song>()
            {
                Content.Load<Song>("audio/Bgm"),
                Content.Load<Song>("audio/Bgm1")
            };

            switchButton = Content.Load<Texture2D>("textures/switch-song");
            switchButtonRect = new Rectangle(BUTTON.SwitchBgmX, BUTTON.SwitchBgmY, BUTTON.ButtonSize, BUTTON.ButtonSize);

            toggleButton = Content.Load<Texture2D>("textures/stop-song");
            toggleButtonRect = new Rectangle(BUTTON.StopBgmX, BUTTON.StopBgmY, BUTTON.ButtonSize, BUTTON.ButtonSize);

            undoMove = Content.Load<Texture2D>("textures/undo-move");
            undoRect = new Rectangle(BUTTON.UndoMoveX, BUTTON.UndoMoveY, BUTTON.ButtonSize, BUTTON.ButtonSize);

            resetGame = Content.Load<Texture2D>("textures/reset-button");
            resetGameRect = new Rectangle(BUTTON.ResetGameX, BUTTON.ResetGameY, BUTTON.ButtonSize, BUTTON.ButtonSize);

            MediaPlayer.Play(Button[songIndex]);
            MediaPlayer.IsRepeating = true;
        }

        private void switchButtonAction()
        {
            this.songIndex++;
            if (this.songIndex == this.Button.Count)
            {
                this.songIndex = 0;
            }

            MediaPlayer.Play(Button[songIndex]);
        }  

        private void toggleButtonAction()
        {
           if (this.isButtonOn)
            {
                MediaPlayer.Stop();
            }
           else
            {
                MediaPlayer.Play(Button[songIndex]);
            }

            this.isButtonOn = !this.isButtonOn;
        }

        private void revertToPreviousMove()
        {
            if (turnsLog.Count == 0) return;

            Turn prevTurn = this.turnsLog[turnsLog.Count - 1];

            int[] start = prevTurn.start, end = prevTurn.end;

            board[start[1], start[0]] = board[end[1], end[0]];
            board[end[1], end[0]] = prevTurn.deletedPiece;

            this.turn = (turn == Team.RED) ? Team.BLACK : Team.RED;
            this.turnsLog.RemoveAt(turnsLog.Count - 1);
        }

        private void resetGameAction()
        {
            this.initBoard();

            this.turn = Team.RED;
            this.turnsLog = new List<Turn>();
    }

        private Boolean isEndGame(Piece[,] board)
        {

            bool redFlag = false, blackFlag = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] is not null)
                    {
                        if (board[i, j].team == Team.RED)
                        {
                            if (board[i, j].validMoves.Count != 0) redFlag = true;
                        }
                        else
                        {
                            if (board[i, j].validMoves.Count != 0) blackFlag = true;
                        }
                    }
                }
            }
            if (!redFlag  || !blackFlag) return true;
            return false;
        }

    }
}