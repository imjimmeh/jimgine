using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics.Sprites
{
    [Serializable]
    public struct SpriteData
    {
        public Rectangle Area;
        public string Name;
        public Color TransparencyColour;
        public string TexturePath;

        public SpriteData(Rectangle area, string name, Color transparencyColour, string texturePath)
        { Area = area; Name = name; TransparencyColour = transparencyColour; TexturePath = texturePath; }
    }
}
