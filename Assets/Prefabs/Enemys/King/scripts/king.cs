using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class king : EnemyBehaviour
{
    public GameObject laserBeam;
    public float intervalAttacks;

    private float count;
    private bool isAttack = true;

    void FixedUpdate()
    {
        if (isAttack)
        {
            float randomNum = Random.Range(0, 2);
            switch (randomNum)
            {
                case 0:
                    animator.SetTrigger("laserBeam");
                    break;
                case 1:
                    animator.SetTrigger("randomBullShit");
                    break;
                case 2:
                    print("cosa3");
                    break;
            }
            isAttack = false;        }
      
    }

    protected void LaserBeam()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
            gameObject.transform.GetChild(i).GetComponent<laserBeam>().SetSound();
        }
        StartCoroutine(LaserBeamCooldown());
    }

    IEnumerator LaserBeamCooldown()
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 3; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        StartCoroutine(Interval());
    }

    protected void RandomBullShit()
    {
        StartCoroutine(ActivateRandomBullShit());
        StartCoroutine(Interval());
    }

    IEnumerator ActivateRandomBullShit()
    {
        print("random");
        for (int i = 3; i < 7; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
            gameObject.transform.GetChild(i).GetComponent<laserBeam>().SetSound();
            yield return new WaitForSeconds(0.75f);
        }

        for (int i = 3; i < 7; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.75f);
        }

    }

    IEnumerator Interval()
    {
        yield return new WaitForSeconds(intervalAttacks);
        print("ya ta");
        isAttack = true;
    }

    public override void Defeated()
    {
        //Instantiate(loot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        base.Defeated();
    }
}
