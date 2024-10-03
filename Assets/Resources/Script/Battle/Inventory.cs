using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
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

public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public int Quantity;

    public virtual void Use()
    {
        // This method will be overridden in each subclass
    }
}

public class Item_Healer : Item
{
    private void Awake()
    {
        Name = "회복";
        Description = "당신의 목숨을 조금 더 늘려준다.";
        Quantity = 5;
    }

    public override void Use()
    {
        BattlePlayingSystem.Instance.Player.HP += 15;
    }
}

public class Item_Buffer : Item
{
    private void Awake()
    {
        Name = "향상";
        Description = "당신의 공격이 잠깐 동안 더 강해진다.";
        Quantity = 5;
    }

    public override void Use()
    {
        BattlePlayingSystem.Instance.BufferFlag = true;
    }
}

public class Item_Shielder : Item
{
    private void Awake()
    {
        Name = "방어";
        Description = "당신이 받는 피해를 일부 반사시킨다.";
        Quantity = 5;
    }

    public override void Use()
    {
        BattlePlayingSystem.Instance.ShielderFlag = true;
    }
}