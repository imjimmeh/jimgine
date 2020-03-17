using Jimgine.Core.Content;
using Jimgine.Core.Models.Levels;
using Jimgine.Core.Models.World.Characters;
using Jimgine.Core.Models.World.Characters.Players;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Manager.State.Levels
{
    public class LevelManager : IGameService
    {
        Level _level;

        public IEnumerable<Character> Characters => _level.Characters;

        public Level Level => _level;

        public LevelManager()
        {

        }

        public void Initialise()
        {
            
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {

        }

        public void LoadLevel(Level level)
        {
            if (_level != null)
                _level.Dispose();

            //TODO: sort this
            _level = level;
        }

        public void UnloadLevel()
        {

        }

        public Character AddCharacter(Character character)
        {
            return _level.AddCharacter(character);
        }

        internal IEnumerable<Tuple<Tile, int, int>> GetTilesToDraw(Point cameraPosition)
        {
            return _level.GetTilesToDraw(cameraPosition);
        }
    }
}
