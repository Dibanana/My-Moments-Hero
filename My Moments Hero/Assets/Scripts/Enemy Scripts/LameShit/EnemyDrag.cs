using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Follow drag function
public class EnemyDrag : MonoBehaviour
{   
    Vector3 mousePosition;
    public int Healthpool = 3;
    public float moveSpeed = .1f;
    public float ThrowImpulse;
    [SerializeField] private float ScaleChange;
    [SerializeField] private GameObject Leaf;
    public Rigidbody2D rb;
    [SerializeField] private Transform thisThing;
    Vector2 position;
    public EnemySpawnApproach GamePlay = null;
    private ScreenBorder ScreenTouch;

    private bool IsDragging = false;
    public bool IsDead;
    public bool CanDrag = true;
    private bool IsImmortal = true;
    [SerializeField]protected bool isGameplay = true;
    private SpriteRenderer ChildObject;

    private void Start()
    {
        //To make sure it spawns in the right location
        //Leaf = GetComponent<GameObject>();
        if(isGameplay == true)
            GamePlay = GameObject.Find("EnemySpawner").GetComponent<EnemySpawnApproach>();
        ScreenTouch = GetComponent<ScreenBorder>();

        thisThing = GetComponent<Transform>();
        position = Vector2.Lerp(transform.position, transform.position, moveSpeed);

        //Set up the child object's renderer so that it can change opacity
        ChildObject = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
    private void FixedUpdate()
    {
        if(IsDragging == true)
        {
            rb.MovePosition(position);
        }
        if (IsDead == true)
        {
            CanDrag = false;
            Die();
        }
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
        if (collisionInfo.CompareTag("Player"))
        {
            collisionInfo.GetComponent<Health>().TakeDamage(1);
        }
    }
    void CreateSmallLeafs(int Num)
    {
        int newGeneration = Healthpool - 1;
        GetMousePosition();
        for (int i = 1; i <= Num; i++)
        {
            float scaleSize = 0.5f;
            GameObject LeafClone = Instantiate(this.Leaf, new Vector3(transform.position.x+Random.Range(-1.00f,1.00f), transform.position.y+Random.Range(-1.00f,1.00f), 0f), transform.rotation);
            LeafClone.transform.localScale = new Vector3(LeafClone.transform.localScale.x * scaleSize, LeafClone.transform.localScale.y * scaleSize, LeafClone.transform.localScale.z * scaleSize);
            LeafClone.GetComponent<EnemyDrag>().Healthpool = newGeneration;
            LeafClone.GetComponent<EnemyDrag>().ThrowImpulse = ThrowImpulse*scaleSize;
            LeafClone.GetComponent<EnemyDrag>().rb.AddForce((LeafClone.transform.position-mousePosition)*10, ForceMode2D.Impulse);
            LeafClone.SetActive(true);
        }
    }
    public void DamageLeaf()
    {
        if (Healthpool > 1)
        {
            CreateSmallLeafs(2);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void HitBorder()
    {
        if (IsImmortal == true)
        {
            StartCoroutine(WaitForFrame());
        }
        if (Healthpool < 3 || IsImmortal == false)
        {
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
        yield return new WaitForSeconds(1);
        yield return new WaitForEndOfFrame();
        if (GamePlay != null)
            GamePlay.currentEnemies = GamePlay.currentEnemies -1;
        Destroy(gameObject);
    }
    IEnumerator WaitForFrame() //prevent damage from affecting the instance multiple times in one frame (a serious problem with projectile attacks)
    {
        yield return new WaitForSeconds(2);
        IsImmortal = false;
    }
}