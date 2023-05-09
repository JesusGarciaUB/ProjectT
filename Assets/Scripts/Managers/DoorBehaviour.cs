using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D bc;
    [SerializeField] private GameObject openDoorSound;
    [SerializeField] private float TimeToDestroy;
    private bool state;
    // Start is called before the first frame update
    void Start()
    {
        state = false;
        int layer = Mathf.FloorToInt(transform.position.y * 100);
        GetComponent<SpriteRenderer>().sortingOrder = -layer + 15;
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    public void Interact() {
        state = true;
        animator.SetTrigger("open");
        bc.enabled = !bc.enabled;
        GameObject sound = Instantiate(openDoorSound);
        Destroy(sound, TimeToDestroy);
    }

    private void TriggerNextState()
    {
        animator.SetTrigger("open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().hasKey && !state) Interact();
        }
    }
}
