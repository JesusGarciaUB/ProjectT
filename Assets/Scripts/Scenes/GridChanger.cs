using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{

    private Vector3 position;
    public int speed = 2;
    public enum DIRECTION_ENUM { TOP, BOT, LEFT, RIGHT};
    public DIRECTION_ENUM direction;
    public int offset;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") collision.transform.position = Vector3.Lerp(collision.transform.position, getPosition(), speed * Time.deltaTime);
    }

    Vector3 getPosition()
    {
        switch(direction)
        {
            case DIRECTION_ENUM.TOP:
                break;
            case DIRECTION_ENUM.BOT:
                break;
            case DIRECTION_ENUM.LEFT:
                break;
            case DIRECTION_ENUM.RIGHT:
                break;
        }
        return position;
    }
}
