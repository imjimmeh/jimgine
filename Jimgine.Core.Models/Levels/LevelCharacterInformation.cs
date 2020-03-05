using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Levels
{
    [Serializable]
    public class LevelCharacterInformation
    {
        public string File;
        public Vector3 Location;
        public bool IsPlayer;
    }
}
