using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionScreenBorder : MonoBehaviour
{
    //public GameObject topRightLimitObject;
    //public GameObject bottomLeftLimitObject;
    [SerializeField] private bool IsPlayer = false;
    private Vector3 topRightLimit;
    private Vector3 bottomLeftLimit;
    private PlayerController playerController = null;
    private EmotionDrag EnemyController = null;
    public bool TouchingBorder = false;

    //private Vector2 input;

    void Start()
    {
        UpdateBorder();
        if (IsPlayer == true)
        {
            playerController = GetComponent<PlayerController>();
        }else{
            EnemyController = GetComponent<EmotionDrag>();
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
            if ((transform.position.x >= topRightLimit.x && EnemyController.rb.velocity.x > 0) || (transform.position.x <= bottomLeftLimit.x && EnemyController.rb.velocity.x <0))
            {
                EnemyController.CanGoSide = false;
            } else{
                EnemyController.CanGoSide = true;
            }
            if ((transform.position.y > topRightLimit.y && EnemyController.rb.velocity.y > 0)||(transform.position.y < bottomLeftLimit.y && EnemyController.rb.velocity.y <0))
            {
                EnemyController.CanGoVert = false;
            } else{
                EnemyController.CanGoVert = true;
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
