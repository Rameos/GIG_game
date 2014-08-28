using UnityEngine;
using System.Collections;
using Controller;
using Backend;

namespace Enemy
{
    public class Policeman : BaseEnemy
    {

        public Policeman(int MaxLivePoints, int Damage, int Armour): base(MaxLivePoints, Damage, Armour)
        {

        }

        public int TakeDamage(int Damage, PlayerModel.DamageTypes DamageType)
        {
            if (DamageType == PlayerModel.DamageTypes.Fire)
            {
                return this.LivePoints;
            }
            return base.TakeDamage(Damage, DamageType);
        }
    }
}
