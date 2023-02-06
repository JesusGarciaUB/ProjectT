using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public float speed = 5.0f;
    public GameObject target;
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
