using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _arrowTest;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _attacker;
    [SerializeField] private Transform _target;


    Quaternion _startRotation;
    Quaternion _endRotation;
    float _startSize;
    float _endSize;

    float _lerpProgress = 0f;


    public float _rotSpeed = 1f;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 3;
    }

    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.CompareTag("Selectable"))
            {
                if(_target == null || _target != raycastHit.transform)
                {
                    _target = raycastHit.transform;
                    _lerpProgress = 0f;

                    float Angle = Vector3.Angle(_attacker.position, _target.position - _attacker.position);
                    _startRotation = _arrowTest.transform.rotation;
                    _endRotation = Quaternion.Euler(90, 0, -Angle);
                    _startSize = _arrowTest.GetComponent<SpriteRenderer>().size.y;
                    Vector2 _targetVect = new Vector2(raycastHit.transform.position.x, raycastHit.transform.position.z);
                    Vector2 _attackerVect = new Vector2(_attacker.transform.position.x, _attacker.transform.position.z);
                    _endSize = Vector2.Distance(_targetVect, _attackerVect);
                }


                if (_lerpProgress < 1 & _lerpProgress >= 0)
                {
                    _lerpProgress += Time.deltaTime * _rotSpeed;
                    _arrowTest.transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, _lerpProgress);
                    _arrowTest.GetComponent<SpriteRenderer>().size = Vector2.Lerp(new Vector2(0.5f, _startSize), new Vector2(0.5f, _endSize), _lerpProgress);
                }

            }
        }
    }

}
