using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatsDisplay : MonoBehaviour
{
    public static CharacterStatsDisplay instance;
    public TMP_Text moveSpeedText;
    public TMP_Text fireRateText;
    public TMP_Text bulletSizeText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        DisplayStats();
    }

    public void DisplayStats()
    { 
        float def_move_speed = 3.3f;
        float def_fire_rate = 0.8f; 
        float def_bullet_size = 0.3f;

        moveSpeedText.text = (GameController.Move_Speed / def_move_speed * 100).ToString("F0") + "%";
        fireRateText.text = (-(GameController.Fire_Rate / def_fire_rate * 100)+200).ToString("F0") + "%";
        bulletSizeText.text = (GameController.Bullet_Size / def_bullet_size * 100).ToString("F0")  + "%";
    }

 
}
