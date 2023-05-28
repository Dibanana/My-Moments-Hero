using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFunction : MonoBehaviour
{
    Vector3 mousePosition;
    public float moveSpeed = .1f;
    public float ThrowImpulse;
    public Rigidbody2D rb;
    Vector2 position;
    private CamSwitch camSwitch;

    private bool IsDragging = false;

    private void Start()
    {
        camSwitch = GameObject.Find("CamSwitch").GetComponent<CamSwitch>();
        rb = GetComponent<Rigidbody2D>();
        position = Vector2.Lerp(transform.position, transform.position, moveSpeed);

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
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
    }
    private void OnMouseDrag()
    {
        IsDragging = true;
        GetMousePosition();
    }
    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition = camSwitch.currentCamera.ScreenToWorldPoint(mousePosition);        
    }
    private void OnMouseUp()
    {
        IsDragging = false;
        rb.velocity = new Vector2(0,0);
        rb.AddForce((mousePosition - transform.position)*ThrowImpulse, ForceMode2D.Impulse);
    }
}
