using UnityEngine;
using System.Collections;
using Backend;
using Controller; 

[ExecuteInEditMode]
public class Debug_Playerstats : MonoBehaviour {

    void OnGUI()
    {
       int HP = PlayerModel.Instance().HealthPoints;
       GUI.Label(new Rect(20,20,200,20), "HealthPoints: " +  PlayerModel.Instance().HealthPoints);
       GUI.Label(new Rect(20, 40, 200, 20), "Max_HealthPoints: " + PlayerModel.Instance().MaxHealthPoints);
       GUI.Label(new Rect(20, 60, 200, 20), "Phial: " + PlayerModel.Instance().PhialInventory);
       GUI.Label(new Rect(20, 100, 200, 20), "Damage: " + PlayerModel.Instance().Damage);
       GUI.Label(new Rect(20, 120, 200, 20), "DamageType: " + PlayerModel.Instance().DamageType_Bolt);

       GUI.Label(new Rect(20, 150, 200, 20), Strings.CRISTALFLOWER + ": " + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.CRISTALFLOWER)));
       GUI.Label(new Rect(20, 170, 200, 20), Strings.HERB + ": " + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.HERB)));
       GUI.Label(new Rect(20, 190, 200, 20), Strings.OIL + ": " + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.OIL)));
       GUI.Label(new Rect(20, 210, 200, 20), Strings.PHOENIXASH + ": " + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.PHOENIXASH)));
       GUI.Label(new Rect(20, 230, 200, 20), Strings.WATER + ": " + PlayerModel.Instance().getCountOfIngredience(Ingredients.Instance().GetSingleIngredient(Strings.WATER)));

       GUI.Label(new Rect(20, 250, 220, 20), "Fire-Poison: " + PlayerModel.Instance().getCountOfPhialsOfSortInInventory(PlayerModel.PhialType.Fire));
       GUI.Label(new Rect(20, 270, 240, 20), "Ice-Poison: " + PlayerModel.Instance().getCountOfPhialsOfSortInInventory(PlayerModel.PhialType.Ice));
       GUI.Label(new Rect(20, 290, 260, 20), "Heal-Poison: " + PlayerModel.Instance().getCountOfPhialsOfSortInInventory(PlayerModel.PhialType.Heal));



       if (GUI.Button(new Rect(200, 20, 50, 20), "-10LP"))
       {
           Gamestatemanager.OnPlayerGetsDamage(10);
       }

       if(GUI.Button(new Rect (20,330, 150,20),"Create HealPoison"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.HEALPOTION));
        }


       if (GUI.Button(new Rect(20, 360, 150, 20), "Create FirePoison"))
       {

           GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.FIREPOTION));
       }


       if (GUI.Button(new Rect(20, 390, 150, 20), "Create IcePoison"))
       {

           GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.ICEPOTION));
       }
    }
}
