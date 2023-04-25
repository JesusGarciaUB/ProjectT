using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    private Color newColor = new Color(1, 1, 1, 0.2f);
    private Color ogColor = new Color(1, 1, 1, 1);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            GetComponent<SpriteRenderer>().color = newColor;
            print("hola");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("adeu");
            GetComponent<SpriteRenderer>().color = ogColor;
        }
    }
}
