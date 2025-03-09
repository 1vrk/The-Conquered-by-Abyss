using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image image;
    void Start()
    {
        StartCoroutine(FadeOutImage(image, 1));
    }

    public static IEnumerator FadeOutImage(Image image, float duration)
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        image.gameObject.SetActive(false);
    }
}
