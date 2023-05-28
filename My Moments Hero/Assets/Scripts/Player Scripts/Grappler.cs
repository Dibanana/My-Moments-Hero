using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Grappler : MonoBehaviour
{
    private Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    private PlayerMovement movement;
    private Rigidbody2D rb;
    private GameObject GrappleJoint;
    //[SerializeField] private GameManagerScript GameManager;

    public float moveSpeed = .1f;
    Vector3 mousePos;
    [SerializeField] private float ThrowImpulse;
    [SerializeField] private float MaxVelocity;
    public bool isGrappling = false;
    public bool isSwinging = false;
    public bool GrappleExtended = false;
    public bool SwingExtended = false;
    private bool CanGrapple = false;
    private bool CanSwing = false;

    public float SwingCooldown;
    public float GrappleCooldown;
    public float MaxCooldown;
    public float ThrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GrappleJoint = GameObject.Find("GrappleJoint");
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
        if (movement.IsGrounded()) //Player must touch the ground before being allowed to Swing again.
        {
            CanSwing = true;
            CanGrapple = true;
        }
        if (SwingCooldown > 0)
        {
            SwingCooldown -= Time.deltaTime;
        }
        else
        {
            if (CanSwing == true&&SwingCooldown<= 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && isGrappling == false)
                {
                    isSwinging = true;
                    GetMousePosition();
                    GrappleJoint.GetComponent<ParentConstraint>().constraintActive = false;
                }
                else if (Input.GetKey(KeyCode.Mouse0) && isSwinging == true) //Right Click (Just Swing)
                {
                    Swing();
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0) && isSwinging == true)
                {
                    EndSwing();
                }
            }
        }



        if (GrappleCooldown > 0)
        {
            GrappleCooldown -= Time.deltaTime;
            
        }
        if (CanGrapple == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && GrappleCooldown <= 0) //This one will move the character towards the location (Left click)
            {
                if (isSwinging == false)
                {
                    isGrappling = true;
                    GetMousePosition();
                    GrappleJoint.GetComponent<ParentConstraint>().constraintActive = false;
                }

            }
            else if (Input.GetKey(KeyCode.Mouse1) && isGrappling == true)
            {
                Grapple();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1)&& isGrappling == true)
            {
                EndGrapple();
            }
        if (rb.velocity.x > MaxVelocity)
        {
            rb.velocity = new Vector2(MaxVelocity, rb.velocity.y);
        }else if (rb.velocity.x < -MaxVelocity)
        {
            rb.velocity = new Vector2(-MaxVelocity, rb.velocity.y);
        }
        else if (rb.velocity.y > MaxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, MaxVelocity);
        }else if (rb.velocity.y < -MaxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -MaxVelocity);
        }
    }
    void GetMousePosition()
        {
            mousePos = Input.mousePosition;
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    void Swing()
    {
        //Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        rb.drag = 0;
        if (SwingExtended == true)
        {
            SwingMadeIt();
        }
        else
        {
            GrappleJoint.transform.position = Vector2.LerpUnclamped(GrappleJoint.transform.position, mousePos,ThrowSpeed);
            StartCoroutine(SwingSetUp());
        }
    }
    void SwingMadeIt()
    {
        _lineRenderer.SetPosition(0, GrappleJoint.transform.position);
        _lineRenderer.SetPosition(1, transform.position);
        _distanceJoint.connectedAnchor = mousePos;
        _distanceJoint.enabled = true;
        _lineRenderer.enabled = true;
    }
    public void EndSwing(bool coolDownOverride = false, float ResetTime = 0)
    {
        rb.drag = 1;
        SwingCooldownReset(coolDownOverride, ResetTime);
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
        GrappleJoint.GetComponent<ParentConstraint>().constraintActive = true;
        GrappleJoint.transform.position = this.transform.position;
        SwingExtended = false;
        isSwinging = false;
        CanSwing = false;
    }
    void Grapple()
    {
        if(GrappleExtended == true)
        {
            GrappleMadeIt();
        }
        else
        {
            GrappleJoint.transform.position = Vector2.LerpUnclamped(GrappleJoint.transform.position, mousePos, ThrowSpeed);
            StartCoroutine(GrappleSetUp());
        }
    }
    void GrappleMadeIt()
    {
        rb.AddForce((mousePos - transform.position) * ThrowImpulse, ForceMode2D.Force);
        _lineRenderer.SetPosition(0, mousePos);
        _lineRenderer.SetPosition(1, transform.position);
        //_distanceJoint.connectedAnchor = mousePos;
        //_distanceJoint.enabled = true
        _lineRenderer.enabled = true;
    }
    public void EndGrapple(bool coolDownOverride = false, float ResetTime = 0)
    {
        GrappleCooldownReset(coolDownOverride, ResetTime);
        GetMousePosition();
        GrappleExtended = false;
        isGrappling = false;
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
        if (coolDownOverride == false)
            rb.AddForce((mousePos - transform.position) * ThrowImpulse, ForceMode2D.Impulse);
        GrappleJoint.GetComponent<ParentConstraint>().constraintActive = true;
        GrappleJoint.transform.position = this.transform.position;
        CanGrapple = false;
    }
    void SwingCooldownReset(bool cooldownOverride = false, float ResetTime = 0)
    {
        if (cooldownOverride == false)
            SwingCooldown = MaxCooldown;
        if (cooldownOverride == true)
            SwingCooldown = ResetTime;
    }
    void GrappleCooldownReset(bool cooldownOverride = false, float ResetTime = 0)
    {
        if (cooldownOverride == false)
            GrappleCooldown = MaxCooldown;
        if (cooldownOverride == true)
            GrappleCooldown = ResetTime;
    }
    private IEnumerator SwingSetUp()
    {
        yield return new WaitForSeconds(.8f-(ThrowSpeed*10));
        if(isSwinging == true)
            SwingExtended = true;
    }
    private IEnumerator GrappleSetUp()
    {
        yield return new WaitForSeconds(.8f - (ThrowSpeed * 10));
        if (isGrappling == true)
            GrappleExtended = true;
    }
}