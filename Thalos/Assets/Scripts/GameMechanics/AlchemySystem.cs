using UnityEngine;
using System.Collections;
using Backend; 

public class AlchemySystem : MonoBehaviour
{
    public bool createPhiole(BaseRecipe Recipe)
    {
        Hashtable itemsOfPlayer = PlayerModel.Instance().convertInventoryToHashTable();
        if(PlayerModel.Instance().phialSizeMax>PlayerModel.Instance().PhialInventory.Count)
        {
            if (Recipe.checkIfBrewingPossible(itemsOfPlayer))
            {
  
                PlayerModel.Instance().addPhialToinventory(Recipe.phialType);
                removeIngrediences(Recipe);
                return true;
            }
        }
        return false;
    }

    private void removeIngrediences(BaseRecipe Recipe)
    {
        Hashtable neededResources = Recipe.getNeededIngredients();

        foreach(string name in neededResources.Keys)
        {
            int count = (int)neededResources[name];
            BaseIngredient item = Ingredients.Instance().GetSingleIngredient(name);
            PlayerModel.Instance().removeCountOfIngredience(item, count);
        }
    }
}
