using UnityEngine;
using System.Collections;
using Controller;
using Backend; 

public class WinningTheme : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.TAG_PLAYER)
        {
            GameObject.FindGameObjectWithTag(Constants.TAG_GAMEMANAGER).GetComponent<Gamestatemanager>().loadLevel(-1);
        }
    }

}
