using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    // destroy the object exactly 2 seconds after it is created
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    // destroy the object if it collides with the ground
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
