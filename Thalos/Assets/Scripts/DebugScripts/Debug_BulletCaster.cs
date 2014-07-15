using UnityEngine;
using System.Collections;
using Backend; 

public class Debug_BulletCaster : MonoBehaviour {

    public GameObject bulletInstance;
    public float updatetimer = 1f;

    [SerializeField]
    private Transform bulletStartPoint;

    public void shoot(Damage damage)
    {
        GameObject bullet = GameObject.Instantiate(bulletInstance,  bulletStartPoint.position, Quaternion.identity) as GameObject;
        bullet.transform.position = bulletStartPoint.position;
        bullet.GetComponent<Bullet>().Init(transform.forward, 1, damage);
    }
}
