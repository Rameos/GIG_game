using UnityEngine;
using System.Collections;
using Controller;
public class FieldDamage : MonoBehaviour {

    [SerializeField]
    private int damagePoints;

    [SerializeField]
    private Backend.PlayerModel.DamageTypes damageType;

    [SerializeField]
    private float damageRadius;

    private Backend.Damage damage;

    public void doDamageAtEnemies(Collider[] enemies)
    {
        foreach(Collider enemyCollider in enemies)
        {
            if(enemyCollider.collider.tag == Constants.TAG_ENEMY)
            {
                Debug.Log("EnemyColliderName: " + enemyCollider);
                EnemyObject enemyScript = enemyCollider.gameObject.GetComponent<EnemyObject>();
                damage = new Backend.Damage(damagePoints, damageType);
                if(enemyScript!= null)
                {
                    enemyScript.ApplyDamage(damage);
                }
            }
        }
    }


}
