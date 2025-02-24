using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubTitlePanel : BasePanel
{
    private static string name = "SubtitlePanel";
    private static string path = "Panel/SubtitlePanel";

    public static readonly UIType uIType = new UIType(path, name);
    public TextMeshProUGUI textCompent;
    public Image image;
    public string text;

    public SubTitlePanel(string _text) : base(uIType)
    {
        text = _text;
      
    }

    public override void OnStart()
    {
        base.OnStart();

        textCompent = ActiveObj.GetComponentInChildren<TextMeshProUGUI>();
        image = ActiveObj.GetComponentInChildren<Image>();

        textCompent.text = null;
        //要做一个动态调整文本框与文本背景框
        //十一个汉字的width是400 一个就是37
        int width = Count(text) * 37;
        textCompent.rectTransform.sizeDelta = new Vector2(width, 50);
        image.rectTransform.sizeDelta = new Vector2(width + 50, 80);
        textCompent.text = text;

    }

    private int Count(string text)
    {
        int count = 0;
        foreach (var cahr in text)
        {
            count++;
        }
        return count;
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
