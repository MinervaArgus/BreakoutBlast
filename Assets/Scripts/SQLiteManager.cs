using UnityEngine;
using System.Data;
using System.Collections.Generic;
using UnityEngine.UI;
using Mono.Data.Sqlite;

/*
 * Jackson Nevins
 * SQLiteManager.cs
 * Controls the functionality of the DB (i.e. connecting and opening the db)
 */
public class SQLiteManager : MonoBehaviour
{

    void Start()
    {
        IDbConnection dbConnection = CreateAndOpenDB();
        dbConnection.Close();
    }

    private IDbConnection CreateAndOpenDB()
    {
        string dbUri = "URI=file:HighScores.db";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "DROP TABLE HighScores";
        dbCommandCreateTable.ExecuteNonQuery();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HighScores ( ID INTEGER PRIMARY KEY AUTOINCREMENT, PlayerName TEXT NOT NULL, Difficulty TEXT NOT NULL, Score INTEGER NOT NULL)";
        dbCommandCreateTable.ExecuteNonQuery();

        return dbConnection;
    }

    
}
