using UnityEngine;
using System.Collections;

public class TriggerParticleSystem : MonoBehaviour {


    public bool debug = false; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        if(!debug)
        {

            particleSystem.Play();
        }

	}
}
