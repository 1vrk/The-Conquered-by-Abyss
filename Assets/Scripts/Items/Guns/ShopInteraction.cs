using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public ShopManager shopManager;

    void Start()
    {
        shopManager = FindAnyObjectByType<ShopManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopManager.OpenShop();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopManager.CloseShop();
        }
    }
}