using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Moving : NetworkBehaviour
{
    void Update()
    {
        if (hasAuthority)
        {
            float translation = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            float speed = 5f * Time.deltaTime;
            translation *= Time.deltaTime;
            transform.Translate(new Vector3(h * speed, 0, translation));
        }
    }
}
