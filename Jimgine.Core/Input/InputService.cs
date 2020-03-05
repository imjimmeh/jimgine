using Jimgine.Core.Models.Commands;
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
        List<KeyboardInputContainer> keyboardInputs;

        Keys[] currentlyPressedKeys;
        Keys[] lastPressedKeys;
        KeyboardState currentKeyboardState;
        KeyboardState lastKeyboardState;

        public InputService(Action exit)
        {
            this.exit = exit;
            Initialise();
        }

        #region IGameService Methods
        public void Initialise()
        {
            keyboardInputs = new List<KeyboardInputContainer>(50);
            var command = new ActionCommand(exit);

            keyboardInputs.Add(new KeyboardInputContainer(Keys.Escape, command));
            SetKeyboardInput(true);
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            CheckForInput(gameTime);
        }
        #endregion
        #region Methods
        public void AddInput(KeyboardInputContainer input)
        {
            keyboardInputs.Add(input);
        }
        void CheckForInput(GameTime gameTime)
        {
            lastPressedKeys = currentlyPressedKeys;
            currentlyPressedKeys = currentKeyboardState.GetPressedKeys();

            //Need to check last pressed keys that aren't pressed now
            // and activate their "on NOT PRESSED" action (which doesnt exist yet)
            if (currentlyPressedKeys.Length == 0)
            {
                ExecuteNoInputCommand();
            }
            else
            {
                ActionKeyboardInputs();
            }

            if(lastPressedKeys?.Length > 0)
            {
                ActionKeyboardInputFinishedCommands();
            }
        }

        private void ActionKeyboardInputFinishedCommands()
        {
            for(var x = 0; x < lastPressedKeys.Length; x++)
            {
                for(var y = 0; y < keyboardInputs.Count; y++)
                {
                    if (keyboardInputs[y] == null || keyboardInputs[y].InputFinishCommand == null)
                        continue;

                    if (keyboardInputs[y].MonitoredKey == lastPressedKeys[x])
                    {
                        bool currentlyPressed = false;
                        for(var z = 0; z < currentlyPressedKeys.Length; z++)
                        {
                            if (currentlyPressedKeys[z] == lastPressedKeys[x])
                            {
                                currentlyPressed = true;
                                break;
                            }
                        }

                        if(!currentlyPressed)
                        {
                            keyboardInputs[y].InputFinishCommand.Execute(keyboardInputs[y].InputCommand);
                        }
                    }
                }
            }
        }

        private void ActionKeyboardInputs()
        {
            for (var x = 0; x < currentlyPressedKeys.Length; x++)
            {
                for (var y = 0; y < keyboardInputs.Count; y++)
                {
                    if (keyboardInputs[y] == null)
                        break;

                    if (keyboardInputs[y].MonitoredKey == currentlyPressedKeys[x])
                        keyboardInputs[y].InputCommand.Execute(keyboardInputs[y].InputCommand);
                }
            }
        }

        private void ExecuteNoInputCommand()
        {
            for (var x = 0; x < keyboardInputs.Count; x++)
            {
                if (keyboardInputs[x].MonitoredKey == Keys.None)
                    keyboardInputs[x].InputCommand.Execute(null);
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
