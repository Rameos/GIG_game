using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Backend
{

    public class PlayerModel
    {

        public enum DamageTypes {Standard, Fire, Ice, None};
        public enum PhialType { Empty, Fire, Ice, Heal };

        private static PlayerModel playerModel;

        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        
        public List<PhialType> PhialInventory { get; set; }
        public List<Recipes> FoundedRecipes { get; set; }
        public int phialSizeMax { get; set; }

        public int Damage { get; set; }
        public DamageTypes DamageType_Bolt { get; set; }
        public DamageTypes DamageType_Poision { get; set; }

        public static PlayerModel Instance()
        {
            if (playerModel == null)
            {
                playerModel = new PlayerModel();
            }

            return playerModel;
        }

        public void addPhialToinventory(PhialType type)
        {
            if(PhialInventory.Count<phialSizeMax)
            {
                PhialInventory.Add(type);
            }
        }

        public bool checkIfPhialIsInInventory(PhialType type)
        {
            if(PhialInventory.Contains(type))
            {
                return true;
            }
            return false;
        }

        public int getCountOfPhialsOfSortInInventory( PhialType type)
        {
            int count = 0;

            foreach (PhialType itemInList in PhialInventory)
            {
                if(itemInList ==type)
                {
                    count++;
                }
            }
            return count; 
        
        }

        public void removePhialFromInventory(PhialType type)
        {
            PhialInventory.Remove(type);
        }

        private PlayerModel()
        {
            this.MaxHealthPoints = 100;
            this.phialSizeMax = 5;

            //Damage
            this.Damage = 42;
            this.DamageType_Bolt = DamageTypes.Standard;
            this.DamageType_Poision = DamageTypes.None;
            
            //Debug
            this.HealthPoints = 100;

            // Inventory Setup
            PhialInventory = new List<PhialType>();
            addPhialToinventory(PhialType.Heal);
            addPhialToinventory(PhialType.Heal);
            addPhialToinventory(PhialType.Heal);
            addPhialToinventory(PhialType.Heal);
            addPhialToinventory(PhialType.Heal);

            Debug.Log("PhialCountHeal:" + getCountOfPhialsOfSortInInventory(PhialType.Heal));
        }


    }
}
