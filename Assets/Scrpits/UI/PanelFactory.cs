using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//static ���ʾ ����ʵ��������ȫ�ַ��ʵĹ���
public static class PanelFactory 
{
    //static �������� �༶��ĳ�Ա������ʵ�����𣩣���Щ��Ա�����౾��
    //---------����Ҫ��������ʵ������ֱ�ӷ���----------------
    private static Dictionary<string, Func<BasePanel>> panelCreators = new Dictionary<string, Func<BasePanel>>()
    {
        {"CluePanel",()=>new CluePanel() },
    };

    public static BasePanel CreatPanel(string panelType)
    {
        if (panelCreators.TryGetValue(panelType, out var creator))
            return creator();
        else
            throw new ArgumentException($"δ֪��������ͣ�{panelType}");
    }




}
