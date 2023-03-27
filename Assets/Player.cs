using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // player's movement speed

    private Rigidbody rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
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
}
