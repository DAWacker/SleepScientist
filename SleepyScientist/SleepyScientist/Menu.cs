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
        private Dictionary<string, Button> _gameOverButtons;

        // Menu textures
        private List<Texture2D> _levelNumButtons;

        private Texture2D _mainMenuButtonTexture;
        private Texture2D _newGameButtonTexture;
        private Texture2D _levelSelectButtonTexture;

        private Texture2D _instructionsButtonTexture;
        private Texture2D _overviewInstButtonTexture;
        private Texture2D _jackInstButtonTexture;
        private Texture2D _eggInstButtonTexture;
        private Texture2D _skateboardInstButtonTexture;
        private Texture2D _otherObjectsInstButtonTexture;

        private Texture2D _resumeButtonTexture;
        private Texture2D _backButtonTexture;
        private Texture2D _musicOffButtonTexture;
        private Texture2D _musicOnButtonTexture;
        private Texture2D _effectsOffButtonTexture;
        private Texture2D _effectsOnButtonTexture;
        private Texture2D _quitButtonTexture;
        private Texture2D _restartButtonTexture;

        private Texture2D _overview_text_texture;
        private Texture2D _jack_inthe_box_text_texture;
        private Texture2D _eggbeater_text_texture;
        private Texture2D _skateboard_text_texture;
        private Texture2D _other_objects_text_texture;
        private Texture2D _overview_menu_texture;
        private Texture2D _jack_inthe_box_menu_texture;
        private Texture2D _eggbeater_menu_texture;
        private Texture2D _skateboard_menu_texture;
        private Texture2D _other_objects_menu_texture;

        private Texture2D _pauseOverlayTexture;
        private Texture2D _nameTexture;
        private Texture2D _pauseMenuTexture;
        private Texture2D _instMenuTexture;
        private Texture2D _gameOverTexture;
        private Texture2D _mainMenuTexture;
        private Texture2D _levelSelectTexture;

        /// <summary>
        /// How much the button depressed when clicked
        /// </summary>
        private const int BUTTON_CLICK_FACTOR = 3;
        /// <summary>
        /// True if button is currently depressed
        /// </summary>
        private bool buttonDepressed = false;
        /// <summary>
        /// The button that is currently depressed
        /// </summary>
        private Button depressedButton;

        private Game1 _game;

        #endregion

        public void Initialize(Game1 game)
        {
            _mainMenuButtons = new Dictionary<string, Button>();
            _levelSelectMenuButtons = new Dictionary<string, Button>();
            _pauseMenuElements = new Dictionary<string, Button>();
            _instructionsButtons = new Dictionary<string, Button>();
            _gameOverButtons = new Dictionary<string, Button>();
            _game = game;

            _levelNumButtons = new List<Texture2D>();
        }

        public void LoadContent(ContentManager Content)
        {
            _mainMenuButtonTexture = Content.Load<Texture2D>("Image/Buttons/main_menu_button");
            _newGameButtonTexture = Content.Load<Texture2D>("Image/Buttons/new_game_button");
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

            _overview_text_texture = Content.Load<Texture2D>("Image/overview_text");
            _jack_inthe_box_text_texture = Content.Load<Texture2D>("Image/jack_inthe_box_text");
            _eggbeater_text_texture = Content.Load<Texture2D>("Image/eggbeater_text");
            _skateboard_text_texture = Content.Load<Texture2D>("Image/skateboard_text");
            _other_objects_text_texture = Content.Load<Texture2D>("Image/other_objects_text");
            _jack_inthe_box_menu_texture = Content.Load<Texture2D>("Image/jack_inthe_box_menu");
            _eggbeater_menu_texture = Content.Load<Texture2D>("Image/eggbeater_menu");
            _skateboard_menu_texture = Content.Load<Texture2D>("Image/skateboard_menu");
            _overview_menu_texture = new Texture2D(_game.GraphicsDevice, _jack_inthe_box_menu_texture.Width, _jack_inthe_box_menu_texture.Height);
            //_other_objects_menu_texture = new Texture2D(_game.GraphicsDevice, _jack_inthe_box_menu_texture.Width, _jack_inthe_box_menu_texture.Height);
            _other_objects_menu_texture = Content.Load<Texture2D>("Image/other_objects_menu");

            _overviewInstButtonTexture = Content.Load<Texture2D>("Image/Buttons/overview_button");
            _jackInstButtonTexture = Content.Load<Texture2D>("Image/Buttons/jack_inthe_box_button");
            _eggInstButtonTexture = Content.Load<Texture2D>("Image/Buttons/eggbeater_button");
            _skateboardInstButtonTexture = Content.Load<Texture2D>("Image/Buttons/skateboard_button");
            _otherObjectsInstButtonTexture = Content.Load<Texture2D>("Image/Buttons/other_objects_button");

            _nameTexture = Content.Load<Texture2D>("Image/name");
            _pauseOverlayTexture = Content.Load<Texture2D>("Image/pause_overlay");
            _pauseMenuTexture = Content.Load<Texture2D>("Image/pause_background");
            _instMenuTexture = Content.Load<Texture2D>("Image/instructions_background");
            _gameOverTexture = Content.Load<Texture2D>("Image/game_over_background");
            _mainMenuTexture = Content.Load<Texture2D>("Image/main_menu_background");
            _levelSelectTexture = Content.Load<Texture2D>("Image/level_select_background");

            for (int i = 0; i < 30; i++) // total levels
            {
                _levelNumButtons.Add(Content.Load<Texture2D>("Image/Buttons/LevelNums/" + (i + 1) ));
            }

            // Set up Main Menu
            _mainMenuButtons.Add("background", new Button((_game.screenWidth / 2) - (_gameOverTexture.Width / 2), (_game.screenHeight / 2) - (_gameOverTexture.Height / 2), _mainMenuTexture.Width, _mainMenuTexture.Height, _mainMenuTexture));
            _mainMenuButtons.Add("quit", new Button((_game.screenWidth / 2) - (_quitButtonTexture.Width / 2), (int)(_mainMenuButtons["background"].Y + _mainMenuButtons["background"].Height - _quitButtonTexture.Height - 60), _quitButtonTexture.Width, _quitButtonTexture.Height, _quitButtonTexture));
            _mainMenuButtons.Add("levelSelect", new Button((int)(_mainMenuButtons["quit"].X), (int)(_mainMenuButtons["quit"].Y - _levelSelectButtonTexture.Height - 10), _levelSelectButtonTexture.Width, _levelSelectButtonTexture.Height, _levelSelectButtonTexture));
            _mainMenuButtons.Add("instructions", new Button((int)(_mainMenuButtons["levelSelect"].X), (int)(_mainMenuButtons["levelSelect"].Y - _instructionsButtonTexture.Height - 10), _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            _mainMenuButtons.Add("newGame", new Button((int)(_mainMenuButtons["instructions"].X), (int)(_mainMenuButtons["instructions"].Y - _newGameButtonTexture.Height - 10), _newGameButtonTexture.Width, _newGameButtonTexture.Height, _newGameButtonTexture));
            _mainMenuButtons.Add("name", new Button((_game.screenWidth / 2) - (_nameTexture.Width / 2), (int)(_mainMenuButtons["background"].Y + ((_mainMenuButtons["newGame"].Y - _mainMenuButtons["background"].Y) / 2) - (_nameTexture.Height/2)), _nameTexture.Width, _nameTexture.Height, _nameTexture));
            // Add Image... 

            // Set up Level Select Menu
            int lvlNum = 29;
            int heightModifier = 1;
            _levelSelectMenuButtons.Add("background", new Button((_game.screenWidth / 2) - (_gameOverTexture.Width / 2), (_game.screenHeight / 2) - (_levelSelectTexture.Height / 2), _levelSelectTexture.Width, _levelSelectTexture.Height, _levelSelectTexture));
            for (int j = 3; j > 0; j--)
            {
                for (int k = 10; k > 0; k--)
                {
                    _levelSelectMenuButtons.Add("level" + (lvlNum + 1), new Button((int)(_levelSelectMenuButtons["background"].X + _levelNumButtons[lvlNum].Width*(k-1) + (27 * (k-1)) + 137), (int)(_levelSelectMenuButtons["background"].Y + _levelSelectMenuButtons["background"].Height - _levelNumButtons[lvlNum].Height * heightModifier - 75 - (50 * heightModifier)), _levelNumButtons[lvlNum].Width, _levelNumButtons[lvlNum].Height, _levelNumButtons[lvlNum]));
                    lvlNum--;
                }
                heightModifier++;
            }
            _levelSelectMenuButtons.Add("mainMenu", new Button((int)(_levelSelectMenuButtons["background"].X + (_levelSelectMenuButtons["background"].Width / 2) - (_mainMenuButtonTexture.Width / 2)), (int)(_levelSelectMenuButtons["background"].Y + _levelSelectMenuButtons["background"].Height - _mainMenuButtonTexture.Height - 10), _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));

            // Set up the Pause Menu
            _pauseMenuElements.Add("pauseOverlay", new Button(0, 0, _pauseOverlayTexture.Width, _pauseOverlayTexture.Height, _pauseOverlayTexture));
            _pauseMenuElements.Add("background", new Button((_game.screenWidth / 2) - (_pauseMenuTexture.Width / 2), (_game.screenHeight / 2) - (_pauseMenuTexture.Height / 2), _pauseMenuTexture.Width, _pauseMenuTexture.Height, _pauseMenuTexture));
            _pauseMenuElements.Add("quit", new Button((_game.screenWidth / 2) - _quitButtonTexture.Width / 2, (int)(_pauseMenuElements["background"].Y + _pauseMenuElements["background"].Height - _quitButtonTexture.Height - 51), _quitButtonTexture.Width, _quitButtonTexture.Height, _quitButtonTexture));
            _pauseMenuElements.Add("instructions", new Button((_game.screenWidth / 2) - _instructionsButtonTexture.Width / 2, (int)(_pauseMenuElements["quit"].Y - _instructionsButtonTexture.Height - 18), _instructionsButtonTexture.Width, _instructionsButtonTexture.Height, _instructionsButtonTexture));
            _pauseMenuElements.Add("restart", new Button((_game.screenWidth / 2) - _restartButtonTexture.Width / 2, (int)(_pauseMenuElements["instructions"].Y - _restartButtonTexture.Height - 18), _restartButtonTexture.Width, _restartButtonTexture.Height, _restartButtonTexture));
            _pauseMenuElements.Add("resume", new Button((_game.screenWidth / 2) - _instructionsButtonTexture.Width / 2, (int)(_pauseMenuElements["restart"].Y - _resumeButtonTexture.Height - 18), _resumeButtonTexture.Width, _resumeButtonTexture.Height, _resumeButtonTexture));
            _pauseMenuElements.Add("music", new Button((int)(_pauseMenuElements["background"].X + _pauseMenuElements["background"].Width - _musicOnButtonTexture.Width - 99), (int)(_pauseMenuElements["background"].Y + _pauseMenuElements["background"].Height - _musicOnButtonTexture.Height - 187), _musicOnButtonTexture.Width, _musicOnButtonTexture.Height, _musicOnButtonTexture));
            _pauseMenuElements.Add("effects", new Button((int)(_pauseMenuElements["background"].X + _pauseMenuElements["background"].Width - _effectsOnButtonTexture.Width - 99), (int)(_pauseMenuElements["music"].Y - _effectsOnButtonTexture.Height - 22), _effectsOnButtonTexture.Width, _effectsOnButtonTexture.Height, _effectsOnButtonTexture));

            // Instructions
            _instructionsButtons.Add("background", new Button((_game.screenWidth / 2) - (_instMenuTexture.Width / 2), (_game.screenHeight / 2) - (_instMenuTexture.Height / 2), _instMenuTexture.Width, _instMenuTexture.Height, _instMenuTexture));
            _instructionsButtons.Add("back", new Button((int)(_instructionsButtons["background"].X + _instructionsButtons["background"].Width - _backButtonTexture.Width * 2 - 15), (int)(_instructionsButtons["background"].Y + _instructionsButtons["background"].Height - _backButtonTexture.Height - 10), _backButtonTexture.Width, _backButtonTexture.Height, _backButtonTexture));
            _instructionsButtons.Add("overview", new Button((int)(_instructionsButtons["background"].X + 96), (int)(_instructionsButtons["background"].Y + 170), _overviewInstButtonTexture.Width, _overviewInstButtonTexture.Height, _overviewInstButtonTexture));
            _instructionsButtons.Add("jack", new Button((int)(_instructionsButtons["overview"].X + _instructionsButtons["overview"].Width + 18), (int)(_instructionsButtons["overview"].Y), _jackInstButtonTexture.Width, _jackInstButtonTexture.Height, _jackInstButtonTexture));
            _instructionsButtons.Add("egg", new Button((int)(_instructionsButtons["jack"].X + _instructionsButtons["jack"].Width + 18), (int)(_instructionsButtons["overview"].Y), _eggInstButtonTexture.Width, _eggInstButtonTexture.Height, _eggInstButtonTexture));
            _instructionsButtons.Add("skateboard", new Button((int)(_instructionsButtons["egg"].X + _instructionsButtons["egg"].Width + 18), (int)(_instructionsButtons["overview"].Y), _skateboardInstButtonTexture.Width, _skateboardInstButtonTexture.Height, _skateboardInstButtonTexture));
            _instructionsButtons.Add("other", new Button((int)(_instructionsButtons["skateboard"].X + _instructionsButtons["skateboard"].Width + 18), (int)(_instructionsButtons["overview"].Y), _otherObjectsInstButtonTexture.Width, _otherObjectsInstButtonTexture.Height, _otherObjectsInstButtonTexture));
            _instructionsButtons.Add("text", new Button((int)(_instructionsButtons["background"].X + 385), (int)(_instructionsButtons["background"].Y + 270), _overview_text_texture.Width, _overview_text_texture.Height, _overview_text_texture));
            _instructionsButtons.Add("image", new Button((int)(_instructionsButtons["background"].X + 123), (int)(_instructionsButtons["background"].Y + 260), _overview_menu_texture.Width, _overview_menu_texture.Height, _overview_menu_texture));

            // Game over
            _gameOverButtons.Add("background", new Button( (_game.screenWidth / 2) - (_gameOverTexture.Width / 2), (_game.screenHeight / 2) - (_gameOverTexture.Height / 2), _gameOverTexture.Width, _gameOverTexture.Height, _gameOverTexture));
            _gameOverButtons.Add("restart", new Button( (int)(_gameOverButtons["background"].X + _gameOverButtons["background"].Width / 2 - (_restartButtonTexture.Width / 2)), (int)(_gameOverButtons["background"].Y + 200), _restartButtonTexture.Width, _restartButtonTexture.Height, _restartButtonTexture));
            _gameOverButtons.Add("main", new Button((int)(_gameOverButtons["restart"].X), (int)(_gameOverButtons["restart"].Y + _gameOverButtons["restart"].Height + 30), _mainMenuButtonTexture.Width, _mainMenuButtonTexture.Height, _mainMenuButtonTexture));
            _gameOverButtons.Add("quit", new Button((int)(_gameOverButtons["main"].X), (int)(_gameOverButtons["main"].Y + _gameOverButtons["main"].Height + 30), _quitButtonTexture.Width, _quitButtonTexture.Height, _quitButtonTexture)); 
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
                #region Button Click
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _mainMenuButtons["newGame"].X && _game._curMouseState.X < _mainMenuButtons["newGame"].X + _mainMenuButtons["newGame"].Width &&
                   _game._curMouseState.Y > _mainMenuButtons["newGame"].Y && _game._curMouseState.Y < _mainMenuButtons["newGame"].Y + _mainMenuButtons["newGame"].Height)
                {
                    ButtonDepress(_mainMenuButtons["newGame"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.X > _mainMenuButtons["levelSelect"].X && _game._curMouseState.X < _mainMenuButtons["levelSelect"].X + _mainMenuButtons["levelSelect"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["levelSelect"].Y && _game._curMouseState.Y < _mainMenuButtons["levelSelect"].Y + _mainMenuButtons["levelSelect"].Height)
                {
                    ButtonDepress(_mainMenuButtons["levelSelect"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.X > _mainMenuButtons["instructions"].X && _game._curMouseState.X < _mainMenuButtons["instructions"].X + _mainMenuButtons["instructions"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["instructions"].Y && _game._curMouseState.Y < _mainMenuButtons["instructions"].Y + _mainMenuButtons["instructions"].Height)
                {
                    ButtonDepress(_mainMenuButtons["instructions"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.X > _mainMenuButtons["quit"].X && _game._curMouseState.X < _mainMenuButtons["quit"].X + _mainMenuButtons["quit"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["quit"].Y && _game._curMouseState.Y < _mainMenuButtons["quit"].Y + _mainMenuButtons["quit"].Height)
                {
                    ButtonDepress(_mainMenuButtons["quit"]);
                }
            #endregion

                #region Button release
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _mainMenuButtons["newGame"].X && _game._curMouseState.X < _mainMenuButtons["newGame"].X + _mainMenuButtons["newGame"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["newGame"].Y && _game._curMouseState.Y < _mainMenuButtons["newGame"].Y + _mainMenuButtons["newGame"].Height)
                {
                    _game._levelNumber = _game.StartLevel;
                    _game.SetupLevel(_game._levelNumber);
                    _game.State = STATE.PLAY;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _mainMenuButtons["levelSelect"].X && _game._curMouseState.X < _mainMenuButtons["levelSelect"].X + _mainMenuButtons["levelSelect"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["levelSelect"].Y && _game._curMouseState.Y < _mainMenuButtons["levelSelect"].Y + _mainMenuButtons["levelSelect"].Height)
                {
                    // Go to the level select screen
                    _game.State = STATE.LEVEL_SELECT;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _mainMenuButtons["instructions"].X && _game._curMouseState.X < _mainMenuButtons["instructions"].X + _mainMenuButtons["instructions"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["instructions"].Y && _game._curMouseState.Y < _mainMenuButtons["instructions"].Y + _mainMenuButtons["instructions"].Height)
                {
                    // Go to the instructions screen
                    _game.State = STATE.INSTRUCTIONS;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _mainMenuButtons["quit"].X && _game._curMouseState.X < _mainMenuButtons["quit"].X + _mainMenuButtons["quit"].Width &&
                    _game._curMouseState.Y > _mainMenuButtons["quit"].Y && _game._curMouseState.Y < _mainMenuButtons["quit"].Y + _mainMenuButtons["quit"].Height)
                {
                    // Quit
                    _game.Exit();
                }
                #endregion
            }
            #endregion
            #region Level Select
            else if (_game.State == STATE.LEVEL_SELECT)
            {
                #region Button Click
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _levelSelectMenuButtons["mainMenu"].X && _game._curMouseState.X < _levelSelectMenuButtons["mainMenu"].X + _levelSelectMenuButtons["mainMenu"].Width &&
                   _game._curMouseState.Y > _levelSelectMenuButtons["mainMenu"].Y && _game._curMouseState.Y < _levelSelectMenuButtons["mainMenu"].Y + _levelSelectMenuButtons["mainMenu"].Height)
                {
                    ButtonDepress(_levelSelectMenuButtons["mainMenu"]);
                }
                for (int i = 0; i < _levelNumButtons.Count; i++)
                {
                    if (i < _game._totalLevels)
                    {
                        if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                            _game._curMouseState.X > _levelSelectMenuButtons["level" + (i + 1)].X && _game._curMouseState.X < _levelSelectMenuButtons["level" + (i + 1)].X + _levelSelectMenuButtons["level" + (i + 1)].Width &&
                            _game._curMouseState.Y > _levelSelectMenuButtons["level" + (i + 1)].Y && _game._curMouseState.Y < _levelSelectMenuButtons["level" + (i + 1)].Y + _levelSelectMenuButtons["level" + (i + 1)].Height)
                        {
                            ButtonDepress(_levelSelectMenuButtons["level" + (i + 1)]);
                        }
                    }
                }
                #endregion

                #region Button Release
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                    _game._curMouseState.LeftButton == ButtonState.Released &&
                    _game._curMouseState.X > _levelSelectMenuButtons["mainMenu"].X && _game._curMouseState.X < _levelSelectMenuButtons["mainMenu"].X + _levelSelectMenuButtons["mainMenu"].Width &&
                    _game._curMouseState.Y > _levelSelectMenuButtons["mainMenu"].Y && _game._curMouseState.Y < _levelSelectMenuButtons["mainMenu"].Y + _levelSelectMenuButtons["mainMenu"].Height)
                {
                    _game.State = STATE.MAIN_MENU;
                }

                for (int i = 0; i < _levelNumButtons.Count; i++)
                {
                    if (i < _game._totalLevels)
                    {
                        if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                            _game._curMouseState.LeftButton == ButtonState.Released &&
                            _game._curMouseState.X > _levelSelectMenuButtons["level" + (i + 1)].X && _game._curMouseState.X < _levelSelectMenuButtons["level" + (i + 1)].X + _levelSelectMenuButtons["level" + (i + 1)].Width &&
                            _game._curMouseState.Y > _levelSelectMenuButtons["level" + (i + 1)].Y && _game._curMouseState.Y < _levelSelectMenuButtons["level" + (i + 1)].Y + _levelSelectMenuButtons["level" + (i + 1)].Height)
                        {
                            _game._levelNumber = i + 1;
                            _game.SetupLevel(_game._levelNumber);
                            _game.State = STATE.PLAY;
                        }
                    }
                }
                #endregion
            }
            #endregion
            #region Pause
            else if (_game.State == STATE.PAUSE)
            {
                #region Button Click
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _pauseMenuElements["instructions"].X && _game._curMouseState.X < _pauseMenuElements["instructions"].X + _pauseMenuElements["instructions"].Width &&
                   _game._curMouseState.Y > _pauseMenuElements["instructions"].Y && _game._curMouseState.Y < _pauseMenuElements["instructions"].Y + _pauseMenuElements["instructions"].Height)
                {
                    ButtonDepress(_pauseMenuElements["instructions"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _pauseMenuElements["resume"].X && _game._curMouseState.X < _pauseMenuElements["resume"].X + _pauseMenuElements["resume"].Width &&
                   _game._curMouseState.Y > _pauseMenuElements["resume"].Y && _game._curMouseState.Y < _pauseMenuElements["resume"].Y + _pauseMenuElements["resume"].Height)
                {
                    ButtonDepress(_pauseMenuElements["resume"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _pauseMenuElements["music"].X && _game._curMouseState.X < _pauseMenuElements["music"].X + _pauseMenuElements["music"].Width &&
                   _game._curMouseState.Y > _pauseMenuElements["music"].Y && _game._curMouseState.Y < _pauseMenuElements["music"].Y + _pauseMenuElements["music"].Height)
                {
                    ButtonDepress(_pauseMenuElements["music"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _pauseMenuElements["effects"].X && _game._curMouseState.X < _pauseMenuElements["effects"].X + _pauseMenuElements["effects"].Width &&
                   _game._curMouseState.Y > _pauseMenuElements["effects"].Y && _game._curMouseState.Y < _pauseMenuElements["effects"].Y + _pauseMenuElements["effects"].Height)
                {
                    ButtonDepress(_pauseMenuElements["effects"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _pauseMenuElements["quit"].X && _game._curMouseState.X < _pauseMenuElements["quit"].X + _pauseMenuElements["quit"].Width &&
                   _game._curMouseState.Y > _pauseMenuElements["quit"].Y && _game._curMouseState.Y < _pauseMenuElements["quit"].Y + _pauseMenuElements["quit"].Height)
                {
                    ButtonDepress(_pauseMenuElements["quit"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _pauseMenuElements["restart"].X && _game._curMouseState.X < _pauseMenuElements["restart"].X + _pauseMenuElements["restart"].Width &&
                   _game._curMouseState.Y > _pauseMenuElements["restart"].Y && _game._curMouseState.Y < _pauseMenuElements["restart"].Y + _pauseMenuElements["restart"].Height)
                {
                    ButtonDepress(_pauseMenuElements["restart"]);
                }
                #endregion

                #region Button Release
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
                    // Restart Level and close menu
                    _game.SetupLevel(_game._levelNumber);
                    _game.State = STATE.PLAY;
                }
                #endregion
            }
            #endregion
            #region Instructions
            else if (_game.State == STATE.INSTRUCTIONS)
            {
                #region Button Click
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _instructionsButtons["back"].X && _game._curMouseState.X < _instructionsButtons["back"].X + _instructionsButtons["back"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["back"].Y && _game._curMouseState.Y < _instructionsButtons["back"].Y + _instructionsButtons["back"].Height)
                {
                    ButtonDepress(_instructionsButtons["back"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
               _game._curMouseState.X > _instructionsButtons["overview"].X && _game._curMouseState.X < _instructionsButtons["overview"].X + _instructionsButtons["overview"].Width &&
               _game._curMouseState.Y > _instructionsButtons["overview"].Y && _game._curMouseState.Y < _instructionsButtons["overview"].Y + _instructionsButtons["overview"].Height)
                {
                    ButtonDepress(_instructionsButtons["overview"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _instructionsButtons["egg"].X && _game._curMouseState.X < _instructionsButtons["egg"].X + _instructionsButtons["egg"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["egg"].Y && _game._curMouseState.Y < _instructionsButtons["egg"].Y + _instructionsButtons["egg"].Height)
                {
                    ButtonDepress(_instructionsButtons["egg"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _instructionsButtons["skateboard"].X && _game._curMouseState.X < _instructionsButtons["skateboard"].X + _instructionsButtons["skateboard"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["skateboard"].Y && _game._curMouseState.Y < _instructionsButtons["skateboard"].Y + _instructionsButtons["skateboard"].Height)
                {
                    ButtonDepress(_instructionsButtons["skateboard"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _instructionsButtons["jack"].X && _game._curMouseState.X < _instructionsButtons["jack"].X + _instructionsButtons["jack"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["jack"].Y && _game._curMouseState.Y < _instructionsButtons["jack"].Y + _instructionsButtons["jack"].Height)
                {
                    ButtonDepress(_instructionsButtons["jack"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _instructionsButtons["other"].X && _game._curMouseState.X < _instructionsButtons["other"].X + _instructionsButtons["other"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["other"].Y && _game._curMouseState.Y < _instructionsButtons["other"].Y + _instructionsButtons["other"].Height)
                {
                    ButtonDepress(_instructionsButtons["other"]);
                }
                #endregion

                #region Button Release
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.LeftButton == ButtonState.Released &&
                   _game._curMouseState.X > _instructionsButtons["back"].X && _game._curMouseState.X < _instructionsButtons["back"].X + _instructionsButtons["back"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["back"].Y && _game._curMouseState.Y < _instructionsButtons["back"].Y + _instructionsButtons["back"].Height)
                {
                    _game.State = _game.PrevState;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.LeftButton == ButtonState.Released &&
                   _game._curMouseState.X > _instructionsButtons["overview"].X && _game._curMouseState.X < _instructionsButtons["overview"].X + _instructionsButtons["overview"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["overview"].Y && _game._curMouseState.Y < _instructionsButtons["overview"].Y + _instructionsButtons["overview"].Height)
                {
                    _instructionsButtons["image"].Image = _overview_menu_texture;
                    _instructionsButtons["text"].Image = _overview_text_texture;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.LeftButton == ButtonState.Released &&
                   _game._curMouseState.X > _instructionsButtons["egg"].X && _game._curMouseState.X < _instructionsButtons["egg"].X + _instructionsButtons["egg"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["egg"].Y && _game._curMouseState.Y < _instructionsButtons["egg"].Y + _instructionsButtons["egg"].Height)
                {
                    _instructionsButtons["image"].Image = _eggbeater_menu_texture;
                    _instructionsButtons["text"].Image = _eggbeater_text_texture;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.LeftButton == ButtonState.Released &&
                   _game._curMouseState.X > _instructionsButtons["skateboard"].X && _game._curMouseState.X < _instructionsButtons["skateboard"].X + _instructionsButtons["skateboard"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["skateboard"].Y && _game._curMouseState.Y < _instructionsButtons["skateboard"].Y + _instructionsButtons["skateboard"].Height)
                {
                    _instructionsButtons["image"].Image = _skateboard_menu_texture;
                    _instructionsButtons["text"].Image = _skateboard_text_texture;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.LeftButton == ButtonState.Released &&
                   _game._curMouseState.X > _instructionsButtons["jack"].X && _game._curMouseState.X < _instructionsButtons["jack"].X + _instructionsButtons["jack"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["jack"].Y && _game._curMouseState.Y < _instructionsButtons["jack"].Y + _instructionsButtons["jack"].Height)
                {
                    _instructionsButtons["image"].Image = _jack_inthe_box_menu_texture;
                    _instructionsButtons["text"].Image = _jack_inthe_box_text_texture;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.LeftButton == ButtonState.Released &&
                   _game._curMouseState.X > _instructionsButtons["other"].X && _game._curMouseState.X < _instructionsButtons["other"].X + _instructionsButtons["other"].Width &&
                   _game._curMouseState.Y > _instructionsButtons["other"].Y && _game._curMouseState.Y < _instructionsButtons["other"].Y + _instructionsButtons["other"].Height)
                {
                    _instructionsButtons["image"].Image = _other_objects_menu_texture;
                    _instructionsButtons["text"].Image = _other_objects_text_texture;
                }
                #endregion
            }
            #endregion
            #region Game Over
            else if (_game.State == STATE.GAME_OVER)
            {
                #region Button Click
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _gameOverButtons["restart"].X && _game._curMouseState.X < _gameOverButtons["restart"].X + _gameOverButtons["restart"].Width &&
                   _game._curMouseState.Y > _gameOverButtons["restart"].Y && _game._curMouseState.Y < _gameOverButtons["restart"].Y + _gameOverButtons["restart"].Height)
                {
                    ButtonDepress(_gameOverButtons["restart"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _gameOverButtons["main"].X && _game._curMouseState.X < _gameOverButtons["main"].X + _gameOverButtons["main"].Width &&
                   _game._curMouseState.Y > _gameOverButtons["main"].Y && _game._curMouseState.Y < _gameOverButtons["main"].Y + _gameOverButtons["main"].Height)
                {
                    ButtonDepress(_gameOverButtons["main"]);
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                   _game._curMouseState.X > _gameOverButtons["quit"].X && _game._curMouseState.X < _gameOverButtons["quit"].X + _gameOverButtons["quit"].Width &&
                   _game._curMouseState.Y > _gameOverButtons["quit"].Y && _game._curMouseState.Y < _gameOverButtons["quit"].Y + _gameOverButtons["quit"].Height)
                {
                    ButtonDepress(_gameOverButtons["quit"]);
                }
                #endregion

                #region Button Release
                if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                  _game._curMouseState.LeftButton == ButtonState.Released &&
                  _game._curMouseState.X > _gameOverButtons["restart"].X && _game._curMouseState.X < _gameOverButtons["restart"].X + _gameOverButtons["restart"].Width &&
                  _game._curMouseState.Y > _gameOverButtons["restart"].Y && _game._curMouseState.Y < _gameOverButtons["restart"].Y + _gameOverButtons["restart"].Height)
                {
                    // Restart the level
                    _game.SetupLevel(_game._levelNumber);
                    _game.State = STATE.PLAY;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                  _game._curMouseState.LeftButton == ButtonState.Released &&
                  _game._curMouseState.X > _gameOverButtons["main"].X && _game._curMouseState.X < _gameOverButtons["main"].X + _gameOverButtons["main"].Width &&
                  _game._curMouseState.Y > _gameOverButtons["main"].Y && _game._curMouseState.Y < _gameOverButtons["main"].Y + _gameOverButtons["main"].Height)
                {
                    // Go to the main menu
                    _game.State = STATE.MAIN_MENU;
                }
                else if (_game._prevMouseState.LeftButton == ButtonState.Pressed &&
                  _game._curMouseState.LeftButton == ButtonState.Released &&
                  _game._curMouseState.X > _gameOverButtons["quit"].X && _game._curMouseState.X < _gameOverButtons["quit"].X + _gameOverButtons["quit"].Width &&
                  _game._curMouseState.Y > _gameOverButtons["quit"].Y && _game._curMouseState.Y < _gameOverButtons["quit"].Y + _gameOverButtons["quit"].Height)
                {
                    // Quit
                    _game.Exit();
                }
                #endregion
            }
            #endregion

            // If a button is depressed and the mouse gets released, reset button
            if (buttonDepressed == true && _game._prevMouseState.LeftButton == ButtonState.Released)
            {
                depressedButton.X -= BUTTON_CLICK_FACTOR;
                depressedButton.Y -= BUTTON_CLICK_FACTOR;
                buttonDepressed = false;
                depressedButton = null;
            }
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
            else if (_game.State == STATE.GAME_OVER)
            {
                foreach (Button b in _gameOverButtons.Values)
                    b.Draw(spriteBatch);
            }

            MessageLayer.Draw(spriteBatch);
        }

        /// <summary>
        /// Visually depress a button and set flags
        /// </summary>
        /// <param name="b"></param>
        public void ButtonDepress(Button b)
        {
            if (buttonDepressed == false)
            {
                b.X += BUTTON_CLICK_FACTOR;
                b.Y += BUTTON_CLICK_FACTOR;
                buttonDepressed = true;
                depressedButton = b;
            }
        }
    }
}