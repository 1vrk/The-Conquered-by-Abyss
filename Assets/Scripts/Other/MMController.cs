using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMController : MonoBehaviour
{
    public Image image;

    public void StartGame()
    {
        StartCoroutine(FadeInImage(image, 1));
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        StartCoroutine(FadeInImage(image, 1));
        Application.Quit();
    }
    public void OpenRecords()
    {
        StartCoroutine(FadeInImage(image, 1));
        SceneManager.LoadScene(8);
    }
    public void Settings()
    {
        StartCoroutine(FadeInImage(image, 1));
        SceneManager.LoadScene(7);

    }
    public void LogOut()
    {
        StartCoroutine(FadeInImage(image, 1));
        SceneManager.LoadScene(0);
    }

    public static IEnumerator FadeInImage(Image image, float duration)
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }
}
