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
    private float dashDistance = 19f;
    private Vector3 playerDashMovement;
    private float cooldownTime = 2f;
    private float nextfireTeam = 0f;
    [SerializeField] Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        Debug.DrawRay(transform.position, actualDirection * 0.5f, rayColor);
        if (hits.Length > 0)
        {
            isTouching = true;
            rayColor = Color.red;
            Debug.DrawRay(transform.position, actualDirection * 0.5f, rayColor);
            
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
                if (isTouching != true)
                {
                    if (Time.time > nextfireTeam)
                    {
                        nextfireTeam = Time.time + cooldownTime;
                        animator.SetTrigger("dashAttack");
                        rb.position = new Vector3(transform.position.x, transform.position.y - 0f + dashDistance * Time.fixedDeltaTime);
                    }
                }
                break;
            case PlayerController.Direction.DOWN:
                actualDirection = Vector3.down;
                detectWalls();
                if (isTouching != true)
                {
                    if (Time.time > nextfireTeam)
                    {
                        nextfireTeam = Time.time + cooldownTime;
                        animator.SetTrigger("dashAttack");
                        rb.position = new Vector3(transform.position.x, transform.position.y - 1f + dashDistance * Time.fixedDeltaTime);
                    }
                }
                break;
            case PlayerController.Direction.LEFT:
                actualDirection = Vector3.left;
                detectWalls();
                if (isTouching != true)
                {
                    if (Time.time > nextfireTeam)
                    {
                        nextfireTeam = Time.time + cooldownTime;
                        animator.SetTrigger("dashAttack");
                        rb.position = new Vector3(transform.position.x - 1f + dashDistance * Time.fixedDeltaTime, transform.position.y - 0f);
                    }
                }
                break;
            case PlayerController.Direction.RIGHT:
                actualDirection = Vector3.right;
                detectWalls();
                if (isTouching != true)
                {
                    if (Time.time > nextfireTeam)
                    {
                        nextfireTeam = Time.time + cooldownTime;
                        animator.SetTrigger("dashAttack");
                        rb.position = new Vector3(transform.position.x + dashDistance * Time.fixedDeltaTime, transform.position.y - 0f);
                    }
                }
                break;

        }

    }
}
