using Jimgine.Core.Models.Levels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace Jimgine.Core
{
    public interface IGameService
    {
        void Initialise();

        void LoadContent();

        void UnloadContent();

        void Update(GameTime gameTime);
    }
}
