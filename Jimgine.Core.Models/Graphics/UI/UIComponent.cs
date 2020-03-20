using Jimgine.Core.Models.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawableUI = Jimgine.Core.Models.Core.IDrawableUI;

namespace Jimgine.Core.Models.Graphics.UI
{
    public abstract class UIComponent : IUIComponent, IDrawableUI, IClickable, IMovable
    {
        UIGroup _owner;
        public UIGroup Owner => _owner;

        internal bool _isMovable { get; set; }
        public bool IsMovable => _isMovable;

        internal Point _position { get; set; }


        public Point DrawablePosition => _position;

        bool _visible;
        public bool Visible => _visible;

        internal bool _hasEvent;
        public bool HasEvent => _hasEvent;

        internal Action<object> _event;
        public Action<object> Event => _event;

        public UIComponent()
        {
            _visible = true;
        }

        public void SetPosition(Point position)
        {
            _position = position;
        }

        public void SetOwner(UIGroup owner)
        {
            _owner = owner;
        }

        public abstract Rectangle GetSize(Point groupPoint);

        public abstract void SetValue<T>(T value);

        public bool IntersectsMouseCoordinates(Point groupPoint, Point mouseCoordinates)
        {
            return GetSize(groupPoint).Contains(mouseCoordinates);
        }

        public abstract void Draw(ref SpriteBatch spriteBatch, Point groupPosition);

        public void MoveTo(Point target)
        {
            _position = target;
        }

        public void SetVisible(bool visible)
        {
            _visible = visible;
        }
    }
}
