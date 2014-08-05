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

        public List<BaseIngredient> ingredieceInventory;

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


        ///PHIALS
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



        ///INGREDIENCE
        public void addIngredience(BaseIngredient newIngredience)
        {
            ingredieceInventory.Add(newIngredience);
        }

        public bool isCountOfIngredience(BaseIngredient searchIngredience, int needCount)
        {
            int count = 0;
            foreach (BaseIngredient itemInList in ingredieceInventory)
            {
                if (itemInList == searchIngredience)
                {
                    count++;
                }
            }

            if(count >= needCount)
            {
                return true;
            }

            return false;
        }

        public bool removeCountOfIngredience(BaseIngredient searchIngredience, int needCount)
        {
            List<BaseIngredient> items = new List<BaseIngredient>();

            foreach (BaseIngredient itemInList in ingredieceInventory)
            {
                if (itemInList == searchIngredience)
                {
                    items.Add(itemInList);
                }
            }

            if(items.Count>= needCount)
            {
                for (int i = 0; i < needCount; i++ )
                {
                    ingredieceInventory.Remove(searchIngredience);
                }
                    return true;
            }
            return false;
        }

        public int getCountOfIngredience(BaseIngredient searchIngredience)
        {
            int count = 0;
            foreach (BaseIngredient itemInList in ingredieceInventory)
            {
                if (itemInList == searchIngredience)
                {
                    count++;
                }
            }
            return count; 
        }

        public Hashtable convertInventoryToHashTable()
        {
            Hashtable table = new Hashtable();

            string [] ingredients = Ingredients.Instance().GetIngredientList();

            foreach (string ingredientName in ingredients)
            {
                int count = getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(ingredientName));
                table.Add(ingredientName, count);
            }

            return table;
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
            this.HealthPoints = 25;

            // Inventory Setup
            PhialInventory = new List<PhialType>();
            addPhialToinventory(PhialType.Heal);
            addPhialToinventory(PhialType.Heal);


            Debug.Log("PhialCountHeal:" + getCountOfPhialsOfSortInInventory(PhialType.Heal));

            ingredieceInventory = new List<BaseIngredient>();
        }

        public PlayerModel.PhialType convertDamageTypeToPhialType()
        {
            switch (DamageType_Poision)
            {
                case DamageTypes.Fire:
                    return PhialType.Fire;
                case DamageTypes.Ice:
                    return PhialType.Ice;
                    
            }
            return PhialType.Empty;
        }

    }
}
