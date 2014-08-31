using UnityEngine;
using System.Collections;

public class ocullusionZone : MonoBehaviour {

    public bool newStatus;

    public GameObject sceneItem;

    void Start()
    {
        renderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Controller.Constants.TAG_PLAYER)
        {
            sceneItem.SetActive(newStatus);
        }
    }



}
