using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    private bool state;
    private Animator animator;
    public DoorBehaviour db;

    private void Start()
    {
        int layer = Mathf.FloorToInt(transform.position.y * 10);
        GetComponentInParent<SpriteRenderer>().sortingOrder = -layer;
        state = false;
        animator = GetComponentInParent<Animator>();
    }

    public void Interact() { state = !state; animator.SetBool("state", state); db.Interact(); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().PalancaSetter = true;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().SetP = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().PalancaSetter = false;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().SetP = null;
        }
    }
}
