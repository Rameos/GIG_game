using UnityEngine;
using System.Collections;
using Enemy;
using Backend;
using Controller; 

public class EnemyObject : MonoBehaviour {

    //Sight
    [SerializeField]
    private float coolDown = 2f;
    [SerializeField]
    private float shootfrequence = 2f;
    [SerializeField]
    private float isAlarmed_Distance = 20f;
    [SerializeField]
    private GameObject explosion; 


    private float fieldOfViewAngle = 110f;
    private bool playerIsInSight = false;
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
    private bool isAlive = true;
    private Animator animator;
    
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
        animator = gameObject.GetComponentInChildren<Animator>();

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
                enemyManager = new Robot(30, 1, 5);
                damageInformation = new Damage(enemyManager.Damage, PlayerModel.DamageTypes.Standard);
                break;
        }

	}

    void Update()
    {
        if (isAlive)
        {
            if (globalLastSightingPlayer != previousSighting)
            {
                personalLastSightingPlayer = globalLastSightingPlayer;
            }
        
        
        }

        Destroyitem();

        updateAnimation();
        
    }

    private void updateAnimation()
    {
        float distance = Vector3.Distance(gameObject.GetComponentInChildren<NavMeshAgent>().destination, transform.position);
        
        if (distance > 0.1f)
        {
            try
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Shoot", false);
       
            }
            catch
            {

            }
             }
        else
        {
            animator.SetBool("Walk", false);

        }
    }

    void OnTriggerStay(Collider other)
    {
        //Is In Sphere
        if (other.gameObject == player)
        {
            playerIsInSight = false;

            Vector3 direction = other.transform.position - transform.position;

            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < fieldOfViewAngle * 0.75f)
            {
                RaycastHit hit = checkPlayerIsInSight(direction);
                
                //Behaviour 
                updateBehaviour(angle);


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
                playerIsInSight = true;
                globalLastSightingPlayer = player.transform.position;
                TurnToPlayer();
            }
        }

        return hit;
    }

    /// <summary>
    /// activate shooting, when Player stands at the 0.5f radius of the Sphere Collider and is visible
    /// </summary>
    /// <param name="angle"></param>
    void updateBehaviour(float angle)
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float distanceNaveMesh = gameObject.GetComponent<NavMeshAgent>().remainingDistance;
         NavMeshPath navPath = new NavMeshPath();
         gameObject.GetComponent<NavMeshAgent>().CalculatePath(player.transform.position, navPath);
       // Debug.Log("DistanceNaveMesh:" + distanceNaveMesh);


         gameObject.GetComponent<NavMeshAgent>().destination = transform.position;

        if (angle < fieldOfViewAngle * 0.65f && distance <= col.radius * 0.75f)
        {
            ShotPlayer(damageInformation);
            animator.SetBool("Shoot", true);
            playerIsInSight = true;

        }
        else
        {

            gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
            playerIsInSight = false;
        }

    }

    void Destroyitem()
    {
        if(!isAlive)
        {
            Gamestatemanager.OnRumbleEvent(2, 1, 1);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(this.gameObject);
        }
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
        transform.rotation = Quaternion.Lerp(transform.rotation,lookAtRotation,0.2f);
    }

    void ShotPlayer(Damage debugdamage)
    {
        if (isShotable&&isAlive)
        {
            animator.SetBool("Shoot", true);
            StartCoroutine(ShotPlayerCoolDown(debugdamage));

        }

    }


    public void ApplyDamage(Damage damage)
    {
        Debug.Log("Old Health: " + enemyManager.LivePoints);
        int health = enemyManager.TakeDamage(damage.damage, damage.typeDamage);

        if (enemyManager.LivePoints <= 0)
        {
            isAlive = false;
        }

    }

    IEnumerator ShotPlayerCoolDown(Damage debugdamage)
    {
        GetComponent<Debug_BulletCaster>().shoot(debugdamage);
        isShotable = false;
        yield return new WaitForSeconds(coolDown);
        isShotable = true;
    }
}
