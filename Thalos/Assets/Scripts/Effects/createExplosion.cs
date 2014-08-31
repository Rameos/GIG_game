using UnityEngine;
using System.Collections;

public class createExplosion : MonoBehaviour {

    [SerializeField]
    private float force;
    
    [SerializeField]
    private float radius;

    [SerializeField]
    private float delaySec;

    private FieldDamage fieldDamagemanager;

	// Use this for initialization
	void Start () {
        fieldDamagemanager = GetComponent<FieldDamage>();
        StartCoroutine(delayExplosion());
	}
	
    IEnumerator delayExplosion()
    {
        yield return new WaitForSeconds(delaySec);
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider colliderItem in colliders)
        {
            if (colliderItem.rigidbody == null || colliderItem.gameObject.tag == Controller.Constants.TAG_PLAYER)
            {
                continue; 
            }
            colliderItem.rigidbody.AddExplosionForce(force, transform.position, radius, 1, ForceMode.Impulse);
        }
        if(fieldDamagemanager != null)
        {
            fieldDamagemanager.doDamageAtEnemies(colliders);
        }
    }
}
