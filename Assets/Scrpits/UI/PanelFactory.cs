using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//static 类表示 无需实例化即可全局访问的工具
public static class PanelFactory 
{
    //static 用于声明 类级别的成员（而非实例级别），这些成员属于类本身，
    //---------不需要创建对象实例即可直接访问----------------
    private static Dictionary<string, Func<BasePanel>> panelCreators = new Dictionary<string, Func<BasePanel>>()
    {
        {"CluePanel",()=>new CluePanel() },
    };

    public static BasePanel CreatPanel(string panelType)
    {
        if (panelCreators.TryGetValue(panelType, out var creator))
            return creator();
        else
            throw new ArgumentException($"未知的面板类型：{panelType}");
    }




}
