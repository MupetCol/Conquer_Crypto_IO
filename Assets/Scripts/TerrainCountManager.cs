using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerrainCountManager : MonoBehaviour
{
    public int _minCountValue = 0, _maxCountValue = 200;
    public int _count = 0;
    public float _countDecimal = 0f;
    private TMP_Text _countString;
    public bool _active;
    [SerializeField] public Transform[] _spawnPoints;
    [SerializeField] public Transform _spawnPointsHolder;

    private void Start()
    {
        if(!_active)_count = 40;
        _maxCountValue = 200;
        _countString = GetComponent<TMP_Text>();
        _countString.text = _count.ToString();
    }

    private void Update()
    {
        _countString.text = Mathf.Clamp(_count, _minCountValue, _maxCountValue).ToString();
        if(_count < 0) _count = 0;
        if (_active)
        {
            _countDecimal += Time.deltaTime;
            if(_countDecimal >= .2f && _count < _maxCountValue)
            {
                _countDecimal = 0;
                _count++;
            }
        }

    }

}
