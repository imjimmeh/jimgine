using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.World.Characters
{
    public interface ICharacter
    {
        void UpdateStatus(GameObjectStatus newStatus);

    }
}
