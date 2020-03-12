using Jimgine.Core.Content;
using Jimgine.Core.Input;
using Jimgine.Core.Manager.Players;
using Jimgine.Core.Manager.State.Levels;
using Jimgine.Core.Models.Commands;
using Jimgine.Core.Models.Input;
using Jimgine.Core.Models.Levels;
using Jimgine.Core.Models.World.Characters;
using Jimgine.Core.Models.World.Characters.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Jimgine.Core.Manager.State
{
    public class StateManager : IGameService
    {
        LevelManager _levelManager;
        PlayerManager _playerManager;
        InputService _inputService;
        Player _player;

        public Player Player { get => _player; }

        public StateManager(InputService inputService)
        {
            _inputService = inputService;

            _levelManager = new LevelManager();
            _playerManager = new PlayerManager();
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
            _playerManager.Update(gameTime);
        }

        internal void LoadLevel(LevelData levelData)
        {
            int layerCount = levelData.Layers == null ? 5 : levelData.Layers.Length;

            _levelManager.LoadLevel(layerCount);

            LoadCharacters(levelData);

            Initialise();
        }

        private void LoadCharacters(LevelData levelData)
        {
            for (var x = 0; x < levelData.Characters.Length; x++)
            {
                LoadCharacter(levelData, x);
            }
        }

        private void LoadCharacter(LevelData levelData, int x)
        {
            if (!levelData.Characters[x].IsPlayer)
            {
                _levelManager.AddCharacter(ContentService.LoadJsonFile<Character>(levelData.Characters[x].File)).MaxSpeed = 5f;
            }
            else
            {
                _player = ContentService.LoadJsonFile<Player>(levelData.Characters[x].File);
                _playerManager.SetPlayer(_player);
                _player.Direction = new Vector3();
            }
        }

        internal IEnumerable<string> GetFilesToLoad()
        {
            return _levelManager.Characters.Where(c => c != null).Select(y => y.SpriteData).SelectMany(x => x.Values).Select(x => x.TexturePath).Concat(_player.SpriteData.Values.Select(y => y.TexturePath));
        }

        internal IEnumerable<Character> GetCharacters()
        {
            return _levelManager.Characters;
        }
    }
}
