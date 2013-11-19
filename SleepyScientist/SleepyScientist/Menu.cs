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
        private Texture2D _generalInstButtonTexture;
        private Texture2D _jackInstButtonTexture;
        private Texture2D _eggInstButtonTexture;
        private Texture2D _skateboardInstButtonTexture;
        private Texture2D _batteryInstButtonTexture;
        private Texture2D _doorPitInstButtonTexture;
        private Texture2D _resumeButtonTexture;
        private Texture2D _backButtonTexture;
        private Texture2D _musicOffButtonTexture;
        private Texture2D _musicOnButtonTexture;
        private Texture2D _effectsOffButtonTexture;
        private Texture2D _effectsOnButtonTexture;
        private Texture2D _quitButtonTexture;
        private Texture2D _restartButtonTexture;

        private Texture2D _pauseOverlayTexture;
        private Texture2D _pauseMenuTemplateTexture;

        private Game1 _game;

        #endregion

        public void Initialize(Game1 game)
        {
            _mainMenuButtons = new Dictionary<string, Button>();
            _levelSelectMenuButtons = new Dictionary<string, Button>();
            _pauseMenuElements = new Dictionary<string, Button>();
            _instructionsButtons = new Dictionary<string, Button>();
            _game = game;
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
            _musicOffButtonTexture = Content.Load<Texture2D>("Image/Buttons/music_off_button");
            _musicOnButtonTexture = Content.Load<Texture2D>("Image/Buttons/music_on_button");
            _effectsOffButtonTexture = Content.Load<Texture2D>("Image/Buttons/effects_off_button");
            _effectsOnButtonTexture = Content.Load<Texture2D>("Image/Buttons/effects_on_button");
            _quitButtonTexture = Content.Load<Texture2D>("Image/Buttons/quit_button");
            _restartButtonTexture = Content.Load<Texture2D>("Image/Buttons/restart_button");

            _generalInstButtonTexture = _levelNumButtonTexture;
            _jackInstButtonTexture = _levelNumButtonTexture;
            _eggInstButtonTexture = _levelNumButtonTexture;
            _skateboardInstButtonTexture = _levelNumButtonTexture;
            _batteryInstButtonTexture = _levelNumButtonTexture;
            _doorPitInstButtonTexture = _levelNumButtonTexture;

            _pauseOverlayTexture = Content.Load<Texture2D>("Image/pause_overlay");
            _pauseMenuTemplateTexture = Content.Load<Texture2D>("Image/pause_background");

            // Set up Main Menu
            _mainMenuButtons.Add("newGame", new Button(0, _instructionsButtonTexture.Height, _newGameButtonTexture.Width, _newGameButtonTexture.Height, _newGameButtonTexture));
            _mainMenuButtons.Add("levelSelect", new Button(0, _instructionsButtonTexture.Height * 3, _levelSelectButtonTexture.Width, _levelSelectButtonTexture.Height, _levelSelectButtonTexture));
            _mainMenuButtons.Add("instructions", new Button(0, _instructionsButtonTexture.Height * 4, _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            _mainMenuButtons.Add("quit", new Button(_game.screenWidth - _mainMenuButtonTexture.Width, _game.screenHeight - _mainMenuButtonTexture.Height, _quitButtonTexture.Width, _quitButtonTexture.Height, _quitButtonTexture));
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
            _levelSelectMenuButtons.Add("mainMenu", new Button((_game.screenWidth / 2), _game.screenHeight / 2 + _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Set up the Pause Menu
            _pauseMenuElements.Add("pauseOverlay", new Button(0, 0, _pauseOverlayTexture.Width, _pauseOverlayTexture.Height, _pauseOverlayTexture));
            _pauseMenuElements.Add("pauseImage", new Button((_game.screenWidth / 2) - (_pauseMenuTemplateTexture.Width / 2), (_game.screenHeight / 2) - (_pauseMenuTemplateTexture.Height / 2), _pauseMenuTemplateTexture.Width, _pauseMenuTemplateTexture.Height, _pauseMenuTemplateTexture));
            _pauseMenuElements.Add("quit", new Button((_game.screenWidth / 2) - _quitButtonTexture.Width / 2, (int)(_pauseMenuElements["pauseImage"].Y + _pauseMenuElements["pauseImage"].Height - _quitButtonTexture.Height - 51), _quitButtonTexture.Width, _quitButtonTexture.Height, _quitButtonTexture));
            _pauseMenuElements.Add("instructions", new Button((_game.screenWidth / 2) - _instructionsButtonTexture.Width / 2, (int)(_pauseMenuElements["quit"].Y - _instructionsButtonTexture.Height - 18), _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            _pauseMenuElements.Add("restart", new Button((_game.screenWidth / 2) - _restartButtonTexture.Width / 2, (int)(_pauseMenuElements["instructions"].Y - _restartButtonTexture.Height - 18), _restartButtonTexture.Width, _restartButtonTexture.Height, _restartButtonTexture));
            _pauseMenuElements.Add("resume", new Button((_game.screenWidth / 2) - _instructionsButtonTexture.Width / 2, (int)(_pauseMenuElements["restart"].Y - _resumeButtonTexture.Height - 18), _resumeButtonTexture.Width, _resumeButtonTexture.Height, _resumeButtonTexture));
            _pauseMenuElements.Add("music", new Button((int)(_pauseMenuElements["pauseImage"].X + _pauseMenuElements["pauseImage"].Width - _musicOnButtonTexture.Width - 99), (int)(_pauseMenuElements["pauseImage"].Y + _pauseMenuElements["pauseImage"].Height - _musicOnButtonTexture.Height - 187), _musicOnButtonTexture.Width, _musicOnButtonTexture.Height, _musicOnButtonTexture));
            _pauseMenuElements.Add("effects", new Button((int)(_pauseMenuElements["pauseImage"].X + _pauseMenuElements["pauseImage"].Width - _effectsOnButtonTexture.Width - 99), (int)(_pauseMenuElements["music"].Y - _effectsOnButtonTexture.Height - 22), _effectsOnButtonTexture.Width, _effectsOnButtonTexture.Height, _effectsOnButtonTexture));

            // Instructions Full Screen
            // NEW SET UP OF INSTRUCTIONS MENU
            _instructionsButtons.Add("back", new Button((_game.screenWidth / 2), _game.screenHeight / 2, _backButtonTexture.Width, _backButtonTexture.Height, _backButtonTexture));
            // Instructions buttons
            _instructionsButtons.Add("general", new Button(0, 0, _generalInstButtonTexture.Width, _generalInstButtonTexture.Height, _generalInstButtonTexture));
            _instructionsButtons.Add("jack", new Button(_generalInstButtonTexture.Width, 0, _jackInstButtonTexture.Width, _jackInstButtonTexture.Height, _jackInstButtonTexture));
            _instructionsButtons.Add("egg", new Button(2 * _generalInstButtonTexture.Width, 0, _eggInstButtonTexture.Width, _eggInstButtonTexture.Height, _eggInstButtonTexture));
            _instructionsButtons.Add("skateboard", new Button(3 * _generalInstButtonTexture.Width, 0, _skateboardInstButtonTexture.Width, _skateboardInstButtonTexture.Height, _skateboardInstButtonTexture));
            _instructionsButtons.Add("battery", new Button(3 * _generalInstButtonTexture.Width, 0, _batteryInstButtonTexture.Width, _batteryInstButtonTexture.Height, _batteryInstButtonTexture));
            _instructionsButtons.Add("doorpit", new Button(4 * _generalInstButtonTexture.Width, 0, _doorPitInstButtonTexture.Width, _doorPitInstButtonTexture.Height, _doorPitInstButtonTexture));
            TextHelper.DrawString("Headsadadasdllo", new Microsoft.Xna.Framework.Vector2(_game.screenWidth / 2, _game.screenHeight / 2));
            // DO WE NEED THIS: _instructionsButtons.Add("mainMenu", new Button(_game.screenWidth - _mainMenuButtonTexture.Width, _game.screenHeight - _mainMenuButtonTexture.Height, _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Instructions Pause
        }

        public void Update()
        {
            #region Play/Pause
            if (_game.State == STATE.PLAY || _game.State == STATE.PAUSE)
            {
                if (_game._prevKeyboardState.IsKeyDown(Keys.P) && _game._curKeyboardState.IsKeyUp(Keys.P))  // Actually escape
                    _game.State = (_game.State == STATE.PLAY) ? STATE.PAUSE : STATE.PLAY;
            }
            #endregion
            #region Main Menu
            if (_game.State == STATE.MAIN_MENU)
            {

                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _mainMenuButtons["newGame"].X && _game._curMouseState.X < _mainMenuButtons["newGame"].X + _mainMenuButtons["newGame"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["newGame"].Y && _game._curMouseState.Y < _mainMenuButtons["newGame"].Y + _mainMenuButtons["newGame"].Height)
                {
                    _game.State = STATE.PLAY;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _mainMenuButtons["levelSelect"].X && _game._curMouseState.X < _mainMenuButtons["levelSelect"].X + _mainMenuButtons["levelSelect"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["levelSelect"].Y && _game._curMouseState.Y < _mainMenuButtons["levelSelect"].Y + _mainMenuButtons["levelSelect"].Height)
                {
                    _game.State = STATE.LEVEL_SELECT;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _mainMenuButtons["instructions"].X && _game._curMouseState.X < _mainMenuButtons["instructions"].X + _mainMenuButtons["instructions"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["instructions"].Y && _game._curMouseState.Y < _mainMenuButtons["instructions"].Y + _mainMenuButtons["instructions"].Height)
                {
                    _game.State = STATE.INSTRUCTIONS;
                }
            }
            #endregion
            #region Level Select
            else if (_game.State == STATE.LEVEL_SELECT)
            {
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _levelSelectMenuButtons["mainMenu"].X && _game._curMouseState.X < _levelSelectMenuButtons["mainMenu"].X + _levelSelectMenuButtons["mainMenu"].Width &&
                    _game._curMouseState.Y > _levelSelectMenuButtons["mainMenu"].Y && _game._curMouseState.Y < _levelSelectMenuButtons["mainMenu"].Y + _levelSelectMenuButtons["mainMenu"].Height)
                {
                    _game.State = STATE.MAIN_MENU;
                }
            }
            #endregion
            #region Pause
            else if (_game.State == STATE.PAUSE)
            {
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _pauseMenuElements["instructions"].X && _game._curMouseState.X < _pauseMenuElements["instructions"].X + _pauseMenuElements["instructions"].Width &&
                    _game._curMouseState.Y > _pauseMenuElements["instructions"].Y && _game._curMouseState.Y < _pauseMenuElements["instructions"].Y + _pauseMenuElements["instructions"].Height)
                {
                    _game.State = STATE.INSTRUCTIONS;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _pauseMenuElements["resume"].X && _game._curMouseState.X < _pauseMenuElements["resume"].X + _pauseMenuElements["resume"].Width &&
                    _game._curMouseState.Y > _pauseMenuElements["resume"].Y && _game._curMouseState.Y < _pauseMenuElements["resume"].Y + _pauseMenuElements["resume"].Height)
                {
                    _game.State = STATE.PLAY;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _pauseMenuElements["music"].X && _game._curMouseState.X < _pauseMenuElements["music"].X + _pauseMenuElements["music"].Width &&
                    _game._curMouseState.Y > _pauseMenuElements["music"].Y && _game._curMouseState.Y < _pauseMenuElements["music"].Y + _pauseMenuElements["music"].Height)
                {
                    // Toggle music
                    _pauseMenuElements["music"].Image = (_musicOffButtonTexture == _pauseMenuElements["music"].Image) ? _musicOnButtonTexture : _musicOffButtonTexture;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _pauseMenuElements["effects"].X && _game._curMouseState.X < _pauseMenuElements["effects"].X + _pauseMenuElements["effects"].Width &&
                    _game._curMouseState.Y > _pauseMenuElements["effects"].Y && _game._curMouseState.Y < _pauseMenuElements["effects"].Y + _pauseMenuElements["effects"].Height)
                {
                    // Toggle effects sounds
                    _pauseMenuElements["effects"].Image = (_effectsOffButtonTexture == _pauseMenuElements["effects"].Image) ? _effectsOnButtonTexture : _effectsOffButtonTexture;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _pauseMenuElements["quit"].X && _game._curMouseState.X < _pauseMenuElements["quit"].X + _pauseMenuElements["quit"].Width &&
                    _game._curMouseState.Y > _pauseMenuElements["quit"].Y && _game._curMouseState.Y < _pauseMenuElements["quit"].Y + _pauseMenuElements["quit"].Height)
                {
                    _game.State = STATE.MAIN_MENU;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _pauseMenuElements["restart"].X && _game._curMouseState.X < _pauseMenuElements["restart"].X + _pauseMenuElements["restart"].Width &&
                    _game._curMouseState.Y > _pauseMenuElements["restart"].Y && _game._curMouseState.Y < _pauseMenuElements["restart"].Y + _pauseMenuElements["restart"].Height)
                {
                    // Restart Level
                    //_game.Load();
                    //_game._sleepy.Loser = true;
                    _game.State = STATE.PLAY;
                }
            }
            #endregion
            #region Instructions
            else if (_game.State == STATE.INSTRUCTIONS)
            {
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.LeftButton == ButtonState.Released &&
                   _game._curMouseState.X > _instructionsButtons["back"].X && _game._curMouseState.X < _instructionsButtons["back"].X + _instructionsButtons["back"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["back"].Y && _game._curMouseState.Y < _instructionsButtons["back"].Y + _instructionsButtons["back"].Height)
                {
                    _game.State = _game.PrevState;
                }

                TextHelper.DrawString("Headsadadasdllo", new Microsoft.Xna.Framework.Vector2(0,0));
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_game.State == STATE.PAUSE)
            {
                foreach (Button b in _pauseMenuElements.Values)
                    b.Draw(spriteBatch);
            }
            else if (_game.State == STATE.MAIN_MENU)
            {
                foreach (Button b in _mainMenuButtons.Values)
                    b.Draw(spriteBatch);
            }
            else if (_game.State == STATE.LEVEL_SELECT)
            {
                foreach (Button b in _levelSelectMenuButtons.Values)
                    b.Draw(spriteBatch);
            }
            else if (_game.State == STATE.INSTRUCTIONS)
            {
                foreach (Button b in _instructionsButtons.Values)
                    b.Draw(spriteBatch);
            }
        }
    }
}