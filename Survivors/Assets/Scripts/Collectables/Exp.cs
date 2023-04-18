using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField] int expAmount = 1;

    public void Collect(Player player)
    {
        player.GetComponent<PlayerLevel>().AddExp(expAmount);
        Disable();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
