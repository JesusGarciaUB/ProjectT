using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum Direction { 
        NONE, UP, DOWN, LEFT, RIGHT
    };
    public Direction dir = Direction.NONE;
    public float speed = 1f;
    public float collOffset = 0.02f;
    public ContactFilter2D cF;
    public SwordAttack swordAttack;
    Vector2 playerMovement;
    Animator animator;
    Rigidbody2D rb;
    List<RaycastHit2D> cColl = new List<RaycastHit2D>();
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        dir = Direction.DOWN;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetLastPosition();
        if (canMove)
        {
            
            if (playerMovement != Vector2.zero)
            {
                animator.SetFloat("movementX", playerMovement.x);
                animator.SetFloat("movementY", playerMovement.y); 
                animator.SetBool("isMoving", true);
                bool success = TryMove(playerMovement);

                if (!success)
                {
                    success = TryMove(new Vector2(playerMovement.x, 0));

                    if (!success) success = TryMove(new Vector2(0, playerMovement.y));
                }

            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
               direction,
               cF,
               cColl,
               speed * Time.deltaTime + collOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        } else return false;
    }

    void OnMove(InputValue movement)
    {
        playerMovement = movement.Get<Vector2>();
    }

    void SetLastPosition()
    {
        //Facing up
        if (playerMovement.y > 0)
        {
            dir = Direction.UP;
            animator.SetFloat("positionY", 1);
            animator.SetFloat("positionX", 0);
        }
        //Facing right
        if (playerMovement.x > 0)
        {
            dir = Direction.RIGHT;
            animator.SetFloat("positionX", 1);
            animator.SetFloat("positionY", 0);
        }
        //Facing left
        if (playerMovement.x < 0)
        {
            dir = Direction.LEFT;
            animator.SetFloat("positionX", -1);
            animator.SetFloat("positionY", 0);
        }
        //Facing down
        if (playerMovement.y < 0)
        {
            dir = Direction.DOWN;
            animator.SetFloat("positionY", -1);
            animator.SetFloat("positionX", 0);
        }


    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();

        switch (dir)
        {
            case Direction.UP:
                print("Attack up");
                swordAttack.AttackUp();
                break;
            case Direction.DOWN:
                print("Attack down");
                swordAttack.AttackDown();
                break;
            case Direction.LEFT:
                print("Attack left");
                swordAttack.AttackLeft();
                break;
            case Direction.RIGHT:
                print("Attack right");
                swordAttack.AttackRight();
                break;
            default:
                break;
        }

        swordAttack.StopAttack();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
