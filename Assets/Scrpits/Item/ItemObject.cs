using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private ItemData itemData;
    


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Trigger()
    {
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }

}
