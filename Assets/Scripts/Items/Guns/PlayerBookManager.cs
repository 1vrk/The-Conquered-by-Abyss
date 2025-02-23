using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBookManager : MonoBehaviour
{
    public BookStats current_book;
    private GameController game_controller;
    public GameObject current_weapon;

    void Start()
    {
        Vector3 spawnPosition = new Vector3(0.738f, 0.152f, 0.0543f);
        current_weapon = Instantiate(current_weapon, spawnPosition, Quaternion.identity);
        current_weapon.transform.SetParent(transform);
    }


    public void ReplaceBook(GameObject newWeapon)
    {
       
        BookStats new_book = newWeapon.GetComponent<BookStats>();

        if (new_book == null)
        {
            Debug.LogError("Новое оружие не содержит компонента BookStats!");
            return; 
        }

      
            ResetBookBuffs(current_book);
        

        ReplaceWeapon(newWeapon);

        current_book = new_book;

        ApplyBookBuffs(current_book);

        Player_Movement player_movement = GetComponent<Player_Movement>();

        if (player_movement != null)
        {
            player_movement.UpdateWeapon(new_book.bullet_prefab);
        }
    }

    private void ReplaceWeapon(GameObject newWeapon)
    {
        if (current_weapon != null)
        {

            Vector3 position = current_weapon.transform.position;
            Quaternion rotation = current_weapon.transform.rotation;

            current_weapon.transform.SetParent(null);
            Destroy(current_weapon);

            current_weapon = Instantiate(newWeapon, position, rotation);
        }
        else
        {
            current_weapon = Instantiate(newWeapon, transform.position, Quaternion.identity);
        }
        BookMovement oldBookMovement = current_weapon.GetComponent<BookMovement>();
        if (oldBookMovement != null)
        {
            oldBookMovement.enabled = true;
        }
        current_weapon.transform.SetParent(transform);
    }
    private void ApplyBookBuffs(BookStats book)
    {
        GameController.FireRateChange(book.fire_rate_modifier);
        GameController.MoveSpeedChange(book.move_speed_modifier);
        GameController.BulletSizeChange(book.bullet_size_modifier);
    }

    private void ResetBookBuffs(BookStats book)
    {
        GameController.FireRateChange(-book.fire_rate_modifier);
        GameController.MoveSpeedChange(-book.move_speed_modifier);
        GameController.BulletSizeChange(-book.bullet_size_modifier);
    }
}
