using Jimgine.Core.Camera;
using Jimgine.Core.Input;
using Jimgine.Core.Manager.Players;
using Jimgine.Core.Manager.State.Levels;
using Jimgine.Core.Models.Levels;
using Jimgine.Core.Models.World.Characters;
using Jimgine.Core.Models.World.Characters.Players;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static Jimgine.Core.Extensions.Conversions.ConversionExtensions;

namespace Jimgine.Core.Manager.State
{
    public class StateManager : IGameService
    {
        readonly CameraService _cameraService;
        readonly InputService _inputService;
        readonly LevelManager _levelManager;
        readonly PlayerManager _playerManager;

        public LevelManager LevelManager => _levelManager;
        public Player Player => _playerManager.Player;

        public IEnumerable<Tuple<Tile, int, int>> GetTilesToDraw()
        {
            return LevelManager.GetTilesToDraw(_cameraService.GetScreenPosition(_playerManager.Player.Position.ToPoint()));
        }

        public StateManager(InputService inputService)
        {
            _inputService = inputService;

            _cameraService = new CameraService(800,480,16);
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

        internal void LoadLevel(Level level)
        {
            _levelManager.LoadLevel(level);
            LoadPlayer(level.Player);
            Initialise();
        }

        void LoadPlayer(Player player)
        {
            _playerManager.SetPlayer(player);
            _playerManager.Player.Direction = new Vector3();
        }

        internal IEnumerable<string> GetFilesToLoad()
        {
            return CharactersSprites()
                .Concat(PlayersSprites())
                .Concat(TerrainSprites());
        }

        private IEnumerable<string> PlayersSprites()
        {
            return _playerManager.Player.SpriteData.Values.Select(y => y.TexturePath);
        }

        private IEnumerable<string> TerrainSprites()
        {
            return _levelManager.Level.SpriteNames != null ? _levelManager.Level.SpriteNames.Where(s => !string.IsNullOrEmpty(s)) : null;
        }

        private IEnumerable<string> CharactersSprites()
        {
            return _levelManager
                            .Characters
                                .Where(c => c != null)
                                    .Select(y => y.SpriteData)
                                    .SelectMany(x => x.Values)
                                    .Select(x => x.TexturePath);
        }

        internal IEnumerable<Character> GetCharacters()
        {
            return _levelManager.Characters;
        }
    }
}
