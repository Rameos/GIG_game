using UnityEngine;
using System.Collections;
using Controller; 
public class CreditsButton : BaseMainMenuButton {

    [SerializeField]
    private GameObject creditsScreen;

   void OnMouseDown()
   {
       DoActionWhenActivated();
   }



   public override void DoActionWhenActivated()
   {
       creditsScreen.SetActive(true);
       Gamestatemanager.OnCloseMainMenu();
       
   }



}
