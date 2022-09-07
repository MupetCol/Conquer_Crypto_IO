using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // HANDLER FOR SELECTION VISUALS, MEANING, CHANGE MATERIAL OF THE OBJECT HOVERED 
    // BY THE PLAYERS MOUSE

    #region PRIVATE_VARIABLES

    private Material[] _highlightArray;
    private Material[] _defaultMaterials;
    private Material _defaultMatChild;
    private Transform _selection;
    private Camera _camera;

    #endregion

    #region PRIVATE_SERIALIZED_VARIABLES

    [SerializeField] private string _selectableTag = "Selectable";
    [SerializeField] private Material _highlightMaterial;

	#endregion

	#region UNITY_METHODS

	private void Start()
	{
        _camera = Camera.main;
	}

	private void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<MeshRenderer>();
            selectionRenderer.materials = _defaultMaterials;
            _selection.GetChild(0).GetComponent<MeshRenderer>().material = _defaultMatChild;
            _selection = null;

        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(_selectableTag))
            {
                var selectionRenderer = selection.GetComponent<MeshRenderer>();
                if (selectionRenderer != null)
                {
                    TerrainSpot terr = selection.GetComponent<TerrainSpot>();
                    _defaultMaterials = terr._terrainMaterials;
                    _defaultMatChild = terr._defaultPillarMat;

                    _highlightArray = new Material[selectionRenderer.materials.Length];
                    for (var j = 0; j < selectionRenderer.materials.Length; j++)
                    {
                        _highlightArray[j] = _highlightMaterial;
                    }
                    selectionRenderer.materials = _highlightArray;
                    terr._pillar.GetComponent<MeshRenderer>().material = _defaultMatChild? _highlightMaterial:null;
                }

                _selection = selection;
            }

        }
    }

    #endregion

}
