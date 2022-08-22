using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string _selectableTag = "Selectable";
    [SerializeField] private Material _highlightMaterial;
    private Material[] _highlightArray;
    private Material[] _defaultMaterials;

    private Transform _selection;

    private void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<MeshRenderer>();
            selectionRenderer.materials = _defaultMaterials;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(_selectableTag))
            {
                var selectionRenderer = selection.GetComponent<MeshRenderer>();
                if (selectionRenderer != null)
                {
                    _defaultMaterials = selectionRenderer.GetComponent<TerrainSpot>()._terrainMaterials;

                    _highlightArray = new Material[selectionRenderer.materials.Length];
                    for (var j = 0; j < selectionRenderer.materials.Length; j++)
                    {
                        _highlightArray[j] = _highlightMaterial;
                    }
                    selectionRenderer.materials = _highlightArray;
                }

                _selection = selection;
            }

        }
    }
}
