using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl 
{

    public Dictionary<string, SceneBase> dict_scene;

    private static SceneControl instance;
    public static SceneControl GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("SceneControl实体不存在");
            return instance;
        }
        return instance;
    }


    /// <summary>
    /// 加载一个场景
    /// </summary>
    /// <param name="scene_name">目标场景的名称</param>
    /// <param name="sceneBase">目标场景的Scene</param>
    public void LoadScene(string scene_name, SceneBase sceneBase)
    {
        if (!dict_scene.ContainsKey(scene_name))
        {
            dict_scene.Add(scene_name, sceneBase);

        }
        if (dict_scene.ContainsKey(SceneManager.GetActiveScene().name))
        {
            dict_scene[SceneManager.GetActiveScene().name].ExitScene();
        }else
        {
            Debug.LogWarning($"SceneControl字典中不包含{SceneManager.GetActiveScene().name}!");
        }

        GameRoot.GetInstacne().UIManager_Root.Pop(true);
        SceneManager.LoadScene(scene_name);
        sceneBase.EnterScene();

    }
}
