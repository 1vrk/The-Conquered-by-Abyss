using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject coinPrefab;
    private GameObject player;
    public Animator animator;
    public int minHits; 
    public int maxHits;
    public int minCoins;
    public int maxCoins;
    private int hitCount = 0;
    private Color originalPlayerColor;
    private bool isOpened = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetOriginalPlayerColor();
    }
    public void SetOriginalPlayerColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        originalPlayerColor = spriteRenderer.color;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpened) return;
        if (collision.tag == "Bullet")
        {
            StartCoroutine(ShowHitEffect(GetComponent<SpriteRenderer>(), originalPlayerColor));

            hitCount++;
            if (hitCount >= Random.Range(minHits, maxHits + 1))
            {
                OpenChest();
            }
            Destroy(collision.gameObject);
        }
    }
    public static IEnumerator ShowHitEffect(SpriteRenderer spriteRenderer, Color originalColor)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.color = originalColor;
    }
    private void OpenChest()
    {
        isOpened = true;
        int outcome = Random.Range(0, 3); 
        switch (outcome)
        {
            case 0:
                animator.SetTrigger("OpenWithCoins");
                DropCoins();
                
                break;
            case 1:
                animator.SetTrigger("OpenEmpty");
                break;
            case 2:
                animator.SetTrigger("OpenWithDamage");
                GameController.DamagePlayer(2, player);
                
                break;
        }


        animator.Play("ChestOpenEnd");
    }

    private void DropCoins()
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1);
        float radius = 1f;

        for (int i = 0; i < coinCount; i++)
        {
           
            float angle = Random.Range(0f, 2 * Mathf.PI);
           

            
            float offsetX = radius * Mathf.Cos(angle);
            float offsetY = radius * Mathf.Sin(angle);

            Vector3 coinPosition = transform.position + new Vector3(offsetX, offsetY, 0);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }
    }
}
