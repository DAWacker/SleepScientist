using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SleepyScientist
{
    class Menu
    {
        #region Fields
        private List<Button> _mainMenuButtons;
        private List<Button> _optionsMenuButtons;
        private List<Button> _levelSelectMenuButtons;
        private List<Button> _pauseMenuButtons;
        private List<Button> _instructionsButtons;

        private Texture2D _mainMenuButtonTexture;
        private Texture2D _newGameButtonTexture;
        private Texture2D _optionsButtonTexture;
        private Texture2D _levelNumButtonTexture;
        private Texture2D _levelSelectButtonTexture;
        private Texture2D _yesButtonTexture;
        private Texture2D _noButtonTexture;
        private Texture2D _instructionsButtonTexture;
        private Texture2D _backButtonTexture;
        private Texture2D _pauseOverlayTexture;
        private Texture2D _instructionsTexture1;

        #endregion

        public void Initialize()
        {
            _mainMenuButtons = new List<Button>();
            _optionsMenuButtons = new List<Button>();
            _levelSelectMenuButtons = new List<Button>();
            _pauseMenuButtons = new List<Button>();
            _instructionsButtons = new List<Button>();
        }

        public void LoadContent(ContentManager Content)
        {
            _mainMenuButtonTexture = Content.Load<Texture2D>("Image/Buttons/main_menu");
            _newGameButtonTexture = Content.Load<Texture2D>("Image/Buttons/new_game");
            _optionsButtonTexture = Content.Load<Texture2D>("Image/Buttons/options");
            _levelNumButtonTexture = Content.Load<Texture2D>("Image/Buttons/level");
            _levelSelectButtonTexture = Content.Load<Texture2D>("Image/Buttons/level_select");
            _yesButtonTexture = Content.Load<Texture2D>("Image/Buttons/yes");
            _noButtonTexture = Content.Load<Texture2D>("Image/Buttons/no");
            _instructionsButtonTexture = Content.Load<Texture2D>("Image/Buttons/instructions");
            _backButtonTexture = Content.Load<Texture2D>("Image/Buttons/back");
            _pauseOverlayTexture = Content.Load<Texture2D>("Image/pause_overlay");
            _instructionsTexture1 = Content.Load<Texture2D>("Image/test_instructions1");

            // Set up Main Menu
            _mainMenuButtons.Add(new Button((Game1.screenWidth / 2) - (_newGameButtonTexture.Width / 2), Game1.screenHeight / 2 - _newGameButtonTexture.Height, _newGameButtonTexture.Width, _newGameButtonTexture.Height, _newGameButtonTexture));
            _mainMenuButtons.Add(new Button((Game1.screenWidth / 2) + (_levelNumButtonTexture.Width / 2), Game1.screenHeight / 2 - _levelSelectButtonTexture.Height, _levelSelectButtonTexture.Width, _levelSelectButtonTexture.Height, _levelSelectButtonTexture));
            _mainMenuButtons.Add(new Button((Game1.screenWidth / 2), Game1.screenHeight / 2 + _optionsButtonTexture.Height, _optionsButtonTexture.Width, _optionsButtonTexture.Height, _optionsButtonTexture));
            _mainMenuButtons.Add(new Button((Game1.screenWidth / 2), (Game1.screenHeight / 2) + 2 * _instructionsButtonTexture.Height, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));

            // Set up Options Menu
            _optionsMenuButtons.Add(new Button((Game1.screenWidth / 2), _optionsButtonTexture.Height, _optionsButtonTexture.Width, _optionsButtonTexture.Height, _optionsButtonTexture));
            for (int i = 0; i < 3; i++)
            {
                _optionsMenuButtons.Add(new Button((Game1.screenWidth / 2) + (_yesButtonTexture.Width / 2), Game1.screenHeight / 2 - i * _yesButtonTexture.Height, _yesButtonTexture.Width, _yesButtonTexture.Height, _yesButtonTexture));
                _optionsMenuButtons.Add(new Button((Game1.screenWidth / 2) - (_noButtonTexture.Width / 2), Game1.screenHeight / 2 - i * _noButtonTexture.Height, _noButtonTexture.Width, _noButtonTexture.Height, _noButtonTexture));
            }
            _optionsMenuButtons.Add(new Button((Game1.screenWidth / 2), Game1.screenHeight / 2 + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Set up Level Select Menu
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                    _levelSelectMenuButtons.Add(new Button(_levelSelectButtonTexture.Width * j, _levelNumButtonTexture.Height * k, _levelNumButtonTexture.Width, _levelNumButtonTexture.Height, _levelNumButtonTexture));
            }
            _levelSelectMenuButtons.Add(new Button((Game1.screenWidth / 2), Game1.screenHeight / 2 + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Set up the Pause Menu
            _pauseMenuButtons.Add(new Button(0, 0, _pauseOverlayTexture.Width, _pauseOverlayTexture.Height, _pauseOverlayTexture));
            _pauseMenuButtons.Add(new Button((Game1.screenWidth / 2), (Game1.screenHeight / 2) + _instructionsButtonTexture.Height, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            _pauseMenuButtons.Add(new Button((Game1.screenWidth / 2), (Game1.screenHeight / 2) - _mainMenuButtonTexture.Height, _backButtonTexture.Width, _backButtonTexture.Height, _backButtonTexture));
            _pauseMenuButtons.Add(new Button((Game1.screenWidth / 2), Game1.screenHeight / 2, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Instructions
            _instructionsButtons.Add(new Button((Game1.screenWidth / 2), Game1.screenHeight / 2, _backButtonTexture.Width, _backButtonTexture.Height, _backButtonTexture));
            _instructionsButtons.Add(new Button(0, 0, _instructionsTexture1.Width, _instructionsTexture1.Height, _instructionsTexture1));
            _instructionsButtons.Add(new Button((Game1.screenWidth / 2), (Game1.screenHeight / 2) + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));
        }

        public void Update()
        {
            #region Play/Pause
            if (Game1.State == STATE.PLAY || Game1.State == STATE.PAUSE)
            {
                if (Game1._prevKeyboardState.IsKeyDown(Keys.P) && Game1._curKeyboardState.IsKeyUp(Keys.P))
                    Game1.State = (Game1.State == STATE.PLAY) ? STATE.PAUSE : STATE.PLAY;

                if (Game1.State == STATE.PAUSE)
                {
                    if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                   Game1._curMouseState.LeftButton == ButtonState.Released &&
                   Game1._curMouseState.X > _pauseMenuButtons[_pauseMenuButtons.Count - 1].X && Game1._curMouseState.X < _pauseMenuButtons[_pauseMenuButtons.Count - 1].X + _pauseMenuButtons[_pauseMenuButtons.Count - 1].Width &&
                   Game1._curMouseState.Y > _pauseMenuButtons[_pauseMenuButtons.Count - 1].Y && Game1._curMouseState.Y < _pauseMenuButtons[_pauseMenuButtons.Count - 1].Y + _pauseMenuButtons[_pauseMenuButtons.Count - 1].Height)
                    {
                        Game1.State = STATE.MAIN_MENU;
                    }
                }
            }
            #endregion

            #region Main Menu
            if (Game1.State == STATE.MAIN_MENU)
            {

                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _mainMenuButtons[0].X && Game1._curMouseState.X < _mainMenuButtons[0].X + _mainMenuButtons[0].Width &&
                    Game1._curMouseState.Y > _mainMenuButtons[0].Y && Game1._curMouseState.Y < _mainMenuButtons[0].Y + _mainMenuButtons[0].Height)
                {
                    Game1.State = STATE.PLAY;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _mainMenuButtons[1].X && Game1._curMouseState.X < _mainMenuButtons[1].X + _mainMenuButtons[1].Width &&
                    Game1._curMouseState.Y > _mainMenuButtons[1].Y && Game1._curMouseState.Y < _mainMenuButtons[1].Y + _mainMenuButtons[1].Height)
                {
                    Game1.State = STATE.LEVEL_SELECT;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _mainMenuButtons[2].X && Game1._curMouseState.X < _mainMenuButtons[2].X + _mainMenuButtons[2].Width &&
                    Game1._curMouseState.Y > _mainMenuButtons[2].Y && Game1._curMouseState.Y < _mainMenuButtons[2].Y + _mainMenuButtons[2].Height)
                {
                    Game1.State = STATE.OPTIONS;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _mainMenuButtons[3].X && Game1._curMouseState.X < _mainMenuButtons[3].X + _mainMenuButtons[3].Width &&
                    Game1._curMouseState.Y > _mainMenuButtons[3].Y && Game1._curMouseState.Y < _mainMenuButtons[3].Y + _mainMenuButtons[3].Height)
                {
                    Game1.State = STATE.INSTRUCTIONS;
                }
            }
            #endregion
            #region Options
            else if (Game1.State == STATE.OPTIONS)
            {
                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _optionsMenuButtons[_optionsMenuButtons.Count - 1].X && Game1._curMouseState.X < _optionsMenuButtons[_optionsMenuButtons.Count - 1].X + _optionsMenuButtons[_optionsMenuButtons.Count - 1].Width &&
                    Game1._curMouseState.Y > _optionsMenuButtons[_optionsMenuButtons.Count - 1].Y && Game1._curMouseState.Y < _optionsMenuButtons[_optionsMenuButtons.Count - 1].Y + _optionsMenuButtons[_optionsMenuButtons.Count - 1].Height)
                {
                    Game1.State = STATE.MAIN_MENU;
                }
            }
            #endregion
            #region Level Select
            else if (Game1.State == STATE.LEVEL_SELECT)
            {
                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].X && Game1._curMouseState.X < _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].X + _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Width &&
                    Game1._curMouseState.Y > _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Y && Game1._curMouseState.Y < _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Y + _levelSelectMenuButtons[_levelSelectMenuButtons.Count - 1].Height)
                {
                    Game1.State = STATE.MAIN_MENU;
                }
            }
            #endregion
            #region Pause
            else if (Game1.State == STATE.PAUSE)
            {
                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _pauseMenuButtons[1].X && Game1._curMouseState.X < _pauseMenuButtons[1].X + _pauseMenuButtons[1].Width &&
                    Game1._curMouseState.Y > _pauseMenuButtons[1].Y && Game1._curMouseState.Y < _pauseMenuButtons[1].Y + _pauseMenuButtons[1].Height)
                {
                    Game1.State = STATE.INSTRUCTIONS;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _pauseMenuButtons[_pauseMenuButtons.Count - 1].X && Game1._curMouseState.X < _pauseMenuButtons[_pauseMenuButtons.Count - 1].X + _pauseMenuButtons[_pauseMenuButtons.Count - 1].Width &&
                    Game1._curMouseState.Y > _pauseMenuButtons[_pauseMenuButtons.Count - 1].Y && Game1._curMouseState.Y < _pauseMenuButtons[_pauseMenuButtons.Count - 1].Y + _pauseMenuButtons[_pauseMenuButtons.Count - 1].Height)
                {
                    Game1.State = STATE.MAIN_MENU;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _pauseMenuButtons[2].X && Game1._curMouseState.X < _pauseMenuButtons[2].X + _pauseMenuButtons[2].Width &&
                    Game1._curMouseState.Y > _pauseMenuButtons[2].Y && Game1._curMouseState.Y < _pauseMenuButtons[2].Y + _pauseMenuButtons[2].Height)
                {
                    Game1.State = Game1.PrevState;
                }
            }
            #endregion
            #region Instructions
            else if (Game1.State == STATE.INSTRUCTIONS)
            {
                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                   Game1._curMouseState.LeftButton == ButtonState.Released &&
                   Game1._curMouseState.X > _instructionsButtons[_instructionsButtons.Count - 1].X && Game1._curMouseState.X < _instructionsButtons[_instructionsButtons.Count - 1].X + _instructionsButtons[_instructionsButtons.Count - 1].Width &&
                   Game1._curMouseState.Y > _instructionsButtons[_instructionsButtons.Count - 1].Y && Game1._curMouseState.Y < _instructionsButtons[_instructionsButtons.Count - 1].Y + _instructionsButtons[_instructionsButtons.Count - 1].Height)
                {
                    Game1.State = STATE.MAIN_MENU;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                   Game1._curMouseState.LeftButton == ButtonState.Released &&
                   Game1._curMouseState.X > _instructionsButtons[0].X && Game1._curMouseState.X < _instructionsButtons[0].X + _instructionsButtons[0].Width &&
                   Game1._curMouseState.Y > _instructionsButtons[0].Y && Game1._curMouseState.Y < _instructionsButtons[0].Y + _instructionsButtons[0].Height)
                {
                    Game1.State = Game1.PrevState;
                }
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.State == STATE.PAUSE)
            {
                foreach (Button b in _pauseMenuButtons)
                    b.Draw(spriteBatch);
            }
            else if (Game1.State == STATE.MAIN_MENU)
            {
                foreach (Button b in _mainMenuButtons)
                    b.Draw(spriteBatch);
            }
            else if (Game1.State == STATE.OPTIONS)
            {
                foreach (Button b in _optionsMenuButtons)
                    b.Draw(spriteBatch);
            }
            else if (Game1.State == STATE.LEVEL_SELECT)
            {
                foreach (Button b in _levelSelectMenuButtons)
                    b.Draw(spriteBatch);
            }
            else if (Game1.State == STATE.INSTRUCTIONS)
            {
                foreach (Button b in _instructionsButtons)
                    b.Draw(spriteBatch);
            }
        }
    }
}