using UnityEngine;
using System.Collections;
using Controller;
using Backend;

public class PlayhealEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        particleSystem.playOnAwake = false;
        Gamestatemanager.SelectNewItemHandler += Gamestatemanager_SelectItem;
	}
	
    private void Gamestatemanager_SelectItem(Constants.selectableItemsCircleMenu selectedItem)
    {
        if(selectedItem == Constants.selectableItemsCircleMenu.HealPoison)
        {
            if(PlayerModel.Instance().checkIfPhialIsInInventory(PlayerModel.PhialType.Heal))
            {
                StartCoroutine(startHealeffect());
            }
        }
    }

    IEnumerator startHealeffect()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(3);
        particleSystem.Stop();
        particleSystem.Clear();
    }
}
