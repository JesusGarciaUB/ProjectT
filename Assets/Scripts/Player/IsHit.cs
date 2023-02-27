using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHit : MonoBehaviour
{
    public bool isHit;
    public float HitCooldown;
    public int Multiplier;
    public float ticks;
    private Color OriginalColor;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        OriginalColor = GetComponentInParent<SpriteRenderer>().color;
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
        for (int x = 0; x < HitCooldown * Multiplier; x++)
        {
            pl.color = Color.magenta;
            yield return new WaitForSeconds(ticks);
            pl.color = OriginalColor;
            yield return new WaitForSeconds(ticks);
        }
        pl.color = OriginalColor;
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
