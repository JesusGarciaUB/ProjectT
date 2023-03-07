using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string TextToShow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().InteractingSetter = true;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().Ins.TextRaw = TextToShow;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().InteractingSetter = false;
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().Ins.State = 2;
        }
    }
}
