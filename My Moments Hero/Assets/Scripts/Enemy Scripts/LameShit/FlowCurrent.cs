using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowCurrent : MonoBehaviour
{
    [SerializeField] protected Transform FlowDirection;
    [SerializeField] protected float ForceModifier;
    // Start is called before the first frame update
    void Start()
    {
        //FlowDirection = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D Collision)
    {
        Vector2 ToFlowDirection = new Vector2((FlowDirection.position.x - Collision.transform.position.x)*ForceModifier, (FlowDirection.position.y -Collision.transform.position.y)*ForceModifier);
        Collision.GetComponent<Rigidbody2D>().AddForce(ToFlowDirection);
        //Debug.Log("Force Should Be Occuring Now");
    }
}
