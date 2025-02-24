using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ű�������ר�ż�Ɑ�ӵİڷ�λ��
/// ��isRoomClean��ֵ��
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
