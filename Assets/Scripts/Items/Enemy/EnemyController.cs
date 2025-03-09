using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType
{
    Melee,
    Ranged
};

public class EnemyController : MonoBehaviour
{
    private AudioSource death_sound;
    private AudioSource hit_sound;

    public GameObject bloodExplosionPrefab; 
    GameObject player;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange;
    public float bulletSpeed;
    public float coolDown;
    public int minCoins; 
    public int maxCoins; 
    public GameObject coinPrefab; 
    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;
    public bool notInRoom = true;
    public int max_health = 3;

    public float initial_points;
    public float curr_points;
    private int current_health;


    private Vector3 randomDir;
    public GameObject bulletPrefab;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RoomController.instance.UpdateRooms();
        current_health = max_health;

        GameObject potionSoundObject = GameObject.Find("EnemyDeathSound");
        death_sound = potionSoundObject.GetComponent<AudioSource>();

        GameObject temp_sound = GameObject.Find("HitSound");
        hit_sound = temp_sound.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(player == null) return;

        if (notInRoom)
        {
            currState = EnemyState.Idle;
            return;
        }

        switch (currState)
        {
            case EnemyState.Wander:
                Wander();
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Die:
                // Die();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }

        if (IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }


        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && !coolDownAttack)
        {
            currState = EnemyState.Attack;
        }

        if (transform.position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    private bool IsPlayerInRange(float range)
    {
        if (player == null)
        {
            return false;
        }
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 3f));

        
        float angle = Random.Range(0f, 360f);
        randomDir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); 
        chooseDir = false;
    }

    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

    

        if (IsPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        if (player == null)
        {
            currState = EnemyState.Idle;
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        StartCoroutine(ReducePointsOverTime());
    }

    private IEnumerator ReducePointsOverTime()
    {
        while (currState == EnemyState.Follow)
        {
            yield return new WaitForSeconds(1f);
            curr_points -= initial_points * 0.1f;
            curr_points = Mathf.Max(curr_points, 0);
        }
    }

    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case EnemyType.Melee:
                    GameController.DamagePlayer(1, player);
                    StartCoroutine(CoolDown());
                    break;
                case EnemyType.Ranged:
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().is_enemy_bullet = true;

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.velocity = (player.transform.position - transform.position).normalized * bulletSpeed;

                    StartCoroutine(CoolDown());
                    break;
            }
        }
    }


    public static IEnumerator ShowHitEffect(SpriteRenderer spriteRenderer, Color originalColor)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void TakeDamage(int damage)
    {
        
        if (currState == EnemyState.Follow || currState == EnemyState.Attack)
        {
            current_health -= damage;
            hit_sound.Play();
            if (current_health <= 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        if (bloodExplosionPrefab != null)
        {
            Instantiate(bloodExplosionPrefab, transform.position, Quaternion.identity);
        }
        DropCoins();
        GivePointsToPlayer();
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
        death_sound.Play();
    }

    private void GivePointsToPlayer()
    {
        PointsController points_controller = player.GetComponent<PointsController>();
        curr_points = initial_points;
        if (points_controller != null)
        {
            points_controller.AddPoints((int)curr_points);
        }
    }
    private void DropCoins()
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1); 

        for (int i = 0; i < coinCount; i++)
        {

            float offsetX = Random.Range(-1f, 1f); 
            float offsetY = Random.Range(0f, 1f); 

        
            Vector3 coinPosition = transform.position + new Vector3(offsetX, offsetY, 0);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }
    }
}