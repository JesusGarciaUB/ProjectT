using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr.color == Color.cyan) PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().MagicSetter = PlayerController.MAGICe.ICE;
            if (sr.color == Color.red) PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().MagicSetter = PlayerController.MAGICe.FIRE;
            if (sr.color == Color.green) PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().MagicSetter = PlayerController.MAGICe.PLANT;
            Destroy(gameObject);
        }
    }
}
