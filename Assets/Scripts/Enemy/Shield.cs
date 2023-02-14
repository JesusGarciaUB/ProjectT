using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Follower
{
    private Vector3 direction;
    float angle;
    // Update is called once per frame
    void FixedUpdate()
    {
        direction = transform.position - target.position;
        direction.Normalize();


        print(angle);

        base.FixedUpdate();
    }
}
