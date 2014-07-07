using UnityEngine;
using System.Collections;
using Backend;

namespace Enemy
{
    public class Robot : BaseEnemy
    {

        public Robot(int MaxLivePoints, int Damage, int Armour)
            : base(MaxLivePoints, Damage, Armour)
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
