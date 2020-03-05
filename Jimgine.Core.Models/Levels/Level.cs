using Jimgine.Core.Models.World.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Levels
{
    public class Level : IDisposable
    {
        public Character[] Characters;

        public Level()
        {
        }

        public void Dispose()
        {
        }
    }
}
