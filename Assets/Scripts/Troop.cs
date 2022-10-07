using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Troop : TroopBehavior
{
    #region PUBLIC_VARIABLES

    public bool _hasBeenStopped = false;
    public GameObject _particleEffect;
    public GameObject _impactEffect;

    public ParticleSystem[] _psSystems;

    private Rigidbody rb;

    #endregion

    #region PRIVATE_VARIABLES

    private Material _troopNation;

    #endregion

    #region UNITY_METHODS

    void Start()
    {
        float Angle = Vector3.Angle(_attackerWhenSpawned.forward, transform.InverseTransformPoint(_defenderWhenSpawned.position));
        transform.rotation = Quaternion.LookRotation(
            transform.InverseTransformPoint(_defenderWhenSpawned.position)-_attackerWhenSpawned.forward, Vector3.up);
        //WHEN INSTANTIATED WE IMMEDIATELY REST THE UNITS VALUE TO THE OVERALL _ATTACKERS COUNT
        _atTerrManager._textCount -= _value;

        //SET PARTICLE SYSTEM COLORS ACCORDING TO THE ATTACKERS TEXT COLOR
        //foreach (var ps in _psSystems)
        //{
        //    var main = ps.main;
        //    main.startColor = _attackerText.color;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Translate troop towards targets direction every frame
        if(!_hasBeenStopped)
        transform.Translate(_troopSpeed * Vector3.Normalize(_direction) * Time.deltaTime, Space.World);

        //When troop reaches it's objective we execute some visuals, change colors, destroy troops, etc
        if (Vector3.Distance(transform.position, _defenderWhenSpawned.position) < _distanceDestroy && !_hasBeenStopped)
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
                Instantiate(_atTerr._conquerEffect, _defTerr.transform.position + new Vector3(0,0.1f,0), Quaternion.identity);
                _defTerr._conquerEffect = _atTerr._conquerEffect;
                _defenderText.color = _attackerText.color;
                _defTerr._nation = _atTerr._nation;
                _defTerr._terrainMaterials = _atTerr._terrainMaterials;
                _defTerr._nation = _atTerr._nation;
                _defTerr._defaultPillarMat = _atTerr._defaultPillarMat;
                _defTerr.GetComponent<MeshRenderer>().materials = _atTerr._terrainMaterials;
                //_defenderWhenSpawned.GetComponent<TerrainSpot>()._terrainMaterials[0] = _attackerMaterial;
                _defTerrManager._active = true;

                if (_defTerr._pillar != null)
                {
                    _defTerr._pillar.GetComponent<MeshFilter>().mesh = _atTerr._pillar.GetComponent<MeshFilter>().mesh;
                    _defTerr._pillar.GetComponent<MeshRenderer>().material = _atTerr._defaultPillarMat;
                }
            }

            if (!_hasBeenStopped)
            StartCoroutine(DestroyTroop());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //DESTROY TROOP ON COLLISION WITH A DIFFERENT NATION'S TROOP
        if (collision.transform.tag == "Troop")
        {
            Troop troop = collision.transform.GetComponent<Troop>();
            if (troop._impactEffect != this._impactEffect)
            {
                StartCoroutine(troop.DestroyTroop());
            }
        }
    }

    #endregion

    #region PRIVATE_METHODS



    #endregion

    #region PUBLIC_METHODS

    public IEnumerator DestroyTroop()
    {
        Instantiate(_impactEffect, transform.position, Quaternion.identity);
        _hasBeenStopped = true;
        GetComponent<Collider>().enabled = false;

		for (int i = 0; i < _psSystems.Length; i++)
		{
            if (i == 0)
			{
                //Dissable main effect so the collision is more clear
                _psSystems[i].gameObject.SetActive(false);
			}
			else
			{
                //Stop the rest of the particle effects
                _psSystems[i].Stop();
            }
		}


        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    #endregion






}
