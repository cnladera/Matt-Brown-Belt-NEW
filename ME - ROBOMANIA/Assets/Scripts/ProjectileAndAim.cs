using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAndAim : MonoBehaviour
{
    // get references to the crosshairs, the blaster, and the bullet prefab
    public GameObject crosshairs;
    public GameObject protonBlaster;
    public GameObject protonPrefab;
    
    // Get the bullet speed from the inspector
    public float protonSpeed = 20.0f;

    // store a reference to the main camera from Start
    private Camera cam;
    

    void Start()
    {
        // remove the mouse cursor
        Cursor.visible = false;
        // store the main camera
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // ask the camera to project the mouse cursor to a world point
        Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);

        // move the crosshairs to the mouse's x and y positions
        crosshairs.transform.position = new Vector3(target.x, target.y, 0);

        // if the user clicks the left mouse button, fire the blaster
        if (Input.GetMouseButtonDown(0))
        {
            FireBlaster();
        }
    }

    void FireBlaster()
    {
        // get a vector pointing between the player and the crosshair
        Vector3 playerToCrosshair = crosshairs.transform.position - protonBlaster.transform.position;

        // convert the difference to be a unit vector with magnitude 1
        // this is because we are only worried about the direction
        // and we don't want how far away from the player the click was to affect speed
        playerToCrosshair.Normalize();

        // to find the angle of our direction vector, we need to do trig!
        // tan = opposite / adjacent
        //
        //           crosshair
        //         / | 
        //        /  |
        //   player__| 
        //   ^  
        //   |__ to find this angle, we need the opposite (y) and adjacent (x) sides
        //
        // so grab the y and x components of the playerToCrosshair vector and 
        // calculate the arctan
        // note: we use Atan2 to pass in two values and not just a ratio
        // finally, convert it to degrees because math is in radians but Unity world is in degrees
        float rotationZ = Mathf.Atan2(playerToCrosshair.y, playerToCrosshair.x) * Mathf.Rad2Deg;

        // move the proton blaster to the correct angle in the z direction
        protonBlaster.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        // finally create a projectile
        GameObject projectile = Instantiate(protonPrefab) as GameObject;

        // set the initial position to be the location of the blaster
        projectile.transform.position = protonBlaster.transform.position;

        // set the initial rotation to match the blaster's rotation
        projectile.transform.rotation = protonBlaster.transform.rotation;

        // set the projectile's velocity to be the unit vector pointing
        // from the player to the crosshair times the velocity.
        projectile.GetComponent<Rigidbody2D>().velocity = playerToCrosshair * protonSpeed;
    }
}
