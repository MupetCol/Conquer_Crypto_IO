using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpot : MonoBehaviour
{
    public Material _terrainMaterial;
    public int _nation = 0;

    enum Nations { Nationless, Nation1, Nation2, Nation3, Nation4};

    void Start()
    {
        _terrainMaterial = GetComponent<Renderer>().material;
        if(_terrainMaterial == Utilities.Instance._nationlessMaterial)
        {
            _nation = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
