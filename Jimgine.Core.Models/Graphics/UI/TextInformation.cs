using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics.UI
{
    public class TextInformation : IUIComponent
    {
        Vector2 _position;
        public Vector2 Position { get => _position;  }

        float _size;
        public float Size { get => _size;  }

        string _text;
        public string Text { get => _text; }

        Color _colour;
        public Color Colour { get => _colour; }

        SpriteFont _font;
        Vector2 _stringSize => _font.MeasureString(Text);

        public TextInformation(Vector2 position, float size, string text, Color colour, SpriteFont font)
        {
            _position = position;
            _size = size;
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _colour = colour;
            _font = font ?? throw new ArgumentNullException(nameof(font));
        }

        public SpriteFont Font { get => _font; }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _position, _colour);
        }

        public void SetFont(SpriteFont font)
        {
            _font = font;
        }

        public void SetText(string text)
        {
            _text = text;
        }

        public void SetValue<T>(T value)
        {
            _text = value.ToString();
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public bool IntersectsMouseCoordinates(Point mouseCoordinates)
        {
            if (mouseCoordinates.X < Position.X || mouseCoordinates.Y < Position.Y)
            {
                return false;
            }
            Rectangle stringArea = new Rectangle((int)Position.X, (int)Position.Y, (int)_stringSize.X, (int)_stringSize.Y);

            return stringArea.Contains(mouseCoordinates);
        }
    }
}
