using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    public Animator animator;
    public string TextToShow;
    private bool Interacting;

    private void Start()
    {
        SetLayer();
        Interacting = false;
        ResetDirection();
    }
    private void FixedUpdate()
    {
        if (Interacting) SetDirection();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Interacting = true;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().InteractingSetter = true;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().Ins.TextRaw = TextToShow;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Interacting = false;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().InteractingSetter = false;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().Ins.State = 2;
            ResetDirection();
        }
    }

    private void ResetDirection()
    {
        animator.SetFloat("movX", 0);
        animator.SetFloat("movY", -1);
    }

    private void SetDirection()
    {
        Vector3 dir = PersistentManager.Instance.PlayerGlobal.transform.position - transform.position;
        bool auxMoveX = false;
        bool auxMoveY = false;
        float y = dir.y;
        float x = dir.x;

        if (Mathf.Sign(x) == -1)
        {
            x = -x;
            auxMoveX = true;
        }
        if (Mathf.Sign(y) == -1)
        {
            y = -y;
            auxMoveY = true;
        }
        if (x >= y) y = 0;
        else x = 0;

        dir.x = auxMoveX ? -x : x;
        dir.y = auxMoveY ? -y : y; ;
        dir = dir.normalized;
        animator.SetFloat("movX", dir.x);
        animator.SetFloat("movY", dir.y);
    }

    private void SetLayer()
    {
        int layer = Mathf.FloorToInt((transform.position.y - 0.11f) * 100);
        GetComponent<SpriteRenderer>().sortingOrder = -layer;
    }
}
