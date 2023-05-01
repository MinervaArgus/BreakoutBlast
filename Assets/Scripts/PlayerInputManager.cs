using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInputManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public Button submitButton;

    private string connectionString = "URI=file:HighScores.db";

    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(SubmitName);
    }

    private void SubmitName()
    {
        string playerName = playerNameInput.text;
        if (string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();

            SavePlayerName(playerName);
            SceneManager.LoadScene("Easy");
        }
    }

    private void SavePlayerName(string playerName)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = "INSERT INTO PlayerScores (PlayerName, Score) VALUES (@PlayerName, 0)";

                IDbDataParameter playerNameParam = dbCmd.CreateParameter();
                playerNameParam.ParameterName = "@PlayerName";
                playerNameParam.Value = playerName;
                dbCmd.Parameters.Add(playerNameParam);

                dbCmd.ExecuteNonQuery();
            }
            dbConnection.Close();
        }
    }

}
