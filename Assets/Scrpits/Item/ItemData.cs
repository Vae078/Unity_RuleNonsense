using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Item Data",menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public string linkedPanelType;

}
