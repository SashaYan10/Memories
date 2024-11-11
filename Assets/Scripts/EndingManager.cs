using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class EndingManager : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;

    private string dbPath;

    void Start()
    {
        dbPath = "URI=file:" + Application.dataPath + "/StreamingAssets/Endings.db";

        string currentSceneName = SceneManager.GetActiveScene().name;

        int endingId = 0;

        if (currentSceneName == "Ending1")
            endingId = 1;
        else if (currentSceneName == "Ending2")
            endingId= 2;
        else if (currentSceneName == "Ending3")
            endingId = 3;
        else if (currentSceneName == "Ending4")
            endingId = 4;

        LoadEndingData(endingId);
        UnlockEnding(endingId, currentSceneName);
    }

    void LoadEndingData(int endingId)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT title, description FROM Endings WHERE id = @id";
                command.Parameters.AddWithValue("@id", endingId);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        titleText.text = reader["title"].ToString();
                        descriptionText.text = reader["description"].ToString();
                    }
                }
            }
        }
    }

    void UnlockEnding(int endingId, string endingTitle)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT date_unlocked FROM GetEndings WHERE id = @id";
                command.Parameters.AddWithValue("@id", endingId);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read() || reader["date_unlocked"] == DBNull.Value)
                    {
                        reader.Close();

                        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        command.CommandText = "INSERT OR REPLACE INTO GetEndings (id, title, description, date_unlocked) VALUES (@id, @title, @description, @date)";
                        command.Parameters.AddWithValue("@title", titleText.text);
                        command.Parameters.AddWithValue("@description", descriptionText.text);
                        command.Parameters.AddWithValue("@date", currentDate);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
