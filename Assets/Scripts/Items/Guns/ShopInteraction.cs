using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public ShopManager shopManager;

    private AudioSource shop_sound;

    void Start()
    {
        shopManager = FindAnyObjectByType<ShopManager>();

        GameObject temp_sound = GameObject.Find("ShopSound");
        shop_sound = temp_sound.GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopManager.OpenShop();
            shop_sound.Play();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopManager.CloseShop();
            shop_sound.Play();
        }
    }
}