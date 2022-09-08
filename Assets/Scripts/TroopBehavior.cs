using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TroopBehavior : MonoBehaviour
{
    #region PROTECTED_VARIABLES

    protected CombatManager _combatManager;
    protected Transform _attackerWhenSpawned, _defenderWhenSpawned;
    protected TMP_Text _attackerText, _defenderText;
    protected Material _attackerMaterial;
    protected float _troopSpeed = 1f;
    public float _distanceDestroy = .615f;
    protected int _value = 5;
    protected Vector3 _direction;
    protected TerrainCountManager _atTerrManager, _defTerrManager;
    protected TerrainSpot _atTerr, _defTerr;

    #endregion

    #region PRIVATE_SERIALIZED_VARIABLES

    [SerializeField] private Troop _troop;

    #endregion

    #region UNITY_METHODS
    void Start()
    {
        SetNeededMembers();
        StartCoroutine(SendTroops());
    }

    #endregion

    #region PUBLIC_METHODS

    public virtual void SetNeededMembers()
    {
        _combatManager = FindObjectOfType<CombatManager>();
        _attackerMaterial = _combatManager._attackerMaterial;
        _attackerWhenSpawned = transform.parent.transform;
        _defenderWhenSpawned = _combatManager._defender;
        _attackerText = _combatManager._attackerText;
        _defenderText = _combatManager._defenderText;
        _atTerr = _attackerWhenSpawned.GetComponent<TerrainSpot>();
        _defTerr = _defenderWhenSpawned.GetComponent<TerrainSpot>();
        _direction = (_defenderWhenSpawned.position - _attackerWhenSpawned.position);
        _atTerrManager = _attackerWhenSpawned.GetComponentInChildren<TerrainCountManager>();
        _defTerrManager = _defenderWhenSpawned.GetComponentInChildren<TerrainCountManager>();
    }

    #endregion

    #region PRIVATE_METHODS

    private void InstantiateTroop(int value, Transform spawnPoint)
    {
        Troop troop = (Troop)Instantiate(_troop, spawnPoint.position, Quaternion.identity);

        //Initialize troop's needed variables
        troop._attackerWhenSpawned = this._attackerWhenSpawned;
        troop._combatManager = this._combatManager;
        troop._defenderWhenSpawned = this._defenderWhenSpawned;
        troop._defenderText = this._defenderText;
        troop._attackerText = this._attackerText;
        troop._direction = this._direction;
        troop._atTerrManager = this._atTerrManager;
        troop._defTerrManager = this._defTerrManager;
        troop._atTerr = this._atTerr;
        troop._defTerr = this._defTerr;
        troop._troopSpeed = this._troopSpeed;
        troop._distanceDestroy = this._distanceDestroy;
        troop._value = value;
        troop._attackerMaterial = this._attackerMaterial;
    }

    private void DestroyCommander()
    {
        Destroy(gameObject);
    }

    #endregion

    #region COROUTINES

    IEnumerator SendTroops()
    {
        _atTerrManager._spawnPointsHolder.LookAt(_atTerrManager._spawnPointsHolder.position - _direction);
        int troopAmount = _attackerWhenSpawned.GetComponentInChildren<TerrainCountManager>()._realCount / _value;
        int res = _attackerWhenSpawned.GetComponentInChildren<TerrainCountManager>()._realCount % _value;
        int counterSpawnPoints = 0;
        int counterTroopsSpawned = 0;

        _atTerrManager._realCount -= (troopAmount * _value) + (res);
        for (int i = 0; i < troopAmount; i++)
        {
            InstantiateTroop(_value, _atTerrManager._spawnPoints[counterSpawnPoints]);
            counterTroopsSpawned++;
            if (counterTroopsSpawned == 1 || counterTroopsSpawned == 4 || counterTroopsSpawned == 9)
            {
                yield return new WaitForSeconds(.2f);
                if (counterTroopsSpawned == 9) counterTroopsSpawned = 0;
            }
            counterSpawnPoints++;
            if (counterSpawnPoints > _atTerrManager._spawnPoints.Length - 1) counterSpawnPoints = 0;
        }

        if (res > 0)
        {
            InstantiateTroop(res, _atTerrManager._spawnPoints[counterSpawnPoints]);
        }
        _atTerrManager._spawnPointsHolder.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        yield return new WaitForSeconds(10);
        DestroyCommander();
    }

    #endregion

}
