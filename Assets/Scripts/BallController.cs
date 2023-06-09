using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class BallController : MonoBehaviour
{
    public float speed = 80f;
    private Rigidbody rb;
    private Vector3 screenBounds;
    private Camera mainCamera;
    public HUDManager hudManager;
    private string connectionString = "URI=file:HighScores.db";

    private void Start()
    {
        hudManager = FindObjectOfType<HUDManager>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        screenBounds = GetScreenBounds();
        LaunchBall();
    }

    private void LaunchBall()
    {
        // Make the initial angle of launch random
        float initialAngle = Random.Range(30f, 150f) * Mathf.Deg2Rad;
        Vector3 intialVelocity = new Vector3(Mathf.Cos(initialAngle), Mathf.Sin(initialAngle), 0f) * speed;
        rb.velocity = intialVelocity;
    }

    private void Update()
    {
        BounceOffScreenEdges();
        CheckLosingCondition();
    }

    private void BounceOffScreenEdges()
    {
        Vector3 ballPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (ballPosition.x <= 0 || ballPosition.x >= 1)
        {
            // Reflect the ball's velocity horizontally
            rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, rb.velocity.z);
        }

        if (ballPosition.y >= 1)
        {
            // Reflect the ball's velocity vertically
            rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);
        }
    }

    private void CheckLosingCondition()
    {
        Vector3 ballPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (ballPosition.y <= 0)
        {
            int playerScore = hudManager.GetScore();
            string playerName = GetPlayerName();
            UpdateHighScore(playerScore, playerName);
            // Player loses, reset the game or implement other game over logic
            SceneManager.LoadScene("GameOver");
        }
    }

    // Save score to DB
    private void UpdateHighScore(int newScore, string playerName)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = "UPDATE HighScores SET Score = @Score WHERE PlayerName = @PlayerName";

                IDbDataParameter scoreParam = dbCmd.CreateParameter();
                scoreParam.ParameterName = "@Score";
                scoreParam.Value = newScore;
                dbCmd.Parameters.Add(scoreParam);

                IDbDataParameter playerNameParam = dbCmd.CreateParameter();
                playerNameParam.ParameterName = "@PlayerName";
                playerNameParam.Value = playerName;
                dbCmd.Parameters.Add(playerNameParam);

                dbCmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
    }

    //Get player name from PlayerPrefs
    private string GetPlayerName()
    {
        return PlayerPrefs.GetString("PlayerName", "Unknown");
    }

    private void ResetBall()
    {
        // Reset ball position to the center of the screen
        transform.position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane + 1f));
        LaunchBall();
    }

    private Vector3 GetScreenBounds()
    {
        float distance = mainCamera.nearClipPlane + 1f;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        return topRight - bottomLeft;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Brick"))
        {
            // Calculate the reflection direction based on the platform's normal and ball's velocity
            Vector3 newDirection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);

            // Add some force to ensure ball actually bounces off player
            newDirection += Vector3.up * 10f;

            //Ensure the ball is launched upward
            if (newDirection.y < 0)
            {
                newDirection.y = -newDirection.y; //flips the negative direction to an upwards one
            }

            // Set new velocity
            rb.velocity = newDirection.normalized * speed;
        }
    }


}
