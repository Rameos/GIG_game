﻿using UnityEngine;
using System.Collections;
using Controller;

public class Bullet : MonoBehaviour {

    private float bulletSpeed;
    private Vector3 forwardVector;
    private int parentType;

    void Init(Vector3 forwardVector, int parentType)
    {
        this.forwardVector = forwardVector;
        this.parentType = parentType;
        this.bulletSpeed = Constants.BULLETSPEED; 
    }

    void FixedUpdate()
    {
        this.transform.position += forwardVector * bulletSpeed;
    }

    void OnCollisionEnter(Collision col)
    {

        switch (parentType)
        {
            case Constants.ID_PLAYER:

                if (col.gameObject.tag == Constants.TAG_ENEMY)
                {
                    Debug.Log("Enemy shot");
                }

                break; 

            case Constants.ID_ENEMY:

                if (col.gameObject.tag == Constants.TAG_PLAYER)
                {
                    Debug.Log("Player");
                }
                break;
        }

        
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