using UnityEngine;
using System.Collections;
using Backend;
using Controller; 
public class DeathZone : MonoBehaviour {


    void Start()
    {
        renderer.enabled = false; 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.TAG_PLAYER)
        {
            PlayerModel.Instance().HealthPoints = 0;
            
        }
    }
}
