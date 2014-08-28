using UnityEngine;
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

    void Update()
    {
        this.transform.Translate(0, 0, bulletSpeed, Space.Self);
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.rigidbody)
        {
            col.gameObject.rigidbody.AddForce(col.contacts[0].normal * -4f);
        }

        GameObject.Instantiate(explosion,col.contacts[0].point,explosion.transform.rotation);

        switch (parentType)
        {
            case Constants.ID_PLAYER:

                if (col.gameObject.tag == Constants.TAG_ENEMY)
                {
                    col.gameObject.transform.parent.gameObject.GetComponent<EnemyObject>().ApplyDamage(damageInformation);
                }

                break; 

            case Constants.ID_ENEMY:

                if (col.gameObject.tag == Constants.TAG_PLAYER)
                {
                    Debug.Log("Player Gets Damage!");
                    Gamestatemanager.OnPlayerGetsDamage(damageInformation.damage);
                }
                break;
        }
        Destroy(gameObject);
    }


    IEnumerator removeAfterLiveTime()
    {

        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}