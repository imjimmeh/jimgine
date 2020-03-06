using Jimgine.Core.Content;
using Jimgine.Core.Models.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Graphics.UI
{
    public class UIService : IGameService
    {
        #region Fields/Methods
        SpriteBatch spriteBatch;

        Dictionary<string, SpriteFont> fonts;

        List<IUIComponent> uiComponents;
        #endregion

        #region Constructor

        public UIService(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch));

            this.Initialise();
        }
        #endregion

        #region IGameService methods
        public void Initialise()
        {
            fonts = new Dictionary<string, SpriteFont>(20);
            uiComponents = new List<IUIComponent>(1000);
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            for(var x = 0; x < uiComponents.Count; x++)
            {
                if(uiComponents[x] != null)
                {
                    uiComponents[x].Draw(ref spriteBatch);
                }
            }
        }
        #endregion

        public void LoadFont(string fontName, string fontPath)
        {
            fonts.Add(fontName, ContentService.LoadContent<SpriteFont>(fontPath));
        }

        public void AddUIComponent(IUIComponent component)
        {
            uiComponents.Add(component);
        }

        #region Private methods

        #endregion
    }
}
