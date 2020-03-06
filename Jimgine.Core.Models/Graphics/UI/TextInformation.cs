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

        public void Draw(ref SpriteBatch spriteBatch, ref SpriteFont font)
        {
            spriteBatch.DrawString(font, _text, _position, _colour);
        }

    }
}
