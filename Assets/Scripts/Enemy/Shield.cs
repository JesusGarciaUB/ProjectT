using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Follower
{
    // Update is called once per frame
    private void Start()
    {
        Armor = 2;

        base.Start();
    }
}
