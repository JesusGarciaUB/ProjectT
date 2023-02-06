using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{

    private Vector3 position = new Vector3(0,0,0);
    public enum DIRECTION_ENUM { TOP, BOT, LEFT, RIGHT};
    public DIRECTION_ENUM direction;
    public int offset;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") GameObject.FindGameObjectWithTag("Follower").transform.position = getPosition();
    }

    Vector3 getPosition()
    {
        switch(direction)
        {
            case DIRECTION_ENUM.TOP:
                position.y += offset;
                break;
            case DIRECTION_ENUM.BOT:
                position.y -= offset;
                break;
            case DIRECTION_ENUM.LEFT:
                position.x -= offset;
                break;
            case DIRECTION_ENUM.RIGHT:
                position.x += offset;
                break;
        }
        return position;
    }
}
