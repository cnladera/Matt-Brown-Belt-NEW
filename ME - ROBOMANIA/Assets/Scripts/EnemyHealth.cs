using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // get references to the Unity Slider we are using for the health bar
    public Slider healthBar;

    // create references for resources loaded in Awake
    private SpriteRenderer enemySpriteRenderer;
    private Material materialWhite;
    private Material materialDefault;
    private GameObject explosionResource;

    // set the enemy's health to 100
    private int health = 100;

    // grab the enemy's sprite renderer and other resources to use later
    void Awake()
    {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        materialDefault = enemySpriteRenderer.material;
        materialWhite = Resources.Load("WhiteFlash") as Material;
        explosionResource = Resources.Load("Explosion") as GameObject;
    }

    // we want to detect when a bullet is touching an enemy
    // but we don't want the bullets to apply a physical force on the enemy
    // so we use OnTrigger instead of OnCollision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the other game object is a bullet
        if (other.CompareTag("Bullet"))
        {
            // Take damage
            TakeDamage();
            // and destroy the bullet
            Destroy(other.gameObject);
        }
    }

    // TakeDamage is called each once each time a bullet is colliding with an Enemy game object
    private void TakeDamage()
    {
        // remove four points of health from the enemy
        health -= 4;
        // update the health bar's value to the new health value to move the slider
        healthBar.value = health;

        // make the enemy flash when it takes damage
        enemySpriteRenderer.material = materialWhite;

        // ask Unity to run (invoke) the StopFlashing function after .1 second
        // this function will make the enemy stop flashing by setting its material
        // to the default enemy material
        Invoke("StopFlashing", .1f);

        // create a new explosion object
        GameObject explosion = Instantiate(explosionResource);

        // move the explosion game object to the location of the enemy
        explosion.transform.position = transform.position;

        // if the enemy's health reaches zero or lower, self destruct!
        if (health <= 0)
        {
            SelfDestruct();
        }
    }

    // This is called .1 seconds after TakeDamage (because it is Invoked)
    // this will set the enemy's sprite material to the default that was
    // set up in Awake
    void StopFlashing()
    {
        enemySpriteRenderer.material = materialDefault;
    }

    // If the enemy runs out of health, then SelfDestruct is called
    private void SelfDestruct()
    {
        // disable the enemy's health bar object
        healthBar.gameObject.SetActive(false);

        // self destruct the enemy!
        Destroy(gameObject);
    }
}
