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
        bool IsMovable { get; }
        Rectangle Size { get; }
        void SetValue<T>(T value);
        void SetPosition(Point position);
    }
}
