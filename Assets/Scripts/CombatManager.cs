using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    //COMBAT MANAGERS, DEALS WITH SPAWN OF TROOPS, ATTACK MECHANICS, ARROW VISUALS, ATTACK RAYCAST INPUTS

    #region PUBLIC_VARIABLES

    public List<Transform> _attacker = new List<Transform>();
    public Material _attackerMaterial;
    public Transform _defender;
    public TMP_Text _defenderText, _attackerText;
    public bool _isAttack = false;

    #endregion

    #region PRIVATE_VARIABLES

    private bool _attackerValidation;
    private float _compoundAttackTimer = 0f;
    private Transform _prevAttacker;
    private bool _holdingClick = false;
    private CombatDrawer _combatDrawer;

    #endregion

    #region PRIVATE_SERIALIZED_VARIABLES

    [SerializeField] private TroopBehavior _troopCommander;
    [SerializeField] private string _selectableTag = "Selectable";


    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        _combatDrawer = GetComponentInChildren<CombatDrawer>();
    }

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0)) _holdingClick = true;

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
                            hit.transform.GetComponentInChildren<CombatDrawer>().InstantiateArrow();
                            _compoundAttackTimer = 0;
                            _attackerText = _attacker[0].GetComponentInChildren<TMP_Text>();
                            _attackerMaterial = _attacker[0].GetComponent<TerrainSpot>()._terrainMaterials[0];
                        }

                        if (_prevAttacker != hit.transform)
                        {
                            _prevAttacker = hit.transform;
                            //_compoundAttackTimer = 0;
                        }
                        else if (_prevAttacker == hit.transform)
                        {
                            _compoundAttackTimer += Time.deltaTime;
                        }
                    }

                }
            }
        }

        if (Physics.Raycast(ray, out hit) && Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!hit.transform.CompareTag(_selectableTag))
            {
                ResetAttackersState();
            }
        }

        if (Physics.Raycast(ray, out hit) && Input.GetKeyUp(KeyCode.Mouse0))
        {
            _holdingClick = false;
            if (_attacker.Count != 0)
            {
                if (hit.transform != _attacker[0] && hit.transform.CompareTag(_selectableTag))
                {
                    _defender = hit.transform;
                    _defenderText = _defender.GetComponentInChildren<TMP_Text>();

                    SpawnTroopAttack();
                    ResetAttackersState();
                }
            }
        }
    }


    #endregion

    #region PRIVATE_METHODS
    private void ResetAttackersState()
    {
        foreach (Transform attacker in _attacker)
        {
            attacker.GetComponentInChildren<CombatDrawer>().DestroyArrow();
        }
        _attacker.Clear();
    }

    private void SpawnTroopAttack()
    {
        foreach (Transform attacker in _attacker)
        {
            Instantiate(_troopCommander, attacker, false);
        }
    }

    #endregion









}
