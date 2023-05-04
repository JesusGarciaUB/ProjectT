using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHit : MonoBehaviour
{
    public bool isHit;
    public float HitCooldown;
    public int Multiplier;
    public float ticks;
    void Awake()
    {
        isHit = false;
    }

    public bool Hit
    {
        get { return isHit; }
        set { isHit = value; }
    }
    public void Hitted()
    {
        StartCoroutine(HitCooldownV());
    }

    public IEnumerator HitCooldownV()
    {
        isHit = true;
        yield return new WaitForSeconds(HitCooldown);
        isHit = false;
    }
}
