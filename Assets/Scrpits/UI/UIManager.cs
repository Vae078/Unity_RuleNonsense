using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Stack<BasePanel> stack_ui; //�洢UI panel��ջ�ṹ
    public Dictionary<string, GameObject> dict_UIobject; //�洢Panel������������Ķ�Ӧ�ṹ
    public GameObject CanvasObj; //��ǰ�����¶�Ӧ��Canvas

    private static UIManager instance;
    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            return instance;
        }
        else
        {
            return instance;
        }
    }

    public UIManager()
    {
        instance = this;
        stack_ui = new Stack<BasePanel>();
        dict_UIobject = new Dictionary<string, GameObject>();
    }

    public GameObject GetSingleObject(UIType uIType)
    {
        if (dict_UIobject.ContainsKey(uIType.Name))
        {
            return dict_UIobject[uIType.Name];
        }

        if (CanvasObj == null)
        {
            CanvasObj = UIMethod.GetInstance().FindCanvas();
           
        }

        GameObject gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uIType.Path),CanvasObj.transform);
        return gameObject;
    }


    /// <summary>
    /// ��stack��ѹһ��panel
    /// </summary>
    /// <param name="basePanel">Ŀ��panel</param>

    public void Push(BasePanel basePanel)
    {
        Debug.Log($"{basePanel.uiType.Name}��Push��stack");

        if (stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisable();
        }
        if (dict_UIobject.ContainsKey(basePanel.uiType.Name))
        {
            //GameObject.Destroy(dict_UIobject[basePanel.uiType.Name]);
            dict_UIobject.Remove(basePanel.uiType.Name);
        }
        GameObject ui_object = GetSingleObject(basePanel.uiType);

        dict_UIobject.Add(basePanel.uiType.Name, ui_object);
        basePanel.ActiveObj = ui_object;

        if (stack_ui.Count == 0)
        {
            stack_ui.Push(basePanel);
        }
        else
        {
            if (stack_ui.Peek().uiType.Name != basePanel.uiType.Name)
            {
                stack_ui.Push(basePanel);
            }
        }
        basePanel.OnStart(); 
    }

    public void Pop(bool isload)
    {
        if (isload == true)  //��ջ���
        {
            if (stack_ui.Count > 0)
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestory();
                GameObject.Destroy(dict_UIobject[stack_ui.Peek().uiType.Name]);
                dict_UIobject.Remove(stack_ui.Peek().uiType.Name);
                stack_ui.Pop();
                Pop(true);
            }

        }else     //����ջ��Ԫ��
        {
            if (stack_ui.Count > 0)
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestory();
                GameObject.Destroy(dict_UIobject[stack_ui.Peek().uiType.Name]);
                dict_UIobject.Remove(stack_ui.Peek().uiType.Name);
                stack_ui.Pop();
                if (stack_ui.Count > 0)
                {
                    stack_ui.Peek().OnEnable();
                }

            }
        }
    }


}