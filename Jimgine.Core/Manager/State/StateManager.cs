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

namespace Jimgine.Core.Manager.State
{
    public class StateManager : IGameService
    {
        //TODO : Move camera into graphics service? 
        //TODO: Make camera the primary service, which gets data from the other services and returns to graphics service?
        readonly CameraService _cameraService;
        readonly InputService _inputService;
        readonly LevelManager _levelManager;
        readonly PlayerManager _playerManager;

        //TODO: remove these public things
        public LevelManager LevelManager => _levelManager;
        public Player Player => _playerManager.Player;
        public CameraService CameraService => _cameraService;

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

        public IEnumerable<Tuple<Tile, Vector2>> GetTilesToDraw()
        {
            return LevelManager.GetTilesToDraw(_cameraService.GetStartOfTilesToDraw(_playerManager.Player.Position.X, _playerManager.Player.Position.Y));
        }

        public Vector2 GetPlayerPosition()
        {
            return _cameraService.GetVisualPosition(_playerManager.Player.Position);
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

        //Move the below sprite/data loading things into separate thing somewher
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
