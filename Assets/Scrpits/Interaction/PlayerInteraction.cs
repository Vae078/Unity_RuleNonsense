using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �������ҽ����ű�
/// �������й�����ҽ����Ĺ��ܶ���������
/// ������ ���� �Ի� ʲô��
/// </summary>
public class PlayerInteraction: MonoBehaviour
{
    public float interactionDistance = 3f;  //��������
    public Camera mainCamera;
    public TextMeshProUGUI textCompent;
    public Image backGround;


    //-----------------״̬��------------------
    private PlayerInteractionState currentState;
    public bool isChangeUI;
    public InteractionIdleState interactionIdle;
    public DetectingDoorState detectingDoor;
    public DetectingCupState detectingCup;
    public holdingCupState holdingCup;

    //---------------UI�ͳ���--------------------
    private UIManager UIManager;
    public UIManager UIManager_Root { get => UIManager; }

    private SceneControl SceneControl;
    public SceneControl Scemetrol_Root { get => SceneControl; }



    private void Awake()
    {
        interactionIdle = new InteractionIdleState(this);
        detectingDoor = new DetectingDoorState(this);
        detectingCup = new DetectingCupState(this);
        holdingCup = new holdingCupState(this);

        SceneControl = new SceneControl();

    }


    private void Start()
    {
        

        mainCamera = Camera.main;

        backGround = GetComponentInChildren<Image>();
        backGround.enabled = false;

        textCompent = GetComponentInChildren<TextMeshProUGUI>();
        textCompent.text = null;
        
        currentState = interactionIdle;
        currentState.Enter();
        
    }



    private void Update()
    {
        currentState.Update();
        
    }

  

    public holdingCupState GetHoldingCupState(ControlCup cup)
    {
        holdingCup.SetCup(cup);
        return holdingCup;
    }


    public void ChangeState(PlayerInteractionState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }


    //����UGUI��UI��ʾ��ʾ�����Ӻ���ʧ
    public void PrintUI(string write)
    {
        HideUI();
        textCompent.text = write;
        textCompent.gameObject.SetActive(true);
        backGround.enabled = true;

    }

    public void HideUI()
    {
        backGround.enabled = false;
        textCompent.text = null;
        textCompent.gameObject.SetActive(false);
    }


    public bool RayDetect(int layer)
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {          
            if (layer == hit.collider.gameObject.layer)
            {
                return true;
            }else
            {
                return false;
            }
            
        }
        return false;
    }



    /// <summary>
    /// �������������Ļ����λ�÷���һ�����ߣ�����Ƿ�����˳����е����塣
    /// </summary>
    /// <param name="hit"> ���ڴ洢������ײ��Ϣ�Ľṹ�壬
    /// ��������ײ�㡢��ײ�������ϸ��Ϣ��������߻������壬�ò����ᱻ���;
    /// ���δ���У��ò�����������Ϣ����Ч�ġ�</param>
    /// <returns>���������ָ���Ľ��������ڻ����˳����е����壬���� true��
    /// ���δ�����κ����壬���� false��</returns>

    public bool Hit(out RaycastHit hit)
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
