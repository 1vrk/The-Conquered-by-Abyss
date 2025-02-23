using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float life_time;
    public bool is_enemy_bullet = false;
    private Vector2 last_pos;
    private Vector2 cur_pos;
    private Vector2 player_pos;
    private GameObject player;
    private Color color;
    private SpriteRenderer sprite;

void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DeathDelay());
        if(!is_enemy_bullet)
        {
            transform.localScale = new Vector2(GameController.Bullet_Size, GameController.Bullet_Size);
        }
        SetOriginalPlayerColor();
    }
    public void SetOriginalPlayerColor()
    {
        sprite = player.GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    void Update()
    {
        if(is_enemy_bullet)
        {
            cur_pos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, player_pos, 5f * Time.deltaTime);
            if(cur_pos == last_pos)
            {
                Destroy(gameObject);
            }
            last_pos = cur_pos;
        }
    }
    public void GetPlayer(Transform player)
    {
        player_pos = player.position;
    }
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(life_time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy" && !is_enemy_bullet)
        {
            collider.gameObject.GetComponent<EnemyController>().TakeDamage(1);
            StartCoroutine(ShowHitEffect(collider.gameObject.GetComponent<SpriteRenderer>()));
        }

        if(collider.tag == "Player" && is_enemy_bullet)
        {
            GameController.DamagePlayer(1, player);
            StartCoroutine(ShowHitEffect(collider.gameObject.GetComponent<SpriteRenderer>()));
            
        }
       
    }

    public IEnumerator ShowHitEffect(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = Color.red;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.4f); 
        spriteRenderer.color = color;
        Destroy(gameObject);

    }

}
