using Jimgine.Core.Models.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Extensions.Movement
{
    public static class MovableObjectExtensions
    {
        public static void SetMovingRight(this MovableObject o, bool moving)
        {
            o.SetMovingRight(moving);
        }

        public static void SetMovingLeft(this MovableObject o, bool moving)
        {
            o.SetMovingLeft(moving);
        }

        public static void SetMovingUp(this MovableObject o, bool moving)
        {
            o.SetMovingUp(moving);
        }

        public static void SetMovingDown(this MovableObject o, bool moving)
        {
            o.SetMovingDown(moving);
        }

        public static void SetMoving(this MovableObject o, bool isMoving)
        {
            if (isMoving)
                o.CurrentSpeed = o.MaxSpeed;
            else
            {
                o.CurrentSpeed = 0;
            }
        }
    }
}
