using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorMedcine : MonoBehaviour
{

    private void Start()
    {
        
    }

    private void Update()
    {
        PerfromDetecting();
    }


    private void PerfromDetecting()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.3f))
        {
            if (hit.collider.tag == "Medicine")
            {
                Debug.Log("Medicine在这");
                StateDetector.Instance.SetState(GameState.isMedicineDestory, false);
            }
        }
        else
        {
            Debug.Log("Medicine不在这");
            StateDetector.Instance.SetState(GameState.isMedicineDestory, true);
        }

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}
