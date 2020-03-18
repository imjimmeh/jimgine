using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Camera
{
    public class CameraService
    {
        //TODO: actually have a camera....
        Point _position;
        public Point Position => _position;

        int _tileSize;

        int _resolutionWidth;
        int _resolutionHeight;

        int _tileCountPerWidth;
        int _tileCountPerHeight;

        public CameraService(int screenWidth, int screenHeight, int tileSize)
        {
            _tileSize = tileSize;
            Initialise(screenWidth, screenHeight);
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

        public Point GetCurrentScreen(float x, float y)
        {
            return GetCurrentScreen((int)x, (int)y);
        }

        public Vector2 GetVisualPosition(Vector3 worldPosition)
        {
            return GetVisualPosition(ref worldPosition.X, ref worldPosition.Y);
        }

        public Vector2 GetVisualPosition(Vector2 worldPosition)
        {
            return GetVisualPosition(ref worldPosition.X, ref worldPosition.Y);
        }

        public Vector2 GetVisualPosition(ref float x, ref float y)
        {
            var screen = GetCurrentScreen(x, y);

            return new Vector2(x - (_resolutionWidth * screen.X), y - (_resolutionHeight * screen.Y));
        }

        public Vector2 GetVisualPosition(float x, float y)
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
