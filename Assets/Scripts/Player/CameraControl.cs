using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera  camera;

    private void Awake()
    {
        camera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    void Update()
    {
        
    }

    private void CameraZoom(){
        
    }
}
