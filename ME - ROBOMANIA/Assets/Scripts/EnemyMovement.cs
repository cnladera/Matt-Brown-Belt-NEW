using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    //public float speed;
    public float yForce;
    public float xForce;
    public float xDirection;

    private Rigidbody2D enemyRigidBody;


    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector2 jumpForce = new Vector2(xForce * xDirection, yForce);
            enemyRigidBody.AddForce(jumpForce);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Vector2 jumpForce = new Vector2(xForce * -xDirection, yForce);
            enemyRigidBody.AddForce(jumpForce);
        }
        /*
        if (collision.gameObject.tag == "Player")
        {
            Vector2 bounceForce = new Vector2(xForce * xDirection * 3, yForce * -2);
            collision.otherRigidbody.AddForce(bounceForce);
        }
        */
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        /*
         * Original Contents
        if (transform.position.x <= -8 || transform.position.x >= 8)
        {
            speed = speed * -1;
        }

        float newXPosition = transform.position.x + speed * Time.deltaTime;
        float newYPosition = transform.position.y;
        Vector2 newPosition = new Vector2(newXPosition, newYPosition);
        transform.position = newPosition;
        */

        if (transform.position.x <= -8)
        {
            xDirection = 1;
            enemyRigidBody.AddForce(Vector2.right * xForce);
        }

        if (transform.position.x >= 8)
        {
            xDirection = -1;
            enemyRigidBody.AddForce(Vector2.left * xForce);
        }

        if (transform.position.y >= 4.5)
        {
            enemyRigidBody.AddForce(Vector2.down * yForce);
        }

    }
}
