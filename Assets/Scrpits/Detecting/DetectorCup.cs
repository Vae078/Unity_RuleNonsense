using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorCup : DetectorBase
{
    protected override GameState StateType => GameState.isCupRight;
    protected override string TargetTag => "Desk";

    protected override Vector3 Towadr => -transform.up;
}
