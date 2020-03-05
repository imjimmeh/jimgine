using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.World
{
    public class MovableObject : GameObject
    {
        public float CurrentSpeed;
        public Vector3 Direction;

        public float MaxSpeed;

        public void Update(GameTime gameTime)
        {
            MoveObject(gameTime);
        }

        void MoveObject(GameTime gameTime)
        {
            Position += Direction * CurrentSpeed;
        }

        void SetMoving(bool isMoving)
        {
            if (isMoving)
                CurrentSpeed = MaxSpeed;
            else
            {
                CurrentSpeed = 0;
            }
        }

        void SetXDirection(float x)
        {
            Direction.X = x;
        }

        void SetYDirection(float y)
        {
            Direction.Y = y;
        }

        public void SetMovingRight(bool moving)
        {
            SetMoving(moving);
            SetXDirection(moving ? 1 : 0);
        }

        public void SetMovingLeft(bool moving)
        {
            SetMoving(moving);
            SetXDirection(moving ? -1 : 0);
        }

        public void SetMovingUp(bool moving)
        {
            SetMoving(moving);
            SetYDirection(moving ? -1 : 0);
        }

        public void SetMovingDown(bool moving)
        {
            SetMoving(moving);
            SetYDirection(moving ? 1 : 0);
        }

        public void SetNotMoving()
        {
            SetMoving(false);
            Direction.X = 0;
            Direction.Y = 0;
        }

    }
}
