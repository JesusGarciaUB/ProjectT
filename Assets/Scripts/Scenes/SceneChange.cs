using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public int sceneBuildIndex;
    public Vector3 nextSpawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PersistentManager.Instance.nextSpawn = nextSpawn;
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
        
    }
}
