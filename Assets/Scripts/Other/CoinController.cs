using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    public TMP_Text coin_balance;
    public List<GameObject> purchasedBooks = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameController.Coin_balance++;
            UpdateUI();
        }
    }

    public bool PurchaseBook(int cost, GameObject book)
    {
        if (GameController.Coin_balance >= cost)
        {
            GameController.Coin_balance -= cost;
            purchasedBooks.Clear();
            purchasedBooks.Add(book);
            UpdateUI();

            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        coin_balance.text = "x " + GameController.Coin_balance;
    }
}