using Jimgine.Core.Models.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = Jimgine.Core.Models.Core.IDrawable;

namespace Jimgine.Core.Models.Graphics.UI
{
    public abstract class UIComponent : IUIComponent, IDrawable, IClickable, IMovable
    {
        internal bool _isMovable { get; set; }
        public bool IsMovable => _isMovable;

        internal Point _position { get; set; }

        public abstract Rectangle Size { get; }

        public Point DrawablePosition => _position;

        public void SetPosition(Point position)
        {
            _position = position;
        }

        public abstract void SetValue<T>(T value);

        public bool IntersectsMouseCoordinates(Point mouseCoordinates)
        {
            return Size.Contains(mouseCoordinates);
        }

        public abstract void Draw(ref SpriteBatch spriteBatch);

        public void MoveTo(Point target)
        {
            _position = target;
        }
    }
}
