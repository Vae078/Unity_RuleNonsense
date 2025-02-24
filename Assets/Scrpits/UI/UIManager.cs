using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Stack<BasePanel> stack_ui; //存储UI panel的栈结构
    public Dictionary<string, GameObject> dict_UIobject; //存储Panel的名称与物体的对应结构
    public GameObject CanvasObj; //当前场景下对应的Canvas

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
    /// 往stack中压一个panel
    /// </summary>
    /// <param name="basePanel">目标panel</param>

    public void Push(BasePanel basePanel)
    {
        Debug.Log($"{basePanel.uiType.Name}被Push进stack");

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
        if (isload == true)  //将栈清空
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

        }else     //弹出栈顶元素
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