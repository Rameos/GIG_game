using UnityEngine;
using System.Collections;
using Backend;

namespace Enemy
{
    public abstract class BaseEnemy
    {

        public int MaxLivePoints { get; private set; }
        public int LivePoints { get; private set; }
        public int Damage { get; private set; }
        public int Armour { get; private set; }
        
        public BaseEnemy(int MaxLivePoints, int Damage, int Armour)
        {
            this.MaxLivePoints = MaxLivePoints;
            this.LivePoints = MaxLivePoints;
            this.Damage = Damage;
            this.Armour = Armour;    
        }

        public int TakeDamage(int Damage, PlayerModel.DamageTypes DamageType)
        {
            this.LivePoints -= (Damage - Armour);
            if (this.LivePoints < 0)
            {
                this.LivePoints = 0;
            }
            return this.LivePoints;
        }

        public bool EnemyIsDead()
        {
            if (this.LivePoints <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
