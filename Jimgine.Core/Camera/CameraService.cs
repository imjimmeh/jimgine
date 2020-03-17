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
        Vector2 _position;
        public Vector2 Position => _position;

        int _tileSize;

        int _resolutionWidth;
        int _resolutionHeight;

        int _tileCountPerWidth;
        int _tileCountPerHeight;

        Point _screenPosition;
        public CameraService(int screenWidth, int screenHeight, int tileSize)
        {
            _tileSize = tileSize;
            Initialise(screenWidth, screenHeight);
        }

        private void Initialise(int screenWidth, int screenHeight)
        {
            _screenPosition = new Point(0, 0);
            SetScreenDimensions(screenWidth, screenHeight);
        }

        void SetScreenDimensions(int width, int height)
        {
            _resolutionWidth = width;
            _resolutionHeight = height;

            SetTileCounts();
        }

        public Point GetScreenPosition(Point currentPlayerPosition)
        {
            _screenPosition.X = currentPlayerPosition.X / _tileCountPerWidth;
            _screenPosition.Y = currentPlayerPosition.Y / _tileCountPerHeight;
            return _screenPosition; 
        }

        public void SetTileSize(int tileSize)
        {
        }


        void SetTileCounts()
        {
            _tileCountPerWidth = _resolutionWidth / _tileSize;
            _tileCountPerHeight = _resolutionHeight / _tileSize;
        }
    }
}
