using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Item Data",menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;         //物品名称
    public Sprite sprite;           //物品图片
    public string linkedPanelType;      //单击打开的界面（如果是规则）
}
