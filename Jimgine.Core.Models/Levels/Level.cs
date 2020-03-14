using Jimgine.Core.Models.World;
using Jimgine.Core.Models.World.Characters;
using Jimgine.Core.Models.World.Characters.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public Layer[] Layers { get => _layers; set => _layers = value; }

        List<GameObject> _objects;
        public List<GameObject> Objects => _objects;

        public string LevelName { get; set; }
        public int TileSize { get; set; }

        bool isDisposing;

        public List<string> SpriteNames { get; set; }

        public Player Player;

        public Level()
        {

        }

        public Level(Character[] characters, Layer[] layers)
        {
            _layers = layers;
            _characters = new List<Character>(characters.Length * 5);
            _objects = new List<GameObject>();

            isDisposing = false;
            TileSize = 16;
        }

        public void Initialise(Character[] characters, Layer[] layers)
        {
            _layers = layers;

            foreach (var c in characters)
                _characters.Add(c);
        }

        public void AddObject(GameObject gameObject)
        {
            _objects.Add(gameObject);
        }

        public Character AddCharacter(Character character)
        {
            _characters.Add(character);
            return character;
        }

        public IEnumerable<Tuple<Tile, int, int>> GetTilesToDraw(Point cameraPosition)
        {
            for (var x = 0; x < _layers.Length; x++)
            {
                if (_layers[x] == null || _layers[x].Tiles == null)
                    continue;

                for (var y = 0; y < _layers[x].Tiles.GetLength(0); y++)
                {
                    for(var z = 0; z < _layers[x].Tiles.GetLength(1); z++)
                    {
                        yield return new Tuple<Tile, int, int>(_layers[x].Tiles[y, z], y + (y * TileSize), z + (z * TileSize));
                    }
                }
            }
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