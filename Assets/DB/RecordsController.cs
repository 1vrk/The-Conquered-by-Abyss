using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class RecordsController : MonoBehaviour
{
    private DatabaseRecordsManager databaseRecordsManager;
    public int score_amount;
    private string login;
    public static bool temp = true;
    private Coroutine timerCoroutine;

    void Start()
    {
        databaseRecordsManager = GetComponent<DatabaseRecordsManager>();
        login = UserManager.login;
        score_amount = databaseRecordsManager.LoadPlayerRecordsScore(login);

    }

    public void SaveRecords()
    {
        databaseRecordsManager.UpdatePlayerRecords(login, score_amount);
        Debug.Log("Records saved: score = " + score_amount);
    }
}