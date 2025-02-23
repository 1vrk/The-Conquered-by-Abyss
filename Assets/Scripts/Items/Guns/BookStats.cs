using UnityEngine;

public class BookStats : MonoBehaviour
{
    [Header("Book Stats")]
    public float fire_rate_modifier;
    public float move_speed_modifier;
    public float bullet_size_modifier;
    public int price;
    public float spawn_chance;

    [Header("Visuals")]
    public Sprite book_sprite;
    public GameObject bullet_prefab;
    
  
}