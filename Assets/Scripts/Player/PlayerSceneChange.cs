using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DoorTop") PersistentManager.Instance.spawnPoint = "top";
        if (collision.tag == "DoorBot") PersistentManager.Instance.spawnPoint = "bot";
        if (collision.tag == "DoorLeft") PersistentManager.Instance.spawnPoint = "left";
        if (collision.tag == "DoorRight") PersistentManager.Instance.spawnPoint = "right";
    }
}
