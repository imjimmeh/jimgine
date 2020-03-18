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
        public Layer[] Layers => _layers;

        List<GameObject> _objects;
        public List<GameObject> Objects => _objects;

        public string LevelName { get; set; }
        public int TileSize { get; set; }

        bool isDisposing;

        //TODO : do these better - only in here atm because effort
        public List<string> SpriteNames { get; set; }
        public Player Player;


        public Level()
        {

        }

        public Level(Character[] characters, Layer[] layers)
        {
            _layers = layers;
            _characters = new List<Character>(0);
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

        public IEnumerable<Tuple<Tile, Vector2>> GetTilesToDraw(Point cameraPosition, int xCount, int yCount)
        {
            int xMax = cameraPosition.X + xCount;
            int yMax = cameraPosition.Y + yCount;

            for (var l = 0; l < _layers.Length; l++)
            {
                if (_layers[l] == null || _layers[l].Tiles == null)
                    continue;

                for (var x = cameraPosition.X; x < _layers[l].Tiles.GetLength(0); x++)
                {
                    for(var y = cameraPosition.Y; y < _layers[l].Tiles.GetLength(1); y++)
                    {
                        if (_layers[l].Tiles[x, y] == null)
                            continue;

                        if (x > xMax)
                            continue;

                        if (y > yMax)
                            continue;

                        yield return new Tuple<Tile, Vector2>
                            (_layers[l].Tiles[x, y], new Vector2(x * TileSize, y * TileSize));
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