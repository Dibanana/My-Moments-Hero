using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorder : MonoBehaviour
{
    //public GameObject topRightLimitObject;
    //public GameObject bottomLeftLimitObject;
    [SerializeField] private bool IsPlayer = false;
    private Vector3 topRightLimit;
    private Vector3 bottomLeftLimit;
    private PlayerController playerController = null;
    private EnemyDrag EnemyController = null;
    public bool TouchingBorder = false;

    //private Vector2 input;

    void Start()
    {
        UpdateBorder();
        if (IsPlayer == true)
        {
            playerController = GetComponent<PlayerController>();
        }else{
            EnemyController = GetComponent<EnemyDrag>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x >= topRightLimit.x || transform.position.y >= topRightLimit.y|| transform.position.y <= bottomLeftLimit.y|| transform.position.x <= bottomLeftLimit.x)
        {
            HitBorder();
            TouchingBorder = true;
        } else {
            StartCoroutine(WaitForFrame());
            TouchingBorder = false;
        }
    }
    void HitBorder()
    {
        //Debug.Log("Has Hit Border");
        if(IsPlayer == true)
        {
            if ((transform.position.x >= topRightLimit.x && playerController.rb.velocity.x > 0) || (transform.position.x <= bottomLeftLimit.x && playerController.rb.velocity.x <0))
            {
                playerController.CanGoSide = false;
            } else{
                playerController.CanGoSide = true;
            }
            if ((transform.position.y > topRightLimit.y && playerController.rb.velocity.y > 0)||(transform.position.y < bottomLeftLimit.y && playerController.rb.velocity.y <0))
            {
                playerController.CanGoVert = false;
            } else{
                playerController.CanGoVert = true;
            }
        } else{
            if (EnemyController.IsDead == false)
            {
                EnemyController.HitBorder();
            }
        }        
    }
    IEnumerator WaitForFrame() //prevent damage from affecting the instance multiple times in one frame (a serious problem with projectile attacks)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
    }
    public void UpdateBorder()
    {
        topRightLimit = GameObject.Find("TopRight").transform.position;
        bottomLeftLimit = GameObject.Find("BottomLeft").transform.position;
    }
}
