using UnityEngine;
using System.Data;
using System.Collections.Generic;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class SQLiteManager : MonoBehaviour
{
    private string dbUri;

    void Start()
    {
        CreateAndOpenDB();
    }

    private IDbConnection CreateAndOpenDB()
    {
        dbUri = "URI=file:Scores.sqlite"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, hits INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        return dbConnection;
    }

    public void SaveScore(string name, int score)
    {
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "INSERT INTO scores (name, score) VALUES (@name, @score)";
        dbCommand.Parameters.Add(new SqliteParameter("@name", name));
        dbCommand.Parameters.Add(new SqliteParameter("@score", score));
        dbCommand.ExecuteNonQuery();

        dbConnection.Close();
    }
}
