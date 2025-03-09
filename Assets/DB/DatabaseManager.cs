using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Collections.Generic;


public class Users
{
    public string login { get; set; }
    public string password { get; set; }

    public Users(string login, string password)
    {
        this.login = login;
        this.password = password;
    }
}

public class DatabaseManager : MonoBehaviour
{
    private SqliteConnection db_connection;
    public string db_path;
    private List<Users> users = new List<Users>();

    private void Awake()
    {
        db_path = Application.dataPath + "/DB/conquered.db";
        //db_path = Application.persistentDataPath + "/testDB.db"; //на показе заменить везде
        Debug.Log("Database path: " + db_path);
        InitializeDatabase();
        SetConnection();
    }


    public void SetConnection()
    {
        if (!File.Exists(db_path))
        {
            Debug.LogError("Database file does not exist: " + db_path);
            return;
        }

        try
        {
            db_connection = new SqliteConnection("Data Source=" + db_path);
            db_connection.Open();

            if (db_connection.State == ConnectionState.Open)
            {
                SqliteCommand cmd = new SqliteCommand("SELECT * FROM Users", db_connection);
                SqliteDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    users.Add(new Users(reader["login"].ToString(), reader["password"].ToString())); // Use column names for clarity
                    Debug.Log($"{reader["login"]} {reader["password"]}");
                }
            }
            else
            {
                Debug.LogError("Error connecting to database");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception occurred: " + ex.Message);
        }
        finally
        {
            db_connection?.Close(); // Ensure the connection is closed
        }
    }

    private void InitializeDatabase()
    {
        
        if (!File.Exists(db_path))
        {
            using (var connection = new SqliteConnection("Data Source=" + db_path))
            {
                connection.Open();

             
                using (var command = connection.CreateCommand())
                {
                   
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Records (
                        login TEXT UNIQUE,
                        score INTEGER,
                    )";
                    command.ExecuteNonQuery();

                   
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        login TEXT UNIQUE,
                        password TEXT
                    )";
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public void InsertUser(string login, string password)
    {
        using (var connection = new SqliteConnection("Data Source=" + db_path))
        {
            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Users (login, password) VALUES (@login, @password)";
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error inserting user: " + ex.Message);
            }
        }
    }

    public bool ValidateUser(string username, string password)
    {
        using (var connection = new SqliteConnection("Data Source=" + db_path))
        {
            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM Users WHERE login = @login AND password = @password"; 
                    command.Parameters.AddWithValue("@login", username);
                    command.Parameters.AddWithValue("@password", password);
                    long userCount = (long)command.ExecuteScalar();
                    return userCount > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error validating user: " + ex.Message);
                return false;
            }
        }
    }

    public bool UserExists(string login)
    {
        using (var connection = new SqliteConnection("Data Source=" + db_path))
        {
            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM Users WHERE login = @login";
                    command.Parameters.AddWithValue("@login", login);
                    long userCount = (long)command.ExecuteScalar();
                    return userCount > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error checking if user exists: " + ex.Message);
                return false;
            }
        }
    }
}