using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{

    public Vector3 position = new Vector3(0,0,-10);
    private List<Transform> enemies = new List<Transform>();
    private List<Vector3> originalPosition = new List<Vector3>();

    private void Start()
    {
        foreach (Transform enemy in transform)
        {
            if (enemy.tag == "Enemy")
            {
                enemies.Add(enemy);
                originalPosition.Add(enemy.transform.position);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { 
            GameObject.FindGameObjectWithTag("Follower").transform.position = position;

            for (int x = 0; x < enemies.Count; x++)
            {
                enemies[x].gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int x = 0; x < enemies.Count; x++)
            {
                enemies[x].transform.position = originalPosition[x];
                enemies[x].gameObject.SetActive(false);
            }
        }
    }
}
