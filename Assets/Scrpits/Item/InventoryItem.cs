using System;


[Serializable]
public class InventoryItem
{
    public ItemData data;    // 物品信息
    public int stackSize;   

    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        AddStack();
    }

    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;


}
