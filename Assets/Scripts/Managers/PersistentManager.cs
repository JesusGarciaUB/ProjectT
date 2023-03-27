using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
   
    public static PersistentManager Instance { get; private set; }
    public GameObject PlayerGlobal;
    public Healthbar hp;
    public GameObject winlose;
    public AbilityUI ability;

    //Global variables
    public string spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        PlayerGlobal = GameObject.FindGameObjectWithTag("Player");
    }

    public Color GetOgColor
    {
        get { return new Color(255, 255, 255, 255); }
    }
}
