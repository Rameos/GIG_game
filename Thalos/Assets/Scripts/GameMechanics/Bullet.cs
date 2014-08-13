﻿using UnityEngine;
using System.Collections;
using Controller;
using Backend; 

public class Bullet : MonoBehaviour {

    private float bulletSpeed;
    private Vector3 forwardVector;
    private int parentType;
    private float lifetime = 5f;
    private Damage damageInformation;

    [SerializeField]
    private GameObject explosion;


    public void Init(Vector3 forwardVector, int parentType,Damage damageInformation)
    {
        this.damageInformation = damageInformation;
        this.forwardVector = forwardVector.normalized;
        this.parentType = parentType;

        initSpeed();

        transform.rotation = Quaternion.LookRotation(this.forwardVector);
        StartCoroutine(removeAfterLiveTime());
    }

    private void initSpeed()
    {
        switch (damageInformation.typeDamage)
        {
            case PlayerModel.DamageTypes.Standard:
                this.bulletSpeed = Constants.BULLETSPEED_STANDARD;
                break;

            case PlayerModel.DamageTypes.Fire:

                this.bulletSpeed = Constants.BULLETSPEED_FIRE;
                break;
            
            case PlayerModel.DamageTypes.Ice:

                this.bulletSpeed = Constants.BULLETSPEED_ICE;
                break;

        }
    }

    void FixedUpdate()
    {
        //this.transform.position += forwardVector.normalized;
        this.transform.Translate(0, 0, bulletSpeed, Space.Self);
    }


    void OnCollisionEnter(Collision col)
    {
        Debug.Log("ColEnter!!!!");

        if (col.gameObject.rigidbody)
        {
            col.gameObject.rigidbody.AddForce(col.contacts[0].normal * -4f);
        }

        GameObject.Instantiate(explosion,col.contacts[0].point,explosion.transform.rotation);

        Debug.Log("Parent:" + parentType);
        switch (parentType)
        {
            case Constants.ID_PLAYER:

                if (col.gameObject.tag == Constants.TAG_ENEMY)
                {
                    col.gameObject.transform.parent.gameObject.GetComponent<EnemyObject>().ApplyDamage(damageInformation);
                    Debug.Log("Enemy gets Damage!");
                }

                break; 

            case Constants.ID_ENEMY:

                if (col.gameObject.tag == Constants.TAG_PLAYER)
                {
                    Gamestatemanager.OnPlayerGetsDamage(damageInformation.damage);
                    Debug.Log("Player Gets Damage!");
                }
                break;
        }

        //ToDO: Explosion

        Destroy(gameObject);
    }


    IEnumerator removeAfterLiveTime()
    {

        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}




/*using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    private Vector3 startPos;
    
    private Vector3 endPos;
    private Rigidbody selectedElement;

    private bool done = false; 
    public float speed=2f;


    [SerializeField]
    private GameObject explosion;

    public void InitElement(Vector3 startPos, Vector3 endPos, Rigidbody element)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.selectedElement = element;
    }

    public void InitElement(Vector3 startPos, Vector3 endPos)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.selectedElement = null;
    }

	// Use this for initialization
	void Start () {
        //transform.position = startPos;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        float steps =speed*Time.time;

        transform.position = Vector3.MoveTowards(transform.position, endPos,steps);
        
        float distance = Vector3.Distance(endPos, transform.position);
       

            if (distance <= 0.1f)
            {
                if (!done)
                {
                    Camera.main.GetComponent<ExplosionManager>().CreateExplosion(endPos);
                    done = true;
                    StartCoroutine(explodeAnimation());
                }
            }
    }


    IEnumerator explodeAnimation()
    {
        this.particleSystem.enableEmission=false;
        GameObject.FindGameObjectWithTag("Audio").audio.Play();
        explosion.particleEmitter.emit = true;
        yield return new WaitForSeconds(0.1f);
        explosion.particleEmitter.emit = false;
        yield return new WaitForSeconds(2f);
        //BAD
        Destroy(this.gameObject);
    }
}
*/