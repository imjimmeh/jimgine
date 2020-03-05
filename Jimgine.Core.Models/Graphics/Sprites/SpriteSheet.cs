using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics.Sprites
{
    public class SpriteSheet : IDisposable
    {
        public Texture2D Image;
        public string FileName;

        public SpriteSheet(Texture2D image)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void Dispose()
        {
            Image.Dispose();
        }
    }
}