using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class GridChanger : MonoBehaviour
{

    private Vector3 position = new Vector3(0,0,-10);
    private List<Transform> enemies = new List<Transform>();
    private List<Vector3> originalPosition = new List<Vector3>();
    public List<Transform> elementals = new List<Transform>();
    private List<Vector3> originalPositionElementals = new List<Vector3>();
    private int randEnemies;
    public List<GameObject> enemyPrefab;
    private float randomPosX, randomPosY;
    public int maxEnemies;
    public int minRange;
    private void Start()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
        if (minRange != 0)
        {
            randEnemies = Range(minRange, maxEnemies + 1);
            for (int x = 0; x < randEnemies; x++)
            {
                randomPosX = Range(-0.7f, 0.7f);
                randomPosY = Range(-0.3f, 0.3f);
                enemies.Add(Instantiate(enemyPrefab[Range(0, enemyPrefab.Count)], new Vector3(position.x + randomPosX, position.y + randomPosY, 0), Quaternion.identity).transform);
                originalPosition.Add(enemies[x].transform.position);
                enemies[x].GetComponent<EnemyBehaviour>().SetUp();
                enemies[x].gameObject.SetActive(false);
                PersistentManager.Instance.EnemiesRemaining++;
            }
        }
        if (elementals.Count != 0)
        {
            for (int i = 0; i < elementals.Count; i++)
            {
                originalPositionElementals.Add(elementals[i].transform.position);
                elementals[i].GetComponent<EnemyBehaviour>().SetUp();
                elementals[i].gameObject.SetActive(false);
                PersistentManager.Instance.EnemiesRemaining++;
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
                {
                    enemies[x].gameObject.SetActive(true);
                    enemies[x].GetComponent<EnemyBehaviour>().SetUp();
                    Ranger r = enemies[x].GetComponent<Ranger>();

                    if (r != null)
                    {
                        r.StartedP = false;
                        r.TimeTo = 0;
                    }
                }
            }

            if (elementals.Count != 0)
            {
                for (int i = 0; i < elementals.Count; i++)
                {
                    if (elementals[i].GetComponent<EnemyBehaviour>().isAlive)
                    {
                        elementals[i].gameObject.SetActive(true);
                        elementals[i].GetComponent<EnemyBehaviour>().SetUp();
                    }
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
