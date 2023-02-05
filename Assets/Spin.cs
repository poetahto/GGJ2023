using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 spinSpeed;

    void Update()
    {
        transform.Rotate(spinSpeed * Time.deltaTime, Space.Self);
    }
}