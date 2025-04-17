using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LightTriggerPro : MonoBehaviour
{
    private Light spotLight;
    private Transform spotTranform;
   // private GameObject triggetObject;
    List<TextMeshPro> texts = new List<TextMeshPro>();

    private Vector3 _overlapCenter;      // OverlapSphere 的中心点
    private float _overlapRadius;        // OverlapSphere 的半径

    private bool isLighting;

    private void Start()
    {
        isLighting = false;
        spotLight = GetComponentInChildren<Light>();
        spotTranform = spotLight.transform;

        GameObject[] textObjects = GameObject.FindGameObjectsWithTag("text");
        foreach (GameObject obj in textObjects)
        {
            TextMeshPro textCompent = obj.GetComponent<TextMeshPro>();
            if (textCompent != null)
            {
                texts.Add(textCompent);
            }
        }
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))    // 切换手电筒开关
        {
            isLighting = !isLighting;
        }

        if (isLighting)
        {
            spotLight.enabled = true;
            UpdateTriggerCollider();
        }else
        {
            spotLight.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.SetActive(false);
        }

    }


    /// <summary>
    /// 这个是Q神头脑风暴出来的动态调整触发器
    /// 成功的使触发器大小和spot光线照在墙面上的大小匹配
    /// 现在的效果是trigger的大小和光照完全匹配
    /// 你的紫外线光找到字上就会显现
    /// </summary>
    private void UpdateTriggerCollider()
    {
        RaycastHit hit;
        if (Physics.Raycast(spotTranform.position, spotTranform.forward, out hit))
        {
            float distance = Vector3.Distance(spotTranform.position, hit.point);
            float angle = spotLight.spotAngle / 2 * Mathf.Deg2Rad;
            float r = Mathf.Tan(angle) * distance;
            Collider[] sphereTrigger = Physics.OverlapSphere(hit.point, r);
            HashSet<Collider> colliderSet = new HashSet<Collider>(sphereTrigger);
            foreach (TextMeshPro textCompent in texts)
            {
                Collider textCollider = textCompent.GetComponent<Collider>();
                if (colliderSet.Contains(textCollider))
                {
                    textCompent.GetComponent<MeshRenderer>().enabled = true;
                    Color textColor = textCompent.color;
                    textColor.a = SetAlapha(distance);
                    textCompent.color = textColor;
                    
                }else
                {
                    textCompent.GetComponent<MeshRenderer>().enabled = false;
                }
            }

            _overlapCenter = hit.point;
            _overlapRadius = r*0.7f;

           
        }
    }

    private float SetAlapha(float distance)
    {
        float maxDistance = 20f;
        distance = Mathf.Clamp(distance, 0, maxDistance);
        float normlizedAlpha = 0.6f - (distance / maxDistance);
        return normlizedAlpha;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_overlapCenter, _overlapRadius);
    }


}
