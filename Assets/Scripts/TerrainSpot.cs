using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpot : MonoBehaviour
{
    public Material[] _terrainMaterials;
    public int _nation = 0;

    enum Nations { Nationless, Nation1, Nation2, Nation3, Nation4};

    void Start()
    {
        _terrainMaterials = GetComponent<MeshRenderer>().materials;
        if(_terrainMaterials[0] == Utilities.Instance._nationlessMaterial)
        {
            _nation = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
