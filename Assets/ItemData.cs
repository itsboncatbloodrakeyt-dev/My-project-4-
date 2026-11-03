using System;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    General,
    Key,
    Consumable,
    Equipment
}
[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public int itemPrice;

}


