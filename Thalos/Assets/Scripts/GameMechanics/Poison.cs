using UnityEngine;
using System.Collections;
using Controller; 
using Backend;
public class Poison : MonoBehaviour {

    private Damage damageInformation;
    private float forcePower = 5f;
    private int parentType;
    private float throwDistance = 5f;


    public GameObject explosionEffect;

    public void Init(Vector3 forceVector, Damage poisonInformation, int parentType)
    {
        this.damageInformation = poisonInformation;
        this.parentType = parentType;
    }
	// Use this for initialization
	void Start () {

        Vector3 destinationPoint = getDestinationPoint();
        Debug.DrawRay(transform.position, destinationPoint, Color.magenta, 2f);

        destinationPoint = destinationPoint - transform.position;

        destinationPoint.y = 0;

        Quaternion lookAtRotation = Quaternion.LookRotation(destinationPoint);
        transform.rotation = lookAtRotation;//Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime);

        rigidbody.velocity = new Vector3(0, forcePower, 0);
        rigidbody.velocity += transform.forward * forcePower;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision information)
    {
        Debug.Log("Boom");
        StartCoroutine(explosion(information.contacts[0].point));
    }


    Vector3 getDestinationPoint()
    {
        Vector3 destinationPoint;
        Vector3 gazePos = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        gazePos.y = Screen.height - gazePos.y;

        Ray screenCast = Camera.main.ScreenPointToRay(gazePos);

        RaycastHit hitInfo;
        if (Physics.Raycast(screenCast, out hitInfo, throwDistance))
        {

            destinationPoint = hitInfo.point;

            destinationPoint = new Vector3(destinationPoint.x, transform.position.y, destinationPoint.z);


            return destinationPoint;

        }
        return Vector3.zero;
    }

    private IEnumerator explosion(Vector3 position)
    {
        yield return new WaitForSeconds(0.1f);

        Instantiate(explosionEffect, position, explosionEffect.transform.rotation);
        Destroy(this);
    }

}
