using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{

    public Vector3 position = new Vector3(0,0,-10);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") GameObject.FindGameObjectWithTag("Follower").transform.position = position;
    }
}
