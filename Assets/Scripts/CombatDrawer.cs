using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3 _mousePos;
    private Vector3 _startMousePos;
    [SerializeField] private SpriteRenderer _arrowTest;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _target;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //Determine direction to rotate towards 
        Vector3 targetDirection = _target.position - _arrowTest.transform.position;

        // The speed at which we will rotate
        float singleStep = /*speed*/ Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(_arrowTest.transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray ponting at our target in
        Debug.DrawRay(_arrowTest.transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies the rotation to the sprite
        _arrowTest.transform.rotation = Quaternion.LookRotation(newDirection);

        //Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit raycastHit))
        //{
        //    _arrowTest.transform.LookAt(raycastHit.point - _arrowTest.transform.position);
        //    //_startMousePos = raycastHit.point;
        //}

        if (Input.GetMouseButton(0))
        {
            //_mousePos = raycastHit.point;
            //_lineRenderer.SetPosition(0, new Vector3(_startMousePos.x, .7f, _startMousePos.z));
            //_lineRenderer.SetPosition(1, new Vector3(_mousePos.x - (_mousePos.x/4), .7f, _mousePos.z - (_mousePos.z / 4)));
            //_lineRenderer.SetPosition(2, new Vector3(_mousePos.x, _mousePos.y, _mousePos.z));
        }
    }
}
