using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastInteractable : Interactable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().InteractingSetter = true;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().Ins.TextRaw = TextToShow;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().OnInteract();
        }
    }
}
