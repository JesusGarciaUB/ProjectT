using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class GridChanger : MonoBehaviour
{

    public Vector3 position = new Vector3(0,0,-10);
    private List<Transform> enemies = new List<Transform>();
    private List<Vector3> originalPosition = new List<Vector3>();
    public List<Transform> elementals = new List<Transform>();
    private List<Vector3> originalPositionElementals = new List<Vector3>();
    private int randEnemies;
    public List<GameObject> enemyPrefab;
    private float randomPosX, randomPosY;
    private void Start()
    {
        randEnemies = Range(1, 5);
        for (int x = 0; x < randEnemies; x++)
        {
            randomPosX = Range(-0.5f, 0.5f);
            randomPosY = Range(-0.5f, 0.5f);
            enemies.Add(Instantiate(enemyPrefab[Range(0, 3)], new Vector3(position.x + randomPosX, position.y + randomPosY, 0), Quaternion.identity).transform);
            originalPosition.Add(enemies[x].transform.position);
            enemies[x].gameObject.SetActive(false);
        }
        if (elementals.Count != 0)
        {
            for (int i = 0; i < elementals.Count; i++)
            {
                originalPositionElementals.Add(elementals[i].transform.position);
                elementals[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { 
            GameObject.FindGameObjectWithTag("Follower").transform.position = position;

            for (int x = 0; x < enemies.Count; x++)
            {
                if (enemies[x].GetComponent<EnemyBehaviour>().isAlive)
                    enemies[x].gameObject.SetActive(true);
            }

            if (elementals.Count != 0)
            {
                for (int i = 0; i < elementals.Count; i++)
                {
                    if (elementals[i].GetComponent<EnemyBehaviour>().isAlive)
                        elementals[i].gameObject.SetActive(true);
                }
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

            if (elementals.Count != 0)
            {
                for (int i = 0; i < elementals.Count; i++)
                {
                    elementals[i].transform.position = originalPositionElementals[i];
                    elementals[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
