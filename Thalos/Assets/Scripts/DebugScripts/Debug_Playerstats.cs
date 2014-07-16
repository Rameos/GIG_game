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


       if (GUI.Button(new Rect(200, 20, 50, 20), "-10LP"))
       {
           Gamestatemanager.OnPlayerGetsDamage(10);
       }
    }
}
