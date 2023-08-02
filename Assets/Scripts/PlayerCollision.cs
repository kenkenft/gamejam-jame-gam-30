using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Debug.Log("Collided with enemy");
            float physicalDamage = _playerHealth.GetMaxHealth() * (-col.gameObject.GetComponent<EnemyMain>().GetDamage()[0]/100f);
            _playerHealth.ModifyHealth(physicalDamage);
        }
    }
}
