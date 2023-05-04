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
    private SpriteRenderer pl;
    // Start is called before the first frame update
    void Awake()
    {
        pl = GetComponentInParent<SpriteRenderer>();
        isHit = false;
        OriginalColor = PersistentManager.Instance.GetOgColor;
    }

    public bool Hit
    {
        get { return isHit; }
        set { isHit = value; }
    }

    private IEnumerator colorChange()
    {
        Color oga = new Color(1, 1, 1, 0.02f);
        for (int x = 0; x < HitCooldown * Multiplier; x++)
        {
            pl.color = oga;
            yield return new WaitForSeconds(ticks);
            //pl.color = OriginalColor;
            yield return new WaitForSeconds(ticks);
        }
        //print("done");
        //yield return null;
        //pl.color = OriginalColor;
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
