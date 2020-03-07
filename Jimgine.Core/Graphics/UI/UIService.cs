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
        SpriteBatch _spriteBatch;

        //shit we're looking after
        Dictionary<string, SpriteFont> _fonts;
        UIComponentFactory _componentFactory;
        List<IUIComponent>uiComponents;

        public UIComponentFactory ComponentFactory { get => _componentFactory; }
        #endregion

        #region Constructor
        public UIService(SpriteBatch spriteBatch)
        {
            this._spriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch));

            this.Initialise();

            var defaultFont = LoadFont("default", "Base/fonts/Default");
        }
        #endregion

        #region IGameService methods
        public void Initialise()
        {
            _fonts = new Dictionary<string, SpriteFont>(20);
            uiComponents = new List<IUIComponent>(1000); //randm number for now, will need optimising soon for sure but for now these can stay like this
            _componentFactory = new UIComponentFactory(AddUIComponent, ref _fonts);

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
                    uiComponents[x].Draw(ref _spriteBatch);
                }
            }
        }
        #endregion

        public SpriteFont LoadFont(string fontName, string fontPath)
        {
            var newFont = ContentService.LoadContent<SpriteFont>(fontPath);
            _fonts.Add(fontName, newFont);
            return newFont;
        }

        public void AddUIComponent(IUIComponent component)
        {
            uiComponents.Add(component);
        }
    }
}
