using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Jimgine.Core.Models.Core.IDrawable;

namespace Jimgine.Core.Models.Levels
{
    public class Tile : IDrawable
    {
        public Texture2D Image;
        public Point DrawablePosition => throw new System.NotImplementedException();
        public bool Walkable;

        public void Draw(ref SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}