using UnityEngine;
using System.Collections;

public class BaseRecipe {

    public string name { get; set; }

    private Hashtable NeededIngredients = new Hashtable();

    public BaseRecipe(string name) 
    {
        this.name = name;
    }

    public void addNeededIngredient(string ingredient, int num) 
    {
        if (NeededIngredients.ContainsKey(ingredient))
        {
            NeededIngredients[ingredient] = num;
        }
        else 
        {
            NeededIngredients.Add(ingredient, num);     
        }
    }

    public Hashtable getNeededIngredients() 
    {
        return NeededIngredients;
    }

    public bool checkIfBrewingPossible(Hashtable ingredients) 
    {
        foreach (DictionaryEntry element in NeededIngredients)
        {
            if (!ingredients.ContainsKey(element.Key)) 
            {
                return false;    
            }
            if((int)element.Value > (int)ingredients[element.Key])
            {
                return false;
            }
        }

        return true;
    }

}
