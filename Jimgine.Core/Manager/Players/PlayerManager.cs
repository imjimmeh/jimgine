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
            UpdatePosition(gameTime);
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

        void SetXDirection(float x)
        {
            player.Direction.X = x;
        }

        void SetYDirection(float y)
        {
            player.Direction.Y = y;
        }

        public void SetMovingRight()
        {
            SetMoving(true);
            SetXDirection(1);
        }

        public void SetMovingLeft()
        {
            SetMoving(true);
            SetXDirection(-1);
        }

        public void SetMovingUp()
        {
            SetMoving(true);
            SetYDirection(-1);
        }

        public void SetMovingDown()
        {
            SetMoving(true);
            SetYDirection(1);
        }

        public void SetNotMoving()
        {
            SetMoving(false);
            player.Direction.X = 0;
            player.Direction.Y = 0;
        }

        public void UpdatePosition(GameTime gameTime)
        {
            player.Position += player.Direction * player.CurrentSpeed;
        }
    }
}
