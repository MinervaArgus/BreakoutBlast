using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Jackson Nevins
 * BrickController.cs
 * Controls the functionality of the Brick prefab in Unity
 */
public class BrickController : MonoBehaviour
{
    public int maxHits = 1;
    private int currentHits;
    private HUDManager hudManager;

    void Start()
    {
        hudManager = FindObjectOfType<HUDManager>();
        currentHits = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // Calculate the bounce direction based on the brick's normal and ball's velocity
            Vector3 newDirection = Vector3.Reflect(ballRigidbody.velocity, collision.contacts[0].normal);

            // Ensure the ball always moves downward after hitting the brick
            if (newDirection.y > -0.1f)
            {
                newDirection.y = -Mathf.Abs(newDirection.y) - 10f;
            }

            // Set the new velocity
            ballRigidbody.velocity = newDirection.normalized * ballRigidbody.velocity.magnitude;

            currentHits++;

            if (currentHits >= maxHits)
            {
                hudManager.IncreaseScore(1);
                Destroy(gameObject);
            }
        }
    }

}