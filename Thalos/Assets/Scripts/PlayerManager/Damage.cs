using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend;


namespace Backend
{
    public class Damage
    {
        public PlayerModel.DamageTypes typeDamage {set;get;}
        public int damage { set; get; }

        public Damage(int damagePoints, PlayerModel.DamageTypes typeDamage)
        {
            this.damage = damagePoints;
            this.typeDamage = typeDamage;
        }
    }
}
