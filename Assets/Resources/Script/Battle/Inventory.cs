using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Use()
    {
        // TODO: Define what happens when the item is used
    }
}

public class Inventory : MonoBehaviour
{
    public List<Item> Items { get; set; }

    public Inventory()
    {
        Items = new List<Item>();
    }
    
    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }
}