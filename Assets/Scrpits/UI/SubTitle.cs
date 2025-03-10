using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SubTitle : MonoBehaviour
{
    private static SubTitle instance;
    public TextMeshProUGUI textCompent;
    public Image image;
    public string text;


    public static SubTitle GetInstance()
    {
        if (instance == null)
        {
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
    }


    private void Start()
    {
        image = GetComponent<Image>();
        textCompent = GetComponentInChildren<TextMeshProUGUI>();
        textCompent.text = null;
        image.enabled = false;

    }

    private void HideUI()
    {
        image.enabled = false;
        textCompent.text = null;
        textCompent.gameObject.SetActive(false);
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

    private void PrintSubtitle(string _text)
    {
        HideUI();
        float width = Count(_text)*37;
        textCompent.rectTransform.sizeDelta = new Vector2(width, 50);
        image.rectTransform.sizeDelta = new Vector2(width + 50, 80);
        textCompent.text = _text;
        textCompent.gameObject.SetActive(true);
        image.enabled = true;
        StartCoroutine(waitForClose());

    }

    IEnumerator waitForClose()
    {
        yield return new WaitForSeconds(3f);
        HideUI();
    }


    //---------------------------------------------//
    //--------下面给各个人物的字幕封装起来---------//

    public void BirdDorTalk(string _text)
    {
        _text = "Bird Dor:" + _text;
        PrintSubtitle(_text);
    }



}
