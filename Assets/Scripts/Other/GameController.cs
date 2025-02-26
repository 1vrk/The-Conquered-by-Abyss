using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Префаб для частиц крови
    public GameObject bloodExplosionPrefab;

    private static float health = 6;
    private static int max_health = 6;
    private static float move_speed = 3.3f;
    private static float fire_rate = 0.8f;
    private static float bullet_size = 0.3f;
    private static float coin_balance = 0;
    private static float point_balance = 0;

    public static float Health { get => health; set => health = value; }
    public static int Max_Health { get => max_health; set => max_health = value; }
    public static float Move_Speed { get => move_speed; set => move_speed = value; }
    public static float Fire_Rate { get => fire_rate; set => fire_rate = value; }
    public static float Bullet_Size { get => bullet_size; set => bullet_size = value; }
    public static float Coin_balance { get => coin_balance; set => coin_balance = value; }
    public static float Point_balance { get => point_balance; set => point_balance = value; }

    public TMP_Text health_text;

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

       
       
        RoomController.instance.ClearLoadedRooms();
   
        DungeonCrawlerController.ClearVisitedPositions();

        SceneManager.LoadScene(0);


        ResetBalance();
        ResetCharacter();
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