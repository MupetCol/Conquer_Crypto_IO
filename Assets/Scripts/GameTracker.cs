using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    TerrainSpot[] _terrains;
    private int _currNations = 0;
    private List<int> _nationsTracked = new List<int>();

    [SerializeField] private GameObject _winPopUp;


    
    void Start()
    {
        _terrains = FindObjectsOfType<TerrainSpot>();

        StartCoroutine(CheckGameState());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckGameState()
    {
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
            if(_currNations == 1)
            {
                Debug.Log("WINNER");
                _winPopUp.SetActive(true);
            }
            yield return new WaitForSeconds(1);
            _currNations = 0;
            _nationsTracked.Clear();
        }
    }
}
