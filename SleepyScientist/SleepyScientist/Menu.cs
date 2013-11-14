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
        private Dictionary<string, Button> _pauseMenuElements;
        private Dictionary<string, Button> _instructionsButtons;

        // Menu textures
        private Texture2D _mainMenuButtonTexture;
        private Texture2D _newGameButtonTexture;
        private Texture2D _levelNumButtonTexture;
        private Texture2D _levelSelectButtonTexture;
        private Texture2D _instructionsButtonTexture;
        private Texture2D _resumeButtonTexture;
        private Texture2D _backButtonTexture;
        private Texture2D _instructionsTexture1;
        private Texture2D _soundOffButtonTexture;
        private Texture2D _soundOnButtonTexture;

        private Texture2D _pauseOverlayTexture;
        private Texture2D _pauseMenuTemplateTexture;

        #endregion

        public void Initialize()
        {
            _mainMenuButtons = new Dictionary<string, Button>();
            _levelSelectMenuButtons = new Dictionary<string, Button>();
            _pauseMenuElements = new Dictionary<string, Button>();
            _instructionsButtons = new Dictionary<string, Button>();
        }

        public void LoadContent(ContentManager Content)
        {
            _mainMenuButtonTexture = Content.Load<Texture2D>("Image/Buttons/main_menu_button");
            _newGameButtonTexture = Content.Load<Texture2D>("Image/Buttons/new_game_button");
            _levelNumButtonTexture = Content.Load<Texture2D>("Image/Buttons/level_num_button");
            _levelSelectButtonTexture = Content.Load<Texture2D>("Image/Buttons/level_select_button");
            _instructionsButtonTexture = Content.Load<Texture2D>("Image/Buttons/instructions_button");
            _resumeButtonTexture = Content.Load<Texture2D>("Image/Buttons/resume_button");
            _backButtonTexture = Content.Load<Texture2D>("Image/Buttons/back_button");
            _soundOffButtonTexture = Content.Load<Texture2D>("Image/Buttons/sound_off_button");
            _soundOnButtonTexture = Content.Load<Texture2D>("Image/Buttons/sound_on_button");

            _pauseOverlayTexture = Content.Load<Texture2D>("Image/pause_overlay");
            _pauseMenuTemplateTexture= Content.Load<Texture2D>("Image/pause_menu_template");
            _instructionsTexture1 = Content.Load<Texture2D>("Image/test_instructions1");

            // Set up Main Menu
            _mainMenuButtons.Add("newGame", new Button(0, _instructionsButtonTexture.Height, _newGameButtonTexture.Width, _newGameButtonTexture.Height, _newGameButtonTexture));
            _mainMenuButtons.Add("levelSelect", new Button(0, _instructionsButtonTexture.Height * 3, _levelSelectButtonTexture.Width, _levelSelectButtonTexture.Height, _levelSelectButtonTexture));
            _mainMenuButtons.Add("instructions", new Button(0, _instructionsButtonTexture.Height * 4, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            // Quit _mainMenuButtons.Add("quit", new Button( (Game1.screenWidth - _mainMenuButtonTexture.Width, Game1.screenHeight - _mainMenuButtonTexture.Height, _quitButtonTexture.Width, _quitButtonTexture.Height, _quitButtonTexture));
            // Add Image... 

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
            _pauseMenuElements.Add("pauseOverlay", new Button(0, 0, _pauseOverlayTexture.Width, _pauseOverlayTexture.Height, _pauseOverlayTexture));
            _pauseMenuElements.Add("pauseImage", new Button((Game1.screenWidth / 2) - (_pauseMenuTemplateTexture.Width / 2), (Game1.screenHeight / 2) - (_pauseMenuTemplateTexture.Height / 2), _pauseMenuTemplateTexture.Width, _pauseMenuTemplateTexture.Height, _pauseMenuTemplateTexture));
            // Restart _pauseMenuElements.Add("restart", new Button( (Game1.screenWidth / 2) - _restartButtonTexture.Width / 2, (Game1.screenHeight / 2) - 2 * _restartButtonTexture.Height, _restartButtonTexture.Width, _restartButtonTexture.Height, _restartButtonTexture)):
            _pauseMenuElements.Add("instructions", new Button((Game1.screenWidth / 2) - _instructionsButtonTexture.Width / 2, (Game1.screenHeight / 2) - 1 * _instructionsButtonTexture.Height, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            _pauseMenuElements.Add("resume", new Button((Game1.screenWidth / 2) - _instructionsButtonTexture.Width / 2, (Game1.screenHeight / 2) + 1 * _instructionsButtonTexture.Height, _resumeButtonTexture.Width, _resumeButtonTexture.Height, _resumeButtonTexture));
            // Quit _pauseMenuElements.Add("quit", new Button( (Game1.screenWidth / 2) - _quitButtonTexture.Width / 2, (Game1.screenHeight / 2) + 2 * _quitButtonTexture.Height, _quitButtonTexture.Width, _quitButtonTexture.Height, _quitButtonTexture)):
            _pauseMenuElements.Add("sound", new Button((Game1.screenWidth / 2) + 1 * _soundOffButtonTexture.Width + (_soundOffButtonTexture.Width/2), (Game1.screenHeight / 2), _soundOnButtonTexture.Width, _soundOnButtonTexture.Height, _soundOnButtonTexture));     
    
            // Instructions Full Screen
            // NEW SET UP OF INSTRUCTIONS MENU
            _instructionsButtons.Add("back", new Button((Game1.screenWidth / 2), Game1.screenHeight / 2, _backButtonTexture.Width, _backButtonTexture.Height, _backButtonTexture));
            _instructionsButtons.Add("instructions1", new Button(0, 0, _instructionsTexture1.Width, _instructionsTexture1.Height, _instructionsTexture1));
            // DO WE NEED THIS: _instructionsButtons.Add("mainMenu", new Button(Game1.screenWidth - _mainMenuButtonTexture.Width, Game1.screenHeight - _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Instructions Pause
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
                    Game1._curMouseState.X > _pauseMenuElements["instructions"].X && Game1._curMouseState.X < _pauseMenuElements["instructions"].X + _pauseMenuElements["instructions"].Width &&
                    Game1._curMouseState.Y > _pauseMenuElements["instructions"].Y && Game1._curMouseState.Y < _pauseMenuElements["instructions"].Y + _pauseMenuElements["instructions"].Height)
                {
                    Game1.State = STATE.INSTRUCTIONS;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _pauseMenuElements["resume"].X && Game1._curMouseState.X < _pauseMenuElements["resume"].X + _pauseMenuElements["resume"].Width &&
                    Game1._curMouseState.Y > _pauseMenuElements["resume"].Y && Game1._curMouseState.Y < _pauseMenuElements["resume"].Y + _pauseMenuElements["resume"].Height)
                {
                    Game1.State = STATE.PLAY;
                }
                else if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
                    Game1._curMouseState.LeftButton == ButtonState.Released &&
                    Game1._curMouseState.X > _pauseMenuElements["sound"].X && Game1._curMouseState.X < _pauseMenuElements["sound"].X + _pauseMenuElements["sound"].Width &&
                    Game1._curMouseState.Y > _pauseMenuElements["sound"].Y && Game1._curMouseState.Y < _pauseMenuElements["sound"].Y + _pauseMenuElements["sound"].Height)
                {
                    _pauseMenuElements["sound"].Image = (_soundOffButtonTexture == _pauseMenuElements["sound"].Image) ? _soundOnButtonTexture : _soundOffButtonTexture;
                    // Toggle sound
                }
            }
            #endregion
            #region Instructions
            else if (Game1.State == STATE.INSTRUCTIONS)
            {
                if (Game1._prevMouseState.LeftButton == ButtonState.Pressed &&
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
                foreach (Button b in _pauseMenuElements.Values)
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