using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Jimgine.Core.Models.Graphics.UI.Components
{
    public class UIButton : UIComponent
    {
        Texture2D _texture;

        public UIButton(Texture2D texture, Vector2 position)
        {
            _position = position.ToPoint();
            _texture = texture;
        }

        public override void Draw(ref SpriteBatch spriteBatch, Point groupPosition)
        {
            if (!Visible)
                return;

            spriteBatch.Draw(_texture, (_position + groupPosition).ToVector2(), Color.White);
        }

        public override void SetValue<T>(T value)
        {
            if (value != null)
            {
                _event = value as Action<object>;
                _hasEvent = true;
            }
        }

        public override Rectangle GetSize(Point groupPoint)
        {
            var size = _texture.Bounds;
            size.X += groupPoint.X + _position.X;
            size.Y += groupPoint.Y + _position.Y;

            return size;
        }
    }
}
