using Jimgine.Core.Models.Graphics.Sprites;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jimgine.Core.Models.Events;

namespace Jimgine.Core.Models.World.Characters
{
    [Serializable]
    public abstract class Character : MovableObject, ICharacter, IHealthUnit, ISpriteContainer
    {
        GameObjectStatus currentStatus;
        GameObjectStatus previousStatus;

        float _health;

        [JsonProperty]
        public float Health { get => _health; private set => _health = value; }
        public bool IsAlive { get => Health > 0; }

        Dictionary<GameObjectStatus, SpriteData> _spriteData { get; set; }

        [JsonProperty]
        public Dictionary<GameObjectStatus, SpriteData> SpriteData { get => _spriteData; private set => _spriteData = value; }

        //Events? Need a good way of doing these but will figure it out after i've done more than 1
        public event EventHandler HealthChanges;

        public Character()
        {
            currentStatus = GameObjectStatus.Idle;
        }

        protected Character(float health, Dictionary<GameObjectStatus, SpriteData> spriteData)
        {
            _health = health;
            SpriteData = spriteData ?? throw new ArgumentNullException(nameof(spriteData));
        }

        public void UpdateStatus(GameObjectStatus newStatus)
        {
            previousStatus = currentStatus;
            currentStatus = newStatus;
        }

        SpriteData GetCurrentSpriteInfo()
        {
            return _spriteData[currentStatus];
        }

        public void AddHealth(float healthToAdd)
        {
            _health += healthToAdd;
            HealthChanges?.Invoke(this, new TextValueChangeEventArgs() { NewText = Health.ToString() });
        }

        public SpriteData GetSpriteData()
        {
            return GetCurrentSpriteInfo();
        }
    }
}
