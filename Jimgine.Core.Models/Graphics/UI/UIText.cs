using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics.UI
{
    public class UIText : UIComponent
    {
        string _text;
        public string Text { get => _text; }

        Color _colour;
        public Color Colour { get => _colour; }

        SpriteFont _font;
        Point _stringSize => _font.MeasureString(Text).ToPoint();

        public SpriteFont Font { get => _font; }

        int _textSize;

        public override Rectangle Size => new Rectangle(_position, _stringSize);

        public UIText(Point position, int size, string text, Color colour, SpriteFont font, bool isMovable)
        {
            _position = position;
            _textSize = size;
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _colour = colour;
            _font = font ?? throw new ArgumentNullException(nameof(font));
            _isMovable = isMovable;
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _position.ToVector2(), _colour);
        }

        public void SetFont(SpriteFont font)
        {
            _font = font;
        }

        public void SetText(string text)
        {
            _text = text;
        }

        public override void SetValue<T>(T value)
        {
            _text = value.ToString();
        }
    }
}
