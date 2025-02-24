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
    /// ��ó����е�Canvas
    /// </summary>
    /// <returns>Canvas</returns>
    public GameObject FindCanvas()
    {
        GameObject gameObject = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (gameObject == null)
        {
            Debug.LogError("û���ڳ������ҵ�Canvas");
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
        Debug.LogError($"��{panel.name}��û���ҵ�{child_name}����");
        return null;
    }

    /// <summary>
    /// ��Ŀ������л�ö�Ӧ���
    /// </summary>
    /// <typeparam name="T">��Ӧ���</typeparam>
    /// <param name="Get_obj">Ŀ�����</param>
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
       // Debug.LogWarning($"�޷���{Get_obj}�����ϻ��Ŀ�����");
        //return null;
    }

    /// <summary>
    /// ��Ŀ��Panel���������У���������������ƻ�ö�Ӧ���
    /// </summary>
    /// <typeparam name="T">Ŀ�����</typeparam>
    /// <param name="panel">Ŀ��Panel</param>
    /// <param name="Component">����������</param>
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

        Debug.LogWarning($"û����{panel.name}���ҵ�{ComponentName}���壡");
        return null;
    }

}
