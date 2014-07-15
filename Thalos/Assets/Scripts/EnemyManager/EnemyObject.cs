using UnityEngine;
using System.Collections;
using Enemy;
using Backend;
using Controller; 

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
    private Vector3 previousSighting;


    private Damage damageInformation;

    private BaseEnemy enemyManager;
    public enemyType actualEnemy;

    private bool isShotable = true;

    [SerializeField]
    private float coolDown = 2f;
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
        globalLastSightingPlayer = Vector3.zero;
        player = GameObject.FindGameObjectWithTag("Player");


        personalLastSightingPlayer = Vector3.zero;
        previousSighting = Vector3.zero; 
    }

    
    void Start() {

        switch (actualEnemy)
        {
            case enemyType.policeman_LVL01:
                enemyManager = new Policeman(100,5,5);
                damageInformation = new Damage(enemyManager.Damage, PlayerModel.DamageTypes.Standard);
                break;

            case enemyType.policeman_LVL02:
                enemyManager = new Policeman(150, 5, 5);
                damageInformation = new Damage(enemyManager.Damage, PlayerModel.DamageTypes.Fire);
                break;

            case enemyType.policeman_LVL03:
                enemyManager = new Policeman(200, 5, 5);
                damageInformation = new Damage(enemyManager.Damage, PlayerModel.DamageTypes.Ice);
                break;

            case enemyType.roboter_LVL01:
                enemyManager = new Robot(300, 10, 20);
                damageInformation = new Damage(enemyManager.Damage, PlayerModel.DamageTypes.Standard);
                break;
        }

	}

    void Update()
    {
        if (globalLastSightingPlayer != previousSighting)
        {
            personalLastSightingPlayer = globalLastSightingPlayer;
        }

        //UpdateAnimation When Status Changed;

    }

    void OnTriggerStay(Collider other)
    {
        //Is In Sphere
        if (other.gameObject == player)
        {
            playerIsInSight = false;

            Vector3 direction = other.transform.position - transform.position;

            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit = checkPlayerIsInSight(direction);
                
                if (angle < fieldOfViewAngle * 0.4f)
                    ShotPlayer(damageInformation);
            }



            //ANIMATIONEN
        }
    }

    RaycastHit checkPlayerIsInSight( Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
        {
            if (hit.collider.gameObject == player)
            {
               //Debug.Log("PlayerDetected");
                playerIsInSight = true;
                globalLastSightingPlayer = player.transform.position;
                TurnToPlayer();
            }
        }

        return hit;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIsInSight = false;
        }
    }

    void TurnToPlayer()
    {
        Vector3 destinationPoint = player.transform.position - transform.position;
        destinationPoint.y = 0;

        Quaternion lookAtRotation = Quaternion.LookRotation(destinationPoint);
        transform.rotation = lookAtRotation;
    }

    void ShotPlayer(Damage debugdamage)
    {
        if (isShotable)
        {
            StartCoroutine(ShotPlayerCoolDown(debugdamage));
        }
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

    IEnumerator ShotPlayerCoolDown(Damage debugdamage)
    {
        GetComponent<Debug_BulletCaster>().shoot(debugdamage);
        isShotable = false;
        yield return new WaitForSeconds(coolDown);
        isShotable = true;
    }
}
