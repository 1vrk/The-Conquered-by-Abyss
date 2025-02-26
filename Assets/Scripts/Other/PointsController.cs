using System.Collections;
using UnityEngine;
using TMPro;

public class PointsController : MonoBehaviour
{
    public TextMeshProUGUI balance_text;
    public float points_increase_speed = 800f;

    public void AddPoints(int points)
    {
        StartCoroutine(IncreasePointsOverTime(points));
    }

    private IEnumerator IncreasePointsOverTime(int target_points)
    {
        float start_points = GameController.Point_balance;
        float end_points = start_points + target_points;
        float duration = Mathf.Abs(end_points - start_points) / points_increase_speed;

        float elapsed_time = 0f;

        while (elapsed_time < duration)
        {
            GameController.Point_balance = Mathf.Lerp(start_points, end_points, elapsed_time / duration);
            UpdateBalanceText();
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        GameController.Point_balance = end_points;
        UpdateBalanceText();
    }

    private void UpdateBalanceText()
    {
        if (balance_text != null)
        {
            balance_text.text = "Score: " + Mathf.RoundToInt(GameController.Point_balance).ToString();
        }
    }
}