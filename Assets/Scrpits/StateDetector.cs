using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


//��Ϸ״̬ö��
public enum GameState
{
    isEatMedicine,  //����Ƿ��ҩ
    isMedicineDestory,    //ҩ���Ƿ�����
    isRoomClean,      //�����Ƿ�����
    isDoorTouch       //�����Ƿ񱻶�
}

public class StateDetector : MonoBehaviour
{
    private static StateDetector _instance;

    public static StateDetector Instance    //ȫ�ַ��ʵ�

    {
        get
        {   // ���ʵ�������ھͲ��һ򴴽�
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

    //�洢����״̬���ֵ�
    private Dictionary<GameState, bool> _states = new Dictionary<GameState, bool>();

    //״̬�ı��¼��ֵ�
    private Dictionary<GameState, Action<bool>> _stateEvents = new Dictionary<GameState, Action<bool>>();

    private void Awake()
    {
       if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            //------ ���ش浵����

            InitializeDefaultState();

        }
       else
        {
            Destroy(gameObject);
        }
    }

    //��ʼ��δ����״̬��Ĭ��ֵ
    private void InitializeDefaultState()
    {
        SetStateIfMissing(GameState.isEatMedicine, false);
        SetStateIfMissing(GameState.isMedicineDestory, false);
        SetStateIfMissing(GameState.isRoomClean, false);
        SetStateIfMissing(GameState.isDoorTouch, false);
    }

    
    /// <summary>
    /// ��ȫ����״̬������״̬������ʱ��
    /// </summary>
    /// <param name="state">Ŀ��״̬</param>
    /// <param name="value">Ĭ��value</param>
    private void SetStateIfMissing(GameState state, bool value)
    {
        if (!_states.ContainsKey(state))
        {
            _states[state] = value;
        }
    }


    /// <summary>
    /// ������Ϸ״̬
    /// </summary>
    /// <param name="state">Ŀ��״̬����</param>
    /// <param name="value">��״ֵ̬</param>
    public void SetState(GameState state, bool value)
    {
        bool currentValue;
        if (_states.TryGetValue(state, out currentValue))
        {
            if (currentValue == value)
                return;
        }

        _states[state] = value;

        //�����ı�״̬�¼�
        if (_stateEvents.ContainsKey(state))
        {
            Action<bool> callBack = _stateEvents[state];
            if (callBack != null)
                callBack(value);
        }

        //�Զ���������
        //Save------������
    }

    /// <summary>
    /// ��ȡ��Ϸ״̬
    /// </summary>
    /// <param name="state">Ŀ��״̬����</param>
    /// <returns>��ǰ״ֵ̬</returns>
    public bool GetState(GameState state)
    {
        bool value;
        if (_states.TryGetValue(state, out value))
            return value;

        return false;
    }


    /// <summary>
    /// ����״̬�ı��¼�
    /// </summary>
    /// <param name="state">Ŀ��״̬</param>
    /// <param name="callback">�ص�����</param>
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
