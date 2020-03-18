using Jimgine.Core.Camera;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jimgine.Unit.Testing.Camera
{
    [TestFixture]
    public class CameraServiceTests
    {
        CameraService _cameraService;

        [SetUp]
        public void Setup()
        {
            _cameraService = new CameraService(800, 600, 32);
            //25 wide, 18.75 high
        }

        [Test]
        public void GetCurrentScreen_InputIsFirstScreen()
        {
            float x = 100;
            float y = 100;

            var currentScreen = _cameraService.GetCurrentScreen(x, y);

            Assert.IsTrue(currentScreen.X == 0);
            Assert.IsTrue(currentScreen.Y == 0);
        }

        [Test]
        public void GetCurrentScreen_InputIsLargeScreen()
        {
            float x = 9000;
            float y = 12000;

            var currentScreen = _cameraService.GetCurrentScreen(x, y);

            Assert.IsTrue(currentScreen.X == 11);
            Assert.IsTrue(currentScreen.Y == 20);
        }

        [Test]
        public void GetVisualPosition_InputIsFirstScreen()
        {
            Vector2 pos = new Vector2(100, 100);

            var visualPosition = _cameraService.GetVisualPosition(pos);

            Assert.IsTrue(visualPosition.X == 100);
            Assert.IsTrue(visualPosition.Y == 100);
        }

        [Test]
        public void GetVisualPosition_InputIsLargeScreen()
        {
            Vector2 pos = new Vector2(9000, 12000);

            var visualPosition = _cameraService.GetVisualPosition(pos);

            Assert.IsTrue(visualPosition.X == 200);
            Assert.IsTrue(visualPosition.Y == 0);
        }

        [Test]
        public void SetTileSize_SmallNumber_IsValid()
        {
            int tileSize = 16;

            _cameraService.SetTileSize(tileSize);

            Assert.IsTrue(true == true);
        }


        [Test]
        public void SetTileSize_Zero_Exception()
        {
            int tileSize = 0;
            try
            {
                _cameraService.SetTileSize(tileSize);
                Assert.IsTrue(true == false);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true == true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true == false);
            }
        }


        [Test]
        public void SetTileSize_NegativeNumber_Exception()
        {
            int tileSize = -100;
            try
            {
                _cameraService.SetTileSize(tileSize);
                Assert.IsTrue(true == false);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true == true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true == false);
            }
        }
    }
}
