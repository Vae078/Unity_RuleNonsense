using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel
{
    public UIType uiType;

    public GameObject ActiveObj;

    public BasePanel(UIType uitype)
    {
        uiType = uitype;
    }

    public virtual void OnStart()
    {
        Debug.Log($"{uiType.Name}开始使用");
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;

    }

    public virtual void OnEnable()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;

    }

    public virtual void OnDisable()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }
    
    public virtual void OnDestory()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;

    }

}
