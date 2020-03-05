using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jimgine.Core.Helpers.ExtensionMethods;
using Jimgine.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jimgine.Core.Input;
using Microsoft.Xna.Framework.Content;
using Jimgine.Core.Content;
using Jimgine.Core.Models.Config;
using System.IO;
using Newtonsoft.Json;
using Jimgine.Core.Manager.State;
using Jimgine.Core.Models.Levels;
using Jimgine.Core.Models.Commands;
using Jimgine.Core.Models.Input;

namespace Jimgine.Core.Manager
{
    public class GameManager : IGameService
    {
        #region fields/properties
        GraphicsDeviceManager graphics;
        GraphicsDevice graphicsDevice;
        IGameService[] gameServices;
        GraphicsService graphicsService;
        InputService inputService;
        StateManager stateManager;
        ContentManager content;

        string baseConfigPath;

        Action Exit;
        #endregion

        #region constructors
        public GameManager(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, Action exit, ContentManager content)
        {
            this.graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            this.graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.Exit = exit;
            this.content = content;
            Initialise(content);
        }
        #endregion

        #region IGameService methods
        public void Initialise(ContentManager content)
        {
            gameServices = new IGameService[20];
            Initialise();
        }

        public void LoadContent()
        {
            
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            for(var x = 0; x < gameServices.Length; x++)
            {
                if (gameServices[x] == null)
                    break;
                else
                    gameServices[x].Update(gameTime);
            }

            /*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();*/
        }
        #endregion

        #region Public methods
        public void InitialiseFromConfig(string configPath)
        {
            baseConfigPath = configPath;

            var gameConfig = ContentService.LoadJsonFile<InitialGameConfig>(configPath);

            LoadInitialLevel(ref gameConfig.Levels);
        }

        private void LoadInitialLevel(ref LevelFileData[] levels)
        {
            for (var x = 0; x < levels.Length; x++)
            {
                if (levels[x].InitialLevel)
                {
                    var levelData = ContentService.LoadJsonFile<LevelData>(levels[x].Path);
                    stateManager.LoadLevel(levelData);
                    graphicsService.LoadTextures(stateManager.GetFilesToLoad());
                    break;
                }
            }
        }

        public void AddService(IGameService gameService)
        {
            gameServices[gameServices.GetFirstEmptyIndexAndCreateIfNone()] = gameService;
        }
        #endregion

        #region Private methods
        void InitiateGraphicsService()
        {
            graphicsService = new GraphicsService(graphics, graphicsDevice, stateManager);
            gameServices[gameServices.GetFirstEmptyIndexAndCreateIfNone()] = graphicsService;
        }

        void InitialiseInputService()
        {
            inputService = new InputService(Exit);
            gameServices[gameServices.GetFirstEmptyIndexAndCreateIfNone()] = inputService;
        }

        public void Initialise()
        {
            InitialiseInputService();
            ContentService.contentManager = content;
            InitialiseStateManager();
            InitiateGraphicsService();
            LoadContent();

            inputService.AddInput(new KeyboardInputContainer(Keys.Escape, new ActionCommand(Exit)));
        }

        private void InitialiseStateManager()
        {
            stateManager = new StateManager(inputService);
            gameServices[gameServices.GetFirstEmptyIndexAndCreateIfNone()] = stateManager;
        }
        #endregion
    }
}
