using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectorBase : MonoBehaviour
{
    protected abstract GameState StateType { get; }
    protected abstract string TargetTag { get; }
    protected abstract Vector3 Towadr { get; }
    protected Transform _objectTransform;
    protected float _raycastDistance = 1f;

    private void Start()
    {
        _objectTransform = transform;
    }

    private void Update()
    {
        PerformDetecting();
        
    }


    private void PerformDetecting()
    {
        Ray ray = new Ray(_objectTransform.position, Towadr);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _raycastDistance))
        {
            if (hit.collider.CompareTag(TargetTag))
            {
                StateDetector.Instance.SetState(StateType, true);
            }else
            {
                return;
            }
        }
    }

}
