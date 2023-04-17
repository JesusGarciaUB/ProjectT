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
        pl = PersistentManager.Instance.PlayerGlobal.GetComponent<SpriteRenderer>();
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
        Color oga = new Color(255, 255, 255, 50);
        for (int x = 0; x < HitCooldown * Multiplier; x++)
        {
            pl.color = oga;
            new WaitForSeconds(ticks);
            //print("change");
            pl.color = OriginalColor;
            new WaitForSeconds(ticks);
            //print("og");
        }
        //print("done");
        yield return null;
        //pl.color = OriginalColor;
    }
    public void Hitted()
    {
        StartCoroutine(colorChange());
        StartCoroutine(HitCooldownV());
    }

    public IEnumerator HitCooldownV()
    {
        isHit = true;
        yield return new WaitForSeconds(HitCooldown);
        isHit = false;
    }
}
