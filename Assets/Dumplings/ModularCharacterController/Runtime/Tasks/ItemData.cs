using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Quest,
}

[System.Serializable]
public class ItemData
{
    #region Private Variables
    private int id;
    [SerializeField, Tooltip("Name of the Item")] private string name;
    [SerializeField, Tooltip("Description of the Item")] private string description;
    [SerializeField, Tooltip("How valuable is the Item")] private int value;
    [SerializeField, Tooltip("The amount of Item")] private int amount;
    [SerializeField, Tooltip("The icon pertaining to the Item")] private Sprite icon;
    [SerializeField, Tooltip("The mesh of the Item")] private GameObject mesh;
    [SerializeField, Tooltip("What type of Item is the current Item")] private ItemType type;
    #endregion

    #region Public Props
    public int ID //allowing id to be modified outside script
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public GameObject Mesh
    {
        get { return mesh; }
        set { mesh = value; }
    }

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }
    #endregion

    public static ItemData CreateItem(int itemID)
    {
        string description = "";
        string name = "";
        int value = 0;
        int amount = 0;
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Quest;

        switch (itemID) 
        {
            case 0:
                name = "Quest";
                description = "What task does the player need to complete?";
                value = 1;
                amount = 1;
                icon = "";
                mesh = "";
                type = ItemType.Quest;
                break;
        }

        ItemData temp = new ItemData
        {
            ID = itemID,
            Name = name,
            Description = description,
            Value = value,
            Amount = amount,
            Type = type,
            Icon = Resources.Load("Icon/" + icon) as Sprite,
            Mesh = Resources.Load("Mesh/" + mesh) as GameObject,
        };
        return temp;
    }
}
