using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool VelocityTooHigh = true;
    public int MaxAngularVelocity;
    public float MovementSpeed;
    public int LookToModifier;
    public float standardVelocity;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float CurrentAngularVelocity = 0f;
    public Rigidbody2D rb;
    public bool CanGoSide = true;
    public bool CanGoVert = true;
    public bool DoTurn = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        CurrentAngularVelocity = rb.angularVelocity * .99f;
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            NotGettingInput();
        } else{
            IsGettingInput();
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                IsGoingUp();
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                IsGoingSide();
            }
        } 
        IsGoingBoth();
        if (CanGoSide == false)
            rb.velocity = new Vector2(0, rb.velocity.y);
        if(CanGoVert == false)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        if (rb.angularVelocity > MaxAngularVelocity || -MaxAngularVelocity > rb.angularVelocity)
        {
            VelocityTooHigh = true;
            if (VelocityTooHigh == true)
                LimitVelocity();
        }
        else
        {
            VelocityTooHigh = false;
        }
    }
    void OnCollisionStay2D (Collision2D collision)
    {
        DoTurn = false;
    }
    void OnCollisionExit2D (Collision2D other)
    {
        DoTurn = true;
    }
    private void IsGoingUp()
    {
        rb.velocity = new Vector2(rb.velocity.x, MovementSpeed*Input.GetAxisRaw("Vertical")+rb.velocity.y);
        return;
    }
    private void IsGoingSide()
    {
        rb.velocity = new Vector2(MovementSpeed * Input.GetAxisRaw("Horizontal")+rb.velocity.x, rb.velocity.y);
        //rb.AddTorque(RotationSpeed*Time.fixedDeltaTime*Input.GetAxisRaw("Horizontal"), ForceMode2D.Force);
        rb.angularVelocity = rb.angularVelocity + standardVelocity*Input.GetAxisRaw("Horizontal");
        return;        
    }
    private void IsGoingBoth()
    {
        rb.velocity = new Vector2 (MovementSpeed * Input.GetAxisRaw("Horizontal")+rb.velocity.x, MovementSpeed*Input.GetAxisRaw("Vertical")+rb.velocity.y);
        //rb.AddTorque(RotationSpeed*Time.fixedDeltaTime*Input.GetAxisRaw("Horizontal"), ForceMode2D.Force);
        rb.angularVelocity = rb.angularVelocity + standardVelocity*Input.GetAxisRaw("Horizontal");
    }
    private void NotGettingInput()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        rb.angularVelocity = CurrentAngularVelocity;
    }
    private void IsGettingInput()
    {
        Vector2 movementDirection = new Vector2 (rb.velocity.x, rb.velocity.y);
        //float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
        if(DoTurn == true)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed*LookToModifier*Time.deltaTime);
    }
    private void LimitVelocity()
    {
        if(rb.angularVelocity> MaxAngularVelocity)
        {
            rb.angularVelocity = MaxAngularVelocity;
        } else{
            rb.angularVelocity = -MaxAngularVelocity;
        }
    }
}
    //Another should be added so that if the leaf it pressed it should do a bobbing animation.
