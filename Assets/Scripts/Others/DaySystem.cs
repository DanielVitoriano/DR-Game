using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    [SerializeField] private Transform directionalLight;

    [SerializeField] [Tooltip("Duração do dia")] private int dayDuration;

    private float sec;
    private float multiplier;

    void Start()
    {
        multiplier = 86400 / dayDuration;
    }
    void Update()
    {
        sec += Time.deltaTime * multiplier;

        if(sec >= 86400){
            sec = 0;
        }

        ProcesSky();
        CalcHour();
    }

    private void ProcesSky(){
        float rotateX = Mathf.Lerp(-90, 270, sec/86400);
        directionalLight.rotation = Quaternion.Euler(rotateX, 0, 0);
    }

    private void CalcHour(){

    }
}
