using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHp = 100;
    [SerializeField] int hp = 100;
    public bool isAlive = true;

    [SerializeField] TextMeshProUGUI hpText;

    WeaponsManager weaponsManager;


    private void Awake()
    {
        weaponsManager = GetComponent<WeaponsManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHpText();
    }

    public void TakeDamage(int damage)
    {
        if (damage != 0)
        {
            hp -= damage;
            //Debug.Log(damage + " damage taken");
            
            if (hp <= 0)
            {
                isAlive = false;
            }
            UpdateHpText();
        }
        return;
    }

    void UpdateHpText()
    {
        hpText.text = "Player  HP: " + hp + "/" + maxHp;
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.tag == "Enemy")
        {
            //Debug.Log(name + " colidiu com " + collision.gameObject.name);
            TakeDamage(collided.GetComponent<EnemyChase>().Attack());
        }
    }
}
