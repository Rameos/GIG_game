using UnityEngine;
using System.Collections;


namespace Backend
{

    public class PlayerModel
    {

        public enum DamageTypes {Standard, Fire, Magic, Silver};

    
        private static PlayerModel playerModel;


        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public int Phial { get; set; }
        public int Damage { get; set; }
        public int Gold { get; set; }
        public int Armour { get; set; }
        public DamageTypes DamageType { get; set; }


        public static PlayerModel Instance()
        {
            if (playerModel == null)
            {
                playerModel = new PlayerModel();
            }

            return playerModel;
        }

        private PlayerModel()
        {
            this.MaxHealthPoints = 100;
            this.Phial = 0;
            this.Damage = 42;
            this.Gold = 10;
            this.Armour = 5;
            this.DamageType = DamageTypes.Standard;

            //Debug
            this.HealthPoints = 100;
        }

        /// <summary>
        /// Check if Player is Dead
        /// </summary>
        /// <returns></returns>
        public bool checkIfDead()
        {
            if (this.HealthPoints > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Remove HealthPoints (
        /// </summary>
        /// <param name="Damage"></param>
        /// <returns></returns>
        public int TakeDamage(int Damage)
        {
            this.HealthPoints -= (Damage - this.Armour);
            return this.HealthPoints;
        }

        public int AddMaxHealthPoints(int HeartPiece)
        {
            this.MaxHealthPoints += HeartPiece;
            return this.MaxHealthPoints;
        }

        public int Heal(int HealPotion)
        {
            if (this.HealthPoints + HealPotion >= this.MaxHealthPoints)
            {
                this.HealthPoints = this.MaxHealthPoints;
            }
            else
            {
                this.HealthPoints += HealPotion;
            }
            return this.HealthPoints;
        }

        public int AddArmour(int PieceOfArmour)
        {
            this.Armour += PieceOfArmour;
            return this.Armour;
        }

        public int AddGold(int GoldCoins)
        {
            this.Gold += GoldCoins;
            return this.Gold;
        }
    }
}
