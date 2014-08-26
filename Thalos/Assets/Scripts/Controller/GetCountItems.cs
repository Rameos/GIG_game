using UnityEngine;
using System.Collections;
using Backend;
public class GetCountItems : MonoBehaviour {

    [SerializeField]
    private PlayerModel.IngredienceType typeItem;
    
    [SerializeField]
    private PlayerModel.PhialType typePhial;
    private TextMesh meshRenderer;
    void Start () {
        meshRenderer = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (typePhial)
        {
            case PlayerModel.PhialType.Empty:
                switch (typeItem)
                {
                    case PlayerModel.IngredienceType.Cristalflower:
                        meshRenderer.text = "" + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.CRISTALFLOWER));
                        break;
                    case PlayerModel.IngredienceType.Herb:
                        meshRenderer.text = "" + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.HERB));
                        break;
                    case PlayerModel.IngredienceType.Oil:
                        meshRenderer.text = "" + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.OIL));
                        break;
                    case PlayerModel.IngredienceType.Phoenixash:
                        meshRenderer.text = "" + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.PHOENIXASH));
                        break;
                    case PlayerModel.IngredienceType.Water:
                        meshRenderer.text = "" + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.WATER));
                        break;

                }
                break;

            case PlayerModel.PhialType.Fire:
            case PlayerModel.PhialType.Heal:
            case PlayerModel.PhialType.Ice:
                meshRenderer.text = "" + PlayerModel.Instance().getCountOfPhialsOfSortInInventory(typePhial);
                break;
        }


	}
}
