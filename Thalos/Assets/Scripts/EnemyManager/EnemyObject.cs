using UnityEngine;
using System.Collections;
using Enemy;
using Backend; 

public class EnemyObject : MonoBehaviour {

    //Sight

    private float fieldOfViewAngle = 110f;
    private bool playerIsInSight;
    private Vector3 personalLastSightingPlayer;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    
    private Vector3 globalLastSightingPlayer;   //WARNING: INTO Model
    private GameObject player;
    private Animator playerAnim;
    private Vector3 previousSighing;


    private BaseEnemy enemyManager;
    public enemyType actualEnemy;




    [SerializeField]
    private float shootfrequence = 2f;
    
    [SerializeField]
    private float isAlarmed_Distance = 20f;
    
    public enum enemyType
    {
        policeman_LVL01,
        policeman_LVL02,
        policeman_LVL03,
        roboter_LVL01,
    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>(); 

        
    }
    
    void Start () {

        switch (actualEnemy)
        {
            case enemyType.policeman_LVL01:
                enemyManager = new Policeman(100,5,5);
                break;

            case enemyType.policeman_LVL02:
                enemyManager = new Policeman(150, 5, 5);
                break;

            case enemyType.policeman_LVL03:
                enemyManager = new Policeman(200, 5, 5);
                break;

            case enemyType.roboter_LVL01:
                enemyManager = new Robot(300, 10, 20);
                break;
        }

        //Debug: SendDamage
        Damage debugdamage = new Damage(100, PlayerModel.DamageTypes.Standard);
        SendMessage("ApplyDamage", debugdamage);
	}
	
	
    void FixedUpdate () {
        
	    
	}

    private bool isPlayerVisible()
    {
        return false;
    }

    private void ApplyDamage(Damage damage)
    {
        Debug.Log("Old Health: " + enemyManager.LivePoints);
        int health = enemyManager.TakeDamage(damage.damage, damage.typeDamage);
        Debug.Log("New Health: " + health);
    }
}
