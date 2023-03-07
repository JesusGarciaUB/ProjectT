using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionScreen : MonoBehaviour
{
    public TextMeshProUGUI text;
    private string textRaw;
    private int state;
    private bool firstInteraction = true;
    private int pos = 0;
    public int State
    {
        set { state = value; }
    }
    public string TextRaw
    {
        set { textRaw = value; }
    }
    private void FixedUpdate()
    {
        switch(state)
        {
            case 0:
                if (pos < textRaw.Length) text.text += textRaw[pos];
                else state = 1;
                pos++;
                break;
            case 1: 
                if (text.text != textRaw) text.text = textRaw;
                break;
            case 2:
                Finished();
                break; 
        }
    }
    public void Interacted()
    {
        if (firstInteraction)
        {
            text.text = "";
            gameObject.SetActive(true);
            pos = 0;
            firstInteraction = false;
            state = 0;
        }
        else state++;
    }

    private void Finished()
    {
        text.text = "";
        firstInteraction = true;
        gameObject.SetActive(false);
    }
}
