using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            float randomNum = Random.Range(0, 3);
            switch (randomNum)
            {
                case 0:
                    animator.SetTrigger("laserBeam");
                    break;
                case 1:
                    animator.SetTrigger("randomBullShit");
                    break;
                case 2:
                    animator.SetTrigger("finalAttack");
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

    protected void FinalAttack()
    {
        StartCoroutine(ActivateFinalAttack());
        StartCoroutine(Interval());
    }

    IEnumerator ActivateFinalAttack()
    {
        for (int i = 7; i < 13; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
            gameObject.transform.GetChild(i).GetComponent<laserBeam>().SetSound();
            yield return new WaitForSeconds(0.75f);
        }

        for (int i = 7; i < 13; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.75f);
        }

    }

    IEnumerator Interval()
    {
        yield return new WaitForSeconds(intervalAttacks);
        isAttack = true;
    }

    public override void Defeated()
    {
        //Instantiate(loot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        base.Defeated();
        StartCoroutine(EndScene());
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(3.0f);

        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        Destroy(GameObject.FindGameObjectWithTag("PersistentManager"));
        Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
        SceneManager.LoadScene(3);
    }
}
