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
    public class Character : GameObject
    {
        CharacterStatus currentStatus;
        CharacterStatus previousStatus;

        public float CurrentSpeed;
        public Vector3 Direction;

        public float MaxSpeed;

        public Dictionary<CharacterStatus, SpriteData> spriteData;

        public Character()
        {
            currentStatus = CharacterStatus.Idle;
            MaxSpeed = 1f;
        }

        public void UpdateStatus(CharacterStatus newStatus)
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
