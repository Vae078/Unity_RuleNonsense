using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorThemomenter : DetectorBase
{
    protected override GameState StateType => GameState.isThermometerRight;
    protected override string TargetTag => "Desk";
    protected override Vector3 Towadr => -transform.forward;


    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.forward * 2f);
    }
}
