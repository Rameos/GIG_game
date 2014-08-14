using UnityEngine;
using System.Collections;
using Backend;
using Controller;
public class Ingredients_GameObject : MonoBehaviour
{


    [SerializeField]
    public PlayerModel.IngredienceType type;

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
            case PlayerModel.IngredienceType.Water:
                return Ingredients.Instance().GetSingleIngredient(Strings.WATER);
            case PlayerModel.IngredienceType.Phoenixash:
                return Ingredients.Instance().GetSingleIngredient(Strings.PHOENIXASH);
            case PlayerModel.IngredienceType.Oil:
                return Ingredients.Instance().GetSingleIngredient(Strings.OIL);
            case PlayerModel.IngredienceType.Herb:
                return Ingredients.Instance().GetSingleIngredient(Strings.HERB);
            case PlayerModel.IngredienceType.Cristalflower:
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