using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 这个是玩家交互脚本
/// 把所有有关于玩家交互的功能都放在这里
/// 开关门 背包 对话 什么的
/// </summary>
public class PlayerInteraction: MonoBehaviour
{
    public float interactionDistance = 3f;  //最大检测距离
    public Camera mainCamera;
    public TextMeshProUGUI textCompent;
    public Image backGround;


    //-----------------状态机------------------
    private PlayerInteractionState currentState;
    public bool isChangeUI;
    public InteractionIdleState interactionIdle;
    public DetectingDoorState detectingDoor;
    public DetectingCupState detectingCup;
    public holdingCupState holdingCup;

    //---------------UI和场景--------------------
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


    //控制UGUI，UI提示显示两秒钟后消失
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
    /// 从主摄像机的屏幕中心位置发射一条射线，检测是否击中了场景中的物体。
    /// </summary>
    /// <param name="hit"> 用于存储射线碰撞信息的结构体，
    /// 包含了碰撞点、碰撞物体等详细信息。如果射线击中物体，该参数会被填充;
    /// 如果未击中，该参数包含的信息是无效的。</param>
    /// <returns>如果射线在指定的交互距离内击中了场景中的物体，返回 true；
    /// 如果未击中任何物体，返回 false。</returns>

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
