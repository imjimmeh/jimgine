using Jimgine.Core.Models.Graphics.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Core
{
    public interface IClickable
    {
        bool IntersectsMouseCoordinates(Point mouseCoordinates, Point groupPoint);

        bool HasEvent { get; }

        Action<object> Event { get;  }
    }
}
