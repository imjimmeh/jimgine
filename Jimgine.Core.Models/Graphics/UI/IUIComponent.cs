using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics.UI
{
    public interface IUIComponent
    {
        Vector2 Position { get; }
        float Size { get; }
        void Draw(ref SpriteBatch spriteBatch);
        void SetValue<T>(T value);
        void SetPosition(Vector2 position);
        bool IntersectsMouseCoordinates(Point mouseCoordinates);
    }
}
