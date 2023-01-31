using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float collOffset = 0.02f;
    public ContactFilter2D cF;
    Vector2 playerMovement;
    Rigidbody2D rb;
    List<RaycastHit2D> cColl = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerMovement != Vector2.zero)
        {
           bool success = TryMove(playerMovement);

            if (!success)
            {
                success = TryMove(new Vector2(playerMovement.x, 0));

                if (!success) success = TryMove(new Vector2(0, playerMovement.y));
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
}
