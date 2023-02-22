using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHitEnemy : MonoBehaviour
{
    private bool isHit;
    private float HitCooldown;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        HitCooldown = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HitEnemy
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
