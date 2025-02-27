using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CluePanel2 : BasePanel
{
    private static string name = "CluePanel2";
    private static string path = "Panel/CluePanel2";

    public static readonly UIType uIType = new UIType(path, name);

    public CluePanel2() : base(uIType)
    {

    }

    public override void OnStart()
    {
        base.OnStart();
        UIMethod.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Back").onClick.AddListener(Back); //��ⷵ��Button�����Ӽ����¼�
        UIMethod.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Turn").onClick.AddListener(Turn); //��ⷵ��Button�����Ӽ����¼�
        FirstPersonalLook.Instance.UnlockCursor();
        PlayerMove.instacnce.canMove = false;
    }

    private void Back()
    {
        GameRoot.GetInstacne().UIManager_Root.Pop(true);

    }
    private void Turn()
    {
        GameRoot.GetInstacne().UIManager_Root.Pop(false);
        GameRoot.GetInstacne().ClueWatch();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        FirstPersonalLook.Instance.LockCursor();  //�������
        PlayerMove.instacnce.canMove = true;  //���������ƶ�   
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }
}
