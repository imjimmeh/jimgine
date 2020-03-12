using Jimgine.Core.Models.World;
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
        List<Character> _characters;
        public List<Character> Characters => _characters;

        Layer[] _layers;
        public Layer[] Layers => _layers;

        List<GameObject> _objects;
        public List<GameObject> Objects => _objects;

        public string LevelName { get; set; }

        bool isDisposing;

        public Level(int layerCount)
        {
            Initialise(layerCount);
        }

        internal void Initialise(int layerCount)
        {
            _characters = new List<Character>();
            _layers = new Layer[layerCount];
            _objects = new List<GameObject>();

            isDisposing = false;
        }

        public Character AddCharacter(Character character)
        {
            _characters.Add(character);
            return character;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposing)
            {
                if (disposing)
                {
                    return;
                }
                _characters.Clear();
                _characters = null;

                _layers = null;

                _objects.Clear();
                _objects = null;

                isDisposing = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}