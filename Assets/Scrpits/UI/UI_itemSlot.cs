using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_itemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    private Button slotButton;

    public InventoryItem item;

    private void Awake()
    {
        slotButton = GetComponent<Button>();
        slotButton.onClick.AddListener(UseItem);    //左键单击事件
    }

    private void Start()
    {
        //添加Evnent Trigger
        EventTrigger trigger = GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry rightClickEntry = new EventTrigger.Entry();
        rightClickEntry.eventID = EventTriggerType.PointerClick;
        rightClickEntry.callback.AddListener((data) => OnPointerClick((PointerEventData)data));
        trigger.triggers.Add(rightClickEntry);

    }

    // 右键单击删除物品
    private void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("右键单击");
            if (item != null)
            {
                Inventory.instance.RemoveItem(item.data);
            }
        }
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemImage.color = Color.white;
        if (item != null)
        {
            itemImage.sprite = item.data.sprite;
            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }else
        {
            itemImage.sprite = null;
            itemText.text = "";
        }
    }

    public void ClearSlot()
    {
        item = null;             // 清空引用
        itemImage.sprite = null; // 清空图片
        itemImage.color = Color.clear; // 设置为透明
        itemText.text = "";      // 清空数量文本
    }



    public void UseItem()
    {
        GameRoot.GetInstacne().GetPanel(item.data.linkedPanelType);   
    }
}
