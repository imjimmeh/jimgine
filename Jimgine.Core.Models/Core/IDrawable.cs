﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Core
{
    public interface IDrawable
    {
        Point DrawablePosition { get; }
        void Draw(ref SpriteBatch spriteBatch);
    }
}
