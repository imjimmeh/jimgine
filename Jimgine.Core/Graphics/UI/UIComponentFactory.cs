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
        Action<UIGroup> _addUIGroup;
        Dictionary<string, SpriteFont> _fonts { get; set; }
        readonly GraphicsDevice _graphicsDevice;

        public UIComponentFactory(Action<UIGroup> addUIGroup, ref Dictionary<string, SpriteFont> fonts, GraphicsDevice graphicsDevice)
        {
            _addUIGroup = addUIGroup;
            _fonts = fonts;
            _graphicsDevice = graphicsDevice;
        }

        public UIGroup CreateUIGroup(UIComponent[] components, Point position)
        {
            var newGroup = new UIGroup(components, position);
            _addUIGroup.Invoke(newGroup);

            foreach(var component in components)
            {
                component.SetOwner(newGroup);
            }

            return newGroup;
        }

        public UIComponent CreateText(Point position, int size, string text, Color colour, string font, bool isMovable)
        {
            return new UIText(position, size, text, colour, _fonts[font], isMovable);
        }

        public UIComponent CreateButtonWithBlockColour(int width, int height, Color colour, Vector2 position)
        {
            return new UIButton(CreateRectangleTexture(ref width, ref height, ref colour), position);
        }

        public UIComponent CreateButtonWithImage(Texture2D texture2D, Vector2 position)
        {
            return new UIButton(texture2D, position);
        }

        Texture2D CreateRectangleTexture(ref int width, ref int height, ref Color colour)
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
