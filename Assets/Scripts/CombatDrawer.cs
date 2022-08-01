using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDrawer : MonoBehaviour
{
    

    // Testing gameobjects
    [SerializeField] private Transform _instantiatePos;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Transform _atacker;

    private Transform _target;
    private GameObject _tempArrow;
    private CombatManager _combatManager;
    private Camera _mainCamera;

    // Variables for saving start and end rotation/size of the arrow sprite
    Quaternion _startRotation;
    Quaternion _endRotation;
    float _startSize;
    float _endSize;

    // Works as lerp's alpha and bool condition at the same time
    float _lerpProgress = 0f;

    public float _rotSpeed = 5f;

    void Start()
    {
        _mainCamera = Camera.main;
        _combatManager = FindObjectOfType<CombatManager>();
    }

    public void InstantiateArrow()
    {
        _tempArrow = Instantiate(_arrow, _instantiatePos.transform.position,Quaternion.identity);
    }

    public void DestroyArrow()
    {
        Destroy(_tempArrow);
    }

    void Update()
    {
        //Debug.Log(_combatManager._attacker.Count);
        // Cast a ray from camera through mouse position
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            // We only execute code if the object's tag is valid
            if (raycastHit.transform.CompareTag("Selectable") && _tempArrow != null)
            {
                // Only execute this part of the code if it's the first object raycasted, meaning _target is still null
                // or if _target is different to the raycasted object, meaning is a different _target object now
                if(_target == null || _target != raycastHit.transform)
                {
                    // Set current _target transform
                    _target = raycastHit.transform;

                    // Reset _lerpProgress each time a new _target is set
                    _lerpProgress = 0f;

                    // Get angle between _attacker and _target vectors to use it for arrows rotation
                    float Angle = Vector3.Angle(_atacker.forward, transform.InverseTransformPoint(_target.position));
                    Angle = Vector3.Dot(Vector3.right, transform.InverseTransformPoint(_target.position)) > 0.0 ? Angle : -Angle;

                    if(_atacker.position.z - _target.position.z < 0.0f && _atacker.position.x == _target.position.x)
                    {
                        Angle = 0;
                    }else if (_atacker.position.z - _target.position.z > 0.0f && _atacker.position.x == _target.position.x)
                    {
                        Angle = 180;
                    }
                    //Debug.Log(Angle);
                    Debug.DrawLine(_atacker.position, _target.position);

                    // Set start rotation
                    _startRotation = _tempArrow.transform.rotation;
                    // Set target rotation
                    _endRotation = Quaternion.Euler(90, 0, -Angle);

                    // Set start size
                    _startSize = _tempArrow.GetComponent<SpriteRenderer>().size.y;
                    // Create Vector2 for _attacker and _target only accounting for X, Z axies which are the ones
                    // that we care about for modifying the arrows size
                    Vector2 _targetVect = new Vector2(raycastHit.transform.position.x, raycastHit.transform.position.z);
                    Vector2 _attackerVect = new Vector2(_atacker.transform.position.x, _atacker.transform.position.z);
                    // Set target size finding distance between the two new vectors
                    _endSize = Vector2.Distance(_targetVect, _attackerVect);
                }


                if (_lerpProgress < 1 & _lerpProgress >= 0)
                {
                    // Lerp of arrow's rotation and size
                    _lerpProgress += Time.deltaTime * _rotSpeed;
                    _tempArrow.transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, _lerpProgress);
                    _tempArrow.GetComponent<SpriteRenderer>().size = Vector2.Lerp(new Vector2(0.5f, _startSize), new Vector2(0.5f, _endSize), _lerpProgress);
                }

            }
        }
    }

}
