using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel:BasePanel
{
    private static string name = "PackagePanel";
    private static string path = "Panel/PackagePanel";

    public static readonly UIType uIType = new UIType(path, name);
    UI_itemSlot[] ui_item;

    public PackagePanel() : base(uIType)
    {

    }

    public override void OnStart()  //进入背包
    {
        base.OnStart();
        UIMethod.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Back").onClick.AddListener(Back); //检测返回Button，增加监听事件
        ui_item=ActiveObj.GetComponentsInChildren<UI_itemSlot>();
        

        FirstPersonalLook.Instance.UnlockCursor();// 显示鼠标
        PlayerMove.instacnce.canMove=false;// 禁用人物移动
    }

    private void Back()    //返回方法
    {
        //GameRoot.GetInstacne().UIManager_Root.Pop(false);
        GameRoot.GetInstacne().UIManager_Root.Pop(false);
        Debug.Log("我在测试这里可以执行吗");
    }


    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()  //退出背包
    {
        
        base.OnDisable();
        FirstPersonalLook.Instance.LockCursor();  //锁定鼠标
        PlayerMove.instacnce.canMove = true;  //启用人物移动     
        GameRoot.GetInstacne().isPackageOpen = false;
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }
}
