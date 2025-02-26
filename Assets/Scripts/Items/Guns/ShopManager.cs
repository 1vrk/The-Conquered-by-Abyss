using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Button[] buyButtons;
    public GameObject[] bookPrefabs;
    public int[] price;
    private float[] fire_rate_modifier;
    private float[] move_speed_modifier;
    private float[] bullet_size_modifier;
    public CoinController coins;

    public TMP_Text[] priceTexts;
    public TMP_Text[] fireRateTexts;
    public TMP_Text[] moveSpeedTexts;
    public TMP_Text[] bulletSizeTexts;

    public Animator shopAnimator;

    void Start()
    {
        // Инициализация массивов
        price = new int[bookPrefabs.Length];
        fire_rate_modifier = new float[bookPrefabs.Length];
        move_speed_modifier = new float[bookPrefabs.Length];
        bullet_size_modifier = new float[bookPrefabs.Length];

        for (int i = 0; i < bookPrefabs.Length; i++)
        {
            BookStats bookStats = bookPrefabs[i].GetComponent<BookStats>();
            if (bookStats != null)
            {
                price[i] = bookStats.price;
                fire_rate_modifier[i] = bookStats.fire_rate_modifier;
                move_speed_modifier[i] = bookStats.move_speed_modifier;
                bullet_size_modifier[i] = bookStats.bullet_size_modifier;
            }
            else
            {
                Debug.LogError($"У префаба книги {bookPrefabs[i].name} отсутствует компонент BookStats!");
            }
        }

        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        float def_move_speed = 3.3f;
        float def_fire_rate = 0.8f;
        float def_bullet_size = 0.3f;

        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (priceTexts[i] != null)
                priceTexts[i].text = $"x {price[i]}";

            if (fireRateTexts[i] != null)
                fireRateTexts[i].text = (100 - (def_fire_rate / (def_fire_rate + fire_rate_modifier[i])) * 100).ToString("F0") + "%";

            if (moveSpeedTexts[i] != null)
                moveSpeedTexts[i].text = (move_speed_modifier[i] / def_move_speed * 100).ToString("F0") + "%";

            if (bulletSizeTexts[i] != null)
                bulletSizeTexts[i].text = ((bullet_size_modifier[i] / def_bullet_size) * 100).ToString("F0") + "%";

            if (GameController.Coin_balance >= price[i] && !coins.purchasedBooks.Contains(bookPrefabs[i]))
            {
                buyButtons[i].interactable = true;
                buyButtons[i].image.color = Color.white;
            }
            else
            {
                buyButtons[i].interactable = false;
                buyButtons[i].image.color = Color.gray;
            }
        }
    }

    public void BuyBook(int index)
    {
        if (coins.PurchaseBook(price[index], bookPrefabs[index]))
        {
            Debug.Log("Книга куплена!");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerBookManager playerBookManager = player.GetComponent<PlayerBookManager>();
            playerBookManager.ReplaceBook(bookPrefabs[index]);
            UpdateShopUI();
        }
        else
        {
            Debug.Log("Недостаточно средств!");
        }
    }

    public void OpenShop()
    {
        UpdateShopUI();
        shopAnimator.SetTrigger("ShopOpen");
    }

    public void CloseShop()
    {
        UpdateShopUI();
        shopAnimator.SetTrigger("ShopClose");
    }
}