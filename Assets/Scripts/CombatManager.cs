using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private TroopBehavior _troopCommander;
    [SerializeField] private string _selectableTag = "Selectable";

    public List<Transform> _attacker = new List<Transform>();
    private Color _attackerTextColor;
    public Material _attackerMaterial;
    private bool _attackerValidation;
    private float _compoundAttackTimer = 0f;
    private Transform _prevAttacker;

    private bool _holdingClick = false;


    public Transform _defender;
    private Color _defenderTextColor;

    private CombatDrawer _combatDrawer;

    public bool _isAttack = true;

    private void Start()
    {
        _combatDrawer = GetComponentInChildren<CombatDrawer>();
    }

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.Mouse0)) _holdingClick=true;

        if (Physics.Raycast(ray, out hit) && _holdingClick)
        {
            if (hit.transform.CompareTag(_selectableTag))
            {
                _attackerValidation = hit.transform.GetComponentInChildren<TerrainCountManager>()._active;
                if (_attackerValidation && !_attacker.Contains(hit.transform))
                {
                    if (_attacker.Count == 0 || _attacker[0].GetComponent<TerrainSpot>()._nation == hit.transform.GetComponent<TerrainSpot>()._nation)
                    {
                        if (_attacker.Count == 0 || _compoundAttackTimer > 1f)
                        {
                            _attacker.Add(hit.transform);
                            _attackerTextColor = _attacker[0].GetComponentInChildren<TMP_Text>().color;
                            _attackerMaterial = _attacker[0].GetComponent<TerrainSpot>()._terrainMaterial;
                        }

                        if (_prevAttacker != hit.transform)
                        {
                            _prevAttacker = hit.transform;
                            _compoundAttackTimer = 0;
                        }
                        else if(_prevAttacker == hit.transform)
                        {
                            _compoundAttackTimer += Time.deltaTime;
                        }
                    }

                }
            }
        }


        if(Physics.Raycast(ray, out hit) && Input.GetKeyUp(KeyCode.Mouse0))
        {
            _holdingClick = false;
            if(_attacker.Count != 0)
            {
                if(hit.transform != _attacker[0] && hit.transform.CompareTag(_selectableTag))
                {
                    _defender = hit.transform;
                    _defenderTextColor = _defender.GetComponentInChildren<TMP_Text>().color;

                    if (_attackerTextColor != _defenderTextColor)
                    {
                        _isAttack = true;
                        SpawnTroopAttack();
                        _attacker.Clear();

                    }
                    else if (_attackerTextColor == _defenderTextColor)
                    {
                        _isAttack = false;
                        SpawnTroopAttack();
                        _attacker.Clear();

                    }
                }
            }
        }
    }

    private void SpawnTroopAttack()
    {
        foreach(Transform attacker in _attacker)
        {
            Instantiate(_troopCommander, attacker, false);
        }
    }
}
