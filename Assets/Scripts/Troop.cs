using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Troop : MonoBehaviour
{
    public CombatManager _combatManager;
    public Transform _attackerWhenSpawned, _deffenderWhenSpawned;
    public TMP_Text _attackerText, _defenderText;
    public TerrainCountManager _atTerrManager, _defTerrManager;
    public TerrainSpot _atTerr, _defTerr;
    public float _troopSpeed;
    public float _distanceDestroy;
    public int _value;
    public Vector3 _direction;
    public Material _attackerMaterial;
    public Material _troopNation;

    void Start()
    {
        _atTerrManager._count -= _value;
        _troopNation = GetComponent<Renderer>().material;
        _troopNation.color = _attackerMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_troopSpeed * Vector3.Normalize(_direction) * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, _deffenderWhenSpawned.position) < _distanceDestroy)
        {
            
            if (_defenderText.color != _attackerText.color)
            {
                _defTerrManager._count -= _value;
            }
            else if (_defenderText.color == _attackerText.color)
            {
                _defTerrManager._count += _value;
            }

            if (_defTerrManager._count <= 0)
            {
                _defenderText.color = _attackerText.color;
                _defTerr._nation = _atTerr._nation;
                _deffenderWhenSpawned.GetComponent<Renderer>().material = _attackerMaterial;
                _deffenderWhenSpawned.GetComponent<TerrainSpot>()._terrainMaterials[0] = _attackerMaterial;
                _defTerrManager._active = true;
            }
            Destroy(gameObject);
        }
    }

    public void DestroyTroop()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Troop")
        {
            Troop troop = collision.transform.GetComponent<Troop>();
            if (_troopNation.color != troop._troopNation.color)
            {
                troop.DestroyTroop();
            }
        }
    }
}
