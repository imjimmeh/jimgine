using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Jimgine.Core.Manager.State;
using Jimgine.Core.Models.Graphics.Sprites;
using System.Collections.Generic;
using Jimgine.Core.Content;
using Jimgine.Core.Models.World.Characters;

namespace Jimgine.Core.Graphics
{
    public class GraphicsService : IGameService
    {
        #region fields/properties
        GraphicsDeviceManager graphics;
        GraphicsDevice graphicsDevice;
        StateManager stateManager;
        SpriteBatch spriteBatch;

        Dictionary<string, Texture2D> sprites;
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
            LoadContent();
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

            spriteBatch.End();
        }

        private void DrawTerrain()
        {
            //TODO
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
            spriteBatch.Draw(sprites[stateManager.Player.GetSpriteData().TexturePath], new Vector2(stateManager.Player.Position.X, stateManager.Player.Position.Y), stateManager.Player.GetSpriteData().Area, Color.White);
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
