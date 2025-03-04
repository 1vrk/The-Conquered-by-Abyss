using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenRecords()
    {
        SceneManager.LoadScene(8);
    }
    public void Settings()
    {
        SceneManager.LoadScene(6);

    }
    public void LogOut()
    {
        SceneManager.LoadScene(7);
    }
}
