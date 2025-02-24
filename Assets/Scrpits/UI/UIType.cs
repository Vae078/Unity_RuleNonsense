using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType 
{
    private string path;
    private string name;

    public string Path { get => path; }
    public string Name { get => name; }
    /// <summary>
    /// 获得UI信息
    /// </summary>
    /// <param name="ui_path">对应Panel的路径</param>
    /// <param name="ui_name">对应Panem的名称</param>
    public UIType(string ui_path, string ui_name)
    {
        path = ui_path;
        name = ui_name;
    }
}
