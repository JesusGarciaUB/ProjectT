using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float collOffset = 0.05f;
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
            int count = rb.Cast(
                playerMovement,
                cF,
                cColl,
                speed * Time.deltaTime * collOffset);
        }
    }

    void OnMove(InputValue movement)
    {
        playerMovement = movement.Get<Vector2>();
    }
}
