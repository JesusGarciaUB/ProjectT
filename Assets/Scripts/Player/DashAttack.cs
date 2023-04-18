using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashAttack: MonoBehaviour
{
    float moveInput;
    RaycastHit2D hit;
    //public LayerMask layerMask;
    int layerMask = 1 << 11;

    
    private void Start()
    {
        Debug.DrawRay(transform.position, Vector3.right * 0.4f, Color.green);

        Debug.DrawRay(transform.position, Vector3.up * 0.4f, Color.green);

        Debug.DrawRay(transform.position, Vector3.down * 0.4f, Color.green);

        Debug.DrawRay(transform.position, Vector3.left * 0.4f, Color.green);
    }

  
    void Update()
    {
        layerMask = LayerMask.GetMask("ObjectCollision");

        RaycastHit2D[] hits;
        Color rayColor;

        hits = Physics2D.RaycastAll(transform.position, Vector3.right, 0.4f, layerMask);
        rayColor = Color.green;
        if(hits.Length > 0)
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(transform.position, Vector3.right * 0.4f, rayColor);

        hits = Physics2D.RaycastAll(transform.position, Vector3.up, 0.4f, layerMask);
        rayColor = Color.green;
        if (hits.Length > 0)
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(transform.position, Vector3.up * 0.4f, rayColor);

        hits = Physics2D.RaycastAll(transform.position, Vector3.down, 0.4f, layerMask);
        rayColor = Color.green;
        if (hits.Length > 0)
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(transform.position, Vector3.down * 0.4f, rayColor);

        hits = Physics2D.RaycastAll(transform.position, Vector3.left, 0.4f, layerMask);
        rayColor = Color.green;
        if (hits.Length > 0)
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(transform.position, Vector3.left * 0.4f, rayColor);

        /*
         for (int i = 0; i < hits.Length; i++) //Mirar dentro de los hits
         {
            RaycastHit2D hit = hits[i];
         }
        */
    }
}
