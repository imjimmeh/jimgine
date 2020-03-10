using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Core
{
    public interface IMovable
    {
        bool IsMovable { get; }

        void MoveTo(Point target);
    }
}
