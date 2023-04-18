using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;

    [SerializeField] float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive) return; // remover depois q a função de morrer estiver pronta

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
