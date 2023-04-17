using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Vector3 initialPosition;
    public float timeShake = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        initialPosition = GetComponent<CameraManager>().target.transform.position;
        if (timeShake > 0)
        {
            timeShake -= Time.deltaTime;
            transform.position = initialPosition + Random.Range(-0.05f, 0.05f) * Vector3.right + Random.Range(-0.05f, 0.05f) * Vector3.up;
        }
        if (timeShake <= 0.01f && timeShake > 0)
        {
            transform.position = initialPosition;
            timeShake = 0;
        }
    }
}
