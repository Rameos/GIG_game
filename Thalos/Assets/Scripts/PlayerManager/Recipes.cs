using UnityEngine;
using System.Collections;
using Backend;

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
        BaseRecipe Healpotion = new BaseRecipe(PlayerModel.PhialType.Heal);
        Healpotion.addNeededIngredient(Strings.WATER, 1);
        Healpotion.addNeededIngredient(Strings.HERB, 1);
        ListOfRecipes.Add(PlayerModel.PhialType.Heal.ToString(), Healpotion);

        BaseRecipe Firepotion = new BaseRecipe(PlayerModel.PhialType.Fire);
        Firepotion.addNeededIngredient(Strings.OIL, 1);
        Firepotion.addNeededIngredient(Strings.PHOENIXASH, 1);
        ListOfRecipes.Add(PlayerModel.PhialType.Fire.ToString(), Firepotion);

        BaseRecipe Icepotion = new BaseRecipe(PlayerModel.PhialType.Ice);
        Icepotion.addNeededIngredient(Strings.WATER, 1);
        Icepotion.addNeededIngredient(Strings.CRISTALFLOWER, 1);
        ListOfRecipes.Add(PlayerModel.PhialType.Ice.ToString(), Icepotion);
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


