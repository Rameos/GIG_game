using UnityEngine;
using System.Collections;

public class FieldDamage : MonoBehaviour {


    [SerializeField]
    private int damagePoints;

    [SerializeField]
    private Backend.PlayerModel.DamageTypes damageType;

    [SerializeField]
    private float damageRadius;

    private Backend.Damage damage;

    public void doDamageAtEnemies()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, damageRadius);

        foreach(Collider enemyCollider in enemies)
        {
            EnemyObject enemyScript = enemyCollider.gameObject.GetComponent<EnemyObject>();
            if (enemyScript != null)
            {
                enemyScript.ApplyDamage(damage);
            }
        }
    }
	// Use this for initialization
	void Start () {
        damage = new Backend.Damage(damagePoints,damageType);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
