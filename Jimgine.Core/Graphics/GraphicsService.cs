using Jimgine.Core.Content;
using Jimgine.Core.Graphics.UI;
using Jimgine.Core.Manager.State;
using Jimgine.Core.Models.Graphics.UI;
using Jimgine.Core.Models.World.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Jimgine.Core.Graphics
{
    public class GraphicsService : IGameService
    {
        #region fields/properties
        GraphicsDeviceManager graphics;
        GraphicsDevice graphicsDevice;
        StateManager stateManager;
        SpriteBatch spriteBatch;
        
        UIService _uiService;
        public UIService UIService => _uiService;

        Dictionary<string, Texture2D> sprites;

        UIComponentFactory _uiComponentFactory;
        public UIComponentFactory UIComponentFactory => _uiComponentFactory;
        #endregion

        #region constructor
        public GraphicsService(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, StateManager stateManager)
        {
            this.graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            this.graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.stateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));

            Initialise();
        }
        #endregion

        #region IGameService methods
        public void Initialise()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            sprites = new Dictionary<string, Texture2D>();
            _uiService = new UIService(spriteBatch);
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
            graphicsDevice.Clear(Color.Aquamarine);
            spriteBatch.Begin();

            DrawTerrain();
            DrawPlayer();
            DrawCharacters();
            _uiService.Update(gameTime);

            spriteBatch.End();
        }

        private void DrawTerrain()
        {
            foreach (var tile in stateManager.GetTilesToDraw())
            {
                if (tile.Item1 == null)
                    continue;

                spriteBatch.Draw(
                    sprites[tile.Item1.Image.TexturePath],
                    stateManager.CameraService.GetVisualPosition(tile.Item2.X, tile.Item2.Y),
                    tile.Item1.Image.Area,
                    Color.White);
            }
        }

        private void DrawCharacters()
        {
            foreach (Character character in stateManager.GetCharacters())
            {
                if (character == null)
                    continue;

                spriteBatch.Draw(sprites[character.GetSpriteData().TexturePath], new Vector2(character.Position.X, character.Position.Y), character.GetSpriteData().Area, Color.White);
            }
        }

        private void DrawPlayer()
        {
            spriteBatch.Draw(sprites[stateManager.Player.GetSpriteData().TexturePath], stateManager.GetPlayerPosition(), stateManager.Player.GetSpriteData().Area, Color.White);
        }
        #endregion

        public void LoadTextures(IEnumerable<string> paths)
        {
            foreach(var path in paths)
            {
                sprites[path] = ContentService.LoadContent<Texture2D>(path);
            }
        }

        public void AddUIComponent(UIComponent component)
        {
            _uiService.AddUIComponent(component);
        }
    }
}
