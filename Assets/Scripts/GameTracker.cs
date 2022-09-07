using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    #region PRIVATE_VARIABLES

    private TerrainSpot[] _terrains;
    private int _currNations = 0;
    private List<int> _nationsTracked = new List<int>();

    #endregion

    #region PRIVATE_SERIALIZED_VARIABLES


    [SerializeField] private GameObject _winPopUp;

    #endregion

    #region UNITY_METHODS

    void Start()
    {
        // Get all terrainSport objects on scene
        _terrains = FindObjectsOfType<TerrainSpot>();
        StartCoroutine(CheckGameState());
    }

    #endregion

    #region COROUTINES


    IEnumerator CheckGameState()
    {
        //Coroutine checks everysecond, if there is only one nation that isn't the default nation (0 number)
        //Then we have a winner as there is only default lands and one other nation left
        while (true)
        {
            foreach (TerrainSpot terrain in _terrains)
            {
                if (terrain._nation != 0 && !_nationsTracked.Contains(terrain._nation))
                {
                    _currNations++;
                    _nationsTracked.Add(terrain._nation);
                }
            }
            if (_currNations == 1)
            {
                Debug.Log("WINNER");
                _winPopUp.SetActive(true);
            }
            yield return new WaitForSeconds(1);
            _currNations = 0;
            _nationsTracked.Clear();
        }
    }

    #endregion








}
