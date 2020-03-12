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

        public void Initialise()
        {
            throw new NotImplementedException();
        }

        public void LoadContent()
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void LoadLevel(int layersCount)
        {
            if (_level != null)
                _level.Dispose();

            _level = new Level(layersCount);
        }

        public void UnloadLevel()
        {

        }

        public Character AddCharacter(Character character)
        {
            return _level.AddCharacter(character);
        }
    }
}
