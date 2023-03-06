using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : ElementalShoot
{

    private Rigidbody2D rb2; //rb
    PlayerController player;
    Transform playerPos;
    public int DurationOfSpell;

    // Start is called before the first frame update
    private void Start()
    {
        player = PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>();
        playerPos = PersistentManager.Instance.PlayerGlobal.GetComponent<Transform>();
        rb2 = GetComponent<Rigidbody2D>();
        Vector3 dir = SetDirection();
        rb2.velocity = new Vector2 (dir.x, dir.y).normalized * speed;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private Vector3 SetDirection()
    {
        Vector3 to_return = playerPos.position;
        switch(player.dir)
        {
            case PlayerController.Direction.UP:
                to_return = playerPos.transform.up;
                break;
            case PlayerController.Direction.DOWN:
                to_return = playerPos.transform.up * -1;
                break;
            case PlayerController.Direction.LEFT:
                to_return = playerPos.transform.right * -1;
                break;
            case PlayerController.Direction.RIGHT:
                to_return = playerPos.transform.right;
                break;
        }
        return to_return;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyBehaviour eb = collision.GetComponent<EnemyBehaviour>();

            switch(player.MagicSetter)
            {
                case PlayerController.MAGICe.FIRE:
                    eb.Burn(damage, DurationOfSpell);
                    break;
                case PlayerController.MAGICe.ICE:
                    eb.Freeze(DurationOfSpell);
                    break;
                case PlayerController.MAGICe.PLANT:
                    player.PlantAttack(damage);
                    eb.Stolen(damage);
                    break;
            }
            Destroy(gameObject);
        } else
        {
            Destroy(gameObject, timeOnScreen);
        }
    }
}
