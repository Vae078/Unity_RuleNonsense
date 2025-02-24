using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMethod
{
    private static UIMethod instance;
    public static UIMethod GetInstance()
    {
        if (instance == null)
        {
            instance = new UIMethod();
        }
        return instance;
    }

    /// <summary>
    /// 获得场景中的Canvas
    /// </summary>
    /// <returns>Canvas</returns>
    public GameObject FindCanvas()
    {
        GameObject gameObject = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (gameObject == null)
        {
            Debug.LogError("没有在场景里找到Canvas");
            return gameObject;
        }

        return gameObject;

    }

    public GameObject FindObjectInChild(GameObject panel, string child_name)
    {
        Transform[] transforms = panel.GetComponentsInChildren<Transform>();
        foreach (var tra in transforms)
        {
            if (tra.gameObject.name == child_name)
            {
                return tra.gameObject;
            }
        }
        Debug.LogError($"在{panel.name}中没有找到{child_name}物体");
        return null;
    }

    /// <summary>
    /// 从目标对象中获得对应组件
    /// </summary>
    /// <typeparam name="T">对应组件</typeparam>
    /// <param name="Get_obj">目标对象</param>
    /// <returns></returns>

    public T AddOrGetComponent<T>(GameObject Get_obj) where T : Component
    {
        T compent = Get_obj.GetComponent<T>();
        if (Get_obj.GetComponent<T>() != null)
        {
            Debug.Log("Get_obj.GetComponentInParent<T>()" + Get_obj.GetComponentInParent<T>());
            return Get_obj.GetComponent<T>();
        }else
        {
            compent = Get_obj.AddComponent<T>();
            return compent;
        }
       // Debug.LogWarning($"无法在{Get_obj}物体上获得目标组件");
        //return null;
    }

    /// <summary>
    /// 从目标Panel的子物体中，根据子物体的名称获得对应组件
    /// </summary>
    /// <typeparam name="T">目标组件</typeparam>
    /// <param name="panel">目标Panel</param>
    /// <param name="Component">子物体名称</param>
    /// <returns></returns>
    public T GetOrAddSingleComponentInChild<T>(GameObject panel, string ComponentName) where T : Component
    {
        Transform[] transforms = panel.GetComponentsInChildren<Transform>();
        foreach (Transform tra in transforms)
        {
            if (tra.gameObject.name == ComponentName)
            {
                return tra.gameObject.GetComponent<T>();
                //break;
            }
        }

        Debug.LogWarning($"没有在{panel.name}中找到{ComponentName}物体！");
        return null;
    }

}
