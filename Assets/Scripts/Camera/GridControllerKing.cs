using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControllerKing : MonoBehaviour
{
    public Vector3 position = new Vector3(0, 0, -10);
    public Transform king;

    private void Start()
    {
        king.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Follower").transform.position = position;
            king.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            king.gameObject.SetActive(false);
        }
    }
}
