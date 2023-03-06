using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{

    public GameObject greyImage;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        greyImage.SetActive(false);
        text.SetActive(false);
    }

    public void usedAbility(int duration)
    {
        StartCoroutine(UILogic(duration));
    }

    private IEnumerator UILogic(int duration)
    {
        greyImage.SetActive(true);
        text.SetActive(true);
        for (int x = duration; x > 0; x--)
        {
            text.GetComponent<TextMeshProUGUI>().text = x.ToString();
            yield return new WaitForSeconds(1);
        }
        greyImage.SetActive(false);
        text.SetActive(false);
    }
}
