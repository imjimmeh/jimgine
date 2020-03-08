using Jimgine.Core.Models.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Jimgine.Core.Graphics.UI
{
    public class UIComponentFactory
    {
        Action<IUIComponent> _addUiComponent;
        Dictionary<string, SpriteFont> _fonts { get; set; }

        public UIComponentFactory(Action<IUIComponent> addUiComponent, ref Dictionary<string, SpriteFont> fonts)
        {
            _addUiComponent = addUiComponent;
            _fonts = fonts;
        }

        public IUIComponent CreateText(Vector2 position, float size, string text, Color colour, string font)
        {
            var newText = new TextInformation(position, size, text, colour, _fonts[font]);
            _addUiComponent(newText);
            return newText;
        }
    }
}
