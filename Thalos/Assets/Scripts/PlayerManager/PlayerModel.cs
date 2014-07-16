using UnityEngine;
using System.Collections;


namespace Backend
{

    public class PlayerModel
    {

        public enum DamageTypes {Standard, Fire, Ice, None};
        public enum PhialType { Empty, Fire, Ice, Heal };

        private static PlayerModel playerModel;


        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public PhialType[] Phial { get; set; }
        public int phialSize { get; set; }

        public int Damage { get; set; }
        public DamageTypes DamageType_Bolt { get; set; }
        public DamageTypes DamageType_Poision { get; set; }
        public int Phial_heal { get; set; }
        public int Phial_fire { get; set; }
        public int Phial_ice  { get; set; }



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
            this.phialSize = 5;

            // Init PhialCount
            this.Phial_heal = 1;
            this.Phial_fire = 0;
            this.Phial_ice = 0; 
            
            
            this.Damage = 42;
            this.DamageType_Bolt = DamageTypes.Standard;
            this.DamageType_Poision = DamageTypes.None;
            //Debug
            this.HealthPoints = 100;

            // ToDo: SetItem
        }
    }
}
