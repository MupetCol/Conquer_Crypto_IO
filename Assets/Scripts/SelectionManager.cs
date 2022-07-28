using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string _selectableTag = "Selectable";
    [SerializeField] private Material _highlightMaterial;
    private Material _defaultMaterial;

    private Transform _selection;

    private void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = _defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(_selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    _defaultMaterial = selectionRenderer.GetComponent<TerrainSpot>()._terrainMaterial;
                    selectionRenderer.material = _highlightMaterial;
                }

                _selection = selection;
            }

        }
    }
}
