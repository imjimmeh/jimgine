using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics
{
    public struct SpriteDrawInformation
    {
        public string TexturePath { get; set; }
        public Vector2 Location { get; set; }
        public Rectangle Rectangle { get; set; }
    }
}
