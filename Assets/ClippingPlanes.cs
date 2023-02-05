using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ClippingPlanes : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    Camera mainCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        float orthoSize = mainCamera.orthographicSize;
        print(orthoSize);
        float magicNumber = 1.74f;
        virtualCamera.m_Lens.NearClipPlane = mainCamera.transform.position.y * 2 + (-magicNumber * orthoSize);
        virtualCamera.m_Lens.FarClipPlane = mainCamera.transform.position.y * 2 + (magicNumber * orthoSize);
    }
}