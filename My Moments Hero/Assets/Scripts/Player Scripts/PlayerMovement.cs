using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    public float KnockbackForce;
    public float KnockbackCounter;
    public float KnockbackTotalTime;

    public bool KnockbackRight;

    private bool DoubleJump;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {   
        horizontal = Input.GetAxisRaw("Horizontal");

        if(IsGrounded() && !Input.GetButton("Jump"))
        {
            DoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }else{
                if(DoubleJump==true)
                {
                    DoubleJump = !DoubleJump;
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                }
            }

        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            return;
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            //changes player velocity to half when letting go of jump key
            //In practice, makes player jump higher by holding jump.
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if(KnockbackCounter <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            if (IsGrounded() && Input.GetAxisRaw("Horizontal") == 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
            //This is most likely just normal player movement. If no knockback=movement remains normal.
        } else
        {
            if(KnockbackRight == true)
            {
                rb.velocity = new Vector2(0,0);
                rb.AddForce(new Vector2(-KnockbackForce,KnockbackForce));
            }
            if(KnockbackRight ==false)
            {
                rb.velocity = new Vector2(0,0);
                rb.AddForce(new Vector2(KnockbackForce,KnockbackForce));
            }
            KnockbackCounter -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
