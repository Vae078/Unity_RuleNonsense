using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这个脚本是用来专门检测杯子的摆放位置
/// 给isRoomClean赋值用
/// </summary>
public class CupDetecting : MonoBehaviour
{
    public GameState isRoomClean = GameState.isRoomClean;
    private Transform CupTransform;



    private void Start()
    {
        CupTransform = transform;
    }


    private void Update()
    {
        RayDetect();
    }

    public void RayDetect()
    {
        Ray ray = new Ray(CupTransform.position, -CupTransform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            if (hit.collider.tag == "Desk")
            {
                StateDetector.Instance.SetState(isRoomClean, true);
            }
        }
    }


}
