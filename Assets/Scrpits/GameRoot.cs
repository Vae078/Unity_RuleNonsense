using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private UIManager UIManager;
    public UIManager UIManager_Root { get => UIManager; }

    private SceneControl SceneControl;
    public SceneControl Scemetrol_Root { get => SceneControl; }

    private static GameRoot instance;
    private string tempText;


    // 以下是用于控制各种组件的bool
    public bool isPackageOpen;  //用于控制背包

    public static GameRoot GetInstacne()
    {
        if (instance == null)
        {
            Debug.LogError("GameRoot 获得实例失败");
            return instance;
        }
        return instance;
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        UIManager = new UIManager();
        SceneControl = new SceneControl();
    }


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        UIManager_Root.CanvasObj = UIMethod.GetInstance().FindCanvas();
        tempText = null;
    }

    private void Update()
    {
        PackageControl();
        //SubtitleControl();
    }

    private void PackageControl()   //控制背包系统
    {
        PackagePanel package = new PackagePanel();

        if (!isPackageOpen)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                UIManager_Root.Push(package);
                isPackageOpen = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                UIManager_Root.Pop(false);
                isPackageOpen = false;
            }
        }

    }

    public void DieControl()
    {
        DiePanel diePanel = new DiePanel();
        UIManager_Root.Push(diePanel);
    }

    public void SubtitleControl_BirDor(string text)
    {
        Debug.Log($"text:{text},tempText{tempText}");
        if (text == tempText)
        {
            return;
        }
        getTempText(text);
        text = "鸟嘴医生:" + text;
        SubTitlePanel subTitlePanel = new SubTitlePanel(text);
        UIManager_Root.Push(subTitlePanel);
        StartCoroutine(waitForSubtitle());
    }


    public void ClueWatch()
    {
        CluePanel cluePanel = new CluePanel();
        UIManager_Root.Push(cluePanel);
    }

    public void ClueWatch2()
    {
        CluePanel2 cluePanel2 = new CluePanel2();
        UIManager_Root.Push(cluePanel2);
    }

    //通过访问Panel工厂来Push Panel
    public void GetPanel(string panel)
    {
        UIManager_Root.Push(PanelFactory.CreatPanel(panel));

    }



    private void getTempText(string _text)
    {
        tempText = _text;
    }

    IEnumerator waitForSubtitle()
    {
        yield return new WaitForSeconds(3f);
        UIManager_Root.Pop(false);
    }
}
