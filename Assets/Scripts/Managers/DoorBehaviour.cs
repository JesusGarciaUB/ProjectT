using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool state;
    private Animator animator;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        int layer = Mathf.FloorToInt(transform.position.y * 10);
        GetComponent<SpriteRenderer>().sortingOrder = -layer;
        state = false;
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    public void Interact() { 
        state = !state; 
        animator.SetBool("state", state);
        bc.enabled = !bc.enabled;
    }
}
