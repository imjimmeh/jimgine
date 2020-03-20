using Jimgine.Core.Content;
using Jimgine.Core.Graphics.UI;
using Jimgine.Core.Manager.State;
using Jimgine.Core.Models.Graphics;
using Jimgine.Core.Models.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Jimgine.Core.Graphics
{
    public class GraphicsService : IGameService
    {
        #region fields/properties
        GraphicsDeviceManager _graphics;
        GraphicsDevice _graphicsDevice;
        StateManager _stateManager;
        SpriteBatch _spriteBatch;
        
        UIService _uiService;
        public UIService UIService => _uiService;

        Dictionary<string, Texture2D> sprites;

        UIComponentFactory _uiComponentFactory;
        public UIComponentFactory UIComponentFactory => _uiComponentFactory;
        #endregion

        #region constructor
        public GraphicsService(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, StateManager stateManager)
        {
            this._graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            this._graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this._stateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));

            Initialise();
        }
        #endregion

        #region IGameService methods
        public void Initialise()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            sprites = new Dictionary<string, Texture2D>();
            _uiService = new UIService(_spriteBatch, _graphicsDevice);
            LoadContent();
            _uiComponentFactory = _uiService.ComponentFactory;
        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.Aquamarine);
            _spriteBatch.Begin();

            DrawTerrain();
            DrawPlayer(_stateManager.CameraService.GetPlayerDrawInformation());
            //DrawCharacters();
            _uiService.Update(gameTime);

            _spriteBatch.End();
        }

        private void DrawTerrain()
        {
            foreach(var terrain in _stateManager.CameraService.GetTerrain())
            {
                _spriteBatch.Draw(sprites[terrain.TexturePath],terrain.Location,terrain.Rectangle,Color.White);
            }
        }

        private void DrawPlayer(SpriteDrawInformation playerDrawInformation)
        {
            _spriteBatch.Draw(sprites[playerDrawInformation.TexturePath], playerDrawInformation.Location, playerDrawInformation.Rectangle, Color.White);
        }
        #endregion

        public void LoadTextures(IEnumerable<string> paths)
        {
            foreach(var path in paths)
            {
                sprites[path] = ContentService.LoadContent<Texture2D>(path);
            }
        }
    }
}
