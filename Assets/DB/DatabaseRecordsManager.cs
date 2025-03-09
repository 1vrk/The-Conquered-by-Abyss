using System;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseRecordsManager : MonoBehaviour
{
    private string dbPath;

    private void Awake()
    {
        dbPath = "URI=file:" + Application.dataPath + "/DB/conquered.db";
        Debug.Log(dbPath);
    }

    public void AddPlayerRecords(string login)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Records (login, score) VALUES (@login, @score)";
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@score", 0); 
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdatePlayerRecords(string login, int score_amount)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Records SET score = @score WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@score", score_amount);
                command.ExecuteNonQuery();
            }
        }
    }

    public int LoadPlayerRecordsScore(string login)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT score FROM Records WHERE login = @login"; 
                command.Parameters.AddWithValue("@login", login);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                    {
                        Debug.LogWarning("No data found for username: " + login);
                        return 0;
                    }
                }
            }
        }
    }

    public void ResetPlayerRecords(string login)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Records SET score = @score WHERE login = @login";
                command.Parameters.AddWithValue("@deaths", 0);
                command.Parameters.AddWithValue("@login", login);
                command.ExecuteNonQuery();
            }
        }
    }

   
   
}