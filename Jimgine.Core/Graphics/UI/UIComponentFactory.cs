using Jimgine.Core.Models.Graphics.UI;
using Jimgine.Core.Models.Graphics.UI.Components;
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
        readonly GraphicsDevice _graphicsDevice;

        public UIComponentFactory(Action<UIComponent> addUiComponent, ref Dictionary<string, SpriteFont> fonts, GraphicsDevice graphicsDevice)
        {
            _addUiComponent = addUiComponent;
            _fonts = fonts;
            _graphicsDevice = graphicsDevice;
        }

        public IUIComponent CreateText(Point position, int size, string text, Color colour, string font, bool isMovable)
        {
            var newText = new UIText(position, size, text, colour, _fonts[font], isMovable);
            _addUiComponent(newText);
            return newText;
        }

        public IUIComponent CreateButton(int width, int height, Color colour, Vector2 position)
        {
            return new UIButton(CreateRectangle(ref width, ref height, ref colour), position);
        }

        Texture2D CreateRectangle(ref int width, ref int height, ref Color colour)
        {
            var textureRectangle = new Texture2D(_graphicsDevice, width, height);
            var colourData = new Color[width * height];

            for(var x = 0; x < colourData.Length; x++)
            {
                colourData[x] = colour;
            }

            textureRectangle.SetData(colourData);
            return textureRectangle;
        }
    }
}
