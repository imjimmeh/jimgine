using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics.UI.Components
{
    public class UIButton : UIComponent
    {
        Texture2D _texture;
        Action _action;

        public override Rectangle Size => _texture.Bounds;

        public UIButton(Texture2D texture, Vector2 position)
        {
            _position = position.ToPoint();
            _texture = texture;
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position.ToVector2(), Color.White);
        }

        public override void SetValue<T>(T value)
        {
            if (value.GetType() != typeof(Action))
                throw new InvalidCastException();

            _action = value as Action;
        }
    }
}
