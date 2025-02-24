using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
