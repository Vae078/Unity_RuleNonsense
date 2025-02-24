using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel:BasePanel
{
    private static string name = "PackagePanel";
    private static string path = "Panel/PackagePanel";

    public static readonly UIType uIType = new UIType(path, name);

    public PackagePanel() : base(uIType)
    {

    }

    public override void OnStart()  //���뱳��
    {
        base.OnStart();
        UIMethod.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Back").onClick.AddListener(Back); //��ⷵ��Button�����Ӽ����¼�
        FirstPersonalLook.Instance.UnlockCursor();// ��ʾ���
        PlayerMove.instacnce.canMove=false;// ���������ƶ�

    }

    private void Back()    //���ط���
    {
        //GameRoot.GetInstacne().UIManager_Root.Pop(false);
        GameRoot.GetInstacne().UIManager_Root.Pop(false);
    }


    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()  //�˳�����
    {
        
        base.OnDisable();
        FirstPersonalLook.Instance.LockCursor();  //�������
        PlayerMove.instacnce.canMove = true;  //���������ƶ�     
        GameRoot.GetInstacne().isPackageOpen = false;
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }
}
