using Jimgine.Core.Manager.Players;
using Jimgine.Core.Manager.State.Levels;
using Jimgine.Core.Models.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Jimgine.Core.Camera
{
    public class CameraService
    {
        readonly PlayerManager _playerManager;
        readonly LevelManager _levelManager;

        //TODO: actually have a camera....
        Point _position;
        public Point Position => _position;

        int _tileSize;

        int _resolutionWidth;
        int _resolutionHeight;

        int _tileCountPerWidth;
        int _tileCountPerHeight;

        Vector3 _playerLastPosition;
        Point _cameraPosition;

        public CameraService(int screenWidth, int screenHeight, int tileSize, PlayerManager playerManager, LevelManager levelManager)
        {
            _tileSize = tileSize;
            Initialise(screenWidth, screenHeight);

            _playerManager = playerManager;
            _levelManager = levelManager;
        }

        public void Update()
        {
            UpdatePlayerPosition();
        }

        public IEnumerable<SpriteDrawInformation> GetTerrain()
        {
            foreach(var tile in _levelManager.GetTilesToDraw(_cameraPosition, _tileCountPerWidth, _tileCountPerHeight))
            {
                yield return new SpriteDrawInformation()
                {
                    TexturePath = tile.Item1.Image.TexturePath,
                    Location = GetVisualPosition(tile.Item2.X, tile.Item2.Y),
                    Rectangle = tile.Item1.Image.Area
                };
            }
        }

        public SpriteDrawInformation GetPlayerDrawInformation()
        {
            return new SpriteDrawInformation()
            {
                TexturePath = _playerManager.Player.GetSpriteData().TexturePath,
                Rectangle = _playerManager.Player.GetSpriteData().Area,
                Location = GetVisualPosition(_playerManager.Player.Position)
            };
        }

        void UpdatePlayerPosition()
        {
            if (!_playerLastPosition.Equals(_playerManager.Player.Position))
            {
                _playerLastPosition = _playerManager.Player.Position;
                _cameraPosition = GetStartOfTilesToDraw(_playerManager.Player.Position.X, _playerManager.Player.Position.Y);
            }
        }

        private void Initialise(int screenWidth, int screenHeight)
        {
            _position = new Point(0, 0);
            SetScreenDimensions(screenWidth, screenHeight);
        }

        void SetScreenDimensions(int width, int height)
        {
            _resolutionWidth = width;
            _resolutionHeight = height;

            SetTileCounts();
        }

        public Point GetCurrentScreen(int x, int y)
        {
            _position.X = x / _tileSize / _tileCountPerWidth;
            _position.Y = y / _tileSize / _tileCountPerHeight;
            return _position;
        }

        public Point GetStartOfTilesToDraw(float x, float y)
        {
            var currentScreen = GetCurrentScreen((int)x, (int)y);
            return new Point(currentScreen.X * _tileCountPerWidth, currentScreen.Y * _tileCountPerHeight);
        }

        Point GetCurrentScreen(float x, float y)
        {
            return GetCurrentScreen((int)x, (int)y);
        }

        Vector2 GetVisualPosition(Vector3 worldPosition)
        {
            return GetVisualPosition(ref worldPosition.X, ref worldPosition.Y);
        }

        Vector2 GetVisualPosition(Vector2 worldPosition)
        {
            return GetVisualPosition(ref worldPosition.X, ref worldPosition.Y);
        }

        Vector2 GetVisualPosition(ref float x, ref float y)
        {
            var screen = GetCurrentScreen(x, y);

            return new Vector2(x - (_resolutionWidth * screen.X), y - (_resolutionHeight * screen.Y));
        }

        Vector2 GetVisualPosition(float x, float y)
        {
            return GetVisualPosition(ref x, ref y);
        }

        public void SetTileSize(int tileSize)
        {
            if (tileSize <= 0)
                throw new ArgumentOutOfRangeException("tileSize", "Tile size must be greater than 0");

            _tileSize = tileSize;
        }

        void SetTileCounts()
        {
            _tileCountPerWidth = _resolutionWidth / _tileSize;
            _tileCountPerHeight = _resolutionHeight / _tileSize;
        }
    }
}
