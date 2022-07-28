using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour 
{
    public static Utilities Instance { get; private set; }
    public Material _nationlessMaterial;

    private void Awake()
    {
        Instance = this;
    }


}
