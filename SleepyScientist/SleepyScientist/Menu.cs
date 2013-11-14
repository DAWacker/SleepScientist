using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SleepyScientist
{
    class Menu
    {
        #region Fields
        // All buttons in menus (objects?)
        private Dictionary<string, Button> _mainMenuButtons;
        private Dictionary<string, Button> _levelSelectMenuButtons;
        private Dictionary<string, Button> _pauseMenuButtons;
        private Dictionary<string, Button> _instructionsButtons;

        // Menu textures
        private Texture2D _mainMenuButtonTexture;
        private Texture2D _newGameButtonTexture;
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
            _mainMenuButtons = new Dictionary<string, Button>();
            _levelSelectMenuButtons = new Dictionary<string, Button>();
            _pauseMenuButtons = new Dictionary<string, Button>();
            _instructionsButtons = new Dictionary<string, Button>();
        }

        public void LoadContent(ContentManager Content)
        {
            _mainMenuButtonTexture = Content.Load<Texture2D>("Image/Buttons/main_menu");
            _newGameButtonTexture = Content.Load<Texture2D>("Image/Buttons/new_game");
            _levelNumButtonTexture = Content.Load<Texture2D>("Image/Buttons/level");
            _levelSelectButtonTexture = Content.Load<Texture2D>("Image/Buttons/level_select");
            _yesButtonTexture = Content.Load<Texture2D>("Image/Buttons/yes");
            _noButtonTexture = Content.Load<Texture2D>("Image/Buttons/no");
            _instructionsButtonTexture = Content.Load<Texture2D>("Image/Buttons/instructions");
            _backButtonTexture = Content.Load<Texture2D>("Image/Buttons/back");
            _pauseOverlayTexture = Content.Load<Texture2D>("Image/pause_overlay");
            _instructionsTexture1 = Content.Load<Texture2D>("Image/test_instructions1");

            // Set up Main Menu
            _mainMenuButtons.Add("newGame", new Button(0, _instructionsButtonTexture.Height, _newGameButtonTexture.Width, _newGameButtonTexture.Height, _newGameButtonTexture));
            _mainMenuButtons.Add("levelSelect", new Button(0, _instructionsButtonTexture.Height * 3, _levelSelectButtonTexture.Width, _levelSelectButtonTexture.Height, _levelSelectButtonTexture));
            _mainMenuButtons.Add("instructions", new Button(0, _instructionsButtonTexture.Height * 4, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            // Add Image...
            // Add Quit Button

            // Set up Level Select Menu
            int lvlNum = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    _levelSelectMenuButtons.Add("levelNum" + lvlNum, new Button(_levelSelectButtonTexture.Width * j, _levelNumButtonTexture.Height * k, _levelNumButtonTexture.Width, _levelNumButtonTexture.Height, _levelNumButtonTexture));
                    lvlNum++;
                }

            }
            _levelSelectMenuButtons.Add("mainMenu", new Button((Game1.screenWidth / 2), Game1.screenHeight / 2 + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Set up the Pause Menu
            _pauseMenuButtons.Add("pauseOverlay", new Button(0, 0, _pauseOverlayTexture.Width, _pauseOverlayTexture.Height, _pauseOverlayTexture));
            // Pause Menu Image
            // Restart
            _pauseMenuButtons.Add("instructions", new Button((Game1.screenWidth / 2), (Game1.screenHeight / 2) + _instructionsButtonTexture.Height, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            /*Actually RESUME*/_pauseMenuButtons.Add("back", new Button((Game1.screenWidth / 2), (Game1.screenHeight / 2) - _mainMenuButtonTexture.Height, _backButtonTexture.Width, _backButtonTexture.Height, _backButtonTexture));
            // Quit

            // Instructions
            // NEW SET UP OF INSTRUCTIONS MENU
            _instructionsButtons.Add("back", new Button((Game1.screenWidth / 2), Game1.screenHeight / 2, _backButtonTexture.Width, _backButtonTexture.Height, _backButtonTexture));
            _instructionsButtons.Add("instructions1", new Button(0, 0, _instructionsTexture1.Width, _instructionsTexture1.Height, _instructionsTexture1));
            _instructionsButtons.Add("mainMenu", new Button(Game1.screenWidth - _mainMenuButtonTexture.Width, Game1.screenHeight - _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));
        }

        public void Update()
        {
            #region Play/Pause
            if (Game1.State == STATE.PLAY || Game1.State == STATE.PAUSE)
            {
                if (Game1._prevKeyboardState.IsKeyDown(Keys.P) && Game1._curKeyboardState.IsKeyUp(Keys.P))  // Actually escape
                    Game1.State = (Game1.State == STATE.PLAY) ? STATE.PAUSE : STATE.PLAY;
            }
            #endregion

            #region Main Menu
            if (Game1.State == STATE.MAIN_MENU)
            {

                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _mainMenuButtons["newGame"].X && Game1._curMouseState.X < _mainMenuButtons["newGame"].X + _mainMenuButtons["newGame"].Width &&
                    Game1._curMouseState.Y > _mainMenuButtons["newGame"].Y && Game1._curMouseState.Y < _mainMenuButtons["newGame"].Y + _mainMenuButtons["newGame"].Height)
                {
                    Game1.State = STATE.PLAY;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _mainMenuButtons["levelSelect"].X && Game1._curMouseState.X < _mainMenuButtons["levelSelect"].X + _mainMenuButtons["levelSelect"].Width &&
                    Game1._curMouseState.Y > _mainMenuButtons["levelSelect"].Y && Game1._curMouseState.Y < _mainMenuButtons["levelSelect"].Y + _mainMenuButtons["levelSelect"].Height)
                {
                    Game1.State = STATE.LEVEL_SELECT;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _mainMenuButtons["instructions"].X && Game1._curMouseState.X < _mainMenuButtons["instructions"].X + _mainMenuButtons["instructions"].Width &&
                    Game1._curMouseState.Y > _mainMenuButtons["instructions"].Y && Game1._curMouseState.Y < _mainMenuButtons["instructions"].Y + _mainMenuButtons["instructions"].Height)
                {
                    Game1.State = STATE.INSTRUCTIONS;
                }
            }
            #endregion
            #region Level Select
            else if (Game1.State == STATE.LEVEL_SELECT)
            {
                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _levelSelectMenuButtons["mainMenu"].X && Game1._curMouseState.X < _levelSelectMenuButtons["mainMenu"].X + _levelSelectMenuButtons["mainMenu"].Width &&
                    Game1._curMouseState.Y > _levelSelectMenuButtons["mainMenu"].Y && Game1._curMouseState.Y < _levelSelectMenuButtons["mainMenu"].Y + _levelSelectMenuButtons["mainMenu"].Height)
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
                    Game1._curMouseState.X > _pauseMenuButtons["instructions"].X && Game1._curMouseState.X < _pauseMenuButtons["instructions"].X + _pauseMenuButtons["instructions"].Width &&
                    Game1._curMouseState.Y > _pauseMenuButtons["instructions"].Y && Game1._curMouseState.Y < _pauseMenuButtons["instructions"].Y + _pauseMenuButtons["instructions"].Height)
                {
                    Game1.State = STATE.INSTRUCTIONS;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _pauseMenuButtons["back"].X && Game1._curMouseState.X < _pauseMenuButtons["back"].X + _pauseMenuButtons["back"].Width &&
                    Game1._curMouseState.Y > _pauseMenuButtons["back"].Y && Game1._curMouseState.Y < _pauseMenuButtons["back"].Y + _pauseMenuButtons["back"].Height)
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
                   Game1._curMouseState.X > _instructionsButtons["mainMenu"].X && Game1._curMouseState.X < _instructionsButtons["mainMenu"].X + _instructionsButtons["mainMenu"].Width &&
                   Game1._curMouseState.Y > _instructionsButtons["mainMenu"].Y && Game1._curMouseState.Y < _instructionsButtons["mainMenu"].Y + _instructionsButtons["mainMenu"].Height)
                {
                    Game1.State = STATE.MAIN_MENU;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                   Game1._curMouseState.LeftButton == ButtonState.Released &&
                   Game1._curMouseState.X > _instructionsButtons["back"].X && Game1._curMouseState.X < _instructionsButtons["back"].X + _instructionsButtons["back"].Width &&
                   Game1._curMouseState.Y > _instructionsButtons["back"].Y && Game1._curMouseState.Y < _instructionsButtons["back"].Y + _instructionsButtons["back"].Height)
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
                foreach (Button b in _pauseMenuButtons.Values)
                    b.Draw(spriteBatch);
            }
            else if (Game1.State == STATE.MAIN_MENU)
            {
                foreach (Button b in _mainMenuButtons.Values)
                    b.Draw(spriteBatch);
            }
            else if (Game1.State == STATE.LEVEL_SELECT)
            {
                foreach (Button b in _levelSelectMenuButtons.Values)
                    b.Draw(spriteBatch);
            }
            else if (Game1.State == STATE.INSTRUCTIONS)
            {
                foreach (Button b in _instructionsButtons.Values)
                    b.Draw(spriteBatch);
            }
        }
    }
}