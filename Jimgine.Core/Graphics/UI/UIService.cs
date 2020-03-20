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
        GraphicsDevice _graphicsDevice;

        //stuff the service monitors
        Dictionary<string, SpriteFont> _fonts;
        UIComponentFactory _componentFactory;
        List<UIGroup> _uiGroups;

        Vector2? _anchor; //TODO: Sort this - move somewhere better

        public UIComponentFactory ComponentFactory { get => _componentFactory; }
        #endregion

        #region Constructor
        public UIService(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            this._spriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch));
            this._graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.Initialise();

            //TODO: Sort this
            var defaultFont = LoadFont("default", "Base/fonts/Default");
        }
        #endregion

        #region IGameService methods
        public void Initialise()
        {
            _fonts = new Dictionary<string, SpriteFont>(20);
            _uiGroups = new List<UIGroup>(100); //TODO: randm number for now, will need optimising soon for sure but for now these can stay like this
            _componentFactory = new UIComponentFactory(AddUIGroup, ref _fonts, _graphicsDevice);
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            for(var x = 0; x < _uiGroups.Count; x++)
            {
                if(_uiGroups[x] != null)
                {
                    _uiGroups[x].Draw(ref _spriteBatch);
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

        public void AddUIGroup(UIGroup group)
        {
            _uiGroups.Add(group);
        }

        public IEnumerable<UIComponent> GetInteractingUIComponents(Point clickPosition, bool? movableOnly = null)
        {
            for(var x = 0; x < _uiGroups.Count; x++)
            {
                if(_uiGroups[x] != null)
                {
                    for (var y = 0; y < _uiGroups[x].UIComponents.Length; y++)
                    {
                        if (_uiGroups[x].UIComponents[y] == null)
                            continue;

                        if (movableOnly.HasValue && movableOnly == true && !_uiGroups[x].UIComponents[y].IsMovable)
                            continue;

                        if (!_uiGroups[x].UIComponents[y].IntersectsMouseCoordinates(_uiGroups[x].Position, clickPosition))
                            continue;

                        yield return _uiGroups[x].UIComponents[y];
                    }
                }
            }
        }
    }
}