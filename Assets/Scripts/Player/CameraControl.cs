using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineFreeLook camera;
    private float currentZoom = 2;
    private CinemachineFreeLook.Orbit[] orbits;

    private void Awake()
    {
        camera = FindObjectOfType<CinemachineFreeLook>();
        orbits = new CinemachineFreeLook.Orbit[camera.m_Orbits.Length];
        for (int x = 0; x < orbits.Length; x++)
        {
            orbits[x] = camera.m_Orbits[x];
        }
    }

    void Update()
    {
        CameraZoom();
    }

    private void CameraZoom()
    {
        currentZoom += Input.GetAxis("Mouse ScrollWheel") * -1;

        for (int x = 0; x < orbits.Length; x++)
        {
            camera.m_Orbits[x].m_Height = orbits[x].m_Height * currentZoom;
            camera.m_Orbits[x].m_Radius = orbits[x].m_Radius * currentZoom;
        }
    }
}
