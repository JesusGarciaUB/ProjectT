using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menustart : MonoBehaviour
{
    public int scenenum;
    public void LoadScene()
    {
        SceneManager.LoadScene(scenenum);
    }


}
