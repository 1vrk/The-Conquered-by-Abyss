using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    public TMP_Text coin_balance;
    public float balance = 0;
    public List<GameObject> purchasedBooks = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            balance++;
            UpdateUI();
        }
    }
  

    public bool PurchaseBook(int cost, GameObject book)
    {
        if (balance >= cost)
        {
            balance -= cost;
            purchasedBooks.Clear();
            purchasedBooks.Add(book);
            UpdateUI();

            return true; 
        }
        return false; 
    }
    void UpdateUI()
    {
        coin_balance.text = "x " + balance;
    }

}
