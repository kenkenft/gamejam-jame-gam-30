using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerMain _playerMain;
    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Debug.Log("Collided with enemy");
            float physicalDamage = _playerHealth.GetMaxHealth() * (-col.gameObject.GetComponent<EnemyMain>().GetDamage()[0]/100f);
            _playerHealth.ModifyHealth(physicalDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Item")
        {
            col.gameObject.GetComponent<ItemHourGlass>().PickedUp();
            _playerMain.ChargePickUp();
        }
    }
}
