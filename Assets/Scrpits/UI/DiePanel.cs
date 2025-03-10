using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiePanel : BasePanel
{
    private static string name = "DiePanel";
    private static string path = "Panel/DiePanel";

    public static readonly UIType uIType = new UIType(path,name);

    public DiePanel() : base(uIType)
    {

    }

    public override void OnStart()
    {
        base.OnStart();
        UIMethod.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Button").onClick.AddListener(Restart); //检测返回Button，增加监听事件
        FirstPersonalLook.Instance.UnlockCursor();// 显示鼠标


    }

    private void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // 重新加载场景
        SceneManager.LoadScene(currentSceneIndex);
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }
}
