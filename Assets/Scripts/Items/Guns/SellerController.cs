using UnityEngine;
using System.Collections.Generic;

public class SellerController : MonoBehaviour
{
    public GameObject shop_panel; // UI ������ ��������
    public List<BookStats> available_books; // ������ ���� ����
    public List<BookStats> current_offers = new List<BookStats>(); // ������� �����������

    private bool is_shop_open = false;
    private GameObject player; // ������ �� ������
    private CoinController coin_controller; // ������ �� CoinController

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // ������� ������ �� ����
        if (player != null)
        {
            coin_controller = player.GetComponent<CoinController>(); // �������� ��������� CoinController
        }
        GenerateOffers();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenShop();
        }
    }

    void OpenShop()
    {
        is_shop_open = true;
        shop_panel.SetActive(true);
        UpdateUI();
    }

    void CloseShop()
    {
        is_shop_open = false;
        shop_panel.SetActive(false);
    }

    void GenerateOffers()
    {
        current_offers.Clear();
        foreach (var book in available_books)
        {
            if (Random.Range(0f, 1f) <= book.spawn_chance) // ���� ��������� �����
            {
                current_offers.Add(book);
            }
        }
    }

    void UpdateUI()
    {
        // ������ UI ������ � �������� �������������
        // ��������, �������� �������� ����, ���� � ��������������
    }

    public void BuyBook(BookStats book)
    {
        if (PlayerHasEnoughCoins(book.price))
        {
            //coin_controller.SpendCoins(book.price);
          
            ApplyBookEffects(book);
            CloseShop();
        }
        else
        {
            Debug.Log("������������ �����!");
        }
    }

    bool PlayerHasEnoughCoins(int price)
    {
      
        if (coin_controller != null)
        {
            return coin_controller.balance >= price;
        }
        return false;
    }

    void ApplyBookEffects(BookStats book)
    {

        PlayerBookManager player_movement = player.GetComponent<PlayerBookManager>();
        //ReplaceBook(book);

    }
}