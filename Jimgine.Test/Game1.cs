using Jimgine.Core.Manager;
using Jimgine.Core.Manager.State.Levels;
using Jimgine.Core.Models.Commands;
using Jimgine.Core.Models.Content.Levels;
using Jimgine.Core.Models.Graphics.Sprites;
using Jimgine.Core.Models.Graphics.UI;
using Jimgine.Core.Models.Input;
using Jimgine.Core.Models.Levels;
using Jimgine.Test.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Jimgine.Test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GameManager gameManager;
        IUIComponent health;
        UIService _uiService;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            var level = LevelFactory.GetLevel("Base\\Levels\\1\\", "levelOneBase.json");
            this.IsMouseVisible = true;
            gameManager = new GameManager(graphics, GraphicsDevice, Exit, Content);
            gameManager.InitialiseFromConfig(@"Base\game.json");
            gameManager.LoadLevel(level);

            //Testing stuff
            var lowerPlayersHealth = new Action(delegate ()
            {
                gameManager.StateManager.Player.AddHealth(-5);
            });

            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Escape, new ActionCommand(Exit)));

            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.D, new ActionCommand(lowerPlayersHealth)));

            gameManager.StateManager.Player.AddHealthChangedEvent(Player_HealthChanges);

            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Left, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingLeft(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingLeft(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Right, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingRight(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingRight(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Up, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingUp(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingUp(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Down, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingDown(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingDown(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.None, new ActionCommand(delegate () { gameManager.StateManager.Player.SetNotMoving(); })));

            gameManager.InputService.AddInput(new MouseInputContainer(MouseButton.Left, ButtonState.Pressed, new ActionCommand(delegate ()
            {
                foreach (var uiElement in gameManager.InputService.GetInteractingUIComponents())
                {
                    if(uiElement.IsMovable)
                        gameManager.InputService.MoveUIComponentToMouse(uiElement);

                    if (uiElement.HasEvent && uiElement.Visible)
                        uiElement.Event(uiElement);
                }
            })));

            _uiService = new UIService(gameManager.GraphicsService.UIComponentFactory, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _uiService.Initialise();
            health = _uiService.CreatePlayerStatsBar(gameManager.StateManager.Player);
        }

        protected override void LoadContent()
        {
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void Player_HealthChanges(object sender, EventArgs e)
        {
            health.SetValue<string>($"Health: {e}");
        }
    }
}
