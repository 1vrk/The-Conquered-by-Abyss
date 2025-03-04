using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject bloodExplosionPrefab;

    private static float health = 6;
    private static int max_health = 6;
    private static float move_speed = 3.3f;
    private static float fire_rate = 0.8f;
    private static float bullet_size = 0.3f;
    private static float coin_balance = 0;
    private static float point_balance = 0;
    private static float max_point = 0;
    private static float max_coin_balance = 0;

    public static float Health { get => health; set => health = value; }
    public static int Max_Health { get => max_health; set => max_health = value; }
    public static float Move_Speed { get => move_speed; set => move_speed = value; }
    public static float Fire_Rate { get => fire_rate; set => fire_rate = value; }
    public static float Bullet_Size { get => bullet_size; set => bullet_size = value; }
    public static float Coin_balance { get => coin_balance; set => coin_balance = value; }
    public static float Point_balance { get => point_balance; set => point_balance = value; }
    public static float Max_Point { get => max_point; set => max_point = value; }
    public static float Max_coin { get => max_coin_balance; set => max_coin_balance = value; }


    public TMP_Text max_score_text;
    public TMP_Text death_score_text;

    public TMP_Text death;
    public Image image_death;
    public Animator deathAnimator;

    private Color originalPlayerColor;
    private SpriteRenderer spriteRenderer;
    private GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetOriginalPlayerColor();
        LoadBalance();
        LoadMaxBalance();
        
    }

    public void SetOriginalPlayerColor()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        originalPlayerColor = spriteRenderer.color;
    }

    public static void DamagePlayer(int damage, GameObject player)
    {
        health -= damage;
        instance.StartCoroutine(EnemyController.ShowHitEffect(player.GetComponent<SpriteRenderer>(), instance.originalPlayerColor));
        if (Health <= 0)
        {
            instance.StartCoroutine(instance.KillPlayerWithDelay());
        }
    }

    public static void HealPlayer(float heal_amount)
    {
        Health = Mathf.Min(max_health, Health + heal_amount);
        if (health < 0)
        {
            instance.StartCoroutine(instance.KillPlayerWithDelay());
        }

    }

    public static void MoveSpeedChange(float speed)
    {
        move_speed += speed;
    }

    public static void FireRateChange(float rate)
    {
        fire_rate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bullet_size += size;
    }

    private IEnumerator KillPlayerWithDelay()
    {
        if (bloodExplosionPrefab != null)
        {
            Instantiate(bloodExplosionPrefab, player.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Префаб частиц крови не назначен!");
        }

        Destroy(player);

        yield return new WaitForSeconds(1f);
        SaveMaxBalance();
        instance.max_score_text.text = "Best score: " + (int)max_point;
        StartCoroutine(FadeInText(death, 1));
        StartCoroutine(FadeInImage(image_death, 1));

    }
    public static IEnumerator FadeInText(TMP_Text text, float duration)
    {
        float startAlpha = text.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        instance.StartCoroutine(FadeOutText(text, 1));
    }
    public static IEnumerator FadeOutText(TMP_Text text, float duration)
    {
        float startAlpha = text.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        instance.OpenMenu();
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
    public void OpenMenu()
    {
        deathAnimator.SetTrigger("DeathOut");
       
        StartCoroutine(UpdateScoreText(death_score_text, point_balance, 1200f));
    }

    public static IEnumerator UpdateScoreText(TMP_Text scoreText, float targetScore, float speed)
    {
        float currentScore = 0f; 
        float increment = speed * Time.deltaTime; 

        while (currentScore < targetScore)
        {
            currentScore = Mathf.Min(currentScore + increment, targetScore);
            scoreText.text = "Score: " + Mathf.RoundToInt(currentScore).ToString(); 
            yield return null; 
        }

        scoreText.text = "Score: " + Mathf.RoundToInt(targetScore).ToString(); 
    }
    public void CloseMenu()
    {
        deathAnimator.SetTrigger("DeathBack");
    }

    public static void RestartGame()
    {
        instance.CloseMenu();
        RoomController.instance.ClearLoadedRooms();

        DungeonCrawlerController.ClearVisitedPositions();

        SceneManager.LoadScene(0);

        ResetBalance();
        ResetCharacter();
        instance.StartCoroutine(FadeOutImage(instance.image_death, 1));
    }
    public static void GoToMenu()
    {
        instance.CloseMenu();
        instance.CloseMenu();
        RoomController.instance.ClearLoadedRooms();
        DungeonCrawlerController.ClearVisitedPositions();
        ResetBalance();
        ResetCharacter();
        instance.StartCoroutine(FadeOutImage(instance.image_death, 1));

        SceneManager.LoadScene(5);
    }
    public static void SaveMaxBalance()
    {
        max_point = PlayerPrefs.GetFloat("MaxPointBalance", 0);
        Max_coin = PlayerPrefs.GetFloat("MaxCoinBalance", 0);
        if (Max_coin < coin_balance)
        {
            PlayerPrefs.SetFloat("MaxCoinBalance", coin_balance);
        }

        if (max_point < point_balance)
        {
            PlayerPrefs.SetFloat("MaxPointBalance", point_balance);
        }
        PlayerPrefs.Save();

    }

    public static void LoadMaxBalance()
    {
        max_point = PlayerPrefs.GetFloat("MaxPointBalance", 0);
        Max_coin = PlayerPrefs.GetFloat("MaxCoinBalance", 0);
    }
    public static void SaveBalance()
    {
        PlayerPrefs.SetFloat("CoinBalance", coin_balance);
        PlayerPrefs.SetFloat("PointBalance", point_balance);
        PlayerPrefs.Save();
    }

    private void LoadBalance()
    {
        coin_balance = PlayerPrefs.GetFloat("CoinBalance", 0);
        point_balance = PlayerPrefs.GetFloat("PointBalance", 0);
    }

    private static void ResetBalance()
    {
        PlayerPrefs.SetFloat("CoinBalance", 0);
        PlayerPrefs.SetFloat("PointBalance", 0);

        coin_balance = 0;
        point_balance = 0;
    }

    private static void ResetCharacter()
    {
        health = 6;
        max_health = 6;
        move_speed = 3.3f;
        fire_rate = 0.8f;
        bullet_size = 0.3f;
    }
}