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
    public abstract class Character : MovableObject, ICharacter, IHealthUnit
    {
        GameObjectStatus currentStatus;
        GameObjectStatus previousStatus;

        float _health;
        public float Health { get => _health; }
        public bool IsAlive { get => Health > 0; }

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

        public void AddHealth(float healthToAdd)
        {
            _health += healthToAdd;
        }

    }
}
