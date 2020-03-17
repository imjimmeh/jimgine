using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Extensions.Conversions
{
    public static class ConversionExtensions
    {
        public static Point ToPoint(this Vector3 v)
        {
            return new Point((int)v.X, (int)v.Y);
        }
    }
}
