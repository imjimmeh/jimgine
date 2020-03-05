using Jimgine.Core.Models.Graphics.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.World.Characters
{
    [Serializable]
    public class Character : MovableObject
    {
        GameObjectStatus currentStatus;
        GameObjectStatus previousStatus;

        public Character()
        {
            currentStatus = GameObjectStatus.Idle;
            MaxSpeed = 1f;
        }

        public void UpdateStatus(GameObjectStatus newStatus)
        {
            previousStatus = currentStatus;
            currentStatus = newStatus;
        }

        public SpriteData GetCurrentSpriteInfo()
        {
            return spriteData[currentStatus];
        }
    }
}
