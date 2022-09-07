using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerrainCountManager : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public int _minCountValue = 0, _maxCountValue = 200;
    public int _realCount = 0;
    public int _textCount = 0;
    public float _countDecimal = 0f;
    public bool _active;
    public Transform[] _spawnPoints;
    public Transform _spawnPointsHolder;

    #endregion

    #region PRIVATE_VARIABLES

    private TMP_Text _countString;

    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        if (!_active) _realCount = 40;
        _maxCountValue = 200;
        _countString = GetComponent<TMP_Text>();
        _textCount = _realCount;
        _countString.text = _textCount.ToString();
    }

    private void Update()
    {
        _countString.text = Mathf.Clamp(_textCount, _minCountValue, _maxCountValue).ToString();
        if (_realCount < 0) _realCount = 0;
        if (_active)
        {
            _countDecimal += Time.deltaTime;
            if (_countDecimal >= .2f && _realCount < _maxCountValue)
            {
                _countDecimal = 0;
                _textCount++;
                _realCount++;
            }
        }

    }

    #endregion

}
