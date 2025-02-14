using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 1f; //Time on screen
    public float floatSpeed = 0.01f; //Floating text speed
    public Vector3 floatDirection = new Vector3(0, 1, 0);
    public TextMeshProUGUI textMesh;
    float timeElapsed = 0.0f; //Start timer
    RectTransform rectTransform;
    Color startingColor;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startingColor = textMesh.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        rectTransform.position += floatDirection * floatSpeed * Time.fixedDeltaTime; //Change text position 
        textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));//Change alpha channel to add banish text effect

        if (timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
