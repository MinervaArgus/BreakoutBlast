using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using TMPro;

/*
 * Jackson Nevins
 * SpeedSelection.cs
 * Controls the Scene that asks what difficulty the user wants
 */
public class SpeedSelection : MonoBehaviour
{
    public Button easyBtn;
    public Button mediumBtn;
    public Button hardBtn;
    // Start is called before the first frame update
    void Start()
    {
        easyBtn.onClick.AddListener(SetEasy);
        mediumBtn.onClick.AddListener(SetMedium);
        hardBtn.onClick.AddListener(SetHard);
    }

    public void SetEasy()
    {
        UpdateDifficulty("Easy");
        SceneManager.LoadScene("Easy");
    }

    private void UpdateDifficulty(string difficulty)
    {
        string connectionString = "URI=file:HighScores.db";
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                if (difficulty.Equals("Easy"))
                {
                    dbCmd.CommandText = "INSERT INTO HighScores (PlayerName, Difficulty, Score) VALUES (@PlayerName, 'Easy', 0)";
                    IDbDataParameter playerNameParam = dbCmd.CreateParameter();
                    playerNameParam.ParameterName = "@PlayerName";
                    playerNameParam.Value = playerName;
                    dbCmd.Parameters.Add(playerNameParam);

                    dbCmd.ExecuteNonQuery();
                }
                else if (difficulty.Equals("Medium"))
                {
                    dbCmd.CommandText = "INSERT INTO HighScores (PlayerName, Difficulty, Score) VALUES (@PlayerName, 'Medium', 0)";
                    IDbDataParameter playerNameParam = dbCmd.CreateParameter();
                    playerNameParam.ParameterName = "@PlayerName";
                    playerNameParam.Value = playerName;
                    dbCmd.Parameters.Add(playerNameParam);

                    dbCmd.ExecuteNonQuery();
                }
                else if (difficulty.Equals("Hard"))
                {
                    dbCmd.CommandText = "INSERT INTO HighScores (PlayerName, Difficulty, Score) VALUES (@PlayerName, 'Hard', 0)";
                    IDbDataParameter playerNameParam = dbCmd.CreateParameter();
                    playerNameParam.ParameterName = "@PlayerName";
                    playerNameParam.Value = playerName;
                    dbCmd.Parameters.Add(playerNameParam);

                    dbCmd.ExecuteNonQuery();
                }
                else
                {
                    Debug.LogWarning("Not a Difficulty");
                }

            }
            dbConnection.Close();
        }
    }

    public void SetMedium()
    {
        SceneManager.LoadScene("Medium");
    }

    public void SetHard()
    {
        SceneManager.LoadScene("Hard");
    }
}
