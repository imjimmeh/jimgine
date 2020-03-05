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
        Player player;
        
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
            player.Update(gameTime);
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        void SetMoving(bool isMoving)
        {
            if (isMoving)
                player.CurrentSpeed = player.MaxSpeed;
            else
            {
                player.CurrentSpeed = 0;
            }
        }

        public void SetMovingRight(bool moving)
        {
            player.SetMovingRight(moving);
        }

        public void SetMovingLeft(bool moving)
        {
            player.SetMovingLeft(moving);
        }

        public void SetMovingUp(bool moving)
        {
            player.SetMovingUp(moving);
        }

        public void SetMovingDown(bool moving)
        {
            player.SetMovingDown(moving);
        }

        public void SetNotMoving()
        {
            player.SetNotMoving();
        }

        public void UpdatePosition(GameTime gameTime)
        {
           // player.Position += player.Direction * player.CurrentSpeed;
        }
    }
}
