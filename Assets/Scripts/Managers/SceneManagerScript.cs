using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public int enemiesLeft;
    public GameObject Interaction;
    private void Update()
    {
        if (enemiesLeft == 0)
        {
            Destroy(Interaction);
            Destroy(gameObject);
        }
    }
}
