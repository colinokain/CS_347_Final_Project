//Colin O'Kain, Joshua Payne, Christopher Day

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false;   // makes object invisible
    }
}
