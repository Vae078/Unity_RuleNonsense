using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


//游戏状态枚举
public enum GameState
{
    isEatMedicine,  //玩家是否吃药
    isMedicineDestory,    //药物是否销毁
    isRoomClean,      //房间是否整洁
    isDoorTouch       //房门是否被动
}

public class StateDetector : MonoBehaviour
{
    private static StateDetector _instance;

    public static StateDetector Instance    //全局访问点

    {
        get
        {   // 如果实例不存在就查找或创建
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<StateDetector>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("StateDetector");
                    _instance = obj.AddComponent<StateDetector>();
                }
            }
            return _instance;
        }
    }

    //存储所有状态的字典
    private Dictionary<GameState, bool> _states = new Dictionary<GameState, bool>();

    //状态改变事件字典
    private Dictionary<GameState, Action<bool>> _stateEvents = new Dictionary<GameState, Action<bool>>();

    private void Awake()
    {
       if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            //------ 加载存档数据

            InitializeDefaultState();

        }
       else
        {
            Destroy(gameObject);
        }
    }

    //初始化未保存状态的默认值
    private void InitializeDefaultState()
    {
        SetStateIfMissing(GameState.isEatMedicine, false);
        SetStateIfMissing(GameState.isMedicineDestory, false);
        SetStateIfMissing(GameState.isRoomClean, false);
        SetStateIfMissing(GameState.isDoorTouch, false);
    }

    
    /// <summary>
    /// 安全设置状态（仅当状态不存在时）
    /// </summary>
    /// <param name="state">目标状态</param>
    /// <param name="value">默认value</param>
    private void SetStateIfMissing(GameState state, bool value)
    {
        if (!_states.ContainsKey(state))
        {
            _states[state] = value;
        }
    }


    /// <summary>
    /// 设置游戏状态
    /// </summary>
    /// <param name="state">目标状态类型</param>
    /// <param name="value">新状态值</param>
    public void SetState(GameState state, bool value)
    {
        bool currentValue;
        if (_states.TryGetValue(state, out currentValue))
        {
            if (currentValue == value)
                return;
        }

        _states[state] = value;

        //触发改变状态事件
        if (_stateEvents.ContainsKey(state))
        {
            Action<bool> callBack = _stateEvents[state];
            if (callBack != null)
                callBack(value);
        }

        //自动保存数据
        //Save------（）；
    }

    /// <summary>
    /// 获取游戏状态
    /// </summary>
    /// <param name="state">目标状态类型</param>
    /// <returns>当前状态值</returns>
    public bool GetState(GameState state)
    {
        bool value;
        if (_states.TryGetValue(state, out value))
            return value;

        return false;
    }


    /// <summary>
    /// 订阅状态改变事件
    /// </summary>
    /// <param name="state">目标状态</param>
    /// <param name="callback">回调方法</param>
    public void SubscribeToState(GameState state, Action<bool> callback)
    {
        if (_stateEvents.ContainsKey(state))
        {
            _stateEvents[state] += callback;
        }else
        {
            _stateEvents[state] = callback;
        }

        bool currentValue;
        if (_states.TryGetValue(state, out currentValue))
        {
            callback(currentValue);
        }
        else
        {
            callback(false);
        }

    }


    public void UnsubscribeFromState(GameState state, Action<bool> callBack)
    {
        if (_stateEvents.ContainsKey(state))
        {
            _stateEvents[state] -= callBack;
        }
    }




}
