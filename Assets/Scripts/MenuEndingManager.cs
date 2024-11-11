using Mono.Data.Sqlite;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEndingManager : MonoBehaviour
{
    public Button[] endingButtons;
    public Text dateText;
    public Text titleText;
    public Text descriptionText;
    private string dbPath;

    void Start()
    {
        dbPath = "URI=file:" + Application.dataPath + "/StreamingAssets/Endings.db";
        CheckProgress();
    }

    void CheckProgress()
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            foreach (Button button in endingButtons)
            {
                int endingId = int.Parse(button.name.Replace("EndingButton", ""));

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT title, description, date_unlocked FROM GetEndings WHERE id = @id";
                    command.Parameters.AddWithValue("@id", endingId);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && reader["date_unlocked"] != DBNull.Value)
                        {
                            string title = reader["title"].ToString();
                            string description = reader["description"].ToString();
                            string dateUnlocked = reader["date_unlocked"].ToString();

                            button.interactable = true;

                            button.onClick.AddListener(() => DisplayEnding(title, description, dateUnlocked));
                        }
                        else
                        {
                            button.interactable = false;
                        }
                    }
                }
            }
        }
    }

    void DisplayEnding(string title, string description, string date)
    {
        dateText.text = date;
        titleText.text = title;
        descriptionText.text = description;
    }

}
