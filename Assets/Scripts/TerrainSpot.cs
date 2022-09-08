using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpot : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public Material[] _terrainMaterials;
    public GameObject _pillar;
    public Material _defaultPillarMat;
    public int _nation = 0;

    #endregion

    enum Nations { Nationless, Nation1, Nation2, Nation3, Nation4 };

    #region UNITY_METHODS

    void Start()
    {
        if(_pillar != null)_defaultPillarMat = _pillar.GetComponent<MeshRenderer>().material;
        _terrainMaterials = GetComponent<MeshRenderer>().materials;
        if (_terrainMaterials[0] == Utilities.Instance._nationlessMaterial)
        {
            _nation = 0;
        }
    }

    #endregion





}
