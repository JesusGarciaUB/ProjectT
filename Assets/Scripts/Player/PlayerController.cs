using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT }; 
    public enum WEAPON{ SWORD, BOW };
    public enum MAGICe { NOONE, FIRE, ICE, PLANT};
    private MAGICe currentMagic = MAGICe.NOONE;
    public Direction dir = Direction.NONE;                  //Used to save player last direction in dir
    public WEAPON weapon;                                   //Last weapon used
    public float speed = 1f;                                //Player speed
    public float collOffset = 0.02f;
    public ContactFilter2D cF;
    public SwordAttack swordAttack;
    public BowAttack bowAttack;
    Vector2 playerMovement;
    Animator animator;
    Rigidbody2D rb;
    public List<RaycastHit2D> cColl = new List<RaycastHit2D>();
    bool canMove = true;                                    //On true player can move
    public GameObject healthText;                           //Displays damage on player
    int health;
    private int maxHealth;
    public List<GameObject> spells;
    private bool healed = false;
    public int MagicCooldown;
    private bool CanMagic;
    public InteractionScreen Ins;
    private bool Interacting;
    private bool InteractingPalanca;
    private Palanca palanca;
    public GameObject swordSound;
    public GameObject _bowSound;
    public bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        SetLayer();
        Interacting = false;
        InteractingPalanca = false;
        CanMagic = true;
        PersistentManager.Instance.PlayerGlobal = gameObject;
        maxHealth = PersistentManager.Instance.MaxHealth;
        health = PersistentManager.Instance.CurrentHealth;
        dir = Direction.DOWN;                               //By default player face down
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Ins = PersistentManager.Instance.ins;
        currentMagic = PersistentManager.Instance.magic;
        transform.position = PersistentManager.Instance.nextSpawn;
        attacking = false;
        hasKey = false;
    }

    public Palanca SetP { set { palanca = value; } }
    public bool PalancaSetter { set { InteractingPalanca = value; } }
    public bool InteractingSetter
    {
        set { Interacting = value; }
    }
    public MAGICe MagicSetter
    {
        get { return currentMagic; }
        set { 
            currentMagic = value;
            PersistentManager.Instance.magic = currentMagic;
        }
    }

    public int FirstHealth { set { health = value; } }
    public int Health
    {
        get { return health; }
        set 
        {
            if (value < health || healed)
            {
                //Set health loss text position on top of the enemy
                Vector3 v3 = transform.position;
                v3.y += 0.16f;
                GameObject gm = Instantiate(healthText, v3, Quaternion.identity);   //moved from -=3 lines
                RectTransform textTransform = gm.GetComponent<RectTransform>();     //
                //textTransform.transform.position = v3;                            //set in instantiate

                //Add damage dealet
                TextMeshProUGUI textMesh = gm.GetComponent<TextMeshProUGUI>();
                int damage = health - value;
                //if (!healed) textMesh.SetText(damage.ToString());
                //else textMesh.SetText(damage.ToString()[1].ToString());
                if (!healed) textMesh.text = damage.ToString();
                else textMesh.text = damage.ToString()[1].ToString();
                if (healed) textMesh.color = Color.green;

                //Set health loss text inside the canvas
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);

                //Set screen shake on hp loss
                if (!healed)
                {
                    GameObject screenShake = GameObject.FindWithTag("MainCamera");
                    screenShake.GetComponent<ScreenShake>().timeShake = 0.15f;

                }

                PersistentManager.Instance.hp.SetHealth(damage);
            }

            health = value; 
            if (health <= 0) Defeated();
            PersistentManager.Instance.CurrentHealth = health;
        } 
    }
    private void Defeated()
    {
        //Time.timeScale = 0f;
        GameObject screenShake = GameObject.FindWithTag("MainCamera");
        screenShake.GetComponent<ScreenShake>().timeShake = 0.0f;
        PersistentManager.Instance.winlose.SetActive(true);
        animator.SetTrigger("isDeath");
        StartCoroutine(EndScene());

        /*Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        Destroy(GameObject.FindGameObjectWithTag("PersistentManager"));
        Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
        SceneManager.LoadScene(3);*/
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        Destroy(GameObject.FindGameObjectWithTag("PersistentManager"));
        Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
        SceneManager.LoadScene(3);
    }

    private void Update()
    {
        SetLayer();
        SetLastPosition();
        if (canMove && health > 0)
        {
            
            if (playerMovement != Vector2.zero)
            {
                animator.SetFloat("movementX", playerMovement.x);
                animator.SetFloat("movementY", playerMovement.y); 
                animator.SetBool("isMoving", true);
                bool success = TryMove(playerMovement);

                if (!success)
                {
                    success = TryMove(new Vector2(playerMovement.x, 0));

                    if (!success) success = TryMove(new Vector2(0, playerMovement.y));
                }

            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
               direction,
               cF,
               cColl,
               speed * Time.deltaTime + collOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        } else return false;
    }

    void OnMove(InputValue movement)
    {
        playerMovement = movement.Get<Vector2>();
    }

    //Save player facing last position
    void SetLastPosition()
    {
        if (canMove)
        {
            //Facing up
            if (playerMovement.y > 0)
            {
                dir = Direction.UP;
                animator.SetFloat("positionY", 1);
                animator.SetFloat("positionX", 0);
            }
            //Facing right
            if (playerMovement.x > 0)
            {
                dir = Direction.RIGHT;
                animator.SetFloat("positionX", 1);
                animator.SetFloat("positionY", 0);
            }
            //Facing left
            if (playerMovement.x < 0)
            {
                dir = Direction.LEFT;
                animator.SetFloat("positionX", -1);
                animator.SetFloat("positionY", 0);
            }
            //Facing down
            if (playerMovement.y < 0)
            {
                dir = Direction.DOWN;
                animator.SetFloat("positionY", -1);
                animator.SetFloat("positionX", 0);
            }
        }

    }

    private bool attacking; 
    void OnSword()
    {
        if (!attacking) animator.SetTrigger("swordAttack");
    }

    void OnBow()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("bowAttack");
        }
    }
   


    public void SwordAttack()
    {
        attacking = true;
        GameObject sound = Instantiate(swordSound);
        Destroy(sound, 1f);
        LockMovement();

        switch (dir)
        {
            case Direction.UP:
                swordAttack.AttackUp();
                break;
            case Direction.DOWN:
                swordAttack.AttackDown();
                break;
            case Direction.LEFT:
                swordAttack.AttackLeft();
                break;
            case Direction.RIGHT:
                swordAttack.AttackRight();
                break;
            default:
                break;
        }
    }

    public void BowAttack()
    {
        bowAttack.Attack();
    }

    public void EndBowAttack()
    {
        attacking = false;
    }
    public void SetSoundBow()
    {
        GameObject sound = Instantiate(_bowSound);
        Destroy(sound, 2f);
    }

    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
        UnlockMovement();
        attacking = false;
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void PlantAttack(int damage)
    {
        healed = true;
        if (Health < maxHealth) Health += damage;
        healed = false;
    }

    public void OnMagicAttack()
    {
        if (currentMagic != MAGICe.NOONE)
        {
            if (CanMagic)
            {
                animator.SetTrigger("magicAttack");
                StartCoroutine(MagicCD());
            }
        }
    }

    public List<GameObject> soundMagic;
    public void MagicAttack()
    {
        Instantiate(spells[(int)currentMagic - 1], transform.position, Quaternion.identity);
        GameObject mySound = Instantiate(soundMagic[(int)currentMagic - 1]);
        Destroy(mySound, 2f);
        PersistentManager.Instance.ability.usedAbility(MagicCooldown);
    }

    private IEnumerator MagicCD()
    {
        CanMagic = false;
        yield return new WaitForSeconds(MagicCooldown);
        CanMagic = true;
    }

    public void OnInteract()
    {
        if (Interacting)
        {
            Ins.Interacted();
        }
        if (InteractingPalanca)
        {
            palanca.Interact();
        }
    }

    protected void SetLayer()
    {
        int layer = Mathf.FloorToInt((transform.position.y - 0.11f) * 100);
        GetComponent<SpriteRenderer>().sortingOrder = -layer;
    }
    /*private void OnDash()
     {
         GetComponent<DashAttack>().DashMovement();
         animator.SetTrigger("dashAttack");
     }
    */

    void OnScene4()
    {
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        Destroy(GameObject.FindGameObjectWithTag("PersistentManager"));
        Destroy(GameObject.FindGameObjectWithTag("EventSystem"));

        PersistentManager.Instance.nextSpawn = new Vector3(0, -0.18f,0);
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    void OnScene5()
    {
        PersistentManager.Instance.nextSpawn = new Vector3(0f, -0.75f, 0);
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }

    void OnScene6()
    {
        PersistentManager.Instance.nextSpawn = new Vector3(-0.896f, 0.649f, 0);
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }

    void OnScene7()
    {
        PersistentManager.Instance.nextSpawn = new Vector3(0.24f, -0.6f, 0);
        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }

    void OnScene8()
    {
        PersistentManager.Instance.nextSpawn = new Vector3(0.1f, -0.793f, 0);
        SceneManager.LoadScene(8, LoadSceneMode.Single);
    }

    void OnScene9()
    {
        PersistentManager.Instance.nextSpawn = new Vector3(1.115f, 0.372f, 0);
        SceneManager.LoadScene(9, LoadSceneMode.Single);
    }
}
