using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header ("For Combat")] //I have no idea why this header in particular doesn't work
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown;
    private Transform player;
    [SerializeField] private int Damage;

    [Header ("Area of Sight")]
    [SerializeField] private float Xrange;
    [SerializeField] private float Yrange;
    [SerializeField] private float colliderDistance;
    [SerializeField] private Collider2D boxCollider;    
    [SerializeField] private LayerMask Player;

    [Header ("If Ranged")]
    public bool IsRanged = false; //if ranged, will shoot when player is in sight.
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Transform projectilePrefabScale;
    public Enemy1ProjectileChauncey DoDamage;

    [Header ("If Melee")]
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance = 0f;
    public Rigidbody2D rigidBody2D;
    //private Animator anim;


    private void Awake()
    {
        //anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidBody2D = GetComponent<Rigidbody2D>();
        DoDamage = projectilePrefab.GetComponent<Enemy1ProjectileChauncey>();
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight()) //Attacks if the enemy is within the red box ("sight") around the enemy
        {
            if (IsRanged == true)
                {if (cooldownTimer >= attackCooldown)
                {
                    if (gameObject != null)
                    {
                        cooldownTimer = 0;
                        //anim.SetTrigger("IsShooting");
                        projectilePrefabScale.localScale = transform.localScale;
                        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                        DoDamage.Damage = Damage;
                    }
                }
            }else
            {
                if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    Vector2 moveDir = (player.transform.position-transform.position).normalized*speed;
                    rigidBody2D.velocity = new Vector2(moveDir.x, rigidBody2D.velocity.y);
                }
            }
        }
    }
    private bool PlayerInSight() //Defines the parameters of the enemy sight.
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * Xrange * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * Xrange, boxCollider.bounds.size.y*Yrange, boxCollider.bounds.size.z),
            0, Vector2.left, 0, Player);
        return hit.collider != null;
    }
    private void OnDrawGizmos() //No gameplay function but visually shows a red box for what counts as sight...
                                //KEEP NOTE: If you change the parameters above, try to make the same changes below.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * Xrange* transform.localScale.x * colliderDistance,
            new Vector3 (boxCollider.bounds.size.x * Xrange, boxCollider.bounds.size.y*Yrange, boxCollider.bounds.size.z));
    }
}