using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.World.Characters
{
    public abstract class OffensiveCharacter : Character, IOffensiveUnit
    {
        float _attackDamage;
        public float AttackDamage => _attackDamage;

        public void Attack(IHealthUnit target)
        {
            target.AddHealth(_attackDamage * -1);
        }
    }
}
