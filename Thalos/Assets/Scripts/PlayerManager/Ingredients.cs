using UnityEngine;
using System.Collections;

public class Ingredients {
    

    private static Ingredients ingredients;


    private Hashtable ListOfIngredients = new Hashtable();


    public static Ingredients Instance()
    {
        if (ingredients == null)
        {
            ingredients = new Ingredients();
        }

        return ingredients;
    }


    private  Ingredients()
    {        
        this.InitIngredients();
    }

    private void InitIngredients()
    {
        Ingredient Water = new Ingredient("Water", "Water.jpeg");
        ListOfIngredients.Add("Water", Water);

        Ingredient Herb = new Ingredient("Herb", "Herb.jpeg");
        ListOfIngredients.Add("Herb", Herb);

        Ingredient Oil = new Ingredient("Oil", "Oil.jpeg");
        ListOfIngredients.Add("Oil", Oil);

        Ingredient PhoenixAsh = new Ingredient("PhoenixAsh", "PhoenixAsh.jpeg");
        ListOfIngredients.Add("PhoenixAsh", PhoenixAsh);

        Ingredient Snowdrop = new Ingredient("Snowdrop", "Snowdrop.jpeg");
        ListOfIngredients.Add("Snowdrop", Snowdrop);

        Ingredient CristalFlower = new Ingredient("CristalFlower", "CristalFlower.jpeg");
        ListOfIngredients.Add("CristalFlower", CristalFlower);            
    }

    public string[] GetIngredientList()
    {
        string[] Keys = new string[ListOfIngredients.Count];
        ListOfIngredients.Keys.CopyTo(Keys, 0);
        return Keys;
    }

    public Ingredient GetSingleIngredient(string Key)
    {
        if(ListOfIngredients.ContainsKey(Key))
        {
            return (Ingredient)ListOfIngredients[Key];
        }
        return null;
    }

    public Ingredient[] GetAllIngredients()
    {
        Ingredient[] Values = new Ingredient[ListOfIngredients.Count];
        ListOfIngredients.Values.CopyTo(Values, 0);
        return Values;
    }
}


