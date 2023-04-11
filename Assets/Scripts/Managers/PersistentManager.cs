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
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    public InteractionScreen ins;
    public PlayerController.MAGICe magic = PlayerController.MAGICe.NOONE;
    public Vector3 nextSpawn;
    public List<GameObject> bloodType = new List<GameObject>();
    public GameObject bloodElemental;

    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    //Global variables
    public string spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentHealth = maxHealth;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        hp.SetMaxHealth(maxHealth);
    }

    public Color GetOgColor
    {
        get { return new Color(255, 255, 255, 255); }
    }
}
