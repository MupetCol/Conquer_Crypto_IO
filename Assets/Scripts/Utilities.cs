using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour 
{
    #region PUBLIC_VARIABLES
    public static Utilities Instance { get; private set; }
    public Material _nationlessMaterial;

    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        Instance = this;
    }

    #endregion


}
