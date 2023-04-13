/*
 * BreakoutBlast by Jackson Nevins
 * Remake of Brick breaker with 3d assets and a locked camera perspective. Connects to MariaDB to keep track of scores and who obtained the score.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // player's movement speed
    public float bounceForce = 10f;
    private Rigidbody rb; // Ridgidbody connector
    private Camera mainCamera; // Camera connector

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //connect the rb to the actual RidgidBody we want
        mainCamera = FindObjectOfType<Camera>(); //connect the camera connector to the Main Camera
    }

    private void FixedUpdate() // Fixed update runs every frame and ignores the frame rate
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f); // only allows horizontal movement for the player
        rb.velocity = movement * moveSpeed;

        // Clamp the player's position to the edges of the screen
        if (mainCamera != null)
        {
            float halfPlayerWidth = transform.localScale.x / 2f;
            float screenLeftEdge = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + halfPlayerWidth;
            float screenRightEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - halfPlayerWidth;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, screenLeftEdge, screenRightEdge), transform.position.y, transform.position.z);
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // Calculate the bounce direction
            Vector3 bounceDirection = (collision.transform.position - transform.position).normalized;

            // Ensure the ball always moves upward
            bounceDirection.y = Mathf.Abs(bounceDirection.y);

            // Apply the bounce force
            ballRigidbody.velocity = bounceDirection * bounceForce;
        }
    }
    */
}
