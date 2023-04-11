using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashAttack: MonoBehaviour
{
    float moveInput;
    RaycastHit2D hit;
 


    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
        if(hit.collider != null) {
            Debug.Log("Estas tocando un objeto a la derecha");
        }
        Debug.DrawRay(transform.position, Vector3.right * 0.4f, Color.green);

        Debug.DrawRay(transform.position, Vector3.up * 0.4f, Color.green);

        Debug.DrawRay(transform.position, Vector3.down * 0.4f, Color.green);

        Debug.DrawRay(transform.position, Vector3.left * 0.4f, Color.green);

        //RaycastHit2D lookingUp = Physics2D.Raycast(transform.position, Vector2.up * 3);
        //Debug.DrawRay(transform.position, Vector2.up * lookingUp.distance, Color.green);
    }
}
