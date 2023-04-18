using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashAttack: MonoBehaviour
{
    float moveInput;
    RaycastHit2D hit;
    int layerMask = 1 << 11;
    private Vector3 trans;
    private Vector3 position;
    private Vector3 lastMovedir;
    private bool isTouching;
    private Vector3 actualDirection;
    private float dashDistance = 2f;
    private Vector3 playerDashMovement;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
       
    }

    private void OnDash()
    {
        Debug.Log("Entra");
        movementDash();
    }
    private void detectWalls()
    {
        layerMask = LayerMask.GetMask("CollisionWall");

        RaycastHit2D[] hits;
        Color rayColor;

        hits = Physics2D.RaycastAll(transform.position, actualDirection, 0.4f, layerMask);
        rayColor = Color.green;
        Debug.DrawRay(transform.position, actualDirection * 0.4f, rayColor);
        if (hits.Length > 0)
        {
            isTouching = true;
            rayColor = Color.red;
            Debug.DrawRay(transform.position, actualDirection * 0.4f, rayColor);
        }
        else
        {
            isTouching = false;
        }
    }
    private void movementDash()
    {
        position = PersistentManager.Instance.PlayerGlobal.transform.position;
        switch (PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().dir) //Check player facing direction to set arrow direction and position
        {
            case PlayerController.Direction.UP:                          //Set arrow facing up
                actualDirection = Vector3.up;
                detectWalls();
                break;
            case PlayerController.Direction.DOWN:
                actualDirection = Vector3.down;
                detectWalls();
                break;
            case PlayerController.Direction.LEFT:
                actualDirection = Vector3.left;
                detectWalls();
                rb.position = new Vector3(transform.position.x + dashDistance, -1f);
                break;
            case PlayerController.Direction.RIGHT:
                actualDirection = Vector3.right;
                detectWalls();
                rb.position = new Vector3(transform.position.x , dashDistance, 0f);
                break;

        }

    }
    /*
         for (int i = 0; i < hits.Length; i++) //Mirar dentro de los hits
         {
            RaycastHit2D hit = hits[i];
         }
        */



}
