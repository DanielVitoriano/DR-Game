using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private int health = 100;
    private bool isFighting = false;

    public int Health { get => health; set => health = value; }
    public bool IsFighting { get => isFighting; set => isFighting = value; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
