using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionSpawnApproach : MonoBehaviour
{
    [Header ("Spawn Rate / Settings")]
    private float spawnRate;
    public int LevelStage;
    //How many enemies are allowed on the field at once
    public int maxEnemies;
    public int currentEnemies = 0;
    private bool canSpawn = true;
    private EmotionDrag EmotionDrag;


    [Header ("What is Spawning")]
    //Int to act as enemy variant (For when there are enemy variants)
    [SerializeField] private int generation;
    public int LeafHealthpool;
    public GameObject RedLeaf;
    private Rigidbody2D rb;

    Sprite sprite; //Change sprite depending on health "If LeafHealthpool == 3, then sprite = Full leaf (Random Range between 1 and 3)

    [Header ("Where To Spawn")]
    //Find the screen borders, choose one at random, then spawn randomly along that line.
    //Find the border to know where to spawn objects
    private Vector3 topRightLimit;
    private Vector3 bottomLeftLimit;
    private Vector2 LeafPos;

    //Randomized float between 0-1 to tell where it will be.
    private float InitialSpawn;

    [Header ("Approach settings")]
    //Velocity and spin of the approach
    [SerializeField] private float ApproachVel;
    [SerializeField] private float approachX;
    [SerializeField] private float approachY;
    [SerializeField] private float maxSpin;
    private float InitialSpinning;
    private float InitialApproachSpeed;
    [SerializeField]private Vector2 playerPos; //if aiming at player

    Vector2 NewForce;
    void Start()
    {
        //Find the border around the screen
        topRightLimit = GameObject.Find("TopRight").transform.position;
        bottomLeftLimit = GameObject.Find("BottomLeft").transform.position;
    }
    void FixedUpdate()
    {
        if (currentEnemies < maxEnemies && canSpawn == true)
        {
            SpawnEnemy();
            canSpawn = false;
            StartCoroutine(SpawnWait());
        }
    }
    IEnumerator SpawnWait() //prevent damage from affecting the instance multiple times in one frame (a serious problem with projectile attacks)
    {
        if (LevelStage == 1)
            spawnRate = Random.Range(3f,6f);
        if (LevelStage == 2)
            spawnRate = Random.Range(1f,2f);
        if (LevelStage == 3)
            spawnRate = Random.Range(0f, 1f);
            maxEnemies += 1;
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
    }
    void SpawnEnemy()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        //Decide which border it will spawn on and the velocity that the object will approach towards in response
        OnBorder(Random.Range(1,4));
        GameObject NewLeaf = Instantiate(RedLeaf, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation);
        rb = NewLeaf.GetComponent<EmotionDrag>().rb;
        EmotionDrag = NewLeaf.GetComponent<EmotionDrag>();
        NewLeaf.transform.position = LeafPos;

        //Dictate spin
        rb.angularVelocity = InitialSpinning;
        NewForce = new Vector2(approachX, approachY);
        rb.velocity = NewForce;
        
        currentEnemies = currentEnemies+1;
        
    }
    void OnBorder(int which)
    {
        InitialSpawn = Random.Range(-1.000f,1.000f);
        if (which == 1) //if left side of screen (X must be positive)
        {
            LeafPos = new Vector2 (bottomLeftLimit.x-1,topRightLimit.y*InitialSpawn);
            approachX = Random.Range(ApproachVel*.75f, ApproachVel);
            if(approachX < 0)
                approachX = -approachX;
            approachY = Random.Range(-ApproachVel*.6f, ApproachVel*.6f);
            InitialSpinning = Random.Range(-maxSpin, maxSpin);
        }else{
            if (which == 2)//if border is top side of screen (Y must be negative)
            {
                LeafPos = new Vector2 (topRightLimit.x*InitialSpawn, topRightLimit.y+2);
                approachX = Random.Range(-ApproachVel*.6f, ApproachVel*.6f);
                approachY = Random.Range(-ApproachVel*.75f, -ApproachVel);
                if(approachY > 0)
                    approachY = -approachY;
                InitialSpinning = Random.Range(-maxSpin, maxSpin);
            }else {
                if (which == 3) //if border is right side of screen (X must be negative)
                {
                    LeafPos = new Vector2 (topRightLimit.x+1,topRightLimit.y*InitialSpawn);
                    approachX = Random.Range(-ApproachVel*.75f, -ApproachVel);
                    approachY = Random.Range(-ApproachVel*.6f, ApproachVel*.6f);
                    if(approachX > 0)
                        approachX = -approachX;
                    InitialSpinning = Random.Range(-maxSpin, maxSpin);
                } else
                { //if border is on bottom of screen (Y must be positive)
                    LeafPos = new Vector2 (topRightLimit.x*InitialSpawn, bottomLeftLimit.y-2);
                    approachX = Random.Range(-ApproachVel*.6f, ApproachVel*.6f);
                    approachY = Random.Range(ApproachVel*.75f, ApproachVel);
                    if(approachY < 0)
                        approachY = -approachY;
                    InitialSpinning = Random.Range(-maxSpin, maxSpin);
                }
            }
        }
    }
}
