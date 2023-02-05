using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject Player;
    private void Start()
    {
        Player.transform.position = getSpawnPoint(); 
    }

    public Vector3 getSpawnPoint()
    {
        if (PersistentManager.Instance.spawnPoint == "top") return GameObject.FindWithTag("SpawnBot").transform.position;
        if (PersistentManager.Instance.spawnPoint == "bot") return GameObject.FindWithTag("SpawnTop").transform.position;
        if (PersistentManager.Instance.spawnPoint == "left") return GameObject.FindWithTag("SpawnRight").transform.position;
        if (PersistentManager.Instance.spawnPoint == "right") return GameObject.FindWithTag("SpawnLeft").transform.position;
        return new Vector3(0,0,0);
    }
}
