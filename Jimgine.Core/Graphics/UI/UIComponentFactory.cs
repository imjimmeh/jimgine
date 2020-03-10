using Jimgine.Core.Models.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Jimgine.Core.Graphics.UI
{
    public class UIComponentFactory
    {
        Action<UIComponent> _addUiComponent;
        Dictionary<string, SpriteFont> _fonts { get; set; }

        public UIComponentFactory(Action<UIComponent> addUiComponent, ref Dictionary<string, SpriteFont> fonts)
        {
            _addUiComponent = addUiComponent;
            _fonts = fonts;
        }

        public IUIComponent CreateText(Point position, int size, string text, Color colour, string font, bool isMovable)
        {
            var newText = new UIText(position, size, text, colour, _fonts[font], isMovable);
            _addUiComponent(newText);
            return newText;
        }
    }
}
