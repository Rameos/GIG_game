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

    private Vector3 destinationPoint;

    public GameObject explosionEffect;

    public void Init(Vector3 forceVector, Damage poisonInformation, int parentType)
    {
        this.damageInformation = poisonInformation;
        this.parentType = parentType;
        directionPoison = forceVector;
    }

    public void Init(Vector3 forceVector, Damage poisonInformation, int parentType, Vector3 destinationPoint)
    {
        this.destinationPoint = destinationPoint;
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

        this.transform.position = Vector3.Lerp(transform.position, destinationPoint,0.2f);
	}

    void OnCollisionEnter(Collision information)
    {

        Gamestatemanager.OnRumbleEvent(1, 1, 1);
        Debug.Log("Boom");
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
