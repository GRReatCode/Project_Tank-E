using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AltitudeCameraController : MonoBehaviour
{

    CinemachineFollowZoom cam;
    public float maxFov, minFov, actualFov = 40;

    void Start()
    {
        cam = GetComponent<CinemachineFollowZoom>();
    }

    // Update is called once per frame
    void Update()
    {


        actualFov += Input.mouseScrollDelta.y *2;
        ;
        if (actualFov < 20) actualFov = 20;
        if (actualFov > 80) actualFov = 80;

        cam.m_Width = actualFov;

    }
}
