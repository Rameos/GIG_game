using UnityEngine;
using System.Collections;
using Backend;
using Controller; 
public class ReviveZone : MonoBehaviour {

    void Start()
    {
        renderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.TAG_PLAYER)
        {
            PlayerModel.Instance().revivePosition = gameObject.transform.position;
            Debug.Log("PlayerModel.Instance().revivePosition:" + PlayerModel.Instance().revivePosition);
        } 
    }
}
