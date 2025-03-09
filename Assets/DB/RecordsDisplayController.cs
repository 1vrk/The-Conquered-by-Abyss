using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class RecordsDisplayController : MonoBehaviour
{
    private DatabaseRecordsManager databaseRecordsManager;

    [SerializeField] private TMP_Text score_txt;

    private void Start()
    {
        databaseRecordsManager = GetComponent<DatabaseRecordsManager>();
        LoadAndDisplayRecords();
    }

    private void LoadAndDisplayRecords()
    {
        List<PlayerRecord> records = LoadPlayerRecords();
        var sortedByDeaths = records.OrderByDescending(r => r.score_amount).Take(5).ToList();

        DisplayScore(sortedByDeaths);
    }

    private List<PlayerRecord> LoadPlayerRecords()
    {
        List<PlayerRecord> records = new List<PlayerRecord>();

        using (var connection = new SqliteConnection("URI=file:" + Application.dataPath + "/DB/conquered.db"))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT login, score FROM Records";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new PlayerRecord
                        {
                            login = reader.GetString(0),
                            score_amount = reader.GetInt32(1),
                        });
                    }
                }
            }
        }

        return records;
    }

    private void DisplayScore(List<PlayerRecord> records)
    {
        score_txt.text = "Top 5 Score Player:\n";
        foreach (var record in records)
        {
            score_txt.text += $"{record.login}: {record.score_amount}\n";
        }
    }
}

[System.Serializable]
public class PlayerRecord
{
    public string login;
    public int score_amount;
}