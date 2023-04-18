using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text upgradeName;
    [SerializeField] TMP_Text upgradeDescription;

    public void SetData(UpgradesData upgradesData)
    {
        icon.sprite = upgradesData.icon;
        upgradeName.text = upgradesData.weaponData.weaponName;
        upgradeDescription.text = upgradesData.UpgradeDescription;
    }

    internal void Clean()
    {
        icon.sprite = null;
        upgradeName.text = "";
        upgradeDescription.text = "";
    }
}
