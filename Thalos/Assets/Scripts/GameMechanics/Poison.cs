using UnityEngine;
using System.Collections;
using Controller; 
using Backend;
public class Poison : MonoBehaviour {

    private Damage damageInformation;
    private float forcePower = 5f;
    private int parentType;
    private float throwDistance = 5f;
    private Vector3 directionPoison;

    public GameObject explosionEffect;

    public void Init(Vector3 forceVector, Damage poisonInformation, int parentType)
    {
        this.damageInformation = poisonInformation;
        this.parentType = parentType;
        directionPoison = forceVector;
    }
	// Use this for initialization
	void Start () {



        //Quaternion lookAtRotation = Quaternion.LookRotation();
        //transform.rotation = lookAtRotation;//Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime);

        rigidbody.velocity = new Vector3(0, forcePower, 0);
        rigidbody.velocity += directionPoison*forcePower;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision information)
    {
        Debug.Log("Boom");
        Gamestatemanager.OnRumbleEvent(2, 2, 1);
        if(information.collider.gameObject.tag != Constants.TAG_PLAYER)
        {
            StartCoroutine(explosion(information.contacts[0].point));
            
        }
    }

    private IEnumerator explosion(Vector3 position)
    {
        yield return new WaitForSeconds(0.1f);

        Instantiate(explosionEffect, position, explosionEffect.transform.rotation);
        Destroy(this);
    }

}
