using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRadius : MonoBehaviour
{
    [SerializeField] private float radius = 2.5f;
    private Player player;

    private void Start()
    {
        player = GameManager.instance.playerTransform.GetComponent<Player>();
    }

    public void SetRadius(float nRadius)
    {
        if (nRadius > 0)
            radius = nRadius;
    }
    public float GetRadius()
    {
        return radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.GetComponent<Exp>().Collect(player);
        }
    }
}
