using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LootController : MonoBehaviour
{
    [SerializeField] private GameObject pickup;
    [SerializeField] private GameObject drop;

    private void Awake()
    {
        GameObject sound = Instantiate(drop);
        Destroy(sound, 2.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            GameObject sound = Instantiate(pickup);
            Destroy(sound, 2.0f);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr.color == Color.cyan) PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().MagicSetter = PlayerController.MAGICe.ICE;
            if (sr.color == Color.red) PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().MagicSetter = PlayerController.MAGICe.FIRE;
            if (sr.color == Color.green) PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().MagicSetter = PlayerController.MAGICe.PLANT;
            Destroy(gameObject);
        }
    }
}
