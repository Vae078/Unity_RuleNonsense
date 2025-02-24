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
    /// ���UI��Ϣ
    /// </summary>
    /// <param name="ui_path">��ӦPanel��·��</param>
    /// <param name="ui_name">��ӦPanem������</param>
    public UIType(string ui_path, string ui_name)
    {
        path = ui_path;
        name = ui_name;
    }
}
