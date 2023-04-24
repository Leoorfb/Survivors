using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;

    Player player;
    PlayerLevel playerLevel;

    [SerializeField] Item itemTest;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerLevel = GetComponent<PlayerLevel>();
    }

    private void Start()
    {
        if (itemTest != null)
        {
            Equip(itemTest);
        }
    }

    public void Equip(Item item)
    {
        if (items == null) items = new List<Item>();

        Item newItem = new Item();
        newItem.Init(item.itemName, item.upgrades);
        newItem.stats.Sum(item.stats);
        newItem.upgrades = item.upgrades;

        items.Add(newItem);
        newItem.Equip(player);
        playerLevel.AddUpgradesIntoTheListOfUpgrades(newItem.GetFirstUpgrade());
    }

    internal void UpgradeItem(UpgradesData upgradesData)
    {
        Item itemToUpgrade = items.Find(id => id.itemName == upgradesData.item.itemName);
        itemToUpgrade.Unequip(player);
        itemToUpgrade.stats.Sum(upgradesData.itemStats);
        itemToUpgrade.Equip(player);
        playerLevel.AddUpgradesIntoTheListOfUpgrades(itemToUpgrade.GetNextUpgradeAndLevelUp());
    }
}
