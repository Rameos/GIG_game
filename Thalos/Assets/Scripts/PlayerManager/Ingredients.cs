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
        BaseIngredient Water = new BaseIngredient("Water", "Water.jpeg");
        ListOfIngredients.Add("Water", Water);

        BaseIngredient Herb = new BaseIngredient("Herb", "Herb.jpeg");
        ListOfIngredients.Add("Herb", Herb);

        BaseIngredient Oil = new BaseIngredient("Oil", "Oil.jpeg");
        ListOfIngredients.Add("Oil", Oil);

        BaseIngredient PhoenixAsh = new BaseIngredient("PhoenixAsh", "PhoenixAsh.jpeg");
        ListOfIngredients.Add("PhoenixAsh", PhoenixAsh);

        BaseIngredient Snowdrop = new BaseIngredient("Snowdrop", "Snowdrop.jpeg");
        ListOfIngredients.Add("Snowdrop", Snowdrop);

        BaseIngredient CristalFlower = new BaseIngredient("CristalFlower", "CristalFlower.jpeg");
        ListOfIngredients.Add("CristalFlower", CristalFlower);            
    }

    public string[] GetIngredientList()
    {
        string[] Keys = new string[ListOfIngredients.Count];
        ListOfIngredients.Keys.CopyTo(Keys, 0);
        return Keys;
    }

    public BaseIngredient GetSingleIngredient(string Key)
    {
        if(ListOfIngredients.ContainsKey(Key))
        {
            return (BaseIngredient)ListOfIngredients[Key];
        }
        return null;
    }

    public BaseIngredient[] GetAllIngredients()
    {
        BaseIngredient[] Values = new BaseIngredient[ListOfIngredients.Count];
        ListOfIngredients.Values.CopyTo(Values, 0);
        return Values;
    }
}


