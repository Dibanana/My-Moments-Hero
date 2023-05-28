using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeObject : MonoBehaviour
{
    [SerializeField] private float GizmosX;
    [SerializeField] private float GizmosY;
    [SerializeField] private Color colorName;
    private void OnDrawGizmos()
    {
        Gizmos.color = colorName;
        Gizmos.DrawWireCube(transform.position, new Vector2 (GizmosX,GizmosY));
    }
}
