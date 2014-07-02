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
        BaseIngredient Water = new BaseIngredient(Strings.WATER, "Water.jpeg");
        ListOfIngredients.Add(Strings.WATER, Water);

        BaseIngredient Herb = new BaseIngredient(Strings.HERB, "Herb.jpeg");
        ListOfIngredients.Add(Strings.HERB, Herb);

        BaseIngredient Oil = new BaseIngredient(Strings.OIL, "Oil.jpeg");
        ListOfIngredients.Add(Strings.OIL, Oil);

        BaseIngredient PhoenixAsh = new BaseIngredient(Strings.PHOENIXASH, "PhoenixAsh.jpeg");
        ListOfIngredients.Add(Strings.PHOENIXASH, PhoenixAsh);

        BaseIngredient CristalFlower = new BaseIngredient(Strings.CRISTALFLOWER, "CristalFlower.jpeg");
        ListOfIngredients.Add(Strings.CRISTALFLOWER, CristalFlower);            
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


