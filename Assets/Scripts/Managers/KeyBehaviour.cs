using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject pickUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().hasKey = true;
            GameObject sound = Instantiate(pickUp);
            Destroy(sound, 3f);
            Destroy(gameObject);
        }
    }
}
