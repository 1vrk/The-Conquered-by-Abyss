using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heart_container;
    private float fill_value;

    void Update()
    {
        fill_value = (float)GameController.Health;
        fill_value = fill_value / GameController.Max_Health;
        heart_container.GetComponent<Image>().fillAmount = fill_value;
    }
}
