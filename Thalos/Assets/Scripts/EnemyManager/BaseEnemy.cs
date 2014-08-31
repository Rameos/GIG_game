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
        public PlayerModel.DamageTypes resistance;
        public PlayerModel.DamageTypes weakElement;
        public BaseEnemy(int MaxLivePoints, int Damage, int Armour)
        {
            this.MaxLivePoints = MaxLivePoints;
            this.LivePoints = MaxLivePoints;
            this.Damage = Damage;
            this.Armour = Armour;    
        }

        public int TakeDamage(int Damage, PlayerModel.DamageTypes DamageType)
        {

            //Resitance against Damage = Stnadard Arm
            if(resistance != DamageType)
            {
                int damage = Damage - Armour;
                this.LivePoints -= damage;
                Debug.Log("Livepoints: " + this.LivePoints);
                if (this.LivePoints < 0)
                {
                    this.LivePoints = 0;
                }
            }
            //Weak Element
            else if(DamageType == weakElement)
            {
                int damage = 2*Damage;

                this.LivePoints -= damage;
                Debug.Log("Livepoints: " + this.LivePoints);
                if (this.LivePoints < 0)
                {
                    this.LivePoints = 0;
                }   
            }
                //Standard
            else
            {
                int damage = Damage - Armour/2;
                this.LivePoints -= damage;
                Debug.Log("Livepoints: " + this.LivePoints);
                if (this.LivePoints < 0)
                {
                    this.LivePoints = 0;
                }
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
