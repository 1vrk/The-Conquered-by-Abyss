using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item : CollectionController
{
    public string name;
    public string description;
    public Sprite item_image;
}

public class CollectionController : MonoBehaviour
{
    public Item item;

    public float health_change;
    public float move_speed_change;
    public float attack_speed_change;
    public float bullet_size_change;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.item_image;
        Destroy(GetComponent<PolygonCollider2D>());
        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.tag == "Player")
        {
            GameController.HealPlayer(health_change);
            GameController.MoveSpeedChange(move_speed_change);
            GameController.FireRateChange(attack_speed_change);
            GameController.BulletSizeChange(bullet_size_change);
            Destroy(gameObject);
        }
    }
}
