using UnityEngine;
using System.Collections;
using Backend;
using Controller;
public class Ingredients_GameObject : MonoBehaviour
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
        if(other.tag ==Constants.TAG_PLAYER)
        {
            Debug.Log("Collect:" + getIngredience().ToString());
            PlayerModel.Instance().addIngredience(getIngredience());

            StartCoroutine(startDestroyEffect());
        }
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
    IEnumerator startDestroyEffect()
    {
        collider.active = false;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}