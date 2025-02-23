using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float move_speed = 3.3f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;

    public GameObject bullet_prefab;
    public GameObject book_prefab;
    public float bullet_speed;
    private float last_fire;
    public float fire_delay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        fire_delay = GameController.Fire_Rate;
        move_speed = GameController.Move_Speed;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");


        float shoot_hor = Input.GetAxis("ShootingHorizontal");
        float shoot_ver = Input.GetAxis("ShootingVertical");


        if((shoot_hor != 0 || shoot_ver != 0) && Time.time > last_fire + fire_delay)
        {
            Shoot(shoot_hor, shoot_ver);
         
            last_fire = Time.time; 

        }





        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }

        anim.SetBool("Run", movement.x != 0 || movement.y != 0);
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate (bullet_prefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bullet_speed : Mathf.Ceil(x) * bullet_speed, 
            (y < 0) ? Mathf.Floor(y) * bullet_speed : Mathf.Ceil(y) * bullet_speed, 
            0
            );
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * move_speed * Time.fixedDeltaTime);
    }

    public void UpdateWeapon(GameObject new_bullet_prefab)
    {
        bullet_prefab = new_bullet_prefab;
    }
}
