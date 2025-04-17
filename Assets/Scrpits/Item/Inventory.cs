using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> inventoryItems;    //物品列表  存储
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;     //物品字典  查找

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    private UI_itemSlot[] itemSlot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
       
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        itemSlot = inventorySlotParent.GetComponentsInChildren<UI_itemSlot>();   //获取槽位
        UpdateSlotUI();
    }


    // 更新所有UI槽位的显示
    private void UpdateSlotUI()
    {
        if (inventoryItems.Count > 0)
        {

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                 itemSlot[i].UpdateSlot(inventoryItems[i]);   
            }
        }else
        {
            for (int i = 0; i <= 10; i++)
            {
                itemSlot[i].ClearSlot();
            }
        }
    }


    /// <summary>
    /// 添加物品到库存
    /// </summary>
    /// <param name="_item"> 要添加的物品数据</param>
    public void AddItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();   // 存在 数量加一
        }else
        {              // 不存在 创建新库存
            InventoryItem newItem = new InventoryItem(_item);
            inventoryItems.Add(newItem);
            inventoryDictionary.Add(_item, newItem);

        }
        UpdateSlotUI();
    }


    /// <summary>
    /// 从库存移除物品
    /// </summary>
    /// <param name="_item">要移除的物品</param>
    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
                value.RemoveStack();
        }
        UpdateSlotUI();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    ItemData newItem = inventoryItems[inventoryItems.Count - 1].data;
        //    RemoveItem(newItem);
        //}
        //string result = string.Join("" ,inventoryItems);
        //Debug.Log("inventoryItems count" + inventoryItems.Count);
        //Debug.Log("InventoryItems:" + result);

    }

}
