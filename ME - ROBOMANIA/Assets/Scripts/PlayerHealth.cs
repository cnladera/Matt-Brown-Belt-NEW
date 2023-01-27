using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // get references to the Unity Slider we are using for the health bar
    public Slider healthBar;

    // create references for resources loaded in Awake
    private SpriteRenderer roboManSpriteRenderer;
    private Material materialWhite;
    private Material materialDefault;
    private GameObject explosionResource;

    // set the player's health to 100
    private int health = 100;


    // grab RoboMan's sprite renderer and other resources to use later
    void Awake()
    {
        roboManSpriteRenderer = GetComponent<SpriteRenderer>();
        materialDefault = roboManSpriteRenderer.material;
        materialWhite = Resources.Load("WhiteFlash") as Material;
        explosionResource = Resources.Load("OrangeExplosion") as GameObject;
    }


    // if the player is colliding with an enemy
    // then the player takes damage each frame
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy")
        {
            TakeDamage();
        }
    }
    
    // TakeDamage is called each frame that RoboMan is colliding with an Enemy game object
    private void TakeDamage()
    {
        // first, remove one point of health from RoboMan
        health--;
        // update the health bar's value to the new health value to move the slider
        healthBar.value = health;

        // make RoboMan flash when he takes damage
        roboManSpriteRenderer.material = materialWhite;

        // ask Unity to run (invoke) the StopFlashing function after .1 second
        // this function will make RoboMan stop flashing by setting his material
        // to the default RoboMan material
        Invoke("StopFlashing", .1f);

        // create a new explosion game object
        GameObject explosion = Instantiate(explosionResource);

        // move the explosion game object to the location of RoboMan
        explosion.transform.position = transform.position;

        // if RoboMan's health reaches zero or lower, self destruct!
        if (health <= 0)
        {
            SelfDestruct();
        }

        
    }

    // This is called .1 seconds after TakeDamage (because it is Invoked)
    // this will set RoboMan's sprite material to the default that was
    // set up in Awake
    void StopFlashing()
    {
        roboManSpriteRenderer.material = materialDefault;
    }

    // If RoboMan runs out of health, then SelfDestruct is called
    private void SelfDestruct()
    {
        // deactivate the RoboMan game object to remove him from the scene
        gameObject.SetActive(false);

        // with RoboMan gone, wait 2 seconds before running the 
        // RestartScene function
        Invoke("RestartScene", 2f);
    }

    // Ask Unity's SceneManager to re-load the Robomania scene
    void RestartScene()
    {
        SceneManager.LoadScene("Robomania");
    }
}
