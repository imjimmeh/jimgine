using Jimgine.Core.Graphics.UI;
using Jimgine.Core.Models.Commands;
using Jimgine.Core.Models.Graphics.UI;
using Jimgine.Core.Models.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Jimgine.Core.Input
{
    public class InputService : IGameService
    {
        Action exit;
        List<KeyboardInputContainer> _keyboardInputs;
        List<MouseInputContainer> _mouseInputs;

        Keys[] _currentlyPressedKeys;
        Keys[] _lastPressedKeys;
        KeyboardState _currentKeyboardState;
        KeyboardState _lastKeyboardState;

        MouseState _lastMouseState;
        MouseState _currentMouseState;

        UIService _uiService;

        public InputService(Action exit)
        {
            this.exit = exit;
            Initialise();
        }

        #region IGameService Methods
        public void Initialise()
        {
            _keyboardInputs = new List<KeyboardInputContainer>(50);
            _mouseInputs = new List<MouseInputContainer>(50);

            var command = new ActionCommand(exit);

            SetKeyboardInput(true);
            _currentMouseState = Mouse.GetState();
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            _lastKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
            CheckForInput(gameTime);
        }
        #endregion

        #region Methods
        public void AddUIService(UIService uiService)
        {
            _uiService = uiService;
        }

        public void AddInput(KeyboardInputContainer input)
        {
            _keyboardInputs.Add(input);
        }

        public void AddInput(MouseInputContainer input)
        {
            _mouseInputs.Add(input);
        }

        public Vector2 GetMousePosition()
        {
            return new Vector2(_currentMouseState.X, _currentMouseState.Y);
        }

        public IEnumerable<IUIComponent> GetInteractingUIComponents()
        {
            return _uiService.GetInteractingUIComponents(_currentMouseState.Position);
        }

        void CheckForInput(GameTime gameTime)
        {
            CheckKeyboardInput();
            CheckMouseInput();
        }

        private void CheckMouseInput()
        {
            _lastMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            for(var x = 0; x < _mouseInputs.Count; x++)
            {
                if(_mouseInputs[x] == null)
                {
                    continue;
                }

                if (_currentMouseState.LeftButton == ButtonState.Pressed && _mouseInputs[x].MonitoredButton == MouseButton.Left && _mouseInputs[x].MonitoredButtonState == ButtonState.Pressed)
                {
                    _mouseInputs[x].InputCommand?.Execute(_mouseInputs[x].InputCommand);
                }
                else if (_currentMouseState.RightButton == ButtonState.Pressed && _mouseInputs[x].MonitoredButton == MouseButton.Right && _mouseInputs[x].MonitoredButtonState == ButtonState.Pressed)
                {
                    _mouseInputs[x].InputCommand?.Execute(_mouseInputs[x].InputCommand);
                }
            }
        }

        private void CheckKeyboardInput()
        {
            _lastPressedKeys = _currentlyPressedKeys;
            _currentlyPressedKeys = _currentKeyboardState.GetPressedKeys();

            if (_currentlyPressedKeys.Length == 0)
            {
                ExecuteNoInputCommand();
            }
            else
            {
                ActionKeyboardInputs();
            }
            if (_lastPressedKeys?.Length > 0)
            {
                ActionKeyboardInputFinishedCommands();
            }
        }

        /// <summary>
        /// Actions any exit commands for inputs that are no longer being pressed
        /// </summary>

        //TODO: Optimise lol
        private void ActionKeyboardInputFinishedCommands()
        {
            //go through all the last pressed keys, then find matching keyboard input in the array. 
            //If there's an input finished command, we look through all the urrently pressed keys to see if it is still bieng pressed - if not action the command
            for(var x = 0; x < _lastPressedKeys.Length; x++)
            {
                for(var y = 0; y < _keyboardInputs.Count; y++)
                {
                    if (_keyboardInputs[y] == null || _keyboardInputs[y].InputFinishCommand == null)
                        continue;

                    if (_keyboardInputs[y].MonitoredKey == _lastPressedKeys[x])
                    {
                        bool currentlyPressed = false;
                        for(var z = 0; z < _currentlyPressedKeys.Length; z++)
                        {
                            if (_currentlyPressedKeys[z] == _lastPressedKeys[x])
                            {
                                currentlyPressed = true;
                                break;
                            }
                        }

                        if(!currentlyPressed)
                        {
                            _keyboardInputs[y].InputFinishCommand.Execute(_keyboardInputs[y].InputCommand);
                        }
                    }
                }
            }
        }

        private void ActionKeyboardInputs()
        {
            for (var x = 0; x < _currentlyPressedKeys.Length; x++)
            {
                for (var y = 0; y < _keyboardInputs.Count; y++)
                {
                    if (_keyboardInputs[y] == null)
                        break;

                    if (_keyboardInputs[y].MonitoredKey == _currentlyPressedKeys[x])
                        _keyboardInputs[y].InputCommand.Execute(_keyboardInputs[y].InputCommand);
                }
            }
        }

        private void ExecuteNoInputCommand()
        {
            for (var x = 0; x < _keyboardInputs.Count; x++)
            {
                if (_keyboardInputs[x].MonitoredKey == Keys.None)
                    _keyboardInputs[x].InputCommand.Execute(null);
            }
        }

        void SetKeyboardInput(bool enable)
        {
            var keyboardType = typeof(Keyboard);
            var methodInfo = keyboardType.GetMethod("SetActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            methodInfo.Invoke(null, new object[] { enable });
        }
        #endregion
    }
}