using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Troop : TroopBehavior
{
    #region PUBLIC_VARIABLES

    public ParticleSystem[] _psSystems;

    #endregion

    #region PRIVATE_VARIABLES

    private Material _troopNation;

    #endregion

    #region UNITY_METHODS

    void Start()
    {
        //WHEN INSTANTIATED WE IMMEDIATELY REST THE UNITS VALUE TO THE OVERALL _ATTACKERS COUNT
        _atTerrManager._textCount -= _value;

        //SET PARTICLE SYSTEM COLORS ACCORDING TO THE ATTACKERS TEXT COLOR
        foreach (var ps in _psSystems)
        {
            var main = ps.main;
            main.startColor = _attackerText.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Translate troop towards targets direction every frame
        transform.Translate(_troopSpeed * Vector3.Normalize(_direction) * Time.deltaTime, Space.World);

        //When troop reaches it's objective we execute some visuals, change colors, destroy troops, etc
        if (Vector3.Distance(transform.position, _defenderWhenSpawned.position) < _distanceDestroy)
        {

            if (_defenderText.color != _attackerText.color)
            {
                _defTerrManager._realCount -= _value;
                _defTerrManager._textCount -= _value;
            }
            else if (_defenderText.color == _attackerText.color)
            {
                _defTerrManager._realCount += _value;
                _defTerrManager._textCount += _value;
            }

            if (_defTerrManager._realCount <= 0)
            {

                _defenderText.color = _attackerText.color;
                _defTerr._nation = _atTerr._nation;
                _defTerr._terrainMaterials = _atTerr._terrainMaterials;
                _defTerr._defaultPillarMat = _atTerr._defaultPillarMat;
                _defTerr._pillar.GetComponent<MeshFilter>().mesh = _atTerr._pillar.GetComponent<MeshFilter>().mesh;
                _defTerr._pillar.GetComponent<MeshRenderer>().material = _atTerr._defaultPillarMat;
                _defTerr.GetComponent<MeshRenderer>().materials = _atTerr._terrainMaterials;
                //_defenderWhenSpawned.GetComponent<TerrainSpot>()._terrainMaterials[0] = _attackerMaterial;
                _defTerrManager._active = true;
            }
            Destroy(gameObject);
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void OnTriggerEnter(Collider collision)
    {
        //DESTROY TROOP ON COLLISION WITH A DIFFERENT NATION'S TROOP
        if (collision.transform.tag == "Troop")
        {
            Troop troop = collision.transform.GetComponent<Troop>();
            if (_attackerText.color != troop._psSystems[0].main.startColor.color)
            {
                troop.DestroyTroop();
            }
        }
    }

    #endregion

    #region PUBLIC_METHODS

    public void DestroyTroop()
    {
        Destroy(gameObject);
    }

    #endregion






}
