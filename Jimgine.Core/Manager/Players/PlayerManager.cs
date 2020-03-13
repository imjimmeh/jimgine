using Jimgine.Core.Input;
using Jimgine.Core.Models.World.Characters.Players;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Manager.Players
{
    public class PlayerManager : IGameService
    {
        Player _player;
        public Player Player => _player;
        
        public PlayerManager()
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
            //UpdatePosition(gameTime);
            _player.Update(gameTime);
        }

        public void SetPlayer(Player player)
        {
            this._player = player;
        }

        public void UpdatePosition(GameTime gameTime)
        {
           // player.Position += player.Direction * player.CurrentSpeed;
        }
    }
}
