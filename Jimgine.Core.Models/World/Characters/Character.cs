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

        WatchableProperty<float> _health;

        [JsonProperty]
        public float Health { get => _health.Value; private set => _health.SetValue(value); }
        public bool IsAlive { get => Health > 0; }

        Dictionary<GameObjectStatus, SpriteData> _spriteData { get; set; }

        [JsonProperty]
        public Dictionary<GameObjectStatus, SpriteData> SpriteData { get => _spriteData; private set => _spriteData = value; }

        //Events? Need a good way of doing these but will figure it out after i've done more than 1
        //public event EventHandler HealthChanges;
        public Character()
        {
            currentStatus = GameObjectStatus.Idle;
            _health = new WatchableProperty<float>(0);
        }

        protected Character(float health, Dictionary<GameObjectStatus, SpriteData> spriteData)
        {
            _health = new WatchableProperty<float>(health);
            SpriteData = spriteData ?? throw new ArgumentNullException(nameof(spriteData));
        }

        public void AddHealthChangedEvent(EventHandler d)
        {
            _health.AddEvent(d);
        }

        public void RemoveHealthChangedEvent(EventHandler d)
        {
            _health.RemoveEvent(d);
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
            _health.SetValue(_health.Value + healthToAdd);
            _health.Invoke(this, new TextValueChangeEventArgs() { NewText = Health.ToString() });
        }

        public SpriteData GetSpriteData()
        {
            return GetCurrentSpriteInfo();
        }
    }
}
