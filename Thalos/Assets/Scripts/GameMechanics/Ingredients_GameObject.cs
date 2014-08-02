using UnityEngine;
using System.Collections;
using Backend;

public class Debug_ShowInventory : MonoBehaviour
{

    public enum IngredienceType
    {
        Water,
        Oil,
        Herb,
        Phoenixash,
        Cristalflower
    }
    [SerializeField]
    public IngredienceType type;

    void OnTriggerEnter(Collider other)
    {
        PlayerModel.Instance().addIngredience(getIngredience());
    }

    BaseIngredient getIngredience()
    {
        switch (type)
        {
            case IngredienceType.Water:
                return Ingredients.Instance().GetSingleIngredient(Strings.WATER);
            case IngredienceType.Phoenixash:
                return Ingredients.Instance().GetSingleIngredient(Strings.PHOENIXASH);
            case IngredienceType.Oil:
                return Ingredients.Instance().GetSingleIngredient(Strings.OIL);
            case IngredienceType.Herb:
                return Ingredients.Instance().GetSingleIngredient(Strings.HERB);
            case IngredienceType.Cristalflower:
                return Ingredients.Instance().GetSingleIngredient(Strings.CRISTALFLOWER);

        }
        return null;
    }
}