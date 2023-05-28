using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Follow drag function
public class EmotionDrag : MonoBehaviour
{   
    Vector3 mousePosition;
    public int Healthpool = 1;
    public float moveSpeed = .1f;
    public float ThrowImpulse;
    [SerializeField] private float ScaleChange;
    [SerializeField] private GameObject Leaf;
    public Rigidbody2D rb;
    [SerializeField] private Transform thisThing;
    Vector2 position;
    public EmotionSpawnApproach GamePlay = null;
    private ScreenBorder ScreenTouch;
    private TrailRenderer ObjectTrail;

    private bool IsDragging = false;
    public bool IsDead;
    public bool CanDrag = true;
    private bool IsImmortal = true;
    private SpriteRenderer ChildObject;

    public bool CanGoSide = true;
    public bool CanGoVert = true;

    [SerializeField]private float AttractionForce;
    private Transform Heart;
    private Vector2 AttractionDirection;

    private void Start()
    {
        //To make sure it spawns in the right location
        //Leaf = GetComponent<GameObject>();
        GamePlay = GameObject.Find("EnemySpawner").GetComponent<EmotionSpawnApproach>();
        ScreenTouch = GetComponent<ScreenBorder>();
        thisThing = GetComponent<Transform>();
        position = Vector2.Lerp(transform.position, transform.position, moveSpeed);

        //Set up the child object's renderer so that it can change opacity
        ChildObject = gameObject.GetComponentInChildren<SpriteRenderer>();
        ObjectTrail = gameObject.GetComponentInChildren<TrailRenderer>();
        Heart = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (CanGoSide == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (CanGoVert == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        if (rb.velocity.x > 1 || rb.velocity.x < -1 || rb.velocity.y > 1 || rb.velocity.y <-1)
        {
            ObjectTrail.enabled = true;
        } else if(IsDragging == false){
            ObjectTrail.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        if(IsDragging == true)
        {
            ObjectTrail.enabled = true;
            rb.MovePosition(position);
        }
        if (IsDead == true)
        {
            CanDrag = false;
            Die();
        }
        AttractionForce += Time.deltaTime * .005f;
        AttractionDirection = new Vector2 ((Heart.position.x - gameObject.transform.position.x)*AttractionForce, (Heart.position.y - gameObject.transform.position.y)*AttractionForce);
        rb.AddForce(AttractionDirection, ForceMode2D.Force);//Add a constant Force towards the center of the screen
    }
    private void OnMouseDrag()
    {
        if (CanDrag == true)
        {
            IsDragging = true;
            GetMousePosition();
        }else
        {
            OnMouseUp();
        }

    }
    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);        
    }
    private void OnMouseUp()
    {
        IsDragging = false;
        rb.velocity = new Vector2(0,0);
        rb.AddForce((mousePosition - transform.position)*ThrowImpulse, ForceMode2D.Impulse);
    }
    private void OnMouseOver()
    {
        //ChildObject
        if (Input.GetMouseButtonDown(1))
            DamageLeaf();
    }
    private void OnMouseExit()
    {
        //ChildObject
    }
    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if(collisionInfo.CompareTag("Enemy"))
            return;

        if (collisionInfo.CompareTag("Player")) //Player tag doesnt seem to work for some reason
        {
            collisionInfo.GetComponent<Health>().TakeDamage(10);    
        }
        Die();
    }
    public void DamageLeaf()
    {
        rb.AddForce((transform.position - Heart.position)*(ThrowImpulse*10), ForceMode2D.Impulse); //knock emotion away from heart
    }
    public void HitBorder()
    {
        if (IsImmortal == true)
        {
            StartCoroutine(WaitForFrame());
        } else {
            Die();
        }
    }
    public void Die()
    {
        IsDead = true;
        if(transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x - ScaleChange, transform.localScale.y - ScaleChange, transform.localScale.z - ScaleChange);
        StartCoroutine(DeathWait());
    }
    IEnumerator DeathWait() //prevent damage from affecting the instance multiple times in one frame (a serious problem with projectile attacks)
    {
        yield return new WaitForEndOfFrame();
        if (GamePlay != null)
            GamePlay.currentEnemies = GamePlay.currentEnemies -1;
        Destroy(gameObject);
    }
    IEnumerator WaitForFrame() //prevent damage from affecting the instance multiple times in one frame (a serious problem with projectile attacks)
    {
        yield return new WaitForSeconds(4);
        IsImmortal = false;
    }
}