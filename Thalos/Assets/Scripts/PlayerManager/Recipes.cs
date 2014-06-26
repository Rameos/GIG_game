using UnityEngine;
using System.Collections;

public class Recipes
{


    private static Recipes recipes;


    private Hashtable ListOfRecipes = new Hashtable();


    public static Recipes Instance()
    {
        if (recipes == null)
        {
            recipes = new Recipes();
        }

        return recipes;
    }


    private Recipes()
    {
        this.InitRecipes();
    }

    private void InitRecipes()
    {
        BaseRecipe Healpotion = new BaseRecipe("Healpotion");
        Healpotion.addNeededIngredient("Water", 1);
        Healpotion.addNeededIngredient("Herb", 2);
        ListOfRecipes.Add("Healpotion", Healpotion);

        BaseRecipe Firepotion = new BaseRecipe("Firepotion");
        Firepotion.addNeededIngredient("Oil", 1);
        Firepotion.addNeededIngredient("PhoenixAsh", 1);
        ListOfRecipes.Add("Firepotion", Firepotion);

        BaseRecipe Icepotion = new BaseRecipe("Icepotion");
        Icepotion.addNeededIngredient("Water", 1);
        Icepotion.addNeededIngredient("Snowdrop", 1);
        Icepotion.addNeededIngredient("CristalFlower", 1);
        ListOfRecipes.Add("Icepotion", Icepotion);
    }

    public string[] GetRecipeList()
    {
        string[] Keys = new string[ListOfRecipes.Count];
        ListOfRecipes.Keys.CopyTo(Keys, 0);
        return Keys;
    }

    public BaseRecipe GetSingleRecipe(string Key)
    {
        if (ListOfRecipes.ContainsKey(Key))
        {
            return (BaseRecipe)ListOfRecipes[Key];
        }
        return null;
    }

    public BaseRecipe[] GetAllRecipes()
    {
        BaseRecipe[] Values = new BaseRecipe[ListOfRecipes.Count];
        ListOfRecipes.Values.CopyTo(Values, 0);
        return Values;
    }
}


