using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_itemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    private Button slotButton;

    public InventoryItem item;

    private void Awake()
    {
        slotButton = GetComponent<Button>();
        slotButton.onClick.AddListener(OpenPanel);
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

        }
    }


    public void OpenPanel()
    {
        GameRoot.GetInstacne().GetPanel(item.data.linkedPanelType);
        
    }



}
