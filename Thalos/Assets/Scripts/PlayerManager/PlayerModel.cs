using UnityEngine;
using System.Collections;


namespace Backend
{

    public class PlayerModel
    {

        public enum DamageTypes {Standard, Fire, Ice};
        
        private static PlayerModel playerModel;


        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public int Phial { get; set; }
        public int Damage { get; set; }
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
            this.DamageType = DamageTypes.Standard;

            //Debug
            this.HealthPoints = 100;

            // ToDo: SetItem
        }
    }
}
