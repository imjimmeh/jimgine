using Jimgine.Core.Content;
using Jimgine.Core.Graphics;
using Jimgine.Core.Helpers.ExtensionMethods;
using Jimgine.Core.Input;
using Jimgine.Core.Manager.State;
using Jimgine.Core.Models.Config;
using Jimgine.Core.Models.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Jimgine.Core.Manager
{
    public class GameManager : IGameService
    {
        #region fields/properties
        GraphicsDeviceManager _graphics;
        GraphicsDevice _graphicsDevice;
        IGameService[] _gameServices;
        GraphicsService _graphicsService;
        InputService _inputService;
        StateManager _stateManager;
        ContentManager _content;

        string baseConfigPath;

        Action Exit;
        #endregion

        //TODO: Front access to these nicer
        public InputService InputService { get => _inputService; }
        public StateManager StateManager { get => _stateManager; }
        public GraphicsService GraphicsService { get => _graphicsService; }

        #region constructors
        public GameManager(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, Action exit, ContentManager content)
        {
            this._graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            this._graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.Exit = exit;
            this._content = content;
            Initialise(content);
        }
        #endregion

        #region IGameService methods
        public void Initialise(ContentManager content)
        {
            _gameServices = new IGameService[20];
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
            for(var x = 0; x < _gameServices.Length; x++)
            {
                if (_gameServices[x] == null)
                    break;
                else
                    _gameServices[x].Update(gameTime);
            }
        }
        #endregion

        #region Public methods
        public void InitialiseFromConfig(string configPath)
        {
            baseConfigPath = configPath;

            var gameConfig = ContentService.LoadJsonFile<InitialGameConfig>(configPath);
        }

        public void LoadLevel(Level level)
        {
            _stateManager.LoadLevel(level);
            _graphicsService.LoadTextures(level.SpriteNames);
        }

        public void AddService(IGameService gameService)
        {
            _gameServices[_gameServices.GetFirstEmptyIndexAndCreateIfNone()] = gameService;
        }
        #endregion

        void InitiateGraphicsService()
        {
            _graphicsService = new GraphicsService(_graphics, _graphicsDevice, _stateManager);
            _gameServices[_gameServices.GetFirstEmptyIndexAndCreateIfNone()] = _graphicsService;
        }

        void InitialiseInputService()
        {
            _inputService = new InputService(Exit);
            _gameServices[_gameServices.GetFirstEmptyIndexAndCreateIfNone()] = _inputService;
        }

        public void Initialise()
        {
            InitialiseInputService();
            ContentService.contentManager = _content;
            InitialiseStateManager();
            InitiateGraphicsService();
            LoadContent();
            InputService.AddUIService(GraphicsService.UIService);
        }


        private void InitialiseStateManager()
        {
            _stateManager = new StateManager(_inputService);
            _gameServices[_gameServices.GetFirstEmptyIndexAndCreateIfNone()] = _stateManager;
        }
    }
}
