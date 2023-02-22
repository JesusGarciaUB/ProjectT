using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHit : MonoBehaviour
{
    public bool isHit;
    private int HitCooldown;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        HitCooldown = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Hit
    {
        get { return isHit; }
        set { isHit = value; }
    }

    private IEnumerator colorChange()
    {
        SpriteRenderer pl = GetComponentInParent<SpriteRenderer>();
        for (int x = 0; x < HitCooldown * 5; x++)
        {
            pl.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            pl.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        pl.color = Color.white;
        print("over");
    }
    public void Hitted()
    {
        StartCoroutine(HitCooldownV());
        StartCoroutine(colorChange());
    }

    public IEnumerator HitCooldownV()
    {
        isHit = true;
        yield return new WaitForSeconds(HitCooldown);
        isHit = false;
    }
}
